using Google.Cloud.Speech.V1;

namespace LanguageService.Services.Speech;

public interface ISpeechToTextService
{
    Task<RecognizeResponse> RecognizeFromAudio(IFormFile audioFile);
}