using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.Entities;
using SrSur.Repositories;

namespace SrSur.Services
{
    public class CommentServices
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public CommentServices(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<Responce<bool>> CreateAdminComment(long PostID, long userId, long? parentId, string content, string device, string country, string region)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var post = await _postRepository.GetByIdAsync(PostID);
            if (user != null && post != null)
            {
                var comment = new Comment()
                {
                    Postid = PostID,
                    Userid = userId,
                    Parentid = parentId,
                    Author = user.Nickname,
                    Emailauthor = user.Email,
                    Content = content,
                    Device = device,
                    Country = country,
                    Region = region
                    
                };


                await _commentRepository.AddAsync(comment);
                return new Responce<bool>() { data = true, succeeded = true };
            }
            return new Responce<bool>() { data = false, succeeded = true };
        }
        public async Task<Responce<bool>> CreateComment(long PostID, long? parentId, string author, string email, string content, string device, string country, string region, string authorUrl)
        {
            var post = await _postRepository.GetByIdAsync(PostID);
            if (post != null)
            {
                var comment = new Comment()
                {

                    Postid = PostID,
                    Parentid = parentId,
                    Author = author,
                    Emailauthor = email,
                    Content = content,
                    Device = device,
                    Country = country,
                    Region = region,
                    Authorurl = authorUrl
                };


                await _commentRepository.AddAsync(comment);
                return new Responce<bool>() { data = true, succeeded = true };
            }
            return new Responce<bool>() { data = false, succeeded = true };
        }

        public async Task<Responce<bool>> EditComment(long id, string content)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment != null)
            {
                comment!.Content = (comment!.Content != content!) ? content! : comment!.Content;
              
                await _commentRepository.UpdateAsync(comment);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<bool>> DeleteComment(long id)
        {
            var tag = await _commentRepository.GetByIdAsync(id);
            if (tag != null)
            {
                await _commentRepository.DeleteAsync(id);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<List<Comment>>> GetComments(int page)
        {
            var categories = await _commentRepository.GetPaginatedAsync(page, 50);
            return new Responce<List<Comment>>() { data = categories, succeeded = true };
        }
        public async Task<Responce<List<Comment>>> GetCommentsByPost(int page, long idPost)
        {
            var categories = await _commentRepository.GetPaginatedAsync(page, 50,idPost);
            return new Responce<List<Comment>>() { data = categories, succeeded = true };
        }
        public async Task<Responce<List<Comment>>> GetCommentsParent(int page, long id)
        {
            var categories = await _commentRepository.GetPaginatedParentAsync(id, page, 50);
            return new Responce<List<Comment>>() { data = categories, succeeded = true };
        }


    }
}
