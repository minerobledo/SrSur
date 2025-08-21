using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Comment
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public long? UserId { get; set; }

    public long? ParentId { get; set; }

    public string Author { get; set; } = null!;

    public string EmailAuthor { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Device { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? AuthorUrl { get; set; }

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Comment? Parent { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User? User { get; set; }
}
