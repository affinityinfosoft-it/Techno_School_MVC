using System.Web.Mvc;

namespace SchoolMVC.Areas.FeesCollection
{
    public class FeesCollectionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FeesCollection";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FeesCollection_default",
                "FeesCollection/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}