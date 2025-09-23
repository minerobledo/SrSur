using SrSur.Repositories;

namespace SrSur.Interface
{
    public interface ICloudflareRepocitory
    {
        Task<CloudflareUploadResponse?> UploadImage(IFormFile file);
        Task<bool> DeleteImageAsync(string cloudflareId);
    }
}
