using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Domain.Model
{
    public abstract class Usuario
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
