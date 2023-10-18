using Google.Cloud.Speech.V1P1Beta1;
using Google.Protobuf;
using LanguageService.Exceptions;
using System.Globalization;
using static Google.Cloud.Speech.V1P1Beta1.RecognitionConfig.Types;

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
        await using var data = file.OpenReadStream();
        return await RecognizeFromAudio(data, language);
    }

    private async Task<RecognizeResponse> RecognizeFromAudio(Stream data, string language)
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
            LanguageCode            = language,
            Encoding                = AudioEncoding.Mp3,
            SampleRateHertz         = 44_100,
            EnableWordConfidence    = true,
            EnableWordTimeOffsets   = true,
        };

        var audio = new RecognitionAudio
        {
            Content = await ByteString.FromStreamAsync(data),
        };

        var request = new RecognizeRequest
        {
            Config  = configuration,
            Audio   = audio,
        };

        return await client.RecognizeAsync(request);
    }

    private static bool IsValidLanguageCode(string code)
    {
        return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => culture.Name ==code);
    }
}
