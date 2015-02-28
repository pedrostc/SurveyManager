using System;
using System.Collections.Generic;
using System.Linq;

namespace SurveyManager.Domain.Model.Implementation
{
    public class Escala : ModelBase
    {
        public Dictionary<int, string> Valores { get; private set; }

        public Escala()
        {
            Valores = new Dictionary<int, string>();
        }

        public void AdicionarNivel(int valor, string texto)
        {
            ValidaValorInsercao(valor);
            ValidaTextoInsercao(texto);

            Valores.Add(valor, texto);
        }

        public void ValidaValorPertecenteAEscala(int valor)
        {
            if(!Valores.ContainsKey(valor)) 
                throw new ValorNaoPertenceAEscalaException();
        }

        private void ValidaValorInsercao(int valor)
        {
            if (Valores.Count(e => e.Key == valor) != 0)
                throw new ValorJaExisteException();
        }
        private void ValidaTextoInsercao(string texto)
        {
            if (Valores.Count(e => e.Value == texto) != 0)
                throw new TextoJaExisteException();
        }
    }

    public class ValorJaExisteException : Exception
    {
        public override string Message
        {
            get { return "O valor informado já está cadastrado nessa escala."; }
        }
    }
    public class TextoJaExisteException : Exception
    {
        public override string Message
        {
            get { return "O texto informado já está cadastrado nessa escala."; }
        }
    }

    public class ValorNaoPertenceAEscalaException : Exception
    {
        public override string Message
        {
            get { return "O valor informado não é valor válido desta escala."; }
        }
    }
}
