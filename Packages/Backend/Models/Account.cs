using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
