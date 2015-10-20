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
    public class BlocosController : Controller
    {
        private Contexto db = new Contexto();
        private readonly BlocosRepositorio BlocosRep = new BlocosRepositorio();
        private readonly CursoRepositorio CursosRep = new CursoRepositorio();

        // GET: Blocoes
        public ActionResult Index()
        {
            //Bloco bloco = db.Blocos.Find(id);
            //db.Entry(bloco).Collection(b => b.Modulos).Load();


            //IQueryable<Curso> cursos = CursosRep.ListarTodos();

            //var blocos = from c in cursos
            //             select c.Blocos


            //     CursosRep.ListarTodos()
            //db.Entry(Curso).Collection(b => b.Blocos).Load();

            return View(BlocosRep.ListarTodos().ToList());
        }

        public ActionResult ActionDeVoltar(string returnUrl)
        {
            if (returnUrl != "")
                return Redirect(returnUrl);
            else
                return null;
        }

        // GET: Blocoes/Create
        public ActionResult Create(Guid cursoId)
        {
            BlocoViewModel bloco = new BlocoViewModel() { CursoId = cursoId };
            return View(bloco);
        }


        // POST: Blocoes/Create
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
                BlocosRep.Adicionar(blocoDb);
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
            BlocosRep.Excluir(b => b == bloco);         
            BlocosRep.SalvarTodos();

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
                BlocosRep.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
