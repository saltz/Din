using System.Web.Mvc;
using System.Web.UI.WebControls;
using Din_Website.Models;
using Logic;
using Models.AD;

namespace Din_Website.Controllers
{
    public class MainController : Controller
    {
        public ActionResult ChangePassword(NewPasswordData data)
        {
            if (AccountManagment.ChangePassword((Session["UserData"] as AdObject).SAMAccountName, data.Password1,
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
    }
}