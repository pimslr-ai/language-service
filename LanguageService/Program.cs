using System.Text.Json.Serialization;
using Google.Cloud.Speech.V1;
using LanguageService.Middlewares;
using LanguageService.Services.Speech;

var builder = WebApplication.CreateBuilder(args);

// Misc
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Google Cloud - Speech-To-Text
builder.Services.AddScoped(_ => SpeechClient.Create());

// Services
builder.Services.AddScoped<ISpeechToTextService, SpeechToTextService>();

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
