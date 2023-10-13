using Microsoft.AspNetCore.Mvc;
using LanguageService.Services.Speech;
using LanguageService.Exceptions;

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

    [HttpPost("recognize")]
    public async Task<IActionResult> RecognizeSpeechFromAudio(IFormFile audio)
    {
        if (audio == null || audio.Length == 0)
        {
            throw new InvalidAudioSource();
        }

        var result = await service.RecognizeFromAudio(audio);

        return Ok(result);
    }
}
