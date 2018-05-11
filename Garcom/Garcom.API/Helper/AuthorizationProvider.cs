using Garcom.Aplicacao.Implementacao;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Infra.UnitOfWork;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Garcom.API.Helper
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            UsuarioDTO usuario = new UsuarioDTO();
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appUsuario = new AppUsuario(unitOfWork))
                    usuario = appUsuario.Autenticar(context.UserName, context.Password);
            }
            catch (MySql.Data.MySqlClient.MySqlException mysqlEx)
            {
                context.SetError("invalid_grant", "Não foi possível conectar com o banco de dados");
                return;
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("usr", context.UserName));
            identity.AddClaim(new Claim("role", "admin"));
            identity.AddClaim(new Claim(ClaimTypes.Role, usuario.PerfilId.ToString()));

            //var claimprincipal = new claimsprincipal(identity);

            //thread.currentprincipal = claimprincipal;

            context.Validated(identity);
        }
    }
}