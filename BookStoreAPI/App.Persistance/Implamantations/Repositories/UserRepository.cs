using App.Application.Interfaces.User;
using App.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Implamantations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public void UpdateAsync(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
