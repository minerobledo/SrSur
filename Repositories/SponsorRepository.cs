using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.DTOs.Imput.Sponsor;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class SponsorRepository: ISponcorRepository
    {
        public readonly SrsurDbContext _context;
        public readonly DbSet<Sponsor> _dbset;
        public readonly IMapper _mapper;

        public SponsorRepository(SrsurDbContext context, DbSet<Sponsor> dbset,IMapper mapper)
        {
            _context = context;
            _dbset = context.Set<Sponsor>();
            _mapper = mapper;
        }

        public async Task CreateSponsor(Sponsor dto)
        {
            await _context.Sponsors.AddAsync(dto);
            await _context.SaveChangesAsync();            
        }

        public async Task<bool> DeleteSponsor(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return false ;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Sponsor>> GetSponsors()
        {
            return await _context.Sponsors.ToListAsync();            
        }

        public async Task<Sponsor?> GetSponsorById(long id)
        {
            return await _context.Sponsors.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateSponsor(Sponsor dto)
        {
            _dbset.Update(dto);
            await _context.SaveChangesAsync();
        }
    }
}
