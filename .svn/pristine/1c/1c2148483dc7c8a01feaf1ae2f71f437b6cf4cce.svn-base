using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    public class BaseCoachController : BaseController
    {
        // GET: BaseCoach
        private string studentDomain = System.Configuration.ConfigurationManager.AppSettings["CoachDomain"];


        public async Task<Models.LoginCoachInfo> Me()
        {
            await GetMe();

            Models.CoachResponse cr = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CoachResponse>(Session[studentDomain].ToString());
            return new Models.LoginCoachInfo()
            {
                CoachInfo = cr
            };
        }

        private async Task GetMe()
        {
            await base.LoginSuccess(studentDomain, base.OpenId, "");

            await base.GetMineInfo(studentDomain, base.AccessToken, base.OpenId);
        }

    }
}