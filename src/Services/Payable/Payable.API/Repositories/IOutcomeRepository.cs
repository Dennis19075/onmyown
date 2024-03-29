﻿using System;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public interface IOutcomeRepository
	{
        Task<IEnumerable<Outcomes>> GetOutcomes();
        Task<IEnumerable<Outcomes>> GetOutcomesByFilters(string createdAt, string category);
        Task<IEnumerable<Outcomes>> GetOutcomesByDay(string createdAt);
        Task<IEnumerable<double>> GetOutcomesByWeek();
        Task<IEnumerable<Outcomes>> GetOutcomeListByWeek(string createdAt, string category, int week);
        Task<Outcomes> GetOutcome(string id);
        Task CreateOutcomes(Outcomes outcomes);
        Task<bool> UpdateOutcomes(Outcomes outcomes);
        Task<bool> DeleteOutcomes(string id);
    }
}

