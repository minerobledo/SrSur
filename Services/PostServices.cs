
using AutoMapper.Configuration.Annotations;
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.Post;
using SrSur.Models.Entities;
using SrSur.Repositories;
using System.Text.Json;

namespace SrSur.Services
{
    public class PostServices
    {
        private readonly IResourseRepocitory _resourseRepocitory;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICloudflareRepocitory _cloudflareRepocitory;

        public PostServices(IResourseRepocitory resourseRepocitory, IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, ICloudflareRepocitory cloudflareRepocitory)
        {
            _resourseRepocitory = resourseRepocitory;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _cloudflareRepocitory = cloudflareRepocitory;
        }

        public async Task<Responce<bool>> CreatePost(CreatePostDto d)
        {
            var res = await _cloudflareRepocitory.UploadImage(d.FormFile);
            var image = res.Result;
            if (res.Success == true)
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
                    CloudflareId = image.Id,
                    FileName = image.Filename,
                    Variants = image.Variants


                };
                if(d.CategoryId != null)
                {
                    var cat = await _categoryRepository.GetByIdAsync(d.CategoryId.Value);
                    if (cat != null) post.Categories.Add(cat);
                }
                if (d.TagsId != null)
                {
                    
                    foreach (var t in d.TagsId)
                    {
                        var temp = await _tagRepository.GetByIdAsync(t);
                        if (temp != null) post.Tags.Add(temp);
                    }           
                }
                await _postRepository.AddAsync(post);
                return new Responce<bool>() { data = true, succeeded = true };
                
            }
            throw new Exception("Error en servidor de imagenes: " + res.Errors);
           



        }

        public async Task<Responce<List<Post?>>> GetPostWithNeighbors(Dictionary<string,object> filtres)
        {
            var r = await _postRepository.SearchPostWithNeighbors(filtres);
            return new Responce<List<Post?>>() { data = r, succeeded = true };
        }

        public async Task<Responce<bool>> EditPost(EditPostDto d)
        {

            var post = await _postRepository.GetByIdAsync(d.Id);
            if (post != null)
            {
                post!.Title = (post!.Title != d.Title!) ? d.Title! : post!.Title;
                post!.Body = (post!.Body != d.Body!) ? d.Body! : post!.Body;
                post!.Commentstatus = (post.Commentstatus != d.CommentStatus!) ? d.CommentStatus! : post.Commentstatus;
                post!.Excerpt = (post.Excerpt != d.Excerpt) ? d.Excerpt! : post.Excerpt;

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
        public async Task<Responce<bool>> EditImagePost(EditImagePostDto d)
        {

            var post = await _postRepository.GetByIdAsync(d.Id);
            if (post != null)
            {
              
                if (d.FormFile != null)
                {
                    var deleteresponce = await _cloudflareRepocitory.DeleteImageAsync(post.CloudflareId);
                    if (deleteresponce)
                    {
                        var cloudresponse = await _cloudflareRepocitory.UploadImage(d.FormFile);
                        if (cloudresponse.Success)
                        {
                            post.CloudflareId = cloudresponse.Result.Id;
                            post.FileName = cloudresponse.Result.Filename;
                            post.Variants = cloudresponse.Result.Variants;
                        }
                        else
                        {
                            return new Responce<bool>() { data = true, succeeded = true, message = "error en cargar la nueva imagen de cloudfler" };
                        }
                    }
                    else
                    {
                        return new Responce<bool>() { data = true, succeeded = true, message = "error en borrar la primera imagen de cloudfler" };
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
                            if(filter.Value is JsonElement je1 && je1.ValueKind == JsonValueKind.String) trueFiltros.Add(filter.Key, je1.GetString());
                            break;
                        case "ContainsBody":
                            // code block
                            if(filter.Value is JsonElement je2 && je2.ValueKind == JsonValueKind.String) trueFiltros.Add(filter.Key, je2.GetString());
                            break;
                        case "Status":
                          
                            if (admin == true && filter.Value is JsonElement je3 && je3.ValueKind == JsonValueKind.String)
                            {
                                string status = je3.GetString(); 
                                trueFiltros.Add(filter.Key, status);

                            }
                            break;
                        case "CategoryId":
                            if (long.TryParse(filter.Value.ToString(), out long c))trueFiltros.Add(filter.Key,c);
                            break;
                        case "TagId":
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
