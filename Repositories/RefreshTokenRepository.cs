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
        public readonly DbSet<Tag> _dbset;

        public RefreshTokenRepository(SrsurDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tag tag)
        {

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Tag>?> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(long id)
        {
            return await _context.Tags
              .Include(u => u.Id) // Traemos los refresh tokens relacionados
              .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags
              .Include(u => u.Tagname) // Traemos los refresh tokens relacionados
              .FirstOrDefaultAsync(u => u.Tagname == name);
        }

        public async Task UpdateAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }
    }
}
