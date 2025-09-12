using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class GuestRepository: IGuestRepository
    {
        private readonly SrsurDbContext _context;
        private readonly DbSet<Guest> _dbset;

        public GuestRepository(SrsurDbContext context, DbSet<Guest> dbset)
        {
            _context = context;
            _dbset = dbset;
        }

        public async Task AddAsync(Guest guest)
        {
            await _context.Guests.AddAsync(guest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guest guest)
        {
            _dbset.Update(guest);
            await _context.SaveChangesAsync();
        }
    }

}
