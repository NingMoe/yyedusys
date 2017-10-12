using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YY.Edu.Sys.Api.Controllers
{
    public class BaseController : ApiController
    {

        /// <summary>
        /// 获取验证的提示信息
        /// </summary>
        public string ValidationMsg
        {
            get
            {
                string error = "";
                foreach (var item in ModelState.Values)
                {
                    foreach (var sub_item in item.Errors)
                    {
                        error += sub_item.ErrorMessage + ",";
                    }
                    error = error.TrimEnd(',');
                    error += "\n";
                }

                return error;
            }
        }

    }
}
