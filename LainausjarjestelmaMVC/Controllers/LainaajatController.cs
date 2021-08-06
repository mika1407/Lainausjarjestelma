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
    public class LainaajatController : Controller
    {
        private LainausjarjestelmaEntities db = new LainausjarjestelmaEntities();

        // GET: Lainaajat
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
                var lainaajat = db.Lainaajat.Include(l => l.Logins);
                return View(lainaajat.ToList());
            }
        }

        // GET: Lainaajat/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lainaajat lainaajat = db.Lainaajat.Find(id);
            if (lainaajat == null)
            {
                return HttpNotFound();
            }
            return View(lainaajat);
        }

        // GET: Lainaajat/Create
        public ActionResult Create()
        {
            ViewBag.LoggedStatus = "In";
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "Email");
            return View();
        }

        // POST: Lainaajat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LainaajaID,Etunimi,Sukunimi,Email,Puhelinnumero,LoginID")] Lainaajat lainaajat)
        {
            if (ModelState.IsValid)
            {
                db.Lainaajat.Add(lainaajat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "Email", lainaajat.LoginID);
            return View(lainaajat);
        }

        // GET: Lainaajat/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lainaajat lainaajat = db.Lainaajat.Find(id);
            if (lainaajat == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "Email", lainaajat.LoginID);
            return View(lainaajat);
        }

        // POST: Lainaajat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LainaajaID,Etunimi,Sukunimi,Email,Puhelinnumero,LoginID")] Lainaajat lainaajat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lainaajat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "Email", lainaajat.LoginID);
            return View(lainaajat);
        }

        // GET: Lainaajat/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lainaajat lainaajat = db.Lainaajat.Find(id);
            if (lainaajat == null)
            {
                return HttpNotFound();
            }
            return View(lainaajat);
        }

        // POST: Lainaajat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lainaajat lainaajat = db.Lainaajat.Find(id);
            db.Lainaajat.Remove(lainaajat);
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
