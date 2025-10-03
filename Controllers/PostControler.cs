using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("AdminCreatePost")]
        [Authorize]
        public async Task<ActionResult> CreatePost([FromForm] CreatePostDto d)
        {

            try
            {
                var responce = await _postServices.CreatePost(d);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("AdminEditPost")]
        [Authorize]
        public async Task<ActionResult> EditPost([FromBody] EditPostDto d)
        {
            try
            {
                var responce = await _postServices.EditPost(d);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("AdminEditImagePost")]
        [Authorize]
        public async Task<ActionResult> EditImagePost([FromForm] EditImagePostDto d)
        {
            try
            {
                var responce = await _postServices.EditImagePost(d);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("AdminDeletePost{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePost([FromRoute] long id)
        {
            try
            {
                var responce = await _postServices.DeletePost(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("AdminSerchPost")]
        [Authorize]
        public async Task<ActionResult> AdminSerchPost([FromBody] SerchPostDto dto)
        {
            try
            {
                var responce = await _postServices.GetPostFiltered(dto.Filtros, dto.Page, true);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("SerchPost")]
        public async Task<ActionResult> SerchPost([FromBody] SerchPostDto dto)
        {
            try
            {
                var responce = await _postServices.GetPostFiltered(dto.Filtros, dto.Page, false);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetPostWithNeighbors")]
        public async Task<ActionResult> GetPostWithNeighbors([FromBody] Dictionary<string,object> dto)
        {
            try
            {
                var responce = await _postServices.GetPostWithNeighbors(dto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
