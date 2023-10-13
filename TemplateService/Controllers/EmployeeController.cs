using Microsoft.AspNetCore.Mvc;
using TemplateService.Models;
using TemplateService.Services.Employees;

namespace TemplateService.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService service;

    public EmployeeController(IEmployeeService service)
    {
        this.service = service;
    }
    
    [HttpGet("{id}")]
    public Employee GetById(Guid id)
    {
        return service.GetEmployeeById(id);
    }

    [HttpGet("list")]
    public IEnumerable<Employee> GetList()
    {
        return service.GetEmployees();
    }

    [HttpPost("create")]
    public void Create([FromBody] Employee employee)
    {
        service.CreateOrUpdateEmployee(employee);
    }

    [HttpDelete("remove")]
    public void Delete(Guid id)
    {
        service.DeleteEmployeeById(id);
    }
}
