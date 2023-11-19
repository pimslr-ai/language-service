using System.Text.Json.Serialization;

namespace LanguageService.Services.TextToSpeech;

public partial class TextToSpeechService
{
    public record Voice
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("language")]
        public string? Language { get; init; }

        [JsonPropertyName("languageCode")]
        public string? LanguageCode { get; init; }

        [JsonPropertyName("styles")]
        public string[]? Styles { get; init; }
    }
}
