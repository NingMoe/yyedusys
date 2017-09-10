using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Api.Services
{
    public class PCLoginService
    {
        private static readonly log4net.ILog logs = Comm.Helper.LogHelper.GetInstance();

        /// <summary>
        /// 简易验证登录是否成功
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool VenueLogin(string userName)
        {

            try
            {

                var sql = @"SELECT [Id],[NameIdentifier],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName] FROM [dbo].[AspNetUsers] where UserName=@userName";
                var result = DapperHelper.Instance.Query<Sys.Models.AspNetUsers>(sql, new
                {
                    username = userName
                });

                if (result == null || result.Count() == 0)
                    return false;

                var venueSql = "SELECT COUNT([VenueID]) FROM[SportsDB].[dbo].[Venue] WHERE [UserId]=@UserId";
                var venueResult = DapperHelper.Instance.Query<int>(venueSql, new
                {
                    UserId = result.FirstOrDefault().Id
                });

                return (venueResult.FirstOrDefault() > 0);

            }
            catch (Exception ex)
            {
                logs.Error("登录失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Sys.Models.PCLoginUser GetMe(string email)
        {

            try
            {

                var sql = @"SELECT [Id],[NameIdentifier],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName] FROM [dbo].[AspNetUsers] where UserName=@userName";
                var result = DapperHelper.Instance.QueryFirst<Sys.Models.AspNetUsers>(sql, new
                {
                    username = email
                });

                if (result == null)
                    throw new YY.Edu.Sys.Comm.YYException.YYException("用户不存在");


                var venueSql = "SELECT [VenueID],[CityID],[UserName],[Pwd],[VenueCode],[VenueName],[VenueAddress],[LinkMan],[LinkManMobile],[LinkManWX],[VenueFax],[LegalPerson],[CardNumber],[AddTime],[BusinessLicense],[LogoUrl],[AddUser],[Status],[SystemRoleIDS],[UserId] FROM[SportsDB].[dbo].[Venue] WHERE [UserId]=@UserId";
                var venueResult = DapperHelper.Instance.QueryFirst<Sys.Models.Venue>(venueSql, new
                {
                    UserId = result.Id
                });

                Sys.Models.PCLoginUser login = new Sys.Models.PCLoginUser()
                {
                    Users = result as Sys.Models.AspNetUsers,
                    VenueInfo = venueResult as Sys.Models.Venue
                };

                return login;
            }
            catch (Exception ex)
            {
                logs.Error("登录失败", ex);
                throw new YY.Edu.Sys.Comm.YYException.YYException("用户不存在");
            }
        }

    }
}