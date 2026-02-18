using SchoolMVC.Areas.FacultyPortal.Models.Request;
using SchoolMVC.Areas.FacultyPortal.Models.Response;
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
    public class CommonController : ApiController
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
        public IHttpActionResult GetClassWiseSection(ClassWiseSectionRequest obj)
        {
            ResultWithData<ClassWiseSectionResponse> Result = new ResultWithData<ClassWiseSectionResponse>();
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

                    var query = service.GetGlobalSelect<ClassWiseSectionResponse>("SectionMaster_SECM", "SECM_SECTIONID", null);
                    var data = query.Where(r => r.SECM_CM_CLASSID == obj.CM_CLASSID && r.SECM_SCM_SCHOOLID == obj.SCM_SCHOOLID).OrderBy(r => r.SECM_SECTIONNAME).ToList();
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
        public IHttpActionResult GetSchoolWiseClass(SchoolWiseClassRequest obj)
        {
            ResultWithData<SchoolWiseClassResponse> Result = new ResultWithData<SchoolWiseClassResponse>(); // Use object instead of List<object>

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

                    var classQuery = service.GetGlobalSelect<SchoolWiseClassResponse>("ClassMaster_CM", "CM_CLASSID", null);
                    var classData = classQuery.Where(r => r.CM_SCM_SCHOOLID == obj.SCM_SCHOOLID)
                                              .OrderBy(r => r.CM_CLASSNAME)
                                              .ToList();

                    if (classData.Any())
                    {
                        Result.IsValid = true;
                        Result.List = classData;
                        Result.SuccessMsg = "Success";
                        return Content(HttpStatusCode.OK, Result);
                    }
                    else
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "No classes found for the given school";
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
        public IHttpActionResult GetSubjectsBySubjectGroup(SchoolWiseSGroupSubRequest obj)
        {
            ResultWithData<SchoolWiseSGroupSubResponse> Result = new ResultWithData<SchoolWiseSGroupSubResponse>();

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
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                    ValidationResult.IsValid = false;
                    ValidationResult.ErrorMsg = "Validation Error";
                    ValidationResult.List = errors;
                    return Content(HttpStatusCode.BadRequest, ValidationResult);
                }

                try
                {
                    if (obj.SGM_SubjectGroupID == null || obj.SGM_SubjectGroupID <= 0)
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "Invalid Subject Group ID.";
                        return Content(HttpStatusCode.BadRequest, Result);
                    }

                    if (obj.SCM_SCHOOLID == null || obj.SCM_SCHOOLID <= 0)
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "Invalid School ID.";
                        return Content(HttpStatusCode.BadRequest, Result);
                    }

                    var allData = service.GetGlobalSelect<SchoolWiseSGroupSubResponse>(
                            "ClsSubGrWiseSubSetting_CSGWS",
                            "CSGWS_SubGr_Id",
                            obj.SGM_SubjectGroupID).ToList();

                    if (allData.Count == 0)
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "No data found for the given Subject Group ID.";
                        return Content(HttpStatusCode.NotFound, Result);
                    }

                    var subjectgroupData = allData.Where(r => r.SGM_SCHOOLID == obj.SCM_SCHOOLID)
                                                  .OrderBy(r => r.SGM_SubjectGroupName)
                                                  .ToList();

                    if (!subjectgroupData.Any())
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "No subjects found for the given subject group and school.";
                        return Content(HttpStatusCode.NotFound, Result);
                    }

                    // ✅ ADDING FINAL RETURN STATEMENT
                    Result.IsValid = true;
                    Result.List = subjectgroupData;
                    Result.SuccessMsg = "Subjects retrieved successfully.";
                    return Content(HttpStatusCode.OK, Result);
                }
                catch (Exception ex)
                {
                    Result.IsValid = false;
                    Result.ErrorMsg = "Internal Error: {ex.Message}"; // Fixed string interpolation
                    return Content(HttpStatusCode.InternalServerError, Result);
                }
            }
            else
            {
                Result.IsValid = false;
                Result.ErrorMsg = "Unauthorized access. Invalid token.";
                return Content(HttpStatusCode.Unauthorized, Result);
            }
        }


        #region GetSchoolWiseSubjectGroup and GetSchoolWiseSubject
        [HttpPost]
        public IHttpActionResult GetSchoolWiseSubjectGroup(SchoolWiseSubjectGroupRequest obj)
        {
            ResultWithData<SchoolWiseSubjectGroupResponse> Result = new ResultWithData<SchoolWiseSubjectGroupResponse>(); // Use object instead of List<object>

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

                    var subjectgroupQuery = service.GetGlobalSelect<SchoolWiseSubjectGroupResponse>("SubjectGroupMaster_SGM", "SGM_SubjectGroupID", null);
                    var subjectgroupData = subjectgroupQuery.Where(r => r.SGM_SCHOOLID == obj.SCM_SCHOOLID)
                                              .OrderBy(r => r.SGM_SubjectGroupName)
                                              .ToList();

                    if (subjectgroupData.Any())
                    {
                        Result.IsValid = true;
                        Result.List = subjectgroupData;
                        Result.SuccessMsg = "Success";
                        return Content(HttpStatusCode.OK, Result);
                    }
                    else
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "No classes found for the given school";
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
        public IHttpActionResult GetSchoolWiseSubject(SchoolWiseSubjectRequest obj)
        {
            ResultWithData<SchoolWiseSubjectResponse> Result = new ResultWithData<SchoolWiseSubjectResponse>(); // Use object instead of List<object>

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

                    var subjectQuery = service.GetGlobalSelect<SchoolWiseSubjectResponse>("SubjectMaster_SBM", "SBM_Id", null);
                    var subjectData = subjectQuery.Where(r => r.SBM_SchoolId == obj.SCM_SCHOOLID)
                                              .OrderBy(r => r.SBM_SubjectName)
                                              .ToList();

                    if (subjectData.Any())
                    {
                        Result.IsValid = true;
                        Result.List = subjectData;
                        Result.SuccessMsg = "Success";
                        return Content(HttpStatusCode.OK, Result);
                    }
                    else
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "No classes found for the given school";
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


        #endregion

    }
}