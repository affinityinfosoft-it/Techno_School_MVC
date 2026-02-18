using Microsoft.IdentityModel.Tokens;
using SchoolMVC.Areas.StudentPortal.Models;
using SchoolMVC.Areas.StudentPortal.Models.Request;
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
    public class StudentLoginController : ApiController
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
        public IHttpActionResult GetStudentLoginById(StudentLoginByIdRequest obj)
        {
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
            ResultWithData<dynamic> Result = new ResultWithData<dynamic>();
            try
            {
                StudetDetails_SD request = new StudetDetails_SD();
                request.SD_StudentId = obj.SD_StudentId;
                request.SD_Password = obj.SD_Password;
                var data = service.GetStudentLogin(request);
                if (data != null)
                {
                    if (data.SD_StudentId == obj.SD_StudentId && data.SD_Password == obj.SD_Password)
                    {

                        var tokenString = GenerateJSONWebToken(data);
                        var resultresponse = new Dictionary<string, object>
                         {
                             { "token", tokenString},
                             { "SD_StudentId", data.SD_StudentId },
                             { "SD_SchoolId", data.SD_SchoolId },
                             { "SCM_SCHOOLNAME", data.SCM_SCHOOLNAME },
                             { "SD_CurrentSessionId", data.SD_CurrentSessionId },
                             { "SD_CurrentClassId", data.SD_CurrentClassId },
                             { "SD_CurrentSectionId", data.SD_CurrentSectionId },
                             { "SD_StudentName", data.SD_StudentName }
                         
                         };
                        Result.IsValid = true;
                        Result.Data = resultresponse;
                        Result.SuccessMsg = "Login Success";
                        return Content(HttpStatusCode.OK, Result);
                    }
                    else
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "Data not found";
                        return Content(HttpStatusCode.BadRequest, Result);
                    }


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


        private string GenerateJSONWebToken(StudetDetails_SD data)
        {
            string key = ConfigurationManager.AppSettings["JWT_key"]; //Secret key which will be used later during validation    
            var issuer = ConfigurationManager.AppSettings["JWT_issuer"];  //normally this will be your site URL 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("SD_Id", data.SD_Id.ToString()));
            permClaims.Add(new Claim("SD_StudentId", data.SD_StudentId.ToString()));
            permClaims.Add(new Claim("SD_SchoolId", data.SD_SchoolId.ToString()));
            permClaims.Add(new Claim("SD_CurrentClassId", data.SD_CurrentClassId.ToString()));
            permClaims.Add(new Claim("SD_CurrentSectionId", data.SD_CurrentSectionId.ToString()));
            permClaims.Add(new Claim("SD_CurrentSessionId", data.SD_CurrentSessionId.ToString()));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);


            return jwt_token;
        }




    }
}
