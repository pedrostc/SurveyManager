using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyManager.Domain.DAO.Shared;
using SurveyManager.Domain.Model.Implementation;
using SurveyManager.Presentation.MVC4.Models;

namespace SurveyManager.Presentation.MVC4.Controllers
{
    public class BlocosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Blocoes
        public ActionResult Index()
        {
            return View(db.Blocos.ToList());
        }

        // GET: Blocoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloco bloco = db.Blocos.Find(id);
            if (bloco == null)
            {
                return HttpNotFound();
            }
            return View(bloco);
        }

        // GET: Blocoes/Create
        public ActionResult Create(Guid cursoId)
        {
            BlocoViewModel bloco = new BlocoViewModel() { CursoId = cursoId };
            return View(bloco);
        }

        // POST: Blocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Codigo,CursoId")] BlocoViewModel bloco)
        {
            if (ModelState.IsValid)
            {
                Curso curso = db.Cursos.Find(bloco.CursoId);
                Bloco blocoDb = new Bloco()
                {
                    Id = Guid.NewGuid(),
                    Nome = bloco.Nome,
                    Codigo = bloco.Codigo
                };
                curso.Blocos.Add(blocoDb);
                db.Blocos.Add(blocoDb);
                db.SaveChanges();
                return RedirectToAction("Edit", "Cursos", new { id =  bloco.CursoId} );
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
            Bloco bloco = db.Blocos.Find(id);
            db.Entry(bloco).Collection(b => b.Modulos).Load();
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
                db.Entry(bloco).State = EntityState.Modified;
                db.SaveChanges();
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
            Bloco bloco = db.Blocos.Find(id);
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
            Bloco bloco = db.Blocos.Find(id);
            db.Blocos.Remove(bloco);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult List(List<Bloco> blocos)
        {
            return View(blocos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
