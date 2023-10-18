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

    public async Task<RecognizeResponse> RecognizeFromAudio(string language, string base64)
    {
        if (!IsValidLanguageCode(language))
        {
            throw new InvalidLanguageCode(language);
        }

        if (string.IsNullOrEmpty(base64))
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
            Content = ByteString.FromBase64(base64),
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
