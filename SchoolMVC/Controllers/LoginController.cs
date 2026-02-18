using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            string domain = Request.Url.Host;
            string tigwsdomain = ConfigurationManager.AppSettings["tigwsdomain"];

            if (tigwsdomain.ToLower()==domain.ToLower())
            {
                return RedirectToAction("TigwsIndex", "Login");
            }
            UserMaster_UM User = new UserMaster_UM();
            ViewBag._ReturnUrl = returnUrl;
            return View(User);
        }

        [AllowAnonymous]
        public ActionResult TigwsIndex(string returnUrl)
        {
          
            UserMaster_UM User = new UserMaster_UM();
            ViewBag._ReturnUrl = returnUrl;
            return View(User);
        }
    }
}