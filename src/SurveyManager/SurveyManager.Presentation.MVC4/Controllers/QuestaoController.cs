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
    public class QuestaoController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Questao
        public ActionResult Index()
        {
            return View(db.Questaos.ToList());
        }

        // GET: Questao/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questao questao = db.Questaos.Find(id);
            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        // GET: Questao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Texto")] Questao questao)
        {
            if (ModelState.IsValid)
            {
                questao.Id = Guid.NewGuid();
                db.Questaos.Add(questao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questao);
        }

        // GET: Questao/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questao questao = db.Questaos.Find(id);
            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        // POST: Questao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Texto")] Questao questao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questao);
        }

        // GET: Questao/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questao questao = db.Questaos.Find(id);
            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        // POST: Questao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Questao questao = db.Questaos.Find(id);
            db.Questaos.Remove(questao);
            db.SaveChanges();
            return RedirectToAction("Index");
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
