namespace CareerWay.Shared.Domain.Entities;

public abstract class Entity : IEntity
{
}

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    public virtual TKey Id { get; set; }

    protected Entity()
    {
        Id = default!;
    }

    protected Entity(TKey id)
    {
        Id = id;
    }
}
