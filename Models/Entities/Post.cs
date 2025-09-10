using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Post
{
    public long Id { get; set; }

    public long AuthorId { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public string CommentStatus { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? PublishDate { get; set; }

    public DateTime? LastEditDate { get; set; }

    public string PostStatus { get; set; } = null!; // Pending , Publish

    public string? Excerpt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
