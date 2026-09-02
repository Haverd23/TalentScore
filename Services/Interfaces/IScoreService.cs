using TalentScore.DTOs;

namespace TalentScore.Services.Interfaces
{
    public interface IScoreService
    {
        int Rate(ResumeAnalysisDTO score);
    }
}
