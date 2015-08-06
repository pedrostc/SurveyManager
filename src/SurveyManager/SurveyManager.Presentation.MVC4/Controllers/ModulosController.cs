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
using SurveyManager.Domain.Repository;
using SurveyManager.Presentation.MVC4.Models;

namespace SurveyManager.Presentation.MVC4.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ModulosController : Controller
    {
        private Contexto db = new Contexto();
        private readonly ModuloRepositorio ModulosRep = new ModuloRepositorio();

        // GET: Modulos
        public ActionResult Index()
        {
            return View(ModulosRep.ListarTodos().ToList());
        }

        public ActionResult ActionDeVoltar(string returnUrl)
        {
            if (returnUrl != "")
                return Redirect(returnUrl);
            else
                return null;
        }

        // GET: Modulos/Create
        public ActionResult Create(Guid blocoId)
        
        {
            ModuloViewModel modulo = new ModuloViewModel() { BlocoId = blocoId };
            return View(modulo);
        }

        // POST: Modulos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "blocoId,Nome,Codigo")] ModuloViewModel modulo)
        {
            if (ModelState.IsValid)
            {
                Bloco bloco = db.Blocos.Find(modulo.BlocoId);
                Modulo moduloDb = new Modulo()
                {
                    Id = Guid.NewGuid(),
                    Nome = modulo.Nome,
                    Codigo = modulo.Codigo
                };
                bloco.Modulos.Add(moduloDb);
                ModulosRep.Adicionar(moduloDb);
                db.SaveChanges();

                return RedirectToAction("Edit", "Blocos", new { id = modulo.BlocoId });
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
            db.Entry(modulo).Collection(b => b.Turmas).Load();
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Codigo")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                ModulosRep.Atualizar(modulo);
                ModulosRep.SalvarTodos();

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
            Modulo modulo = ModulosRep.Buscar(id);
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
            Modulo modulo = ModulosRep.Buscar(id);
            ModulosRep.Excluir(m => m == modulo);
            ModulosRep.SalvarTodos();

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
                ModulosRep.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
