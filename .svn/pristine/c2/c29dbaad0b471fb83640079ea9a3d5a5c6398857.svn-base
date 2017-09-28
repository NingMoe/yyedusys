using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Filters
{
    public class SessionUserParameterAttribute : System.Web.Mvc.ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            const string key = "venueId";

            //if (filterContext.ActionParameters.ContainsKey(key))
            {
                Models.LoginUser loginUser = (filterContext.HttpContext.Session["venue"] as Models.LoginUser);//为Action设置参数
                if (loginUser == null)
                {
                    //todo 
                    //filterContext.HttpContext.Response.Redirect("/");
                    filterContext.ActionParameters[key] = 1;// loginUser.VenueInfo.VenueID;
                }
                else
                {
                    filterContext.ActionParameters[key] = loginUser.VenueInfo.VenueID;
                }
            }

            base.OnActionExecuting(filterContext);
        }

    }
}