namespace SrSur.Models.Entities
{
    public class SponsorImage
    {
        public long Id { get; set; }
        public long SponsorID { get; set; }
        public string CloudflareId { get; set; } = null!; // id devuelto por Cloudflare
        public string FileName { get; set; } = null!;
        public List<string> Variants { get; set; } = new List<string>();
        public virtual Sponsor Sponsor { get; set; }    
    }
}
