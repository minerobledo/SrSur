using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrSur.Models.Entities;

public partial class Tag
{
    public long Id { get; set; }

    public string Tagname { get; set; } = null!;

    public string? Slug { get; set; }

    public string? Tagdescription { get; set; }
    [Column(TypeName = "timestamp with time zone")]
    public DateTime Createdate { get; set; }

    [Column(TypeName = "timestamp with time zone")]
    public DateTime? Lasteditdate { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
