using System;
using System.Globalization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Payable.API.Data;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public class OutcomeRepository : IOutcomeRepository
	{
        private readonly IOutcomeContext _context;

        public OutcomeRepository(IOutcomeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Outcomes>> GetOutcomes()
        {
            return await _context
                    .Outcomes
                    .Find(p => true)
                    .SortBy(p => p.createdAt)
                    .ToListAsync();

        }

        public async Task<IEnumerable<Outcomes>> GetOutcomesByMonthAndYear(string createdAt)
        {

            var dateTime = Convert.ToDateTime(createdAt);

            var first_date = new DateTime(dateTime.Year, dateTime.Month, 1);
            var last_date = first_date.AddMonths(1);

            return await _context
                .Outcomes
                .Find(x => x.createdAt >= first_date && x.createdAt < last_date)
                .SortBy(p => p.createdAt)
                .ToListAsync(); //Date Filter
        }

        public async Task<IEnumerable<double>> GetOutcomesByWeek()
        {
            var weeks = new double[7];

            DateTime thisDay = DateTime.Today;
            var currentMonthList = await this.GetOutcomesByMonthAndYear(thisDay.ToString());

            foreach (Outcomes element in currentMonthList)
            {
                weeks[GetWeekNumberOfMonth(element.createdAt)-1] += element.expense;
            }

            weeks[5] = thisDay.Month;
            weeks[6] = thisDay.Year;

            return weeks;

        }

        public static int GetWeekNumberOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }


        public async Task<Outcomes> GetOutcome(string id)
        {

            return await _context
                        .Outcomes
                        .Find(p => p.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task CreateOutcomes(Outcomes outcome)
        {
            await _context.Outcomes.InsertOneAsync(outcome);
        }

        public async Task<bool> UpdateOutcomes(Outcomes outcome)
        {
            var updateResult = await _context
                                        .Outcomes
                                        .ReplaceOneAsync(filter: g => g.Id == outcome.Id, replacement: outcome);
            return updateResult.IsAcknowledged
                        && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteOutcomes(string id)
        {
            FilterDefinition<Outcomes> filter = Builders<Outcomes>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Outcomes
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                        && deleteResult.DeletedCount > 0;
        }
    }
}

