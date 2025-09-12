using System;
using System.Collections.Generic;

namespace SrSur.Models.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Password { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public DateTime DateRegister { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
