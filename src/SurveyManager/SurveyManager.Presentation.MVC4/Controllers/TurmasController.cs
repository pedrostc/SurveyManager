using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyManager.Domain.DAO.Shared;
using SurveyManager.Domain.Repository;
using SurveyManager.Domain.Model.Implementation;
using SurveyManager.Presentation.MVC4.Models;

namespace SurveyManager.Presentation.MVC4.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TurmasController : Controller
    {
        private Contexto db = new Contexto();
        private readonly TurmaRepositorio TurmasRep = new TurmaRepositorio();

        // GET: Turmas
        public ActionResult Index()
        {
            return View(TurmasRep.ListarTodos().ToList());
        }

        public ActionResult ActionDeVoltar(string returnUrl)
        {
            if (returnUrl != "")
                return Redirect(returnUrl);
            else
                return null;
        }


        // GET: Turmas/Create
        public ActionResult Create(Guid ModuloId)
        {
            TurmaViewModel turma = new TurmaViewModel() { ModuloId = ModuloId };
            return View();
        }

        // POST: Turmas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuloId,Inicio,Fim,Nome,Codigo")] TurmaViewModel turma)
        {
            if (ModelState.IsValid)
            {
                Modulo modulo = db.Moduloes.Find(turma.ModuloId);
                Turma turmadb = new Turma()
                {
                    Id = Guid.NewGuid(),
                    Inicio = turma.Inicio,
                    Fim = turma.Fim,
                    Nome = turma.Nome,
                    Codigo = turma.Codigo
                };
                modulo.Turmas.Add(turmadb);
                TurmasRep.Adicionar(turmadb);
                db.SaveChanges();
                //TurmasRep.SalvarTodos();

                return RedirectToAction("Edit", "Modulos", new { id = turma.ModuloId });
            }

            return View(turma);
        }

        // GET: Turmas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = TurmasRep.Buscar(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Inicio,Fim,Nome,Codigo")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                TurmasRep.Adicionar(turma);
                TurmasRep.SalvarTodos();

                return RedirectToAction("Index");
            }
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = TurmasRep.Buscar(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Turma turma = TurmasRep.Buscar(id);
            TurmasRep.Excluir(t => t == turma);
            TurmasRep.SalvarTodos();

            return RedirectToAction("Index");
        }

        public ActionResult List(List<Turma> turmas)
        {
            return View(turmas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TurmasRep.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
