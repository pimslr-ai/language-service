namespace LanguageService.Exceptions;

public class InvalidAudioSource : HttpException
{
    public InvalidAudioSource() : base(400, "Invalid or missing audio file.")
    {

    }
}
