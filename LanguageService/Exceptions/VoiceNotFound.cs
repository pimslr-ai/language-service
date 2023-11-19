namespace LanguageService.Exceptions;

public class VoiceNotFound : HttpException
{
    public VoiceNotFound(string language = "")
        : base(500, $"Could not find voice for provided language: {language}", "Could not find someone to voice your audio...")
    {
    }
}
