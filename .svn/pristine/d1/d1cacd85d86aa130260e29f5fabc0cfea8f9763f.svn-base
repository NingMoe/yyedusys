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

        /// <summary>
        /// 检测场馆编码是否生成
        /// </summary>
        /// <param name="venudId"></param>
        /// <returns></returns>
        public static bool IsExistVenueCode(int venudId)
        {

            if (venudId <= 0)
                throw new Comm.YYException.YYException("参数错误");

            var result = Comm.Helper.DapperHelper.Instance.Get<Sys.Models.Venue>(venudId);

            if (result == null)
                return false;

            return !string.IsNullOrWhiteSpace(result.VenueCode);
        }

        /// <summary>
        /// 生成场馆编码
        /// </summary>
        /// <param name="venue"></param>
        /// <returns></returns>
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