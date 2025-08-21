using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class RefreshToken
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public DateTime CreatedDate { get; set; } 

    public bool Revoked { get; set; }

    public string? ReplacedByToken { get; set; }

    public virtual User User { get; set; } = null!;
}
