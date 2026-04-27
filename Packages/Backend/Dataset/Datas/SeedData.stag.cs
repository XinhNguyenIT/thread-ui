using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dataset.Interfaces;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dataset.Datas
{
	public class StagSeedData : ISeedData
	{
		public string Type => "stag";

		public List<string> Roles => new() { "ADMIN", "USER" };

		public List<(User user, string password, List<RoleTypeEnum> roles)> Users => new()
		{
			(
				new User
				{
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
	}
}