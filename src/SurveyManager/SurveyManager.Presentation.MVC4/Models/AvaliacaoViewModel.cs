using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Presentation.MVC4.Models
{
    public class AvaliacaoViewModel
    {
        public Guid? Id { get; set; }
        public string Codigo { get; set; }
        public string Proposito { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public List<Questao> Questoes { get; set; }

        public AvaliacaoViewModel()
        {       
            Questoes = new List<Questao>();
        }

        public AvaliacaoViewModel(Avaliacao avaliacaoDb)
        {
            Id = avaliacaoDb.Id;
            Codigo = avaliacaoDb.Codigo;
            Proposito = avaliacaoDb.Proposito;
            DataInicio = avaliacaoDb.DataInicio;
            DataTermino = avaliacaoDb.DataTermino;
            Questoes = avaliacaoDb.Questoes;

        }

        public Avaliacao ConverterParaModelo()
        {
            Avaliacao avaliacaoDb = new Avaliacao()
            {
                Id = Id ?? Guid.NewGuid(),
                Codigo = Codigo,
                Proposito = Proposito
            };

            avaliacaoDb.DefinirDataInicio(DataInicio);
            avaliacaoDb.DefinirDataTermino(DataTermino);

            foreach (Questao questao in Questoes)
            {
                avaliacaoDb.AdicionarQuestao(questao);
            }

            return avaliacaoDb;
        }

        public void AtualizarModelo(Avaliacao avaliacaoDb)
        {
            avaliacaoDb.Codigo = Codigo;
            avaliacaoDb.Proposito = Proposito;

            avaliacaoDb.DefinirDataInicio(DataInicio);
            avaliacaoDb.DefinirDataTermino(DataTermino);

            foreach (Questao questao in Questoes)
            {
                avaliacaoDb.AdicionarQuestao(questao);
            }
        }
    }
}
