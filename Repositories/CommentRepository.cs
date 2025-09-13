using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.Entities;

namespace SrSur.Repositories
{
    public class CommentRepository:ICommentRepository
    {

        public readonly SrsurDbContext _context;
        public readonly DbSet<Comment> _dbset;

        public CommentRepository(SrsurDbContext context, DbSet<Post> dbset)
        {
            _context = context;
            _dbset = context.Set<Comment>();
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>?> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(long id)
        {
            return await _context.Comments
               .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<List<Comment>> GetPaginatedAsync(int pageNumber, int pageSize, long? idPost = null)
        {
            IQueryable<Comment> query = _context.Comments;

            if(idPost != null)
            {
                query= query.Where(c => c.Postid == idPost);
                query = query.Where(c => c.Parentid == null);
            }
            query = query.OrderBy(t => t.Datecreate)
                .Skip((pageNumber - 1) * pageSize) // salta los registros previos
                .Take(pageSize);                    // trae solo los que corresponden

            return await query.ToListAsync();
        }
        public async Task<List<Comment>> GetPaginatedParentAsync(long parentID, int pageNumber, int pageSize)
        {
            return await _dbset
               .Where(t => t.Parentid== parentID)
               .OrderBy(t => t.Datecreate)
               .Skip((pageNumber - 1) * pageSize) // salta los registros previos
               .Take(pageSize)                    // trae solo los que corresponden
               .ToListAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _dbset.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}
