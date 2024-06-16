﻿using System.Threading.Tasks;
using PackIT.Application.Commands;
using PackIT.Application.Exceptions;
using PackIT.Domain.Entities;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Handlers;

internal sealed class RemovePackingItemHandler : ICommandHandler<RemovePackingItemCommand>
{
    private readonly IPackingListRepository _repository;

    public RemovePackingItemHandler(IPackingListRepository repository) => _repository = repository;

    public async Task HandleAsync(RemovePackingItemCommand command)
    {
        PackingList packingList = await _repository.GetAsync(command.PackingListId);

        if (packingList is null) throw new PackingListNotFoundException(command.PackingListId);

        packingList.RemoveItem(command.Name);

        await _repository.UpdateAsync(packingList);
    }
}