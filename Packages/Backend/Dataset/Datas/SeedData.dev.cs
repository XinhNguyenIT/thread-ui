using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dataset.Interfaces;
using Backend.Enums;
using Backend.Models;

namespace Backend.Dataset.Datas
{
	public class DevSeedData : ISeedData
	{
		public string Type => "dev";

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
					Gender = GenderTypeEnum.Male,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.ADMIN, RoleTypeEnum.USER}
			),
			(
				new User
				{
					UserName = "adminB@gmail.com",
					Email = "adminB@gmail.com",
					EmailConfirmed = true,
					FirstName = "B",
					LastName = "admin",
					Gender = GenderTypeEnum.Female,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.ADMIN, RoleTypeEnum.USER}
			),
			(
				new User
				{
					UserName = "a@gmail.com",
					Email = "a@gmail.com",
					EmailConfirmed = true,
					FirstName = "1",
					LastName = "Demo",
					Gender = GenderTypeEnum.Male,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.USER}
			),
			(
				new User
				{
					UserName = "b@gmail.com",
					Email = "b@gmail.com",
					EmailConfirmed = true,
					FirstName = "2",
					LastName = "Demo",
					Gender = GenderTypeEnum.Unknown,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.USER}
			),
			(
				new User
				{
					UserName = "b@gmail.com",
					Email = "b@gmail.com",
					EmailConfirmed = true,
					FirstName = "3",
					LastName = "Demo",
					Gender = GenderTypeEnum.Female,
				},
				"Abc123456@",
				new List<RoleTypeEnum> {RoleTypeEnum.USER}
			),
		};
	}
}