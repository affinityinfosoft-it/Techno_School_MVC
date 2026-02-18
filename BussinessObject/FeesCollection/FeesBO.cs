using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.FeesCollection
{
    public class FeesSearchBO
    {
        public long SchoolId { get; set; }
        public long SessionId { get; set; }
        public long ClassId { get; set; }
        public string FormNo { get; set; }
        public string AdmissionNo { get; set; }
        public int? CategoryId { get; set; }
        public string StudentId { get; set; }
        public string RegNo { get; set; }

    }
    public class StudentList
    {
        public Int64 SD_Id { get; set; }
        public int rownum { get; set; }
        public int ClassId { get; set; }
        public string SD_StudentId { get; set; }
        public string SD_FatherName { get; set; }
        public string SD_FormNo { get; set; }
        public string SD_AppliactionNo { get; set; }

        public string AdmissionNo { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string SD_StudentName { get; set; }
        public DateTime? SD_AppliactionDate { get; set; }
        public string SD_AppliactionDateS
        {
            get
            {
                return SD_AppliactionDate.HasValue
                    ? SD_AppliactionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime SD_DOB { get; set; }
        public string SD_DOBS
        {
            get
            {
                return SD_DOB.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Year;
            }
        }

        public int SD_StudentCategoryId { get; set; }
        public string CAT_STUDENTCATEGORY { get; set; }
        public string SD_RegNo { get; set; }

    }
    public class studentFeesDetails
    {
        public string FeesName { get; set; }
        public int FeesId { get; set; }
        public int NoOfInstallment { get; set; }
        public decimal FeesAmount { get; set; }
        public int NoOfFins { get; set; }
        public int InstallmentNo { get; set; }
        public string DueDate { get; set; }
        public decimal? InsAmount { get; set; }
        public decimal? Payable { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? AdustAmnt { get; set; }
        public decimal? DueAmt { get; set; }
        public int ClassId { get; set; }
        public int ClassFeesId { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDisc { get; set; }
        public string IsPaidChecked { get; set; }
        public string IsDiscChecked { get; set; }
        public string IsWaveChecked { get; set; }
        public string StudentId { get; set; }
        public decimal? LateFees { get; set; }

      
        public decimal? DiscountAmt { get; set; }
        public decimal? IsDiscApplied { get; set; }

        public decimal? LateDiscountAmt { get; set; }
        public string IsLateDiscApplied { get; set; }
        public bool? IsAdmissionTime { get; set; }

       // LateFees property
        public bool IsWave { get; set; }
        public decimal? WaveAmount { get; set; }
    }
    public class studentInfo
    {
        public string StudentName { get; set; }
        public string formNo { get; set; }
        public string AdmissionNo { get; set; }
        public string ClassName { get; set; }
        public int ClassId { get; set; }
        public int SD_StudentCategoryId { get; set; }
        public string StudentId { get; set; }
        public string RegNo { get; set; }
        public string SECM_SECTIONNAME { get; set; }
        public string SD_FatherName { get; set; }
        public string SD_CurrentRoll { get; set; }

    }
    public class FeesCollectionBO
    {
        public long? feesCollectionId { get; set; }
        public int? CM_CLASSID { get; set; }
        public long schoolId { get; set; }
        public long sessionId { get; set; }
        public long? userId { get; set; }
        public string formNo { get; set; }
        public string admissionNo { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? paidAmount { get; set; }
        public decimal? discount { get; set; }
        public decimal? totalDue { get; set; }
        public decimal? waveamount { get; set; }
        public string discountedBy { get; set; }
        public DateTime? feesDate { get; set; }
        public string feesDateS
        {
            get
            {
                return feesDate.HasValue
                    ? feesDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      feesDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      feesDate.Value.Year
                    : string.Empty;
            }
        }
        public string studentName { get; set; }
        public string studentId { get; set; }
        public string paymodeType { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string cheqDDNo { get; set; }
        public DateTime? cheqDDDate { get; set; }
        public string cheqDDDateS
        {
            get
            {
                return cheqDDDate.HasValue
                    ? cheqDDDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      cheqDDDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      cheqDDDate.Value.Year
                    : string.Empty;
            }
        }
        public string Card_TrnsRefNo { get; set; }
        public string rcptNo { get; set; }
        public string studenRegistrationId { get; set; }
        public int? stuCatId { get; set; }
        public string className { get; set; }



        public decimal? totallatefees { get; set; }
        public decimal? latefeesdiscount { get; set; }
        public decimal? totalpaidlatefees { get; set; }

        public decimal? totalpaidamount { get; set; }

        public DateTime? fromDate { get; set; }
        public string fromDateS
        {
            get
            {
                return fromDate.HasValue
                    ? fromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      fromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      fromDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? toDate { get; set; }

        public string toDateS
        {
            get
            {
                return toDate.HasValue
                    ? toDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      toDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      toDate.Value.Year
                    : string.Empty;
            }
        }




        public List<FeesCollectionTransBO> collectionsTrans { get; set; }
        public FeesCollectionBO()
        {
            collectionsTrans = new List<FeesCollectionTransBO>();
        }
    }
    public class FeesCollectionTransBO
    {
        public long? FEESCOLCTRANSID { get; set; }
        public long? FEESCOLCID { get; set; }
        public long? FEESID { get; set; }
        public long? CLASSFEESID { get; set; }
        public decimal? FEESAMNT { get; set; }
        public decimal? TOTALINSTLMNT { get; set; }
        public long? INSTALMENTNO { get; set; }
        public string DUEDATE { get; set; }
        public decimal? INSTALLMENTAMOUNT { get; set; }

        public decimal? PAYMENTAMOUNT { get; set; }
        public decimal? DISCOUNT { get; set; }
        public decimal? DUEAMT { get; set; }
        public long? CLASSID { get; set; }
        public bool? IsDisc { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsWave { get; set; }

        public decimal? LATEFEES { get; set; }
        public decimal? LATEDISCOUNT { get; set; }
        public bool? IsLateDisc { get; set; }
        public decimal? WAVEAMOUNT { get; set; }






    }

    public class FormBO
    {
        public int? feesCollectionId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalDue { get; set; }
        public string DiscountedBy { get; set; }
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
        public string Card_RefNo { get; set; }
        public string PaymodeType { get; set; }
        public string RECIPTNO { get; set; }
        public List<studentFeesDetails> StudentFees { get; set; }
        public studentInfo StudentInformation { get; set; }
        public FormBO()
        {
            StudentFees = new List<studentFeesDetails>();
            StudentInformation = new studentInfo();
        }


    }
}