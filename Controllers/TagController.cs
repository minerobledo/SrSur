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
        [HttpPost("AdminCreateTag")]
        public async Task<ActionResult> CreateTag([FromBody] TagDto d)
        {
            
            try
            {
                var responce = await _tagSeService.CreateTag(d);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("AdminEditTag")]
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
        [HttpDelete("DeleteTag/{id}")]
        public async Task<ActionResult> DeleteTag([FromRoute] long id)
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
        [HttpGet("GetTag/{page}")]
        public async Task<ActionResult> GetTag ([FromRoute] int page)
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
