using System;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;

public record RemovePackingItemCommand(Guid PackingListId, string Name) : ICommand;