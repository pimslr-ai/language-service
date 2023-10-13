using System.Text.Json.Serialization;
using TemplateService.Middlewares;
using TemplateService.Repositories.Connection;
using TemplateService.Repositories.Employees;
using TemplateService.Services.Employees;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddSingleton<IEmployeeRepository>(_ => new EmployeeRepository(ConnectionFactory.Of(builder.Configuration["ConnectionStrings:Employees"])));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<HttpExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
