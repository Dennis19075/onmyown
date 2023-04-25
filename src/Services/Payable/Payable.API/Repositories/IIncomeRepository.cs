using System;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public interface IIncomeRepository
	{
        Task<IEnumerable<Incomes>> GetIncomes();
        Task<Incomes> GetIncome(string id);
        Task<IEnumerable<Incomes>> GetIncomesByFilters(string createdAt, string category);
        Task<IEnumerable<double>> GetIncomesByWeek();
        Task CreateIncomes(Incomes incomes);
        Task<bool> UpdateIncomes(Incomes incomes);
        Task<bool> DeleteIncomes(string id);
    }
}

