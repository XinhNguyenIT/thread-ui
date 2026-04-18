using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dataset.Interfaces;
using Backend.Models;

namespace Backend.Dataset.Dev
{
	public class DevSeedData : ISeedData
	{
		public string Type => "dev";

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
				"Abc123456@",
				"ADMIN"
			),
			(
				new User
				{
					UserName = "adminB@gmail.com",
					Email = "adminB@gmail.com",
					EmailConfirmed = true,
					FirstName = "B",
					LastName = "admin"
				},
				"Abc123456@",
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
				"Abc123456@",
				"USER"
			),
			(
				new User
				{
					UserName = "b@gmail.com",
					Email = "b@gmail.com",
					EmailConfirmed = true,
					FirstName = "2",
					LastName = "Demo"
				},
				"Abc123456@",
				"USER"
			),
			(
				new User
				{
					UserName = "b@gmail.com",
					Email = "b@gmail.com",
					EmailConfirmed = true,
					FirstName = "3",
					LastName = "Demo"
				},
				"Abc123456@",
				"USER"
			),
		};
	}
}