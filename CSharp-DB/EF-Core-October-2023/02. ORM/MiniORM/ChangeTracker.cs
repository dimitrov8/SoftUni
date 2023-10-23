namespace MiniORM;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

internal class ChangeTracker<T>
    where T : class, new()
{
    private readonly IList<T> allEntities;
    private readonly IList<T> added;
    private readonly IList<T> removed;

    /// <summary>
    ///     Constructor for the ChangeTracker class.
    ///     Initializes the lists for added and removed entities.
    /// </summary>
    public ChangeTracker()
    {
        this.added = new List<T>();
        this.removed = new List<T>();
    }

    // <summary>
    /// Constructor for the ChangeTracker class with initial entities.
    /// Clones the initial entities and initializes the lists for added and removed entities.
    /// </summary>
    /// <param name="allEntities">The initial entities to manage.</param>
    public ChangeTracker(IEnumerable<T> allEntities)
        : this()
    {
        this.allEntities = CloneEntities(allEntities); // Clone the initial entities.
    }

    // Properties
    public IReadOnlyCollection<T> AllEntities => (IReadOnlyCollection<T>)this.allEntities;

    public IReadOnlyCollection<T> Added => (IReadOnlyCollection<T>)this.added;

    public IReadOnlyCollection<T> Removed => (IReadOnlyCollection<T>)this.removed;

    // Methods
    public void Add(T entity) => this.added.Add(entity);

    public void Remove(T entity) => this.removed.Add(entity);

    private static IList<T> CloneEntities(IEnumerable<T> originalEntities) // Clones initial entities with allowed SQL types.
    {
        IList<T> clonedEntities = new List<T>();

        PropertyInfo[] propertiesToClone = typeof(T)
            .GetProperties()
            .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
            .ToArray(); // Get PropertyInfo objects for properties with allowed SQL types.

        foreach (var originalEntity in originalEntities) // Foreach "originalEntity".
        {
            var entityClone = Activator.CreateInstance<T>(); // Create a clone entity ("entityClone").
            foreach (var property in propertiesToClone) // Foreach "propertyToClone".
            {
                object originalValue = property.GetValue(originalEntity); // Get the value of the property in the "originalEntity".
                property.SetValue(entityClone, originalValue); // Set the same property value in the "entityClone" as the "originalEntity".
            }

            clonedEntities.Add(entityClone); // Add the "clonedEntity" in the "clonedEntities" List.
        }

        return clonedEntities; // Return the "clonedEntities" List.
    }

    public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet) // Get modified entities in a DbSet.
    {
        var modifiedEntities = new List<T>();

        PropertyInfo[] primaryKeys = typeof(T).GetProperties()
            .Where(pi => pi.HasAttribute<KeyAttribute>())
            .ToArray(); // Get the properties that serve as primary keys.


        foreach (var proxyEntity in this.AllEntities) // For each entity in the ChangeTracker's entity collection.
        {
            object[]
                primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray(); // Get the values of the primary key properties for the current entity (proxyEntity).

            // Find the corresponding entity in the DbSet using primary keys (originalEntity)
            var entity = dbSet.Entities
                .Single(e => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeyValues)); // e => entity.


            bool isModified = IsModified(proxyEntity, entity); // Check if the "proxyEntity" is modified compared to the "originalEntity".
            if (isModified) // If the entity is modified, add it to the list of modified entities.
            {
                modifiedEntities.Add(entity); // Add the "entity" in the "modifiedEntities" List.
            }
        }

        return modifiedEntities; // Return the "modifiedEntities" List.
    }

    private static IEnumerable<object>
        GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T entity) // Gets each primary key property's value.
        => primaryKeys.Select(pk => pk.GetValue(entity));

    private static bool IsModified(T proxyEntity, T originalEntity) // Check if there are modified properties between "proxyEntity" and "originalEntity".
    {
        PropertyInfo[] monitoredProperties = typeof(T)
            .GetProperties()
            .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
            .ToArray(); // Get PropertyInfo objects for properties with allowed SQL types

        PropertyInfo[] modifiedProperties = monitoredProperties
            .Where(pi => !Equals(pi.GetValue(proxyEntity), pi.GetValue(originalEntity)))
            .ToArray(); // Find modified properties by comparing proxyEntity and originalEntity

        return modifiedProperties.Any(); // Return (true/false) => If there are modified properties or not
    }
}