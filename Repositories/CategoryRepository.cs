using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SrsurDbContext _context;
        private readonly DbSet<Category> _dbset;
        public CategoryRepository(SrsurDbContext context)
        {
            _context = context;
            _dbset =context.Set<Category>();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if(entity == null) return;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>?> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(long id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
               .FirstOrDefaultAsync(c => c.Categoryname == name);
            
        }

        public async Task UpdateAsync(Category category)
        {
            _dbset.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetByPrefixAsync(string prefix, int pageNumber, int pageSize)
        {
            return await _context.Categories
                .Where(t => t.Categoryname.StartsWith(prefix))   // empieza con "prefix"
                .OrderBy(t => t.Categoryname)                    // ordenados por nombre
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Category>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _dbset
                .OrderBy(t => t.Categoryname)
                .Skip((pageNumber - 1) * pageSize) // salta los registros previos
                .Take(pageSize)                    // trae solo los que corresponden
                .ToListAsync();
        }
    }
}
