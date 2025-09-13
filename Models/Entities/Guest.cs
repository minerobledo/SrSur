using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Guest
{
    public long Id { get; set; }

    public string? Useragent { get; set; }

    public string Ipaddress { get; set; } = null!;

    public string? Referrer { get; set; }

    public string Device { get; set; } = null!;

    public DateTime Datecreate { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public virtual ICollection<Postsvisitedbyguest> Postsvisitedbyguests { get; set; } = new List<Postsvisitedbyguest>();
}
