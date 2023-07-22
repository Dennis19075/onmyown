using System;
using MongoDB.Driver;
using Payable.API.Entities;

namespace Payable.API.Data
{
	public class IncomeContext : IIncomeContext
    {
		public IncomeContext(IConfiguration configuration)
		{
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Incomes = database.GetCollection<Incomes>(configuration.GetValue<string>("DatabaseSettings:IncomesCollectionName"));
        }

        public IMongoCollection<Incomes> Incomes { get; }
    }
}

