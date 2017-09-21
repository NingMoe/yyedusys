using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Api.Services
{
    public class ApplyBuyHoursService
    {
        private static readonly log4net.ILog logs = Comm.Helper.LogHelper.GetInstance();

        /// <summary>
        /// 确认缴费成功
        /// </summary>
        /// <param name="applyBuyHours"></param>
        public static bool SetPayRecordAudited(dynamic applyBuyHours)
        {

            int applyId = Convert.ToInt32(applyBuyHours.ApplyID);
            System.Data.IDbConnection connection = Comm.Helper.DapperHelper.Instance;

            using (connection)
            {
                connection.Open();
                System.Data.IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    Sys.Models.ApplyBuyHours model = DapperHelper.Instance.Get<Sys.Models.ApplyBuyHours>(applyId);

                    //设置为成功
                    var sql = "UPDATE [dbo].[ApplyBuyHours] SET [Status] =1 WHERE ApplyID=@ApplyID";
                    connection.Execute(sql, new { ApplyID = applyId }, transaction);

                    var isExistClassHoursNumber = Services.ClassHoursNumberService.IsExist(Convert.ToInt32(applyBuyHours.VenueID), model.StudentID, model.CoachID);
                    if (isExistClassHoursNumber)
                    {
                        var updateClassHoursNumberSql = @"update ClassHoursNumber set ClassNumber=ClassNumber+@classNumber 
                            where StudentID=@studentID and CoachID=@coachID and VenueID=@venueID";
                        //修改学生课时数量
                        int updateClassHoursNumber = connection.Execute(updateClassHoursNumberSql, new
                        {
                            classNumber = model.ClassNumber,
                            studentID = model.StudentID,
                            coachID = model.CoachID,
                            venueID = model.VenueID,
                        }, transaction);
                    }
                    else
                    {
                        connection.Insert(new Sys.Models.ClassHoursNumber()
                        {
                            StudentID = model.StudentID,
                            CoachID = model.CoachID,
                            VenueID = model.VenueID,
                            ClassNumber = model.ClassNumber,
                            AddTime = DateTime.Now,
                        }, transaction);
                    }

                    connection.Insert(new Sys.Models.ClassHoursDetailed()
                    {
                        AddTime = DateTime.Now,
                        StudentID = model.StudentID,
                        CoachID = model.CoachID,
                        VenueID = model.VenueID,
                        CMoney = model.PaidMoney,
                        DNumber = model.ClassNumber,
                        DType = 1,
                        PKType = model.PKType,
                        Remark = "后台确认购买课时缴费成功"
                    }, transaction);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    logs.Error("确认缴费失败", ex);
                    transaction.Rollback();
                    return false;
                }
            }
        }

    }
}