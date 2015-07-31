using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.DAO.Shared
{
    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto") { }

        public System.Data.Entity.DbSet<SurveyManager.Domain.Model.Implementation.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<SurveyManager.Domain.Model.Implementation.Curso> Cursoes { get; set; }
    }
}
