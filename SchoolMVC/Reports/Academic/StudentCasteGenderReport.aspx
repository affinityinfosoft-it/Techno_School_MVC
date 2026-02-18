<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentCasteGenderReport.aspx.cs" Inherits="SchoolMVC.Reports.Academic.StudentCasteGenderReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<script src="../../Scripts/jquery-1.12.4.js"></script>
<script src="../../Scripts/jquery-1.12.4.min.js"></script>
<head runat="server">
    <title>Student Caste Gender Report.</title>
</head>
<body>
    <section class="content">
        <div class="container-fluid">
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">
                        <div class="body">
                            <form id="form2" runat="server">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div style="float: right;">
                                            <asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/Content/images/word_logo.png" OnClick="BtnWord_Click" Style="width: 30px; margin-right: 5px;" ToolTip="Export this report as Word format." />
                                            <asp:ImageButton runat="server" ID="ImageButton3" ImageUrl="~/Content/images/excel_icon.png" OnClick="BtnExcel_Click" Style="width: 30px; margin-right: 5px;" ToolTip="Export this report as Excel format." />
                                            <asp:ImageButton runat="server" ID="ImageButton4" ImageUrl="~/Content/images/application-pdf.png" OnClick="BtnPdf_Click" Style="width: 30px; margin-right: 5px;" ToolTip="Export this report as PDF format." />
                                            <asp:ImageButton runat="server" ImageUrl="~/Content/images/close_icon.png" OnClientClick="window.close();" Style="width: 30px;" ToolTip="Close this report." />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">                                     
                                         <div id="dvReport">
                                          <CR:CrystalReportViewer runat="server" OnUnload="crystalreportviewer_Unload" AutoDataBind="true" ID="CrystalReportViewer" HasToggleGroupTreeButton="false" BestFitPage="true" Width="100%"></CR:CrystalReportViewer>
                                        </div>                                       
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</body>

