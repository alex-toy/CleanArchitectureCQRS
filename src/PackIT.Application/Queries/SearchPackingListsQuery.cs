using System.Collections.Generic;
using PackIT.Application.DTO;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Application.Queries;

public class SearchPackingListsQuery : IQuery<IEnumerable<PackingListDto>>
{
    public string SearchPhrase { get; set; }
}