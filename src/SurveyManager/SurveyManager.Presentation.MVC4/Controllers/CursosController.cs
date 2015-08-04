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
        private readonly Contexto db = new Contexto();
        private readonly CursoRepositorio CursosRep = new CursoRepositorio();
        private readonly BlocosRepositorio BlocosRep = new BlocosRepositorio();

        // GET: Cursos
        public ActionResult Index()
        {
            return View(CursosRep.ListarTodos().ToList());
        }

        // GET: Blocos
        public ActionResult ListarBlocos(List<Bloco> blocos)
        {
            return View(blocos);
        }

        // GET: Cursoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cursoes/Create
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

        // GET: Blocos/Create
        public ActionResult AdicionarBloco(Guid? id)
        {
            ViewBag.CodCurso = id;
            return View();
        }

        // POST: Cursoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarBloco(Guid? id, [Bind(Include = "Id,Nome,Codigo")] Bloco bloco)
        {
            if (ModelState.IsValid)
            {
                //bloco.Id = Guid.NewGuid();

                Curso curso = CursosRep.Buscar(id);

                curso.Blocos.Add(new Bloco { Id = Guid.NewGuid(), Nome = bloco.Nome, Codigo = bloco.Codigo });

                CursosRep.Atualizar(curso);
                CursosRep.SalvarTodos();

                return RedirectToAction("Edit", curso.Id);
            }

            return View(bloco);
        }

        // GET: Cursoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            db.Entry(curso).Collection(c => c.Blocos).Load();
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
