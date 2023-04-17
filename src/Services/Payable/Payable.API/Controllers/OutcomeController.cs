using System;
using Microsoft.AspNetCore.Mvc;
using Payable.API.Entities;
using Payable.API.Repositories;
using System.Net;
using System.Xml.Linq;

namespace Payable.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OutcomeController : ControllerBase
    {
        private readonly IOutcomeRepository _repository;
        private readonly ILogger<OutcomeController> _logger;

        public OutcomeController(IOutcomeRepository repository, ILogger<OutcomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Outcomes>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Outcomes>>> GetOutcomes()
        {
            var outcomes = await _repository.GetOutcomes();
            return Ok(outcomes);
        }

        [HttpGet("[action]/{createdAt}", Name = "GetOutcomesByMonthAndYear")]
        [ProducesResponseType(typeof(IEnumerable<Outcomes>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Outcomes>>> GetOutcomesByMonthAndYear(string createdAt)
        {
            var outcomes = await _repository.GetOutcomesByMonthAndYear(createdAt);
            return Ok(outcomes);
        }

        //GetOutcomesByDay
        [HttpGet("[action]/{createdAt}", Name = "GetOutcomesByDay")]
        [ProducesResponseType(typeof(IEnumerable<Outcomes>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Outcomes>>> GetOutcomesByDay(string createdAt)
        {
            var outcomes = await _repository.GetOutcomesByDay(createdAt);
            return Ok(outcomes);
        }

        //GetOutcomesBySearchBar
        [HttpGet("[action]/{createdAt}/{description}", Name = "GetOutcomeBySearch")]
        [ProducesResponseType(typeof(IEnumerable<Outcomes>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Outcomes>>> GetOutcomeBySearch(string createdAt, string description)
        {
            var outcomes = await _repository.GetOutcomeBySearch(createdAt, description);
            return Ok(outcomes);
        }

        [HttpGet("[action]", Name = "GetOutcomesByWeek")]
        [ProducesResponseType(typeof(IEnumerable<double>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<double>>> GetOutcomesByWeek()
        {
            var outcomes = await _repository.GetOutcomesByWeek();
            return Ok(outcomes);
        }

        [HttpGet("{id:length(24)}", Name = "GetOutcome")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Outcomes>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Outcomes>>> GetOutcomeById(string id)
        {
            var outcome = await _repository.GetOutcome(id);
            if (outcome == null)
            {
                _logger.LogError($"Outcome with id: {id}, not found.");
                return NotFound();
            }
            return Ok(outcome);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Outcomes), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Outcomes>> CreateOutcome([FromBody] Outcomes outcome)
        {
            await _repository.CreateOutcomes(outcome);
            return CreatedAtRoute("GetOutcome", new { id = outcome.Id }, outcome);
        }

        [HttpPut("{id:length(24)}", Name = "UpdateOutcomes")]
        [ProducesResponseType(typeof(Outcomes), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOutcome([FromBody] Outcomes outcome)
        {
            return Ok(await _repository.UpdateOutcomes(outcome));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteOutcomes")]
        [ProducesResponseType(typeof(Outcomes), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteOutcomeById(string id)
        {
            return Ok(await _repository.DeleteOutcomes(id));
        }
    }
}

