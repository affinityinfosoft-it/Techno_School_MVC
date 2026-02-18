using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolMVC.Reports.MarkSheet
{
    public partial class MarkSheetReport : System.Web.UI.Page
    {
        DataSet DMSObjSet = null;
        ReportDocument objReportDoc = new ReportDocument();
        public class QuiryParameter
        {
            public string StudentId { get; set; }
            public int? ClassId { get; set; }
            public int? TermId { get; set; }
        }
        QuiryParameter QParameter = new QuiryParameter();
        public string StudentIds = "";
        public string ClassIds = "";
        public string TermIds = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            QParameter.StudentId = Convert.ToString(Request.QueryString["SId"]);
            QParameter.ClassId = Convert.ToInt32(Request.QueryString["CId"]);
            QParameter.TermId = Convert.ToInt32(Request.QueryString["TermId"]);

            if (Session["PrintMarksheetStudents"] != null)
            {
                List<clsStudentList> studentList = new List<clsStudentList>();
                studentList = Session["PrintMarksheetStudents"] as List<clsStudentList>;
                Session["PrintMarksheetStudents"] = null;

                foreach (var studentdetails in studentList)
                {
                    StudentIds += "'" + studentdetails.StudentId + "',";
                    ClassIds += studentdetails.ClassId + ",";
                    TermIds += studentdetails.TermId + ",";
                }
                studentList = new List<clsStudentList>();

                if (StudentIds != "" && StudentIds != null)
                {
                    StudentIds = StudentIds.Remove(StudentIds.LastIndexOf(','));
                    Session["StudentIds"] = StudentIds;
                }
                if (ClassIds != "" && ClassIds != null)
                {
                    ClassIds = ClassIds.Remove(ClassIds.LastIndexOf(','));
                    Session["ClassIds"] = ClassIds;
                }
                if (TermIds != "" && TermIds != null)
                {
                    TermIds = TermIds.Remove(TermIds.LastIndexOf(','));
                    Session["TermIds"] = TermIds;
                }

            }
            else if (QParameter.StudentId != null && QParameter.ClassId != null && QParameter.TermId != null)
            {
                StudentIds += "'" + QParameter.StudentId + "',";
                ClassIds += QParameter.ClassId + ",";
                TermIds += QParameter.TermId + ",";

                if (StudentIds != "" && StudentIds != null)
                {
                    StudentIds = StudentIds.Remove(StudentIds.LastIndexOf(','));
                    Session["StudentIds"] = StudentIds;
                }
                if (ClassIds != "" && ClassIds != null)
                {
                    ClassIds = ClassIds.Remove(ClassIds.LastIndexOf(','));
                    Session["ClassIds"] = ClassIds;
                }
                if (TermIds != "" && TermIds != null)
                {
                    TermIds = TermIds.Remove(TermIds.LastIndexOf(','));
                    Session["TermIds"] = TermIds;
                }
            }

            else
            {
                StudentIds = Session["StudentIds"] as string;
                ClassIds = Session["ClassIds"] as string;
                TermIds = Session["TermIds"] as string;
            }


            if (IsPostBack)
            {
                DMSObjSet = (DataSet)ViewState["DataSet"];
                if (DMSObjSet != null)
                {
                    objReportDoc.Load(Server.MapPath("~/Reports/MarkSheet/StudentMarkSheetReport.rpt"));
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
            using (SqlDataAdapter da = new SqlDataAdapter("usp_StudentMarkSheetPrint", ConfigurationManager.ConnectionStrings["School_DbEntity"].ToString()))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@StudentId", StudentIds);
                da.SelectCommand.Parameters.AddWithValue("@ClassId", ClassIds);
                da.SelectCommand.Parameters.AddWithValue("@TermId", TermIds);
                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "usp_StudentMarkSheetPrint");
            }

            objReportDoc.Load(Server.MapPath("~/Reports/MarkSheet/StudentMarkSheetReport.rpt"));
            CrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            objReportDoc.SetDataSource(DMSObjSet.Tables["usp_StudentMarkSheetPrint"]);
            //objReportDoc.SetParameterValue("DateFrom", QParameter.FDate);
            //objReportDoc.SetParameterValue("DateTo", QParameter.TDate);
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
            objReportDoc.ExportToHttpResponse(formatType, Response, true, "Student MarkSheet " + QParameter.StudentId + "");
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