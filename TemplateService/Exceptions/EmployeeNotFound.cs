namespace TemplateService.Exceptions;

public class EmployeeNotFound : HttpException
{
    public EmployeeNotFound(Guid? id = null) 
        : base(404, id != null ? "Could not find employee of id: " + id : "Employee not found", "Could not find anything on that employee...")
    {
        
    }
}