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
    public class LoginsController : Controller
    {
        private LainausjarjestelmaEntities db = new LainausjarjestelmaEntities();

        // GET: Logins
        public ActionResult Index()
        {
            //Kirjautumisen tarkistus

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return View(db.Logins.ToList());
            }
        }

        // GET: Logins/Details
        public ActionResult Details(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logins logins = db.Logins.Find(id);
            if (logins == null)
            {
                return HttpNotFound();
            }
            return View(logins);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            ViewBag.LoggedStatus = "In";
            return View();
        }

        // POST: Logins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginID,Email,Salasana,Kirjautumisvirhe,Admin")] Logins logins)
        {
            if (ModelState.IsValid)
            {
                db.Logins.Add(logins);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logins);
        }

        // GET: Logins/Edit
        public ActionResult Edit(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logins logins = db.Logins.Find(id);
            if (logins == null)
            {
                return HttpNotFound();
            }
            return View(logins);
        }

        // POST: Logins/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginID,Email,Salasana,Kirjautumisvirhe,Admin")] Logins logins)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logins).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logins);
        }

        // GET: Logins/Delete
        public ActionResult Delete(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logins logins = db.Logins.Find(id);
            if (logins == null)
            {
                return HttpNotFound();
            }
            return View(logins);
        }

        // POST: Logins/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logins logins = db.Logins.Find(id);
            db.Logins.Remove(logins);
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