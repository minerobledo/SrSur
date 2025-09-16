using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class PostVisitedByGusetRepository : IPostsVisitedByGuestRepository
    {
        private readonly SrsurDbContext _context;
        private readonly DbSet<Postsvisitedbyguest> _dbset;
        public PostVisitedByGusetRepository(SrsurDbContext context)
        {
            _context = context;
            _dbset = context.Set<Postsvisitedbyguest>();
        }
        public async Task AddAsync(Postsvisitedbyguest category)
        {
            await _context.Postsvisitedbyguests.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> TopRecentPosts()
        {
            var topRecentPosts = await _context.Postsvisitedbyguests
                .Where(pv => pv.Datevisited >= DateTime.UtcNow.AddDays(-30))
                .GroupBy(pv => pv.Post)
                .Select(g => new
                {
                    Post = g.Key,
                    VisitCount = g.Sum(pv => pv.Visitetime)
                })
                .OrderByDescending(x => x.VisitCount)
                .Take(10)
                .ToListAsync();
            var list = new List<Post>();
            foreach (var post in topRecentPosts)
            {
                list.Add(post.Post);
            }
            return list;
        }

        public async Task UpdateAsync(Postsvisitedbyguest category)
        {
            _dbset.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
