using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.Model.Contract.Base
{
    interface IRepositorio<TEntity> where TEntity : class
    {
        IQueryable<TEntity> ListarTodos();
        IQueryable<TEntity> Listar(Func<TEntity, bool> predicate);
        IQueryable<Aluno> ListarAlunos();
        IQueryable<Administrador> ListarAdms();
        TEntity Buscar(params object[] key);
        void Atualizar(TEntity obj);
        void SalvarTodos();
        void Adicionar(TEntity obj);
        void Excluir(Func<TEntity, bool> predicate);
    }
}
