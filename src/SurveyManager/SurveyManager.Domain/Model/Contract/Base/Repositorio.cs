using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SurveyManager.Domain.DAO.Shared;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.Model.Contract.Base
{
    public abstract class Repositorio<TEntity> : IDisposable,
       IRepositorio<TEntity> where TEntity : class
    {
        Contexto contexto = new Contexto();

        public IQueryable<TEntity> ListarTodos()
        {
            return contexto.Set<TEntity>();
        }

        public IQueryable<TEntity> Listar(Func<TEntity, bool> predicate)
        {
            return ListarTodos().Where(predicate).AsQueryable();
        }

        public IQueryable<Aluno> ListarAlunos()
        {
            return ListarTodos().OfType<Aluno>();
        }

        public IQueryable<Administrador> ListarAdms()
        {
            return ListarTodos().OfType<Administrador>();
        }

        public TEntity Buscar(params object[] key)
        {
            return contexto.Set<TEntity>().Find(key);
        }

        public void Atualizar(TEntity obj)
        {
            contexto.Entry(obj).State = EntityState.Modified;
        }

        public void SalvarTodos()
        {
            contexto.SaveChanges();
        }

        public void Adicionar(TEntity obj)
        {
            contexto.Set<TEntity>().Add(obj);
        }

        public void Excluir(Func<TEntity, bool> predicate)
        {
            contexto.Set<TEntity>()
                .Where(predicate).ToList()
                .ForEach(del => contexto.Set<TEntity>().Remove(del));
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
