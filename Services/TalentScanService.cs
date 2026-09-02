using TalentScore.Models;
using TalentScore.Repository;
using TalentScore.Services.Interfaces;

namespace TalentScore.Services
{
    public class TalentScanService : ITalentScanService
    {
        private readonly IResumeRepository _repository;
        private readonly IFileService _file;
        private readonly IOpenAIService _openAI;
        private readonly IScoreService _score;

        public TalentScanService(IResumeRepository repository, IFileService file,
            IOpenAIService openAI, IScoreService score)
        {
            _repository = repository;
            _file = file;
            _openAI = openAI;
            _score = score;
        }

        public async Task AnalyzeResume(IFormFile file)
        {
            _file.ValidateFile(file);
            var contentType = _file.GetContentType(file);
            var bytes = await _file.GetBytesAsync(file);

            var airesult = await _openAI.AnalyzeFileAsync(bytes, contentType, file.FileName);

            var score = _score.Rate(airesult);

            if(score > 30)
            {
                var resume = new Resume(airesult.Name, airesult.Email, airesult.Phone, score);
                await _repository.AddAsync(resume);
                
            }
            
            
            
        }
    }
}
