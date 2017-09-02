using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Api.Services
{
    public class CoachService
    {

        private static readonly log4net.ILog logs = Comm.Helper.LogHelper.GetInstance();

        /// <summary>
        /// 获取教练指定日期中的授课情况(按照日期分组)
        /// </summary>
        /// <param name="coachID">教练编号</param>
        /// <param name="curriculumStartDate">开始日期</param>
        /// <param name="curriculumEndDate">结束日期</param>
        /// <returns>列表 {日期,授课时间-状态-场馆-校区,}</returns>
        public static IEnumerable<dynamic> GetCoachTeachingSche(int coachID, string curriculumStartDate, string curriculumEndDate)
        {

            var sql = @"select CurriculumDate,CurriculumStr = ( STUFF(( SELECT    ',' + CurriculumBeginTime+'-'+CurriculumEndTime+'|'+CONVERT(varchar(1), [State])+'|'+CONVERT(varchar(1), VenueID)+'|'+CONVERT(varchar(1), CampusID) FROM TeachingSchedule WHERE  CurriculumDate = t.CurriculumDate FOR XML PATH('') ), 1, 1, '') )  
from TeachingSchedule as t where CoachID = @CoachID and CurriculumDate>= @CurriculumDateStart and CurriculumDate< @CurriculumDateEnd
group by CurriculumDate";

            try
            {

                var result = DapperHelper.Instance.Query(sql,
                    new
                    {
                        CoachID = coachID,
                        CurriculumDateStart = curriculumStartDate,
                        CurriculumDateEnd = curriculumEndDate
                    });

                return result;
            }
            catch (Exception ex)
            {
                logs.Error("获取教练排课失败", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取教练指定日期中的授课情况
        /// </summary>
        /// <param name="coachID">教练编号</param>
        /// <param name="curriculumStartDate">开始日期</param>
        /// <param name="curriculumEndDate">结束日期</param>
        /// <returns>列表 {日期,授课时间}</returns>
        public static IEnumerable<dynamic> GetCoachTeachingScheByDay(int coachID, string curriculumStartDate, string curriculumEndDate)
        {

            var sql = @"select CurriculumDate, CurriculumBeginTime,CurriculumEndTime, [State]
from TeachingSchedule where CoachID = @CoachID and CurriculumDate>= @CurriculumDateStart and CurriculumDate< @CurriculumDateEnd ";

            try
            {
                var result = DapperHelper.Instance.Query(sql,
                    new
                    {
                        CoachID = coachID,
                        CurriculumDateStart = curriculumStartDate,
                        CurriculumDateEnd = curriculumEndDate
                    });

                return result;
            }
            catch (Exception ex)
            {
                logs.Error("获取教练排课失败", ex);
                return null;
            }
        }


        /// <summary>
        /// 教练是否存在
        /// </summary>
        /// <param name="coach"></param>
        /// <returns></returns>
        public static bool IsExist(Sys.Models.Coach coach)
        {

            if (coach == null)
                throw new Comm.YYException.YYException("参数不能为空");

            if (string.IsNullOrWhiteSpace(coach.UserName) || coach.VenueID <= 0)
                throw new Comm.YYException.YYException("参数不能为空");

            var sql = "select count([CoachID]) from [Coach] where [UserName]=@userName";
            var count = Comm.Helper.DapperHelper.Instance.Query<int>(sql,
                new
                {
                    username = coach.UserName
                });

            return (count.FirstOrDefault() > 0);

        }

        /// <summary>
        /// 通过用户名查找教练
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static Sys.Models.Coach FindCoachByUserName(string userName)
        {

            if (string.IsNullOrWhiteSpace(userName))
                throw new Comm.YYException.YYException("参数不能为空");

            var sql = @"SELECT [CoachID],[FullName],[CardPositiveUrl],[CardReverseUrl],[UserName],[State],[Introduce],[NickName],[HeadUrl],[Address],[Mobile],[Sex],[VenueID] FROM [dbo].[Coach] where username=@username";
            var result = Comm.Helper.DapperHelper.Instance.Query<Sys.Models.Coach>(sql,
                new
                {
                    username = userName
                });

            return result.FirstOrDefault();

        }

        /// <summary>
        /// 检查教练与教练是否已经绑定
        /// </summary>
        /// <param name="coach"></param>
        /// <returns></returns>
        public static bool IsExistInVenue(Sys.Models.Coach coach)
        {

            var sql = @"select count(cv.CVID) from Coach_Venue as cv join Coach as c on c.CoachID=cv.CoachID
where c.UserName=@userName and cv.VenueID=@venueID";

            var count = Comm.Helper.DapperHelper.Instance.Query<int>(sql,
                new
                {
                    userName = coach.UserName,
                    venueID = coach.VenueID
                });

            return (count.FirstOrDefault() > 0);

        }

        /// <summary>
        /// 为场馆添加一个
        /// </summary>
        /// <param name="coach">教练信息</param>
        /// <returns></returns>
        public static bool Create(Sys.Models.Coach coach)
        {
            //todo 并发暂时不考虑
            try
            {
                bool isExistVenueCode = VenueService.IsExistVenueCode(coach.VenueID.Value);

                if (!isExistVenueCode)
                    throw new Comm.YYException.YYException("场馆编码不存在");

                bool isExistCoach = IsExist(coach);

                if (!isExistCoach)
                {
                    return ImportCoach(coach);
                }
                else
                {
                    bool isExistStudentVenue = IsExistInVenue(coach);
                    if (isExistStudentVenue)
                        throw new Comm.YYException.YYException(coach.FullName + "教练已经绑定");

                    var coach_info = FindCoachByUserName(coach.UserName);

                    var coach_venue_result = Comm.Helper.DapperHelper.Instance.Insert(new Sys.Models.Coach_Venue()
                    {
                        AddTime = DateTime.Now,
                        CoachID = coach_info.CoachID,
                        VenueID = coach.VenueID,
                    });
                    return coach_venue_result > 0;
                }
            }
            catch (Comm.YYException.YYException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logs.Error("教练添加失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 插入教练信息和绑定信息
        /// </summary>
        /// <param name="coach"></param>
        /// <returns></returns>
        private static bool ImportCoach(Sys.Models.Coach coach)
        {

            System.Data.IDbConnection connection = Comm.Helper.DapperHelper.Instance;
            connection.Open();
            System.Data.IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                int coach_id = connection.Insert<Sys.Models.Coach>(coach, transaction);
                var student_venue_result = connection.Insert<Sys.Models.Coach_Venue>(new Sys.Models.Coach_Venue()
                {
                    AddTime = DateTime.Now,
                    CoachID = coach_id,
                    VenueID = coach.VenueID,
                }, transaction);

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                logs.Error("教练添加失败", ex);
                transaction.Rollback();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}