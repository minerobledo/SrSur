using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs.Imput.SponsorImage;
using SrSur.Services;
using System.Security.AccessControl;

namespace SrSur.Controllers
{
    public class SponsorImageController: ControllerBase
    {
        private readonly SponsorImagService sponsorImagService;

        public SponsorImageController(SponsorImagService sponsorImagService)
        {
            this.sponsorImagService = sponsorImagService;
        }
        [Authorize]
        [HttpPost("CreateSponsorImage")]
        public async Task< IActionResult> CreateSponsorImage([FromForm] CreateSponsorImageDto dto)
        {
            if (!dto.IsComplete())
            {
                return BadRequest("Faltan Datos");
            }
            try
            {
                var result = await sponsorImagService.CreateSponsorImage(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpPatch("UpdateSponsorImage")]
        public async Task<IActionResult> UpdateSponsorImage([FromForm]EditSponsorImageDto dto)
        {
            if (!dto.IsComplete())
            {
                return BadRequest("Faltan Datos");
            }
            try
            {
                var result = await sponsorImagService.UpdateSponsorImage(dto);
                return Ok(result);
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("DeleteSponsorImage/{id}")]
        public async Task<IActionResult> DeleteSponsorImage([FromRoute]long id)
        {

            try
            {
            var result = await sponsorImagService.DeleteSponsorImage(id);
            return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetAllSponsorImage/{page}")]
        public async Task< IActionResult> GetAllSponsorImage([FromRoute] int page)
        {
            try
            {
                var result = await sponsorImagService.GetAllSponsorImage(page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetRandomSponsorImage/{page}")]
        public async Task<IActionResult> GetRandomSponsorImage([FromRoute] int count)
        {
            try
            {
                var result = await sponsorImagService.GetRandomSponsorImage(count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }

}
