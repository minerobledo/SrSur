using System.ComponentModel.DataAnnotations.Schema;

namespace SrSur.Models.DTOs.output
{
    public class TagOutDto
    {
        public long Id { get; set; }

        public string Tagname { get; set; } = null!;

        public string? Slug { get; set; }

        public string? Tagdescription { get; set; }

        public DateTime Createdate { get; set; } = DateTime.UtcNow;

        public DateTime? Lasteditdate { get; set; }
    }
}
