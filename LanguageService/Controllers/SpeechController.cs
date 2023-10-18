using Microsoft.AspNetCore.Mvc;
using LanguageService.Services.Speech;

namespace LanguageService.Controllers;

[ApiController]
[Route("speech")]
public class SpeechController : ControllerBase
{
    private readonly ISpeechToTextService service;

    public SpeechController(ISpeechToTextService service)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost("recognize/{language}")]
    public async Task<IActionResult> RecognizeSpeechFromAudio(string language, [FromBody] Body body)
    {
        return Ok(await service.RecognizeFromAudio(language, body.Audio));
    }
}

public record Body(string Audio);
