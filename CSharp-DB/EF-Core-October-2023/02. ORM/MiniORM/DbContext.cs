namespace MiniORM;

using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

public abstract class DbContext
{
    private readonly DatabaseConnection connection;
    private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

    protected DbContext(string connectionString)
    {
        this.connection = new DatabaseConnection(connectionString);
        this.dbSetProperties = this.DiscoverDbSets();

        using (new ConnectionManager(this.connection))
        {
            this.InitializeDbSets();
        }

        this.MapAllRelations();
    }

    internal static readonly Type[] AllowedSqlTypes =
    {
        typeof(string), // VARCHAR, NVARCHAR, CHAR, NCHAR
        typeof(int), // INT
        typeof(uint), // INT
        typeof(long), // BIGINT
        typeof(ulong), // BIGINT
        typeof(decimal), // DECIMAL
        typeof(bool), // BIT 
        typeof(DateTime) // DATETIME, DATETIME2
    };

    public void SaveChanges()
    {
        object[] dbSets = this.dbSetProperties
            .Select(pi => pi.Value.GetValue(this))
            .ToArray();

        foreach (IEnumerable<object> dbSet in dbSets)
        {
            IEnumerable<object> invalidEntities = dbSet // e => "entity"
                .Where(e => !IsObjectValid(e))
                .ToArray();

            if (invalidEntities.Any())
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.EntityNullException, invalidEntities.Count(),
                    dbSet.GetType().Name));
            }
        }

        using (new ConnectionManager(this.connection))
        {
            using var transaction = this.connection.StartTransaction();

            foreach (IEnumerable dbSet in dbSets)
            {
                var dbSetType = dbSet.GetType().GetGenericArguments().First();

                var persistMethod = typeof(DbContext)
                    .GetMethod("Persist", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType);

                try
                {
                    persistMethod.Invoke(this, new object[] { dbSet });
                }
                catch (TargetInvocationException tie)
                {
                    // No need of rollback because Persist<T> method was never invoked!
                    throw tie.InnerException;
                }
                catch (InvalidOperationException)
                {
                    transaction.Rollback();
                    throw;
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            transaction.Commit();
        }
    }

    private void Persist<TEntity>(DbSet<TEntity> dbSet)
        where TEntity : class, new()
    {
        string tableName = this.GetTableName(typeof(TEntity));
        string[] columns = this.connection
            .FetchColumnNames(tableName)
            .ToArray();

        if (dbSet.ChangeTracker.Added.Any())
        {
            this.connection.InsertEntities(dbSet.ChangeTracker.Added, tableName, columns);
        }

        IEnumerable<TEntity> modifiedEntities = dbSet.ChangeTracker
            .GetModifiedEntities(dbSet)
            .ToArray(); // Gets the "modifiedEntities"

        if (modifiedEntities.Any()) // If there are any "modifiedEntities"
        {
            this.connection.UpdateEntities(modifiedEntities, tableName,
                columns); // Update the database using "UpdateEntities" which accepts our "modifiedEntities", "tableName" and "columns"
        }

        if (dbSet.ChangeTracker.Removed.Any()) // If there are any removed entities in "Removed"(Collection) 
        {
            this.connection.DeleteEntities(dbSet.ChangeTracker.Removed, tableName, columns); // Delete them from the database
        }
    }

    private void InitializeDbSets()
    {
        foreach (KeyValuePair<Type, PropertyInfo> dbSet in this.dbSetProperties)
        {
            var dbSetType = dbSet.Key;
            var dbSetProperty = dbSet.Value;

            var populateDbSetGeneric = typeof(DbContext)
                .GetMethod("PopulateDbSet", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(dbSetType);

            populateDbSetGeneric.Invoke(this, new object[] { dbSetProperty });
        }
    }

    private void PopulateDbSet<TEntity>(PropertyInfo dbSet)
        where TEntity : class, new()
    {
        IEnumerable<TEntity> entities = this.LoadTableEntities<TEntity>();

        var dbSetInstance = new DbSet<TEntity>(entities);
        ReflectionHelper.ReplaceBackingField(this, dbSet.Name, dbSetInstance);
    }

    private void MapAllRelations() // All this method will do is call "MapRelations()" dynamically for each DB set property
    {
        foreach (KeyValuePair<Type, PropertyInfo> dbSetProperty in this.dbSetProperties)
        {
            var dbSetType = dbSetProperty.Key;

            var mapRelationsGeneric = typeof(DbContext)
                .GetMethod("MapRelations", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(dbSetType);

            object dbSet = dbSetProperty.Value.GetValue(this);

            mapRelationsGeneric.Invoke(this, new[] { dbSet });
        }
    }

    private void MapRelations<TEntity>(DbSet<TEntity> dbSet) // Maps all the relations of the DB set
        where TEntity : class, new()
    {
        var entityType = typeof(TEntity);
        this.MapNavigationProperties(dbSet);
        PropertyInfo[] collections = entityType.GetProperties()
            .Where(pi =>
                pi.PropertyType.IsGenericType &&
                pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection))
            .ToArray();

        foreach (var collection in collections)
        {
            var collectionType = collection.PropertyType
                .GenericTypeArguments
                .First();

            var mapCollectionMethod = typeof(DbContext)
                .GetMethod("MapCollection", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(entityType, collectionType);

            mapCollectionMethod.Invoke(this, new object[] { dbSet, collection });
        }
    }

    private void MapCollection<TEntity, TCollection>(DbSet<TEntity> dbSet, PropertyInfo collectionProperty)
        where TEntity : class, new()
        where TCollection : class, new()
    {
        var entityType = typeof(TEntity);
        var collectionType = typeof(TCollection);

        PropertyInfo[] primaryKeys = collectionType.GetProperties()
            .Where(pi => pi.HasAttribute<KeyAttribute>())
            .ToArray();

        var primaryKey = primaryKeys.First();
        var foreignKey = entityType.GetProperties()
            .First(pi => pi.HasAttribute<KeyAttribute>());

        bool isManyToMany = primaryKeys.Length >= 2;
        if (isManyToMany)
        {
            primaryKey = collectionType.GetProperties()
                .First(pi =>
                    collectionType
                        .GetProperty(pi.GetCustomAttribute<ForeignKeyAttribute>().Name)
                        .PropertyType == entityType);
        }

        var navigationDbSet = (DbSet<TCollection>)this.dbSetProperties[collectionType].GetValue(this);

        foreach (var entity in dbSet)
        {
            object primaryKeyValue = foreignKey.GetValue(entity);

            TCollection[] navigationEntities = navigationDbSet
                .Where(navigationEntity => primaryKey.GetValue(navigationEntity).Equals(primaryKeyValue))
                .ToArray();

            ReflectionHelper.ReplaceBackingField(entity, collectionProperty.Name, navigationEntities);
        }
    }

    private void MapNavigationProperties<TEntity>(DbSet<TEntity> dbSet)
        where TEntity : class, new()
    {
        var entityType = typeof(TEntity);

        PropertyInfo[] foreignKeys = entityType.GetProperties()
            .Where(pi => pi.HasAttribute<ForeignKeyAttribute>())
            .ToArray();

        foreach (var foreignKey in foreignKeys)
        {
            string navigationPropertyName = foreignKey
                .GetCustomAttribute<ForeignKeyAttribute>().Name;

            var navigationProperty = entityType
                .GetProperty(navigationPropertyName);

            object navigationDbSet = this.dbSetProperties[navigationProperty.PropertyType]
                .GetValue(this);

            var navigationPrimaryKey = navigationProperty.PropertyType
                .GetProperties()
                .First(pi => pi.HasAttribute<KeyAttribute>());

            foreach (var entity in dbSet)
            {
                object foreignKeyValue = foreignKey.GetValue(entity);

                object navigationPropertyValue = ((IEnumerable<object>)navigationDbSet)
                    .First(currentNavigationProperty => navigationPrimaryKey.GetValue(currentNavigationProperty).Equals(foreignKeyValue));

                navigationProperty.SetValue(entity, navigationPropertyValue);
            }
        }
    }

    // We do not use this Validator in real projects!
    private static bool IsObjectValid(object o)
    {
        var validationContext = new ValidationContext(o);
        var validationErrors = new List<ValidationResult>();

        bool validationResult = Validator.TryValidateObject(o, validationContext, validationErrors, true);

        return validationResult;
    }

    private IEnumerable<TEntity> LoadTableEntities<TEntity>()
        where TEntity : class, new()
    {
        var table = typeof(TEntity);
        string[] columns = this.GetEntityColumnNames(table);
        string tableName = this.GetTableName(table);

        IEnumerable<TEntity> fetchedRows = this.connection
            .FetchResultSet<TEntity>(tableName, columns)
            .ToArray();

        return fetchedRows;
    }

    private string GetTableName(Type tableType)
    {
        string tableName = tableType.GetCustomAttribute<TableAttribute>()?.Name ?? this.dbSetProperties[tableType].Name;

        return tableName;
    }

    private Dictionary<Type, PropertyInfo> DiscoverDbSets()
    {
        Dictionary<Type, PropertyInfo> dbSets = this.GetType().GetProperties()
            .Where(pi => pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .ToDictionary(pi => pi.PropertyType.GetGenericArguments().First(), pi => pi);

        return dbSets;
    }

    private string[] GetEntityColumnNames(Type table)
    {
        string tableName = this.GetTableName(table);
        IEnumerable<string> dbColumns = this.connection
            .FetchColumnNames(tableName)
            .ToArray();

        string[] columns = table.GetProperties()
            .Where(pi =>
                dbColumns.Contains(pi.Name) &&
                !pi.HasAttribute<NotMappedAttribute>() &&
                AllowedSqlTypes.Contains(pi.PropertyType))
            .Select(pi => pi.Name)
            .ToArray();

        return columns;
    }
}