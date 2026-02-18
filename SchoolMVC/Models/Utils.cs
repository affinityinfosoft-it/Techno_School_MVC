using BussinessObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;


namespace SchoolMVC.Models
{
    public class Utils
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static string api = ConfigurationManager.AppSettings["SmsApi"].Replace("MIRAJUL", "&");
        static string emailFrom = ConfigurationManager.AppSettings["EmailFrom"];
        static string emailHost = ConfigurationManager.AppSettings["EmailHost"];
        static string emailUID = ConfigurationManager.AppSettings["EmailUID"];
        static string emailPWD = ConfigurationManager.AppSettings["EmailPWD"];
        static string emailCC = ConfigurationManager.AppSettings["EmailCC"];
        static string emailBCC = ConfigurationManager.AppSettings["EmailBCC"];

       
        public static bool sendEmail(string emailTo, string subject, string message, bool sendCC, bool sendBCC)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                MailMessage mailmessage = new MailMessage(emailFrom, emailTo)
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                if (sendCC && !string.IsNullOrWhiteSpace(emailCC))
                    mailmessage.CC.Add(emailCC);

                if (sendBCC && !string.IsNullOrWhiteSpace(emailBCC))
                    mailmessage.Bcc.Add(emailBCC);

                SmtpClient smtp = new SmtpClient("smtp.office365.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailUID, emailPWD)
                };

                smtp.Send(mailmessage);
                return true;
            }
            catch (Exception ex)
            {
                // LOG ex.ToString()
                return false;
            }
        }

        public static SMSBO sendSMS(string mobileno, string msgtxt)
        {
            var url = string.Format(api, mobileno.Trim(), msgtxt.Trim());
            try
            {
                WebRequest request = HttpWebRequest.Create(url.Trim());
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream s = (Stream)response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(s))
                        {
                            string smsResponse = readStream.ReadToEnd();
                            return new SMSBO() { mobileNo = mobileno.Trim(), trackingNo = smsResponse.Substring(2, 6), remarks = "Success", msg = msgtxt };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new SMSBO() { mobileNo = mobileno.Trim(), remarks = "Fail", msg = msgtxt };
            }
        }

        public static async Task<SmsResponse> SendSmsAsync(SmsGatewayModel request)
        {
            request.UserId = ConfigurationManager.AppSettings["SMS_UserId"];
            request.Password = ConfigurationManager.AppSettings["SMS_Password"];
            request.SenderID = ConfigurationManager.AppSettings["SMS_SenderID"];
            var jsonContent = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://sms.ommdigitalsolution.com/Api/smsapi/SendSms", content);
            var responseString = await response.Content.ReadAsStringAsync();

            
            return JsonConvert.DeserializeObject<SmsResponse>(responseString);
        }

        public static DateTime GetIndiaTime()
        {
            TimeZoneInfo indiaZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indiaZone);
        }
        public static string GenerateSecureRandomCode()
        {
            // Generates a secure 6-digit numeric OTP
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
        
                int value = BitConverter.ToInt32(bytes, 0) & int.MaxValue;
                int otp = value % 1000000;
        
                // Always return 6 digits
                return otp.ToString("D6");
            }
        }

        #region MVCUtils

        public static string FinancialCalender(Int32 MonthId, string Session){
            string[] Sessions = Session.Split('-');
            var startSession = Convert.ToInt32(Sessions[0]);
            var year = startSession;
            MonthId = MonthId <= 9 ? MonthId + 3 : MonthId - 9;

            if (MonthId < 4) year = year + 1;    
            var lastDayOfMonth = DateTime.DaysInMonth(year, MonthId);
            var IndMonth = MonthId.ToString();
            if(MonthId < 10){
                IndMonth = "0" + MonthId;
            }

            return lastDayOfMonth + "/" + IndMonth + "/" + year;
        }
   
        #endregion  MVCUtils


    }
}