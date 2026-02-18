using SchoolMVC.Areas.Notification.Models.Request;
using SchoolMVC.Areas.Notification.Models.Response;
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

namespace SchoolMVC.Areas.Notification.Controllers.api
{
    public class NotificationController : ApiController
    {
        public Service service = new Service();

        [HttpPost]
        public IHttpActionResult RegisterDevice(RegistrationRequest obj)
        {
            ResultWithoutData Result = new ResultWithoutData();

            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }

                // Validate model
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
                    // Call service to insert or update the device
                    var data = service.RegisterDevice(obj);
                    if (data != null)
                    {
                        Result.IsValid = true;
                        Result.SuccessMsg = "Device registered successfully";
                        return Content(HttpStatusCode.OK, Result);
                    }
                    else
                    {
                        Result.IsValid = false;
                        Result.ErrorMsg = "Device registration failed";
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
