using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Presentation.MVC4.Models
{
    public class TurmaViewModel
    {
        public Guid ModuloId { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }
}