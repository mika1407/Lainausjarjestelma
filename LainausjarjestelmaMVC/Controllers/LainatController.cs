using LainausjarjestelmaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LainausjarjestelmaMVC.Controllers
{
    public class LainatController : Controller
    {

        LainausjarjestelmaEntities db = new LainausjarjestelmaEntities();

        // GET: Lainat
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HaeTuote()
        {
            var tuote = db.Tuotteet.ToList();

            return Json(tuote, JsonRequestBehavior.AllowGet);
        }
    }
}