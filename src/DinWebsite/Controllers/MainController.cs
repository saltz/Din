using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DinWebsite.ExternalModels.AD;
using DinWebsite.Logic;
using TMDbLib.Objects.Search;

namespace DinWebsite.Controllers
{
    public class MainController : Controller
    {
        public ActionResult ChangePassword()
        {
            if (AccountManagment.ChangePassword((Session["UserData"] as ADObject).SAMAccountName, Request.Form["Password1"],
                Request.Form["Password2"]))
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
                var searchQuery = Request.Form["searchQuery"];
                Session["MovieResults"] = ContentManager.SearchMovie(searchQuery);
                Session["CurrentMovies"] = ContentManager.GetCurrentMovies();
                return View("../UserPanel/SearchResults");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddMovie()
        {
            if (!string.IsNullOrEmpty(Request.Form["selected-movie"]))
            {
                var movieId = Convert.ToInt32(Request.Form["selected-movie"]);
                foreach (var s in (List<SearchMovie>) Session["MovieResults"])
                    if (s.Id == movieId)
                        switch (ContentManager.AddMovie(s, Session["UserData"] as ADObject).ToLower())
                        {
                            case "created":
                                Session["AddStatus"] = "success";
                                return View("../UserPanel/MovieAdded");
                            case "error":
                                Session["AddStatus"] = "failed";
                                return View("../UserPanel/MovieAdded");
                        }
            }
            Session["AddStatus"] = "failed";
            return View("../UserPanel/MovieAdded");
        }

        public ActionResult GetMovieStatus()
        {
            Session["AddedContent"] = ContentManager.GetContentStatus(Session["UserData"] as ADObject);
            return View("../UserPanel/AddedMovies");
        }
    }
}