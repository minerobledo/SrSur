using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs.Imput.resourse;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class ResourseController : ControllerBase
    {
        private readonly ResourseServices _services;

        public ResourseController(ResourseServices services)
        {
            _services = services;
        }
        [Authorize]
        [HttpPost("AdminCreateResourse")]
        public async Task<IActionResult> CreateResourse([FromForm]CreateResourseDto dto)
        {
            if (!dto.IsComplete())
            {
                return BadRequest("faltan datos");
            }
            try
            {
                var responce = await _services.CreateResourse(dto);
                return Ok(responce);

            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [Authorize]
        [HttpDelete("AdminDeleteResourse/{id}")]
        public async Task<IActionResult> DeleteResourse([FromRoute] long id)
        {
           
            try
            {
                var responce = await _services.DeleteResorse(id);
                return Ok(responce);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [Authorize]
        [HttpPatch("AdminEditResourse")]
        public async Task<IActionResult> EditResourse([FromBody]EditResourseDto dto)
        {
            if (!dto.IsComplete())
            {
                return BadRequest("faltan datos");
            }
            try
            {
                var responce = await _services.EditResourse(dto);
                return Ok(responce);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [Authorize]
        [HttpPost("AdminGetResourseList")]
        public async Task<IActionResult> GetResourseList([FromBody]GetResourseDto dto)
        {
            if (!dto.IsComplete())
            {
                return BadRequest("faltan datos");
            }
            try
            {
                var responce = await _services.GetResourses(dto);
                return Ok(responce);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
