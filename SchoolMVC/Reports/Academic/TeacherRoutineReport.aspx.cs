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
    public partial class TeacherRoutineReport : System.Web.UI.Page
    {
        DataSet DMSObjSet = null;
        ReportDocument objReportDoc = new ReportDocument();
        public class QuiryParameter
        {

            public long? ClassId { get; set; }
            public long? SecId { get; set; }
            public long? SchoolId { get; set; }
            public long? SessionId { get; set; }
            public long? FacId { get; set; }


        }
        QuiryParameter QParameter = new QuiryParameter();
        protected void Page_Load(object sender, EventArgs e)
        {
            //var t = Convert.ToInt64(Request.QueryString["ClassId"]);

            QParameter.SchoolId = Convert.ToInt64(Request.QueryString["SchoolId"]);
            QParameter.SessionId = Convert.ToInt64(Request.QueryString["SessionId"]);
            QParameter.ClassId = Convert.ToInt64(Request.QueryString["ClassId"]);
            QParameter.SecId = Convert.ToInt64(Request.QueryString["SecId"]);
            QParameter.FacId = Convert.ToInt64(Request.QueryString["FacId"]);
            //QParameter.ischeck = Convert.ToBoolean(Request.QueryString["ischeck"]);

            // printreport();
            if (IsPostBack)
            {
                DMSObjSet = (DataSet)ViewState["DataSet"];
                if (DMSObjSet != null)
                {

                    objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/TeacherRoutineReport.rpt"));
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
            using (SqlDataAdapter da = new SqlDataAdapter("SP_RoutineReport", ConfigurationManager.ConnectionStrings["School_DbEntity"].ToString()))
            {

                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (QParameter.ClassId != 0)
                {
                    da.SelectCommand.Parameters.AddWithValue("@CLASSID", QParameter.ClassId);
                }
                if (QParameter.SecId != 0)
                {
                    da.SelectCommand.Parameters.AddWithValue("@SectioId", QParameter.SecId);
                }
                if (QParameter.FacId != 0)
                {
                    da.SelectCommand.Parameters.AddWithValue("@TeacherId", QParameter.FacId);
                }
                da.SelectCommand.Parameters.AddWithValue("@SchoolId", QParameter.SchoolId);
                da.SelectCommand.Parameters.AddWithValue("@SessionId", QParameter.SessionId);
              
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "SP_RoutineReport");
            }

            objReportDoc.Load(Server.MapPath("~/Reports/Academic/RPT/TeacherRoutineReport.rpt"));
            CrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            objReportDoc.SetDataSource(DMSObjSet.Tables["SP_RoutineReport"]);
            //objReportDoc.SetParameterValue("FDate", QParameter.FromDate);
            //objReportDoc.SetParameterValue("TDate", QParameter.ToDate);
            //objReportDoc.SetParameterValue("ischeck", QParameter.ischeck);
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
            objReportDoc.ExportToHttpResponse(formatType, Response, true, "Student Admission Details ");
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
            catch (Exception Ex)
            {

            }
            finally
            {
                GC.Collect();
            }
        }
    }
}