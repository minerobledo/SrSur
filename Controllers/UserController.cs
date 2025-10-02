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
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto.IsComplete() == false) return BadRequest("Faltan Datos");
            try
            {
                var responceDTO = await _userService.Login(dto.Email, dto.Password);
                return Ok(responceDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [Authorize]
        [HttpPost("Logout/{token}")]
        public async Task<ActionResult> Logout([FromRoute] string token)
        {

            try
            {
                var responceDTO = await _userService.Logout(token);
                return Ok(responceDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [Authorize]
        [HttpPost("Refresh/{token}")]
        public async Task<ActionResult> Refresh([FromRoute] string token)
        {

            try
            {
                var responceDTO = await _userService.Refresh(token);
                return Ok(responceDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturado:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"Tipo: {ex.GetType().FullName}");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(ex.StackTrace); // Acá vas a ver la línea
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
