﻿using System;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;

public record AddPackingItemCommand(Guid PackingListId, string Name, uint Quantity) : ICommand;