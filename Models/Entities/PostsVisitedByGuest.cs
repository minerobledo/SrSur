using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class PostsVisitedByGuest
{
    public long PostId { get; set; }

    public long GuestId { get; set; }

    public int VisiteTime { get; set; }

    public DateTime DateVisited { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
