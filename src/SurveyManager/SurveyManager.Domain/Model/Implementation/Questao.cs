using System;
using System.Collections.Generic;

namespace SurveyManager.Domain.Model.Implementation
{
    public class Questao : ModelBase
    {
        public string Texto { get; set; }
        public Escala Escala { get; set; }

        public void ValidaResposta(int resposta)
        {
            try
            {
                Escala.ValidaValorPertecenteAEscala(resposta);
            }
            catch (ValorNaoPertenceAEscalaException)
            {
                throw new RespostaInvalidaException();
            }
        }
    }

    public class RespostaInvalidaException : Exception
    {
        public override string Message
        {
            get { return "A resposta informada não é válida."; }
        }
    }
}
