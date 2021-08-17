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
    public class TuotteetController : Controller
    {
        private LainausjarjestelmaEntities db = new LainausjarjestelmaEntities();

        // GET: Tuotteet
        public ActionResult Index()
        {
            //Kirjautumisen tarkistus

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Admin";
                var tuotteet = db.Tuotteet.Include(t => t.Lainaajat).Include(t => t.Varastot);
                return View(tuotteet.ToList());
            }
        }

        public ActionResult Kuvat()
        {
            //Tuotekuvat -sivua pääsee selaamaan kirjautumatta

            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Uloskirjautunut";
                return View(db.Tuotteet.ToList());
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
                return View(db.Tuotteet.ToList());
            }
        }

        // GET: Tuotteet/Details
        public ActionResult Details(int? id)
        {
            ViewBag.LoggedStatus = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Create
        public ActionResult Create()
        {
            ViewBag.LoggedStatus = "Admin";

            //Yhdistetään Varastot-taulun varastopaikka ja -numero, käytetään pudotusvalikossa

            var KokoVarasto = db.Varastot;
            IEnumerable<SelectListItem> SelectVarastoList = from v in KokoVarasto
                                                            select new SelectListItem
                                                            {
                                                                Value = v.VarastoID.ToString(),
                                                                Text = v.Varastopaikka + " " + v.Numero
                                                            };
            ViewBag.kotiVarasto = new SelectList(SelectVarastoList, "Value", "Text");

            ViewBag.LainaajaID = new SelectList(db.Lainaajat, "LainaajaID", "Etunimi");
            return View();
        }

        // POST: Tuotteet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Kotivarasto,Kuva,Tila,Lainaaja,Lainauspaiva,Palautuspaiva,Varastopaikka,LainaajaID,VarastoID")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Tuotteet.Add(tuotteet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LainaajaID = new SelectList(db.Lainaajat, "LainaajaID", "Etunimi", tuotteet.LainaajaID);
            ViewBag.VarastoID = new SelectList(db.Varastot, "VarastoID", "Varastopaikka", tuotteet.VarastoID);
            return View(tuotteet);
        }

        // GET: Tuotteet/Edit
        public ActionResult Edit(int? id)
        {
            ViewBag.LoggedStatus = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            ViewBag.LainaajaID = new SelectList(db.Lainaajat, "LainaajaID", "Etunimi", tuotteet.LainaajaID);
            ViewBag.VarastoID = new SelectList(db.Varastot, "VarastoID", "Varastopaikka", tuotteet.VarastoID);
            return View(tuotteet);
        }

        // POST: Tuotteet/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Kotivarasto,Kuva,Tila,Lainaaja,Lainauspaiva,Palautuspaiva,Varastopaikka,LainaajaID,VarastoID")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuotteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LainaajaID = new SelectList(db.Lainaajat, "LainaajaID", "Etunimi", tuotteet.LainaajaID);
            ViewBag.VarastoID = new SelectList(db.Varastot, "VarastoID", "Varastopaikka", tuotteet.VarastoID);
            return View(tuotteet);
        }

        // GET: Tuotteet/Delete
        public ActionResult Delete(int? id)
        {
            ViewBag.LoggedStatus = "Admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // POST: Tuotteet/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(tuotteet);
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