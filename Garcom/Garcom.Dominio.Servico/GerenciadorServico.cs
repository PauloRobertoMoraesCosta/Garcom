using Garcom.Dominio.Entidade.Models.Excecao;
using Garcom.Dominio.Servico.Interface;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico
{
    public class GerenciadorServico : IGerenciadorServico
    {
        private UnitOfWork _unitOfWork;

        public GerenciadorServico()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void RemoverBanco()
        {
            try
            {
                using (var servicoGrupoProduto = new ServicoGrupoProduto(_unitOfWork))
                using (var servicoIngrediente = new ServicoIngrediente(_unitOfWork))
                using (var servicoMesa = new ServicoMesa(_unitOfWork))
                using (var servicoProduto = new ServicoProduto(_unitOfWork))
                using (var servicoTamanhoProduto = new ServicoTamanhoProduto(_unitOfWork))
                {
                    servicoProduto.RemoverBanco();
                    _unitOfWork.Salvar();

                    servicoTamanhoProduto.RemoverBanco();
                    _unitOfWork.Salvar();

                    servicoGrupoProduto.RemoverBancoDados();
                    _unitOfWork.Salvar();

                    servicoMesa.RemoverBanco();
                    _unitOfWork.Salvar();

                    servicoIngrediente.RemoverBanco();
                    _unitOfWork.Salvar();
                }
            }
            catch (Exception ex)
            {
                string mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                using (var servicoExcecao = new ServicoExcecao(_unitOfWork))
                    servicoExcecao.Incluir(new Excecao { Rotina = "Remover Banco", Mensagem = mensagem });
            }
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
    }
}
