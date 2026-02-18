using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class DashBoardBO
    {
        public DashBoardBO()
        {
            DashboardList = new List<clsBarChart>();
            AttendanceList = new List<clsAttendanceChart>();
        }
        public long TOTALSTUDENTS { get; set; }
        public long TOTALMALE { get; set; }
        public long TOTALFEMALE { get; set; }
        public string Notice { get; set; }
        public decimal TOTALPAYMENTS { get; set; }
        public long TotalTC { get; set; }
        public long TotalDropOut { get; set; }
        public List<clsBarChart> DashboardList { get; set; }
        public List<clsAttendanceChart> AttendanceList { get; set; }
        public long? dash_SchoolId { get; set; }
        public long? dash_SessionId { get; set; }
        

    }
    public class clsBarChart
    {
        public string CM_CLASSNAME { get; set; }
        public long NOOFSTUDENTS { get; set; }
        
    }
}

public class clsAttendanceChart
    {
        public string CLASSNAME { get; set; }
        public int TOTALSTUDENTS { get; set; }
        public int PRESENTCOUNT { get; set; }
        
    }