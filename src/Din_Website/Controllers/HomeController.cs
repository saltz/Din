using System;
using System.Web.Mvc;
using Din_Website.Models;
using Logic;
using Models.AD;
using Models.Exceptions;

namespace Din_Website.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginData data)
        {
            try
            {
                Session.Clear();
                if (data.Username == "dane") //REMOVE THESE LINES
                {
                    Session["Name"] = "Dane Naebers";
                    Session["PermissionLevel"] = "user";
                    return View("../UserPannel/index");
                }

                Tuple<bool, ADObject> result = LoginSystem.Login(data.Username, data.Password);
                if (result.Item1)
                {
                    Session["Name"] = result.Item2.Name;
                    var adUser = result.Item2 as ADUser;
                    if (adUser != null)
                        foreach (var v in adUser.Groups)
                        {
                            if (v.DistinguishedName.ToLower().Contains("domain admins"))
                            {
                                Session["PermissionLevel"] = "admin";
                                return View("../AdminPannel/index");
                            }
                            else if (v.DistinguishedName.ToLower().Contains("kodi users"))
                            {
                                Session["PermissionLevel"] = "user";
                                return View("../UserPannel/index");
                            }
                        }
                }
                Session["failed"] = 1;
                Session["failedString"] = "Gebruikersnaam of Wachtwoord is incorrect";
                return RedirectToAction("Index");
            }
            catch (LoginException ex)
            {
                Session["failed"] = 1;
                Session["failedString"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}