using Dapper;
using Metrc.TaskManagement.Application.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Metrc.TaskManagement.Persistence.Services;

public class DbService : IDbService
{
    private readonly IDbConnection _db;

    public DbService(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("TaskManagementdb"));
    }

    public async Task<T?> GetAsync<T>(
        string command, 
        object? parms, 
        CancellationToken cancellationToken = default)
    {
        var commandDefinition = new CommandDefinition(
                commandText: command,
                parameters: parms,
                cancellationToken: cancellationToken
            );

        var result = (await _db
            .QueryAsync<T>(commandDefinition)
            .ConfigureAwait(false))
            .FirstOrDefault();

        return result;
    }

    public async Task<List<T>> GetListAsync<T>(
        string command, 
        object? parms,
        CancellationToken cancellationToken = default)
    {
        var commandDefinition = new CommandDefinition(
            commandText: command,
            parameters: parms,
            cancellationToken: cancellationToken
        );

        var result = (await _db
            .QueryAsync<T>(commandDefinition)
            .ConfigureAwait(false))
            .ToList();

        return result;
    }

    public async Task<int> EditDataAsync(
        string command, 
        object? parms,
        CancellationToken cancellationToken = default)
    {
        var commandDefinition = new CommandDefinition(
            commandText: command,
            parameters: parms,
            cancellationToken: cancellationToken
        );

        var result = await _db.ExecuteAsync(commandDefinition).ConfigureAwait(false);

        return result;
    }
}
