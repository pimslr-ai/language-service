using Swan;
using TemplateService.Exceptions;
using TemplateService.Models;
using TemplateService.Repositories.Employees;
using TemplateService.Services.Employees;

namespace TemplateService.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        this.repository = repository;
    }

    public Employee GetEmployeeById(Guid id)
    {
        var employee = repository.GetEmployeeById(id).Await();

        if (employee == null)
        {
            throw new EmployeeNotFound(id);
        }

        return employee;
    }

    public IEnumerable<Employee> GetEmployees()
    {
        return repository.GetEmployees().Await();
    }

    public void CreateOrUpdateEmployee(Employee employee)
    {
        repository.UpsertEmployee(employee).Await();
    }

    public void DeleteEmployeeById(Guid id)
    {
        repository.DeleteEmployeeById(id).Await();
    }
}
