using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dataset.Interfaces;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dataset.Datas
{
	public class TestSeedData : ISeedData
	{
		public string Type => "test";

		public List<string> Roles => new() { "ADMIN", "USER" };

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
					Gender = GenderTypeEnum.OTHER,
				},
				"123456",
				new List<RoleTypeEnum> {RoleTypeEnum.ADMIN}
			),
			(
				new User
				{
					Id = SeedIds.UserAIds[0],
					UserName = "a@gmail.com",
					Email = "a@gmail.com",
					EmailConfirmed = true,
					FirstName = "1",
					Gender = GenderTypeEnum.FEMALE,
					LastName = "Demo"
				},
				"123456",
				new List<RoleTypeEnum> {RoleTypeEnum.USER}
			)
		};

		public List<Post> Posts => new List<Post>
		{
			new Post
			{
				Content = "📢 Thông báo: Hệ thống sẽ bảo trì định kỳ vào lúc 2:00 sáng mai để cập nhật những tính năng mới nhất. Cảm ơn mọi người đã đồng hành!",
				CreateAt = DateTime.UtcNow.AddHours(-10),
				UserId = SeedIds.AdminIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				IsAvatar = false,
			},

			new Post
			{
				Content = "Lần đầu đến với Sa Pa, không khí thật tuyệt vời! ☁️⛰️",
				CreateAt = DateTime.UtcNow.AddDays(-5),
				UserId = SeedIds.UserAIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/sapa/600/900" }
				}
			},

			new Post
			{
				Content = "Có những ngày chỉ muốn gác lại mọi thứ và đi đâu đó thật xa... 🌊",
				CreateAt = DateTime.UtcNow.AddDays(-1),
				UserId = SeedIds.UserAIds[0],
				PrivacySetting = PrivacySettingEnum.PRIVATE,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/sea/800/600" }
				}
			},

			new Post
			{
				Content = "Một chút bình yên cuối ngày. Chúc mọi người ngủ ngon! ✨",
				CreateAt = DateTime.UtcNow.AddMinutes(-45),
				UserId = SeedIds.UserAIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.VIDEO, Src = "https://www.w3schools.com/html/movie.mp4" }
				}
			},

			new Post
			{
				Content = "Nhìn lại những khoảnh khắc đáng nhớ của năm ngoái. Thời gian trôi nhanh quá! 📸✨",
				CreateAt = DateTime.UtcNow.AddMonths(-6),
				UserId = SeedIds.AdminIds[0],
				PrivacySetting = PrivacySettingEnum.PUBLIC,
				Medias = new List<Media>
				{
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/mem1/800/800" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/mem2/800/800" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/mem3/800/800" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/mem4/800/800" },
					new Media { Type = MediaTypeEnum.IMAGE, Src = "https://picsum.photos/seed/mem5/800/800" }
				}
			}
		};
	}
}