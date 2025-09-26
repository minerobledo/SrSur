using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SrSur.Interface;
using SrSur.Models.DTOs.Imput.Guest;
using SrSur.Models.DTOs.Imput.Sponsor;
using SrSur.Services;
using System.Security.AccessControl;

namespace SrSur.Controllers
{
    public class SponsorController: ControllerBase
    {
        private readonly SponsorService _sponcorServices;
        

        public SponsorController(SponsorService sponcorRepository)
        {
            _sponcorServices = sponcorRepository;
        }

        [Authorize]
        [HttpPost("CreateSponsor")]
        public async Task<IActionResult> CreateSponsor([FromBody] CreateSponsorDto dto)
        {            
            try
            {
                var responce = await _sponcorServices.CreateSponsor(dto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpPost("EditSponsor")]
        public async Task<IActionResult> EditSponsor([FromBody] UpdateSponsorDto dto)
        {
            try
            {
                var responce = await _sponcorServices.UpdateSponsor(dto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("DeleteSponsor/{id}")]
        public async Task<IActionResult> DeleteSponsor([FromRoute] long id)
        {
            try
            {
                var responce = await _sponcorServices.DeleteSponsor(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetSponsors")]
        public async Task<IActionResult> GetSponsor()
        {
            try
            {
                var responce = await _sponcorServices.GetSponsors();
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("ChangeSponsorPaid/{id}")]
        public async Task<IActionResult> ChangeSponsorPaid([FromRoute] long id)
        {
            try
            {
                var responce = await _sponcorServices.ChangeSponsorPaid(id);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
