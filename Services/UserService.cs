using Microsoft.EntityFrameworkCore;
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.output;
using SrSur.Models.Entities;
using SrSur.Repositories;
using System;

namespace SrSur.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

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
        public async Task<Responce<LoginResponceDTO?>> Login(string email, string password)
        {
            var usuario = await _userRepository.GetByEmailAsync(email);

            if (usuario == null) throw new Exception("Usuario no encontrado");

            bool passwordValido = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
            if (!passwordValido) throw new Exception("Contraseña incorrecta");

            // Generar JWT y un nuevo RefreshToken cada login
            var jwt = _jwtService.GenerateToken(email);
            var refreshToken = _jwtService.GenerateRefeshToken();

            // Eliminar tokens viejos
            foreach (var oldToken in usuario.Refreshtokens)
            {
                oldToken.Revoked = true;
                oldToken.Replacedbytoken = refreshToken.Token;
            }

            // Agregar nuevo
            usuario.Refreshtokens.Clear(); // 🔹 si querés que quede solo 1
            usuario.Refreshtokens.Add(refreshToken);
            await _userRepository.UpdateAsync(usuario);

            var data = new LoginResponceDTO { JWT = jwt, RefreshToken = refreshToken.Token, UserId = usuario.Id };
            return new Responce<LoginResponceDTO?> { data = data, succeeded = true };
        }
        public async Task<Responce<bool>> Logout(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByRefreshTokenAsync(refreshToken);
            if (token == null) return new Responce<bool> { succeeded = true,data= false, message="Registro no encontrado"};

            token.Revoked = true;
            await _refreshTokenRepository.UpdateAsync(token);
            return new Responce<bool> { succeeded = true, data = true };
        }
        public async Task<Responce<LoginResponceDTO?>> Refresh(string refreshToken)
        {
            // 1. Buscar el refresh token en la DB
            var storedToken = await _refreshTokenRepository.GetByRefreshTokenAsync(refreshToken);

            if (storedToken == null || storedToken.Revoked || storedToken.Expirydate <= DateTime.UtcNow)
                return new Responce<LoginResponceDTO?> { data = null, succeeded = true , message ="Refresh token inválido o expirado" };

            var usuario = storedToken.User;

            // 2. Generar nuevo JWT
            var jwt = _jwtService.GenerateToken(usuario.Email);

            // 3. (Opcional) Generar un nuevo RefreshToken y reemplazar el viejo
            var newRefreshToken = _jwtService.GenerateRefeshToken();

            storedToken.Revoked = true;
            storedToken.Replacedbytoken = newRefreshToken.Token;

            usuario.Refreshtokens.Add(newRefreshToken);

            await _userRepository.UpdateAsync(usuario);

            // 4. Responder
            var data = new LoginResponceDTO
            {
                JWT = jwt,
                RefreshToken = newRefreshToken.Token,
                UserId = usuario.Id
            };

            return new Responce<LoginResponceDTO?> { data = data, succeeded = true };
        }
    }
}
