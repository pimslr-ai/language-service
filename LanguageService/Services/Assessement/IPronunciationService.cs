using LanguageService.Services.Assessement.Models;
namespace LanguageService.Services.Assessement;

public interface IPronunciationService
{
    Task<PronunciationAssessment> AssessPronunciationFromAudio(string language, string reference, string base64);
}