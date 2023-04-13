using System;
using MongoDB.Driver;
using Payable.API.Entities;

namespace Payable.API.Data
{
	public class UserContextSeed
	{
		public static void SeedData(IMongoCollection<Users> userCollection)
		{
			bool existUser = userCollection.Find(p => true).Any();
			if(!existUser)
			{
				userCollection.InsertManyAsync(GetPreconfiguredUsers());
			}
		}

        private static IEnumerable<Users> GetPreconfiguredUsers()
        {
			return new List<Users>()
			{
				new Users()
				{
					Id = "639a530c058b3ae812b1e1ec",
					firstName = "Vilma",
					lastName = "Palma",
					email = "lilvapalma@hotmail.com",
					phone = "+593992906175",
					password = "vilma123",
					createdAt = DateTime.Now,
					editedAt = DateTime.Now
				}
			};
        }
    }
}

