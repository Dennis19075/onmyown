using System;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<Users>> GetUsers();
		Task<Users> GetUser(string id);
		Task CreateUser(Users user);
		Task<bool> UpdateUser(Users user);
		Task<bool> DeleteUser(string id);
	}
}

