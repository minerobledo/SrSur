using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SrSur.Models.DTOs.Imput.Guest;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class GuestController: ControllerBase
    {
        private readonly GuestServices _services;

        public GuestController(GuestServices services)
        {
            _services = services;
        }
       
        [HttpPost("CreateGuest")]
        public async Task<IActionResult> CreateGuest([FromBody] CreateGuestDto dto)
        {
            if (dto.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responce = await _services.CreateGuset(dto.Useragent,dto.Ipaddress,dto.Referrer,dto.Country,dto.Region);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("AdminDeleteGuset/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteGuest([FromRoute] long id)
        {
            try
            {
                var responce = await _services.DeleteGuest(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
