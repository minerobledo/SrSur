using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrSur.Models.Entities;

public partial class Comment
{
    public long Id { get; set; }

    public long Postid { get; set; }

    public long? Userid { get; set; }

    public long? Parentid { get; set; }

    public string Author { get; set; } = null!;

    public string Emailauthor { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Device { get; set; } = null!;
    [Column(TypeName = "timestamp with time zone")]
    public DateTime Datecreate { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? Authorurl { get; set; }

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Comment? Parent { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User? User { get; set; }
}
