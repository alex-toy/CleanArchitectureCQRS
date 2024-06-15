using System.Collections.Generic;
using System.Linq;

namespace PackIT.Shared.Abstractions.Domain;

public abstract class AggregateRoot<TypeId>
{
    public TypeId Id { get; protected set; }
    public int Version { get; protected set; }
    public IEnumerable<IDomainEvent> Events => _events;

    private readonly List<IDomainEvent> _events = new(); 
    private bool _versionIncremented;

    public void ClearEvents() => _events.Clear();

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any() && !_versionIncremented)
        {
            Version++;
            _versionIncremented = true;
        }
        
        _events.Add(@event);
    }

    protected void IncrementVersion()
    {
        if (_versionIncremented) return;

        Version++;
        _versionIncremented = true;
    }
}
