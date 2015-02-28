namespace SurveyManager.Domain.Model.Implementation
{
    public class RespostaCincoNiveis : Resposta
    {
        public override int ValorMinimo { get { return 0; } }
        public override int ValorMaximo { get { return 4; } }
    }
}