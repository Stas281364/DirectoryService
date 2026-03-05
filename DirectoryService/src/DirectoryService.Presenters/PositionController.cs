using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presenter;

[ApiController]
[Route("[controller]")]
public class PositionController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePositionDto positionDto, CancellationToken cancellationToken)
    {
        
        return Ok("Create Position");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CreatePositionDto positionDto, CancellationToken cancellationToken)
    {
        return Ok("Get Position");
    }
    
    [HttpGet("{positionId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid positionId, CancellationToken cancellationToken)
    {
        return Ok($"Get PositionById {positionId}");
    }
    [HttpPut("{positionId:guid}")]
    public async Task<IActionResult> UpdateById([FromBody]UpdatePositionDto updatePositionDto,[FromRoute] Guid positionId, CancellationToken cancellationToken)
    {
        return Ok($"Put UpdatePositionById {positionId}");
    }
    
    [HttpDelete("{positionId:guid}")]
    
    public async Task<IActionResult> DeleteById([FromRoute] Guid positionId, CancellationToken cancellationToken)
    {
        return Ok($"Delete DeleteLocationById {positionId}");
    }
}