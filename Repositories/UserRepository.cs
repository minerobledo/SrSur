using SrSur.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;

namespace SrSur.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly SrsurDbContext _context;

        public UserRepository(SrsurDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users
                .Include(u => u.RefreshTokens) // Traemos los refresh tokens relacionados
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}

