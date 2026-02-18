using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SchoolMVC
{
    public class AuthenticationDB : DbContext
    {
        public AuthenticationDB()
            : base("School_DbEntity")
        {

        }
        //public DbSet<StudentRegistration_ST> User { get; set; }
        public DbSet<StudentRegistration_ST> StudentRegistration_ST { get; set; }

    }
}