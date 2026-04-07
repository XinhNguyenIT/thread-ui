using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class TiktokdbContext : DbContext
{
    public TiktokdbContext()
    {
    }

    public TiktokdbContext(DbContextOptions<TiktokdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<RelationshipFromUserToUser> RelationshipFromUserToUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCommentLike> UserCommentLikes { get; set; }

    public virtual DbSet<UserVideoLike> UserVideoLikes { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A60A9E773E");

            entity.ToTable("Account");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__UserId__49C3F6B7");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCA67E6451A");

            entity.ToTable("Comment");

            entity.HasIndex(e => e.CommentId, "UQ__Comment__C3B4DFCB928A9202").IsUnique();

            entity.Property(e => e.Text).HasMaxLength(1);

            entity.HasOne(d => d.FromUser).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__FromUse__4D94879B");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK__Comment__ParentC__4E88ABD4");

            entity.HasOne(d => d.ToVideo).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ToVideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__ToVideo__4F7CD00D");
        });

        modelBuilder.Entity<RelationshipFromUserToUser>(entity =>
        {
            entity.HasKey(e => e.RelationshipId).HasName("PK__Relation__31FEB881450CA443");

            entity.ToTable("RelationshipFromUserToUser");

            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("FOLLOWER");

            entity.HasOne(d => d.FromUser).WithMany(p => p.RelationshipFromUserToUserFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Relations__FromU__4BAC3F29");

            entity.HasOne(d => d.ToUser).WithMany(p => p.RelationshipFromUserToUserToUsers)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Relations__ToUse__4CA06362");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C713CF253");

            entity.ToTable("User");

            entity.Property(e => e.Avatar)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(80);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<UserCommentLike>(entity =>
        {
            entity.HasKey(e => new { e.CommentId, e.UserId }).HasName("PK__UserComm__12CC530E3B975D49");

            entity.ToTable("UserCommentLike");

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserVideoLike>(entity =>
        {
            entity.HasKey(e => new { e.VideoId, e.UserId }).HasName("PK__UserVide__6B9D9EAE2FB88AEB");

            entity.ToTable("UserVideoLike");

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserVideoLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserVideo__UserI__5070F446");

            entity.HasOne(d => d.Video).WithMany(p => p.UserVideoLikes)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserVideo__Video__5165187F");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PK__Video__BAE5126A3FB86A88");

            entity.ToTable("Video");

            entity.Property(e => e.Src)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Videos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Video__UserId__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
