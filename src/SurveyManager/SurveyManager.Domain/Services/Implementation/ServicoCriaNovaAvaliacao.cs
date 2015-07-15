using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.Services.Implementation
{
    public static class ServicoCriaNovaAvaliacao
    {
        public static Avaliacao Para(Turma turma)
        {
            Avaliacao avaliacao = new Avaliacao();
            turma.AdicionarAvaliacao(avaliacao);

            return avaliacao;
        }
    }
}
