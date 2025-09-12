using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Guest
{
    public long Id { get; set; }

    public string? UserAgent { get; set; }

    public string? IpAddress { get; set; }

    public string? Referrer { get; set; }

    public string Device { get; set; } = null!;

    public DateTime DateCreate { get; set; } = DateTime.Now;

    public string? Country { get; set; }

    public string? Region { get; set; }

    public virtual ICollection<PostsVisitedByGuest> PostsVisitedByGuests { get; set; } = new List<PostsVisitedByGuest>();
}
