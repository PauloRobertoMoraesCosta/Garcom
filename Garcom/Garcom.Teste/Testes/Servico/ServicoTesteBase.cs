using Garcom.Core;
using Garcom.Dominio.Entidade.Mapeamento;
using Garcom.Infra.UnitOfWork;

namespace Garcom.Teste.Testes.Servico
{
    internal abstract class ServicoTesteBase
    {
        protected IUnitOfWork _unitOfWork;
        protected IGerenciadorMensagens _mensagem;

        public ServicoTesteBase()
        {
            _unitOfWork = new UnitOfWork();
            _mensagem = new GerenciadorMensagensRetorno();
            MapperConfig.ConfigurarMapper();
        }
    }
}
