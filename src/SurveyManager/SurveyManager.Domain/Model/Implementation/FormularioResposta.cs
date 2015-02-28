using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManager.Domain.Model.Implementation
{
    public class FormularioResposta
    {
        public Aluno Respondente { get; set; }
        public Dictionary<Questao, Resposta> Respostas { get; private set; }
        public DateTime RespondidoEm { get; set; }

        public FormularioResposta(List<Questao> questoes)
        {
            Respostas = new Dictionary<Questao, Resposta>();
            questoes.ForEach(q => { Respostas.Add(q, null); });
        }

        public void AdicionarResposta(Questao questao, int resposta)
        {
            ValidaQuestaoResposta(questao, resposta);

            Respostas[questao] = new RespostaValida(resposta);
        }
        public void AdicionarRespostaEmBranco(Questao questao)
        {
            ValidaQuestao(questao);

            Respostas[questao] = new RespostaEmBranco();
        }

        private void ValidaQuestao(Questao questao)
        {
            if(!Respostas.ContainsKey(questao))
                throw new QuestaoNaoFazParteDaAvaliacaoException();
        }
        private void ValidaQuestaoResposta(Questao questao, int resposta)
        {
            ValidaQuestao(questao);
            questao.ValidaResposta(resposta);
        }
    }

    public class QuestaoNaoFazParteDaAvaliacaoException : Exception
    {
        public override string Message
        {
            get { return "A questão informada não faz parte da avaliação."; }
        }
    }
}
