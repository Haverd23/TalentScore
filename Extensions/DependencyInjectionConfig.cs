#pragma warning disable OPENAI001

using Microsoft.EntityFrameworkCore;
using OpenAI.Responses;
using TalentScore.Data;
using TalentScore.Repository;
using TalentScore.Services;
using TalentScore.Services.Interfaces;

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



            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IOpenAIService, OpenAIService>();
            services.AddScoped<IScoreService, ScoreService>();
            services.AddScoped<ITalentScanService, TalentScanService>();


            var apiKey =
           Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "OpenAI API key is not set.");
            }

            services.AddSingleton(
                new ResponsesClient(apiKey));
        }
    }
}
