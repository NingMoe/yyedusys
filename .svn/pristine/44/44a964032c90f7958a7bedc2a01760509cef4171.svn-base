using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Services
{
    public class LoginService
    {

        public string Domain { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public LoginService()
        {
        }

        public LoginService(string domain, string userName, string passWord)
        {
            Domain = domain;
            UserName = userName;
            PassWord = passWord;
        }

        public bool Login()
        {

            //todo 检查输入

            var studentDomain = System.Configuration.ConfigurationManager.AppSettings["StudentDomain"];
            var coachDomain = System.Configuration.ConfigurationManager.AppSettings["CoachDomain"];
            var venueDomain = System.Configuration.ConfigurationManager.AppSettings["VenueDomain"];
            var manageDomain = System.Configuration.ConfigurationManager.AppSettings["ManageDomain"];

            if (Domain.ToLower() == studentDomain)
            {
                return Services.StudentService.StudentLogin(UserName);
            }
            else if (Domain.ToLower() == coachDomain)
            {

            }
            else if (Domain.ToLower() == venueDomain)
            {
                return Services.VenueService.VenueLogin(UserName);
            }
            else if (Domain.ToLower() == manageDomain)
            {

            }

            return false;

        }

    }
}