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

        var input = AudioInputStream.CreatePushStream();
        using var audio = AudioConfig.FromStreamInput(input);
        using var recognizer = new SpeechRecognizer(configuration, language, audio);

        var pronunciation = new PronunciationAssessmentConfig(reference, GradingSystem.HundredMark, Granularity.Phoneme, true);
        pronunciation.EnableProsodyAssessment();
        pronunciation.ApplyTo(recognizer);

        byte[] audioBytes = Convert.FromBase64String(base64);
        using var stream = new MemoryStream(audioBytes);

        int bytesRead;
        byte[] readBytes = new byte[1024];

        do
        {
            bytesRead = stream.Read(readBytes, 0, readBytes.Length);
            input.Write(readBytes, bytesRead);
        } while (bytesRead > 0);

        var result = await recognizer.RecognizeOnceAsync();

        return PronunciationAssessmentResult.FromResult(result);
    }
}
