using System;
using System.Collections.Generic;

namespace SurveyManager.Domain.Model
{
    public class Avaliacao
    {
        public string Codigo { get; set; }
        public string Proposito { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public Aluno Respondente { get; set; }
        public List<Questao> Questoes { get; set; }
        public Dictionary<Questao, int> Respostas { get; set; }  
    }
}
