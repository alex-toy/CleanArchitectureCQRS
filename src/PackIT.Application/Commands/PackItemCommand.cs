using System;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;

public record PackItemCommand(Guid PackingListId, string Name) : ICommand;