using Microsoft.EntityFrameworkCore;
using SrSur.Interface;
using SrSur.Models.DTOs.output;
using SrSur.Models.Entities;
using System;

namespace SrSur.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTService _jwtService;

        public UserService(IUserRepository userRepo, JWTService tokenService)
        {
            _userRepository = userRepo;
            _jwtService = tokenService;
        }

        // Registrar usuario
        public async Task CreateUserAsync(string email, string password,string nickname)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null) throw new Exception();

            var usuario = new User { Email = email, Password = BCrypt.Net.BCrypt.HashPassword(password),Nickname = nickname };
            await _userRepository.AddAsync(usuario);

            
            await _userRepository.UpdateAsync(usuario);
                       
        }

        // Login
        public async Task<LoginResponceDTO?> Login(string email, string password)
        {
            var usuario = await _userRepository.GetByEmailAsync(email);

            if (usuario == null) throw new Exception("Usuario no encontrado");

            bool passwordValido = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
            if (!passwordValido) throw new Exception("Contraseña incorrecta");

            // Generar JWT y un nuevo RefreshToken cada login
            var jwt = _jwtService.GenerateToken(email);
            var refreshToken = _jwtService.GenerateRefeshToken();

            usuario.Refreshtokens.Add(refreshToken);
            await _userRepository.UpdateAsync(usuario);

            return new LoginResponceDTO { JWT = jwt, RefreshToken = refreshToken.Token, UserId =usuario.Id};
        }

    }
}
