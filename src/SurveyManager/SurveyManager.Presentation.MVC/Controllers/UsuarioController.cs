using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyManager.Presentation.MVC.Models;

namespace SurveyManager.Presentation.MVC.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            List<UsuarioViewModel> model = new List<UsuarioViewModel>();
            model.Add(new AlunoViewModel()
            {
                Nome = "Fulano de Tal",
                Email = "Fulano@Mariolanet.com.br",
                Matricula = "666666-6"
            });

            model.Add(new AdministradorViewModel()
            {
                Nome = "Cicrano de Tal",
                Email = "cicrano@Mariolanet.com.br",
                Matricula = "666666-7"
            });

            return View(model);
        }

        public ActionResult Create(UsuarioViewModel model)
        {
            return null;
        }

    }
}
