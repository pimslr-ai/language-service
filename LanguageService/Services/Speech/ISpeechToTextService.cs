using Google.Cloud.Speech.V1P1Beta1;

namespace LanguageService.Services.Speech;

public interface ISpeechToTextService
{
    Task<RecognizeResponse> RecognizeFromAudio(IFormFile data, string language);
}