using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Video
{
    public int VideoId { get; set; }

    public string Src { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserVideoLike> UserVideoLikes { get; set; } = new List<UserVideoLike>();
}
