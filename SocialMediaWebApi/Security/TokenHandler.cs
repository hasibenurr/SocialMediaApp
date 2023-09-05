using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace SocialMediaWebApi.Security
{
    public class TokenHandler
    {
        public static Token CreateToken(IConfiguration configuration)
        {
            Token token = new();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));

            JwtSecurityToken jwtSecurityToken = new(
                issuer: configuration["Token:Issuer"],
                audience: configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            token.AccessToken = handler.WriteToken(jwtSecurityToken);

            /// Create Refresh token
            byte[] numbers = new byte[32];
            using RandomNumberGenerator randomNumber = RandomNumberGenerator.Create();
            randomNumber.GetBytes(numbers);
            token.RefreshToken = Convert.ToBase64String(numbers);

            return token;
        }
    }
}
