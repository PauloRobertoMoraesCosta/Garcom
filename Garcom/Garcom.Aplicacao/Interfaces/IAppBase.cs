using System;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppBase<DTO> : IDisposable
        where DTO : class
    {
        DTO Incluir(DTO dto);
        DTO Alterar(DTO dto);
        DTO SelecionarPorId(int id);
        ICollection<DTO> ListarTodos();
    }
}
