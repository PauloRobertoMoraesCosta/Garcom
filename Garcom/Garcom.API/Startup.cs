using Garcom.API.Helper;
using Garcom.Dominio.Servico;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Garcom.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(appBuilder);

            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(httpConfiguration);

            try
            {
                //RemoverBanco();
            }
            catch
            {
            }
        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/autenticar"), //URL usada para gerar o token
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1), //Configura o tempo que o token vai expirar
                Provider = new AuthorizationProvider()
            };

            appBuilder.UseOAuthAuthorizationServer(oAuthOptions);
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private async Task RemoverBanco()
        {
            using (var gerenciadorServico = new GerenciadorServico())
            {
                await Task.Run(() =>
                {
                    gerenciadorServico.RemoverBanco();
                });
            }
        }

    }
}
