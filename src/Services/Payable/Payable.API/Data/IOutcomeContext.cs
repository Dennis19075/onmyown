using System;
using MongoDB.Driver;
using Payable.API.Entities;

namespace Payable.API.Data
{
	public interface IOutcomeContext
	{
        IMongoCollection<Outcomes> Outcomes { get; }
    }
}

