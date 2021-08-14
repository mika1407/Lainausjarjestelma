using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LainausjarjestelmaMVC.Models;

namespace LainausjarjestelmaMVC.Controllers
{
    public class LainauksetController : Controller
    {
        private LainausjarjestelmaEntities db = new LainausjarjestelmaEntities();

        // GET: Lainaukset
        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                var lainaukset = db.Lainaukset.Include(l => l.Lainaajat).Include(l => l.Tuotteet).Include(l => l.Varastot);
                return View(lainaukset.ToList());
            }
        }

        // GET: Lainaukset/Details
        public ActionResult Details(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lainaukset lainaukset = db.Lainaukset.Find(id);
            if (lainaukset == null)
            {
                return HttpNotFound();
            }
            return View(lainaukset);
        }

        // GET: Lainaukset/Create
        public ActionResult Create()
        {
            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                var KokoNimi = db.Lainaajat;
                IEnumerable<SelectListItem> SelectNimiList = from l in KokoNimi
                                                             select new SelectListItem
                                                             {
                                                                 Value = l.LainaajaID.ToString(),
                                                                 Text = l.Etunimi + " " + l.Sukunimi
                                                             };
                ViewBag.LainaajaID = new SelectList(SelectNimiList, "Value", "Text");

                ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", "Tila"); //////TILA LISÄTTY

                var KokoVarasto = db.Varastot;
                IEnumerable<SelectListItem> SelectVarastoList = from v in KokoVarasto
                                                             select new SelectListItem
                                                             {
                                                                 Value = v.VarastoID.ToString(),
                                                                 Text = v.Varastopaikka + " " + v.Numero
                                                             };
                ViewBag.VarastoID = new SelectList(SelectVarastoList, "Value", "Text");
                return View();



            }

        }

        // POST: Lainaukset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LainausID,Tuote,Lainaaja,Lainauspaiva,Palautuspaiva,Varastopaikka,TuoteID,LainaajaID,VarastoID")] Lainaukset lainaukset)
        {
            if (ModelState.IsValid)
            {

                Tuotteet tuote = (from t in db.Tuotteet
                                  where t.TuoteID == lainaukset.TuoteID
                                  select t).FirstOrDefault();

                if (tuote.Tila == "Vapaa")
                {
                    tuote.Tila = "Lainassa";
                    //db.SaveChanges();
                    db.Lainaukset.Add(lainaukset);
                    db.SaveChanges();
                    TempData["onnistui"] = "Muistathan palauttaa tuotteen ajoissa. Kiitos!";
                    return RedirectToAction("Index");
                }
                else
                    TempData["epaonnistui"] = "Valitsemasi tuote on jo lainassa! Tarkasta voimassa olevat lainaukset alta.";
                return RedirectToAction("Index");
            }

            ViewBag.LainaajaID = new SelectList(db.Lainaajat, "LainaajaID", "Etunimi", lainaukset.LainaajaID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", lainaukset.TuoteID);
            ViewBag.VarastoID = new SelectList(db.Varastot, "VarastoID", "Varastopaikka", lainaukset.VarastoID);
            return View(lainaukset);
        }

        // GET: Lainaukset/Edit
        public ActionResult Edit(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lainaukset lainaukset = db.Lainaukset.Find(id);
            if (lainaukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.LainaajaID = new SelectList(db.Lainaajat, "LainaajaID", "Etunimi", lainaukset.LainaajaID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", lainaukset.TuoteID);
            ViewBag.VarastoID = new SelectList(db.Varastot, "VarastoID", "Varastopaikka", lainaukset.VarastoID);
            return View(lainaukset);
        }

        // POST: Lainaukset/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LainausID,Tuote,Lainaaja,Lainauspaiva,Palautuspaiva,Varastopaikka,TuoteID,LainaajaID,VarastoID")] Lainaukset lainaukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lainaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LainaajaID = new SelectList(db.Lainaajat, "LainaajaID", "Etunimi", lainaukset.LainaajaID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", lainaukset.TuoteID);
            ViewBag.VarastoID = new SelectList(db.Varastot, "VarastoID", "Varastopaikka", lainaukset.VarastoID);
            return View(lainaukset);
        }

        // GET: Lainaukset/Delete
        public ActionResult Delete(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lainaukset lainaukset = db.Lainaukset.Find(id);
            if (lainaukset == null)
            {
                return HttpNotFound();
            }
            return View(lainaukset);
        }

        // POST: Lainaukset/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Lainaukset lainaukset = db.Lainaukset.Find(id);

            Tuotteet tuote = (from t in db.Tuotteet
                              where t.TuoteID == lainaukset.TuoteID
                              select t).FirstOrDefault();

            tuote.Tila = "Vapaa";
            db.SaveChanges();

            db.Lainaukset.Remove(lainaukset);
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
