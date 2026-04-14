using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;

namespace DirectoryService.Application.Locations;
using Domain.Locations;

public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken); 
    //По id update?
    Task<Guid> UpdateAsync(Guid locationId, Location location,  CancellationToken cancellationToken);
    
    Task<Guid> DeleteAsync(Guid locationId, CancellationToken cancellationToken);
    
    Task<Location> GetByIdAsync(Guid locationId, CancellationToken cancellationToken);
}