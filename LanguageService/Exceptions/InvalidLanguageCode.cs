namespace LanguageService.Exceptions;

public class InvalidLanguageCode : HttpException
{
    public InvalidLanguageCode(string? code = null) 
        : base(400, string.IsNullOrWhiteSpace(code) ? "Invalid language code provided." : $"`{code}` is not a valid language code.")
    {

    }
}
