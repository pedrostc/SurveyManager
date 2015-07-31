using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyManager.Domain.Model.Contract.Base;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.Repository
{
    public class BlocosRepositorio : Repositorio<Bloco>
    {
        public IQueryable<Curso> ListarCursos()
        {
            return ListarTodos().OfType<Curso>();
        }
    }
}
