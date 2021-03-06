﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Models
{
    public partial class WxUserInfo
    {
        public WxUserInfo()
        { }
        #region Model
        private int _wxinfoid;
        private string _city;
        private string _country;
        private string _headimgurl;
        private string _nickname;
        private string _openid;
        private string _province;
        private bool _sex;
        private DateTime? _addtime;
        /// <summary>
        /// 
        /// </summary>
        public int WxInfoID
        {
            set { _wxinfoid = value; }
            get { return _wxinfoid; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl
        {
            set { _headimgurl = value; }
            get { return _headimgurl; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// openid
        /// </summary>
        public string OpenId
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 省
        /// </summary>
        public string Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 性别 1男 0女
        /// </summary>
        public bool Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 关注时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }

        /// <summary>
        /// 1关注 ...
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 取关时间
        /// </summary>
        public DateTime UnFollowTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 纬度，浮点数，范围为90 ~ -90
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 经度，浮点数，范围为180 ~ -180
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 速度，以米/每秒计
        /// </summary>
        public decimal Speed { get; set; }

        /// <summary>
        /// 位置精度
        /// </summary>
        public decimal Accuracy { get; set; }
        #endregion Model

    }
}
