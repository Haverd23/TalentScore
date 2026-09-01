using Microsoft.EntityFrameworkCore;
using TalentScore.Data;
using TalentScore.Repository;

namespace TalentScore.Extensions
{
    public static class DependencyInjectionConfig 
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IResumeRepository, ResumeRepository>();

            services.AddDbContext<appDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
