using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using Garcom.Infra.Repositorio;
using System;
using Garcom.Dominio.Entidade.Models.Auditoria;
using System.Collections.Generic;

namespace Garcom.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ContextoEscrita _contextoEscrita;
        private ContextoLeitura _contextoLeitura;
        private RepositorioProduto _repositorioProduto;
        private RepositorioAuditoria _repositorioAuditoria;
        private RepositorioGrupoProduto _repositorioGrupoProduto;
        private RepositorioIngrediente _repositorioIngrediente;
        private RepositorioMesa _repositorioMesa;
        private RepositorioTamanhoProduto _repositorioTamanhoProduto;
        private RepositorioProdutoTamanhoValor _repositorioTamanhoProdutoValor;
        private RepositorioUsuario _repositorioUsuario;
        private RepositorioProdutoIngrediente _repositorioProdutoIngrediente;
        private RepositorioProdutosIngredienteTamanhoValor _repositorioProdutoIngredienteTamanho;
        private RepositorioExcecao _repositorioExcecao;

        public UnitOfWork()
        {
            this._contextoEscrita = new ContextoEscrita();
            this._contextoLeitura = new ContextoLeitura();
            try
            {
                this._contextoLeitura.MySqlConnection.Open();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível conectar no banco de dados.");
            }
        }

        public RepositorioAuditoria RepositorioAuditoria
        {
            get
            {
                if (this._repositorioAuditoria == null)
                    _repositorioAuditoria = new RepositorioAuditoria(this._contextoEscrita, this._contextoLeitura);
                return _repositorioAuditoria;
            }
        }

        public RepositorioProduto RepositorioProduto
        {
            get
            {
                if (_repositorioProduto == null)
                    _repositorioProduto = new RepositorioProduto(this._contextoEscrita, this._contextoLeitura);
   
                return _repositorioProduto;
            }
        }

        public RepositorioGrupoProduto RepositorioGrupoProduto
        {
            get
            {
                if (_repositorioGrupoProduto == null)
                    _repositorioGrupoProduto = new RepositorioGrupoProduto(this._contextoEscrita, this._contextoLeitura);

                return _repositorioGrupoProduto;
            }
        }

        public RepositorioUsuario RepositorioUsuario
        {
            get
            {
                if (_repositorioUsuario == null)
                    _repositorioUsuario = new RepositorioUsuario(this._contextoEscrita, this._contextoLeitura);

                return _repositorioUsuario;
            }
        }

        public RepositorioIngrediente RepositorioIngrediente
        {
            get
            {
                if (_repositorioIngrediente == null)
                    _repositorioIngrediente = new RepositorioIngrediente(this._contextoEscrita, this._contextoLeitura);

                return _repositorioIngrediente;
            }
        }

        public RepositorioMesa RepositorioMesa
        {
            get
            {
                if (_repositorioMesa == null)
                    _repositorioMesa = new RepositorioMesa(this._contextoEscrita, this._contextoLeitura);

                return _repositorioMesa;
            }
        }

        public RepositorioProdutoIngrediente RepositorioProdutoIngrediente
        {
            get
            {
                if (_repositorioProdutoIngrediente == null)
                    _repositorioProdutoIngrediente = new RepositorioProdutoIngrediente(this._contextoEscrita, this._contextoLeitura);

                return _repositorioProdutoIngrediente;
            }
        }

        public RepositorioTamanhoProduto RepositorioTamanhoProduto
        {
            get
            {
                if (_repositorioTamanhoProduto == null)
                    _repositorioTamanhoProduto = new RepositorioTamanhoProduto(this._contextoEscrita, this._contextoLeitura);

                return _repositorioTamanhoProduto;
            }
        }

        public RepositorioProdutoTamanhoValor RepositorioTamanhoProdutoValor
        {
            get
            {
                if (_repositorioTamanhoProdutoValor == null)
                    _repositorioTamanhoProdutoValor = new RepositorioProdutoTamanhoValor(this._contextoEscrita, this._contextoLeitura);

                return _repositorioTamanhoProdutoValor;
            }
        }

        public RepositorioProdutosIngredienteTamanhoValor RepositorioProdutoIngredienteTamanho
        {
            get
            {
                if (_repositorioProdutoIngredienteTamanho == null)
                    _repositorioProdutoIngredienteTamanho = new RepositorioProdutosIngredienteTamanhoValor(this._contextoEscrita, this._contextoLeitura);
                
                return _repositorioProdutoIngredienteTamanho;
            }
        }

        public RepositorioExcecao RepositorioExcecao
        {
            get
            {
                if (_repositorioExcecao == null)
                    _repositorioExcecao = new RepositorioExcecao(this._contextoEscrita, this._contextoLeitura);

                return _repositorioExcecao;
            }
        }

        public void Commit()
        {
            _contextoEscrita.Commit();
        }

        public void Dispose()
        {
            _contextoEscrita.Database.Connection.Close();
            _contextoLeitura.MySqlConnection.Close();
        }

        public void Salvar()
        {
            _contextoEscrita.Save();
        }

        public void Transacao()
        {
            _contextoEscrita.StartTransaction();
        }

        public void SalvarECommit()
        {
            _contextoEscrita.SaveAndCommit();
        }

        public IEnumerable<ChangeLog> RetornaModificacoes()
        {
            return _contextoEscrita.RetornaModificacoes();
        }
    }
}
