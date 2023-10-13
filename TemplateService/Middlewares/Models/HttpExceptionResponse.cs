using System.Text.Json.Serialization;

namespace TemplateService.Middlewares.Models;

public record HttpExceptionResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FriendlyMessage { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}