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
    public class VarastotController : Controller
    {
        private LainausjarjestelmaEntities db = new LainausjarjestelmaEntities();

        // GET: Varastot
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return View(db.Varastot.ToList());
            }
        }

        // GET: Varastot/Details
        public ActionResult Details(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Varastot varastot = db.Varastot.Find(id);
            if (varastot == null)
            {
                return HttpNotFound();
            }
            return View(varastot);
        }

        // GET: Varastot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Varastot/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VarastoID,Varastopaikka,Numero")] Varastot varastot)
        {
            if (ModelState.IsValid)
            {
                db.Varastot.Add(varastot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(varastot);
        }

        // GET: Varastot/Edit
        public ActionResult Edit(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Varastot varastot = db.Varastot.Find(id);
            if (varastot == null)
            {
                return HttpNotFound();
            }
            return View(varastot);
        }

        // POST: Varastot/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VarastoID,Varastopaikka,Numero")] Varastot varastot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(varastot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(varastot);
        }

        // GET: Varastot/Delete
        public ActionResult Delete(int? id)
        {
            ViewBag.LoggedStatus = "In";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Varastot varastot = db.Varastot.Find(id);
            if (varastot == null)
            {
                return HttpNotFound();
            }
            return View(varastot);
        }

        // POST: Varastot/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Varastot varastot = db.Varastot.Find(id);
            db.Varastot.Remove(varastot);
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
