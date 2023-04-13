using System;
using MongoDB.Driver;
using Payable.API.Data;
using Payable.API.Entities;

namespace Payable.API.Repositories
{
	public class UserRepository : IUserRepository
	{

        private readonly IUserContext _context;

		public UserRepository(IUserContext context)
		{
            _context = context ?? throw new ArgumentNullException(nameof(context));
		}

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _context
                    .Users
                    .Find(p => true)
                    .ToListAsync();
        }

        public async Task<Users> GetUser(string id)
        {

            return await _context
                        .Users
                        .Find(p => p.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task CreateUser(Users user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> UpdateUser(Users user)
        {
            var updateResult = await _context
                                        .Users
                                        .ReplaceOneAsync(filter: g => g.Id == user.Id, replacement: user);
            return updateResult.IsAcknowledged
                        && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUser(string id)
        {
            FilterDefinition<Users> filter = Builders<Users>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Users
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                        && deleteResult.DeletedCount > 0;
        }


    }
}

