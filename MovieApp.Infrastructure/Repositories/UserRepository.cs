using Microsoft.EntityFrameworkCore;
using MovieApp.Application.Interfaces;
using MovieApp.Domain;
using MovieApp.Infrastructure.Data;

namespace MovieApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieAppDbContext _context;

        public UserRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
