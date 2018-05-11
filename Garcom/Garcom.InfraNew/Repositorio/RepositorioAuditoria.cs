using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;

namespace Garcom.Infra.Repositorio
{
    public class RepositorioAuditoria : RepositorioBase<Auditoria>
    {
        public RepositorioAuditoria(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {}
    }
}
