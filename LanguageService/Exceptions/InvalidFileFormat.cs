namespace LanguageService.Exceptions;

public class InvalidFileFormat : HttpException
{
    public InvalidFileFormat() 
        : base(400, "Invalid file format. Possible file formats are m4a, wav, and mp3")
    {
    }
}
