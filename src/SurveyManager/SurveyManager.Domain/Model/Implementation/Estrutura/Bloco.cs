using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Domain.Model.Implementation
{
    public class Bloco : EstruturaBase
    {
        public List<Modulo> Modulos { get; set; }
        
        public virtual Curso Curso { get; set; }

        public Bloco()
        {
            Modulos = new List<Modulo>();
        }
    }
}
