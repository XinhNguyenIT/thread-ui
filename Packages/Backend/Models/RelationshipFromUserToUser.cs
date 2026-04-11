using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class RelationshipFromUserToUser
{
    public int RelationshipId { get; set; }

    public int FromUserId { get; set; }

    public int ToUserId { get; set; }

    public string Type { get; set; } = null!;

    public virtual User FromUser { get; set; } = null!;

    public virtual User ToUser { get; set; } = null!;
}
