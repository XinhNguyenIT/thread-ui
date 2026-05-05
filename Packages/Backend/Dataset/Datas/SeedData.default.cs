using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dataset.Interfaces;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dataset.Datas
{
	public class DefaultSeedData : ISeedData
	{
		public string Type => "default";

		public List<string> Roles => new() { "ADMIN", "USER" };

		public List<Post> Posts => new List<Post>
		{
			// --- USER 1: Admin / KOL (Đã có 2 bài, thêm 1 bài Avatar) ---
			new Post
			{
				Content = "Cập nhật ảnh đại diện mới cho năm 2024! 🌟",
				CreateAt = DateTime.UtcNow.AddDays(-10),
				IsAvatar = true, // Case: Đổi ảnh đại diện
				UserId = SeedIds.AdminIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://i.pravatar.cc/500?u=1" }
				}
			},

			// --- USER 2: Người dùng năng động (Nhiều ảnh + Video) ---
			new Post
			{
				Content = "Review chuyến đi Đà Lạt cực chill của mình. Mọi người nên đi thử nhé! 🌲☕",
				CreateAt = DateTime.UtcNow.AddDays(-2),
				IsAvatar = false,
				UserId = SeedIds.AdminIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/dalat1/800/600" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/dalat2/800/600" },
					new Media { Type = MediaTypeEnum.VIDEO, Src = "https://sample-videos.com/video123/mp4/720/big_buck_bunny_720p_1mb.mp4" }
				}
			},

			// --- USER 3: Người sống nội tâm (Chỉ Text, Private/Friend) ---
			new Post
			{
				Content = "Chỉ là một dòng suy nghĩ vẩn vơ vào đêm khuya... Mong ngày mai sẽ khá hơn.",
				CreateAt = DateTime.UtcNow.AddHours(-5),
				IsAvatar = false,
				UserId = SeedIds.UserAIds[2],
				PrivacySetting = PrivacySettingEnum.PRIVATE, // Case: Chỉ mình tôi
			},
			new Post
			{
				Content = "Tin vui! Mình vừa nhận được job mới rồi cả nhà ơi! 🎉",
				CreateAt = DateTime.UtcNow.AddDays(-1),
				IsAvatar = false,
				UserId = SeedIds.AdminIds[0],
				PrivacySetting = PrivacySettingEnum.FRIEND, // Case: Chỉ bạn bè
			},

			// --- USER 4: User "Ảo" (Chỉ đăng ảnh, không caption) ---
			new Post
			{
				Content = null, // Case: Post không có text
				CreateAt = DateTime.UtcNow.AddDays(-3),
				IsAvatar = false,
				UserId = SeedIds.UserAIds[1],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/abstract/1080/1080" }
				}
			},

			// --- USER 5: Case nhạy cảm (Bị báo cáo) ---
			new Post
			{
				Content = "Nội dung này vi phạm tiêu chuẩn cộng đồng và đã bị báo cáo.",
				CreateAt = DateTime.UtcNow.AddMinutes(-30),
				IsAvatar = false,
				IsReported = true, // Case: Bài viết bị report
				UserId = SeedIds.UserAIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/warning/800/600" }
				}
			},

			// --- USER 5: Thêm 1 bài đăng bình thường để test Avatar ---
			new Post
			{
				Content = "Chào cả nhà, mình là thành viên mới!",
				CreateAt = DateTime.UtcNow.AddDays(-15),
				IsAvatar = true,
				UserId = SeedIds.UserAIds[1],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://i.pravatar.cc/500?u=5" }
				}
			},
			new Post
			{
				Content = "Outfit của tuần này. Mọi người thích bộ nào nhất? 👗👠",
				CreateAt = DateTime.UtcNow.AddDays(-4),
				IsAvatar = false,
				UserId = SeedIds.UserAIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/fashion1/800/1000" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/fashion2/800/1000" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/fashion3/800/1000" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/fashion4/800/1000" }
				}
			},

			// --- CASE 7: USER 4 - Bài đăng "Siêu dài" (Test giới hạn hiển thị/Xem thêm) ---
			new Post
			{
				Content = "Hôm nay mình sẽ chia sẻ về lộ trình tự học Backend cho các bạn mới bắt đầu. \n\n" +
						"Bước 1: Học ngôn ngữ lập trình cơ bản (C#, Java...)\n" +
						"Bước 2: Tìm hiểu về Database (SQL Server, MySQL...)\n" +
						"Bước 3: Học Framework (ASP.NET Core...)\n" +
						"Bước 4: Tìm hiểu về RESTful API và cách triển khai.\n" +
						"Bước 5: Học về Docker và CI/CD để triển khai ứng dụng.\n\n" +
						"Hy vọng những chia sẻ này sẽ giúp ích cho các bạn trong quá trình chinh phục code! 💻🚀",
				CreateAt = DateTime.UtcNow.AddDays(-6),
				IsAvatar = false,
				UserId = SeedIds.AdminIds[1],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/coding/1200/630" }
				}
			},

			// --- CASE 8: USER 1 - Bài đăng "Kỷ niệm" (Thời gian từ rất lâu trước đây) ---
			new Post
			{
				Content = "Ngày này 2 năm trước, bắt đầu hành trình xây dựng hệ thống này. Thật nhiều cảm xúc! ❤️",
				CreateAt = DateTime.UtcNow.AddYears(-2), // Test sắp xếp thời gian cũ
				IsAvatar = false,
				UserId = SeedIds.AdminIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/memory/800/600" }
				}
			},

			// --- CASE 9: USER 5 - Bài đăng Video ngắn (dạng Reels/Shorts) ---
			new Post
			{
				Content = "Chill một chút với bản nhạc này... 🎵",
				CreateAt = DateTime.UtcNow.AddMinutes(-120),
				IsAvatar = false,
				UserId = SeedIds.UserAIds[2],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.VIDEO, Src = "https://www.w3schools.com/html/movie.mp4" }
				}
			},

			// --- CASE 10: USER 3 - Bài đăng "Friend Only" (Kiểm tra quyền truy cập) ---
			new Post
			{
				Content = "Chỉ những người bạn thân thiết mới thấy được tấm hình 'dìm hàng' này thôi nhé! 🤫",
				CreateAt = DateTime.UtcNow.AddHours(-1),
				IsAvatar = false,
				UserId = SeedIds.UserAIds[2],
				PrivacySetting = PrivacySettingEnum.FRIEND,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/funny/600/800" }
				}
			}
		};

		public List<(User user, string password, List<RoleTypeEnum> roles)> Users => new()
		{
			(
				new User
				{
					Id = SeedIds.AdminIds[0],
					UserName = "adminA@gmail.com",
					Email = "adminA@gmail.com",
					EmailConfirmed = true,
					FirstName = "A",
					LastName = "admin",
					Gender = GenderTypeEnum.MALE,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.ADMIN, RoleTypeEnum.USER}
			),
			(
				new User
				{
					Id = SeedIds.AdminIds[1],
					UserName = "adminB@gmail.com",
					Email = "adminB@gmail.com",
					EmailConfirmed = true,
					FirstName = "B",
					LastName = "admin",
					Gender = GenderTypeEnum.FEMALE,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.ADMIN, RoleTypeEnum.USER}
			),
			(
				new User
				{
					Id = SeedIds.UserAIds[0],
					UserName = "a@gmail.com",
					Email = "a@gmail.com",
					EmailConfirmed = true,
					FirstName = "1",
					LastName = "Demo",
					Gender = GenderTypeEnum.MALE,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.USER}
			),
			(
				new User
				{
					Id = SeedIds.UserAIds[1],
					UserName = "b@gmail.com",
					Email = "b@gmail.com",
					EmailConfirmed = true,
					FirstName = "2",
					LastName = "Demo",
					Gender = GenderTypeEnum.UNKNOWN,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.USER}
			),
			(
				new User
				{
					Id = SeedIds.UserAIds[2],
					UserName = "c@gmail.com",
					Email = "c@gmail.com",
					EmailConfirmed = true,
					FirstName = "3",
					LastName = "Demo",
					Gender = GenderTypeEnum.FEMALE,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.USER}
			),
		};
	}
}