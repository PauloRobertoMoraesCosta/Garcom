using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Garcom.Infra.Repositorio
{
    public abstract class RepositorioBase<T> : IDisposable where T : ClasseBase
    {
        protected readonly ContextoLeitura _dbLeitura;
        protected readonly ContextoEscrita _dbEscrita;

        public RepositorioBase(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
        {
            this._dbEscrita = dbEscrita;
            this._dbLeitura = dbLeitura;
        }

        /// <summary>
        /// Metodo incluir generico
        /// </summary>
        /// <param name="entidade">Entidade</param>
        /// <returns>Entidade</returns>
        public virtual T Incluir(T entidade)
        {
            entidade = _dbEscrita.Set<T>().Add(entidade);

            return entidade;
        }

        /// <summary>
        /// Metodo Alterar generico
        /// </summary>
        /// <param name="entidade">Entidade</param>
        /// <returns>Entidade</returns>
        public virtual T Alterar(T entidade)
        {
            _dbEscrita.Set<T>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;

            return entidade;
        }

        /// <summary>
        /// Método generico para selecionar um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T SelecionarPorId(int id)
        {
            return _dbEscrita.Set<T>().Find(id);
        }

        /// <summary>
        /// Metodo excluir generico
        /// </summary>
        /// <param name="id"></param>
        public virtual IEnumerable<ChangeLog> Excluir(int id)
        {
            var entidade = _dbEscrita.Set<T>().Find(id);
            entidade.Excluido = true;
            _dbEscrita.Set<T>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
            var alteracoes = _dbEscrita.RetornaModificacoes();

            return alteracoes;
        }

        /// <summary>
        /// Método que desfaz a exclusão de uma entidade
        /// </summary>
        /// <param name="id"></param>
        public virtual void DesfazExclusao(int id)
        {
            var entidade = _dbEscrita.Set<T>().Find(id);
            entidade.Excluido = false;
            _dbEscrita.Set<T>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
        }

        /// <summary>
        /// Método excluir do banco para ser rodado por rotinas do sistema
        /// </summary>
        /// <param name="id"></param>
        public virtual void RemoverBanco(int id)
        {
            _dbEscrita.Set<T>().Remove(_dbEscrita.Set<T>().Find(id));
        }

        public void Dispose()
        {
        }
    }
}
