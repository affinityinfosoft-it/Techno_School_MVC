using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.Notification.Models.Request
{
    public class RegistrationRequest
    {
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string FcmToken { get; set; }
        public string DeviceType { get; set; }
    }
}