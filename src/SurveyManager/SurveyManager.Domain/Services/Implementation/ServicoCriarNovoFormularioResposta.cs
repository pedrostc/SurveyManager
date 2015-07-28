using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.Services.Implementation
{
    public static class ServicoCriarNovoFormularioResposta
    {
        public static void Para(Avaliacao avaliacao)
        {
            FormularioResposta formResposta = new FormularioResposta(avaliacao.Questoes);
        }
    }
}
