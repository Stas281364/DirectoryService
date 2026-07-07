using DirectoryService.Contracts.Location;

namespace DirectoryService.Application.Locations;

public record CreateLocationCommand(CreateLocationDto locationDto, CancellationToken cancellationToken);