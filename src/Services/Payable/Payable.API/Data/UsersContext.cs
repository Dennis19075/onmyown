using System;
using MongoDB.Driver;
using Payable.API.Entities;

namespace Payable.API.Data
{
	public class UsersContext : IUserContext
	{

		public UsersContext(IConfiguration configuration)
		{
			var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

			Users = database.GetCollection<Users>(configuration.GetValue<string>("DatabaseSettings:UserCollectionName"));
			//UserContextSeed.SeedData(Users);
		}

        public IMongoCollection<Users> Users { get; }
    }
}

