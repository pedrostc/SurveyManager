using System;

namespace SurveyManager.Domain.Model.Implementation
{
    public abstract class Resposta : ModelBase
    {
        public bool EmBranco { get; protected set; }
        public int? Valor { get; protected set; }
        public Questao Questao { get; set; }

        public void CadastrarQuestao(Questao questao)
        {
            Questao = questao;
        }
    }
}