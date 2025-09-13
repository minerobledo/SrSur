using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Post
{
    public long Id { get; set; }

    public long Authorid { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public string Commentstatus { get; set; } = null!;

    public DateTime Createdate { get; set; }

    public DateTime? Publishdate { get; set; }

    public DateTime? Lasteditdate { get; set; }

    public string Poststatus { get; set; } = null!;

    public string? Excerpt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Postsvisitedbyguest> Postsvisitedbyguests { get; set; } = new List<Postsvisitedbyguest>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
