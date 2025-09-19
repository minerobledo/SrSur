
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.Post;
using SrSur.Models.Entities;
using SrSur.Repositories;

namespace SrSur.Services
{
    public class PostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public PostServices(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Responce<bool>> CreatePost(CreatePostDto d)
        {

            var post = new Post()
            {
                Authorid = d.AuthorId,
                Title = d.Title,
                Body = d.Body,
                Commentstatus = d.CommentStatus,
                Poststatus = d.PostStatus,
                Excerpt = d.Excerpt,
                Createdate = DateTime.UtcNow,
                
            };
            if(d.CategoryId != null)
            {
                var cat = await _categoryRepository.GetByIdAsync(d.CategoryId.Value);
                if (cat != null) post.Categories.Add(cat);
            }
            if (d.TagsId != null)
            {
               
                foreach(var t in d.TagsId)
                {
                   var temp = await _tagRepository.GetByIdAsync(t.Value);
                   if (temp != null) post.Tags.Add(temp);
                }           
            }

            await _postRepository.AddAsync(post);
            return new Responce<bool>() { data = true, succeeded = true };


        }

        public async Task<Responce<bool>> EditPost(EditPostDto d)
        {

            var post = await _postRepository.GetByIdAsync(d.Id);
            if (post != null)
            {
                post!.Title = (post!.Title != d.Title!) ? d.Title! : post!.Title;
                post!.Body = (post!.Body != d.Body!) ? d.Body! : post!.Body;
                post!.Commentstatus = (post.Commentstatus != d.CommentStatus!) ? d.CommentStatus! : post.Commentstatus;
                post!.Excerpt = (post.Excerpt != d.Excerpt)? d.Excerpt! :post.Excerpt;
               
                if (d.CategoryId != null)
                {
                    var cat = await _categoryRepository.GetByIdAsync(d.CategoryId.Value);
                    if (cat != null) post.Categories.Add(cat);
                }
                if (d.TagsId != null)
                {

                    foreach (var t in d.TagsId)
                    {
                        var temp = await _tagRepository.GetByIdAsync(t.Value);
                        if (temp != null) post.Tags.Add(temp);
                    }
                }


                await _postRepository.UpdateAsync(post);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<bool>> DeletePost(long id)
        {
            var tag = await _postRepository.GetByIdAsync(id);
            if (tag != null)
            {
                await _postRepository.DeleteAsync(id);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<List<Post>>> GetPostFiltered(Dictionary<string,object> filtros, int page,bool admin)
        {
            var trueFiltros = new Dictionary<string, object>();
            foreach (var filter in filtros)
            {
                if (filter.Value != null)
                {
                    switch (filter.Key)
                    {
                        case "Titel":
                            // code block
                            if(filter.Value is string a) trueFiltros.Add(filter.Key, filter.Value);
                            break;
                        case "ContainsBody":
                            // code block
                            if(filter.Value is string b) trueFiltros.Add(filter.Key, filter.Value);
                            break;
                        case "Status":
                            Console.WriteLine("Si entra 1");
                            Console.WriteLine($"admin: {admin}");
                            if (admin == true && filter.Value.GetType() == typeof(string))
                            {
                                Console.WriteLine("Si entra 2");
                                trueFiltros.Add(filter.Key, filter.Value);

                            }
                            break;
                        case "CategoryId":
                            if (long.TryParse(filter.Value.ToString(), out long c))trueFiltros.Add(filter.Key,c);
                            break;
                        case "Tag":
                            if (long.TryParse(filter.Value.ToString(), out long d)) trueFiltros.Add(filter.Key,d);
                            break;

                        default:
                            // code block

                            break;
                    }
                }
            }
            if (admin== false)
            {
                trueFiltros.Add("Status", "publish");
            }
            var posts = await _postRepository.SerchByFilterAsync(trueFiltros, page, 50);
            return new Responce<List<Post>>() { data = posts, succeeded = true };
        }
         

    }
}
