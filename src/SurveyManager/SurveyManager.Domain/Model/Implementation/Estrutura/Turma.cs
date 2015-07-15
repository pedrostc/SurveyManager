using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyManager.Domain.Model.Implementation
{
    public class  Turma : EstruturaBase
    {
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public List<Aluno> Alunos { get; set; }

        public Turma()
        {
            Alunos = new List<Aluno>();
        }
    }
}
