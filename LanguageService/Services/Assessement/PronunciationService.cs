using LanguageService.Exceptions;
using LanguageService.Services.Assessement.Models;
using Newtonsoft.Json;
using System.Text;

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

    public async Task<PronunciationAssessment> AssessPronunciationFromAudio(string language, string reference, string base64)
    {
        var accessToken = await GetAccessToken(azureKey, azureRegion);

        var apiUrl = $"https://{azureRegion}.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1";

        var pronunciationParams = new
        {
            ReferenceText               = reference,
            GradingSystem               = "HundredMark",
            Dimension                   = "Comprehensive",
            EnableProsodyAssessment     = true,
        };

        var jsonParams = JsonConvert.SerializeObject(pronunciationParams);
        var base64Params = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonParams));

        var content = new StringContent(base64, Encoding.UTF8);

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        client.DefaultRequestHeaders.Add("Pronunciation-Assessment", base64Params);

        var response = await client.PostAsync($"{apiUrl}?language={language}&format=detailed", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new FailedPronunciationAssessement();
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PronunciationAssessment>(json)!;
    }

    private static async Task<string> GetAccessToken(string azureKey, string azureRegion)
    {
        var fetchTokenUrl = $"https://{azureRegion}.api.cognitive.microsoft.com/sts/v1.0/issueToken";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", azureKey);
        var result = await client.PostAsync(fetchTokenUrl, null);
        return await result.Content.ReadAsStringAsync();
    }
}
