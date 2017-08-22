using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Comm.Helper
{
    /// <summary>
    /// dapper 帮助类
    /// </summary>
    public class DapperHelper
    {

        //静态只读的线程辅助对象
        static readonly object syncInstance = new object();

        private static System.Data.IDbConnection instance = null;

        /// <summary>
        /// dapper实例
        /// </summary>
        /// <returns></returns>
        public static System.Data.IDbConnection Instance
        {
            get
            {
                return CreateInstance();
                //if (instance == null)
                //{
                //    lock (syncInstance)
                //    {
                //        if (instance == null)
                //        {
                //            instance = CreateInstance();
                //        }
                //    }
                //}
                //return instance;
            }
            set
            {
                instance = null;
            }
        }

        #region 初始化

        /// <summary>
        /// 创建云存储服务商实例
        /// </summary>
        /// <returns></returns>
        private static System.Data.IDbConnection CreateInstance()
        {
            // 添加
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            System.Data.IDbConnection connection = new System.Data.SqlClient.SqlConnection(conn);
            return connection;
        }

        #endregion

        /// <summary>
        /// 通用分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static PageDataView<T> GetPageData<T>(PageCriteria criteria, object param = null)
        {

            //using (Instance)
            {
                var p = new DynamicParameters();
                string proName = "ProcGetPageData";
                p.Add("TableName", criteria.TableName);
                p.Add("PrimaryKey", criteria.PrimaryKey);
                p.Add("Fields", criteria.Fields);
                p.Add("Condition", criteria.Condition);
                p.Add("CurrentPage", criteria.CurrentPage);
                p.Add("PageSize", criteria.PageSize);
                p.Add("Sort", criteria.Sort);
                p.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                //conn.Open();
                var pageData = new PageDataView<T>();
                pageData.Items = Instance.Query<T>(proName, p, commandType: CommandType.StoredProcedure).ToList();
                //conn.Close();
                pageData.TotalNum = p.Get<int>("RecordCount");
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;

            }
        }

    }
}
