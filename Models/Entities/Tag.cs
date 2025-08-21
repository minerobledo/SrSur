using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Tag
{
    public long Id { get; set; }

    public string TagName { get; set; } = null!;

    public string? Slug { get; set; }

    public string? TagDescription { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? LastEditDate { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
