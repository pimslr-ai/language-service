using Microsoft.AspNetCore.Mvc;
using LanguageService.Services.Speech;
using LanguageService.Services.TextToSpeech;

namespace LanguageService.Controllers;

[ApiController]
[Route("speech")]
public class SpeechController : ControllerBase
{
    private readonly ISpeechToTextService speechToText;
    private readonly ITextToSpeechService textToSpeech;

    public SpeechController(ISpeechToTextService speechToText, ITextToSpeechService textToSpeech)
    {
        this.speechToText = speechToText ?? throw new ArgumentNullException(nameof(speechToText));
        this.textToSpeech = textToSpeech ?? throw new ArgumentNullException(nameof(textToSpeech));
    }

    [HttpPost("recognize/{language}")]
    public async Task<IActionResult> RecognizeSpeechFromAudio(string language, [FromBody] AudioBody? body)
    {
        return Ok(await speechToText.RecognizeFromAudio(language, body.Audio));
    }

    [HttpPost("generate/{language}")]
    public async Task<IActionResult> GenerateSpeechFromText(string language, [FromBody] TextBody? body)
    {
        return Ok(await textToSpeech.GenerateFromText(language, body.Text));
    }
}

public record AudioBody(string Audio);

public record TextBody(string Text);
