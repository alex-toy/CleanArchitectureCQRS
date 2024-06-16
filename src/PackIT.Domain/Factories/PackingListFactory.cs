using System.Collections.Generic;
using System.Linq;
using PackIT.Domain.Entities;
using PackIT.Domain.Enums;
using PackIT.Domain.Policies;
using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Factories;

public sealed class PackingListFactory : IPackingListFactory
{
    private readonly IEnumerable<IPackingItemsPolicy> _policies;

    public PackingListFactory(IEnumerable<IPackingItemsPolicy> policies) => _policies = policies;

    public PackingList Create(PackingListId id, PackingListName name, Localization localization) => new(id, name, localization);
    
    public PackingList CreateWithDefaultItems(PackingListId id, PackingListName name, TravelDays days, Gender gender, Temperature temperature, Localization localization)
    {
        PolicyData data = new (days, gender, temperature, localization);
        IEnumerable<IPackingItemsPolicy> applicablePolicies = _policies.Where(p => p.IsApplicable(data));

        IEnumerable<PackingItem> items = applicablePolicies.SelectMany(p => p.GenerateItems(data));
        PackingList packingList = Create(id, name, localization);
        
        packingList.AddItems(items);

        return packingList;
    }
}