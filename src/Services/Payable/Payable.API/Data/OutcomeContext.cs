using System;
using MongoDB.Driver;
using Payable.API.Entities;
        
namespace Payable.API.Data
{
	public class OutcomeContext : IOutcomeContext
	{
        public OutcomeContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Outcomes = database.GetCollection<Outcomes>(configuration.GetValue<string>("DatabaseSettings:OutcomesCollectionName"));
        }

        public IMongoCollection<Outcomes> Outcomes { get; }
    }
}

            