using LanguageService.Services.TextToSpeech.Models;
namespace LanguageService.Services.TextToSpeech;

public interface ITextToSpeechService
{
    Task<AudioRecord> GenerateFromText(string language, string text);
}
