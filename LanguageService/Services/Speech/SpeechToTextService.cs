using Google.Cloud.Speech.V1;
using Google.Protobuf;
using LanguageService.Exceptions;
using System.Globalization;
using static Google.Cloud.Speech.V1.RecognitionConfig.Types;

namespace LanguageService.Services.Speech;

public class SpeechToTextService : ISpeechToTextService
{
    private readonly SpeechClient client;

    public SpeechToTextService(SpeechClient client)
    {
        this.client = client;
    }

    public async Task<RecognizeResponse> RecognizeFromAudio(IFormFile file, string language = "en-US")
    {
        using var data = file.OpenReadStream();
        return await RecognizeFromAudio(data, language);
    }

    public async Task<RecognizeResponse> RecognizeFromAudio(Stream data, string language)
    {
        if (!IsValidLanguageCode(language))
        {
            throw new InvalidLanguageCode(language);
        }

        if (data == null || data.Length == 0)
        {
            throw new InvalidAudioSource();
        }

        var configuration = new RecognitionConfig
        {
            Encoding                = AudioEncoding.Linear16,
            SampleRateHertz         = 16_000,
            LanguageCode            = language,
            EnableWordConfidence    = true,
            EnableWordTimeOffsets   = true,
        };

        var audio = new RecognitionAudio
        {
            Content = ByteString.FromStream(data),
        };

        var request = new RecognizeRequest
        {
            Config  = configuration,
            Audio   = audio,
        };

        return await client.RecognizeAsync(request);
    }
   
    public static bool IsValidLanguageCode(string code)
    {
        return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => culture.Name ==code);
    }
}
