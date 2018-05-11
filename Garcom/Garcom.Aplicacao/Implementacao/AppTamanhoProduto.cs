using Garcom.Aplicacao.Interfaces;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Implementacao
{
    public class AppTamanhoProduto : AppBase<TamanhoProdutoDTO>, IAppTamanhoProduto
    {
        private ServicoTamanhoProduto _servico;
        public AppTamanhoProduto(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
            _servico = new ServicoTamanhoProduto(unitOfWork);
        }

        public override TamanhoProdutoDTO Alterar(TamanhoProdutoDTO dto)
        {
            var tamanhoProduto = _imap.Map<TamanhoProduto>(dto);
            _unitOfWork.Transacao();
            tamanhoProduto = _servico.Alterar(tamanhoProduto);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
            return dto;
        }

        public override TamanhoProdutoDTO Incluir(TamanhoProdutoDTO dto)
        {
            var tamanhoProduto = _imap.Map<TamanhoProduto>(dto);
            _unitOfWork.Transacao();
            tamanhoProduto = _servico.Incluir(tamanhoProduto);
            _unitOfWork.Salvar();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.InclusaoRegistro(dto.UsuarioLogado, "Tamanho_Produto", tamanhoProduto.Id);
            _unitOfWork.SalvarECommit();
            return _imap.Map<TamanhoProdutoDTO>(tamanhoProduto);
        }

        public void Desfazer(TamanhoProdutoDTO tamanhoDTO)
        {
            _unitOfWork.Transacao();
            _servico.Desfazer(tamanhoDTO.Id);
            _servico.DesfazerGrupoProdutoTamanhoProduto(tamanhoDTO.Id);
            var alteracoes = _unitOfWork.RetornaModificacoes(); 
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(tamanhoDTO.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
        }

        public void Excluir(TamanhoProdutoDTO tamanhoProdutoDTO)
        {
            _unitOfWork.Transacao();
            _servico.Excluir(tamanhoProdutoDTO.Id);
            _servico.ExcluirGrupoProdutoTamanhoProduto(tamanhoProdutoDTO.Id);
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.ExclusaoRegistro(tamanhoProdutoDTO.UsuarioLogado, "Tamanho_Produto", tamanhoProdutoDTO.Id);
            _unitOfWork.SalvarECommit();
        }

        public override ICollection<TamanhoProdutoDTO> ListarTodos()
        {
            var tamanhos = _servico.ListarTodos();
            var tamanhosDTO = _imap.Map<ICollection<TamanhoProduto>, ICollection<TamanhoProdutoDTO>>(tamanhos);
            return tamanhosDTO;
        }

        public override TamanhoProdutoDTO SelecionarPorId(int id)
        {
            var tamanho = _servico.SelecionarPorId(id);

            if (tamanho == null)
                return null;

            var tamanhoDTO = _imap.Map<TamanhoProdutoDTO>(tamanho);
            tamanhoDTO.PossuiProduto = _servico.TamanhoPossuiProduto(id);
            return tamanhoDTO;
        }

        public ICollection<string> NomeProdutosVinculos(int grupoProdutoId, int tamanhoId)
        {
            return _servico.NomeProdutosVinculos(grupoProdutoId, tamanhoId);
        }

        public ICollection<string> ValidarExclusao(int id)
        {
            return _servico.ValidarExclusao(id);
        }

        public override void Dispose()
        {
            if (_servico != null)
                _servico.Dispose();
        }
    }
}
