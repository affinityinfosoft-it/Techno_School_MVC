using BussinessObject.FeesCollection;
using SchoolMVC.Areas.FeesCollection.Models;
using SchoolMVC.Controllers;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Areas.FeesCollection.Controllers
{
    public class FeesCollectionController : BaseController
    {
        static int additionServiceChargeId;
        static int additionLateFeesId;
        public FeesCollectionController() { GetAdditionalFeesIds(); }
        public static void GetAdditionalFeesIds()
        {
            List<AdditionalFeesBO> FeesIds = service.GetAdditionalFeesIds();
            additionServiceChargeId = FeesIds.Count == 0 ? 0 : FeesIds.Single(m => m.FeesCode == "SC").FeesId;
            additionLateFeesId = FeesIds.Count == 0 ? 0 : FeesIds.Single(m => m.FeesCode == "LF").FeesId;
        }

        #region AdmissionCollection
        [HttpGet]
        public ActionResult AdmissionFees()
        {
            ViewBag._Action = "add";
            ViewBag._ViewAction = "add";
            Session["AdmissionserviceChargeFees"] = null;

            if (UserModel == null) return returnLogin("~/FeesCollection/FeesCollection/AdmissionFees");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("FeesCollectionList", "FeesCollection", new { area = "FeesCollection" });
            }
            try
            {
                AdmissionFeesModel Model = new AdmissionFeesModel();
                FillList(Model);
                Session["AdmissionFees_Model"] = Model;
                return View("AdmissionFees", Model);
            }
            catch { ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator."; return View("Error"); }
        }
        [HttpPost]
        public ActionResult AdmissionFees(AdmissionFeesModel model)
        {
            ViewBag._Action = "add";
            ViewBag._ViewAction = "add";
            try
            {
                ModelState.Clear();
                model.listSearchDetails = new List<StudentList>();
                model.StudentFees = new List<studentFeesDetails>();
                model.StudentInformation = new studentInfo();
                if (string.IsNullOrWhiteSpace(model.FormNo) == false) { model.FormNo = model.FormNo.Trim(); }
                if (string.IsNullOrWhiteSpace(model.AdmissionNo) == false) { model.AdmissionNo = model.AdmissionNo.Trim(); }

                if (model.CM_CLASSID == null && model.FormNo == null && model.AdmissionNo == null)
                {
                    ViewBag._Message = "please provide any input... !!";
                    FillList(model);
                    Session["AdmissionFees_Model"] = model;
                    return View("AdmissionFees", model);
                }
                Session["AdmissionFees_Model"] = null;
                FeesSearchBO SearchBO = new FeesSearchBO();
                SearchBO = mapToBO(model, "A");
                if (SearchBO.ClassId != null && SearchBO.ClassId > 0 && SearchBO.FormNo == null && SearchBO.AdmissionNo == null)
                {
                    model.listSearchDetails = service.SearchNonAdmittedStudent(SearchBO);
                    if (model.listSearchDetails != null && model.listSearchDetails.Count > 0)
                    {
                        int i = 0; foreach (var item in model.listSearchDetails) item.rownum = ++i;
                    }
                }
                else
                {
                    FillFeesDetails(model, SearchBO);
                    if (model.StudentFees == null || model.StudentFees.Count == 0) ViewBag._Message = "No Fees are available... !!";
                }
                FillList(model);
                Session["AdmissionFees_Model"] = model;
                return View("AdmissionFees", model);
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator." + ex.ToString();
                return View("AdmissionFees", reFillAutoList(model));
            }
        }

        [HttpGet]
        public ActionResult FeesCollectionWhileStudentAdmitted(string AdmissionNo)
        {
            if (AdmissionNo == null && AdmissionNo == "")
            {
                TempData["_Message"] = "There is an issue in Admission Page!!!";
                return (ActionResult)RedirectToAction("AdmittedStudentList", "StudentManagement", new { area = "" });
            }
            else
            {
                ViewBag._Action = "add";
                ViewBag._ViewAction = "add";
                try
                {
                    Session["AdmissionFees_Model"] = null;
                    AdmissionFeesModel model = new AdmissionFeesModel();
                    model.AdmissionNo = AdmissionNo.Trim();
                    FeesSearchBO SearchBO = new FeesSearchBO();
                    SearchBO = mapToBO(model, "A");
                    FillFeesDetails(model, SearchBO);
                    if (model.StudentFees == null || model.StudentFees.Count == 0) ViewBag._Message = "No Fees are available... !!";
                    FillList(model);
                    Session["AdmissionFees_Model"] = model;
                    return View("AdmissionFees", model);
                }
                catch (Exception ex)
                {
                    ViewBag._Message = "There are issues at the time of Admission, admission fees Collection" + ex.ToString();
                    return View("AdmissionFees");
                }
            }
        }

        public ActionResult GetFeesDetails(string formNo, int? classId, int? CategoryId)
        {

            if (UserModel == null) return returnLogin(null);
            Session["AdmissionFees_Model"] = null;
            Session["AdmissionserviceChargeFees"] = null;
            var FeesList = service.GetGlobalSelect<FeesMaster_FEM>("FeesMaster_FEM", "FEM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            ViewBag.FeesHeadId = new SelectList(FeesList, "FEM_FEESID", "FEM_FEESNAME");
            ViewBag._Action = "add";
            ViewBag._ViewAction = "add";
            try
            {
                ModelState.Clear();
                AdmissionFeesModel model = new AdmissionFeesModel();
                FeesSearchBO SearchBO = new FeesSearchBO()
                {
                    ClassId = classId ?? 0,
                    SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0,
                    SessionId = UserModel.UM_SCM_SESSIONID ?? 0,
                    CategoryId = CategoryId,
                    FormNo = formNo,
                };
                model.listSearchDetails = new List<StudentList>();
                FillList(model);
                model.CM_CLASSID = classId;
                model.FormNo = formNo;
                //SearchBO.FormNo = formNo;
                SearchBO.AdmissionNo = null;
                FillFeesDetails(model, SearchBO);
                if (model.StudentFees == null || model.StudentFees.Count == 0) ViewBag._Message = "No Fees are available... !!";
                Session["AdmissionFees_Model"] = model;
                return View("AdmissionFees", model);
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator.";
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult AdmissionFeesCollection(AdmissionFeesModel model)
        {
            try
            {
                ViewBag._Action = "add";
                ViewBag._ViewAction = "add";
                FeesCollectionBO collection = new FeesCollectionBO()
                {
                    feesCollectionId = model.feesCollectionId ?? 0,
                    feesDate = model.FeesDate,
                    formNo = model.StudentInformation.formNo,
                    CM_CLASSID = model.StudentInformation.ClassId,
                    admissionNo = model.StudentInformation.AdmissionNo,
                    totalAmount = model.TotalAmount,
                    paidAmount = model.PaidAmount,
                    totalDue = model.TotalDue,
                    discount = model.Discount,
                    sessionId = UserModel.UM_SCM_SESSIONID ?? 0,
                    schoolId = UserModel.UM_SCM_SCHOOLID ?? 0,
                    userId = UserModel.UM_USERID,
                    paymodeType = model.PaymodeType.Trim(),
                    bankName = model.BankName,
                    branchName = model.BranchName,
                    cheqDDDate = model.CheqDDDate,
                    cheqDDNo = model.CheqDDNo,
                    discountedBy = model.DiscountedBy.Trim(),
                    //waveamount =model.WaveAmount,
                    Card_TrnsRefNo = string.IsNullOrWhiteSpace(model.Card_TrnsRefNo) == false ? model.Card_TrnsRefNo.Trim() : null

                };
                //foreach (var fees in model.StudentFees)
                //{
                //    if (fees.IsPaidChecked.Trim() == "true" || fees.IsDiscChecked.Trim() == "true" || fees.IsWaveChecked == "true")
                //    {
                //        FeesCollectionTransBO colltrn = new FeesCollectionTransBO()
                //        {
                //            FEESID = fees.FeesId,
                //            CLASSFEESID = fees.ClassFeesId,
                //            CLASSID = fees.ClassId,
                //            FEESAMNT = fees.FeesAmount,
                //            TOTALINSTLMNT = fees.NoOfFins,
                //            INSTALMENTNO = fees.InstallmentNo,
                //            DUEDATE = fees.DueDate,
                //            DUEAMT = fees.DueAmt,
                //            INSTALLMENTAMOUNT = fees.InsAmount,
                //            //WAVEAMOUNT = fees.WaveAmount,
                //        };
                //        // ---------------- PAID ----------------
                //        if (fees.IsPaidChecked != null && fees.IsPaidChecked.Trim() == "true")
                //        {
                //            colltrn.PAYMENTAMOUNT = fees.PaymentAmount ?? 0;
                //            colltrn.IsPaid = true;
                //        }
                //        else
                //        {
                //            colltrn.IsPaid = false;
                //        }

                //        // ---------------- DISCOUNT ----------------
                //        if (fees.IsDiscChecked != null && fees.IsDiscChecked.Trim() == "true")
                //        {
                //            colltrn.DISCOUNT = fees.AdustAmnt ?? 0;
                //            colltrn.IsDisc = true;
                //        }
                //        else
                //        {
                //            colltrn.DISCOUNT = 0;
                //            colltrn.IsDisc = false;
                //        }

                //        // ---------------- WAVE ----------------
                //        if (fees.IsWaveChecked != null && fees.IsWaveChecked.Trim() == "true")
                //        {
                //            colltrn.IsWave = true;
                //        }
                //        else
                //        {
                //            colltrn.IsWave = false;
                //        }


                //        collection.collectionsTrans.Add(colltrn);
                //    }
                //}


                foreach (var fees in model.StudentFees)
                {
                    if ((fees.IsPaidChecked != null && fees.IsPaidChecked.Trim() == "true") ||
                        (fees.IsDiscChecked != null && fees.IsDiscChecked.Trim() == "true") ||
                        (fees.IsWaveChecked != null && fees.IsWaveChecked.Trim() == "true"))
                    {
                        FeesCollectionTransBO colltrn = new FeesCollectionTransBO()
                        {
                            FEESID = fees.FeesId,
                            CLASSFEESID = fees.ClassFeesId,
                            CLASSID = fees.ClassId,
                            FEESAMNT = fees.FeesAmount,
                            TOTALINSTLMNT = fees.NoOfFins,
                            INSTALMENTNO = fees.InstallmentNo,
                            DUEDATE = fees.DueDate,
                            DUEAMT = fees.DueAmt,
                            INSTALLMENTAMOUNT = fees.InsAmount
                        };

                        // ---------------- WAVE FIRST (Priority) ----------------
                        if (fees.IsWaveChecked != null && fees.IsWaveChecked.Trim() == "true")
                        {
                            colltrn.PAYMENTAMOUNT = fees.InsAmount ?? 0;   //  InstallmentAmount goes to PaymentAmount
                            colltrn.IsWave = true;
                            colltrn.IsPaid = false;
                        }
                        else if (fees.IsPaidChecked != null && fees.IsPaidChecked.Trim() == "true")
                        {
                            // ---------------- PAID ----------------
                            colltrn.PAYMENTAMOUNT = fees.PaymentAmount ?? 0;
                            colltrn.IsPaid = true;
                            colltrn.IsWave = false;
                        }
                        else
                        {
                            colltrn.PAYMENTAMOUNT = 0;
                            colltrn.IsPaid = false;
                            colltrn.IsWave = false;
                        }

                        // ---------------- DISCOUNT ----------------
                        if (fees.IsDiscChecked != null && fees.IsDiscChecked.Trim() == "true")
                        {
                            colltrn.DISCOUNT = fees.AdustAmnt ?? 0;
                            colltrn.IsDisc = true;
                        }
                        else
                        {
                            colltrn.DISCOUNT = 0;
                            colltrn.IsDisc = false;
                        }

                        collection.collectionsTrans.Add(colltrn);
                    }
                }

                /* For Addition Fees Start */
                if (Session["AdmissionserviceChargeFees"] != null)
                {
                    studentFeesDetails serviceFeesModel = Session["AdmissionserviceChargeFees"] as studentFeesDetails;
                    FeesCollectionTransBO TransBO = new FeesCollectionTransBO();
                    FillAdditionalFeesForPosting(serviceFeesModel, TransBO);
                    collection.collectionsTrans.Add(TransBO);
                }
                /* For Addition Fees End */
                var type = "A";
                var msg = collection.feesCollectionId > 0 ? "updated" : "saved";
                var feesCollectionDBId = service.InsertUpdateFeesCollection(collection, type);
                if (feesCollectionDBId != 0)
                    TempData["_Message"] = "Form has been successfully " + msg + ".. ";
                TempData["ReceiptUrl"] = Url.Action("FeesCollectionListView", "FeesCollection",
                new { FeesType = "A", Id = feesCollectionDBId });

                return RedirectToAction("AdmissionFees");
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in Fees Collection..!! ";
                return View("AdmissionFees", reFillAutoList(model));
            }
        }
 
        public ActionResult FeesCollectionList()
        {
            if (UserModel == null) return returnLogin("~/FeesCollection/FeesCollection/FeesCollectionList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            StudentEnquery_ENQ list = new StudentEnquery_ENQ();
            return View(list);
        }
        #endregion AdmissionCollection

        #region SessionFeesCollection
        [HttpGet]
        public ActionResult SessionFees()
        {
            ViewBag._Action = "add";
            ViewBag._ViewAction = "add";
            Session["serviceChargeFees"] = null;
            Session["lateFees"] = null;

            if (UserModel == null) return returnLogin("~/FeesCollection/FeesCollection/SessionFees");
            
            try
            {
                SessionFeesModel Model = new SessionFeesModel();
                FillList(Model);
                Session["SessionFees_Model"] = Model;
                return View("SessionFees", Model);
            }
            catch { ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator."; return View("Error"); }
        }


        [HttpPost]
        public ActionResult SessionFees(SessionFeesModel model)
        {
            ViewBag._Action = "add";
            ViewBag._ViewAction = "add";
            try
            {
                ModelState.Clear();
                model.listSearchDetails = new List<StudentList>();
                model.StudentFees = new List<studentFeesDetails>();
                model.StudentInformation = new studentInfo();

                if (string.IsNullOrWhiteSpace(model.StudentID) == false) { model.StudentID = model.StudentID.Trim(); }
                if (string.IsNullOrWhiteSpace(model.RegistrationNo) == false) { model.RegistrationNo = model.RegistrationNo.Trim(); }

                if (model.CM_CLASSID == null && model.StudentID == null && model.RegistrationNo == null)
                {
                    ViewBag._Message = "please provide any input... !!";
                    FillList(model);
                    Session["SessionFees_Model"] = model;
                    return View("SessionFees", model);
                }
                Session["SessionFees_Model"] = null;
                FeesSearchBO SearchBO = new FeesSearchBO();
                SearchBO = mapToBO(model);
                if (SearchBO.ClassId != null && SearchBO.ClassId > 0 && SearchBO.StudentId == null && SearchBO.RegNo == null)
                {
                    model.listSearchDetails = service.FetchAdmittedStudent(SearchBO);
                    if (model.listSearchDetails != null && model.listSearchDetails.Count > 0)
                    {
                        int i = 0; foreach (var item in model.listSearchDetails) item.rownum = ++i;
                    }
                }
                else
                {
                    FillSessionFeesDetails(model, SearchBO);
                    if (model.StudentFees == null || model.StudentFees.Count == 0) ViewBag._Message = "No Fees are available... !!";
                }
                FillList(model);
                Session["SessionFees_Model"] = model;
                return View("SessionFees", model);
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator." + ex.ToString();
                return View("SessionFees_Model", reFillSessionModel(model));
            }
        }
        public ActionResult GetSessionFeesDetails(string StudentId, int? classId)
        {
            if (UserModel == null) return returnLogin(null);
            Session["SessionFees_Model"] = null;
            Session["serviceChargeFees"] = null;
            Session["lateFees"] = null;
            ViewBag._Action = "add";
            ViewBag._ViewAction = "add";
            try
            {
                ModelState.Clear();
                SessionFeesModel model = new SessionFeesModel();
                FeesSearchBO SearchBO = new FeesSearchBO()
                {
                    ClassId = classId ?? 0,
                    SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0,
                    SessionId = UserModel.UM_SCM_SESSIONID ?? 0,
                    StudentId = StudentId,
                };
                model.listSearchDetails = new List<StudentList>();
                //model.listSearchDetails = service.FetchAdmittedStudent(SearchBO);
                //if (model.listSearchDetails != null && model.listSearchDetails.Count > 0)
                //{
                //    int i = 0; foreach (var item in model.listSearchDetails) item.rownum = ++i;
                //}
                FillList(model);
                model.CM_CLASSID = classId;
                model.StudentID = StudentId;
                FillSessionFeesDetails(model, SearchBO);
                if (model.StudentFees == null || model.StudentFees.Count == 0) ViewBag._Message = "No Fees are available... !!";
                Session["SessionFees_Model"] = model;
                return View("SessionFees", model);
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator.";
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult SessionFeesCollection(SessionFeesModel model)
        {
            ViewBag._Action = "add";
            ViewBag._ViewAction = "add";

            try
            {
                if (model.StudentInformation == null || model.StudentInformation.ClassId <= 0)
                {
                    throw new Exception("Class is missing. Please reselect the student.");
                }

                int clsId = model.StudentInformation.ClassId;

                FeesCollectionBO collection = new FeesCollectionBO()
                {
                    feesCollectionId = model.feesCollectionId ?? 0,
                    feesDate = model.FeesDate,
                    formNo = model.StudentInformation.formNo,

                    CM_CLASSID = clsId,

                    admissionNo = model.StudentInformation.AdmissionNo,
                    totalAmount = model.TotalAmount,
                    paidAmount = model.PaidAmount,
                    totalDue = model.TotalDue,

                    // Late fees
                    totallatefees = model.TotalLateFees,
                    latefeesdiscount = model.LateFeesDiscount,
                    totalpaidlatefees = model.TotalPaidLateFees,
                    totalpaidamount = model.TotalPaidAmount,

                    discount = model.Discount,
                    sessionId = UserModel.UM_SCM_SESSIONID ?? 0,
                    schoolId = UserModel.UM_SCM_SCHOOLID ?? 0,
                    userId = UserModel.UM_USERID,

                    paymodeType = model.PaymodeType != null ? model.PaymodeType.Trim() : null,
                    bankName = model.BankName,
                    branchName = model.BranchName,
                    cheqDDDate = model.CheqDDDate,
                    cheqDDNo = model.CheqDDNo,
                    discountedBy = model.DiscountedBy != null ? model.DiscountedBy.Trim() : null,

                    studentId = model.StudentInformation.StudentId,
                    studenRegistrationId = model.StudentInformation.RegNo,

                    Card_TrnsRefNo = string.IsNullOrWhiteSpace(model.Card_TrnsRefNo)
                                        ? null
                                        : model.Card_TrnsRefNo.Trim()
                };

                foreach (var fees in model.StudentFees)
                {
                    if (fees.IsPaidChecked != null && fees.IsPaidChecked.Trim() == "true")
                    {
                        FeesCollectionTransBO colltrn = new FeesCollectionTransBO()
                        {
                            FEESID = fees.FeesId,
                            CLASSFEESID = fees.ClassFeesId,
                            CLASSID = fees.ClassId,
                            FEESAMNT = fees.FeesAmount,
                            TOTALINSTLMNT = fees.NoOfFins,
                            INSTALMENTNO = fees.InstallmentNo,
                            DUEDATE = fees.DueDate,
                            DUEAMT = fees.DueAmt,
                            INSTALLMENTAMOUNT = fees.InsAmount,

                            // Late fees
                            LATEFEES = fees.LateFees,
                            LATEDISCOUNT = fees.LateDiscountAmt,
                            IsLateDisc = (fees.IsLateDiscApplied != null &&
                                          fees.IsLateDiscApplied.Trim().ToLower() == "true")
                        };

                        // Payment
                        colltrn.IsPaid = true;
                        colltrn.PAYMENTAMOUNT = fees.PaymentAmount ?? 0;

                        // Discount
                        if (fees.IsDiscChecked != null && fees.IsDiscChecked.Trim() == "true")
                        {
                            colltrn.IsDisc = true;
                            colltrn.DISCOUNT = fees.AdustAmnt ?? 0;
                        }
                        else
                        {
                            colltrn.IsDisc = false;
                            colltrn.DISCOUNT = 0;
                        }

                        collection.collectionsTrans.Add(colltrn);
                    }
                }

                if (Session["serviceChargeFees"] != null)
                {
                    studentFeesDetails serviceFeesModel =
                        Session["serviceChargeFees"] as studentFeesDetails;

                    FeesCollectionTransBO transBO = new FeesCollectionTransBO();
                    FillAdditionalFeesForPosting(serviceFeesModel, transBO);
                    collection.collectionsTrans.Add(transBO);
                }

                if (Session["lateFees"] != null)
                {
                    studentFeesDetails lateFeesModel =
                        Session["lateFees"] as studentFeesDetails;

                    FeesCollectionTransBO transBO = new FeesCollectionTransBO();
                    FillAdditionalFeesForPosting(lateFeesModel, transBO);
                    collection.collectionsTrans.Add(transBO);
                }

                var type = "C";
                var msg = collection.feesCollectionId > 0 ? "updated" : "saved";

                var feesCollectionDBId =
                    service.InsertUpdateFeesCollection(collection, type);

                if (feesCollectionDBId != 0)
                {
                    TempData["_Message"] = "Form has been successfully " + msg + ".. ";
                }

                TempData["ReceiptUrl"] = Url.Action(
                    "SeesionFeesCollectionListView",
                    "FeesCollection",
                    new { FeesType = "A", Id = feesCollectionDBId });

                return RedirectToAction("SessionFees");
            }
            catch (Exception)
            {
                ViewBag._Message = "There are issues in Fees Collection..!! ";
                return View("SessionFees", reFillSessionModel(model));
            }
        }



        public ActionResult FeesSessionCollectionList()
        {
            if (UserModel == null) return returnLogin("~/FeesCollection/FeesCollection/FeesSessionCollectionList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            StudentEnquery_ENQ list = new StudentEnquery_ENQ();
            return View(list);
        }

        #endregion SessionFeesCollection

        #region Others Collection 
        [HttpGet]
        public void AddServiceCharge(string FeesType, decimal amount)
        {
            try
            {
                if (FeesType == "A")
                {
                    Session["AdmissionserviceChargeFees"] = null;
                    if (amount > 0)
                    {
                        studentFeesDetails serviceChargeFees = new studentFeesDetails();
                        FillAdditionalFees(serviceChargeFees, additionServiceChargeId, amount);
                        Session["AdmissionserviceChargeFees"] = serviceChargeFees;
                    }
                }
                if (FeesType == "C")
                {
                    Session["serviceChargeFees"] = null;
                    if (amount > 0)
                    {
                        studentFeesDetails serviceChargeFees = new studentFeesDetails();
                        FillAdditionalFees(serviceChargeFees, additionServiceChargeId, amount);
                        Session["serviceChargeFees"] = serviceChargeFees;
                    }
                }
            }
            catch (Exception ex) { }
        }

        [HttpGet]
        public void AddLateFeesCharge(decimal amount)
        {
            try
            {
                Session["lateFees"] = null;
                if (amount > 0)
                {
                    studentFeesDetails lateFees = new studentFeesDetails();
                    FillAdditionalFees(lateFees, additionLateFeesId, amount);
                    Session["lateFees"] = lateFees;
                }
            }
            catch (Exception ex) { }
        }
        [HttpGet]
        public JsonResult FetchServiceCharge(string FeesType)
        {
            studentFeesDetails serviceFeesModel = new studentFeesDetails();
            if (FeesType == "A")
            {
                if (Session["AdmissionserviceChargeFees"] != null)
                {
                    serviceFeesModel = Session["AdmissionserviceChargeFees"] as studentFeesDetails;
                    return Json(serviceFeesModel, JsonRequestBehavior.AllowGet);
                }
            }
            if (FeesType == "C")
            {

                if (Session["serviceChargeFees"] != null)
                {
                    serviceFeesModel = Session["serviceChargeFees"] as studentFeesDetails;
                    return Json(serviceFeesModel, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(serviceFeesModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FetchLateFeesCharge()
        {
            studentFeesDetails lateFees = new studentFeesDetails();
            if (Session["lateFees"] != null)
            {
                lateFees = Session["lateFees"] as studentFeesDetails;
                return Json(lateFees, JsonRequestBehavior.AllowGet);
            }
            return Json(lateFees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FindLateFeesAmount(DateTime dueDate, DateTime collectionDate) //int fineTypeId,
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;

            LateFeesSlap lateSlap = service.FindLateFeesAmount(schoolId, sessionId, dueDate, collectionDate); // fineTypeId
            return Json(lateSlap, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult update(string FeesType, long Id)
        {
            ViewBag._Action = "update";
            ViewBag._ViewAction = "update";
            Session["serviceChargeFees"] = null;
            Session["AdmissionserviceChargeFees"] = null;
            Session["lateFees"] = null;
            try
            {
                FormBO formBO = service.getAsyncFeesUpdate(Id);
                if (FeesType == "A")
                {
                    AdmissionFeesModel model = new AdmissionFeesModel();
                    FillFormDetails(formBO, model, FeesType);
                    model.feesCollectionId = Id;
                    Session["AdmissionFees_Model"] = model;
                    return View("AdmissionFees", model);
                }
                else
                {
                    SessionFeesModel model = new SessionFeesModel();
                    FillFormDetails(formBO, model, FeesType);
                    model.feesCollectionId = Id;
                    Session["SessionFees_Model"] = model;
                    return View("SessionFees", model);
                }
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator.";
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult FeesCollectionListView(string FeesType, long Id)
        {
            try
            {
                FormBO formBO = service.getAsyncFeesUpdate(Id);
                if (FeesType == "A")
                {
                    AdmissionFeesModel model = new AdmissionFeesModel();
                    SchoolMasters_SCM SchoolMasters_SCM = new SchoolMasters_SCM();
                    StudetDetails_SD StudetDetails_SD = new StudetDetails_SD();
                    SchoolMasters_SCM = service.GetGlobalSelectOne<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                    ViewBag.SchoolName = SchoolMasters_SCM.SCM_SCHOOLNAME;
                    ViewBag.SchoolAddress = SchoolMasters_SCM.SCM_SCHOOLADDRESS1;
                    FillFormDetails(formBO, model, FeesType);
                    model.feesCollectionId = Id;
                    Session["AdmissionFees_Model"] = model;
                    return View(model);
                }
                else
                {
                    SessionFeesModel model = new SessionFeesModel();
                    FillFormDetails(formBO, model, FeesType);
                    model.feesCollectionId = Id;
                    Session["SessionFees_Model"] = model;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator.";
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult SeesionFeesCollectionListView(string FeesType, long Id)
        {
            try
            {
                FormBO formBO = service.getAsyncFeesUpdate(Id);
                if (FeesType == "A")
                {
                    AdmissionFeesModel model = new AdmissionFeesModel();
                    SchoolMasters_SCM SchoolMasters_SCM = new SchoolMasters_SCM();
                    StudetDetails_SD StudetDetails_SD = new StudetDetails_SD();
                    SchoolMasters_SCM = service.GetGlobalSelectOne<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                    ViewBag.SchoolName = SchoolMasters_SCM.SCM_SCHOOLNAME;
                    ViewBag.SchoolAddress = SchoolMasters_SCM.SCM_SCHOOLADDRESS1;
                    FillFormDetails(formBO, model, FeesType);
                    model.feesCollectionId = Id;
                    Session["AdmissionFees_Model"] = model;
                    return View(model);
                }
                else
                {
                    SessionFeesModel model = new SessionFeesModel();
                    FillFormDetails(formBO, model, FeesType);
                    model.feesCollectionId = Id;
                    Session["SessionFees_Model"] = model;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator.";
                return View("Error");
            }
        }
#endregion
        #region MethodFees
        private void FillList(dynamic model)
        {
            model.SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            model.SessionId = UserModel.UM_SCM_SESSIONID ?? 0;

            // Already a List<ClassMaster_CM>, no need to call ToList()
            List<ClassMaster_CM> classList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", model.SchoolId);

            var sortedClassList = classList
                .Where(c => !string.IsNullOrWhiteSpace(c.CM_CLASSNAME))
                .OrderBy(c => c.CM_FROMAGE.ToString().Length)
                .ThenBy(c => c.CM_FROMAGE)
                .ToList();

            model.ListClass = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
        }
        private FeesSearchBO mapToBO(dynamic admissionSearch, string Type = "C")
        {
            FeesSearchBO objBO = new FeesSearchBO()
            {
                ClassId = admissionSearch.CM_CLASSID ?? 0,
                SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0,
                SessionId = UserModel.UM_SCM_SESSIONID ?? 0,
            };
            if (Type == "A")
            {
                objBO.AdmissionNo = admissionSearch.AdmissionNo;
                objBO.FormNo = admissionSearch.FormNo;
            }
            else
            {
                objBO.StudentId = admissionSearch.StudentID;
                objBO.RegNo = admissionSearch.RegistrationNo;
            }
            return objBO;
        }
        private void FillFeesDetails(AdmissionFeesModel model, FeesSearchBO SearchBO)
        {
            model.StudentFees = service.getStudentFeesDetails(SearchBO);
            getStudentInformation(model, SearchBO);
        }
        private void getStudentInformation(AdmissionFeesModel model, FeesSearchBO SearchBO)
        {
            if (model.StudentFees != null && model.StudentFees.Count > 0)
            {
                model.FeesDate = DateTime.Now;
                model.CheqDDDate = DateTime.Now;
                var students = service.SearchNonAdmittedStudent(SearchBO).FirstOrDefault();
                if (students != null)
                {
                    studentInfo info = new studentInfo()
                    {
                        AdmissionNo = students.SD_AppliactionNo,
                        ClassName = students.CM_CLASSNAME,
                        ClassId = students.ClassId,
                        formNo = students.SD_FormNo,
                        StudentName = students.SD_StudentName,
                        SD_StudentCategoryId = students.SD_StudentCategoryId,
                        StudentId = students.SD_StudentId,
                        RegNo = students.SD_RegNo,
                    };
                    model.StudentInformation = info;
                }
                model.TotalAmount = model.StudentFees.Sum(s => s.InsAmount);
                model.TotalAmount = Math.Round(model.TotalAmount ?? 0, 2);

                model.PaidAmount = model.StudentFees.Sum(s => s.PaymentAmount ?? 0);
                model.PaidAmount = Math.Round(model.PaidAmount ?? 0, 2);

                model.Discount = model.StudentFees.Sum(s => s.AdustAmnt ?? 0);
                model.Discount = Math.Round(model.Discount ?? 0, 2);

                model.TotalDue = model.StudentFees.Sum(s => s.DueAmt ?? 0);
                model.TotalDue = Math.Round(model.TotalDue ?? 0, 2);
            }
        }
        private AdmissionFeesModel reFillAutoList(AdmissionFeesModel model)
        {
            AdmissionFeesModel oldModel = Session["AdmissionFees_Model"] as AdmissionFeesModel;
            model.SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            model.SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            var query = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null);
            var results = query.Where(r => r.CM_SCM_SCHOOLID == model.SchoolId).OrderBy(r => r.CM_CLASSNAME).ToList();
            model.ListClass = new SelectList(results, "CM_CLASSID", "CM_CLASSNAME");
            return model;
        }
        private void FillFormDetails(FormBO formBO, dynamic model, string FeesType)
        {
            model.TotalAmount = formBO.TotalAmount;
            model.PaidAmount = formBO.PaidAmount;
            model.Discount = formBO.Discount;
            model.TotalDue = formBO.TotalDue;
            model.DiscountedBy = formBO.DiscountedBy;
            model.FeesDate = formBO.FeesDate;
            model.BankName = formBO.BankName;
            model.BranchName = formBO.BranchName;
            model.CheqDDNo = formBO.CheqDDNo;
            model.CheqDDDate = formBO.CheqDDDate;
            model.Card_TrnsRefNo = formBO.Card_RefNo;
            model.PaymodeType = formBO.PaymodeType.Trim();
            model.RECIPTNO = formBO.RECIPTNO;

            if (model.PaymodeType == "Cash") model.payCash = true; else model.payCash = false;
            if (model.PaymodeType == "Cheque") model.payCheque = true; else model.payCheque = false;
            if (model.PaymodeType == "DD") model.payDD = true; else model.payDD = false;
            if (model.PaymodeType == "Card") model.payCard = true; else model.payCard = false;
            model.StudentInformation.AdmissionNo = formBO.StudentInformation.AdmissionNo;
            model.StudentInformation.ClassId = formBO.StudentInformation.ClassId;
            model.StudentInformation.ClassName = formBO.StudentInformation.ClassName;
            model.StudentInformation.StudentId = formBO.StudentInformation.StudentId;
            model.StudentInformation.formNo = formBO.StudentInformation.formNo;
            model.StudentInformation.SD_StudentCategoryId = formBO.StudentInformation.SD_StudentCategoryId;
            model.StudentInformation.StudentName = formBO.StudentInformation.StudentName;
            model.StudentInformation.SECM_SECTIONNAME = formBO.StudentInformation.SECM_SECTIONNAME;
            model.StudentInformation.SD_CurrentRoll = formBO.StudentInformation.SD_CurrentRoll;
            model.StudentInformation.SD_FatherName = formBO.StudentInformation.SD_FatherName;

            if (FeesType == "C")
            {
                model.StudentInformation.StudentId = formBO.StudentInformation.StudentId;
                model.StudentInformation.RegNo = formBO.StudentInformation.RegNo;
            }
            foreach (var fees in formBO.StudentFees)
            {
                if (fees.FeesId == additionServiceChargeId)
                {
                    studentFeesDetails serviceChargeFees = FillStudentFeesSettingForEditing(fees);
                    if (FeesType == "C") Session["serviceChargeFees"] = serviceChargeFees;
                    else Session["AdmissionserviceChargeFees"] = serviceChargeFees;
                }
                else if (fees.FeesId == additionLateFeesId)
                {
                    studentFeesDetails lateFees = FillStudentFeesSettingForEditing(fees);
                    Session["lateFees"] = lateFees;
                }
                else
                {
                    studentFeesDetails stdFees = FillStudentFeesSettingForEditing(fees);
                    model.StudentFees.Add(stdFees);
                }
            }
        }
        private SessionFeesModel reFillSessionModel(SessionFeesModel model)
        {
            SessionFeesModel oldModel = Session["SessionFees_Model"] as SessionFeesModel;
            model.SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            model.SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            var query = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null);
            var results = query.Where(r => r.CM_SCM_SCHOOLID == model.SchoolId).OrderBy(r => r.CM_CLASSNAME).ToList();
            model.ListClass = new SelectList(results, "CM_CLASSID", "CM_CLASSNAME");
            return model;
        }
        private void FillSessionFeesDetails(SessionFeesModel model, FeesSearchBO SearchBO)
        {
            List<studentFeesDetails> searchFees = new List<studentFeesDetails>();
            var sessionNewFeesInstall = service.getStudentSessionFeesDetails(SearchBO).ToList();
            var sessionNewFeesInstallGroups = sessionNewFeesInstall.GroupBy(x => new { x.InstallmentNo, x.FeesId }).ToList();
            foreach (var group in sessionNewFeesInstallGroups)
            {
                searchFees.Add(new studentFeesDetails()
                {
                    FeesName = group.FirstOrDefault().FeesName,
                    FeesId = group.FirstOrDefault().FeesId,
                    NoOfInstallment = group.FirstOrDefault().NoOfInstallment,
                    FeesAmount = group.FirstOrDefault().FeesAmount,
                    NoOfFins = group.FirstOrDefault().NoOfFins,
                    InstallmentNo = group.FirstOrDefault().InstallmentNo,
                    DueDate = group.FirstOrDefault().DueDate,
                    InsAmount = group.FirstOrDefault().InsAmount,
                    PaymentAmount = group.FirstOrDefault().PaymentAmount,
                    AdustAmnt = group.FirstOrDefault().AdustAmnt,
                    DueAmt = group.FirstOrDefault().DueAmt,
                    ClassId = group.FirstOrDefault().ClassId,
                    ClassFeesId = group.FirstOrDefault().ClassFeesId,
                    IsPaid = group.FirstOrDefault().IsPaid,
                    IsDisc = group.FirstOrDefault().IsDisc,
                    IsPaidChecked = group.FirstOrDefault().IsPaidChecked,
                    IsDiscChecked = group.FirstOrDefault().IsDiscChecked,
                    Payable = group.FirstOrDefault().Payable,
                });
            }
            model.StudentFees = searchFees.OrderBy(r => r.InstallmentNo).ToList();
            getSessionStudentInformation(model, SearchBO);
        }
        private void getSessionStudentInformation(SessionFeesModel model, FeesSearchBO SearchBO)
        {
            if (model.StudentFees != null && model.StudentFees.Count > 0)
            {
                model.FeesDate = DateTime.Now;
                model.CheqDDDate = DateTime.Now;
                var students = service.FetchAdmittedStudent(SearchBO).FirstOrDefault();
                if (students != null)
                {
                    studentInfo info = new studentInfo()
                    {
                        AdmissionNo = students.SD_AppliactionNo,
                        ClassName = students.CM_CLASSNAME,
                        ClassId = students.ClassId,
                        formNo = students.SD_FormNo,
                        StudentName = students.SD_StudentName,
                        SD_StudentCategoryId = students.SD_StudentCategoryId,
                        StudentId = students.SD_StudentId,
                        RegNo = students.SD_RegNo,

                    };
                    model.StudentInformation = info;
                    model.CM_CLASSID = students.ClassId;
                }
                model.TotalAmount = model.StudentFees.Sum(s => s.Payable);
                model.TotalAmount = Math.Round(model.TotalAmount ?? 0, 2);

                model.PaidAmount = model.StudentFees.Sum(s => s.PaymentAmount ?? 0);
                model.PaidAmount = Math.Round(model.PaidAmount ?? 0, 2);

                model.Discount = model.StudentFees.Sum(s => s.AdustAmnt ?? 0);
                model.Discount = Math.Round(model.Discount ?? 0, 2);

                model.TotalDue = model.StudentFees.Sum(s => s.DueAmt ?? 0);
                model.TotalDue = Math.Round(model.TotalDue ?? 0, 2);
            }
        }
        private void FillAdditionalFeesForPosting(studentFeesDetails additionalFees, FeesCollectionTransBO TransBO)
        {
            TransBO.FEESID = additionalFees.FeesId;
            TransBO.CLASSFEESID = additionalFees.ClassFeesId;
            TransBO.CLASSID = additionalFees.ClassId;
            TransBO.FEESAMNT = additionalFees.FeesAmount;
            TransBO.TOTALINSTLMNT = additionalFees.NoOfFins;
            TransBO.INSTALMENTNO = additionalFees.InstallmentNo;
            TransBO.DUEDATE = additionalFees.DueDate;
            TransBO.DUEAMT = additionalFees.DueAmt;
            TransBO.INSTALLMENTAMOUNT = additionalFees.InsAmount;
            TransBO.PAYMENTAMOUNT = additionalFees.PaymentAmount;
            TransBO.IsPaid = additionalFees.IsPaid;
            TransBO.DISCOUNT = additionalFees.AdustAmnt;
            TransBO.IsDisc = additionalFees.IsDisc;
        }
        private void FillAdditionalFees(studentFeesDetails additionalFees, int staticFessId, decimal amount)
        {

            additionalFees.FeesId = staticFessId;
            additionalFees.ClassFeesId = 0;
            additionalFees.ClassId = 0;
            additionalFees.FeesAmount = 0;
            additionalFees.NoOfFins = 0;
            additionalFees.InstallmentNo = 0;
            additionalFees.DueDate = DateTime.Now.ToString("dd/MM/yyyy");
            additionalFees.DueAmt = 0;
            additionalFees.InsAmount = 0;
            additionalFees.PaymentAmount = amount;
            additionalFees.IsPaid = true;
            additionalFees.AdustAmnt = 0;
            additionalFees.IsDisc = false;
        }
        private studentFeesDetails FillStudentFeesSettingForEditing(studentFeesDetails fees)
        {
            studentFeesDetails stdFees = new studentFeesDetails()
            {
                AdustAmnt = fees.AdustAmnt,
                ClassFeesId = fees.ClassFeesId,
                ClassId = fees.ClassId,
                DueAmt = fees.DueAmt,
                DueDate = fees.DueDate,
                FeesAmount = fees.FeesAmount,
                FeesId = fees.FeesId,
                FeesName = fees.FeesName,
                InsAmount = fees.InsAmount,
                InstallmentNo = fees.InstallmentNo,
                IsDisc = fees.IsDisc,
                IsPaid = fees.IsPaid,
                NoOfFins = fees.NoOfFins,
                NoOfInstallment = fees.NoOfInstallment,
                PaymentAmount = fees.PaymentAmount,
                IsDiscChecked = fees.IsDiscChecked,
                IsPaidChecked = fees.IsPaidChecked,
                Payable = fees.Payable,
            };
            return stdFees;
        }

        [HttpGet]
        public JsonResult GetPreviousPayments(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                return Json(new { success = false, message = "StudentId missing" }, JsonRequestBehavior.AllowGet);

            var payments = new List<object>();
            try
            {
                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_GetStudentPaymentDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        con.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                payments.Add(new
                                {
                                    PaymentDate = Convert.ToDateTime(rdr["PaymentDate"]).ToString("dd/MM/yyyy"),
                                    INSTALMENTNO = rdr["INSTALMENTNO"].ToString(),
                                    FEM_FEESNAME = rdr["FEM_FEESNAME"].ToString(),
                                    INSTALMENTAMOUNT = Convert.ToDecimal(rdr["INSTALMENTAMOUNT"]),
                                    PYMENTAMOUNT = Convert.ToDecimal(rdr["PYMENTAMOUNT"]),
                                    Discount = rdr["Discount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["Discount"]),
                                    DueAmount = rdr["DueAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["DueAmount"]),
                                    PAYMODE = rdr["PAYMODE"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, data = payments }, JsonRequestBehavior.AllowGet);
        }

        #endregion MethodFees

     
        #region Miscellaneous Fees Collection

        public ActionResult MiscCollection(long? id, int? MTM_FeesHeadId)
        {
            if (UserModel == null)
                return returnLogin("~/FeesCollection/FeesCollection/MiscCollection");

            if (Session["CanAdd"] == null || (bool)Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("MiscCollectionList", "FeesCollection");
            }
             MiscellaneousTransactionMaster misc = new MiscellaneousTransactionMaster();
            var SchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
            var SessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);
            var BankList = service.GetGlobalSelect<BankMaster_BM>("BankMaster_BM", "SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.MFD_BankId = new SelectList(BankList ?? new List<BankMaster_BM>(), "BankId", "BankName", misc.MFD_BankId);
            var FeesHeadList = service.GetGlobalSelect<FeesMaster_FEM>("FeesMaster_FEM", "FEM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            ViewBag.MTM_FeesHeadId = new SelectList(FeesHeadList ?? new List<FeesMaster_FEM>(), "FEM_FEESID", "FEM_FEESNAME", MTM_FeesHeadId);
           

            if (id.HasValue)
            {
                misc = service.GetGlobalSelectOne<MiscellaneousTransactionMaster>(
                    "MiscellaneousTransactionMaster_MTM", "MTM_Id", id.Value);
            }

            return View(misc);
        }

        public ActionResult MiscCollectionList()
        {
            if (UserModel == null) return returnLogin("~/FeesCollection/FeesCollection/MiscCollectionList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            MiscellaneousTransactionMaster mlist = new MiscellaneousTransactionMaster();
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View(mlist);
        }


        [HttpGet]
        public ActionResult MiscFeesCollectionListView(string MTM__TransId)
        {
            try
            {
                MiscellaneousTransactionMaster model = new MiscellaneousTransactionMaster();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SP_InsertUpdate_MiscCollection", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransType", "PrintReceipt");
                    cmd.Parameters.AddWithValue("@MTM__TransId", MTM__TransId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        model.MTM_Id = rdr["MTM_Id"] == DBNull.Value ? 0L : Convert.ToInt64(rdr["MTM_Id"]);
                        model.MTM__TransId = rdr["MTM__TransId"] == DBNull.Value ? string.Empty : rdr["MTM__TransId"].ToString();
                        model.MTM_StudentId = rdr["MTM_StudentId"] == DBNull.Value ? string.Empty : rdr["MTM_StudentId"].ToString();
                        model.MFD_Paymentmode = rdr["MFD_Paymentmode"] == DBNull.Value ? string.Empty : rdr["MFD_Paymentmode"].ToString();
                        model.BankName = rdr["BankName"] == DBNull.Value ? string.Empty : rdr["BankName"].ToString();
                        model.MFD_BranchName = rdr["MFD_BranchName"] == DBNull.Value ? string.Empty : rdr["MFD_BranchName"].ToString();
                        model.MFD_CheqDDNo = rdr["MFD_CheqDDNo"] == DBNull.Value ? string.Empty : rdr["MFD_CheqDDNo"].ToString();
                        model.MFD_Card_TrnsRefNo = rdr["MFD_Card_TrnsRefNo"] == DBNull.Value ? string.Empty : rdr["MFD_Card_TrnsRefNo"].ToString();
                        model.MTM_Amount = rdr["MTM_Amount"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["MTM_Amount"]);
                        model.MFD_PaidAmount = rdr["MFD_PaidAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["MFD_PaidAmount"]);
                        model.MFD_CheqDDDate = rdr["MFD_CheqDDDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["MFD_CheqDDDate"]);
                        model.MFD__FeesCollectionDate = rdr["MFD__FeesCollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["MFD__FeesCollectionDate"]);
                        model.MTM_FeesName = rdr["MTM_FeesName"] == DBNull.Value ? string.Empty : rdr["MTM_FeesName"].ToString();
                        model.SCM_SCHOOLNAME = rdr["SCM_SCHOOLNAME"] == DBNull.Value ? string.Empty : rdr["SCM_SCHOOLNAME"].ToString();
                        model.SCM_FULLNAME = rdr["SCM_FULLNAME"] == DBNull.Value ? string.Empty : rdr["SCM_FULLNAME"].ToString();
                        model.SCM_SCHOOLADDRESS1 = rdr["SCM_SCHOOLADDRESS1"] == DBNull.Value ? string.Empty : rdr["SCM_SCHOOLADDRESS1"].ToString();
                        model.SM_SESSIONNAME = rdr["SM_SESSIONNAME"] == DBNull.Value ? string.Empty : rdr["SM_SESSIONNAME"].ToString();
                        model.SD_StudentName = rdr["SD_StudentName"] == DBNull.Value ? string.Empty : rdr["SD_StudentName"].ToString();
                        model.MTM_ClassName = rdr["MTM_ClassName"] == DBNull.Value ? string.Empty : rdr["MTM_ClassName"].ToString();
                        model.MTM_Narration = rdr["MTM_Narration"] == DBNull.Value ? string.Empty : rdr["MTM_Narration"].ToString();
                        model.SECM_SECTIONNAME = rdr["SECM_SECTIONNAME"] == DBNull.Value ? string.Empty : rdr["SECM_SECTIONNAME"].ToString();
                    }
                }

                Session["HostelFees_Model"] = model;

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag._Message = "Error loading Hostel Fees Receipt: " + ex.Message;
                return View("Error");
            }
        }

        #endregion
    }
}