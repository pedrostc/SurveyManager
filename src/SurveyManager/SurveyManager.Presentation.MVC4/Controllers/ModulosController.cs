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

namespace SurveyManager.Presentation.MVC4.Controllers
{
    public class ModulosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Modulos
        public ActionResult Index()
        {
            return View(db.Moduloes.ToList());
        }

        // GET: Modulos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Moduloes.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // GET: Modulos/Create
        public ActionResult Create(Guid blocoId)
        {
            return View();
        }

        // POST: Modulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Codigo")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                modulo.Id = Guid.NewGuid();
                db.Moduloes.Add(modulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modulo);
        }

        // GET: Modulos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Moduloes.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Codigo")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modulo);
        }

        // GET: Modulos/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Moduloes.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Modulo modulo = db.Moduloes.Find(id);
            db.Moduloes.Remove(modulo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult List(List<Modulo> modulos)
        {
            return View(modulos);
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
