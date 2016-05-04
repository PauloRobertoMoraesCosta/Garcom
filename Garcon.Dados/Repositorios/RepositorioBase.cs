using Garcom.Dominio.Interfaces.Repositorios;
using System;
using Garcom.Dados.Contexto;
using System.Linq;
using System.Data.Entity;

namespace Garcom.Dados.Repositorios
{
    public class RepositorioBase<TEntity> : IDisposable, IRepositorioBase<TEntity> where TEntity : class
    {
        protected Contexto.Context db = new Contexto.Context();
        public void Adiciona(TEntity objeto)
        {
            db.Set<TEntity>().Add(objeto);
            db.SaveChanges();
        }

        public TEntity RetornaPorId(int Id)
        {
            return db.Set<TEntity>().Find(Id);
        }


        public System.Collections.Generic.IEnumerable<TEntity> RetornaTodos()
        {
            return db.Set<TEntity>().ToList();
        }

        public System.Collections.Generic.IEnumerable<TEntity> RetornaTodosAsNoTracking()
        {
            return db.Set<TEntity>().AsNoTracking();
        }

        public void Alterar(TEntity objeto)
        {
            db.Entry(objeto).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remover(TEntity objeto)
        {
            db.Set<TEntity>().Remove(objeto);
            db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
