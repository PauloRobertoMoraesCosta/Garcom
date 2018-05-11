using Garcom.Dominio.Entidade.DTOs;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppUsuario : IAppBase<UsuarioDTO>
    {
        ICollection<PerfilDTO> ListaTodosPerfil();
        UsuarioDTO SelecionarPorLogin(string login);
        UsuarioDTO Autenticar(string login, string senha);
    }
}
