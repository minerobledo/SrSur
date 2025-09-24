namespace SrSur.Models.Entities
{
    public class Resourse
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string CloudflareId { get; set; } = null!; // id devuelto por Cloudflare
        public string FileName { get; set; } = null!;
        // Variants lo vamos a mapear a JSONB en PostgreSQL
        public List<string> Variants { get; set; } = new List<string>();
        public DateTime Uploaded { get; set; }

    }
}
