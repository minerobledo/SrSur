using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.DTOs.output;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class TagRepository : ITagRepository
    {
        public readonly SrsurDbContext _context;
        public readonly DbSet<Tag> _dbset;
        public readonly IMapper _mapper;

        public TagRepository(SrsurDbContext context, IMapper mapper)
        {
            _context = context;
            _dbset = context.Set<Tag>();
            _mapper = mapper;
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
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Max(pageSize, 1);
            return await _dbset
                .OrderBy(t => t.Tagname)
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
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Max(pageSize, 1);
            return await _context.Tags
                .Where(t => t.Tagname.StartsWith(prefix))   // empieza con "prefix"
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(t => t.Tagname)                    // ordenados por nombre
                .ToListAsync();
            

        }
        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags
               .FirstOrDefaultAsync(t => t.Tagname == name);
        }

        public async Task UpdateAsync(Tag tag)
        {
            _dbset.Update(tag);
            await _context.SaveChangesAsync();
        }

        public List<TagOutDto> TransformList(List<Tag> list)
        {
            List<TagOutDto> r = new List<TagOutDto>();
            foreach (Tag t in list)
            {
                r.Add(_mapper.Map<TagOutDto>(t));
            }
            return r;
        }
    }
}
