using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Domain.Model.Implementation
{
    public class Modulo : EstruturaBase
    {
        public List<Turma> Turmas { get; set; }

        public virtual Bloco Bloco { get; set; }

        public Modulo()
        {
            Turmas = new List<Turma>();
        }
    }
}
