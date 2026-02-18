using BussinessObject.FeesCollection;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Areas.FeesCollection.Models
{
    public class AdmissionFeesModel
    {
        public long? feesCollectionId { get; set; }
        public SelectList ListClass { get; set; }
        public int? CM_CLASSID { get; set; }
        public string CM_CLASSNAME { get; set; }
        public long SchoolId { get; set; }
        public long SessionId { get; set; }
        public string FormNo { get; set; }
        public string AdmissionNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalDue { get; set; }
        [Required]
        public string DiscountedBy { get; set; }
        public decimal? WaveAmount { get; set; }
        public DateTime? FeesDate { get; set; }

        public string FeesDateS
        {
            get
            {
                return FeesDate.HasValue
                    ? FeesDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FeesDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FeesDate.Value.Year
                    : string.Empty;
            }
        }

        [Required]
        public bool Paymode { get; set; }
        public bool? payCash { get; set; }
        public bool? payCheque { get; set; }
        public bool? payDD { get; set; }
        public bool? payCard { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string CheqDDNo { get; set; }
        public DateTime? CheqDDDate { get; set; }
        public string CheqDDDateS
        {
            get
            {
                return CheqDDDate.HasValue
                    ? CheqDDDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CheqDDDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CheqDDDate.Value.Year
                    : string.Empty;
            }
        }
        public string Card_TrnsRefNo { get; set; }
        public string PaymodeType { get; set; }
        public string RECIPTNO { get; set; }
        public List<StudentList> listSearchDetails { get; set; }
        public List<studentFeesDetails> StudentFees { get; set; }
        public studentInfo StudentInformation { get; set; }
        public long? FeesHeadId { get; set; }
        public bool? IsWave { get; set; }
        public AdmissionFeesModel()
        {
            listSearchDetails = new List<StudentList>();
            StudentFees = new List<studentFeesDetails>();
            StudentInformation = new studentInfo();
            PaymodeType = "Cash";
            payCash = true;
            payCheque = payDD = payCard= false;
        }
    }

}