using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class UserVideoLike
{
    public int VideoId { get; set; }

    public int UserId { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
