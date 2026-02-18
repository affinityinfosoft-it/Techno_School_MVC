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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
namespace SchoolMVC.Areas.StudentPortal.Controllers.api
{
    public class NoticeController : ApiController
    {
        public Service service = new Service();

        [HttpPost]
        public IHttpActionResult GetNotice(NoticeRequest obj)
        {
            ResultWithData<NoticeResponse> Result = new ResultWithData<NoticeResponse>();
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                if (!ModelState.IsValid)
                {
                    ResultWithData<string> ValidationResult = new ResultWithData<string>();
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    ValidationResult.IsValid = false;
                    ValidationResult.ErrorMsg = "Validation Error";
                    ValidationResult.List = errors;
                    return Content(HttpStatusCode.BadRequest, ValidationResult);
                }

                try
                {

                    var data = service.GetNotice(obj);
                    if (data != null)
                    {
                       

                        Result.IsValid = true;
                        Result.List = data;
                        Result.SuccessMsg = "Success";
                        return Content(HttpStatusCode.OK, Result);



                    }
                    else
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "Data not found";
                        return Content(HttpStatusCode.BadRequest, Result);
                    }

                }
                catch (Exception ex)
                {
                    Result.IsValid = false;
                    Result.ErrorMsg = "Internal Error";
                    return Content(HttpStatusCode.InternalServerError, Result);
                }
            }
            else
            {
                Result.IsValid = false;
                Result.ErrorMsg = "Invalid Token";
                return Content(HttpStatusCode.Unauthorized, Result);
            }

        }

    }
}
