using System;
using System.Globalization;
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

        public async Task<IEnumerable<Incomes>> GetIncomesByFilters(string createdAt, string category)
        {
            var dateTime = Convert.ToDateTime(createdAt);

            var first_date = new DateTime(dateTime.Year, dateTime.Month, 1);
            var last_date = first_date.AddMonths(1);

            if (category.Equals("all"))
            {
                return await _context
                .Incomes
                .Find(x =>
                    (x.createdAt >= first_date && x.createdAt < last_date))
                .SortBy(p => p.createdAt)
                .ToListAsync(); //Date Filter
            }

            return await _context
                .Incomes
                .Find(x =>
                    (x.createdAt >= first_date && x.createdAt < last_date) &&
                        (x.category.Equals(category)))
                .SortBy(p => p.createdAt)
                .ToListAsync(); //Date Filter
        }

        public async Task<IEnumerable<double>> GetIncomesByWeek()
        {
            var weeks = new double[7];

            DateTime thisDay = DateTime.Today;
            var currentMonthList = await this.GetIncomesByFilters(thisDay.ToString(), "all");

            foreach (Incomes element in currentMonthList)
            {
                weeks[GetWeekNumberOfMonth(element.createdAt) - 1] += element.expense;
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

