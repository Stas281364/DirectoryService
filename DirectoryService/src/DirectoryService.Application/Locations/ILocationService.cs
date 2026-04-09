using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using DirectoryService.Contracts;
using DirectoryService.Contracts.Location;

namespace DirectoryService.Application.Locations;

public interface ILocationService
{
    Task<Guid> Create(CreateLocationDto request, CancellationToken cancellationToken);
    Task UpdateById(Guid id, UpdateLocationDto locationDto, CancellationToken cancellationToken);
}