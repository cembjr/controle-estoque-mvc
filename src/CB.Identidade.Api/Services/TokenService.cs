using CB.Core.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static CB.Identidade.Api.Models.UsuarioViewModels;

namespace CB.Identidade.Api.Services
{
    public class TokenService
    {
        private readonly AppSettings _appSettings;
        public TokenService(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        public LoginResponse GerarToken(IdentityUser user, IList<Claim> claims = null, IList<string> roles = null)
        {
            var email = user.Email;
            var idUsuario = user.Id;

            claims = claims ?? new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, idUsuario));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));

            if (roles != null)
                roles.ToList().ForEach(userRole => claims.Add(new Claim("role", userRole)));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, claims, email, idUsuario);
        }



        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private LoginResponse ObterRespostaToken(string encodedToken, IEnumerable<Claim> claims, string email, string idUsuario)
        {
            return new LoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = idUsuario,
                    Email = email,
                    Claims = claims.ToDictionary(c => c.Type, c => c.Value)
                }
            };
        }

    }
}
