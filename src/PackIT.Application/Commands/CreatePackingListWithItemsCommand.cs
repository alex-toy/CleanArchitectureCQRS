using System;
using PackIT.Domain.Enums;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;

public record CreatePackingListWithItemsCommand(Guid Id, string Name, ushort Days, Gender Gender, LocalizationWriteModel Localization) : ICommand;

public record LocalizationWriteModel(string City, string Country);