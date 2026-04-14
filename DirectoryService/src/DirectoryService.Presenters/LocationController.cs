using DirectoryService.Application.Location;
using DirectoryService.Application.Locations;
using DirectoryService.Contracts;
using DirectoryService.Contracts.Location;
using DirectoryService.Domain.Locations;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Presenter;

[ApiController]
[Route("/api/locations")]
public class LocationController : Controller
{
    private readonly ILocationService _locationService;
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationDto request, CancellationToken cancellationToken)
    {
        var locationId = await _locationService.Create(request, cancellationToken);
        return Ok($"Created Location with id: {locationId}");
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