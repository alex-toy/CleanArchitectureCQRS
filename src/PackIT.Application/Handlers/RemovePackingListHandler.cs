using System.Threading.Tasks;
using PackIT.Application.Commands;
using PackIT.Application.Exceptions;
using PackIT.Domain.Entities;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Handlers;

internal sealed class RemovePackingListHandler : ICommandHandler<RemovePackingListCommand>
{
    private readonly IPackingListRepository _repository;

    public RemovePackingListHandler(IPackingListRepository repository) => _repository = repository;

    public async Task HandleAsync(RemovePackingListCommand command)
    {
        PackingList packingList = await _repository.GetAsync(command.Id);

        if (packingList is null) throw new PackingListNotFoundException(command.Id);

        await _repository.DeleteAsync(packingList);
    }
}