using System;
using System.Web.Mvc;
using DIN.Models;
using Logic;
using Models.AD;

namespace DIN.Controllers
{
    public class HomeController : Controller
    {
        private LoginSystem _loginSystem = new LoginSystem();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginData data)
        {
            Session.Clear();
            Tuple<bool, ADObject> result = _loginSystem.Login(data.Username, data.Password);
            if (result.Item1)
            {
                Session["Name"] = result.Item2.Name;
                foreach (var v in (result.Item2 as ADUser).Groups)
                {
                    if (v.DistinguishedName.ToLower().Contains("domain admins"))
                    {
                        Session["PermissionLevel"] = "admin";
                        return View("../AdminPannel/index");
                    }
                    else if (v.DistinguishedName.ToLower().Contains("kodi users"))
                    {
                        Session["PermissionLevel"] = "user";
                        return View("user");
                    }
                }
            }
            Session["failed"] = 1;
            return RedirectToAction("Index");
        }
    }
}