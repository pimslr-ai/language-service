namespace LanguageService.Exceptions;

public class FailedPronunciationAssessement : HttpException
{
    public FailedPronunciationAssessement() 
        : base(500, "Failed to perfrom pronunciation assessement", "Could not assess your pronunciation...")
    {
    }
}
