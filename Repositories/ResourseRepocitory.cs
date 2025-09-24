using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class ResourseRepocitory: IResourseRepocitory
    {
        public readonly SrsurDbContext _context;
        public readonly DbSet<Resourse> _dbset;
        public readonly IMapper _mapper;

        public ResourseRepocitory(SrsurDbContext context, IMapper mapper)
        {
            _context = context;
            _dbset = context.Set<Resourse>();
            _mapper = mapper;
        }

        public async Task CreateResorse(Resourse resourse)
        {
            await _context.Resourses.AddAsync(resourse);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> DeleteResorse(long Id)
        {
            var entity = await _dbset.FindAsync(Id);
            if (entity == null) return false;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task EditResourse(Resourse resourse)
        {
            _dbset.Update(resourse);
            await _context.SaveChangesAsync();
        }

        public async Task<Resourse?> GetResorseByID(long id)
        {
            return await _context.Resourses.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Resourse>> GetResourseList(string title,int pageNumber,int pageSize)
        {
            IQueryable<Resourse> query = _context.Resourses;
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(s => s.Title!.StartsWith(title));
            }
            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(10);
            return await query.ToListAsync();

        }
    }
}
