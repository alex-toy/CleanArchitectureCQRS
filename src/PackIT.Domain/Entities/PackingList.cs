using PackIT.Domain.Events;
using PackIT.Domain.Exceptions.PackingItemExceptions;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PackIT.Domain.Entities;

public class PackingList : AggregateRoot<PackingListId>
{
    public PackingListId Id { get; private set; }

    private PackingListName _name;
    private Localization _localization;
    private readonly LinkedList<PackingItem> _items = new();

    private PackingList(PackingListId id, PackingListName name, Localization localization, LinkedList<PackingItem> items)
        : this(id, name, localization)
    {
        _items = items;
    }

    private PackingList()
    {
    }

    internal PackingList(PackingListId id, PackingListName name, Localization localization)
    {
        Id = id;
        _name = name;
        _localization = localization;
    }

    public void AddItem(PackingItem item)
    {
        bool alreadyExists = _items.Any(i => i.Name == item.Name);

        if (alreadyExists)
        {
            throw new PackingItemAlreadyExistsException(_name, item.Name);
        }

        _items.AddLast(item);
        AddEvent(new PackingItemAdded(this, item));
    }

    public void AddItems(IEnumerable<PackingItem> items)
    {
        foreach (var item in items) AddItem(item);
    }

    public void PackItem(string itemName)
    {
        PackingItem item = GetItem(itemName);
        PackingItem packedItem = item with { IsPacked = true };

        _items.Find(item).Value = packedItem;
        AddEvent(new PackingItemPacked(this, item));
    }

    public void RemoveItem(string itemName)
    {
        PackingItem item = GetItem(itemName);
        _items.Remove(item);
        AddEvent(new PackingItemRemoved(this, item));
    }

    private PackingItem GetItem(string itemName)
    {
        PackingItem item = _items.SingleOrDefault(i => i.Name == itemName);

        if (item is null)  throw new PackingItemNotFoundException(itemName);

        return item;
    }
}