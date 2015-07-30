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
    public class UsuariosController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private readonly UsuarioRepositorio UsuarioRep = new UsuarioRepositorio();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(UsuarioRep.ListarTodos().ToList());
        }

        // GET: Usuarios
        public ActionResult ListarAdministrador()
        {
            return View(UsuarioRep.ListarAdms().ToList());
        }

        // GET: Usuarios
        public ActionResult ListarAluno()
        {
            return View(UsuarioRep.ListarAlunos().ToList());
        }

        public ActionResult ActionDeVoltar(string returnUrl)
        {
            if (returnUrl != "")
                return Redirect(returnUrl);
            else
                return null;
        }

        // GET: Usuarios/CreateAluno
        public ActionResult CreateAluno()
        {
            return View();
        }

        // POST: Usuarios/CreateAluno
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAluno([Bind(Include = "Id,Matricula,Nome,Email")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Id = Guid.NewGuid();
                UsuarioRep.Adicionar(aluno);
                UsuarioRep.SalvarTodos();

                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        // GET: Usuarios/CreateAdm
        public ActionResult CreateAdministrador()
        {
            return View();
        }

        // POST: Usuarios/CreateAdministrador
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdministrador([Bind(Include = "Id,Matricula,Nome,Email")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                administrador.Id = Guid.NewGuid();
                UsuarioRep.Adicionar(administrador);
                UsuarioRep.SalvarTodos();

                return RedirectToAction("ListarAdministrador");
            }

            return View(administrador);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = UsuarioRep.Buscar(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Matricula,Nome,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                UsuarioRep.Atualizar(usuario);
                UsuarioRep.SalvarTodos();

                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = UsuarioRep.Buscar(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Usuario usuario = UsuarioRep.Buscar(id);
            UsuarioRep.Excluir(c => c == usuario);
            UsuarioRep.SalvarTodos();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UsuarioRep.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
