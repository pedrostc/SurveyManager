using System;
using System.Collections.Generic;

namespace SurveyManager.Domain.Model
{
    public class Avaliacao
    {
        public string Codigo { get; set; }
        public string Proposito { get; set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTermino { get; private set; }
        public Aluno Respondente { get; set; }
        public List<Questao> Questoes { get; private set; }
        public Dictionary<Questao, int> Respostas { get; private set; }

        public Avaliacao()
        {
            Questoes = new List<Questao>();
            Respostas = new Dictionary<Questao, int>();
        }

        public void AdicionarQuestao(Questao questao)
        {
            Questoes.Add(questao);
        }

        public void DefinirDataInicio(DateTime dataInicio)
        {
            ValidarDataInicioContraDataTermino(dataInicio);

            DataInicio = dataInicio;
        }
        private void ValidarDataInicioContraDataTermino(DateTime dataInicio)
        {
            if (DataTermino == DateTime.MinValue) return;

            ValidarDatasIguais(dataInicio, DataTermino);

            if (dataInicio.CompareTo(DataTermino) > 0) throw new DataInicioPosteriorADataTerminoException();
        }

        public void DefinirDataTermino(DateTime dataTermino)
        {
            ValidarDataTerminoContraDataInicio(dataTermino);

            DataTermino = dataTermino;
        }
        private void ValidarDataTerminoContraDataInicio(DateTime dataTermino)
        {
            if (DataInicio == DateTime.MinValue) return;

            ValidarDatasIguais(DataInicio, dataTermino);

            if (dataTermino.CompareTo(DataInicio) < 0) throw new DataTerminoAnteriorADataInicioException();
        }
        private void ValidarDatasIguais(DateTime dataInicio, DateTime dataTermino)
        {
            if(dataInicio.CompareTo(dataTermino) == 0) throw new DatasIguaisException();
        }

        public void AdicionarResposta(Questao questao, int resposta)
        {
            Respostas.Add(questao, resposta);
        }
    }

    #region Exceptions
    public class DataInicioPosteriorADataTerminoException : Exception
    {
        public override string Message
        {
            get { return "A data de inicio deve ser anterior a data de termino"; }
        }
    }
    public class DataTerminoAnteriorADataInicioException : Exception
    {
        public override string Message
        {
            get { return "A data de termino deve ser posterior a data de inicio"; }
        }
    }
    public class DatasIguaisException : Exception
    {
        public override string Message
        {
            get { return "As datas de inicio e termino não podem ser iguais."; }
        }
    }

    public class QuestaoNaoPertenceAAvaliacaoException : Exception
    {
        public override string Message
        {
            get { return "A pergunta que esta sendo respondida não pertence a avaliação."; }
        }
    }

    #endregion
}
