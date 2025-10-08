
using Microsoft.EntityFrameworkCore;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.DTOs.output;
using SrSur.Models.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;

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
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Max(pageSize, 1);
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
                    case "TagId":
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
            query = query.Skip((pageNumber - 1) * pageSize);

            // Limitar resultados
            query = query
                .Take(pageSize)
                .Select(p => new Post
                {
                    Id = p.Id,
                    Authorid = p.Authorid,
                    Title = p.Title,
                    Body = p.Body,
                    Createdate = p.Createdate,
                    Publishdate = p.Publishdate,
                    Lasteditdate = p.Lasteditdate,
                    Poststatus = p.Poststatus,
                    Excerpt = p.Excerpt,
                    Categories = p.Categories.Select(c => new Category {
                        Id =c.Id,
                        Categoryname =c.Categoryname
                    }).ToList(),
                    Tags = p.Tags.Select(t=> new Tag {
                        Id = t.Id,
                        Tagname = t.Tagname,
                    }).ToList(),
                    Comments = p.Comments,
                    Postsvisitedbyguests = p.Postsvisitedbyguests.Select(p => new Postsvisitedbyguest
                    {
                        Guestid = p.Guestid,
                    }).ToList(),
                    CloudflareId= p.CloudflareId,
                    FileName= p.FileName,
                    Variants = p.Variants
                });

            return await query.ToListAsync(); 
        }
        public async Task<List<Post?>> SearchPostWithNeighbors(Dictionary<string, object> filters)
        {    
            Post? basePost = null;
            // Si viene un filtro especial con "PostWithNeighbors"
            foreach (var f in filters)
            {
                switch (f.Key)
                {
                    case "Id":

                        long postId = ((JsonElement)filters["Id"]).GetInt64();
                        basePost = await _context.Posts
                            .Include(p => p.Categories)                            
                            .FirstOrDefaultAsync(p => p.Id == postId && p.Poststatus == "publish");
                        break;
                    case "Name":
                        var name = (string)filters["Name"];
                        basePost = await _context.Posts
                            .Include(p => p.Categories)
                            .FirstOrDefaultAsync(p => p.Title == name && p.Poststatus == "publish");
                        break;
                    default: break;

                }
            }
            // Obtengo el post base

            if (basePost == null)
                return new List<Post?>();

            var categoryId = basePost.Categories.FirstOrDefault()?.Id;

            if (categoryId == null)
                return new List<Post?>();

            // Post anterior
            var prevPost = await _context.Posts
                .Where(p => p.Poststatus == "publish" && p.Categories.Any(c => c.Id == categoryId) && p.Createdate < basePost.Createdate)
                .OrderByDescending(p => p.Createdate)
                .Select(ProjectPost)
                .FirstOrDefaultAsync(p => p.Poststatus == "publish");

            // Post posterior
            var nextPost = await _context.Posts
                .Where(p => p.Poststatus == "publish" && p.Categories.Any(c => c.Id == categoryId) && p.Createdate > basePost.Createdate)
                .OrderBy(p => p.Createdate)
                .Select(ProjectPost)
                .FirstOrDefaultAsync();

            // 3 randoms (distintos del actual, anterior y posterior)
            var excludedIds = new List<long> { basePost.Id };
            if (prevPost != null) excludedIds.Add(prevPost.Id);
            if (nextPost != null) excludedIds.Add(nextPost.Id);

            var randomPosts = await _context.Posts
                .Where(p => p.Poststatus == "publish" && p.Categories.Any(c => c.Id == categoryId) && !excludedIds.Contains(p.Id))
                .OrderBy(p => Guid.NewGuid()) // Random EF Core (no muy eficiente pero funciona)
                .Take(3)
                .Select(ProjectPost)
                .ToListAsync();

            var basePostProjected = await _context.Posts
               .Where(p => p.Id == basePost.Id)
               .Select(ProjectPost)
               .FirstOrDefaultAsync();
            // Combino todos
            var result = new List<Post?>();
            result.Add(basePostProjected);
            if (prevPost != null) result.Add(prevPost);
            if (nextPost != null) result.Add(nextPost);
            result.AddRange(randomPosts);

            return result;
           
        }

        public  async Task UpdateAsync(Post post)
        {
            _dbset.Update(post);
            await _context.SaveChangesAsync();
        }
        private static Expression<Func<Post, Post>> ProjectPost => 
            p=> new Post
            {
                Id = p.Id,
                Authorid = p.Authorid,
                Title = p.Title,
                Body = p.Body,
                Createdate = p.Createdate,
                Publishdate = p.Publishdate,
                Lasteditdate = p.Lasteditdate,
                Poststatus = p.Poststatus,
                Excerpt = p.Excerpt,
                Categories = p.Categories.Select(c => new Category
                {
                    Id = c.Id,
                    Categoryname = c.Categoryname
                }).ToList(),
                Tags = p.Tags.Select(t => new Tag
                {
                    Id = t.Id,
                    Tagname = t.Tagname,
                }).ToList(),
                Comments = p.Comments,
                Postsvisitedbyguests = p.Postsvisitedbyguests.Select(p => new Postsvisitedbyguest
                {
                    Guestid = p.Guestid,
                }).ToList(),
                CloudflareId = p.CloudflareId,
                FileName = p.FileName,
                Variants = p.Variants
            };
        
    }
}
