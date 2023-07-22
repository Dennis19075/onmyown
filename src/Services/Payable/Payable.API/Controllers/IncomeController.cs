using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Payable.API.Entities;
using Payable.API.Repositories;

namespace Payable.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class IncomeController : ControllerBase
	{

		private readonly IIncomeRepository _repository;
		private readonly ILogger<IncomeController> _logger;


		public IncomeController(IIncomeRepository repository, ILogger<IncomeController> logger)
		{
			_repository = repository;
			_logger = logger;

		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Incomes>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<Incomes>>> GetIncomes()
		{
			var incomes = await _repository.GetIncomes();
			return Ok(incomes);
		}

        [HttpGet("{id:length(24)}", Name = "GetIncome")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Incomes>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Incomes>>> GetIncomeById(string id)
        {
            var income = await _repository.GetIncome(id);
            if (income == null)
            {
                _logger.LogError($"Income with id: {id}, not found.");
                return NotFound();
            }
            return Ok(income);
        }

        [HttpGet("[action]/{createdAt}/{category}", Name = "GetIncomesByFilters")]
        [ProducesResponseType(typeof(IEnumerable<Incomes>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Incomes>>> GetIncomesByFilters(string createdAt, string category)
        {
            var incomes = await _repository.GetIncomesByFilters(createdAt, category);
            return Ok(incomes);
        }

        [HttpGet("[action]", Name = "GetIncomesByWeek")]
        [ProducesResponseType(typeof(IEnumerable<double>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<double>>> GetIncomesByWeek()
        {
            var incomes = await _repository.GetIncomesByWeek();
            return Ok(incomes);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Incomes), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Incomes>> CreateIncomes([FromBody] Incomes income)
        {
            await _repository.CreateIncomes(income);
            return CreatedAtRoute("GetIncome", new { id = income.Id }, income);
        }

        [HttpPut("{id:length(24)}", Name = "UpdateIncomes")]
        [ProducesResponseType(typeof(Incomes), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateIncome([FromBody] Incomes income)
        {
            return Ok(await _repository.UpdateIncomes(income));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteIncomes")]
        [ProducesResponseType(typeof(Incomes), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteIncomeById(string id)
        {
            return Ok(await _repository.DeleteIncomes(id));
        }
    }
}

