using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YY.Edu.Sys.Api.Controllers
{
    [RoutePrefix("api/Coach/TeachingSchedule")]
    public class TeachingScheduleController : ApiController
    {
        // GET: api/TeachingSchedule
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TeachingSchedule/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TeachingSchedule
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TeachingSchedule/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TeachingSchedule/5
        public void Delete(int id)
        {
        }
    }
}
