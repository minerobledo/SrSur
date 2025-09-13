using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Postsvisitedbyguest
{
    public long Postid { get; set; }

    public long Guestid { get; set; }

    public int Visitetime { get; set; }

    public DateTime Datevisited { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
