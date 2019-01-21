using ESX.Api.Models.ModelView;
using ESX.Domain.Core.Entity;
using ESX.Domain.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace ESX.Api.Security
{
    public class TokenGenerator
    {
        public TokenModelView GenerateToken(
            Usuario usuario,
            TokenConfiguration tokenConfiguration
            )
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(usuario.Login, "Login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.UsuarioId),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login),
                }
            );

            foreach (var item in usuario.Roles)
                identity.AddClaim(new Claim(item.Nome, item.Nome));

            var dateCreate = DateTime.Now;

            var dateExpired = dateCreate + TimeSpan.FromDays(tokenConfiguration.ExpireIn);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfiguration.Issuer,
                Audience = tokenConfiguration.Audience,
                Subject = identity,
                NotBefore = dateCreate,
                Expires = dateExpired,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(tokenConfiguration.SigningKey)),
                    SecurityAlgorithms.HmacSha256
                )
            });

            var token = handler.WriteToken(securityToken);

            var tokenViewModel = new TokenModelView()
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Autenticado = true,
                Criacao = dateCreate,
                Expira = dateExpired,
                Token = token
            };

            return tokenViewModel;
        }
    }
}