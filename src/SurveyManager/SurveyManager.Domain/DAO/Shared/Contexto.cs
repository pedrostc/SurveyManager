﻿using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SurveyManager.Domain.Model.Implementation;

namespace SurveyManager.Domain.DAO.Shared
{
    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
            Database.SetInitializer(new ModelDbInitializer());
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Bloco> Blocos { get; set; }

        public DbSet<Modulo> Moduloes { get; set; }

        public DbSet<Turma> Turmas { get; set; }

        public System.Data.Entity.DbSet<SurveyManager.Domain.Model.Implementation.Questao> Questaos { get; set; }

        public System.Data.Entity.DbSet<SurveyManager.Domain.Model.Implementation.Avaliacao> Avaliacaos { get; set; }
    }
}
