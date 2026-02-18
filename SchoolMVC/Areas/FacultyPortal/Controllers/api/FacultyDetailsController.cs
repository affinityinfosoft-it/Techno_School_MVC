using SchoolMVC.Areas.StudentPortal.Models;
using SchoolMVC.Areas.StudentPortal.Models.Request;
using SchoolMVC.Areas.StudentPortal.Models.Response;
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
    public class FacultyDetailsController : ApiController
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
        public IHttpActionResult GetFacultyDetailsById(FacultyDetailsByIdRequest obj)
        {
            ResultWithData<FacultyDetailsByIdResponse> Result = new ResultWithData<FacultyDetailsByIdResponse>();
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
                    FacultyProfileMasters_FPM request = new FacultyProfileMasters_FPM();
                    request.FP_FacultyCode = obj.FP_FacultyCode;
                    var data = service.GetFacultyLogin(request);
                    if (data != null)
                    {

                        FacultyDetailsByIdResponse facultyDetailsByIdResponse = new FacultyDetailsByIdResponse();


                        facultyDetailsByIdResponse.FP_Id = data.FP_Id;
                        facultyDetailsByIdResponse.FP_FacultyCode = data.FP_FacultyCode;
                        facultyDetailsByIdResponse.FP_UserType = data.FP_UserType;
                        facultyDetailsByIdResponse.FP_SchoolId = data.FP_SchoolId;
                        facultyDetailsByIdResponse.FP_SessionId = data.FP_SessionId;
                        facultyDetailsByIdResponse.FP_DesignationId = data.FP_DesignationId;
                        facultyDetailsByIdResponse.FP_Phone = data.FP_Phone;
                        facultyDetailsByIdResponse.FP_Name = data.FP_Name;
                        facultyDetailsByIdResponse.SCM_SCHOOLNAME = data.SCM_SCHOOLNAME;
                        facultyDetailsByIdResponse.SM_SESSIONNAME = data.SM_SESSIONNAME;
                        facultyDetailsByIdResponse.DM_Name = data.DM_Name;

                        Result.IsValid = true;
                        Result.Data = facultyDetailsByIdResponse;
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