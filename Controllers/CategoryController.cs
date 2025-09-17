using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("AdminCreateCategory")]
        [Authorize]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryDto d)
        {
            
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
        [HttpPatch("AdminEditCategory")]
        [Authorize]
        public async Task<ActionResult> EditCategory([FromBody] EditCategoryDto d)
        {
            try
            {
                var responce = await _categoryService.EditCategory(d);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("DeleteCategory/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteCategory([FromRoute] long id)
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
        [HttpGet("GetCategory/{page}")]

        public async Task<ActionResult> GetCategory([FromRoute] int page)
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
        [HttpPost("SerchCategory")]
        
        public async Task<ActionResult> SerchCategory([FromBody] SerchTagDto dto)
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
