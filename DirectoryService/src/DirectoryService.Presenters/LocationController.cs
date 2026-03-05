using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presenter;

[ApiController]
[Route("[controller]")]
public class LocationController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        
        return Ok("Create Location");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        return Ok("Get Location");
    }
    
    [HttpGet("{locationId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid locationId, CancellationToken cancellationToken)
    {
        return Ok($"Get LocationById {locationId}");
    }
    [HttpPut("{locationId:guid}")]
    public async Task<IActionResult> UpdateById([FromBody]UpdateLocationDto updateLocationDto,[FromRoute] Guid locationId, CancellationToken cancellationToken)
    {
        return Ok($"Put UpdateLocationById {locationId}");
    }
    
    [HttpDelete("{LocationId:guid}")]
    
    public async Task<IActionResult> DeleteById([FromRoute] Guid LocationId, CancellationToken cancellationToken)
    {
        return Ok($"Delete DeleteLocationById {LocationId}");
    }
}