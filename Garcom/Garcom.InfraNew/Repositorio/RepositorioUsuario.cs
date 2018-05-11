using Dapper;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Garcom.Infra.Repositorio
{
    /// <summary>
    /// Repositório da classe Usuário
    /// </summary>
    public class RepositorioUsuario : RepositorioBase<Usuario>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioUsuario(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {
        }

        /// <summary>
        /// Método responsável por retornar todos os usuários cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Usuario> ListaTodosUsuarios()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT id, login, nome, perfilId, ativo FROM usuario");
            sql.Append("where excluido <> 1 order by Id");

            var usuarios = _dbLeitura.MySqlConnection
                                    .Query<Usuario>(sql.ToString())
                                    .ToList();
            
            return usuarios;
        }

        /// <summary>
        /// Método responsável por selecionar um usuário pelo login
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <returns>Usuario</returns>
        public Usuario SelecionaUsuarioPorLogin(string login)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT id, login, nome, senha, ativo, datacadastro, perfilId FROM usuario");
            sql.Append("WHERE login = @Login");

            var usuario = _dbLeitura.MySqlConnection
                                    .Query<Usuario>(sql.ToString(),
                                                    new { Login = login })
                                    .FirstOrDefault();
            
            return usuario;
        }

        /// <summary>
        /// Método responsável por selecionar um usuário pelo id
        /// </summary>
        /// <param name="id">Login do usuário</param>
        /// <returns>Usuario</returns>
        public Usuario SelecionaUsuarioPorId(int id)
        {
            var usuario = _dbLeitura.MySqlConnection.Query<Usuario>("SELECT id, login, nome, senha, ativo, datacadastro, perfilId FROM usuario WHERE id = @Id",
                                                                    new { Id = id }).FirstOrDefault();
            
            return usuario;
        }

        /// <summary>
        /// Método responsável por selecionar um usuário ativo pelo seu login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Usuario SelecionaUsuarioAtivoPorLogin(string login)
        {
            var usuario = _dbLeitura.MySqlConnection.Query<Usuario>("SELECT id, login, nome, perfilId FROM usuario WHERE login = @Login and ativo = 1",
                                                                    new { Login = login }).FirstOrDefault();
            
            return usuario;
        }

        /// <summary>
        /// Método responsável por selecionar um usuário ativo com o atributo senha pelo seu login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Usuario SelecionaUsuarioAtivoComSenhaPorLogin(string login)
        {
            var usuario = _dbLeitura.MySqlConnection.Query<Usuario>("SELECT id, login, senha, nome, perfilId FROM usuario WHERE login = @Login and ativo = 1",
                                                                    new { Login = login }).FirstOrDefault();
            
            return usuario;
        }

        /// <summary>
        /// Método responsável por listar todos os perfieis
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Perfil> ListaTodosPerfil()
        {
            var perfies = _dbLeitura.MySqlConnection.Query<Perfil>("SELECT id, descricao FROM perfil where excluido <> 1 order by descricao").ToList();
            
            return perfies;
        }

        /// <summary>
        /// Método responsável por selecionar o perfil pelo id
        /// </summary>
        /// <returns></returns>
        public Perfil SelecionaPerfilPorId(int  id)
        {
            var perfies = _dbLeitura.MySqlConnection.Query<Perfil>("SELECT id, descricao FROM perfil where id = @Id",
                                                                    new { Id = id }).FirstOrDefault();
            
            return perfies;
        }
    }
}
