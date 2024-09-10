﻿namespace FastCleanArchitecture.Domain.Common;

public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected BaseEntity(Guid id) => Id = id;

    protected BaseEntity()
    { }

    public Guid Id { get; init; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}