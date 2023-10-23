namespace MiniORM;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

/// <summary>
///     The `DbContext` class serves as an abstract base class for managing interactions between C# objects and a
///     relational database.
///     It provides various features for configuring, persisting, and mapping data entities.
/// </summary>
public abstract class DbContext
{
    private readonly DatabaseConnection connection;
    private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

    /// <summary>
    ///     Initializes a new instance of the `DbContext` class with a database connection string.
    ///     It establishes a connection to the database and discovers DbSet properties representing tables.
    /// </summary>
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
    }; // Gets the allowed SQL data types for mapping.

    /// <summary>
    ///     Discovers and maps DbSet properties within the derived DbContext class.
    /// </summary>
    /// <returns>A dictionary of DbSet types and their corresponding PropertyInfo objects.</returns>
    private Dictionary<Type, PropertyInfo> DiscoverDbSets()
    {
        Dictionary<Type, PropertyInfo> dbSets = this.GetType().GetProperties()
            .Where(pi => pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .ToDictionary(pi => pi.PropertyType.GetGenericArguments().First(), pi => pi);

        return dbSets;
    }

    private void InitializeDbSets() // Initializes DbSet properties by populating them with entities loaded from the database.
    {
        foreach (KeyValuePair<Type, PropertyInfo> dbSet in this.dbSetProperties)
        {
            var dbSetType = dbSet.Key;
            var dbSetProperty = dbSet.Value;

            var populateDbSetGeneric = typeof(DbContext)
                .GetMethod("PopulateDbSet", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(dbSetType); // Using reflection to call the 'PopulateDbSet' method for the specific DbSet type.

            populateDbSetGeneric.Invoke(this,
                new object[] { dbSetProperty }); // Invoke the 'PopulateDbSet' method to populate the DbSet with entities.
        }
    }

    /// <summary>
    ///     Saves changes made to entities in DbSet instances.
    ///     It validates entities, handles transactions, and invokes appropriate persisting methods.
    /// </summary>
    public void SaveChanges()
    {
        object[] dbSets = this.dbSetProperties
            .Select(pi => pi.Value.GetValue(this))
            .ToArray(); // Get an array of DbSet instances from DbContext properties.

        foreach (IEnumerable<object> dbSet in dbSets)
        {
            IEnumerable<object> invalidEntities = dbSet // e => "entity".
                .Where(e => !IsObjectValid(e))
                .ToArray(); // Validate each entity within the DbSet.

            if (invalidEntities.Any()) // If invalid entities are found, throw an exception with a message.
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.INVALID_ENTITIES_EXCEPTION, invalidEntities.Count(),
                    dbSet.GetType().Name));
            }
        }

        using (new ConnectionManager(this.connection))
        {
            using var transaction = this.connection.StartTransaction();

            foreach (IEnumerable dbSet in dbSets) // For each DbSet, determine the entity type and its corresponding persisting method.
            {
                var dbSetType = dbSet.GetType().GetGenericArguments().First();

                var persistMethod = typeof(DbContext)
                    .GetMethod("Persist", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType);

                try
                {
                    persistMethod.Invoke(this, new object[] { dbSet }); // Invoke the appropriate persisting method for each entity type.
                }
                catch (TargetInvocationException tie)
                {
                    throw tie.InnerException; // If an exception occurs during the invocation, rethrow the inner exception.
                    // No need of rollback because Persist<T> method was never invoked!
                }
                catch (InvalidOperationException)
                {
                    transaction.Rollback(); // Roll back the transaction if an invalid operation exception occurs.
                    throw;
                }
                catch (SqlException)
                {
                    transaction.Rollback(); // Roll back the transaction if a SQL exception occurs.
                    throw;
                }
            }

            transaction.Commit(); // Commit the transaction after processing all DbSet instances.
        }
    }

    private void Persist<TEntity>(DbSet<TEntity> dbSet) // Manages the insertion, updating, and deletion of entities in the database for a given DbSet.
        where TEntity : class, new()
    {
        string tableName = this.GetTableName(typeof(TEntity));
        string[] columns = this.connection
            .FetchColumnNames(tableName)
            .ToArray();

        if (dbSet.ChangeTracker.Added.Any()) // Check if there are any added entities in the ChangeTracker.
        {
            this.connection.InsertEntities(dbSet.ChangeTracker.Added, tableName, columns); // Insert newly added entities into the database.
        }

        IEnumerable<TEntity> modifiedEntities = dbSet.ChangeTracker
            .GetModifiedEntities(dbSet)
            .ToArray(); // Gets the "modifiedEntities"

        if (modifiedEntities.Any()) // If there are any "modifiedEntities".
        {
            this.connection.UpdateEntities(modifiedEntities, tableName,
                columns); // Update the database using "UpdateEntities" which accepts our "modifiedEntities", "tableName" and "columns".
        }

        if (dbSet.ChangeTracker.Removed.Any()) // If there are any removed entities in "Removed"(Collection) .
        {
            this.connection.DeleteEntities(dbSet.ChangeTracker.Removed, tableName, columns); // Delete them from the database.
        }
    }

    private void PopulateDbSet<TEntity>(PropertyInfo dbSet) // Populates a DbSet property with entities loaded from the database.
        where TEntity : class, new()
    {
        IEnumerable<TEntity> entities = this.LoadTableEntities<TEntity>(); // Load entities of the specified entity type from the database.

        // Create a new DbSet instance and replace the backing field of the property with the loaded entities.
        var dbSetInstance = new DbSet<TEntity>(entities);
        ReflectionHelper.ReplaceBackingField(this, dbSet.Name, dbSetInstance);
    }

    private void MapRelations<TEntity>(DbSet<TEntity> dbSet) // Maps all the relations of a given DbSet, including one-to-many and many-to-many relationships.
        where TEntity : class, new()
    {
        var entityType = typeof(TEntity);
        this.MapNavigationProperties(dbSet); // Map navigation properties for the given entity.
        PropertyInfo[] collections = entityType.GetProperties()
            .Where(pi =>
                pi.PropertyType.IsGenericType &&
                pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection))
            .ToArray(); // Get all properties that represent collections (one-to-many or many-to-many relationships).

        foreach (var collection in collections)
        {
            var collectionType = collection.PropertyType
                .GenericTypeArguments
                .First(); // Get the type of elements in the collection property (e.g., the related entity type).

            // Use reflection to call the "MapCollection" method for mapping collection relationships.
            var mapCollectionMethod = typeof(DbContext)
                .GetMethod("MapCollection", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(entityType, collectionType);

            // Invoke the "MapCollection" method to map the collection relationship for the entity.
            mapCollectionMethod.Invoke(this, new object[] { dbSet, collection });
        }
    }

    private void MapAllRelations() // All this method will do is call "MapRelations()" dynamically for each DB set property.
    {
        foreach (KeyValuePair<Type, PropertyInfo> dbSetProperty in this.dbSetProperties)
        {
            var dbSetType = dbSetProperty.Key; // Get the type of the DbSet associated with the current property.

            // Get the "MapRelations" method via reflection for the specific entity type and invoke it.
            var mapRelationsGeneric = typeof(DbContext)
                .GetMethod("MapRelations", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(dbSetType);

            object dbSet = dbSetProperty.Value.GetValue(this); // Retrieve the DbSet instance for the property and map its relations.

            mapRelationsGeneric.Invoke(this, new[] { dbSet }); // Invoke the "MapRelations" method for the DbSet property.
        }
    }

    private void MapNavigationProperties<TEntity>(DbSet<TEntity> dbSet) // Maps navigation properties of a specific DB set.
        where TEntity : class, new()
    {
        var entityType = typeof(TEntity); // Get the type of the entity (TEntity).

        // Retrieve properties of the entity type marked with ForeignKeyAttribute.
        PropertyInfo[] foreignKeys = entityType.GetProperties()
            .Where(pi => pi.HasAttribute<ForeignKeyAttribute>())
            .ToArray();

        foreach (var foreignKey in foreignKeys)
        {
            string navigationPropertyName = foreignKey
                .GetCustomAttribute<ForeignKeyAttribute>().Name;

            // Get the related navigation property and associated DB set.
            var navigationProperty = entityType
                .GetProperty(navigationPropertyName);

            // Retrieve the DbSet associated with the navigation property's type and store it in 'navigationDbSet'.
            object navigationDbSet = this.dbSetProperties[navigationProperty.PropertyType]
                .GetValue(this);

            // Find the navigation property's primary key.
            var navigationPrimaryKey = navigationProperty.PropertyType
                .GetProperties()
                .First(pi => pi.HasAttribute<KeyAttribute>());

            foreach (var entity in dbSet)
            {
                object foreignKeyValue = foreignKey.GetValue(entity); // Get the foreign key value from the entity.

                // Find the related entity based on the foreign key value.
                object navigationPropertyValue = ((IEnumerable<object>)navigationDbSet)
                    .First(currentNavigationProperty => navigationPrimaryKey.GetValue(currentNavigationProperty).Equals(foreignKeyValue));

                navigationProperty.SetValue(entity, navigationPropertyValue); // Set the navigation property of the entity to the related entity.
            }
        }
    }

    private void MapCollection<TEntity, TCollection>(DbSet<TEntity> dbSet, PropertyInfo collectionProperty) // Maps a collection of related entities in a many-to-many relationship.
        where TEntity : class, new()
        where TCollection : class, new()
    {
        var entityType = typeof(TEntity); // Get the type of the entity (TEntity).
        var collectionType = typeof(TCollection); // Get the type of the related collection (TCollection).

        PropertyInfo[] primaryKeys = collectionType.GetProperties()
            .Where(pi => pi.HasAttribute<KeyAttribute>())
            .ToArray(); // Get the primary key property of the related collection type.

        var primaryKey = primaryKeys.First();  // Get the first primary key property.

        var foreignKey = entityType.GetProperties()
            .First(pi => pi.HasAttribute<KeyAttribute>()); // Get the foreign key property of the entity type.

        bool isManyToMany = primaryKeys.Length >= 2; // Determine if this is a many-to-many relationship by checking the number of primary keys.
        if (isManyToMany)
        {
            // In a many-to-many relationship, the primary key is determined based on a foreign key attribute.
            primaryKey = collectionType.GetProperties()
                .First(pi =>
                    collectionType
                        .GetProperty(pi.GetCustomAttribute<ForeignKeyAttribute>().Name)
                        .PropertyType == entityType);
        }

        var navigationDbSet = (DbSet<TCollection>)this.dbSetProperties[collectionType].GetValue(this); // Get the related collection's DbSet.

        foreach (var entity in dbSet)
        {
            object primaryKeyValue = foreignKey.GetValue(entity); // Get the primary key value from the foreign key property of the entity.
 
            // Find related entities in the collection based on the primary key value.
            TCollection[] navigationEntities = navigationDbSet
                .Where(navigationEntity => primaryKey.GetValue(navigationEntity).Equals(primaryKeyValue))
                .ToArray();

            // Replace the backing field of the collection property with the related entities.
            ReflectionHelper.ReplaceBackingField(entity, collectionProperty.Name, navigationEntities);
        }
    }

    private IEnumerable<TEntity> LoadTableEntities<TEntity>() // Loads entities from the database based on entity metadata and column definitions.
        where TEntity : class, new()
    {
        var table = typeof(TEntity); // Get the type information of the entity to be loaded.
        string[] columns = this.GetEntityColumnNames(table); // Retrieve the column names for the properties of the entity.
        string tableName = this.GetTableName(table); // Determine the table name associated with the entity type.

        // Fetch and load entities of the specified entity type from the database using the table name and columns.
        IEnumerable<TEntity> fetchedRows = this.connection
            .FetchResultSet<TEntity>(tableName, columns)
            .ToArray();

        return fetchedRows; // Return the loaded entities as an enumerable.
    }

    private string GetTableName(Type tableType) // Gets the table name associated with a specific entity type.
    {
        string tableName = tableType.GetCustomAttribute<TableAttribute>()?.Name ?? this.dbSetProperties[tableType].Name;

        return tableName;
    }

    private string[] GetEntityColumnNames(Type table) // Gets the column names for the properties of a specific entity type.
    {
        string tableName = this.GetTableName(table);

        // Retrieve the names of columns available in the associated table.
        IEnumerable<string> dbColumns = this.connection
            .FetchColumnNames(tableName)
            .ToArray();

        // Select the names of entity properties that match the available columns and are of allowed SQL types.
        string[] columns = table.GetProperties()
            .Where(pi =>
                dbColumns.Contains(pi.Name) &&
                !pi.HasAttribute<NotMappedAttribute>() &&
                AllowedSqlTypes.Contains(pi.PropertyType))
            .Select(pi => pi.Name)
            .ToArray();

        return columns;
    }

    private static bool IsObjectValid(object o) // Validates an object using data annotations, although it is not used in real projects.
    {
        var validationContext = new ValidationContext(o);
        var validationErrors = new List<ValidationResult>();

        bool validationResult = Validator.TryValidateObject(o, validationContext, validationErrors, true);

        return validationResult;
    }
}