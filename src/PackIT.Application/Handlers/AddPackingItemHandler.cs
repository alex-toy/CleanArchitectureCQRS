﻿using System.Threading.Tasks;
using PackIT.Application.Exceptions;
using PackIT.Domain.Entities;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers;

internal sealed class AddPackingItemHandler : ICommandHandler<AddPackingItemCommand>
{
    private readonly IPackingListRepository _repository;

    public AddPackingItemHandler(IPackingListRepository repository) => _repository = repository;

    public async Task HandleAsync(AddPackingItemCommand command)
    {
        PackingList packingList = await _repository.GetAsync(command.PackingListId);

        if (packingList is null)  throw new PackingListNotFoundException(command.PackingListId);

        PackingItem packingItem = new (command.Name, command.Quantity);
        packingList.AddItem(packingItem);

        await _repository.UpdateAsync(packingList);
    }
}