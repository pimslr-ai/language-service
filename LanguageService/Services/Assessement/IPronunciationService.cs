using Microsoft.CognitiveServices.Speech.PronunciationAssessment;

namespace LanguageService.Services.Assessement;

public interface IPronunciationService
{
    Task<PronunciationAssessmentResult> AssessSpeechFromAudio(string language, string reference, string base64);
}