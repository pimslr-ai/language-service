namespace LanguageService.Exceptions;

public class FailedAudioGeneration : HttpException
{
    public FailedAudioGeneration(string error = "") 
        : base(500, $"Failed to generate audio from text: {error}", "Could not generate audio for you...")
    {
    }
}
