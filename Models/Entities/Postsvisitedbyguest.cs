using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrSur.Models.Entities;

public partial class Postsvisitedbyguest
{
    public long Postid { get; set; }

    public long Guestid { get; set; }

    public int Visitetime { get; set; }
    [Column(TypeName = "timestamp with time zone")]
    public DateTime Datevisited { get; set; } = DateTime.Now;

    public virtual Guest Guest { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
