namespace LanguageService.Exceptions;

public class FailedPronunciationAssessment : HttpException
{
    public FailedPronunciationAssessment() 
        : base(500, "Failed to perfrom pronunciation assessement", "Could not assess your pronunciation...")
    {
    }
}
