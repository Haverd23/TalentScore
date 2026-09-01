using Microsoft.EntityFrameworkCore;
using TalentScore.Models;

namespace TalentScore.Data
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
        }
        public DbSet<Resume> Resume { get; set; }
    }
}
