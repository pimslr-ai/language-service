namespace LanguageService.Services.TextToSpeech.Models;

public record AudioRecord
{
    public string? Voice { get; set; }
    public string? Language { get; set; }
    public string? Text { get; set; }
    public string? Audio { get; set; }
}