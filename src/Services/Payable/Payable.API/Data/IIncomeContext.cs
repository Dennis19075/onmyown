using System;
using MongoDB.Driver;
using Payable.API.Entities;

namespace Payable.API.Data
{
	public interface IIncomeContext
	{
        IMongoCollection<Incomes> Incomes { get; }
    }
}

