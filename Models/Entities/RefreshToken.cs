using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class Refreshtoken
{
    public long Id { get; set; }

    public long Userid { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expirydate { get; set; }

    public DateTime Createddate { get; set; }

    public bool Revoked { get; set; }

    public string? Replacedbytoken { get; set; }

    public virtual User User { get; set; } = null!;
}
