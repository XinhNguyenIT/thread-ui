using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
	public class ThreadDbContext : IdentityDbContext<User>
	{
		public ThreadDbContext(DbContextOptions<ThreadDbContext> options) : base(options) { }

		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Friendship> Friendships { get; set; }
		public DbSet<Hashtag> Hashtags { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Picture> Pictures { get; set; }
		public DbSet<PostHashtag> PostHashtags { get; set; }
		public DbSet<PostReport> PostReports { get; set; }
		public DbSet<Story> Stories { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>(entity =>
			{
				entity.ToTable("Users");

				entity.Property(n => n.Gender)
						.HasConversion<string>();
			});

			builder.Entity<Post>(entity =>
			{
				entity.HasOne(p => p.Author)
					  .WithMany(u => u.Posts)
					  .HasForeignKey(p => p.UserId);

				entity.HasMany(p => p.Comments)
						.WithOne(c => c.Post)
						.HasForeignKey(c => c.PostId);

				entity.HasMany(p => p.Pictures)
						.WithOne(pic => pic.Post)
						.HasForeignKey(pic => pic.PostId);

				entity.HasMany(p => p.Notifications)
						.WithOne(n => n.Post)
						.HasForeignKey(n => n.PostId);

				entity.Property(p => p.PrivacySetting)
						.HasConversion<string>();

				entity.HasMany(p => p.Reports)
						.WithOne(pr => pr.Post)
						.HasForeignKey(pr => pr.PostId);
			});

			builder.Entity<PostHashtag>(entity =>
			{
				entity.HasKey(ph => new { ph.PostId, ph.HashtagId });

				entity.HasOne(ph => ph.Post)
					  	.WithMany(p => p.PostHashtags)
					  	.HasForeignKey(ph => ph.PostId);


				entity.HasOne(ph => ph.Hashtag)
					  .WithMany(h => h.PostHashtags)
					  .HasForeignKey(ph => ph.HashtagId);
			});

			builder.Entity<Comment>(entity =>
			{
				entity.HasOne(c => c.User)
						.WithMany(u => u.Comments)
						.HasForeignKey(c => c.UserId)
						.OnDelete(DeleteBehavior.NoAction);
			});

			builder.Entity<Friendship>(entity =>
			{
				entity.HasOne(f => f.FromUser)
						.WithMany(u => u.FriendshipRequests)
						.HasForeignKey(f => f.FromUserId)
						.OnDelete(DeleteBehavior.NoAction);

				entity.HasOne(f => f.ToUser)
						.WithMany(u => u.FriendshipResponses)
						.HasForeignKey(f => f.ToUserId);
			});

			builder.Entity<Hashtag>();

			builder.Entity<Like>(entity =>
			{
				entity.HasOne(l => l.FromUser)
						.WithMany(u => u.Likes)
						.HasForeignKey(l => l.UserId);

				entity.Property(l => l.TargetType)
						.HasConversion<string>();
			});

			builder.Entity<Notification>(entity =>
			{
				entity.Property(n => n.Type)
						.HasConversion<string>();

				entity.HasOne(n => n.FromUser)
						.WithMany(u => u.SentNotifications)
						.HasForeignKey(n => n.FromUserId)
						.OnDelete(DeleteBehavior.NoAction);

				entity.HasOne(n => n.ToUser)
						.WithMany(u => u.ReceiveNotifications)
						.HasForeignKey(n => n.ToUserId);
			});

			builder.Entity<Picture>(entity =>
			{
				entity.HasOne(p => p.Post)
						.WithMany(po => po.Pictures)
						.HasForeignKey(p => p.PostId);

				entity.HasOne(p => p.Comment)
						.WithMany(c => c.Pictures)
						.HasForeignKey(p => p.CommentId);
			});

			builder.Entity<Story>(entity =>
			{
				entity.HasOne(s => s.User)
						.WithMany(u => u.Stories)
						.HasForeignKey(s => s.UserId);
			});

			builder.Entity<PostReport>(entity =>
			{
				entity.HasOne(pr => pr.Post)
						.WithMany(p => p.Reports)
						.HasForeignKey(pr => pr.PostId);

				entity.HasOne(pr => pr.User)
						.WithMany(u => u.CreateReport)
						.HasForeignKey(pr => pr.UserId);
			});

			builder.Entity<RefreshToken>(entity =>
			{
				entity.HasOne(r => r.User)
						.WithMany(u => u.RefreshTokens)
						.HasForeignKey(r => r.UserId);
			});
		}
	}
}