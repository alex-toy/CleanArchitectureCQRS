using System;
using PackIT.Application.DTO;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Application.Queries;

public class GetPackingListQuery : IQuery<PackingListDto>
{
    public Guid Id { get; set; }
}