﻿using System;
using System.ComponentModel.DataAnnotations;

namespace YY.Edu.Sys.Models
{
  
    /// <summary>
    /// VenueNotice:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public partial class VenueNotice
    {
        public VenueNotice()
        { }
        #region Model
        private int _noticeid;
        private int _userid;
        private string _username;
        private int _noticetype;
        private string _noticemsg;
        private DateTime? _addtime = DateTime.Now;
        private int? _noticerange;
        private int _venueid;
        private DateTime? _sendtime = DateTime.Now;

        //private int _source;
        //private int _studentID;
        //private int _coachID;
        //private string _noticeTitle;



        /// <summary>
        /// 
        /// </summary>
        public int NoticeId
        {
            set { _noticeid = value; }
            get { return _noticeid; }
        }
        /// <summary>
        /// 添加公告的后台用户
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 添加公告的后台用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 公告类型 1公告 2活动 3微信消息 4短信消息 5提醒
        /// </summary>
        [Required(ErrorMessage = "公告类型不能为空")]
        public int NoticeType
        {
            set { _noticetype = value; }
            get { return _noticetype; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "公告内容不能为空")]
        public string NoticeMsg
        {
            set { _noticemsg = value; }
            get { return _noticemsg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 通知范围 1场馆 2教练 4学生 3场馆教练 5场馆学生 6教练学生 7全部
        /// </summary>
        [Required(ErrorMessage = "公告通知范围不能为空")]
        public int? NoticeRange
        {
            set { _noticerange = value; }
            get { return _noticerange; }
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [Required(ErrorMessage = "场馆编号不能为空")]
        public int VenueID
        {
            set { _venueid = value; }
            get { return _venueid; }
        }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime? SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }

        /// <summary>
        /// 0 无效 1有效 2发送成功
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 来源，1场馆，2运营平台，3场馆教练 
        /// </summary>
        public int source { get;set;  }

        /// <summary>
        /// 学生ID（用于直接通知的，公共通知这个字段为0）
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 教练ID（用于直接通知的，公共通知这个字段为0）
        /// </summary>
        public int CoachID { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string NoticeTitle { get; set; }
        #endregion Model

    }
}

