using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SrsurDbContext _context;
        public readonly DbSet<Refreshtoken> _dbset;

        public RefreshTokenRepository(SrsurDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Refreshtoken token)
        {

            await _context.Refreshtokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task DeletaAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Refreshtoken>?> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Refreshtoken?> GetByIdAsync(long id)
        {
            return await _context.Refreshtokens
              .Include(u => u.Id) // Traemos los refresh tokens relacionados
              .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<Refreshtoken?> GetByRefreshTokenAsync(string token)
        {
            return await _context.Refreshtokens
              .Include(u => u.Id) // Traemos los refresh tokens relacionados
              .FirstOrDefaultAsync(u => u.Token == token);
        }
        public async Task<Refreshtoken?> GetByUserIdAsync(long id)
        {
            return await _context.Refreshtokens
              .Include(u => u.Userid) // Traemos los refresh tokens relacionados
              .FirstOrDefaultAsync(u => u.Userid == id);
        }

        public async Task UpdateAsync(Refreshtoken token)
        {
            _context.Refreshtokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
