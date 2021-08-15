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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Kirjautumisen tarkistuksia

            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return View();
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return RedirectToAction("Kirjautunut", "Home");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Tietoa järjestelmästä.";
            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return View();
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ota yhteyttä.";
            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return View();
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return View();
            }
        }

        public ActionResult Login()
        {
            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return View();
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return RedirectToAction("Kirjautunut", "Home");
            }
        }

        public ActionResult Kirjautunut()
        {
            if (Session["Email"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return View();
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            LainausjarjestelmaEntities db = new LainausjarjestelmaEntities();

            //Kirjautumistoiminto (Haetaan Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ-kyselyllä)

            var LoggedUser = db.Logins.SingleOrDefault(x => x.Email == LoginModel.Email && x.Salasana == LoginModel.Salasana);

            if (LoggedUser == null)
            {
                ViewBag.LoginMessage = "Kirjautuminen epäonnistui!";
                ViewBag.LoggedStatus = "Out";
                LoginModel.Kirjautumisvirhe = "Tuntematon sähköpostiosoite tai salasana.";
                return View("Login", LoginModel);
            }

            else
            {
                {

                //Onnistunut normaali käyttäjän kirjautuminen, jonka jälkeen siirrytään pääsivulle

                if (LoggedUser.Admin == false)
                    {
                ViewBag.LoginMessage = "Kirjautuminen onnistui.";
                ViewBag.LoggedStatus = "In";
                Session["Email"] = LoggedUser.Email;
                return RedirectToAction("Index", "Home");
                    }

                //Onnistunut admin käyttäjän kirjautuminen, jonka jälkeen siirrytään pääsivulle

                else
                    {
                        Session["Admin"] = LoggedUser.Email;
                        Session["Email"] = LoggedUser.Email;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }

        public ActionResult LogOut()
        {
            //Uloskirjautuminen, jonka jälkeen siirrytään pääsivulle

            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home");
        }
    }
}