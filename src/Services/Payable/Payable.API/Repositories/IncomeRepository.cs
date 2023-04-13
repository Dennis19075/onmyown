using System;
using MongoDB.Driver;
using Payable.API.Data;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public class IncomeRepository : IIncomeRepository
	{
        private readonly IIncomeContext _context;

        public IncomeRepository(IIncomeContext context)
		{
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Incomes>> GetIncomes()
        {
            return await _context
                    .Incomes
                    .Find(p => true)
                    .ToListAsync();
        }

        public async Task<Incomes> GetIncome(string id)
        {

            return await _context
                        .Incomes
                        .Find(p => p.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task CreateIncomes(Incomes incomes)
        {
            await _context.Incomes.InsertOneAsync(incomes);
        }

        public async Task<bool> UpdateIncomes(Incomes incomes)
        {
            var updateResult = await _context
                                        .Incomes
                                        .ReplaceOneAsync(filter: g => g.Id == incomes.Id, replacement: incomes);
            return updateResult.IsAcknowledged
                        && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteIncomes(string id)
        {
            FilterDefinition<Incomes> filter = Builders<Incomes>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Incomes
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                        && deleteResult.DeletedCount > 0;
        }
    }
}

