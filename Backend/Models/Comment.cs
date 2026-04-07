using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Text { get; set; } = null!;

    public int FromUserId { get; set; }

    public int ToVideoId { get; set; }

    public byte Level { get; set; }

    public int? ParentCommentId { get; set; }

    public virtual User FromUser { get; set; } = null!;

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParentComment { get; set; }

    public virtual Video ToVideo { get; set; } = null!;
}
