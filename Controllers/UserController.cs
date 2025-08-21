using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SrSur.Models.DTOs.Imput;
using SrSur.Services;

namespace SrSur.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody]RegistrarDto dto )
        {
            if (dto.IsComplete()== false) return BadRequest("Faltan Datos");
            try
            {
                await _userService.CreateUserAsync(dto.Email, dto.Password, dto.Nombre);
                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginDto dto)
        {
            if (dto.IsComplete()== false) return BadRequest("Faltan Datos");
            try
            {
                var responceDTO = await _userService.Login(dto.Email, dto.Password);
                return Ok(responceDTO);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
