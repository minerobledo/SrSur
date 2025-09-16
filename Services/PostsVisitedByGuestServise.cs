using Microsoft.AspNetCore.Components.Forms;
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.Coment;
using SrSur.Models.Entities;
using System.Runtime.InteropServices;

namespace SrSur.Services
{
    public class PostsVisitedByGuestServise
    {
        private readonly IPostsVisitedByGuestRepository _repo;

        public PostsVisitedByGuestServise(IPostsVisitedByGuestRepository repo)
        {
            _repo = repo;
        }

        public async Task<Responce<bool>> Create(long Postid, long Guestid,int Visitetime)
        {
            var item = new Postsvisitedbyguest
            {
                Postid = Postid,
                Guestid = Guestid,
                Visitetime = Visitetime
            };
            await _repo.AddAsync(item);
            return new Responce<bool> {data = true,succeeded= true};
        }
        public async Task<Responce<List<Post>>> GetPostTendency()
        { 
            var posts = await _repo.TopRecentPosts();
            return new Responce<List<Post>> { data =posts, succeeded= true };
        }

    }
}
