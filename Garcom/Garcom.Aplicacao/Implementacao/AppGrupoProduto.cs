using Garcom.Aplicacao.Interfaces;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Implementacao
{
    public class AppGrupoProduto : AppBase<GrupoProdutoDTO>, IAppGrupoProduto
    {
        private ServicoGrupoProduto _servico;

        public AppGrupoProduto(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _servico = new ServicoGrupoProduto(unitOfWork);
        }

        public override GrupoProdutoDTO Alterar(GrupoProdutoDTO dto)
        {
            var grupoProduto = _imap.Map<GrupoProduto>(dto);
            _unitOfWork.Transacao();
            grupoProduto.GruposProdutoTamanhosProduto = MontarListaGrupoProdutoTamanhoProduto(dto.Tamanhos, dto.Id);
            grupoProduto = _servico.Altera(grupoProduto);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
            return dto;
        }

        public override GrupoProdutoDTO Incluir(GrupoProdutoDTO dto)
        {
            if (dto.DataCadastro == null || dto.DataCadastro == DateTime.MinValue) dto.DataCadastro = DateTime.Now;
            var grupoProduto = _imap.Map<GrupoProduto>(dto);
            grupoProduto.GruposProdutoTamanhosProduto = MontarListaGrupoProdutoTamanhoProduto(dto.Tamanhos, dto.Id);
            _unitOfWork.Transacao();
            grupoProduto = _servico.Incluir(grupoProduto);
            _unitOfWork.Salvar();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.InclusaoRegistro(dto.UsuarioLogado, "Grupo_Produto", grupoProduto.Id);
            _unitOfWork.SalvarECommit();
            return _imap.Map<GrupoProdutoDTO>(grupoProduto);
        }

        public override ICollection<GrupoProdutoDTO> ListarTodos()
        {
            var gruposProdutos = _servico.ListarTodos();
            var gruposProdutosDTO = _imap.Map<IEnumerable<GrupoProduto>, ICollection<GrupoProdutoDTO>>(gruposProdutos);
            return gruposProdutosDTO;
        }

        public override GrupoProdutoDTO SelecionarPorId(int id)
        {
            var grupoProduto = _servico.SelecionaPorId(id);
            var grupoProdutoDTO = _imap.Map<GrupoProdutoDTO>(grupoProduto);
            grupoProdutoDTO.Tamanhos = MontarListarTamanhoProdutoDTOComOrdemPorGrupoProdutoId(grupoProdutoDTO.Id);
            return grupoProdutoDTO;
        }

        public void Excluir(GrupoProdutoDTO grupoProdutoDTO)
        {
            _unitOfWork.Transacao();
            _servico.ExcluirGrupoProdutoTamanhoProduto(grupoProdutoDTO.Id);
            var alteracoes = _servico.Excluir(grupoProdutoDTO.Id);
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(grupoProdutoDTO.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
        }

        public void Desfazer(GrupoProdutoDTO grupoProdutoDTO)
        {
            _unitOfWork.Transacao();
            _servico.Desfazer(grupoProdutoDTO.Id);
            _servico.DesfazerGrupoProdutoTamanhoProduto(grupoProdutoDTO.Id);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(grupoProdutoDTO.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
        }

        public ICollection<GrupoProdutoDTO> ListarTodosComTamanhos()
        {
            var gruposProdutos = _imap.Map<IEnumerable<GrupoProduto>, ICollection<GrupoProdutoDTO>>(_servico.ListarTodos());
            foreach (var gp in gruposProdutos)
                gp.Tamanhos = MontarListarTamanhoProdutoDTOComOrdemPorGrupoProdutoId(gp.Id);

            return gruposProdutos;
        }

        public ICollection<string> ValidaExclusao(int id)
        {
            return _servico.ValidaExclusao(id);
        }

        /// <summary>
        /// Método responsável por montar a retornar a lista de tamanhos vinculado a um grupo de produto com a ordem informado
        /// </summary>
        /// <param name="grupoProdutoId"></param>
        /// <returns></returns>
        private ICollection<TamanhoProdutoDTO> MontarListarTamanhoProdutoDTOComOrdemPorGrupoProdutoId(int grupoProdutoId)
        {
            var tamanhosProdutosDTO = new List<TamanhoProdutoDTO>();
            var gpTamanhosProdutos = _unitOfWork.RepositorioGrupoProduto.ListarGrupoProdutoTamanhoProdutoPorGrupoProdutoId(grupoProdutoId);
            foreach (var item in gpTamanhosProdutos)
            {
                var tamanhoDTO = _imap.Map<TamanhoProduto, TamanhoProdutoDTO>(item.TamanhoProduto);
                tamanhoDTO.Utilizado = _unitOfWork.RepositorioTamanhoProduto.PossuiProdutoVinculadoPorGrupoId(grupoProdutoId, tamanhoDTO.Id);
                tamanhoDTO.Ordem = item.Ordem;
                tamanhosProdutosDTO.Add(tamanhoDTO);
            }

            return tamanhosProdutosDTO;
        }

        private ICollection<GrupoProdutoTamanhoProduto> MontarListaGrupoProdutoTamanhoProduto(ICollection<TamanhoProdutoDTO> tamanhos, int grupoProdutoId)
        {
            if (tamanhos == null || tamanhos.Count == 0)
                return null;

            var gruposProdutoTamanhosProduto = new List<GrupoProdutoTamanhoProduto>();
            foreach (var tamanho in tamanhos)
                gruposProdutoTamanhosProduto.Add(new GrupoProdutoTamanhoProduto(tamanho.Ordem.Value, grupoProdutoId, tamanho.Id));
            return gruposProdutoTamanhosProduto;
        }

        public override void Dispose()
        {
            if (_servico != null)
                _servico.Dispose();
        }
    }
}
