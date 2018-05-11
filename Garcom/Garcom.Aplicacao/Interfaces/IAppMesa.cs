using Garcom.Dominio.Entidade.DTOs;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppMesa : IAppBase<MesaDTO>
    {
        ICollection<MesaDTO> ListarTodasAtivas();
        MesaDTO SelecionaPorDescricao(string descricao);
        void Excluir(MesaDTO mesaDTO);
        void Desfazer(MesaDTO mesa);
    }
}
