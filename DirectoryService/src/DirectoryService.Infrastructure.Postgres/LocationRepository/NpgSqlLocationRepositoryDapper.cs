using System.Data;
using System.Text.Json;
using Dapper;
using DirectoryService.Application.Locations;
using DirectoryService.Domain.Locations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;

namespace DirectoryService.Infrastructure.Postgres;


//Этот файл/класс для использования взаимодействия с БД через Dapper

public interface INpgSqlConnectionFactoryDapper
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
}

public class NpgSqlConnectionFactoryDapper : IDisposable, IAsyncDisposable, INpgSqlConnectionFactoryDapper
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly ILoggerFactory _loggerFactory;

    public NpgSqlConnectionFactoryDapper(IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("DsServiceDb"));
        dataSourceBuilder
            .UseLoggerFactory(CreateLoggerFactory()); // Configure logging
            //.UsePeriodicPasswordProvider() // Automatically rotate the password periodically
            //.UseNodaTime(); // Use NodaTime for date/time types
            _dataSource = dataSourceBuilder.Build();
            
    }
    
    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
    {
        return await _dataSource.OpenConnectionAsync(cancellationToken);
        
    }
    
    public ILoggerFactory CreateLoggerFactory() => LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

    

    public void Dispose()
    {
        _dataSource.Dispose();
        _loggerFactory.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dataSource.DisposeAsync();
        if (_loggerFactory is IAsyncDisposable loggerFactoryAsyncDisposable)
        {
            await loggerFactoryAsyncDisposable.DisposeAsync();
        }
        else
        {
            _loggerFactory.Dispose();
        }
    }
}
/// <summary>
/// //////////////////////////////////////////////
/// </summary>
public class NpgSqlLocationRepositoryDapper : ILocationRepository
{
    public readonly IConfiguration _configuration;
    public readonly INpgSqlConnectionFactoryDapper _connectionFactory;
    
    public NpgSqlLocationRepositoryDapper(INpgSqlConnectionFactoryDapper connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Guid> AddAsyncLocation(Location location, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
        
        const string query = """
                              INSERT INTO location ("Id", location_name, timezone, "Addresses", "is_active", created_at, updated_at)
                              VALUES (@Id, @Name, @Timezone, @Addresses::jsonb, @Is_active, @Created_at, @Updated_at)
                             """;

        var addressesJson = JsonSerializer.Serialize(location.Address);
        var locationInsertParam = new
        {
            Id = location.Id.value,
            Name = location.LocationName.Value,
            Timezone = location.TimeZone.Value,
            Addresses = addressesJson,
            Is_active = location.IsActive,
            Created_at = location.CreatedAt,
            Updated_at = location.UpdatedAt
        };
        //, param: new {Addresses = new NpgsqlParameter("Addresses", NpgsqlDbType.Jsonb)
        await connection.ExecuteAsync(query, locationInsertParam);
        
        /*var parameters = new DynamicParameters();

        // Добавляем все обычные параметры
        parameters.Add("@Id", location.Id.value);
        parameters.Add("@Name", location.LocationName.Value);
        parameters.Add("@Timezone", location.TimeZone.Value);

        // Добавляем Address с явным указанием типа NpgsqlDbType.Jsonb
        // Специальный параметр для JSONB
        var jsonParam = new NpgsqlParameter("@Addresses", NpgsqlDbType.Jsonb)
        {
            Value = JsonSerializer.Serialize(location.Address)
        };
        parameters.Add("@Addresses", location.Address );
        
        parameters.Add("@Is_Active", true);
        parameters.Add("@Created_at", DateTime.UtcNow);
        parameters.Add("@Updated_at", DateTime.UtcNow);

        await connection.ExecuteAsync(query, parameters);*/
        
        
        return location.Id.value;
    }

    public Task<Guid> UpdateAsync(Guid locationId, Location location, CancellationToken cancellationToken)
    {
        throw new Exception("Not Implemented");
    }

    public Task<Guid> DeleteAsync(Guid locationId, CancellationToken cancellationToken)
    {
        throw new Exception("Not Implemented");
    }
    
    public Task<Location> GetByIdAsync(Guid locationId, CancellationToken cancellationToken)
    {
        throw new Exception("Not Implemented");
    }
}