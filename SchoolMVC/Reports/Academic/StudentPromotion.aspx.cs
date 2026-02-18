using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolMVC.Reports.Academic
{
    public partial class StudentPromotion : System.Web.UI.Page
    {
        DataSet DMSObjSet = null;
        ReportDocument objReportDoc = new ReportDocument();

        public class QuiryParameter
        {
            public long? ClassId { get; set; }
            public long? SecId { get; set; }
            public long? SchoolId { get; set; }
            public long? SessionId { get; set; }
            public int PromotionStatus { get; set; }

        }

        QuiryParameter QParameter = new QuiryParameter();

        protected void Page_Load(object sender, EventArgs e)
        {

            QParameter.SchoolId = Convert.ToInt64(Request.QueryString["SchoolId"]);
            QParameter.SessionId = Convert.ToInt64(Request.QueryString["SessionId"]);
            QParameter.ClassId = Convert.ToInt64(Request.QueryString["ClassId"]);
            QParameter.SecId = Convert.ToInt64(Request.QueryString["SecId"]);
            QParameter.PromotionStatus = Convert.ToInt32(Request.QueryString["PromotionStatus"] ?? "0");

            if (IsPostBack)
            {
                DMSObjSet = (DataSet)ViewState["DataSet"];
                if (DMSObjSet != null)
                {
                    objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/StudentPromotion.rpt"));
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
            string reportPath = "";

            using (SqlDataAdapter da = new SqlDataAdapter("StudentPromotionReport_SP", ConfigurationManager.ConnectionStrings["School_DbEntity"].ToString()))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@SchoolId", QParameter.SchoolId);
                da.SelectCommand.Parameters.AddWithValue("@SessionId", QParameter.SessionId);
                da.SelectCommand.Parameters.AddWithValue("@ClassId", QParameter.ClassId);
                da.SelectCommand.Parameters.AddWithValue("@SectionId", QParameter.SecId);
                da.SelectCommand.Parameters.AddWithValue("@PromotionStatus", QParameter.PromotionStatus);

                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "StudentPromotionReport_SP");
            }

            // Select report file based on PromotionStatus
            if (QParameter.PromotionStatus == 1) // Promoted
            {
                reportPath = Server.MapPath("~/Reports/Academic/RPT/StudentPromotion.rpt");
            }
            else if (QParameter.PromotionStatus == 2) // Unpromoted
            {
                reportPath = Server.MapPath("~/Reports/Academic/RPT/StudentUnpromoted.rpt");
            }
            else
            {
                reportPath = Server.MapPath("~/Reports/Academic/RPT/StudentPromotion.rpt"); // default
            }

            objReportDoc.Load(reportPath);
            CrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            objReportDoc.SetDataSource(DMSObjSet.Tables["StudentPromotionReport_SP"]);

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
            objReportDoc.ExportToHttpResponse(formatType, Response, true, "Student Promotion Report");
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
            catch { }
            finally
            {
                GC.Collect();
            }
        }
    }
}