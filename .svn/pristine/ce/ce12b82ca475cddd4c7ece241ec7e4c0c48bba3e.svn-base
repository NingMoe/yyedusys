using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;

namespace YY.Edu.Sys.Api.Services
{
    public class VenueService
    {

        public bool Create()
        {

            return true;

            //var query = Helper.DapperHelper.Instance.Query<Models.City>("select * from city where CityID=@id", new { Id = 1 });
        }

        public string GenVenueCode(YY.Edu.Sys.Models.Venue venue)
        {

            Random r = new Random();
            string code = venue.VenueID.ToString().PadLeft(6, Convert.ToChar(r.Next(1, 9)));

            while (true)
            {
                IList<IPredicate> predList = new List<IPredicate>();
                predList.Add(Predicates.Field<YY.Edu.Sys.Models.Venue>(f => f.CityID, Operator.Le, venue.VenueCode));
                int count = Comm.Helper.DapperHelper.Instance.Count<YY.Edu.Sys.Models.Venue>(predList);
                if (count == 0)
                {
                    return code;
                }
            }

        }

    }
}