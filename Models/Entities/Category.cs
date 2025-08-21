using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Category
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Slug { get; set; }

    public string? CategoryDescription { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? LastEditDate { get; set; }

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
