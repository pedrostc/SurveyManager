using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SurveyManager.Domain.Model.Implementation
{
    public class  Turma : EstruturaBase
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime Inicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime Fim { get; set; }

        public List<Aluno> Alunos { get; private set; }
        public Avaliacao Avaliacao { get; private set; }

        public virtual Modulo Modulo { get; set; }

        public Turma()
        {
            Alunos = new List<Aluno>();
        }

        public void AdicionarAvaliacao(Avaliacao avaliacao)
        {
            Avaliacao = avaliacao;
        }

        public void AdicionarAluno(Aluno aluno)
        {
            Alunos.Add(aluno);
        }
    }
}
