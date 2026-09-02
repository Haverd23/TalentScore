using TalentScore.DTOs;
using TalentScore.Services.Interfaces;

namespace TalentScore.Services
{
    public class ScoreService : IScoreService
    {
        public int Rate(ResumeAnalysisDTO score)
        {
            var education = score.EducationsCount * 10;
            var experience = score.ExperiencesCount * 8;
            var skils = score.SkillsCount * 4;

            var total = education + experience + skils;

            return total;
        }
    }
}
