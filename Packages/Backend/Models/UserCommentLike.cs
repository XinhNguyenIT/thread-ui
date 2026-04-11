using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class UserCommentLike
{
    public int CommentId { get; set; }

    public int UserId { get; set; }

    public DateTime? CreateAt { get; set; }
}
