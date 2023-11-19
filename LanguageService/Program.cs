using System.Text.Json.Serialization;
using Google.Cloud.Speech.V1P1Beta1;
using LanguageService.Middlewares;
using LanguageService.Services.Speech;
using LanguageService.Services.TextToSpeech;
using OpenAI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Misc
builder.Services.AddControllers().AddJsonOptions(options =>
{
    var enumConverter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(enumConverter);
    options.AllowInputFormatterExceptionMessages = false;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Google Cloud - Speech-To-Text
builder.Services.AddScoped(_ => SpeechClient.Create());
builder.Services.AddScoped<ISpeechToTextService, SpeechToTextService>();

// OpenAI
builder.Services.AddOpenAIService(settings => { settings.ApiKey = builder.Configuration["OpenAI:ApiKey"]; });

// Narakeet - Text-To-Speech
builder.Services.AddScoped<ITextToSpeechService>(_ => new TextToSpeechService(builder.Configuration["Narakeet:ApiKey"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<HttpExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
