using System;

namespace SurveyManager.Domain.Model.Implementation
{
    public abstract class Resposta : ModelBase
    {
        public Questao Questao { get; set; }
        public int Valor { get; private set; }
        public virtual int ValorMinimo { get; set; }
        public virtual int ValorMaximo { get; set; }

        public void DefinirResposta(int valor)
        {
            ValidarValor(valor);
            Valor = valor;
        }

        private void ValidarValor(int valor)
        {
            if(valor < ValorMinimo) throw new RespostaMenorQueValorMinimoException();
            if(valor > ValorMaximo) throw new RespostaMaiorQueValorMaximoException();
        }
    }

    public class RespostaMenorQueValorMinimoException : Exception
    {
        public override string Message
        {
            get { return "A resposta informada é menor que o valor mínimo permitido."; }
        }
    }
    public class RespostaMaiorQueValorMaximoException : Exception
    {
        public override string Message
        {
            get { return "A resposta informada é maior que o valor máximo permitido."; }
        }
    }
}