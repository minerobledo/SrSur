using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs.Imput.Post;
using SrSur.Models.DTOs.Imput.Tag;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class TagController : ControllerBase
    {
        private readonly TagSeervice _tagSeService;

        public TagController(TagSeervice tagSeService)
        {
            _tagSeService = tagSeService;
        }
        [HttpPost("CreateTag")]
        public async Task<ActionResult> CreateTag([FromBody] TagDto d)
        {
            if (d.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await _tagSeService.CreateTag(d.TagName,d.Slug,d.TagDescription);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("EditTag")]
        public async Task<ActionResult> EditTag([FromBody] EditTagDto d)
        {
            try
            {
                var responce = await _tagSeService.EditTag(d.TagId, d.TagName, d.Slug, d.TagDescription);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("DeleteTag")]
        public async Task<ActionResult> DeleteTag([FromBody] long id)
        {
            try
            {
                var responce = await _tagSeService.DeleteTag(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("GetTag")]
        public async Task<ActionResult> GetTag ([FromBody] int page)
        {
            try
            {
                var responce = await _tagSeService.GetTags(page);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("SerchTag")]
        public async Task<ActionResult> SerchTag([FromBody] SerchTagDto dto)
        {
            try
            {
                var responce = await _tagSeService.SerchTag(dto.Prefix!,dto.Page);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
