using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PackIT.Application.Commands;
using PackIT.Application.DTO;
using PackIT.Application.Queries;
using PackIT.Shared.Abstractions.Commands;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Api.Controllers;

public class PackingListsController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public PackingListsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PackingListDto>> Get([FromRoute] GetPackingListQuery query)
    {
        PackingListDto result = await _queryDispatcher.QueryAsync(query);
        return OkOrNotFound(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PackingListDto>>> Get([FromQuery] SearchPackingListsQuery query)
    {
        IEnumerable<PackingListDto> result = await _queryDispatcher.QueryAsync(query);
        return OkOrNotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePackingListWithItemsCommand command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
    }
    
    [HttpPut("{packingListId}/items")]
    public async Task<IActionResult> Put([FromBody] AddPackingItemCommand command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
    
    [HttpPut("{packingListId:guid}/items/{name}/pack")]
    public async Task<IActionResult> Put([FromBody] PackItemCommand command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
    
    [HttpDelete("{packingListId:guid}/items/{name}")]
    public async Task<IActionResult> Delete([FromBody] RemovePackingItemCommand command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromBody] RemovePackingListCommand command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}