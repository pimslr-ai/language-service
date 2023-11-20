namespace LanguageService.Services.Assessement.Models;

public record WordItem
{
    public string Word { get; set; }
    public double AccuracyScore { get; set; }
    public string ErrorType { get; set; }
    public string Offset { get; set; }
    public string Duration { get; set; }
}
