using Garcom.Aplicacao.Interfaces;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Implementacao
{
    public class AppIngrediente : AppBase<IngredienteDTO>, IAppIngrediente
    {
        private ServicoIngrediente _servico;

        public AppIngrediente(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
            _servico = new ServicoIngrediente(unitOfWork);
        }

        public override IngredienteDTO Alterar(IngredienteDTO dto)
        {
            _unitOfWork.Transacao();
            var ingrediente = _imap.Map<Ingrediente>(dto);
            ingrediente = _servico.Alterar(ingrediente);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();

            return _imap.Map<IngredienteDTO>(ingrediente);
        }

        public override IngredienteDTO Incluir(IngredienteDTO dto)
        {
            _unitOfWork.Transacao();
            var ingrediente = _imap.Map<Ingrediente>(dto);
            ingrediente = _servico.Incluir(ingrediente);
            _unitOfWork.Salvar();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.InclusaoRegistro(dto.UsuarioLogado, "Ingrediente", ingrediente.Id);
            _unitOfWork.SalvarECommit();

            return _imap.Map<IngredienteDTO>(ingrediente);
        }

        public override ICollection<IngredienteDTO> ListarTodos()
        {
            var ingredientes = _servico.ListarTodos();
            return _imap.Map<IEnumerable<Ingrediente>, ICollection<IngredienteDTO>>(ingredientes);
        }

        public override IngredienteDTO SelecionarPorId(int id)
        {
            var ingrediente = _servico.SelecionaPorId(id);
            return _imap.Map<IngredienteDTO>(ingrediente);
        }

        public ICollection<ProdutoDTO> ValidaExclusao(int id)
        {
            var produtos = _servico.ValidaExclusao(id);
            var produtosDTO = _imap.Map<ICollection<Produto>, ICollection<ProdutoDTO>>(produtos);
            return produtosDTO;
        }

        public void Excluir(IngredienteDTO dto)
        {
            _unitOfWork.Transacao();
            var alteracoes = _servico.Excluir(dto.Id);
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
        }

        public void Desfazer(IngredienteDTO dto)
        {
            _unitOfWork.Transacao();
            _servico.Desfazer(dto.Id);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
        }

        public override void Dispose()
        {
            if (_servico != null)
                _servico.Dispose();
        }
    }
}
