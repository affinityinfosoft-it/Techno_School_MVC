using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace SchoolMVC.Reports.Academic
{
    public partial class EnqueryReport : System.Web.UI.Page
    {
        DataSet DMSObjSet = null;
        ReportDocument objReportDoc = new ReportDocument();

        public class QuiryParameter
        {
            public long? SchoolId { get; set; }
            public long? SessionId { get; set; }
            //public long? ClassId { get; set; }
            public string ClassId { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public int? AdmissionStatus { get; set; }
        }

        QuiryParameter QParameter = new QuiryParameter();

        protected void Page_Load(object sender, EventArgs e)
        {
            string classQs = Request.QueryString["ClassId"];
            string fromDateQs = Request.QueryString["FromDate"];
            string toDateQs = Request.QueryString["ToDate"];
            string schoolQs = Request.QueryString["SchoolId"];
            string sessionQs = Request.QueryString["SessionId"];
            string admissionQs = Request.QueryString["AdmissionStatus"];

            int tempInt;
            if (int.TryParse(admissionQs, out tempInt))
                QParameter.AdmissionStatus = tempInt;
            else
                QParameter.AdmissionStatus = null;

            long tempLong;
            if (long.TryParse(schoolQs, out tempLong))
                QParameter.SchoolId = tempLong;
            else
                QParameter.SchoolId = null;

            if (long.TryParse(sessionQs, out tempLong))
                QParameter.SessionId = tempLong;
            else
                QParameter.SessionId = null;

            //if (long.TryParse(classQs, out tempLong))
            //    QParameter.ClassId = tempLong;
            //else
            //    QParameter.ClassId = null;
            QParameter.ClassId = string.IsNullOrWhiteSpace(classQs) ? null : classQs;

            DateTime fDate;
            DateTime tDate;

            if (DateTime.TryParse(fromDateQs, out fDate))
                QParameter.FromDate = fDate;
            else
                QParameter.FromDate = null;

            if (DateTime.TryParse(toDateQs, out tDate))
                QParameter.ToDate = tDate;
            else
                QParameter.ToDate = null;

            if (IsPostBack)
            {
                DMSObjSet = (DataSet)ViewState["DataSet"];
                if (DMSObjSet != null)
                {
                    objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/EnqueryReport.rpt"));
                    //objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/StudentIDCard.rpt"));
                    CrystalReportViewer.ReportSource = objReportDoc;
                    objReportDoc.SetDataSource(DMSObjSet);
                    printreport();
                }
            }
            else
            {
                printreport();
            }
        }



        public void printreport()
        {
            DataSet DMSObjSet = new DataSet();

            using (SqlDataAdapter da = new SqlDataAdapter("SP_EnqueryReport",
                ConfigurationManager.ConnectionStrings["School_DbEntity"].ToString()))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@SchoolId", SqlDbType.BigInt).Value =
                    (object)QParameter.SchoolId ?? DBNull.Value;
                da.SelectCommand.Parameters.Add("@SessionId", SqlDbType.BigInt).Value =
                    (object)QParameter.SessionId ?? DBNull.Value;


                da.SelectCommand.Parameters.Add("@AdmissionStatus", SqlDbType.Int).Value =
                   (object)QParameter.AdmissionStatus ?? DBNull.Value;

                da.SelectCommand.Parameters.Add("@ClassId", SqlDbType.VarChar, -1).Value =
                     (object)QParameter.ClassId ?? DBNull.Value;

                da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.Date).Value =
                    (object)QParameter.FromDate ?? DBNull.Value;
                da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.Date).Value =
                    (object)QParameter.ToDate ?? DBNull.Value;

                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "SP_EnqueryReport");
            }

            objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/EnqueryReport.rpt"));
            //objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/StudentIDCard.rpt"));
            CrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

            objReportDoc.SetDataSource(DMSObjSet.Tables["SP_EnqueryReport"]);

            // ==== Safe Parameter Assignment ====
            string fDateStr = QParameter.FromDate.HasValue
                ? QParameter.FromDate.Value.ToString("dd/MM/yyyy")
                : "";
            string tDateStr = QParameter.ToDate.HasValue
                ? QParameter.ToDate.Value.ToString("dd/MM/yyyy")
                : "";

            // Directly set parameters (no need to loop/cast)
            if (objReportDoc.DataDefinition.ParameterFields["FDate"] != null)
                objReportDoc.SetParameterValue("FDate", fDateStr);

            if (objReportDoc.DataDefinition.ParameterFields["TDate"] != null)
                objReportDoc.SetParameterValue("TDate", tDateStr);

            CrystalReportViewer.ReportSource = objReportDoc;
            CrystalReportViewer.DataBind();
            ViewState["DataSet"] = DMSObjSet;
        }

        public void ExportPDFWordExecel(string type)
        {
            printreport();
            ExportFormatType formatType = ExportFormatType.NoFormat;
            switch (type)
            {
                case "Word":
                    formatType = ExportFormatType.WordForWindows;
                    break;
                case "PDF":
                    formatType = ExportFormatType.PortableDocFormat;
                    break;
                case "Excel":
                    formatType = ExportFormatType.Excel;
                    break;
                case "CSV":
                    formatType = ExportFormatType.CharacterSeparatedValues;
                    break;
            }
            objReportDoc.ExportToHttpResponse(formatType, Response, true, "Enquery Details ");
            Response.End();
        }

        protected void BtnWord_Click(object sender, ImageClickEventArgs e)
        {
            ExportPDFWordExecel("Word");
        }

        protected void BtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportPDFWordExecel("Excel");
        }

        protected void BtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            ExportPDFWordExecel("PDF");
        }

        protected void crystalreportviewer_Unload(object sender, EventArgs e)
        {
            try
            {
                if (objReportDoc != null)
                {
                    objReportDoc.Close();
                    objReportDoc.Dispose();
                }
            }
            catch (Exception) { }
            finally
            {
                GC.Collect();
            }
        }
    }
}
