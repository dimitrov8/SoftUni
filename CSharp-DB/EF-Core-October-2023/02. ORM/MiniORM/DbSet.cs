namespace MiniORM;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DbSet<TEntity> : ICollection<TEntity>
    where TEntity : class, new()
{
    internal ChangeTracker<TEntity> ChangeTracker { get; set; } // Deals with the tracking of changes
    internal IList<TEntity> Entities { get; set; } // Where we collect our entities

    internal DbSet(IEnumerable<TEntity> entities)
    {
        this.Entities = entities.ToList(); // Sets the entities
        this.ChangeTracker = new ChangeTracker<TEntity>(entities); // Creates a new ChangeTracker to keep track of changes in the entities
    }

    public void Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), ExceptionMessages.EntityNullException);
        }

        // If "entity" is not null 
        // => Add it to the "Entities" property
        // ==> And in to the "ChangeTracker" property
        this.Entities.Add(entity);
        this.ChangeTracker.Add(entity);
    }

    public bool Remove(TEntity entity)
    {
        if (entity == null) // If "entity" is null => Throw Exception
        {
            throw new ArgumentNullException(nameof(entity), ExceptionMessages.EntityNullException);
        }

        // Checks if an "entity" is removed successfully => (true/false)
        bool removedSuccessfully = this.Entities.Remove(entity);

        if (removedSuccessfully) // If "entity" is removed successfully 
        {
            this.ChangeTracker.Remove(entity); // Remove it from the "ChangeTracker"
        }

        return removedSuccessfully; // Return (true/false)
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities.ToArray())
        {
            this.Remove(entity);
        }
    }

    public bool Contains(TEntity entity)
        => this.Entities.Contains(entity);

    public void Clear()
    {
        // It's implemented like this so the "ChangeTracker" knows that all entities are removed
        while (this.Entities.Any())
        {
            var entity = this.Entities.First();
            this.Remove(entity);
        }
    }

    public void CopyTo(TEntity[] array, int arrayIndex)
        => this.Entities.CopyTo(array, arrayIndex);

    public IEnumerator<TEntity> GetEnumerator()
        => this.Entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    public int Count
        => this.Entities.Count;

    public bool IsReadOnly
        => this.Entities.IsReadOnly;
}