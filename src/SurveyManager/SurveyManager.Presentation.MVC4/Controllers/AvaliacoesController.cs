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
    public class AvaliacoesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Avaliacoes
        public ActionResult Index()
        {
            return View(db.Avaliacaos.ToList());
        }

        // GET: Avaliacoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        // GET: Avaliacoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avaliacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Proposito,DataInicio,DataTermino")] AvaliacaoViewModel avaliacao)
        {
            if (ModelState.IsValid)
            {
                Avaliacao avaliacaoDb = avaliacao.ConverterParaModelo();

                db.Avaliacaos.Add(avaliacaoDb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }

            AvaliacaoViewModel avaliacaoView = new AvaliacaoViewModel(avaliacao);

            return View(avaliacaoView);
        }

        // POST: Avaliacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Proposito,DataInicio,DataTermino")] AvaliacaoViewModel avaliacao)
        {
            if (ModelState.IsValid)
            {
                Avaliacao avaliacaoDb = db.Avaliacaos.Find(avaliacao.Id);

                avaliacao.AtualizarModelo(avaliacaoDb);

                db.Entry(avaliacaoDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            db.Avaliacaos.Remove(avaliacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MapQuestoes(Guid id)
        {
            ViewData["avaliacaoId"] = id;
            
            Avaliacao avaliacaodb = db.Avaliacaos.Find(id);
            db.Entry(avaliacaodb).Collection(a=>a.Questoes).Load();

            return View(avaliacaodb.Questoes);
        }

        public ActionResult ListToAvaliacao(Guid AvaliacaoId, List<Questao> questoesAval)
        {
            ViewData["AvaliacaoId"] = AvaliacaoId;
            List<Questao> questoesDb = db.Questaos.ToList();

            foreach (var questao in questoesAval)
            {
                if (questoesDb.Any(q => q.Id == questao.Id))
                {
                    questoesDb.Remove(questoesDb.FirstOrDefault(q => q.Id == questao.Id));
                }
            }

            return View(questoesDb);
        }

        [HttpGet]
        public ActionResult AddToAvaliacao(Guid AvaliacaoId, Guid QuestaoId)
        {
            Avaliacao avaliacao = db.Avaliacaos.Find(AvaliacaoId);
            db.Entry(avaliacao).Collection(q=>q.Questoes).Load();
            Questao questao = db.Questaos.Find(QuestaoId);

            avaliacao.AdicionarQuestao(questao);

            db.Entry(avaliacao).State = EntityState.Modified;
            db.SaveChanges();

            return View(AvaliacaoId);
        }

        [HttpGet]
        public ActionResult RemoveFromAvaliacao(Guid AvaliacaoId, Guid QuestaoId)
        {
            Avaliacao avaliacao = db.Avaliacaos.Find(AvaliacaoId);
            db.Entry(avaliacao).Collection(q => q.Questoes).Load();
            Questao questao = db.Questaos.Find(QuestaoId);

            avaliacao.RemoverQuestao(questao);

            db.Entry(avaliacao).State = EntityState.Modified;
            db.SaveChanges();

            return View(AvaliacaoId);
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
