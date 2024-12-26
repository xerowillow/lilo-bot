using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class SqliteDataStore(ILogger<SqliteDataStore> logger, IConfiguration configuration) : IDataStore, IHostedService {
    private SqliteConnection m_connection;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        m_connection = new SqliteConnection(configuration.GetValue<string>("ApplicationConfig:sql-connection-string"));

        await m_connection.OpenAsync(cancellationToken);

        logger.LogInformation("Connected to SQLite data store.");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await m_connection.CloseAsync();
    }
}