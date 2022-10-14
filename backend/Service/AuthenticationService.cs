using Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly IConfiguration _configuration;
        public AuthenticationService(UserManager<IdentityUser<Guid>> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(UserForAuthenticationDto userForAuth)
        {
            var user = await _userManager.FindByNameAsync(userForAuth.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, userForAuth.Password))
                throw new UserBadRequestException();

            var claims = await GetClaimsAsync(user);
            var signingCredentials = GetSigningCredentials();
            var tokenOpts = GenerateTokenOptions(signingCredentials, claims);

            var jwtHandler = new JwtSecurityTokenHandler();
            
            return jwtHandler.WriteToken(tokenOpts);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaimsAsync(IdentityUser<Guid> user)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["TokenValidityInMinutes"])),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
