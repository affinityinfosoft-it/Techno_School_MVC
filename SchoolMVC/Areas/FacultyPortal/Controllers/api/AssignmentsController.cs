using SchoolMVC.Areas.FacultyPortal.Models.Request;
using SchoolMVC.Areas.FacultyPortal.Models.Response;
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

namespace SchoolMVC.Areas.FacultyPortal.Controllers.api
{
    public class AssignmentsController : ApiController
    {
        public Service service = new Service();

        [HttpPost]
        public IHttpActionResult InsertUpdateAssignmentMaster(AssignmentMaster_ASM obj)
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
                    var data = service.InsertUpdateAssignmentMaster(obj);
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
        public IHttpActionResult UpdateAssignmentMarks(AssignmentMarkUpdate assignmentMarkUpdate)
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
                    var data = service.UpdateAssignmentMarks(assignmentMarkUpdate);
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
        public IHttpActionResult GetAssignmentList(AssignmentsListRequest obj)
        {
            ResultWithData<AssignmentsListResponse> Result = new ResultWithData<AssignmentsListResponse>();
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

                    var data = service.GetAssignmentList(obj);
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

        //List of those students who has uploaded the assignment with document
        [HttpPost]
        public IHttpActionResult GetUploadedStudentList(AssignmentsListRequest obj)
        {
            ResultWithData<UploadedStudentListResponse> Result = new ResultWithData<UploadedStudentListResponse>();
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

                    var data = service.GetUploadedStudentList(obj);
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




        //[HttpPost]
        //public IHttpActionResult InsertUpdateAssignmentMaster(AssignmentsListRequest obj)
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
        //            var data = service.InsertUpdateAssignmentMaster(obj);
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

        [HttpGet]
        public IHttpActionResult DeleteAssignment(int id)
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
                    string data = service.DeleteAssignment(id);
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


        
    }
}
