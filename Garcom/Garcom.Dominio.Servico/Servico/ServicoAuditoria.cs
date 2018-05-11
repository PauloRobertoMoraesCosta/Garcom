using Garcom.Core;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.Repositorio;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garcom.Dominio.Servico.Servico
{
    /// <summary>
    /// Serviço responsável por registrar auditoria
    /// </summary>
    public class ServicoAuditoria : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public string UsuarioLogado { get; set; }
        public string NomeTabela { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public ServicoAuditoria(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ServicoAuditoria(IUnitOfWork unitOfWork, string login, string tabela)
        {
            this._unitOfWork = unitOfWork;
            this.UsuarioLogado = login;
            this.NomeTabela = tabela;
        }

        /// <summary>
        /// Método responsável por inserir auditoria na inclusão de registro
        /// </summary>
        /// <param name="login"></param>
        /// <param name="tabela"></param>
        /// <param name="chave"></param>
        public void InclusaoRegistro(string login, string tabela, int chave)
        {
            var auditoria = new Auditoria
            {
                Usuario = login,
                Tabela = tabela,
                Chave = chave,
                Acao = Acao.INCLUSAO
            };

            this._unitOfWork.RepositorioAuditoria.Incluir(auditoria);
        }

        /// <summary>
        /// Método responsável por inserir auditoria na alteração de registro
        /// </summary>
        /// <param name="login"></param>
        /// <param name="alteracoes"></param>
        public void RegistraAuditoria(string login, IEnumerable<ChangeLog> alteracoes)
        {
            try
            {
                foreach (var tabela in alteracoes.GroupBy(p => new { p.EntityName, p.PrimaryKeyValue })
                                 .Select(p => new { p.Key.EntityName, p.Key.PrimaryKeyValue }))
                {
                    string valoresAntigo = string.Empty;
                    string novosValores = string.Empty;

                    foreach (var alteracao in alteracoes.Where(p => p.EntityName.Equals(tabela.EntityName) &&
                                                                    p.PrimaryKeyValue.Equals(tabela.PrimaryKeyValue)).ToList())
                    {

                        foreach (var propriedade in alteracao.PropertiesChangeLog)
                        {
                            valoresAntigo += string.IsNullOrEmpty(valoresAntigo) ? string.Format("{0}: {1}", propriedade.PropertyName, propriedade.OldValue)
                                                                                 : string.Format(" - {0}: {1}", propriedade.PropertyName, propriedade.OldValue);

                            novosValores += string.IsNullOrEmpty(novosValores) ? string.Format("{0}: {1}", propriedade.PropertyName, propriedade.NewValue)
                                                                                 : string.Format(" - {0}: {1}", propriedade.PropertyName, propriedade.NewValue);
                        }

                        var auditoria = new Auditoria
                        {
                            Tabela = alteracao.EntityName,
                            Acao = alteracao.State,
                            Usuario = login,
                            NovosValores = novosValores,
                            ValoresAntigos = valoresAntigo,
                            Chave = string.IsNullOrEmpty(tabela.PrimaryKeyValue) ? 0 : Convert.ToInt32(tabela.PrimaryKeyValue)
                        };

                        this._unitOfWork.RepositorioAuditoria.Incluir(auditoria);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao Registra Auditoria. Error: " + Funcoes.TratamentoMessageExcecao(ex));
            }
        }

        public void ExclusaoRegistro(string login, string tabela, int chave)
        {
            try
            {
                var auditoria = new Auditoria
                {
                    Usuario = login,
                    Tabela = tabela,
                    Chave = chave,
                    Acao = Acao.EXCLUSAO
                };

                this._unitOfWork.RepositorioAuditoria.Incluir(auditoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao Registra Auditoria. Error: " + Funcoes.TratamentoMessageExcecao(ex));
            }
        }

        public void GravarListaInclusao(List<dynamic> Inclusao)
        {
            Inclusao.ForEach(i => InclusaoRegistro(this.UsuarioLogado, this.NomeTabela, i.Id));
        }

        public void Dispose()
        {
        }
    }
}
