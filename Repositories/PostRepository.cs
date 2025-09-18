
using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.DTOs.output;
using SrSur.Models.Entities;
using System.Linq;

namespace SrSur.Repositories
{
    public class PostRepository : IPostRepository
    {

        public readonly SrsurDbContext _context;
        public readonly DbSet<Post> _dbset;

        public PostRepository(SrsurDbContext context)
        {
            _context = context;
            _dbset = context.Set<Post>();
        }

        public async Task AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null) return;

            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>?> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(long id)
        {
            return await _context.Posts
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<List<Post>> SerchByFilterAsync(Dictionary<string, object> filters,int pageNumber, int pageSize)
        {
            IQueryable<Post> query = _context.Posts;

            foreach (var filtro in filters)
            {
                switch (filtro.Key)
                {
                    
                    case "Titel":
                        query = query.Where(r => r.Title!.StartsWith(filtro.Value.ToString()!));
                        break;
                   
                    case "ContainsBody":
                        query = query.Where(p => p.Body! .Contains(filtro.Value.ToString()!));
                        break;
                    case "Status":
                        query = query.Where(p=> p.Poststatus == filtro.Value.ToString());
                        break;
                    case "CategoryId":
                        query = query.Where(p => p.Categories.Any(c => c.Id == (long)filtro.Value || c.Parentid == (long)filtro.Value));
                        break;
                    case "Tag":
                        query = query.Where(p => p.Tags.Any(t => t.Id == (long)filtro.Value));
                        break;
                    default:
                        // Para propiedades genéricas necesitarías expresión dinámica
                        break;
                }
            }

            // Ordenar por CreateAt
            query = query.OrderByDescending(r => r.Createdate);

            // Paginación usando fecha
            query.Skip((pageNumber - 1) * pageSize);

            // Limitar resultados
            query = query
                .Take(pageSize)
                .Select(p => new Post
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    Createdate = p.Createdate,
                    Publishdate = p.Publishdate,
                    Lasteditdate = p.Lasteditdate,
                    Poststatus = p.Poststatus,
                    Excerpt = p.Excerpt,
                    Author = p.Author,
                    Categories = p.Categories.Select(c => new Category {
                        Id =c.Id,
                        Categoryname =c.Categoryname
                    }).ToList(),
                    Tags = p.Tags.Select(t=> new Tag {
                        Id = t.Id,
                        Tagname = t.Tagname,
                    }).ToList(),
                    Comments = p.Comments
                });

            return await query.ToListAsync(); 
        }
        

        public  async Task UpdateAsync(Post post)
        {
            _dbset.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
