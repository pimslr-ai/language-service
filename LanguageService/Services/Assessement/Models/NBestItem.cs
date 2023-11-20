namespace LanguageService.Services.Assessement.Models;

public record NBestItem
{
    public double Confidence { get; set; }
    public string Lexical { get; set; }
    public string ITN { get; set; }
    public string MaskedITN { get; set; }
    public string Display { get; set; }
    public double PronScore { get; set; }
    public double AccuracyScore { get; set; }
    public double FluencyScore { get; set; }
    public double CompletenessScore { get; set; }
    public List<WordItem> Words { get; set; }
}
