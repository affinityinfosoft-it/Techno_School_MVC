using Microsoft.IdentityModel.Tokens;
using SchoolMVC.Areas.StudentPortal.Models;
using SchoolMVC.Areas.StudentPortal.Models.Request;
using SchoolMVC.Areas.StudentPortal.Models.Response;
using SchoolMVC.BLLService;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SchoolMVC.Areas.FacultyPortal.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/FileUpload")]
    public class FileUploadController : ApiController
    {
        //[HttpPost]
        //public async Task<IHttpActionResult> Upload()
        //{
        //    ResultWithData<Dictionary<string, object>> result = new ResultWithData<Dictionary<string, object>>();

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        try
        //        {
        //            if (!Request.Content.IsMimeMultipartContent())
        //            {
        //                result.IsValid = false;
        //                result.ErrorMsg = "Invalid content type.";
        //                return Content(HttpStatusCode.BadRequest, result);
        //            }
        //            var request = HttpContext.Current.Request;

        //            if (request.Files.Count == 0)
        //                return BadRequest("No file received");

        //            var file = request.Files["File"];
        //            var fileName = file.FileName;
        //            var extension = Path.GetExtension(fileName).ToLowerInvariant();
        //            string[] permittedExtensions = { ".txt", ".pdf", ".png", ".jpg", ".jpeg" };
        //            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        //            {
        //                result.IsValid = false;
        //                result.ErrorMsg = "Invalid file extension. (" + string.Join(",", permittedExtensions) + ")";
        //                return Content(HttpStatusCode.BadRequest, result);
        //            }

        //            var newFileName = Guid.NewGuid() + extension;

        //            var savePath = HttpContext.Current.Server.MapPath("~/Uploads/");
        //            if (!Directory.Exists(savePath))
        //                Directory.CreateDirectory(savePath);

        //            var filePath = Path.Combine(savePath, newFileName);
        //            file.SaveAs(filePath);



        //            result.IsValid = true;
        //            result.Data = new Dictionary<string, object> { { "fileName", newFileName } };
        //            return Content(HttpStatusCode.OK, result);
        //        }
        //        catch (Exception ex)
        //        {
        //            result.IsValid = false;
        //            result.ErrorMsg = "Internal Error";
        //            return Content(HttpStatusCode.InternalServerError, result);
        //        }
        //    }
        //    else
        //    {
        //        result.IsValid = false;
        //        result.ErrorMsg = "Invalid Token";
        //        return Content(HttpStatusCode.Unauthorized, result);
        //    }
        //}


        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            ResultWithData<Dictionary<string, object>> result =
                new ResultWithData<Dictionary<string, object>>();

            if (!User.Identity.IsAuthenticated)
            {
                result.IsValid = false;
                result.ErrorMsg = "Invalid Token";
                return Content(HttpStatusCode.Unauthorized, result);
            }

            // Check multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                result.IsValid = false;
                result.ErrorMsg = "Invalid content type. Use multipart/form-data.";
                return Content(HttpStatusCode.BadRequest, result);
            }

            try
            {
                // Upload folder
                string uploadRoot =
                    HttpContext.Current.Server.MapPath("~/UploadAssignment/");

                if (!Directory.Exists(uploadRoot))
                    Directory.CreateDirectory(uploadRoot);

                // Read multipart content
                MultipartFormDataStreamProvider provider =
                    new MultipartFormDataStreamProvider(uploadRoot);

                await Request.Content.ReadAsMultipartAsync(provider);

                if (provider.FileData == null || provider.FileData.Count == 0)
                {
                    result.IsValid = false;
                    result.ErrorMsg = "No file received";
                    return Content(HttpStatusCode.BadRequest, result);
                }

    
                MultipartFileData fileData = provider.FileData[0];

                string filepath="";
                string originalFileName = "file";
                if (fileData.Headers != null &&
                    fileData.Headers.ContentDisposition != null &&
                    !string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    originalFileName =
                        fileData.Headers.ContentDisposition.FileName.Replace("\"", "");

                    
                }

                // Get extension
                string ext = Path.GetExtension(originalFileName);
                if (ext == null)
                    ext = "";

                ext = ext.ToLowerInvariant();

                string[] permittedExtensions =
                    new string[] { ".txt", ".pdf", ".png", ".jpg", ".jpeg" };

                if (string.IsNullOrWhiteSpace(ext) ||
                    !permittedExtensions.Contains(ext))
                {
                    SafeDelete(fileData.LocalFileName);

                    result.IsValid = false;
                    result.ErrorMsg =
                        "Invalid file extension. (" +
                        string.Join(", ", permittedExtensions) + ")";
                    return Content(HttpStatusCode.BadRequest, result);
                }

                // Create new safe filename
                string newFileName =
                    Guid.NewGuid().ToString("N") + ext;

                string finalPath =
                    Path.Combine(uploadRoot, newFileName);

                // Move temp file to final name
                if (File.Exists(finalPath))
                    File.Delete(finalPath);

                File.Move(fileData.LocalFileName, finalPath);
                filepath = "~/UploadAssignment/" + newFileName;
                result.IsValid = true;
                result.Data = new Dictionary<string, object>
            {
                { "fileName", newFileName },
                { "originalFileName", originalFileName },
                { "filepath", filepath }

            };

                return Content(HttpStatusCode.OK, result);
            }
            catch (UnauthorizedAccessException ex)
            {
                result.IsValid = false;
                result.ErrorMsg = ex.Message;
                return Content(HttpStatusCode.InternalServerError, result);
            }
            catch (DirectoryNotFoundException ex)
            {
                result.IsValid = false;
                result.ErrorMsg = ex.Message;
                return Content(HttpStatusCode.InternalServerError, result);
            }
            catch (FileNotFoundException ex)
            {
                result.IsValid = false;
                result.ErrorMsg = ex.Message;
                return Content(HttpStatusCode.InternalServerError, result);
            }
            catch (IOException ex)
            {
                result.IsValid = false;
                result.ErrorMsg = ex.Message;
                return Content(HttpStatusCode.InternalServerError, result);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMsg = ex.Message;
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        private void SafeDelete(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                // ignore
            }
        }





        [HttpGet]
        public IHttpActionResult Download(string fileName)
        {
            ResultWithData<Dictionary<string, object>> result = new ResultWithData<Dictionary<string, object>>();

            if (User.Identity.IsAuthenticated)
            {

                try
                {
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        result.IsValid = false;
                        result.ErrorMsg = "Filename is not provided.";
                        return Content(HttpStatusCode.BadRequest, result);
                    }

                    var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), fileName);
                    if (!File.Exists(filePath))
                    {
                        result.IsValid = false;
                        result.ErrorMsg = "File not found.";
                        return Content(HttpStatusCode.NotFound, result);
                    }

                    var extension = Path.GetExtension(filePath).ToLowerInvariant();
                    var contentType = GetContentType(extension);
                    var fileBytes = File.ReadAllBytes(filePath);

                    result.IsValid = true;
                    result.Data = new Dictionary<string, object>
        {
            { "base64String", Convert.ToBase64String(fileBytes) },
            { "contentType", contentType },
            { "fileName", fileName }
        };

                    return Content(HttpStatusCode.OK, result);
                }
                       catch (UnauthorizedAccessException ex)
                      {
                          result.IsValid = false;
                          result.ErrorMsg = ex.Message;
                          return Content(HttpStatusCode.InternalServerError, result);
                      }
                      catch (DirectoryNotFoundException ex)
                      {
                          result.IsValid = false;
                          result.ErrorMsg = ex.Message;
                          return Content(HttpStatusCode.InternalServerError, result);
                      }
                      catch (FileNotFoundException ex)
                      {
                          result.IsValid = false;
                          result.ErrorMsg =ex.Message;
                          return Content(HttpStatusCode.InternalServerError, result);
                      }
                      catch (IOException ex)
                      {
                          result.IsValid = false;
                          result.ErrorMsg = ex.Message;
                          return Content(HttpStatusCode.InternalServerError, result);
                      }
                      catch (Exception ex)
                      {
                          result.IsValid = false;
                          result.ErrorMsg = ex.Message;
                          return Content(HttpStatusCode.InternalServerError, result);
                      }
            }
            else
            {
                result.IsValid = false;
                result.ErrorMsg = "Invalid Token";
                return Content(HttpStatusCode.Unauthorized, result);
            }
        }

        [HttpPost]
        public IHttpActionResult Delete(string fileName)
        {
            ResultWithoutData result = new ResultWithoutData();
            if (User.Identity.IsAuthenticated)
            {

                try
                {
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        result.IsValid = false;
                        result.ErrorMsg = "Filename is not provided.";
                        return Content(HttpStatusCode.BadRequest, result);
                    }

                    var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Uploads"), fileName);
                    if (!File.Exists(filePath))
                    {
                        result.IsValid = false;
                        result.ErrorMsg = "File not found.";
                        return Content(HttpStatusCode.NotFound, result);
                    }

                    File.Delete(filePath);
                    result.IsValid = true;
                    result.SuccessMsg = "File deleted successfully.";
                    return Content(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    result.IsValid = false;
                    result.ErrorMsg = "Internal Error";
                    return Content(HttpStatusCode.InternalServerError, result);
                }
            }
            else
            {
                result.IsValid = false;
                result.ErrorMsg = "Invalid Token";
                return Content(HttpStatusCode.Unauthorized, result);
            }
        }


        private string GetContentType(string extension)
        {
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".png": return "image/png";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                default: return "application/octet-stream";
            }
        }
    }
}
