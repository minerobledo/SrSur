using Microsoft.IdentityModel.Tokens;
using SrSur.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SrSur.Services
{
    public class JWTService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;


        public JWTService(IConfiguration configuration)
        {
            _secret = configuration["JWT:Key"] ?? "deault";
            _issuer = configuration["JWT:Issuer"] ?? "deault";
            _audience = configuration["JWT:Audience"] ?? "deault";
        }




        public string GenerateToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            string encryptedUserEmail = Encrypt(email);
            var claims = new[]
            {

                new Claim("Email",encryptedUserEmail)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var prinsipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = securityKey
                }, out var validatedToken);

                return prinsipal;
            }
            catch
            {
                return null;
            }
        }

        public string JWTReader(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(jwt);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        }

        public RefreshToken GenerateRefeshToken()
        {
            var token = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), // string seguro
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                CreatedDate = DateTime.UtcNow
            };
            return token;
        }

       
        public string? GetUserEmailFromJwt(ClaimsPrincipal claims)
        {
            var claimValue = claims.FindFirst("Email")?.Value;

            if (string.IsNullOrEmpty(claimValue))
                return null;

            return Decrypt(claimValue);
        }

        //funciones internas
        private string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_secret.Substring(0, 32)); // 32 bytes para AES-256
                aes.IV = new byte[16]; // IV en cero para simplicidad, pero deberías usar un IV seguro

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
        private string Decrypt(string cipherText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_secret.Substring(0, 32));
                aes.IV = new byte[16];

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                var cipherBytes = Convert.FromBase64String(cipherText);
                var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
