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
    public partial class GenderWiseStudentStrengthReport : System.Web.UI.Page
    {
        DataSet DMSObjSet = null;
        ReportDocument objReportDoc = new ReportDocument();

        public class QuiryParameter
        {
            public long? ClassId { get; set; }
            public long? SecId { get; set; }
            public long? SchoolId { get; set; }
            public long? SessionId { get; set; }
            public string Gender { get; set; }

        }

        QuiryParameter QParameter = new QuiryParameter();

        protected void Page_Load(object sender, EventArgs e)
        {
            QParameter.SchoolId = Convert.ToInt64(Request.QueryString["SchoolId"]);
            QParameter.SessionId = Convert.ToInt64(Request.QueryString["SessionId"]);
            QParameter.ClassId = Convert.ToInt64(Request.QueryString["ClassId"]);
            QParameter.SecId = Convert.ToInt64(Request.QueryString["SecId"]);
            QParameter.Gender = Request.QueryString["Gender"];

            if (IsPostBack)
            {
                DMSObjSet = (DataSet)ViewState["DataSet"];
                if (DMSObjSet != null)
                {

                    objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/GenderWiseStudentStrength.rpt"));
                    CrystalReportViewer.ReportSource = objReportDoc;
                    objReportDoc.SetDataSource(DMSObjSet);
                    printreport();

                }
            }
            else printreport();
        }



        public void printreport()
        {

            DataSet DMSObjSet = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter("SP_AdmissionReport", ConfigurationManager.ConnectionStrings["School_DbEntity"].ToString()))
            {
                
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@SchoolId", QParameter.SchoolId);
                da.SelectCommand.Parameters.AddWithValue("@SessionId", QParameter.SessionId);
                da.SelectCommand.Parameters.AddWithValue("@SD_ClassId", QParameter.ClassId);
                da.SelectCommand.Parameters.AddWithValue("@SD_CurrentSectionId", QParameter.SecId);

                // GENDER
                if (!string.IsNullOrWhiteSpace(QParameter.Gender) && QParameter.Gender != "0")
                    da.SelectCommand.Parameters.AddWithValue("@Gender", QParameter.Gender);
                else
                    da.SelectCommand.Parameters.AddWithValue("@Gender", DBNull.Value);

                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "SP_AdmissionReport");
            }

            objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/GenderWiseStudentStrength.rpt"));
            CrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            objReportDoc.SetDataSource(DMSObjSet.Tables["SP_AdmissionReport"]);
            objReportDoc.DataSourceConnections.Clear();
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
            objReportDoc.ExportToHttpResponse(formatType, Response, true, "Gender wise Student Strength Report ");
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
