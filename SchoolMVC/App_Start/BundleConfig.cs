using System;
using System.Web;
using System.Web.Optimization;

namespace SchoolMVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            var version = DateTime.Now.ToString("yyyyMMddHHmmss");


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery.form.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css").Include("~/Content/jquery.fancybox.min.css"));
            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include("~/Content/themes/base/*.css"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryBasicjs").Include("~/Content/plugins/jquery/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquerypluginsjs").Include("~/Content/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js")
              .Include("~/Content/plugins/dropzone/dropzone.js").Include("~/Content/plugins/jquery-inputmask/jquery.inputmask.bundle.js")
              .Include("~/Content/plugins/node-waves/waves.js").Include("~/Content/plugins/autosize/autosize.js")
              .Include("~/Content/plugins/momentjs/moment.js"));//.Include("~/Content/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js")
            bundles.Add(new ScriptBundle("~/bundles/ContentBasicjs").Include("~/Content/js/admin.js").Include("~/Content/js/demo.js"));//.Include("~/Content/js/pages/forms/basic-form-elements.js").
            bundles.Add(new ScriptBundle("~/bundles/query-datatablePluginsjs").Include("~/Content/plugins/jquery-datatable/jquery.dataTables.js").Include("~/Content/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js")
               .Include("~/Content/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.min.js").Include("~/Content/plugins/jquery-datatable/extensions/export/dataTables.buttons.min.js").Include("~/Content/plugins/jquery-datatable/extensions/export/buttons.flash.min.js")
               .Include("~/Content/plugins/jquery-datatable/extensions/export/jszip.min.js").Include("~/Content/plugins/jquery-datatable/extensions/export/pdfmake.min.js")
               .Include("~/Content/plugins/jquery-datatable/extensions/export/vfs_fonts.js").Include("~/Content/plugins/jquery-datatable/extensions/export/buttons.html5.min.js")
               .Include("~/Content/plugins/jquery-datatable/extensions/export/buttons.print.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-datatablejs").Include("~/Content/js/pages/tables/jquery-datatable.js"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/utils.js"));
            bundles.Add(new ScriptBundle("~/bundles/toastjs").Include("~/Content/js/pages/ui/toast.js"));
            bundles.Add(new StyleBundle("~/bundles/Contentcss").Include("~/Content/css/googleapis.css").Include("~/Content/css/Materialicon.css"));
            bundles.Add(new StyleBundle("~/bundles/Contentpluginscss").Include("~/Content/plugins/bootstrap/css/bootstrap.css").Include("~/Content/plugins/node-waves/waves.css")
                .Include("~/Content/plugins/animate-css/animate.css").Include("~/Content/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css").Include("~/Content/plugins/waitme/waitMe.css")
                .Include("~/Content/plugins/bootstrap-select/css/bootstrap-select.css"));
            bundles.Add(new StyleBundle("~/bundles/ContentThemescss").Include("~/Content/css/style.css").Include("~/Content/*.css").Include("~/Content/css/themes/all-themes.css").Include("~/Content/themes/base/*.css"));
            bundles.Add(new StyleBundle("~/bundles/UIcss").Include("~/Content/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css").Include("~/Content/plugins/dropzone/dropzone.css")
                .Include("~/Content/plugins/multi-select/css/multi-select.css").Include("~/Content/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
