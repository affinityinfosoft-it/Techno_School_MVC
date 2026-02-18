using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolMVC.Models;
using System.Globalization;
namespace SchoolMVC.Controllers
{
    public class PettyCashController : BaseController
    {
        // GET: PettyCash
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountMaster(int? Id)
        {
            //if (!User.Identity.IsAuthenticated || UserModel == null)
            //{
            //    return RedirectToAction("Login", "Account", new { Area = "Addmission" });
            //}
            if (UserModel == null) return returnLogin("~/Masters/School");
            AccountMaster AccountMaster = new AccountMaster();
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            //ViewBag.AcademicYear = UserModel.;
            //ViewBag.Role = UserModel.;
            ViewBag.TUserId = UserModel.UM_USERID;
            //ViewBag.RoleId = UserModel.RoleId;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            
            if (Id != null && Id != 0)
            {
                AccountMaster = service.GetAccountMasterDetails("SELECT-ONE", Id);
            }
            AccountMaster.AccountGroupList = service.getAccountGroupList();
            return View(AccountMaster);
        }
        [HttpPost]
        public ActionResult AccountMaster(FormCollection FC, int? Id)
        {
            //if (!User.Identity.IsAuthenticated || UserModel == null)
            //{
            //    return RedirectToAction("Login", "Account", new { Area = "Addmission" });
            //}
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            //ViewBag.AcademicYear = UserModel.;
            //ViewBag.Role = UserModel.;
            ViewBag.TUserId = UserModel.UM_USERID;
            //ViewBag.RoleId = UserModel.RoleId;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            AccountMaster AccountMaster = new AccountMaster();
            //long? TransactionId = Convert.ToInt64(FC["hdnTransactionId"] == "" ? "0" : FC["hdnTransactionId"]);
            AccountMaster.AM_AccountId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            AccountMaster.AM_AccountCode = Convert.ToString(FC["txtAccountCode"]);
            AccountMaster.AM_CompanyID = UserModel.UM_SCM_SCHOOLID;
            AccountMaster.AM_FYId = UserModel.UM_SCM_SESSIONID;

            AccountMaster.AM_LongName = Convert.ToString(FC["txtAccountName"]);
            AccountMaster.AM_AccDescription = Convert.ToString(FC["txtAccountDesc"]);
            AccountMaster.AM_GroupId = Convert.ToInt32(FC["ddlAcgroup"] == "" ? "0" : FC["ddlAcgroup"]);
            AccountMaster.ISSubAc = Convert.ToString(FC["ISSubAc"]);
            //AccountMaster.AM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance"] == "" ? "0" : FC["txtOpeningBalance"]);

            //AccountMaster.AM_OPeningType = Convert.ToString(FC["ddlOpeningType"]);
            Boolean chk = Convert.ToBoolean(FC["IsSuppressPayee"].Split(',')[0]);
            if (chk == true)
            {
                AccountMaster.AM_SuppressPayee = "Y";
            }
            else
            {
                AccountMaster.AM_SuppressPayee = "N";
            }

            AccountMaster.AM_IsFund = Convert.ToBoolean(FC["AM_IsFund"].Split(',')[0]);


            try
            {

                int? AccId = service.InsertAccountMasterDetails(AccountMaster);
                if (AccountMaster.AM_AccountId > 0)
                {
                    AccountMaster = service.GetAccountMasterDetails("SELECT-ONE", AccId);
                }



                if (AccId > 0)
                {
                    ViewBag.Message = "Record saved successfully...";
                    ViewBag.MessageId = "1";
                }
                else if (AccId == -100)
                {
                    ViewBag.Message = "Account code already exists";
                    ViewBag.MessageId = "-100";
                }



            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            AccountMaster.AccountGroupList = service.getAccountGroupList();
            return View(AccountMaster);
        }

        [HttpGet]
        public ActionResult AccountMasterList()
        {
            //if (!User.Identity.IsAuthenticated || UserModel == null)
            //{
            //    return RedirectToAction("Login", "Account", new { Area = "Addmission" });
            //}
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            //ViewBag.AcademicYear = UserModel.;
            //ViewBag.Role = UserModel.;
            ViewBag.TUserId = UserModel.UM_USERID;
            //ViewBag.RoleId = UserModel.RoleId;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            AccountMaster FModel = new AccountMaster();
          
            FModel.AccountGroupList = service.getAccountGroupList();
           
            return View(FModel);
        }


        public ActionResult SubAccountMaster(int? Id)
        {
            //if (!User.Identity.IsAuthenticated || UserModel == null)
            //{
            //    return RedirectToAction("Login", "Account", new { Area = "Addmission" });
            //}
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
           
            ViewBag.TUserId = UserModel.UM_USERID;
          
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            SubAccountMaster AccountMaster = new SubAccountMaster();
           
            AccountMaster.AccountMasterList = service.GetAccountMasterDropdownList();
            if (Id != null && Id != 0)
            {
                AccountMaster = service.GetSubAccountMasterDetails("SELECT-ONE", Id);
            }

            return View(AccountMaster);
        }
        [HttpPost]
        public ActionResult SubAccountMaster(FormCollection FC, int? Id)
        {
            //if (!User.Identity.IsAuthenticated || UserModel == null)
            //{
            //    return RedirectToAction("Login", "Account", new { Area = "Addmission" });
            //}
            if (UserModel == null) return returnLogin("~/Masters/School");
           
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
           
            ViewBag.TUserId = UserModel.UM_USERID;
         
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            SubAccountMaster AccountMaster = new SubAccountMaster();
            //long? TransactionId = Convert.ToInt64(FC["hdnTransactionId"] == "" ? "0" : FC["hdnTransactionId"]);
            AccountMaster.SAM_SubId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            AccountMaster.SAM_AccountId = Convert.ToInt32(FC["ddlAccount"] == "" ? "0" : FC["ddlAccount"]);
            AccountMaster.SAM_SubCode = Convert.ToString(FC["txtAccountCode"]);
            AccountMaster.SAM_CompanyID = UserModel.UM_SCM_SCHOOLID;
            AccountMaster.SAM_FYID = UserModel.UM_SCM_SESSIONID;
            AccountMaster.SAM_SubLongDesc = Convert.ToString(FC["txtAccountName"]);
            AccountMaster.SAM_SubDescription = Convert.ToString(FC["txtAccountDesc"]);
            AccountMaster.SAM_Address1 = Convert.ToString(FC["txtAddress1"]);
            AccountMaster.SAM_Address2 = Convert.ToString(FC["txtAddress2"]);
            AccountMaster.SAM_Address3 = Convert.ToString(FC["txtAddress3"]);
            AccountMaster.SAM_Address4 = Convert.ToString(FC["txtAddress4"]);
            AccountMaster.SAM_OPhone = Convert.ToString(FC["txtOPhone"]);
            AccountMaster.SAM_FAX = Convert.ToString(FC["txtFAX"]);
            AccountMaster.SAM_Email = Convert.ToString(FC["txtEmail"]);
            AccountMaster.SAM_Website = Convert.ToString(FC["txtWebsite"]);
            AccountMaster.SAM_PAN = Convert.ToString(FC["txtPAN"]);
            AccountMaster.SAM_CST = Convert.ToString(FC["txtCST"]);
            AccountMaster.SAM_SST = Convert.ToString(FC["txtSST"]);
            //AccountMaster.SAM_IsFund = Convert.ToBoolean(FC["SAM_IsFund"].Split(',')[0]);
            //AccountMaster.AM_GroupId = Convert.ToInt32(FC["ddlAcgroup"] == "" ? "0" : FC["ddlAcgroup"]);

            //AccountMaster.AM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance"] == "" ? "0" : FC["txtOpeningBalance"]);

            //AccountMaster.AM_OPeningType = Convert.ToString(FC["ddlOpeningType"]);

            try
            {

                int? AccId = service.InsertSubAccountMasterDetails(AccountMaster);
                if (AccountMaster.SAM_SubId > 0)
                {
                    AccountMaster = service.GetSubAccountMasterDetails("SELECT-ONE", AccId);

                }
                //AccountMaster.AccountGroupList = AcBLogic.getAccountGroupList();


                ViewBag.Message = "Record saved successfully...";
                ViewBag.MessageId = "1";


            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            //AccountMaster.AccountMasterList = AcBLogic.GetAccountMasterDropdownList();
            return View(AccountMaster);
        }

        [HttpGet]
        public ActionResult SubAccountMasterList()
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            ViewBag.TUserId = UserModel.UM_USERID;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            SubAccountMaster FModel = new SubAccountMaster();
        
            return View(FModel);
        }

        [HttpGet]
        public ActionResult OpeningBalanceAccountMaster(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            //ViewBag.AcademicYear = UserModel.;
            //ViewBag.Role = UserModel.;
            ViewBag.TUserId = UserModel.UM_USERID;
            //ViewBag.RoleId = UserModel.RoleId;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            AccountMaster AccountMaster = new AccountMaster();
          
            if (Id != null && Id != 0)
            {
                AccountMaster = service.GetAccountMasterDetails("SELECT-ONE", Id);
            }
            //AccountMaster.AccountGroupList = AcBLogic.getAccountGroupList();
            return View(AccountMaster);
        }
        [HttpPost]
        public ActionResult OpeningBalanceAccountMaster(FormCollection FC, int? Id)
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            //ViewBag.AcademicYear = UserModel.;
            //ViewBag.Role = UserModel.;
            ViewBag.TUserId = UserModel.UM_USERID;
            //ViewBag.RoleId = UserModel.RoleId;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            AccountMaster AccountMaster = new AccountMaster();
            //long? TransactionId = Convert.ToInt64(FC["hdnTransactionId"] == "" ? "0" : FC["hdnTransactionId"]);
            AccountMaster.AM_AccountId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            AccountMaster.AM_AccountOpId = Convert.ToInt32(FC["hdnAccountOpId"] == "" ? "0" : FC["hdnAccountOpId"]);
            AccountMaster.AM_AccountCode = Convert.ToString(FC["hdnAccountCode"]);


            //AccountMaster.AM_AccountCode = Convert.ToString(FC["txtAccountCode"]);
            AccountMaster.AM_CompanyID = UserModel.UM_SCM_SCHOOLID;
            AccountMaster.AM_FYId = UserModel.UM_SCM_SESSIONID;

            AccountMaster.AM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance"] == "" ? "0" : FC["txtOpeningBalance"]);

            AccountMaster.AM_OPeningType = Convert.ToString(FC["ddlOpeningType"]);
            

            try
            {

                int? AccId = service.InsertAccountMasterOpeningDetails(AccountMaster);
                AccountMaster = service.GetAccountMasterDetails("SELECT-ONE", AccountMaster.AM_AccountId);



                ViewBag.Message = "Record saved successfully...";
                ViewBag.MessageId = "1";


            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            return View(AccountMaster);
        }


        [HttpGet]
        public ActionResult OpeningBalanceSubAccountMaster(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            ViewBag.TUserId = UserModel.UM_USERID;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            SubAccountMaster SubAccountMaster = new SubAccountMaster();
           
            
            if (Id != null && Id != 0)
            {
                SubAccountMaster.SubAccountMasterList = service.SubAccountMasterOpeningbalanceList("SELECT-ONE", Id);
            }
            //AccountMaster.AccountGroupList = AcBLogic.getAccountGroupList();
            return View(SubAccountMaster);
        }
        [HttpPost]
        public ActionResult OpeningBalanceSubAccountMaster(FormCollection FC, int? Id)
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            ViewBag.TUserId = UserModel.UM_USERID;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            SubAccountMaster SubAccountMaster = new SubAccountMaster();
            int? AccId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            SubAccountMaster.AM_AccountId = AccId;
            SubAccountMaster.SAM_CompanyID = UserModel.UM_SCM_SCHOOLID;
            SubAccountMaster.SAM_FYID = UserModel.UM_SCM_SESSIONID;
            //long? TransactionId = Convert.ToInt64(FC["hdnTransactionId"] == "" ? "0" : FC["hdnTransactionId"]);


            //AccountMaster.AM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance"] == "" ? "0" : FC["txtOpeningBalance"]);

            //AccountMaster.AM_OPeningType = Convert.ToString(FC["ddlOpeningType"]);
            for (int i = 1; i < 1000; ++i)
            {
                var obj = new SubAccountMaster();

                if (FC["hdnSubAccountId_" + i.ToString()] != null)
                {

                    obj.SAM_AccountId = Convert.ToInt32(FC["hdnSubAccountId_" + i.ToString()]);
                    obj.SAM_SubCode = Convert.ToString(FC["hdnSubAccountCode_" + i.ToString()]);
                    //obj.SAM_SubDescription = Convert.ToString(FC["hdnSubDescription_" + i.ToString()]);

                    obj.SAM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance_" + i.ToString()] == "" ? "0" : FC["txtOpeningBalance_" + i.ToString()]);
                    obj.SAM_OPeningType = Convert.ToString(FC["OpeningType_" + i.ToString()]);

                    SubAccountMaster.SubAccountMasterList.Add(obj);
                }
                else
                {
                    break;
                }
            }

            try
            {

                int? val = service.InsertSubAccountMasterOpeningDetails(SubAccountMaster);
                SubAccountMaster.SubAccountMasterList = service.SubAccountMasterOpeningbalanceList("SELECT-ONE", AccId);
                if (val > 0)
                {
                    service.InsertUpdateAccountMasterOpening(AccId);
                }


                ViewBag.Message = "Record saved successfully...";
                ViewBag.MessageId = "1";


            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            return View(SubAccountMaster);
        }


        [HttpGet]
        public ActionResult OpeningBalanceMasterList()
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            ViewBag.TUserId = UserModel.UM_USERID;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            AccountMaster FModel = new AccountMaster();
            FModel.AM_FYId = UserModel.UM_SCM_SESSIONID;
            FModel.AM_CompanyID = UserModel.UM_SCM_SCHOOLID;

            FModel.AccountMasterList = service.AccountMasterList("SELECT-ALL", FModel);
            //ViewBag.GlobalData = new JavaScriptSerializer().Serialize(FSRecords.AsQueryable<SubAccountMaster>().ToList<SubAccountMaster>());
            return View(FModel);
        }



        [HttpGet]
        public ActionResult VoucherEntry(int? VoucherTypeId, int? Id)
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
           
            AccountLedger AccountLedgerS = new AccountLedger();

            if (VoucherTypeId == 0 || VoucherTypeId == null)
            {

                return RedirectToAction("Login", "Account", new { Area = "Addmission" });

            }
            else
            {
                AccountVoucherTypeMaster AccountVoucherTypeMasterS = new AccountVoucherTypeMaster();
                AccountVoucherTypeMasterS.VoucherTypeId = VoucherTypeId;
                AccountVoucherTypeMasterS.AccountVoucherTypeMasterList = service.AccountVoucherTypeList(AccountVoucherTypeMasterS);
                if (AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.Count() > 0)
                {
                    AccountLedgerS.VoucherType = AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.FirstOrDefault().VoucherType;
                }
                else
                {
                    return RedirectToAction("Login", "Account", new { Area = "Addmission" });

                }
            }
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            ViewBag.TUserId = UserModel.UM_USERID;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;

            AccountLedgerS.LD_VoucherTypeId = VoucherTypeId;
            AccountLedgerS.LD_VoucherNo = service.GetVoucherID(UserModel.UM_SCM_SESSIONID, VoucherTypeId);
            AccountMaster AModel = new AccountMaster();
            AModel.AM_FYId = UserModel.UM_SCM_SESSIONID;
            AModel.AM_CompanyID = UserModel.UM_SCM_SCHOOLID;
            AccountLedgerS.AccountMasterList = service.AccountMasterList("SELECT-ALL", AModel);

            SubAccountMaster SAModel = new SubAccountMaster();
            SAModel.SAM_FYID = UserModel.UM_SCM_SESSIONID;
            SAModel.SAM_CompanyID = UserModel.UM_SCM_SCHOOLID;
            AccountLedgerS.SubAccountMasterList = service.SubAccountMasterList("SELECT-ALL", SAModel);

            if (VoucherTypeId == 1 || VoucherTypeId == 2)
            {
                if (AccountLedgerS.AccountMasterList.Count() > 0)
                {
                    //foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.AM_AccountCode.ToUpper() == "CA"))
                    foreach (var item in AccountLedgerS.AccountMasterList)
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.AM_AccountId + "-" + "0";
                        Account.Text = item.AM_LongName;
                        Account.LD_IsFund = item.AM_IsFund;
                        AccountLedgerS.AccountList.Add(Account);
                    }
                }
                if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                {
                    //foreach (var Acc in AccountLedgerS.AccountMasterList.Where(a => a.AM_AccountCode.ToUpper() == "BK"))
                    foreach (var Acc in AccountLedgerS.AccountMasterList)
                    {
                        foreach (var item in AccountLedgerS.SubAccountMasterList)
                        {
                            if (Acc.AM_AccountId == item.SAM_AccountId)
                            {
                                AccountLedger Account = new AccountLedger();

                                Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                                Account.Text = item.SAM_SubLongDesc;
                                Account.LD_IsFund = item.SAM_IsFund;
                                AccountLedgerS.AccountList.Add(Account);
                            }

                        }
                    }

                }


                if (AccountLedgerS.AccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.AM_AccountId + "-" + "0";
                        Account.Text = item.AM_LongName;
                        Account.LD_IsFund = item.AM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }

                if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.SubAccountMasterList)
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                        Account.Text = item.SAM_SubLongDesc;
                        Account.LD_IsFund = item.SAM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }

            }
            else
            {
                if (AccountLedgerS.AccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.AM_AccountId + "-" + "0";
                        Account.Text = item.AM_LongName;
                        Account.LD_IsFund = item.AM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }

                if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.SubAccountMasterList)
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                        Account.Text = item.SAM_SubLongDesc;
                        Account.LD_IsFund = item.SAM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }
            }

            return View(AccountLedgerS);
        }
        [HttpPost]
        public ActionResult VoucherEntry(FormCollection FC)
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            ViewBag.TUserId = UserModel.UM_USERID;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            AccountLedger AccountLedgerS = new AccountLedger();
            //int? AccId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);

            AccountLedgerS.LD_CompanyId = UserModel.UM_SCM_SCHOOLID;
            AccountLedgerS.LD_FYId = UserModel.UM_SCM_SESSIONID;
            AccountLedgerS.LD_DateS = Convert.ToString(FC["txtDateS"]);

            AccountLedgerS.LD_VoucherTypeId = Convert.ToInt32(FC["LD_VoucherTypeId"]);

            if (AccountLedgerS.LD_VoucherTypeId != 3)
            {
                AccountLedgerS.LD_ChequeDate = DateTime.ParseExact(FC["LD_ChequeDate"] == "" ? DateTime.Now.ToString("dd/MM/yyyy") : FC["txtDateS"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                AccountLedgerS.LD_VoucherNo = Convert.ToString(FC["txtVoucherNo"]);
                AccountLedgerS.LD_ChequeNo = Convert.ToString(FC["txtChequeNo"]);
                AccountLedgerS.AccountValue = Convert.ToString(FC["ddlAccount"]);


            }


            AccountLedgerS.LD_ReferenceNo = Convert.ToString(FC["txtReferenceNo"]);
            AccountLedgerS.LD_Payee = Convert.ToString(FC["txtPayee"]);
            AccountLedgerS.LD_Narration = Convert.ToString(FC["txtNarration"]);

            //long? TransactionId = Convert.ToInt64(FC["hdnTransactionId"] == "" ? "0" : FC["hdnTransactionId"]);


            //AccountMaster.AM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance"] == "" ? "0" : FC["txtOpeningBalance"]);

            //AccountMaster.AM_OPeningType = Convert.ToString(FC["ddlOpeningType"]);
            int RowCount = Convert.ToInt32(FC["RowCount"] == "" ? "0" : FC["RowCount"]);

            decimal Amount = 0;
            for (int i = 0; i < RowCount; ++i)
            {
                var obj = new AccountLedger();

                if (FC["Particulars_" + i.ToString()] != null)
                {
                    var ParticularsValue = FC["Particulars_" + i.ToString()].Split('-');
                    obj.LD_AccountID = Convert.ToInt32(ParticularsValue[0]);
                    obj.LD_SubID = Convert.ToInt32(ParticularsValue[1]);
                    var FundValue = FC["Fund_" + i.ToString()].Split('-');
                    obj.LD_FundAccountID = Convert.ToInt32(FundValue[0]);
                    obj.LD_FundSubID = Convert.ToInt32(FundValue[1]);
                    obj.LD_DrCr = Convert.ToString(FC["DrCr_" + i.ToString()]);
                    if (obj.LD_DrCr == "C")
                    {
                        obj.LD_CrAmount = Convert.ToDecimal(FC["Amount_" + i.ToString()]);
                    }
                    else
                    {
                        obj.LD_DrAmount = Convert.ToDecimal(FC["Amount_" + i.ToString()]);

                    }


                    obj.LD_Remarks = Convert.ToString(FC["Remarks_" + i.ToString()]);
                    obj.LD_VoucherTypeId = AccountLedgerS.LD_VoucherTypeId;


                    AccountLedgerS.AccountLedgerList.Add(obj);
                    Amount += Convert.ToDecimal(FC["Amount_" + i.ToString()]);
                }
                else
                {
                    break;
                }
            }


            if (AccountLedgerS.LD_VoucherTypeId == 1)
            {
                var AccountValue = AccountLedgerS.AccountValue.Split('-');
                var obj = new AccountLedger();
                obj.LD_AccountID = Convert.ToInt32(AccountValue[0]);
                obj.LD_SubID = Convert.ToInt32(AccountValue[1]);
                obj.LD_CrAmount = Amount;
                obj.LD_DrCr = "C";
                obj.LD_VoucherTypeId = AccountLedgerS.LD_VoucherTypeId;


                AccountLedgerS.AccountLedgerList.Add(obj);
            }
            if (AccountLedgerS.LD_VoucherTypeId == 2)
            {
                var AccountValue = AccountLedgerS.AccountValue.Split('-');
                var obj = new AccountLedger();
                obj.LD_AccountID = Convert.ToInt32(AccountValue[0]);
                obj.LD_SubID = Convert.ToInt32(AccountValue[1]);
                obj.LD_DrAmount = Amount;
                obj.LD_DrCr = "D";
                obj.LD_VoucherTypeId = AccountLedgerS.LD_VoucherTypeId;


                AccountLedgerS.AccountLedgerList.Add(obj);
            }
            try
            {

                int? val = service.InsertVoucherEntryDetails(AccountLedgerS);

                ViewBag.LD_LedgerID = val;

                int? VoucherTypeId = AccountLedgerS.LD_VoucherTypeId;
                AccountLedgerS = new AccountLedger();

                if (VoucherTypeId == 0 || VoucherTypeId == null)
                {

                    return RedirectToAction("Login", "Account", new { Area = "Addmission" });

                }
                else
                {
                    AccountVoucherTypeMaster AccountVoucherTypeMasterS = new AccountVoucherTypeMaster();
                    AccountVoucherTypeMasterS.VoucherTypeId = VoucherTypeId;
                    AccountVoucherTypeMasterS.AccountVoucherTypeMasterList = service.AccountVoucherTypeList(AccountVoucherTypeMasterS);
                    if (AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.Count() > 0)
                    {
                        AccountLedgerS.VoucherType = AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.FirstOrDefault().VoucherType;
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account", new { Area = "Addmission" });

                    }
                }


                AccountLedgerS.LD_VoucherTypeId = VoucherTypeId;
                AccountLedgerS.LD_VoucherNo = service.GetVoucherID(UserModel.UM_SCM_SESSIONID, VoucherTypeId);
                AccountMaster AModel = new AccountMaster();
                AModel.AM_FYId = UserModel.UM_SCM_SESSIONID;
                AModel.AM_CompanyID = UserModel.UM_SCM_SCHOOLID;
                AccountLedgerS.AccountMasterList = service.AccountMasterList("SELECT-ALL", AModel);

                SubAccountMaster SAModel = new SubAccountMaster();
                SAModel.SAM_FYID = UserModel.UM_SCM_SESSIONID;
                SAModel.SAM_CompanyID = UserModel.UM_SCM_SCHOOLID;
                AccountLedgerS.SubAccountMasterList = service.SubAccountMasterList("SELECT-ALL", SAModel);

                if (VoucherTypeId == 1 || VoucherTypeId == 2)
                {
                    if (AccountLedgerS.AccountMasterList.Count() > 0)
                    {
                        foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.AM_AccountCode.ToUpper() == "CA"))
                        {
                            AccountLedger Account = new AccountLedger();

                            Account.Value = item.AM_AccountId + "-" + "0";
                            Account.Text = item.AM_LongName;
                            Account.LD_IsFund = item.AM_IsFund;
                            AccountLedgerS.AccountList.Add(Account);
                        }
                    }
                    if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                    {
                        foreach (var Acc in AccountLedgerS.AccountMasterList.Where(a => a.AM_AccountCode.ToUpper() == "BK"))
                        {
                            foreach (var item in AccountLedgerS.SubAccountMasterList)
                            {
                                if (Acc.AM_AccountId == item.SAM_AccountId)
                                {
                                    AccountLedger Account = new AccountLedger();

                                    Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                                    Account.Text = item.SAM_SubLongDesc;
                                    Account.LD_IsFund = item.SAM_IsFund;
                                    AccountLedgerS.AccountList.Add(Account);
                                }

                            }
                        }

                    }


                    if (AccountLedgerS.AccountMasterList.Count() > 0)
                    {
                        foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                        {
                            AccountLedger Account = new AccountLedger();

                            Account.Value = item.AM_AccountId + "-" + "0";
                            Account.Text = item.AM_LongName;
                            Account.LD_IsFund = item.AM_IsFund;
                            AccountLedgerS.ParticularsList.Add(Account);
                        }
                    }

                    if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                    {
                        foreach (var item in AccountLedgerS.SubAccountMasterList)
                        {
                            AccountLedger Account = new AccountLedger();

                            Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                            Account.Text = item.SAM_SubLongDesc;
                            Account.LD_IsFund = item.SAM_IsFund;
                            AccountLedgerS.ParticularsList.Add(Account);
                        }
                    }

                }
                else
                {
                    if (AccountLedgerS.AccountMasterList.Count() > 0)
                    {
                        foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                        {
                            AccountLedger Account = new AccountLedger();

                            Account.Value = item.AM_AccountId + "-" + "0";
                            Account.Text = item.AM_LongName;
                            Account.LD_IsFund = item.AM_IsFund;
                            AccountLedgerS.ParticularsList.Add(Account);
                        }
                    }

                    if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                    {
                        foreach (var item in AccountLedgerS.SubAccountMasterList)
                        {
                            AccountLedger Account = new AccountLedger();

                            Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                            Account.Text = item.SAM_SubLongDesc;
                            Account.LD_IsFund = item.SAM_IsFund;
                            AccountLedgerS.ParticularsList.Add(Account);
                        }
                    }
                }
                ViewBag.Message = "Record saved successfully...";
                ViewBag.MessageId = "1";


            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            return View(AccountLedgerS);
        }

        [HttpGet]
        public ActionResult VoucherEntryList(int? VoucherTypeId)
        {
            if (!User.Identity.IsAuthenticated || UserModel == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Addmission" });
            }
            AccountLedger AccountLedgerS = new AccountLedger();

            if (VoucherTypeId == 0 || VoucherTypeId == null)
            {

                return RedirectToAction("Login", "Account", new { Area = "Addmission" });

            }
            else
            {
                AccountVoucherTypeMaster AccountVoucherTypeMasterS = new AccountVoucherTypeMaster();
                AccountVoucherTypeMasterS.VoucherTypeId = VoucherTypeId;
                AccountVoucherTypeMasterS.AccountVoucherTypeMasterList = service.AccountVoucherTypeList(AccountVoucherTypeMasterS);
                if (AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.Count() > 0)
                {
                    AccountLedgerS.VoucherType = AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.FirstOrDefault().VoucherType;
                }
                else
                {
                    return RedirectToAction("Login", "Account", new { Area = "Addmission" });

                }
                AccountLedgerS.LD_VoucherTypeId = VoucherTypeId;
                AccountLedgerS.LD_FYId = UserModel.UM_SCM_SESSIONID;
                AccountLedgerS.AccountLedgerList = service.VoucherEntryList(AccountLedgerS);
                //var FSRecords = AccountLedgerS.AccountLedgerList;
                //ViewBag.GlobalData = new JavaScriptSerializer().Serialize(FSRecords.AsQueryable<AccountLedger>().ToList<AccountLedger>());


            }


            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            ViewBag.TUserId = UserModel.UM_USERID;
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;

            return View(AccountLedgerS);
        }

      

    }
}