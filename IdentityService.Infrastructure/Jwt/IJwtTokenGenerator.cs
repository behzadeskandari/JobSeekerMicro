using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;

namespace IdentityService.Infrastructure.Jwt
{
    public interface IJwtTokenGenerator
    {
        public Task<string> GenerateToken(User user);

        Task<string> GetToken(User user);

        public JwtTokenClaims ReadToken(string token);

        public Task<string> GenerateRefreshToken();
    }
}
