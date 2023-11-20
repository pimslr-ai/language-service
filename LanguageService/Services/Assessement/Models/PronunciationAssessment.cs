namespace LanguageService.Services.Assessement.Models;

public record PronunciationAssessment
{
    public string RecognitionStatus { get; set; }
    public string Offset { get; set; }
    public string Duration { get; set; }
    public List<NBestItem> NBest { get; set; }
}
