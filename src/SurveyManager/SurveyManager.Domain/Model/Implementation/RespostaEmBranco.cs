using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Domain.Model.Implementation
{
    public class RespostaEmBranco : Resposta
    {
        public RespostaEmBranco()
        {
            EmBranco = true;
            Valor = null;
        }
    }
}
