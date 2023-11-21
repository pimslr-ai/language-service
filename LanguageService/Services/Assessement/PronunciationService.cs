using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;

namespace LanguageService.Services.Assessement;

public class PronunciationService : IPronunciationService
{
    private readonly string azureKey;
    private readonly string azureRegion;

    public PronunciationService(string azureKey, string azureRegion)
    {
        this.azureKey = azureKey;
        this.azureRegion = azureRegion;
    }

    public async Task<PronunciationAssessmentResult> AssessSpeechFromAudio(string language, string reference, string base64)
    {
        var configuration = SpeechConfig.FromSubscription(azureKey, azureRegion);
        configuration.RequestWordLevelTimestamps();

        using var audio = AudioConfig.FromWavFileInput(@"C:\Users\greff\Downloads\download.wav");

        using var recognizer = new SpeechRecognizer(configuration, language, audio);

        var pronunciation = new PronunciationAssessmentConfig(reference, GradingSystem.HundredMark, Granularity.Phoneme, true);
        pronunciation.EnableProsodyAssessment();

        pronunciation.ApplyTo(recognizer);

        var result = await recognizer.RecognizeOnceAsync();

        return PronunciationAssessmentResult.FromResult(result);
    }
}

public class PronunciationAssessment
{
    public string Id { get; set; }
    public int RecognitionStatus { get; set; }
    public long Offset { get; set; }
    public long Duration { get; set; }
    public string DisplayText { get; set; }
    public List<NBestItem> NBest { get; set; }
}

public class NBestItem
{
    public double Confidence { get; set; }
    public string Lexical { get; set; }
    public string ITN { get; set; }
    public string MaskedITN { get; set; }
    public string Display { get; set; }
    public PronunciationAssessment PronunciationAssessment { get; set; }
    public List<WordItem> Words { get; set; }
}

public class PronunciationAssessment
{
    public double AccuracyScore { get; set; }
    public double FluencyScore { get; set; }
    public double CompletenessScore { get; set; }
    public double PronScore { get; set; }
}

public class WordItem
{
    public string Word { get; set; }
    public long Offset { get; set; }
    public long Duration { get; set; }
    public PronunciationAssessment PronunciationAssessment { get; set; }
    public List<SyllableItem> Syllables { get; set; }
    public List<PhonemeItem> Phonemes { get; set; }
}

public class SyllableItem
{
    public string Syllable { get; set; }
    public PronunciationAssessment PronunciationAssessment { get; set; }
    public long Offset { get; set; }
    public long Duration { get; set; }
}

public class PhonemeItem
{
    public string Phoneme { get; set; }
    public PronunciationAssessment PronunciationAssessment { get; set; }
    public long Offset { get; set; }
    public long Duration { get; set; }
    public List<NBestPhonemeItem> NBestPhonemes { get; set; }
}

public class NBestPhonemeItem
{
    public string Phoneme { get; set; }
    public double Score { get; set; }
}
