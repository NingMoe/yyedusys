﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Data;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Containers;
using YY.Edu.Sys.Comm.Helper;
using Newtonsoft.Json.Linq;

namespace YY.Edu.Sys.Test
{
    [TestClass]
    public class UnitTest1
    {

        public void TestMethod1()
        {

            //https://github.com/tmsmith/Dapper-Extensions
            //https://github.com/tmsmith/Dapper-Extensions/wiki/Predicates
            try
            {
                string _connectionString = "Data Source=.;Initial Catalog=SportsDB;Persist Security Info=True;User ID=sa;Password=11111111;Integrated Security=True";
                using (System.Data.IDbConnection cn = new System.Data.SqlClient.SqlConnection(_connectionString))
                {
                    cn.Open();

                    //添加
                    //var multiKey = cn.Insert<City>(new City() { CityName = "北京5" });
                    //Console.WriteLine(multiKey);

                    //分页

                    //分页查询
                    IList<ISort> sort = new List<ISort>();
                    sort.Add(new Sort { PropertyName = "CityID", Ascending = false });

                    long allRowsCount = 0;
                    //cn.GetPage<City>(1, 10, out allRowsCount, new { ID = 1 }, sort);
                    var predicate_page = Predicates.Field<City>(f => f.CityName, Operator.Like, "%北京%");
                    IEnumerable<City> list_page = cn.GetList<City>(predicate_page);

                    var result = cn.GetPage<City>(predicate_page, sort, 1, 1);
                    allRowsCount = cn.Count<City>(predicate_page);
                    Console.WriteLine(result.AsList().Count);

                    //查询
                    var predicate = Predicates.Field<City>(f => f.CityName, Operator.Eq, "北京2");
                    IEnumerable<City> list = cn.GetList<City>(predicate);
                    Console.WriteLine(list.AsList()[0].CityName);

                    //查询
                    int cityId = 1;
                    City person = cn.Get<City>(cityId);

                    person.CityName = "上海";
                    cn.Update(person);

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void TestMethod2()
        {

            string _connectionString = "Data Source=.;Initial Catalog=SportsDB;Persist Security Info=True;User ID=sa;Password=11111111;Integrated Security=True";
            System.Data.IDbConnection connection = new System.Data.SqlClient.SqlConnection(_connectionString);

            var sql = @"select * from Venue inner join City on Venue.CityID=City.CityID where venue.venueid=@id";

            //var result = connection.Query<Venue, City, Venue>(sql,
            //                        (product, users) =>
            //                        {
            //                            product. = users; return product;
            //                        }, splitOn: "UserName");

            var result = connection.Query(sql, new { id = 1 });
            Console.WriteLine(result);

        }

        public void TestMethon3()
        {

            NewCity n = new NewCity()
            {
                CityID = 1,
                CityName = "保定",
                NewName = "新北京"
            };

            City c = new City();
            c = n;

            Console.WriteLine(c.CityName);

            //string _connectionString = "Data Source=.;Initial Catalog=SportsDB;Persist Security Info=True;User ID=sa;Password=11111111;Integrated Security=True";
            //System.Data.IDbConnection connection = new System.Data.SqlClient.SqlConnection(_connectionString);

            System.Data.IDbConnection connection = Comm.Helper.DapperHelper.Instance;
            connection.Open();
            System.Data.IDbTransaction transaction = connection.BeginTransaction();

            List<M_User> user = new List<M_User>()
            {
                new M_User() {  UserName="小赵",Pwd="111111" },
                new M_User() {  UserName="小赵1",Pwd="11111111111111111111111111111111" },
            };
            try
            {
                connection.Insert<M_User>(user, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void TestMethod4()
        {
            DataTable dt = new DataTable();

            string strFileName = @"D:\junfu.zhao\project\yyedusys\src\YY.Edu.Sys.Admin\App_Data\学生.xls";
            HSSFWorkbook hssfworkbook;

            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        if (row.GetCell(j).ToString().Contains("/"))
                        {
                            string[] a = row.GetCell(j).ToString().Split('/');

                            dataRow[j] = "20" + a[2] + "-" + a[0] + "-" + a[1];
                        }
                        else
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }
                    else
                    {
                        dataRow[j] = null;
                    }
                }

                dt.Rows.Add(dataRow);
            }

        }

        public void SendTemplateMessageTestForBook()
        {
            var _appId = "wx03ea2f7f93b7cf96";
            var _appSecret = "609bd314b63811293cec1d9adb84e699";

            //Senparc.Weixin.MP.Containers.AccessTokenContainer.Register(_appId, _appSecret);

            var accessToken = AccessTokenContainer.TryGetAccessToken(_appId, _appSecret);

            var openId = "ozLW4wHYTcApj55HIUT0o8Qdet6U";//消息目标用户的OpenId
            var templateId = "7VMaAiEXaI9WabE8z5_oRTtUjimclgeLc6XuwgIw__o"; //7VMaAiEXaI9WabE8z5_oRTtUjimclgeLc6XuwgIw__o
            templateId = "jGWhXy1FpVQP2GaEki1ReAQAcbE6KyWEp5PmiNlnvhk";

            var data = new
            {
                first = new TemplateDataItem("您的订单已经支付"),
                keyword1 = new TemplateDataItem("飞翔的企鹅"),
                keyword2 = new TemplateDataItem("123456789"),
                keyword3 = new TemplateDataItem("1000", "#ff0000"),//显示为红色
                keyword4 = new TemplateDataItem("购买课时"),
                remark = new TemplateDataItem("更详细信息，官方网站（http://www.baidu.com）查看！")
            };

            var result = TemplateApi.SendTemplateMessage(_appId, openId, templateId, "http://www.cnblogs.com", data);

            Assert.AreEqual(Senparc.Weixin.ReturnCode.请求成功, result.errcode);
        }

        public void T()
        {
            string token = @"{""access_token"": ""9sx2ojliKmWgoi9QzcccOUVACyFliVp1HnRUDnI8u7X-VsjqesakAtqWyzYxWxo4dXSnV374VAb5p42NOkWqLCRWNruqmzjA_WzrBHF9pBcB7Pw33hW5lAkNwwS6Y-lG8xZ0zGPdjMzIPNSPTt89u2fMfZODJ7GR_uG9pP8kEv3ef51prx-AqlWAkD9xDIqxh5ZnkMmQDhMN4bnIItmZKuhXJLbGPu0WYbFBtoKULdQ"",
  ""token_type"": ""bearer"",
  ""expires_in"": 1209599,
  ""refresh_token"": ""667a9cff7ad74447aa703da97a73114b""
}";

            //TokenInfo t = new TokenInfo()
            //{
            //    access_token = "9sx2ojliKmWgoi9QzcccOUVACyFliVp1HnRUDnI8u7X",
            //    token_type = "bearer",
            //    expires_in = 1209599,
            //    refresh_token = "667a9cff7ad74447aa703da97a73114b"
            //};
            TokenInfo tokenInfo = new TokenInfo(token);

        }

        [TestMethod]
        public void TestMethod5()
        {
            //string query = @"{""VenueID"":""1"",""WorkDate"":""2017-08"",""CoachID"":""-1"",""CurriculumBeginTime"":""2017-08"",""CurriculumId"":[""2"",""1""]}";

            //CreateCoachWageRequest oData = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateCoachWageRequest>(query);

            HttpRequestHelper RequestHelper = new HttpRequestHelper();

            //检查输入项
            string apiUrl = "http://localhost:53262/api";
            string url = string.Format("{0}/WxUserInfo/SetPosition", apiUrl);

            RequestHelper.Create(url);
            RequestHelper.WebRequest.ContentType = "application/json;charset=UTF-8";
            RequestHelper.WebRequest.Headers.Add("Accept-Encoding: gzip, deflate");
            RequestHelper.WebRequest.AutomaticDecompression = System.Net.DecompressionMethods.Deflate;

            Sys.Models.WxUserInfo userInfo = new Models.WxUserInfo()
            {
                OpenId = "ozLW4wHYTcApj55HIUT0o8Qdet6U",
                Latitude = 2222,
                Longitude = 333,
            };
            string jsonStr = JsonHelper.ToJsonStringByNewtonsoft(userInfo);

            string result = RequestHelper.PostString(jsonStr);


            JObject jo = JObject.Parse(result);
            if (Convert.ToBoolean(jo["Error"].ToString()))
                throw new Comm.YYException.YYException(jo["Msg"].ToString());

        }
    }

    public class CreateCoachWageRequest
    {
        public int VenueID { get; set; }

        public string WorkDate { get; set; }

        public List<int> CurriculumId { get; set; }
    }

    public class TokenInfo
    {

        //        {
        //  "access_token": "9sx2ojliKmWgoi9QzcccOUVACyFliVp1HnRUDnI8u7X-VsjqesakAtqWyzYxWxo4dXSnV374VAb5p42NOkWqLCRWNruqmzjA_WzrBHF9pBcB7Pw33hW5lAkNwwS6Y-lG8xZ0zGPdjMzIPNSPTt89u2fMfZODJ7GR_uG9pP8kEv3ef51prx-AqlWAkD9xDIqxh5ZnkMmQDhMN4bnIItmZKuhXJLbGPu0WYbFBtoKULdQ",
        //  "token_type": "bearer",
        //  "expires_in": 1209599,
        //  "refresh_token": "667a9cff7ad74447aa703da97a73114b"
        //}

        public TokenInfo() { }

        public TokenInfo(string token)
        {

            TokenInfo oData = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenInfo>(token);
            this.access_token = oData.access_token;
            this.token_type = oData.token_type;
            this.expires_in = oData.expires_in;
            this.refresh_token = oData.refresh_token;
        }

        /// <summary>
        /// token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// token_type
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// refresh_token
        /// </summary>
        public string refresh_token { get; set; }

    }

    public class M_User
    {

        public string UserName { get; set; }

        public string Pwd { get; set; }

    }

    public class NewCity : City
    {
        public string NewName { get; set; }
    }

    public class City
    {
        public int CityID { get; set; }

        public string CityName { get; set; }

    }

    public partial class Venue
    {
        public Venue()
        { }
        #region Model
        private int _venueid;
        private int? _cityid;
        private string _username;
        private string _pwd;
        private string _venuecode;
        private string _venuename;
        private string _venueaddress;
        private string _linkman;
        private string _linkmanmobile;
        private string _linkmanwx;
        private string _venuefax;
        private string _legalperson;
        private string _cardnumber;
        private DateTime? _addtime = DateTime.Now;
        private string _businesslicense;
        private string _logourl;
        private string _adduser;
        private int? _status = 0;
        private string _systemroleids;
        /// <summary>
        /// 主键ID
        /// </summary>
        public int VenueID
        {
            set { _venueid = value; }
            get { return _venueid; }
        }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int? CityID
        {
            set { _cityid = value; }
            get { return _cityid; }
        }
        /// <summary>
        /// 用户名（手机号）
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// 场馆代码（生成唯的一4～6位数）
        /// </summary>
        public string VenueCode
        {
            set { _venuecode = value; }
            get { return _venuecode; }
        }
        /// <summary>
        /// 场馆名称
        /// </summary>
        public string VenueName
        {
            set { _venuename = value; }
            get { return _venuename; }
        }
        /// <summary>
        /// 场馆地址
        /// </summary>
        public string VenueAddress
        {
            set { _venueaddress = value; }
            get { return _venueaddress; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }
        /// <summary>
        /// 联系人手机号
        /// </summary>
        public string LinkManMobile
        {
            set { _linkmanmobile = value; }
            get { return _linkmanmobile; }
        }
        /// <summary>
        /// 联系人微信
        /// </summary>
        public string LinkManWX
        {
            set { _linkmanwx = value; }
            get { return _linkmanwx; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string VenueFax
        {
            set { _venuefax = value; }
            get { return _venuefax; }
        }
        /// <summary>
        /// 法人姓名
        /// </summary>
        public string LegalPerson
        {
            set { _legalperson = value; }
            get { return _legalperson; }
        }
        /// <summary>
        /// 法人身份证号
        /// </summary>
        public string CardNumber
        {
            set { _cardnumber = value; }
            get { return _cardnumber; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 营业执照（图片地址）
        /// </summary>
        public string BusinessLicense
        {
            set { _businesslicense = value; }
            get { return _businesslicense; }
        }
        /// <summary>
        /// 场馆LOGO
        /// </summary>
        public string LogoUrl
        {
            set { _logourl = value; }
            get { return _logourl; }
        }
        /// <summary>
        /// 注册人
        /// </summary>
        public string AddUser
        {
            set { _adduser = value; }
            get { return _adduser; }
        }
        /// <summary>
        /// 状态0待审核，1审核通过，2删除
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 运营系统给场馆分配的权限，为空代表所有权限
        /// </summary>
        public string SystemRoleIDS
        {
            set { _systemroleids = value; }
            get { return _systemroleids; }
        }
        #endregion Model

    }

}
