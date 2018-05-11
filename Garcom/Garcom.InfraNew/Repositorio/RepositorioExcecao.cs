using Garcom.Dominio.Entidade.Models.Excecao;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;

namespace Garcom.Infra.Repositorio
{
    public class RepositorioExcecao : RepositorioBase<Excecao>
    {
        public RepositorioExcecao(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            : base(dbEscrita, dbLeitura)
        {

        }
    }
}
