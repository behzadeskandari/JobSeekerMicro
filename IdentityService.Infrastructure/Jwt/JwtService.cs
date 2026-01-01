using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;
using JobSeeker.Shared.Models.Roles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Jwt
{
    public class JwtService : IJwtTokenGenerator
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _jwtKey;
        public JwtService(IConfiguration config, IMemoryCache memoryCache)
        {
            _config = config;
            _cache = memoryCache;
            // jwtToken is used for bath encrypting and description the JWT token 
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

        }
        public async Task<string> GenerateToken(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()), // Standard JWT claim for subject (UserId)
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Keep for backward compatibility
                new Claim(ClaimTypes.Email, user.Email ?? user.UserName ?? string.Empty),
                new Claim("email", user.Email ?? user.UserName ?? string.Empty), // Standard JWT claim
                new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim("given_name", user.FirstName ?? string.Empty), // Standard JWT claim
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                new Claim("family_name", user.LastName ?? string.Empty), // Standard JWT claim
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim()),
                new Claim("name", $"{user.FirstName} {user.LastName}".Trim()), // Standard JWT claim
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
            };

            var creadentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.Now.AddDays(int.Parse(_config["JWT:ExpiresInDays"])),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                SigningCredentials = creadentials,
            };
            //await _signInManager.SignInAsync(user, isPersistent: false);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(jwt);
            if (string.IsNullOrEmpty(token) || token.Split('.').Length != 3)
            {
                _cache.Set(user.Id, token, TimeSpan.FromDays(int.Parse(_config["JWT:ExpiresInDays"])));
                throw new Exception("Generated JWT is invalid");
            }
            return token;
        }

        public async Task<string> GetToken(User user)
        {
            //string token = _cache.Get<string>(user.UserName);
            //if (token == null)
            //{
            var token = await GenerateToken(user);
            // _cache.Set(user.UserName, token);
            //}
            return token;
        }

        public JwtTokenClaims ReadToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
                throw new ArgumentException("Invalid JWT token");

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken.ValidTo <= DateTime.Now)
            {
                return new JwtTokenClaims();
            }
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var roles = jwtToken.Claims.FirstOrDefault(X => X.Value == AppRoles.Staff | 
                                                       X.Value == AppRoles.Admin | 
                                                       X.Value == AppRoles.User);
            var given_name = jwtToken.Claims.FirstOrDefault(c => c.Type == "given_name");
            var family_name = jwtToken.Claims.FirstOrDefault(x => x.Type == "family_name");
            var userId = jwtToken.Claims.First().Value;

            return new JwtTokenClaims
            {
                UserId = userId,
                Email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty,
                FirstName = given_name.ToString(),
                LastName = family_name.ToString(),
                Role = roles.Value,
                ExpiresAt = jwtToken.ValidTo
            };
        }

        public Task<string> GenerateRefreshToken()
        {
            // Get refresh token length from configuration (default to 32 bytes if not specified)
            int tokenLength = int.TryParse(_config["JWT:RefreshTokenLength"], out var length) ? length : 32;

            // Generate a cryptographically secure random string
            byte[] randomBytes = RandomNumberGenerator.GetBytes(tokenLength);
            string refreshToken = Convert.ToBase64String(randomBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "")
                .Substring(0, tokenLength);

            // Set expiration for refresh token (default to 7 days if not specified)
            int refreshTokenExpiryDays = int.TryParse(_config["JWT:RefreshTokenExpiresInDays"], out var expiry) ? expiry : 7;

            // Store the refresh token in cache with expiration
            _cache.Set($"RefreshToken_{refreshToken}", true, TimeSpan.FromDays(refreshTokenExpiryDays));

            return Task.FromResult(refreshToken);
        }
    }

}
