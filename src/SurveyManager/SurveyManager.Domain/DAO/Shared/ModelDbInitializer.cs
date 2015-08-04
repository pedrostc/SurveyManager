using System.Data.Entity;

namespace SurveyManager.Domain.DAO.Shared
{
    public class ModelDbInitializer : DropCreateDatabaseAlways<Contexto>
    {
    }
}
