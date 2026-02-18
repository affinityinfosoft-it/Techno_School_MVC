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
    public class StudentDetailsController : ApiController
    {

        #region Return Type
        //Ok() or Ok(data) – returns 200 OK with optional data.
        //NotFound() – returns 404 Not Found.
        //BadRequest() – returns 400 Bad Request.
        //Unauthorized() – returns 401 Unauthorized.
        //InternalServerError() – returns 500 Internal Server Error.
        #endregion
        public Service service = new Service();

        [HttpPost]
        public IHttpActionResult GetStudentDetailsById(StudentDetailsByIdRequest obj)
        {
            ResultWithData<StudentDetailsByIdResponse> Result = new ResultWithData<StudentDetailsByIdResponse>();
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
                    StudetDetails_SD request = new StudetDetails_SD();
                    request.SD_StudentId = obj.SD_StudentId;
                    var data = service.GetStudentLogin(request);
                    if (data != null)
                    {

                        StudentDetailsByIdResponse studentDetailsByIdResponse = new StudentDetailsByIdResponse();


                        studentDetailsByIdResponse.SD_Id = data.SD_Id;
                        studentDetailsByIdResponse.SD_StudentId = data.SD_StudentId;
                        studentDetailsByIdResponse.SD_SchoolId = data.SD_SchoolId;
                        studentDetailsByIdResponse.SD_CurrentClassId = data.SD_CurrentClassId;
                        studentDetailsByIdResponse.SD_CurrentSectionId = data.SD_CurrentSectionId;
                        studentDetailsByIdResponse.SD_CurrentSessionId = data.SD_CurrentSessionId;
                        studentDetailsByIdResponse.SD_CurrentRoll = data.SD_CurrentRoll;
                        studentDetailsByIdResponse.SD_StudentName = data.SD_StudentName;
                        studentDetailsByIdResponse.SCM_SCHOOLNAME = data.SCM_SCHOOLNAME;
                        studentDetailsByIdResponse.SD_CM_CLASSNAME = data.SD_CM_CLASSNAME;
                        studentDetailsByIdResponse.SECM_SECTIONNAME = data.SECM_SECTIONNAME;
                        studentDetailsByIdResponse.SM_SESSIONNAME = data.SM_SESSIONNAME;


                        Result.IsValid = true;
                        Result.Data = studentDetailsByIdResponse;
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
