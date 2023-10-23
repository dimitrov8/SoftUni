namespace MiniORM;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a collection of entities managed by the MiniORM framework.
/// </summary>
/// <typeparam name="TEntity">The type of entities stored in the collection.</typeparam>
public class DbSet<TEntity> : ICollection<TEntity>
    where TEntity : class, new()
{
    internal ChangeTracker<TEntity> ChangeTracker { get; set; } // Deals with the tracking of changes.
    internal IList<TEntity> Entities { get; set; } // Where we collect our entities.

    /// <summary>
    /// Initializes a new instance of the <see cref="DbSet{TEntity}"/> class with a collection of entities.
    /// </summary>
    /// <param name="entities">The initial collection of entities.</param>
    internal DbSet(IEnumerable<TEntity> entities)
    {
        this.Entities = entities.ToList(); // Sets the entities.
        this.ChangeTracker = new ChangeTracker<TEntity>(entities); // Creates a new ChangeTracker to keep track of changes in the entities.
    }

    /// <summary>
    /// Adds an entity to the collection.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public void Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), ExceptionMessages.ENTITY_NULL_EXCEPTION);
        }

        // If "entity" is not null 
        // => Add it to the "Entities" property
        // ==> And in to the "ChangeTracker" property.
        this.Entities.Add(entity);
        this.ChangeTracker.Add(entity);
    }

    /// <summary>
    /// Removes an entity from the collection.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    /// <returns><c>true</c> if the entity was removed successfully; otherwise, <c>false</c>.</returns>
    public bool Remove(TEntity entity)
    {
        if (entity == null) // If "entity" is null => Throw Exception.
        {
            throw new ArgumentNullException(nameof(entity), ExceptionMessages.ENTITY_NULL_EXCEPTION);
        }

        bool removedSuccessfully = this.Entities.Remove(entity);

        if (removedSuccessfully) 
        {
            this.ChangeTracker.Remove(entity); 
        }

        return removedSuccessfully;
    }

    /// <summary>
    /// Removes a range of entities from the collection.
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities.ToArray())
        {
            this.Remove(entity);
        }
    }

    /// <summary>
    /// Determines whether the collection contains a specific entity.
    /// </summary>
    /// <param name="entity">The entity to check for.</param>
    /// <returns><c>true</c> if the entity is found in the collection; otherwise, <c>false</c>.</returns>
    public bool Contains(TEntity entity)
        => this.Entities.Contains(entity);

    // Clears the collection of all entities.
    public void Clear()
    {
        // It's implemented like this so the "ChangeTracker" knows that all entities are removed.
        while (this.Entities.Any())
        {
            var entity = this.Entities.First();
            this.Remove(entity);
        }
    }

    /// <summary>
    /// Copies the elements of the collection to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    public void CopyTo(TEntity[] array, int arrayIndex)
        => this.Entities.CopyTo(array, arrayIndex);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator for the collection.</returns>
    public IEnumerator<TEntity> GetEnumerator()
        => this.Entities.GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator for the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    // Gets the number of entities in the collection.
    public int Count
        => this.Entities.Count;

    // Gets a value indicating whether the collection is read-only.
    public bool IsReadOnly
        => this.Entities.IsReadOnly;
}