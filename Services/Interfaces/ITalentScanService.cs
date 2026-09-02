namespace TalentScore.Services.Interfaces
{
    public interface ITalentScanService
    {
        Task AnalyzeResume(IFormFile file);
    }
}
