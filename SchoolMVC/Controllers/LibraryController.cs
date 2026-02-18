using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolMVC.Models;

namespace SchoolMVC.Controllers
{
    public class LibraryController : BaseController
    {
        // GET: Masters
        public ActionResult Index()
        {
            return View();
        }

        #region Book Master
        public ActionResult BookMaster(long? id)
        {
            try
            {

                if (UserModel == null) return returnLogin("~/Library/BookMaster");
                if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
                {
                    TempData["_Message"] = "You are not authorized..!!";
                    return RedirectToAction("BookMasterList", "Masters");
                }
                BookMaster_BM data = new BookMaster_BM();
                Int64 editId = 0;
                if (id != null)
                {
                    editId = (Int64)id;
                    data = service.GetGlobalSelectOne<BookMaster_BM>("BookMaster_BM", "BM_BookId", editId);
                }
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["_Message"] = "Something went wrong. Please try again.";
                return RedirectToAction("BookMasterList", "Masters");
            }
        }
        public ActionResult BookMasterList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            BookMaster_BM list = new BookMaster_BM();
            return View(list);
        }
        #endregion

        #region Book Copy Master
        public ActionResult BookCopyMaster(long? id)
        {
           try
            {
                if (UserModel == null) return returnLogin("~/Library/BookCopyMasterList");
                if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
                {
                    TempData["_Message"] = "You are not authorized..!!";
                    return RedirectToAction("BookCopyMasterList", "Library");
                }
                BookCopyMaster_BCM data = new BookCopyMaster_BCM();
                Int64 editId = 0;
                if (id == null)
                {
                    //var booklist = service.GetGlobalSelect<BookMaster_BM>("BookMaster_BM", "BM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                    //ViewBag.BCM_BookId = new SelectList(booklist, "BM_BookId", "BM_Title");

                    var booklist = service.GetGlobalSelect<BookMaster_BM>("BookMaster_BM", "BM_BookId", null);
                    ViewBag.BCM_BookId = new SelectList(booklist, "BM_BookId", "BM_Title");
                }
                else
                {
                    editId = (Int64)id;

                    data = service.GetGlobalSelectOne<BookCopyMaster_BCM>("BookCopyMaster_BCM", "BCM_CopyId", editId);

                    var booklist = service.GetGlobalSelect<BookMaster_BM>("BookMaster_BM", "BM_BookId", UserModel.UM_SCM_SCHOOLID);

                    ViewBag.BCM_BookId = new SelectList(booklist, "BM_BookId", "BM_Title", data.BCM_BookId);
                }
                return View(data);
            }
           catch (Exception ex)
           {
               TempData["_Message"] = "Something went wrong. Please try again.";
               return RedirectToAction("BookMasterList", "Masters");
           }
        }
        public ActionResult BookCopyMasterList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            BookCopyMaster_BCM list = new BookCopyMaster_BCM();
            return View(list);
        }
        #endregion

        #region Library Setting
        public ActionResult LibrarySettingMasters(int? id)
        {
            if (UserModel == null) return returnLogin("~/Library/LibrarySettingMaster");
            LibrarySetting setting = new LibrarySetting();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                setting = service.GetGlobalSelectOne<LibrarySetting>("LibrarySetting_LS", "LS_SettingId", editId);
            }
            return View(setting);
        }

        [HttpPost]
        public JsonResult LibrarySettingMasterList()
        {
            LibrarySetting setting = new LibrarySetting();
            service.LibrarySettingMasterList(setting);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region Category Master

        public ActionResult CategoryMasters(int? id)
        {
            if (UserModel == null) return returnLogin("~/Library/CategoryMasters");
            CategoryMaster category = new CategoryMaster();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                category = service.GetGlobalSelectOne<CategoryMaster>("CategoryMaster_CM", "CM_CategoryId", editId);
            }
            return View(category);
        }

        [HttpPost]
        public JsonResult CategoryMasterList()
        {
            CategoryMaster category = new CategoryMaster();
            service.CategoryMasterList(category);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region Supplier Master
        public ActionResult SupplierMasters(int? id)
        {
            if (UserModel == null) return returnLogin("~/Library/SupplierMasters");
            SupplierMaster supplier = new SupplierMaster();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                supplier = service.GetGlobalSelectOne<SupplierMaster>("SupplierMaster_SM", "SM_SupplierId", editId);
            }
            return View(supplier);
        }

        [HttpPost]
        public JsonResult SupplierMasterList()
        {
            SupplierMaster supplier = new SupplierMaster();
            service.SupplierMasterList(supplier);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region Member Master
        public ActionResult MemberMaster(long? id)
        {
            try
            {
                if (UserModel == null) return returnLogin("~/Library/MemberMaster");
                if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
                {
                    TempData["_Message"] = "You are not authorized..!!";
                    return RedirectToAction("MemberMasterList", "Library");
                }
                MemberMaster data = new MemberMaster();
                Int64 editId = 0;
                if (id == null)
                {

                    var typelist = service.GetGlobalSelect<MemberTypeMaster_MTM>("MemberTypeMaster_MTM", "MTM_TypeId", null);
                    ViewBag.MM_MemberTypeId = new SelectList(typelist, "MTM_TypeId", "MTM_Type");
                }
                else
                {
                    editId = (Int64)id;

                    data = service.GetGlobalSelectOne<MemberMaster>("MemberMaster_MM", "MM_MemberId", editId);

                    var typelist = service.GetGlobalSelect<MemberTypeMaster_MTM>("MemberTypeMaster_MTM", "MTM_TypeId", null);

                    ViewBag.MM_MemberTypeId = new SelectList(typelist, "MTM_TypeId", "MTM_Type", data.MM_MemberTypeId);
                }
                return View(data); 
            }
            catch (Exception ex)
            {
                TempData["_Message"] = "Something went wrong. Please try again.";
                return RedirectToAction("MemberMasterList", "Library");
            }
        }
        public ActionResult MemberMasterList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            MemberMaster list = new MemberMaster();
            return View(list);
        }
        #endregion

        #region Issue Transaction
        //public ActionResult IssueTransaction(long? id)
        //{
        //    try
        //    {
        //        if (UserModel == null) return returnLogin("~/Library/IssueTransactionList");
        //        if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
        //        {
        //            TempData["_Message"] = "You are not authorized..!!";
        //            return RedirectToAction("IssueTransactionList", "Library");
        //        }
        //        IssueTransaction data = new IssueTransaction();
        //        Int64 editId = 0;
        //        if (id == null)
        //        {
        //            var typelist = service.GetGlobalSelect<MemberTypeMaster_MTM>("MemberTypeMaster_MTM", "MTM_TypeId", null);
        //            ViewBag.MTM_TypeId = new SelectList(typelist, "MTM_TypeId", "MTM_Type");

        //            var memberlist = service.GetGlobalSelect<MemberMaster>("MemberMaster_MM", "MM_MemberId", null);
        //            ViewBag.MM_MemberId = new SelectList(memberlist, "MM_MemberId", "MM_MemberCode");

        //            var booklist = service.GetGlobalSelect<BookMaster_BM>("BookMaster_BM", "BM_BookId", null);
        //            ViewBag.BCM_BookId = new SelectList(booklist, "BM_BookId", "BM_Title");

        //            var copylist = service.GetGlobalSelect<BookMaster_BM>("BookCopyMaster_BCM", "BCM_CopyId", null);
        //            ViewBag.IT_CopyId = new SelectList(booklist, "BM_BookId", "BM_Title");
        //        }
        //        else
        //        {
        //            editId = (Int64)id;

        //            data = service.GetGlobalSelectOne<IssueTransaction>("IssueTransaction_IT", "IT_IssueId", editId);
        //        }
        //        return View(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["_Message"] = "Something went wrong. Please try again.";
        //        return RedirectToAction("BookMasterList", "Masters");
        //    }
        //}
        public ActionResult IssueTransaction()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            IssueTransaction list = new IssueTransaction();

            var typelist = service.GetGlobalSelect<MemberTypeMaster_MTM>("MemberTypeMaster_MTM", "MTM_TypeId", null);
            ViewBag.MTM_TypeId = new SelectList(typelist, "MTM_TypeId", "MTM_Type");

            //var memberlist = service.GetGlobalSelect<MemberMaster>("MemberMaster_MM", "MM_MemberId", null);
            //ViewBag.MM_MemberId = new SelectList(memberlist, "MM_MemberId", "MM_MemberCode");

            var booklist = service.GetGlobalSelect<BookMaster_BM>("BookMaster_BM", "BM_BookId", null);
            ViewBag.BCM_BookId = new SelectList(booklist, "BM_BookId", "BM_Title");

            //var copylist = service.GetGlobalSelect<BookMaster_BM>("BookCopyMaster_BCM", "BCM_CopyId", null);
            //ViewBag.IT_CopyId = new SelectList(booklist, "BM_BookId", "BM_Title");

            ViewBag.IT_MemberId = new SelectList(Enumerable.Empty<SelectListItem>());

            ViewBag.IT_CopyId = new SelectList(Enumerable.Empty<SelectListItem>());

            return View(list);
        }
        //public ActionResult MemberMasterDdl(MemberMaster member)
        //{
        //    var list = service.MemberMasterList(member);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult BookCopyDdl(BookCopyMaster_BCM copy)
        //{
        //    var list = service.BookCopyMasterList(copy);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        #endregion
    }
}


