using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dataset.Interfaces;
using Backend.Models;

namespace Backend.Dataset.Stag
{
	public class StagSeedData : ISeedData
	{
		public string Type => "stag";

		public List<string> Roles => new() { "ADMIN", "USER" };

		public List<(User user, string password, string role)> Users => new()
		{
			(
				new User
				{
					UserName = "adminA@gmail.com",
					Email = "adminA@gmail.com",
					EmailConfirmed = true,
					FirstName = "A",
					LastName = "admin"
				},
				"123456",
				"ADMIN"
			),
			(
				new User
				{
					UserName = "a@gmail.com",
					Email = "a@gmail.com",
					EmailConfirmed = true,
					FirstName = "1",
					LastName = "Demo"
				},
				"123456",
				"USER"
			)
		};
	}
}