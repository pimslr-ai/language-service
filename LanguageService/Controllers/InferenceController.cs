using Microsoft.AspNetCore.Mvc;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.ResponseModels;

namespace LanguageService.Controllers;

[ApiController]
[Route("infer")]
public class InferenceController : ControllerBase
{
    private readonly IOpenAIService service;

    public InferenceController(IOpenAIService service)
    {
        this.service = service;
    }

    [HttpPost("prompt")]
    public async Task<IActionResult> InferFromPrompt([FromForm] string prompt, [FromQuery] bool asJson = true)
    {
        return Ok(await GetCompletion(prompt, asJson));
    }

    private async Task<ChatCompletionCreateResponse> GetCompletion(string prompt, bool asJson)
    {
        var role = asJson ? "You are a helpful assistant designed to output JSON." : $"You are a helpful assistant.";

        var request = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(role),
                ChatMessage.FromUser(prompt),
            },
            Model = Models.Gpt_3_5_Turbo_1106,
            ResponseFormat = new ResponseFormat { Type = asJson ? "json_object" : "text" }
        };

        return await service.ChatCompletion.CreateCompletion(request);
    }
}
