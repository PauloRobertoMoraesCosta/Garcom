using Garcom.Aplicacao.Interfaces;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Implementacao
{
    public class AppProduto : AppBase<ProdutoDTO>, IAppProduto
    {
        private ServicoProduto _servico;

        public AppProduto(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _servico = new ServicoProduto(_unitOfWork);
        }

        public override ProdutoDTO Alterar(ProdutoDTO dto)
        {
            var produto = _imap.Map<Produto>(dto);
            produto.NomeImagem = _unitOfWork.RepositorioProduto.NomeImagem(produto.Id);
            _servico.AjustarNomeImagem(produto, dto);
            _unitOfWork.Transacao();
            _servico.Alterar(produto);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork, dto.UsuarioLogado, "Produto"))
            {
                /*serviceAuditoria.GravarListaInclusao(listaTamanhoProdutoValorInclusao.Cast<dynamic>().ToList());
                serviceAuditoria.GravarListaInclusao(listaProdutoIngredienteInclusao.Cast<dynamic>().ToList());
                serviceAuditoria.GravarListaInclusao(listaProdutoIngredienteTamanhoInclusao.Cast<dynamic>().ToList());*/
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            }
            _unitOfWork.SalvarECommit();
            return dto;
        }

        public void Desfazer(ProdutoDTO produtoDTO)
        {
            _unitOfWork.Transacao();
            _servico.Desfazer(produtoDTO.Id);
            using (var servicoAuditoria = new ServicoAuditoria(this._unitOfWork))
            {
                var alteracoes = new List<ChangeLog>();
                var propertiesChangeLog = new List<PropertyChangeLog>();
                propertiesChangeLog.Add(new PropertyChangeLog
                {
                    DateChanged = DateTime.Now,
                    PropertyName = "Excluido",
                    OldValue = "true",
                    NewValue = "false"
                });
                alteracoes.Add(new ChangeLog
                {
                    Entity = "Produto",
                    PrimaryKeyValue = produtoDTO.Id.ToString(),
                    State = Acao.ALTERACAO,
                    PropertiesChangeLog = propertiesChangeLog
                });
                servicoAuditoria.RegistraAuditoria(produtoDTO.UsuarioLogado, alteracoes);
            }
            _unitOfWork.SalvarECommit();
        }

        public void Inativar(ProdutoDTO produtoDTO)
        {
            _unitOfWork.Transacao();
            _servico.Inativar(produtoDTO.Id);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var servicoAuditoria = new ServicoAuditoria(this._unitOfWork))
                servicoAuditoria.RegistraAuditoria(produtoDTO.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
        }

        public override ProdutoDTO Incluir(ProdutoDTO dto)
        {
            var produto = _imap.Map<Produto>(dto);
            produto.NomeImagem = _servico.InsereImagem(dto.Imagem);
            _unitOfWork.Transacao();
            produto = _servico.Incluir(produto);
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.InclusaoRegistro(dto.UsuarioLogado, "Produto", produto.Id);
            _unitOfWork.SalvarECommit();
            return _imap.Map<ProdutoDTO>(produto);
        }

        public override ICollection<ProdutoDTO> ListarTodos()
        {
            return _servico.Listar();
        }

        public void RemoverProduto(ProdutoDTO produtoDTO)
        {
            _unitOfWork.Transacao();
            _servico.RemoverProduto(produtoDTO.Id);
            using (var servicoAuditoria = new ServicoAuditoria(this._unitOfWork))
            {
                var alteracoes = new List<ChangeLog>();
                var propertiesChangeLog = new List<PropertyChangeLog>();
                propertiesChangeLog.Add(new PropertyChangeLog
                {
                    DateChanged = DateTime.Now,
                    PropertyName = "Excluido",
                    OldValue = "false",
                    NewValue = "true"
                });
                alteracoes.Add(new ChangeLog
                {
                    Entity = "Produto",
                    PrimaryKeyValue = produtoDTO.Id.ToString(),
                    State = Acao.ALTERACAO,
                    PropertiesChangeLog = propertiesChangeLog
                });
                servicoAuditoria.RegistraAuditoria(produtoDTO.UsuarioLogado, alteracoes);
            }
            _unitOfWork.SalvarECommit();
        }

        public ProdutoDTO SelecionaProdutoPorCodigoRapido(string codigoRapido)
        {
            var produto = _servico.SelecionaProdutoPorCodigoRapido(codigoRapido);
            return _imap.Map<ProdutoDTO>(produto);
        }

        public override ProdutoDTO SelecionarPorId(int id)
        {
            var produto = _servico.SelecionaProdutoPorId(id);
            if (produto == null)
                return null;

            using (var servicoGrupoProduto = new ServicoGrupoProduto(_unitOfWork))
            {
                if (produto.GrupoProdutoId.HasValue && servicoGrupoProduto.Excluido(produto.GrupoProdutoId.Value))
                    produto.GrupoProdutoId = null;
            }

            var produtoDto = _imap.Map<ProdutoDTO>(produto);
            return produtoDto;
        }

        public bool JaUtilizado(int id)
        {
            return _servico.JaUtilizado(id);
        }

        public override void Dispose()
        {
            if (_servico != null)
                _servico.Dispose();
        }
    }
}
