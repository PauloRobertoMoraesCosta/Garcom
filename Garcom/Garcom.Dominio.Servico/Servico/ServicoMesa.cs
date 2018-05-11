using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Servico.Servico
{
    public class ServicoMesa : ServicoBase<Mesa>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ServicoMesa(IUnitOfWork unitOfWork) : base(unitOfWork, new ValidacaoMesa(unitOfWork))
        {}

        /// <summary>
        /// Método responsável por incluir uma mesa
        /// </summary>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        public Mesa Incluir(Mesa mesa)
        {
            if (mesa.DataCadastro == null) mesa.DataCadastro = DateTime.Now;
            mesa.Ativo = true;
            _validador.ValidaInclusao(mesa);
            mesa = this._unitOfWork.RepositorioMesa.Incluir(mesa);
            return mesa;
        }

        /// <summary>
        /// Método responsável por alterar a mesa
        /// </summary>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        public Mesa Altera(Mesa mesaAlterada)
        {
            var mesa = this._unitOfWork.RepositorioMesa.SelecionarPorId(mesaAlterada.Id);
            if (mesa == null)
                throw new Exception(_mensagens.GetMensagem("MesaNaoCadastrado"));
            mesa.Descricao = mesaAlterada.Descricao;
            mesa.Ativo = mesaAlterada.Ativo;
            _validador.ValidaAlteracao(mesa);
            return this._unitOfWork.RepositorioMesa.Alterar(mesa);
        }

        /// <summary>
        /// Método responsável por listar todos as mesas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mesa> ListarTodas()
        {
            var mesas = this._unitOfWork.RepositorioMesa.ListarTodasMesas();
            return mesas;
        }

        /// <summary>
        /// Método responsável por listar todos as mesas ativas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mesa> ListarTodasAtivas()
        {
            var mesas = this._unitOfWork.RepositorioMesa.ListarTodasMesasAtivas();
            return mesas;
        }

        /// <summary>
        /// Método responsável por selecionar a mesa pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Mesa SelecionaPorId(int id)
        {
            var mesa = this._unitOfWork.RepositorioMesa.SelecionarPorId(id);
            if (mesa == null)
                throw new Exception(_mensagens.GetMensagem("MesaNaoCadastrado"));
            return mesa;
        }


        /// <summary>
        /// Método responsável por selecionar a mesa pela Descricao
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public Mesa SelecionaPorDescricao(string descricao)
        {
            var mesa = this._unitOfWork.RepositorioMesa.SelecionaPorDescricao(descricao);
            if (mesa == null)
                throw new Exception(_mensagens.GetMensagem("MesaNaoCadastrado"));
            return mesa;
        }


        /// <summary>
        /// Método responsável por excluir um grupo de produto
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var mesa = this._unitOfWork.RepositorioMesa.SelecionarPorId(id);
            if (mesa == null)
                return;
            this._unitOfWork.RepositorioMesa.Excluir(mesa.Id);
        }

        /// <summary>
        /// Método responsável por desfazer a exclusão da mesa
        /// </summary>
        /// <param name="mesa"></param>
        public void Desfazer(int id)
        {
            var _mesa = this._unitOfWork.RepositorioMesa.SelecionarPorId(id);
            if (_mesa == null)
                throw new Exception(_mensagens.GetMensagem("MesaNaoCadastrado"));
            _unitOfWork.RepositorioMesa.DesfazExclusao(id);
        }

        /// <summary>
        /// Método responsável por remover do banco 
        /// </summary>
        public void RemoverBanco()
        {
            var mesasIds = _unitOfWork.RepositorioMesa.ListarTodosIdsMesaExcluido();

            foreach (var mesaId in mesasIds)
                _unitOfWork.RepositorioMesa.RemoverBanco(mesaId);
        }

    }
}
