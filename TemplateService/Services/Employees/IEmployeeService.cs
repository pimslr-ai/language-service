using TemplateService.Models;

namespace TemplateService.Services.Employees;

public interface IEmployeeService
{
    Employee GetEmployeeById(Guid id);

    IEnumerable<Employee> GetEmployees();

    void CreateOrUpdateEmployee(Employee employee);
    
    void DeleteEmployeeById(Guid id);
}