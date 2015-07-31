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
    public class BlocosController : Controller
    {
        private readonly BlocosRepositorio BlocosRep = new BlocosRepositorio();
        private readonly CursoRepositorio CursosRep = new CursoRepositorio();

        // GET: Blocoes
        public ActionResult Index()
        {
            return View(BlocosRep.ListarTodos().ToList());
        }

        // GET: Blocoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blocoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid idCurso, [Bind(Include = "Id,Nome,Codigo")] Bloco bloco)
        {
            if (ModelState.IsValid)
            {
                bloco.Id = Guid.NewGuid();

                Curso curso = new Curso();
                curso.Blocos.Add(new Bloco { Id =  Guid.NewGuid(), Nome = bloco.Nome, Codigo = bloco.Codigo });

                CursosRep.Adicionar(curso);
                CursosRep.SalvarTodos();
                return RedirectToAction("Index");
            }

            return View(bloco);
        }

        // GET: Blocoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloco bloco = BlocosRep.Buscar();
            if (bloco == null)
            {
                return HttpNotFound();
            }
            return View(bloco);
        }

        // POST: Blocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Codigo")] Bloco bloco)
        {
            if (ModelState.IsValid)
            {
                BlocosRep.Atualizar(bloco);
                BlocosRep.SalvarTodos();
                return RedirectToAction("Index");
            }
            return View(bloco);
        }

        // GET: Blocoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloco bloco = BlocosRep.Buscar(id);
            if (bloco == null)
            {
                return HttpNotFound();
            }
            return View(bloco);
        }

        // POST: Blocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Bloco bloco = BlocosRep.Buscar(id);
            BlocosRep.Excluir(c => c == bloco);
            BlocosRep.SalvarTodos();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BlocosRep.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
