using Garcom.Dominio.Entidade.DTOs;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppGrupoProduto : IAppBase<GrupoProdutoDTO>
    {
        ICollection<GrupoProdutoDTO> ListarTodosComTamanhos();
        ICollection<string> ValidaExclusao(int id);
        void Desfazer(GrupoProdutoDTO grupoProdutoDTO);
        void Excluir(GrupoProdutoDTO grupoProdutoDTO);
    }
}
