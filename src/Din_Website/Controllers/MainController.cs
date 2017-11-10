using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Din_Website.Models;
using Logic;
using Models.AD;
using TMDbLib.Objects.Search;

namespace Din_Website.Controllers
{
    public class MainController : Controller
    {
        public ActionResult ChangePassword(NewPasswordData data)
        {
            if (AccountManagment.ChangePassword((Session["UserData"] as ADObject).SAMAccountName, data.Password1,
                data.Password2))
            {
                Session.Clear();
                Session["success"] = 1;
                Session["successString"] = "Password has been changed succesfully";
                return RedirectToAction("Index", "Home");
            }
            Session.Clear();
            Session["failed"] = 1;
            Session["failedString"] = "Oeps! password has not been changed";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SearchMovie()
        {
            if (!string.IsNullOrEmpty(Request.Form["searchQuery"]))
            {
                string searchQuery = Request.Form["searchQuery"];
                Session["MovieResults"] = MovieManager.SearchMovie(searchQuery);
                Session["CurrentMovies"] = MovieManager.GetCurrentMovies();
                return View("../UserPanel/SearchResults");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddMovie()
        {
            if (!string.IsNullOrEmpty(Request.Form["selected-movie"]))
            {
                int movieId = Convert.ToInt32(Request.Form["selected-movie"]);
                foreach (SearchMovie s in (List<SearchMovie>)Session["MovieResults"])
                {
                    if (s.Id == movieId)
                    {
                        switch (MovieManager.AddMovie(s, (Session["UserData"] as ADObject)).ToLower())
                        {
                            case "created":
                                Session["AddStatus"] = "success";
                                return View("../UserPanel/AddedMovie");
                            case "error":
                                Session["AddStatus"] = "failed";
                                return View("../UserPanel/AddedMovie");
                        }
                    }
                }
            }
            Session["AddStatus"] = "failed";
            return View("../UserPanel/AddedMovie");
        }
    }
}