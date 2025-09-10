using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class TagRepository : ITagRepository
    {
        public readonly SrsurDbContext _context;
        public readonly DbSet<Tag> _dbset;

        public TagRepository(SrsurDbContext context)
        {
            _context = context;
            _dbset = context.Set<Tag>();
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

        public async Task<List<Tag>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _dbset
                .OrderBy(t => t.TagName)
                .Skip((pageNumber - 1) * pageSize) // salta los registros previos
                .Take(pageSize)                    // trae solo los que corresponden
                .ToListAsync();
        }
        public async Task<Tag?> GetByIdAsync(long id)
        {
            return await _context.Tags
               .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<List<Tag>> GetByPrefixAsync(string prefix, int pageNumber, int pageSize)
        {
            return await _context.Tags
                .Where(t => t.TagName.StartsWith(prefix))   // empieza con "prefix"
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(t => t.TagName)                    // ordenados por nombre
                .ToListAsync();
        }
        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags
               .FirstOrDefaultAsync(t => t.TagName == name);
        }

        public async Task UpdateAsync(Tag tag)
        {
            _dbset.Update(tag);
            await _context.SaveChangesAsync();
        }
    }
}
