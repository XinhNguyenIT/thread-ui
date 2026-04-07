using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<RelationshipFromUserToUser> RelationshipFromUserToUserFromUsers { get; set; } = new List<RelationshipFromUserToUser>();

    public virtual ICollection<RelationshipFromUserToUser> RelationshipFromUserToUserToUsers { get; set; } = new List<RelationshipFromUserToUser>();

    public virtual ICollection<UserVideoLike> UserVideoLikes { get; set; } = new List<UserVideoLike>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
