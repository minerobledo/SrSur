
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.Entities;
using SrSur.Repositories;

namespace SrSur.Services
{
    public class PostServices
    {
        private readonly IPostRepository _postRepository;

        public PostServices(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Responce<bool>> CreatePost(long authorId, string titel, string body, string comentStatus, string exert)
        {

            var comment = new Post()
            {
               Authorid = authorId,
               Title = titel,
               Body = body,
               Commentstatus = comentStatus,
               Poststatus = "Pending",
               Excerpt = exert

            };


            await _postRepository.AddAsync(comment);
            return new Responce<bool>() { data = true, succeeded = true };


        }

        public async Task<Responce<bool>> EditPost(long id, string? titel, string? body, string? comentStatus, string? exert)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                post!.Title = (post!.Title != titel!) ? titel! : post!.Title;
                post!.Body = (post!.Body != body!) ? body! : post!.Body;
                post!.Commentstatus = (post.Commentstatus != comentStatus!) ? comentStatus! : post.Commentstatus;
                post!.Excerpt = (post.Excerpt != exert)? exert! :post.Excerpt;

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

                        case "CategoryId":
                            if (long.TryParse(filter.Value.ToString(), out long c))trueFiltros.Add(filter.Key,c);
                            break;
                        case "Tag":
                            if (long.TryParse(filter.Value.ToString(), out long d)) trueFiltros.Add(filter.Key, filter.Value);
                            break;

                        default:
                            // code block

                            break;
                    }
                }
            }
            if (admin== false)
            {
                trueFiltros.Add("Status", "publis");
            }
            var posts = await _postRepository.SerchByFilterAsync(trueFiltros, page, 50);
            return new Responce<List<Post>>() { data = posts, succeeded = true };
        }
         

    }
}
