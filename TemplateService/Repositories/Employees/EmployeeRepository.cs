using TemplateService.Models;
using TemplateService.Repositories.Connection;
using Dapper;

namespace TemplateService.Repositories.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IConnectionFactory connectionFactory;

    public EmployeeRepository(IConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<Employee> GetEmployeeById(Guid id)
    {
        await using var connection = connectionFactory.TryConnect();
        const string sql = @"SELECT * FROM Employees WHERE Id = @Id LIMIT 1";
        IEnumerable<Employee?> results = await connection.QueryAsync<Employee>(sql, new { Id = id });
        return results.FirstOrDefault();
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        await using var connection = connectionFactory.TryConnect();
        const string sql = @"SELECT * FROM Employees";
        return await connection.QueryAsync<Employee>(sql);
    }

    public async Task UpsertEmployee(Employee employee)
    {
        await using var connection = connectionFactory.TryConnect();
        const string sql = @"INSERT INTO Employee (""id"", ""first_name"", ""last_name"", ""first_screw_up"", ""created_at"")
            VALUES (@Id, @FirstName, @LastName, @FirstScrewUp, CURRENT_TIMESTAMP)
            ON CONFLICT (""id"") DO UPDATE
            SET
                ""first_name"" = EXCLUDED.""first_name"",
                ""last_name"" = EXCLUDED.""last_name"",
                ""first_screw_up"" = EXCLUDED.""first_screw_up"",
                ""updated_at"" = CURRENT_TIMESTAMP";
        await connection.ExecuteAsync(sql, employee);
    }
    
    public async Task DeleteEmployeeById(Guid id)
    {
        await using var connection = connectionFactory.TryConnect();
        const string sql = @"DELETE FROM Employees WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}

