using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Areas.StudentPortal.Models
{
    public class ResultWithData<T>
    {
        public T Data { get; set; }
        public List<T> List { get; set; }
        public bool IsValid { get; set; }
        public string SuccessMsg { get; set; }
        public string ErrorMsg { get; set; }
        public string StackTrace { get; set; }
    }

    public class ResultWithoutData
    {
        public bool IsValid { get; set; }
        public string SuccessMsg { get; set; }
        public string ErrorMsg { get; set; }
        public int StatusCode { get; set; }
        public string StackTrace { get; set; }

    }

}