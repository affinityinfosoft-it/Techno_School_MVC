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
    public class FacultyLoginController : ApiController
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
        public IHttpActionResult GetFacultyLoginById(FacultyLoginByIdRequest obj)
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
                FacultyProfileMasters_FPM request = new FacultyProfileMasters_FPM();
                request.FP_FacultyCode = obj.FP_FacultyCode;
                request.FP_Password = obj.FP_Password;
                var data = service.GetFacultyLogin(request);
                if (data != null)
                {
                    if (data.FP_FacultyCode == obj.FP_FacultyCode && data.FP_Password == obj.FP_Password)
                    {

                        var tokenString = GenerateJSONWebToken(data);
                        var resultresponse = new Dictionary<string, object>
                         {
                             { "token", tokenString},
                             { "FP_FacultyCode", data.FP_FacultyCode },
                             { "FP_UserType", data.FP_UserType },
                             { "FP_SchoolId", data.FP_SchoolId },
                             { "FP_SessionId", data.FP_SessionId },
                             { "FP_Name", data.FP_Name },
                             { "FP_Id", data.FP_Id }
                         
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



        private string GenerateJSONWebToken(FacultyProfileMasters_FPM data)
        {
            string key = ConfigurationManager.AppSettings["JWT_key"]; //Secret key which will be used later during validation    
            var issuer = ConfigurationManager.AppSettings["JWT_issuer"];  //normally this will be your site URL 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("FP_Id", data.FP_Id.ToString()));
            permClaims.Add(new Claim("FP_FacultyCode", data.FP_FacultyCode.ToString()));
            permClaims.Add(new Claim("FP_SchoolId", data.FP_SchoolId.ToString()));
            permClaims.Add(new Claim("FP_SessionId", data.FP_SessionId.ToString()));
            //permClaims.Add(new Claim("FP_DesignationId", data.FP_DesignationId.ToString()));
            //permClaims.Add(new Claim("FP_Phone", data.FP_Phone.ToString()));

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
