using SrSur.Services;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SrSur.Repositories
{
    public class CloudflareRepocitory
    {
        private readonly string accountId = "TU_ACCOUNT_ID";
        private readonly string apiToken = "TU_API_TOKEN";
        private readonly HttpClient httpClient = new HttpClient();

       
        public async Task<CloudflareUploadResponse?> UploadImage(IFormFile file)
        {
            using var form = new MultipartFormDataContent();
            using var stream = file.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            form.Add(fileContent, "file", file.FileName);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiToken);

            var response = await httpClient.PostAsync(
                $"https://api.cloudflare.com/client/v4/accounts/{accountId}/images/v1", form);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error subiendo la imagen: {content}");
            }

            return JsonSerializer.Deserialize<CloudflareUploadResponse>(content);
        }
        public async Task<bool> DeleteImageAsync(string cloudflareId)
        {
            if (string.IsNullOrEmpty(cloudflareId))
                throw new ArgumentException("CloudflareId no puede estar vacío");

            // Header de autorización
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiToken);

            // Petición DELETE a Cloudflare
            var response = await httpClient.DeleteAsync(
                $"https://api.cloudflare.com/client/v4/accounts/{accountId}/images/v1/{cloudflareId}");

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error borrando la imagen: {content}");
            }

            // Cloudflare devuelve JSON con "success": true
            var result = JsonSerializer.Deserialize<CloudflareDeleteResponse>(content);
            return result?.Success ?? false;
        }

    }
    public class CloudflareDeleteResponse
    {
        public bool Success { get; set; }
        public List<object> Errors { get; set; }
        public List<object> Messages { get; set; }
    }
    public class CloudflareUploadResponse
    {
        public CloudflareImageResult Result { get; set; } 
        public bool Success { get; set; }
        public List<object> Errors { get; set; }
        public List<object> Messages { get; set; }
    }

    public class CloudflareImageResult
    {
        public string Id { get; set; }
        public string Filename { get; set; }
        public DateTime Uploaded { get; set; }
        public List<string> Variants { get; set; }
    }
}
