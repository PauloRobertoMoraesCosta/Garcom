using Garcom.Dominio.Entidade.DTOs;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppTamanhoProduto : IAppBase<TamanhoProdutoDTO>
    {
        ICollection<string> ValidarExclusao(int id);
        void Excluir(TamanhoProdutoDTO tamanhoProdutoDTO);
        void Desfazer(TamanhoProdutoDTO tamanhoDTO);
    }
}
