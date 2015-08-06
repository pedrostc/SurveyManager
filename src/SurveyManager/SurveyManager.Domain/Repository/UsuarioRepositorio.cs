using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SurveyManager.Domain.Model.Contract.Base;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.Repository
{
    public class UsuarioRepositorio : Repositorio<Usuario>
    {
        public IQueryable<Aluno> ListarAlunos()
        {
            return ListarTodos().OfType<Aluno>();
        }

        public IQueryable<Administrador> ListarAdms()
        {
            return ListarTodos().OfType<Administrador>();
        }
    }
}
