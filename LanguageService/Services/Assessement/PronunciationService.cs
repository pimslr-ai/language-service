using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Channels;

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
        byte[] bytes = Convert.FromBase64String(base64);
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid()}.wav");
        File.WriteAllBytes(filePath, bytes);

        var configuration = SpeechConfig.FromSubscription(azureKey, azureRegion);
        configuration.RequestWordLevelTimestamps();

        using var audio = AudioConfig.FromWavFileInput(filePath);
        using var recognizer = new SpeechRecognizer(configuration, language, audio);

        var pronunciation = new PronunciationAssessmentConfig(reference, GradingSystem.HundredMark, Granularity.Phoneme, true);
        pronunciation.EnableProsodyAssessment();
        pronunciation.ApplyTo(recognizer);

        var result = await recognizer.RecognizeOnceAsync();

        File.Delete(filePath);

        return PronunciationAssessmentResult.FromResult(result);
    }
}
