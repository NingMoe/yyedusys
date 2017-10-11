using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Api.Services
{
    public class ManageService
    {

        private static readonly log4net.ILog logs = Comm.Helper.LogHelper.GetInstance();

        public static bool ManageLogin(string userName)
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

                return true;

            }
            catch (Exception ex)
            {
                logs.Error("登录失败", ex);
                return false;
            }
        }


    }
}