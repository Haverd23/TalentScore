using Microsoft.AspNetCore.StaticFiles;
using TalentScore.Services.Interfaces;

namespace TalentScore.Services
{
    public class FileService : IFileService
    {
        private readonly string[] _allowedExtensions =
      {
            ".png",
            ".jpg",
            ".jpeg",
            ".webp",
            ".pdf",
            ".txt",
            ".doc",
            ".docx"
        };
        public void ValidateFile(IFormFile file)
        {
            if(file == null || file.Length == 0) {
                throw new ArgumentException("Invalid file");
            }
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid file extension");
            }


        }

        public string GetContentType(IFormFile file)
        {
            if (!string.IsNullOrWhiteSpace(file.ContentType))
            {
                return file.ContentType;
            }
            var provider = new FileExtensionContentTypeProvider();

            if(provider.TryGetContentType(file.FileName, out var contetType))
            {
                return contetType;
            }

            return "application/octet-stream";


        }
    }
}
