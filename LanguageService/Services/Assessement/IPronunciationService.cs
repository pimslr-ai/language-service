using Microsoft.CognitiveServices.Speech.PronunciationAssessment;

namespace LanguageService.Services.Assessement;

public interface IPronunciationService
{
    Task<PronunciationAssessment> AssessSpeechFromAudio(string language, string reference, string base64);
}