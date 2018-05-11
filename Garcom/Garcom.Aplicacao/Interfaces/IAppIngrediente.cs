using Garcom.Dominio.Entidade.DTOs;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppIngrediente : IAppBase<IngredienteDTO>
    {
        void Excluir(IngredienteDTO dto);
        ICollection<ProdutoDTO> ValidaExclusao(int id);
        void Desfazer(IngredienteDTO dto);
    }
}
