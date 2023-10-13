using System.Data.Common;
using Npgsql;
using Polly;

namespace TemplateService.Repositories.Connection;

public class ConnectionFactory : IConnectionFactory
{
    private readonly string connectionString;

    private ConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public static ConnectionFactory Of(string connectionString)
    {
        return new ConnectionFactory(connectionString);
    }

    public DbConnection TryConnect()
    {
        return Policy.Handle<Exception>().WaitAndRetry(5, GetWaitingTime).Execute(Connect);
    }

    private DbConnection Connect()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    private static TimeSpan GetWaitingTime(int attempt)
    {
        var backoff = Math.Pow(attempt, 2);
        var randomness = 500 + new Random().Next(500);
        return TimeSpan.FromMilliseconds(backoff * randomness);
    }
}