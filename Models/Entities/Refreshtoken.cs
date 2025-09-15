using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrSur.Models.Entities;

public partial class Refreshtoken
{
    public long Id { get; set; }

    public long Userid { get; set; }

    public string Token { get; set; } = null!;

    [Column(TypeName = "timestamp with time zone")]
    public DateTime Expirydate { get; set; }
    [Column(TypeName = "timestamp with time zone")]
    public DateTime Createddate { get; set; }

    public bool Revoked { get; set; }

    public string? Replacedbytoken { get; set; }

    public virtual User User { get; set; } = null!;
}
