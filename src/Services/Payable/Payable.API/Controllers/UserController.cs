using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Payable.API.Entities;
using Payable.API.Repositories;

namespace Payable.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Users>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var users = await _repository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Users>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Users>>> GetUserById(string id)
        {
            var user = await _repository.GetUser(id);
            if (user == null)
            {
                _logger.LogError($"User with id: {id}, not found.");
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Users), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Users>> CreateUser([FromBody] Users user)
        {
            await _repository.CreateUser(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Users), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] Users user)
        {
            return Ok(await _repository.UpdateUser(user));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteUser")]
        [ProducesResponseType(typeof(Users), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            return Ok(await _repository.DeleteUser(id));
        }
    }
}

