using SchoolMVC.Areas.StudentPortal.Models.Request;
using SchoolMVC.Areas.StudentPortal.Models.Response;
using SchoolMVC.Areas.StudentPortal.Models;
using SchoolMVC.BLLService;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
namespace SchoolMVC.Areas.StudentPortal.Controllers.api
{
    public class StudentAssignmentsController : ApiController
    {
        public Service service = new Service();


        [HttpPost]
        public IHttpActionResult InsertUpdateStudentAssignmentMaster(StudentAssignmentMaster obj)
        {
            ResultWithoutData Result = new ResultWithoutData();
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
                    var data = service.InsertUpdateStudentAssignmentMaster(obj);
                    if (data != null)
                    {
                        Result.IsValid = true;
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

        [HttpPost]
        public IHttpActionResult GetStudentAssignmentList(StudentAssignmentsListRequest obj)
        {
            ResultWithData<StudentAssignmentsListResponse> Result = new ResultWithData<StudentAssignmentsListResponse>();
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
                    var data = service.GetStudentAssignmentList(obj);
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


        [HttpPost]
        public IHttpActionResult GetStudentAssignmentDetails(StudentAssignmentDtlsRequest studentAssignmentDtlsRequest)
        {
            ResultWithData<StudentAssignmentsListResponse> Result = new ResultWithData<StudentAssignmentsListResponse>();
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
                    var data = service.GetStudentAssignmentById(studentAssignmentDtlsRequest);
                    if (data != null)
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
        //[HttpGet]
        //public IHttpActionResult DeleteStudentAssignment(int id)
        //{
        //    ResultWithoutData Result = new ResultWithoutData();
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        if (identity != null)
        //        {
        //            IEnumerable<Claim> claims = identity.Claims;
        //        }
        //        if (!ModelState.IsValid)
        //        {
        //            ResultWithData<string> ValidationResult = new ResultWithData<string>();
        //            var errors = new List<string>();
        //            foreach (var state in ModelState)
        //            {
        //                foreach (var error in state.Value.Errors)
        //                {
        //                    errors.Add(error.ErrorMessage);
        //                }
        //            }
        //            ValidationResult.IsValid = false;
        //            ValidationResult.ErrorMsg = "Validation Error";
        //            ValidationResult.List = errors;
        //            return Content(HttpStatusCode.BadRequest, ValidationResult);
        //        }

        //        try
        //        {
        //            string data = service.DeleteStudentAssignment(id);
        //            if (data != null)
        //            {


        //                Result.IsValid = true;
        //                Result.SuccessMsg = "Success";
        //                return Content(HttpStatusCode.OK, Result);



        //            }
        //            else
        //            {
        //                Result.IsValid = false;
        //                Result.ErrorMsg = "Data not found";
        //                return Content(HttpStatusCode.BadRequest, Result);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            Result.IsValid = false;
        //            Result.ErrorMsg = "Internal Error";
        //            return Content(HttpStatusCode.InternalServerError, Result);
        //        }
        //    }
        //    else
        //    {
        //        Result.IsValid = false;
        //        Result.ErrorMsg = "Invalid Token";
        //        return Content(HttpStatusCode.Unauthorized, Result);
        //    }

        //}
    }
}
