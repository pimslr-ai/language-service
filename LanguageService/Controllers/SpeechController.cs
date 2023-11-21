using Microsoft.AspNetCore.Mvc;
using LanguageService.Services.Speech;
using LanguageService.Services.TextToSpeech;
using LanguageService.Services.Assessement;

namespace LanguageService.Controllers;

[ApiController]
[Route("speech")]
public class SpeechController : ControllerBase
{
    private readonly ISpeechToTextService speechToText;
    private readonly ITextToSpeechService textToSpeech;
    private readonly IPronunciationService pronunciation;

    public SpeechController(ISpeechToTextService speechToText, ITextToSpeechService textToSpeech, IPronunciationService pronunciation)
    {
        this.speechToText = speechToText ?? throw new ArgumentNullException(nameof(speechToText));
        this.textToSpeech = textToSpeech ?? throw new ArgumentNullException(nameof(textToSpeech));
        this.pronunciation = pronunciation ?? throw new ArgumentNullException(nameof(pronunciation));
    }

    [HttpPost("recognize/{language}")]
    public async Task<IActionResult> RecognizeSpeechFromAudio(string language, [FromBody] AudioBody? body)
    {
        return Ok(await speechToText.RecognizeFromAudio(language, body.Audio));
    }

    [HttpPost("generate/{language}")]
    public async Task<IActionResult> GenerateSpeechFromText(string language, [FromBody] TextBody? body)
    {
        return Ok(await textToSpeech.GenerateFromText(language, body.Text, body.Format));
    }

    [HttpPost("assess/{language}")]
    public async Task<IActionResult> AssessSpeechFromAudio(string language, [FromBody] AssessementBody? body)
    {
        return Ok(await pronunciation.AssessSpeechFromAudio(language, body.Reference, body.Audio));
    }
}

public record AudioBody(string Audio);

public record AssessementBody(string Audio, string Reference);

public record TextBody(string Text, string Format);
