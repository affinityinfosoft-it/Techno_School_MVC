using System;
using System.Web.Mvc;
using SchoolMVC.Models;
using System.Data.SqlClient;
using SchoolMVC.Controllers;
using System.Web.Security;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using BussinessObject;
using SchoolMVC.BLLService;

namespace SchoolMVC.Controllers
{
    public class RegistrationController : Controller
    {
       
        public static Service service;
        public RegistrationController()
        {

            service = new Service();
        }
        #region Reigstration Student
        [HttpGet]
        public ActionResult Registrationss()
        {
            return View("Registrationss");
        }

        [HttpPost]
        public ActionResult Registrationss(StudentRegistration_ST model)
        {
            if (ModelState.IsValid)
            {
                // GENERATE OTP
                string otp = Utils.GenerateSecureRandomCode();

                model.ST_OTP = otp;
                TempData["OtpSent"] = model.ST_OTP;
                TempData["OTP_TIME"] = Utils.GetIndiaTime();
                TempData["MOBILE"] = model.ST_MOBILENO;
               
                using (var db = new AuthenticationDB())
                {
                    var cmd = db.Database.Connection.CreateCommand();
                    cmd.CommandText = "SP_InsUpdateStudentRegistration";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.Add(new SqlParameter("@ST_ID", (object)model.ST_ID ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ST_CANDIDATEID", (object)model.ST_CANDIDATEID ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ST_PASSWORD", (object)model.ST_PASSWORD ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ST_REGDATE", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@ST_SESSION", (object)model.ST_SESSION ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ST_AUTOID", (object)model.ST_AUTOID ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ST_CLASS", (object)model.ST_CLASS ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ST_NAME", model.ST_NAME));
                    cmd.Parameters.Add(new SqlParameter("@ST_DOB", model.ST_DOB));
                    cmd.Parameters.Add(new SqlParameter("@ST_PG_DETAILS", model.ST_PG_DETAILS));
                    cmd.Parameters.Add(new SqlParameter("@ST_MOBILENO", model.ST_MOBILENO));
                    cmd.Parameters.Add(new SqlParameter("@ST_EMAIL", model.ST_EMAIL));
                    cmd.Parameters.Add(new SqlParameter("@ST_OTP", otp));

                    //var OutputCode = new SqlParameter("@OutputCode", SqlDbType.VarChar, 50)
                    //{
                    //    Direction = ParameterDirection.Output
                    //};
                    //cmd.Parameters.Add(OutputCode);

                    //var OutputMessage = new SqlParameter("@OutputMessage", SqlDbType.VarChar, 50)
                    //{
                    //    Direction = ParameterDirection.Output
                    //};
                    //cmd.Parameters.Add(OutputMessage);

                    var candidateIdOutput = new SqlParameter("@ST_CANDIDATEID_OUTPUT", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(candidateIdOutput);

                    var passwordOutput = new SqlParameter("@ST_PASSWORD_OUTPUT", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(passwordOutput);

                    var returnParam = new SqlParameter();
                    returnParam.ParameterName = "@ReturnVal";
                    returnParam.Direction = ParameterDirection.ReturnValue;
                    returnParam.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(returnParam);
                    db.Database.Connection.Open();
                    cmd.ExecuteNonQuery();
                    db.Database.Connection.Close();

                    int result = (int)returnParam.Value;

                    if (result == -1)
                    {
                        ModelState.AddModelError("ST_EMAIL", "This email ID is already used.");
                        return View(model);
                    }

                    TempData["ST_NAME"] = model.ST_NAME;
                    TempData["ST_EMAIL"] = model.ST_EMAIL;
                    TempData["ST_CANDIDATEID"] = candidateIdOutput.Value.ToString();
                    TempData["ST_PASSWORD"] = passwordOutput.Value.ToString();

                    SendOtp(model);

                    ViewBag.SuccessMessage = "Data saved successfully! Your User ID and password have been sent to your registered email address...";
                    ViewBag.RedirectToLogin = true;
                    return View();
                    //return RedirectToAction("Registration", "ValidateOTP");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SendOtp(StudentRegistration_ST model)
        {
            var smsrequest = new SmsGatewayModel
            {
                Phno = model.ST_MOBILENO,
                Msg = "Welcome to Sister Nivedita University. Your OTP for application form is " + model.ST_OTP,
                TemplateID = "1707171414676918937",
                FlashMsg = 0
            };
            var smsresult =  Utils.SendSmsAsync(smsrequest);
            return View("Login");
        }

        [HttpGet]
        public ActionResult ValidateOtp()
        {
            if (TempData["OtpSent"] == null || TempData["OTP_TIME"] == null)
            {
                return RedirectToAction("Registrationss");
            }
             TempData.Keep(); 
            return View();
        }

        [HttpPost]
        public ActionResult ValidateOtp(string enteredOtp)
        {
            if (TempData["OtpSent"] == null || TempData["OTP_TIME"] == null)
            {
                ViewBag.Error = "OTP session expired. Please register again.";
                return RedirectToAction("Registrationss");
            }

            string sentOtp = TempData["OtpSent"].ToString();
            string mobile = TempData["MOBILE"].ToString();

            DateTime otpTime = Convert.ToDateTime(TempData["OTP_TIME"]);

            TempData.Keep();

            if ((DateTime.Now - otpTime).TotalMinutes > 5)
            {
                ViewBag.Error = "OTP expired. Please register again.";
                return View();
            }

            if (enteredOtp == sentOtp)
            {
                using (var db = new AuthenticationDB())
                {
                    var student = db.StudentRegistration_ST.FirstOrDefault(s => s.ST_MOBILENO == mobile);

                    if (student != null)
                    {
                        student.ST_ACTIVE = "Y";         
                        db.SaveChanges();                  
                    }
                    else
                    {
                        ViewBag.Error = "Student record not found.";
                        return View();
                    }
                }
                string subject = "Student Registration Successful";

                string message =
                    "Dear " + TempData["ST_NAME"] + "<br/><br/>" +
                    "Your registration has been completed successfully.<br/><br/>" +
                    "<b>User ID:</b> " + TempData["ST_CANDIDATEID"] + "<br/>" +
                    "<b>Password:</b> " + TempData["ST_PASSWORD"] + "<br/><br/>" +
                    "Please keep this information safe.<br/><br/>" +
                    "Regards,<br/>" +
                    "School Administration";

                Utils.sendEmail(
                    TempData["ST_EMAIL"].ToString(),
                    subject,
                    message,
                    false,
                    false
                );

                //TempData.Remove("OtpSent");
                //TempData.Remove("OTP_TIME");

               
                return RedirectToAction("Login", "Registration");
            }

            ViewBag.Error = "Invalid OTP. Please try again.";
            return View();
        }

         //RESEND OTP
        [HttpPost]
        public ActionResult ReSendOtp()
        {
            // OTP session validation
            if (TempData["OtpSent"] == null || TempData["MOBILE"] == null)
            {
                ViewBag.Error = "OTP session expired. Please register again.";
                return RedirectToAction("Registrationss");
            }

            // Fetch existing OTP & mobile
            string otp = TempData["OtpSent"].ToString();
            string mobile = TempData["MOBILE"].ToString();

            // Reset OTP time
            TempData["OTP_TIME"] = Utils.GetIndiaTime();

            // Keep TempData alive for next request
            TempData.Keep();

            // RESEND SAME OTP
            var smsrequest = new SmsGatewayModel
            {
                Phno = mobile,
                Msg = "Welcome to Sister Nivedita University. Your OTP for application form is " + otp,
                TemplateID = "1707171414676918937",
                FlashMsg = 0
            };

            Utils.SendSmsAsync(smsrequest);

            ViewBag.Success = "OTP has been resent successfully.";
            return View("ValidateOtp");
        }

        #endregion

        private AuthenticationDB db = new AuthenticationDB();

        #region Student Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.StudentRegistration_ST
                    .FirstOrDefault(u =>
                        u.ST_CANDIDATEID.Equals(model.ST_CANDIDATEID.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        u.ST_PASSWORD == model.ST_PASSWORD);

                if (user != null)
                {
                    // Login successful
                    Session["CandidateId"] = user.ST_CANDIDATEID;
                    Session["Password"] = user.ST_PASSWORD;

                    return RedirectToAction("Dashboard", "Registration");
                }

                ModelState.AddModelError("", "Invalid Credential...");
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult ForgotPassword(StudentRegistration_ST model)
        {
            //TempData["ST_NAME"] = model.ST_NAME;
            //TempData["ST_EMAIL"] = model.ST_EMAIL;
            //TempData["ST_CANDIDATEID"] = model.ST_CANDIDATEID_OUTPUT;
            //TempData["ST_PASSWORD"] = model.ST_PASSWORD;

            string name = TempData["ST_NAME"].ToString();
            string email = TempData["ST_EMAIL"].ToString();
            string candidateid = TempData["ST_CANDIDATEID"].ToString();
            string password = TempData["ST_PASSWORD"].ToString();


            using (var db = new AuthenticationDB())
            {
                var user = db.StudentRegistration_ST
                             .FirstOrDefault(x => x.ST_EMAIL == email);

                if (user == null)
                {
                    ViewBag.Error = "Email not registered.";
                    return View();
                }

                // EMAIL CONTENT

                string subject = "Student Registration Password Reset Details";
                string message =
                    "Dear " + TempData["ST_NAME"] + "<br/><br/>" +
                    "Your registration has been completed successfully.<br/><br/>" +
                    "<b>User ID:</b> " + TempData["ST_CANDIDATEID"] + "<br/>" +
                    "<b>Password:</b> " + TempData["ST_PASSWORD"] + "<br/><br/>" +
                    "Please keep this information safe.<br/><br/>" +
                    "Regards,<br/>" +
                    "School Administration";

                Utils.sendEmail(
                    TempData["ST_EMAIL"].ToString(),
                    subject,
                    message,
                    false,
                    false
                );

                return View();
            }
        }

        #endregion
        #region Dashboard

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["CandidateId"] == null)
            {
                return RedirectToAction("Index");// Or wherever your login is
            }

            ViewBag.CandidateId = Session["CandidateId"];
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            if (Session["CandidateId"] == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.CandidateId = Session["CandidateId"];
            string candidateId = Session["CandidateId"].ToString();
            var applicationList = service.GetApplicationList(candidateId);
            return View(applicationList);
        }
     
        #endregion
        #region Apply Student

       
        [HttpGet]
        public ActionResult Apply()
        {
            if (Session["CandidateId"] == null)
            {
                return RedirectToAction("Login");
            }
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.RedirectToLogin = true;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Apply(StudentApplication_AP model)
        {
            //if (Session["CandidateId"] == null)
            //{
            //    return RedirectToAction("Login");
            //}
            try
            {
                string loggedInCandidateId = Session["CandidateId"] as string;

                if (string.IsNullOrEmpty(loggedInCandidateId))
                {
                    return RedirectToAction("Login", "Registration");
                }

                model.SA_ST_CANDIDATEID = loggedInCandidateId;

                string birthFileRelPath = null, idFileRelPath = null, marksheetFileRelPath = null;
                string uploadFolderPath = Server.MapPath("~/SPUploadedFiles/");
                if (!Directory.Exists(uploadFolderPath))
                    Directory.CreateDirectory(uploadFolderPath);

                // Save Birth Certificate
                if (model.AP_BirthCertificate != null && model.AP_BirthCertificate.ContentLength > 0)
                {
                    string birthFileName = Path.GetFileName(model.AP_BirthCertificate.FileName);
                    string fullBirthPath = Path.Combine(uploadFolderPath, birthFileName);
                    model.AP_BirthCertificate.SaveAs(fullBirthPath);
                    birthFileRelPath = "~/SPUploadedFiles/" + birthFileName;
                }

                // Save ID Proof
                if (model.AP_IdProof != null && model.AP_IdProof.ContentLength > 0)
                {
                    string idFileName = Path.GetFileName(model.AP_IdProof.FileName);
                    string fullIdPath = Path.Combine(uploadFolderPath, idFileName);
                    model.AP_IdProof.SaveAs(fullIdPath);
                    idFileRelPath = "~/SPUploadedFiles/" + idFileName;
                }

                // Save Marksheet
                if (model.AP_Marksheet != null && model.AP_Marksheet.ContentLength > 0)
                {
                    string marksheetFileName = Path.GetFileName(model.AP_Marksheet.FileName);
                    string fullMarksheetPath = Path.Combine(uploadFolderPath, marksheetFileName);
                    model.AP_Marksheet.SaveAs(fullMarksheetPath);
                    marksheetFileRelPath = "~/SPUploadedFiles/" + marksheetFileName;
                }

                string selectedSchools = model.AP_School != null ? string.Join(",", model.AP_School) : null;

                using (var db = new AuthenticationDB())
                {
                    var connection = db.Database.Connection;
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SP_InsertStudentApplication";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@SA_ST_ID", model.SA_ST_ID));
                        cmd.Parameters.Add(new SqlParameter("@SA_ST_CANDIDATEID", loggedInCandidateId));
                        cmd.Parameters.Add(new SqlParameter("@SA_ST_SESSION", model.SA_ST_SESSION));
                        //cmd.Parameters.Add(new SqlParameter("@AP_School", model.AP_School));

                        cmd.Parameters.Add(new SqlParameter("@AP_School", (object)selectedSchools ?? DBNull.Value));

                        cmd.Parameters.Add(new SqlParameter("@SA_ST_CLASS", model.SA_ST_CLASS));
                        cmd.Parameters.Add(new SqlParameter("@AP_Gender", model.AP_Gender));
                        cmd.Parameters.Add(new SqlParameter("@AP_Nationality", model.AP_Nationality));
                        cmd.Parameters.Add(new SqlParameter("@AP_Religion", model.AP_Religion));
                        cmd.Parameters.Add(new SqlParameter("@AP_Caste", model.AP_Caste));
                        cmd.Parameters.Add(new SqlParameter("@AP_BloodGroup", model.AP_BloodGroup));
                        cmd.Parameters.Add(new SqlParameter("@AP_AadhaarNo", model.AP_AadhaarNo));
                        cmd.Parameters.Add(new SqlParameter("@AP_PerAddress", model.AP_PerAddress));
                        cmd.Parameters.Add(new SqlParameter("@AP_PreAddress", model.AP_PreAddress));
                        cmd.Parameters.Add(new SqlParameter("@AP_State", model.state));
                        cmd.Parameters.Add(new SqlParameter("@AP_District", model.district));
                        cmd.Parameters.Add(new SqlParameter("@AP_PoliceStation", model.AP_PoliceStation));
                        cmd.Parameters.Add(new SqlParameter("@AP_PO", model.AP_PO));
                        cmd.Parameters.Add(new SqlParameter("@AP_Pin", model.AP_Pin));
                        cmd.Parameters.Add(new SqlParameter("@AP_LastSchoolName", model.AP_LastSchoolName));
                        cmd.Parameters.Add(new SqlParameter("@AP_TCNO", model.AP_TCNO));
                        cmd.Parameters.Add(new SqlParameter("@AP_TCDate", model.AP_TCDate ?? (object)DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@AP_TCTYPE", model.AP_TCTYPE));
                        cmd.Parameters.Add(new SqlParameter("@AP_AdmissionDate", model.AP_AdmissionDate ?? (object)DBNull.Value));

                        cmd.Parameters.Add(new SqlParameter("@AP_BirthCertificate", SqlDbType.NVarChar) { Value = (object)birthFileRelPath ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@AP_IdProof", SqlDbType.NVarChar) { Value = (object)idFileRelPath ?? DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@AP_Marksheet", SqlDbType.NVarChar) { Value = (object)marksheetFileRelPath ?? DBNull.Value });

                        cmd.Parameters.Add(new SqlParameter("@AP_CreatedDate", DateTime.Now));

                        var returnParam = new SqlParameter("@ReturnVal", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(returnParam);

                        cmd.ExecuteNonQuery();
                        int returnVal = (int)returnParam.Value;

                        if (returnVal == 1)
                        {
                            TempData["Success"] = "Application submitted successfully!";
                            return RedirectToAction("Dashboard", new { candidateId = loggedInCandidateId });
                        }
                        else if (returnVal == -1)
                        {
                            ModelState.AddModelError("", "Duplicate application found (SA_ID already exists).");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unexpected error while saving the application.");
                        }
                    }

                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error submitting application: " + ex.Message);
            }

            return View(model);
        }


        #endregion
        #region Student Apply View Form
       
        [HttpGet]
        public ActionResult ApplyView(long saId)
        {
          
            //if (Session["CandidateId"] == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //string sessionCandidateId = Session["CandidateId"].ToString();

            StudentApplication_AP model = null;

            using (var db = new AuthenticationDB())
            {
                var connection = db.Database.Connection;
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SP_GetStudentApplicationById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SA_ID", saId));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new StudentApplication_AP
                            {
                                SA_ID = Convert.ToInt64(reader["SA_ID"]),
                                SA_ST_CANDIDATEID = reader["SA_ST_CANDIDATEID"].ToString(),
                               // ST_REGDATE = Convert.ToDateTime(reader["ST_REGDATE"]),
                                ST_REGDATE = reader["ST_REGDATE"] != DBNull.Value ? Convert.ToDateTime(reader["ST_REGDATE"]) : (DateTime?)null,
                                ST_NAME = reader["ST_NAME"].ToString(),
                                ST_PG_DETAILS = reader["ST_PG_DETAILS"].ToString(),
                                ST_DOB = Convert.ToDateTime(reader["ST_DOB"]),
                                SCM_SCHOOLNAME = reader["SCM_SCHOOLNAME"].ToString(),
                                SA_ST_CLASS = reader["SA_ST_CLASS"].ToString(),
                                SA_ST_SESSION = reader["SA_ST_SESSION"].ToString(),
                                AP_Nationality = reader["AP_Nationality"].ToString(),
                                AP_Religion = reader["AP_Religion"].ToString(),
                                AP_Caste = reader["AP_Caste"].ToString(),
                                AP_BloodGroup = reader["AP_BloodGroup"].ToString(),
                                AP_Gender = reader["AP_Gender"].ToString(),
                                AP_AadhaarNo = reader["AP_AadhaarNo"].ToString(),
                                ST_MOBILENO = reader["ST_MOBILENO"].ToString(),
                                ST_EMAIL = reader["ST_EMAIL"].ToString(),
                                AP_PerAddress = reader["AP_PerAddress"].ToString(),
                                AP_PreAddress = reader["AP_PreAddress"].ToString(),
                                AP_State = reader["AP_State"].ToString(),
                                district = reader["district"].ToString(),
                                AP_PoliceStation = reader["AP_PoliceStation"].ToString(),
                                AP_PO = reader["AP_PO"].ToString(),
                                AP_Pin = reader["AP_Pin"].ToString(),
                                AP_LastSchoolName = reader["AP_LastSchoolName"].ToString(),
                                AP_TCNO = reader["AP_TCNO"].ToString(),
                                AP_TCDate = reader["AP_TCDate"] != DBNull.Value ? Convert.ToDateTime(reader["AP_TCDate"]) : (DateTime?)null,
                                AP_TCTYPE = reader["AP_TCTYPE"].ToString(),
                                AP_AdmissionDate = reader["AP_AdmissionDate"] != DBNull.Value ? Convert.ToDateTime(reader["AP_AdmissionDate"]) : (DateTime?)null,
                                AP_BirthCertificates = reader["AP_BirthCertificates"].ToString(),
                                AP_IdProofs = reader["AP_IdProofs"].ToString(),
                                AP_Marksheets = reader["AP_Marksheets"].ToString(),
                            };
                        }
                    }
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            if (model == null)
            {
                ViewBag.Error = "No application found for SA_ID: " + saId;
                return View("Error");
            }

            //if (!string.Equals(model.SA_ST_CANDIDATEID, sessionCandidateId, StringComparison.OrdinalIgnoreCase))
            //{
            //    return new HttpStatusCodeResult(403, "You are not authorized to view this application.");
            //}

            return View(model); 
        }
        #endregion        
    }
}
