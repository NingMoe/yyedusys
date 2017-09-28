using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Services
{
    public class StudentService
    {

        private static readonly log4net.ILog logs = Comm.Helper.LogHelper.GetInstance();

        /// <summary>
        /// 学生是否存在
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static bool IsExist(Sys.Models.Student student)
        {

            if (student == null)
                throw new Comm.YYException.YYException("参数不能为空");

            if (string.IsNullOrWhiteSpace(student.UserName) || student.VenueID <= 0)
                throw new Comm.YYException.YYException("参数不能为空");

            var sql = "select count(studentID) from student where username=@username";
            var count = Comm.Helper.DapperHelper.Instance.Query<int>(sql,
                new
                {
                    username = student.UserName
                });

            return (count.FirstOrDefault() > 0);

        }

        /// <summary>
        /// 通过用户名查找学生
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static Sys.Models.Student FindStudentByUserName(string userName)
        {

            if (string.IsNullOrWhiteSpace(userName))
                throw new Comm.YYException.YYException("参数不能为空");

            var sql = @"SELECT [StudentID],[UserName],[FullName],[NickName],[Mobile],[Address],[ParentFullName],[ParentMobile],[HeadUrl],[VenueID],[BirthDate] FROM[dbo].[Student] where username=@username";
            var result = Comm.Helper.DapperHelper.Instance.Query<Sys.Models.Student>(sql,
                new
                {
                    username = userName
                });

            return result.FirstOrDefault();

        }

        /// <summary>
        /// 检查学生与学校是否已经绑定
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static bool IsExistInVenue(Sys.Models.Student student)
        {

            var sql = @"select count(sv.svid) from student_venue as sv join Student as s on s.StudentID=sv.StudentID 
where s.UserName=@userName and sv.VenueID=@venueID";

            var count = Comm.Helper.DapperHelper.Instance.Query<int>(sql,
                new
                {
                    userName = student.UserName,
                    venueID = student.VenueID
                });

            return (count.FirstOrDefault() > 0);

        }

        /// <summary>
        /// 为场馆添加一个学生
        /// </summary>
        /// <param name="student">学生信息</param>
        /// <returns></returns>
        public static bool Create(Sys.Models.Student student)
        {

            //todo 并发暂时不考虑

            /*
             检查场馆code是否存在
             检查学生信息是否存在
             检查学生场馆绑定关系是否存在
             插入绑定关系
             插入学生信息和绑定关系
                
             */

            try
            {
                bool isExistVenueCode = VenueService.IsExistVenueCode(student.VenueID.Value);

                if (!isExistVenueCode)
                    throw new Comm.YYException.YYException("场馆编码不存在");

                bool isExistStudent = IsExist(student);

                if (isExistStudent)//学生已经存在
                {
                    bool isExistStudentVenue = IsExistInVenue(student);

                    if (isExistStudentVenue)
                        throw new Comm.YYException.YYException(student.FullName + "学生已经绑定");

                    var student_info = FindStudentByUserName(student.UserName);

                    var student_venue_result = Comm.Helper.DapperHelper.Instance.Insert(new Sys.Models.Student_Venue()
                    {
                        AddTime = DateTime.Now,
                        StudentID = student_info.StudentID,
                        VenueID = student.VenueID,
                    });
                    return student_venue_result > 0;
                }
                else//学生不存在
                {
                    return ImportStudent(student);
                }
            }
            catch (Comm.YYException.YYException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logs.Error("学生添加失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 插入学生信息和绑定信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private static bool ImportStudent(Sys.Models.Student student)
        {

            System.Data.IDbConnection connection = Comm.Helper.DapperHelper.Instance;
            connection.Open();
            System.Data.IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                int student_id = connection.Insert<Sys.Models.Student>(student, transaction);
                var student_venue_result = connection.Insert<Sys.Models.Student_Venue>(new Sys.Models.Student_Venue()
                {
                    AddTime = DateTime.Now,
                    StudentID = student_id,
                    VenueID = student.VenueID,
                }, transaction);

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                logs.Error("学生添加失败", ex);
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