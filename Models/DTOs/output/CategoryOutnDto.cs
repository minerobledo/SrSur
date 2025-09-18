using SrSur.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrSur.Models.DTOs.output
{
    public class CategoryOutnDto
    {
        public long Id { get; set; }

        public long? Parentid { get; set; }

        public string Categoryname { get; set; } = null!;

        public string? Slug { get; set; }

        public string? Categorydescription { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime Createdate { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "timestamp with time zone")]
        public DateTime? Lasteditdate { get; set; }

        public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

        public virtual Category? Parent { get; set; }

        
    }
}
