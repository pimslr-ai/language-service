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
        this.service = service;
    }

    [HttpPost("recognize/{language}")]
    public async Task<IActionResult> RecognizeSpeechFromAudio(string language, IFormFile audio)
    {
        return Ok(await service.RecognizeFromAudio(audio, language));
    }
}
