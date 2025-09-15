using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs.Imput.Coment;
using SrSur.Models.DTOs.Imput.Post;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class ComentController:ControllerBase
    {
        private readonly CommentServices _commentServices;

        public ComentController(CommentServices commentServices)
        {
            _commentServices = commentServices;
        }
        /// <summary>
        /// endpoint para crear un comentario y sub comentarios ligados a un post
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AdminCreateComment")]
        public async Task<ActionResult> CreateComment([FromBody] CreateComentDto d)
        {
            try
            {
                var responce = await _commentServices.CreateComment(d.UserId,d.ParentId,d.Author,d.EmailAuthor,d.Content,d.Device,d.Country,d.Region,d.AuthorUrl);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Endpoint para que el admin edite algun cometario
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("AdminEditComment")]
        public async Task<ActionResult> EditComment([FromBody] EditComentDto d)
        {
            if (d.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await _commentServices.EditComment(d.Id,d.Body!);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// endpoin para que el admin elimine un comentario
        /// </summary>
        /// <param name="id">Id del comenttario a eliminar</param>
        /// <returns></returns>
        [HttpDelete("AdminDeleteComment/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteComment([FromRoute] long id)
        {
            try
            {
                var responce = await _commentServices.DeleteComment(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// endpoint para que el admin obtenga odos los comentarios en existencia
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("AdminGetAllComments/{page}")]
        [Authorize]
        public async Task<ActionResult> GetAllComments([FromRoute] int page)
        {
            try
            {
                var responce = await _commentServices.GetComments(page);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// edpoint para obtener todos los comentarios de un post
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpPost("GetCommentsByPost")]
        public async Task<ActionResult> GetCommentsByPost([FromBody] GetComentByPostDto d)
        {
            if (d.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await _commentServices.GetCommentsByPost(d.Page,d.PostId);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// endpoin para obtener todos los comentarios hijos de otro comentario
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpPost("GetCommentsChild")]
        public async Task<ActionResult> GetCommentsChild([FromBody] GetComentParentsByPostDto d)
        {
            if (d.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await _commentServices.GetCommentsParent(d.Page, d.CommentParent);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
         
    }
}
