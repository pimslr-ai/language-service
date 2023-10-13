namespace LanguageService.Exceptions;

public class InvalidAudioSource : HttpException
{
    public InvalidAudioSource() 
        : base(400, "Unreadable or missing audio file.")
    {

    }
}
