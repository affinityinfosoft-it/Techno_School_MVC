using SchoolMVC.Areas.StudentPortal.Models;
using SchoolMVC.Areas.StudentPortal.Models.Request;
using SchoolMVC.Areas.StudentPortal.Models.Response;
using SchoolMVC.BLLService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace SchoolMVC.Areas.StudentPortal.Controllers.api
{
    public class StudentPaidReceiptController : ApiController
    {
        public Service service = new Service();

        [HttpPost]
        public IHttpActionResult GetStudentPaidReceipt(StudentPaidReceiptRequest obj)
        {
            ResultWithData<StudentPaidReceiptResponse> Result = new ResultWithData<StudentPaidReceiptResponse>();
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

                    var data = service.GetStudentPaidReceipt(obj);
                    if (data.FEESCOLLECTIONID > 0)
                    {


                        Result.IsValid = true;
                        Result.Data = data;
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

