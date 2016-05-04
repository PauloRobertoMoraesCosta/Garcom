using Garcom.Dominio.Interfaces.Repositorios;
using Garcom.Dominio.Interfaces.Servicos;
using System;

namespace Garcom.Dominio.Servicos
{
    public class ServicoBase<TEntity> : IDisposable, IServicoBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity> _repositorio;

        public ServicoBase(IRepositorioBase<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }


        public void Adiciona(TEntity objeto)
        {
            _repositorio.Adiciona(objeto);
        }

        public TEntity RetornaPorId(int Id)
        {
            return _repositorio.RetornaPorId(Id);
        }

        public System.Collections.Generic.IEnumerable<TEntity> RetornaTodos()
        {
            return _repositorio.RetornaTodos();
        }

        public System.Collections.Generic.IEnumerable<TEntity> RetornaTodosAsNoTracking()
        {
            return _repositorio.RetornaTodosAsNoTracking();
        }

        public void Alterar(TEntity objeto)
        {
            _repositorio.Alterar(objeto);
        }

        public void Remover(TEntity objeto)
        {
            _repositorio.Remover(objeto);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}
