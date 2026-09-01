using TalentScore.Data;
using TalentScore.Models;

namespace TalentScore.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly appDbContext _context;
        public ResumeRepository(appDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Resume resume)
        {
            _context.Resume.Add(resume);
            return _context.SaveChangesAsync();
        }
    }
}
