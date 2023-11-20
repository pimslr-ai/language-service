using LanguageService.Exceptions;
using LanguageService.Services.TextToSpeech.Models;
using System.Text;
using System.Text.Json;

namespace LanguageService.Services.TextToSpeech;

public partial class TextToSpeechService : ITextToSpeechService
{
    private static readonly string generationUrl = "https://api.narakeet.com/text-to-speech/m4a";
    private static readonly string voicesUrl = "https://api.narakeet.com/voices";
    private readonly HttpClient client = new();

    public TextToSpeechService(string apiKey)
    {
        client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        client.DefaultRequestHeaders.Add("accept", "application/octet-stream");
    }

    public async Task<AudioRecord> GenerateFromText(string language, string text)
    {
        var voice = await GetVoiceByLanguage(language);

        if (voice == null)
        {
            throw new VoiceNotFound(language);
        }
        
        var content = new StringContent(text, Encoding.UTF8, "text/plain");
        var url = generationUrl + $"?voice={Uri.EscapeDataString(voice.Name!)}";
        var response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsByteArrayAsync();
            var result = Encoding.UTF8.GetString(body);
            throw new FailedAudioGeneration(result);
        }

        var audio = await response.Content.ReadAsByteArrayAsync();
        var base64 = Convert.ToBase64String(audio);

        return new AudioRecord
        {
            Voice = voice.Name,
            Language = voice.LanguageCode,
            Text = text,
            Audio = base64,
        };
    }

    private async Task<Voice?> GetVoiceByLanguage(string languageCode)
    {
        var response = await client.GetAsync(voicesUrl);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsByteArrayAsync();
            var result = Encoding.UTF8.GetString(body);
            throw new FailedAudioGeneration(result);
        }

        var json = await response.Content.ReadAsStringAsync();
        var voices = JsonSerializer.Deserialize<List<Voice>>(json);

        var candidates = voices?.Where(l => l.LanguageCode == languageCode);
        
        return candidates?.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
    }
}
