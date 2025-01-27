using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHarvest_Case.Application.Services
{
	public class UserMockService
	{
		public static Dictionary<int, (string Name, string Surname)> Users = new()
		{
			{ 1, ("Cenkhan", "Tarkan") },
			{ 2, ("Lebron", "James") }
		};

		public static (string Name, string Surname)? GetUser(int customerId)
		{
			return Users.ContainsKey(customerId) ? Users[customerId] : null;
		}
	}
}
