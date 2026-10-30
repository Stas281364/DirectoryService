using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations;
using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres;

public class  LocationRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public LocationRepository(DirectoryServiceDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Guid> AddAsyncLocation(Location location, CancellationToken cancellationToken)
    {
        await _dbContext.Locations.AddAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return location.Id.value;
    }

    public Task<Guid> UpdateAsync(Guid locationId, Location location, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> DeleteAsync(Guid locationId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Location> GetByIdAsync(Guid locationId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<List<Guid>> GetAllExistingIdLocationAsync(List<Guid> requestIds, CancellationToken cancellationToken)
    {
        return await _dbContext.Locations
            .Where(l => requestIds.Contains(l.Id.value))
            .Select(l => l.Id.value)
            .ToListAsync(cancellationToken);
    }
}