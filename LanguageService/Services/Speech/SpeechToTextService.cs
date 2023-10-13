using Google.Cloud.Speech.V1;
using Google.Protobuf;
using static Google.Cloud.Speech.V1.RecognitionConfig.Types;

namespace LanguageService.Services.Speech;

public class SpeechToTextService : ISpeechToTextService
{
    private readonly SpeechClient client;

    public SpeechToTextService(SpeechClient client)
    {
        this.client = client;
    }

    public async Task<RecognizeResponse> RecognizeFromAudio(IFormFile audioFile)
    {
        var configuration = new RecognitionConfig
        {
            Encoding                = AudioEncoding.Linear16,
            SampleRateHertz         = 16_000,
            LanguageCode            = LanguageCodes.English.UnitedStates,
            EnableWordConfidence    = true,
            EnableWordTimeOffsets   = true,
        };

        using var stream = audioFile.OpenReadStream();

        var audio = new RecognitionAudio
        {
            Content = ByteString.FromStream(stream),
        };

        var request = new RecognizeRequest
        {
            Config  = configuration,
            Audio   = audio,
        };

        return await client.RecognizeAsync(request);
    }
}
