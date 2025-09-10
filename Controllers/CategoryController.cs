using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs.Imput.Category;
using SrSur.Models.DTOs.Imput.Tag;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class CategoryController: ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("CreateTag")]
        public async Task<ActionResult> CreateTag([FromBody] CategoryDto d)
        {
            if (d.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await _categoryService.CreateCategory(d.CategoryName,d.ParentId,d.Slug,d.CategoryDescription);
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
                var responce = await _categoryService.EditCategory(d.TagId, d.TagName, d.Slug, d.TagDescription);
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
                var responce = await _categoryService.DeleteCategory(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("GetTag")]
        public async Task<ActionResult> GetTag([FromBody] int page)
        {
            try
            {
                var responce = await _categoryService.GetCategory(page);
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
                var responce = await _categoryService.SerchCategory(dto.Prefix!, dto.Page);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
