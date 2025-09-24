using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs.Imput.PostVisitedByGuset;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class PostsVisitedByGuestController: ControllerBase
    {
        private readonly PostsVisitedByGuestServise servise;

        public PostsVisitedByGuestController(PostsVisitedByGuestServise servise)
        {
            this.servise = servise;
        }
        
        [HttpPost("CreateEntry")]
        public async Task<ActionResult> CreateEntry(CreatePostVisitedByGusetDto dto)
        {
            if (dto.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await servise.Create(dto.Postid,dto.Guestid,dto.Visitetime);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetPostTencency")]
        public async Task<ActionResult> GetPostTencency()
        {
            try
            {
                var responce = await servise.GetPostTendency();
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
