using System;

namespace Garcom.Dominio.Servico.Interface
{
    public interface IGerenciadorServico : IDisposable
    {
        void RemoverBanco();
    }
}
