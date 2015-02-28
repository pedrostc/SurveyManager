namespace SurveyManager.Domain.Model.Implementation
{
    public abstract class Usuario : ModelBase
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
