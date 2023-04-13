using System;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public interface IIncomeRepository
	{
        Task<IEnumerable<Incomes>> GetIncomes();
        Task<Incomes> GetIncome(string id);
        Task CreateIncomes(Incomes outcomes);
        Task<bool> UpdateIncomes(Incomes outcomes);
        Task<bool> DeleteIncomes(string id);
    }
}

