using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC
{
    /// <summary>
    /// Summary description for fileUpload
    /// </summary>
    public class fileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];

                    string path = context.Server.MapPath("UploadFile/" + file.FileName);
                    //string fname = context.Server.MapPath("Files/EmployeePic/" + file.FileName);
                    file.SaveAs(path);
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("File Uploaded Successfully!");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}