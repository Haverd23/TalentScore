using TalentScore.Models;

namespace TalentScore.Repository
{
    public interface IResumeRepository
    {
        Task AddAsync(Resume resume);
    }
}
