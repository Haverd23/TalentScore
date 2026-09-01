namespace TalentScore.Services.Interfaces
{
    public interface IFileService
    {
        public void ValidateFile(IFormFile file);
        public string GetContentType(IFormFile file);
        Task<byte[]> GetBytesAsync(IFormFile file); 
    }
}
