using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolMVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Demo()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Dashboard(long schoolid, long sesid, string returnUrl)
        {
            ViewBag.SchoolId = schoolid;
            ViewBag.SessionId = sesid;
            if (UserModel != null) return home(returnUrl);
            return View();
        }
        public ActionResult HomeDashboard()
        {
            return View("Dashboard");
        }
        #region LogOut
        [OutputCache(NoStore = true, Duration = 60, VaryByParam = "*")]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        #endregion
        public ActionResult index1()
        {
            return View();
        }
        public ActionResult index2()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            var user = Session["UserLoginModel"] as UserMaster_UM;
            if (user == null)
                return RedirectToAction("Index", "Login");

            return PartialView("_ChangePasswordModal");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(ChangePasswordVM model)
        {
            try
            {
                var user = Session["UserLoginModel"] as UserMaster_UM;
                if (user == null)
                {
                    return Json(new
                    {
                        IsSuccess = false,
                        Message = "Session expired. Please login again.",
                        Logout = true
                    });
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    return Json(new
                    {
                        IsSuccess = false,
                        Message = "New Password and Confirm Password do not match"
                    });
                }

                long? userId = user.UM_FP_ID;
                // ✅ get numeric UserID safely

                string conStr = ConfigurationManager
                                .ConnectionStrings["School_DbEntity"]
                                .ConnectionString;

                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand("SP_ChangePassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERID", userId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@OLDPASSWORD", model.OldPassword);
                    cmd.Parameters.AddWithValue("@NEWPASSWORD", model.NewPassword);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        // Logout after password change
                        Session.Clear();
                        Session.Abandon();
                        FormsAuthentication.SignOut();

                        return Json(new
                        {
                            IsSuccess = true,
                            Message = "Password changed successfully. Please login again.",
                            Logout = true
                        });
                    }

                    return Json(new
                    {
                        IsSuccess = false,
                        Message = "Old password is incorrect"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }


    }

}