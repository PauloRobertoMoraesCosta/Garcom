using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.Repositorio;
using System;
using System.Collections.Generic;

namespace Garcom.Infra.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        RepositorioProduto RepositorioProduto { get; }
        RepositorioAuditoria RepositorioAuditoria { get; }
        RepositorioGrupoProduto RepositorioGrupoProduto { get; }
        RepositorioUsuario RepositorioUsuario { get; }
        RepositorioIngrediente RepositorioIngrediente { get; }
        RepositorioMesa RepositorioMesa { get; }
        RepositorioProdutoIngrediente RepositorioProdutoIngrediente { get; }
        RepositorioTamanhoProduto RepositorioTamanhoProduto { get; }
        RepositorioProdutoTamanhoValor RepositorioTamanhoProdutoValor { get; }
        RepositorioProdutosIngredienteTamanhoValor RepositorioProdutoIngredienteTamanho { get; }
        RepositorioExcecao RepositorioExcecao { get; }
        void Salvar();
        void Commit();
        void SalvarECommit();
        void Transacao();
        IEnumerable<ChangeLog> RetornaModificacoes();
    }
}
