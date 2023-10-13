using LanguageService.Models;

namespace LanguageService.Repositories.Employees;

public interface IEmployeeRepository
{
    Task<Employee> GetEmployeeById(Guid id);

    Task UpsertEmployee(Employee employee);
    
    Task<IEnumerable<Employee>> GetEmployees();
    
    Task DeleteEmployeeById(Guid id);
}
