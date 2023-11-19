namespace LanguageService.Exceptions;

public class EmptyString : HttpException
{
    public EmptyString() 
        : base(400, "Cound not generate audio as an empty string was sent.", "Could not generate audio for you...")
    {
    }
}
