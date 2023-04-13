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

        [HttpPost]
        [ProducesResponseType(typeof(Incomes), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Incomes>> CreateIncomes([FromBody] Incomes income)
        {
            await _repository.CreateIncomes(income);
            return CreatedAtRoute("CreateIncomes", new { id = income.Id }, income);
        }

        [HttpPut]
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

