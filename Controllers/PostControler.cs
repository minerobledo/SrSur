using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.Post;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class PostControler : ControllerBase
    {
        private readonly PostServices _postServices;

        public PostControler(PostServices postServices)
        {
            _postServices = postServices;
        }
        [HttpPost("CreatePost")]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDto d)
        {
            if (d.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await _postServices.CreatePost(d.AuthorId,d.Title!,d.Body!,d.CommentStatus!,d.Excerpt!);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("EditPost")]
        public async Task<ActionResult> EditPost([FromBody] EditPostDto d)
        {
            try
            {
                var responce = await _postServices.EditPost(d.Id, d.Title, d.Body, d.CommentStatus, d.Excerpt);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("DeletePost")]
        public async Task<ActionResult> DeletePost([FromBody] long id)
        {
            try
            {
                var responce = await _postServices.DeletePost(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("SerchPost")]
        public async Task<ActionResult> SerchPost([FromBody] SerchPostDto dto)
        {
            try
            {
                var responce = await _postServices.GetPostFiltered(dto.Filtros, dto.Page);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
