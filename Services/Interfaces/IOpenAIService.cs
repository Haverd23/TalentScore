using TalentScore.DTOs;

namespace TalentScore.Services.Interfaces
{
    public interface IOpenAIService
    {
        Task<ResumeAnalysisDTO> AnalyzeFileAsync(byte[] bytes, string contentType, string fileName);
    }
}
