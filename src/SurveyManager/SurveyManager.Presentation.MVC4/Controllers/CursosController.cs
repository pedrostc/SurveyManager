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

namespace SurveyManager.Presentation.MVC4.Controllers
{
    public class CursosController : Controller
    {
        private readonly CursoRepositorio CursosRep = new CursoRepositorio();
        private readonly BlocosRepositorio BlocosRep = new BlocosRepositorio();

        // GET: Cursoes
        public ActionResult Index()
        {
            return View(CursosRep.ListarTodos().ToList());
        }

        // GET: Cursoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cursoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Codigo")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                curso.Id = Guid.NewGuid();
                CursosRep.Adicionar(curso);
                CursosRep.SalvarTodos();
                return RedirectToAction("Index");
            }

            return View(curso);
        }

        // GET: Cursoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = CursosRep.Buscar(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Cursoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Codigo")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                CursosRep.Atualizar(curso);
                CursosRep.SalvarTodos();
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        // GET: Cursoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = CursosRep.Buscar(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Cursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Curso curso = CursosRep.Buscar(id);
            CursosRep.Excluir(c => c == curso);
            CursosRep.SalvarTodos();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CursosRep.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
