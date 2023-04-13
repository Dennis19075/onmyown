using System;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public interface IOutcomeRepository
	{
        Task<IEnumerable<Outcomes>> GetOutcomes();
        Task<IEnumerable<Outcomes>> GetOutcomesByMonthAndYear(string createdAt);
        Task<IEnumerable<double>> GetOutcomesByWeek();
        Task<Outcomes> GetOutcome(string id);
        Task CreateOutcomes(Outcomes outcomes);
        Task<bool> UpdateOutcomes(Outcomes outcomes);
        Task<bool> DeleteOutcomes(string id);
    }
}

