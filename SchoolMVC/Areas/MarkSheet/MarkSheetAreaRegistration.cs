using System.Web.Mvc;

namespace SchoolMVC.Areas.MarkSheet
{
    public class MarkSheetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MarkSheet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MarkSheet_default",
                "MarkSheet/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}