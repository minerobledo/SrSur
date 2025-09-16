using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface IPostsVisitedByGuestRepository
    {
        Task AddAsync(Postsvisitedbyguest guest);
        Task UpdateAsync(Postsvisitedbyguest guest);
        Task DeleteAsync(long id);
        Task<List<Post>> TopRecentPosts();
    }
}
