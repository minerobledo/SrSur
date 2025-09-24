using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class SponsorImageRepocitory : ISponsorImageRepocitory
    {
        public readonly SrsurDbContext _context;
        public readonly DbSet<SponsorImage> _dbset;

        public SponsorImageRepocitory(SrsurDbContext context)
        {
            _context = context;
            _dbset = context.Set<SponsorImage>();
        }

        public async Task CreateAsync(SponsorImage image)
        {
            await _context.SponsorsImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

       

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return false;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

       

        public async Task<List<SponsorImage>> GetAllPagitatedAsync(int pageNumber, int pageSize)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Max(pageSize, 1);
            IQueryable<SponsorImage> query = _context.SponsorsImages;
            query= query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();
            
        }

        public async Task<SponsorImage?> GetAsync(long id)
        {
            return await _context.SponsorsImages
              .Include(u => u.Id) // Traemos los refresh tokens relacionados
              .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<SponsorImage>> GetRandomAsync(int count)
        {
            // 1. Traer imágenes con sus sponsors
            var images = await _context.SponsorsImages
                .Include(si => si.Sponsor)
                .ToListAsync();

            if (!images.Any()) return new List<SponsorImage>();

            // 2. Calcular el total de pesos
            double totalWeight = images.Sum(i => i.Sponsor.Amount);

            var random = new Random();
            var result = new List<SponsorImage>();

            for (int k = 0; k < count; k++)
            {
                // 3. Generar un número aleatorio en el rango [0, totalWeight)
                double r = random.NextDouble() * totalWeight;
                double cumulative = 0;

                // 4. Selección proporcional al peso
                foreach (var img in images)
                {
                    cumulative += img.Sponsor.Amount;
                    if (r <= cumulative)
                    {
                        result.Add(img);
                        break;
                    }
                }
            }

            return result;
        }

        public async Task<List<SponsorImage>> GetSponsors()
        {
            return await _dbset.ToListAsync();
        }

        public async Task UpdateSponsor(SponsorImage dto)
        {
            _dbset.Update(dto);
            await _context.SaveChangesAsync();
        }
    }
}
