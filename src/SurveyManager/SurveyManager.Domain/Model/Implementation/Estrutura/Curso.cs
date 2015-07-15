using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Domain.Model.Implementation
{
    public class Curso : EstruturaBase
    {
        public List<Bloco> Blocos { get; set; }

        public Curso()
        {
            Blocos = new List<Bloco>();
        }
    }
}
