﻿using System;
namespace YY.Edu.Sys.Api.Models
{
	/// <summary>
	/// Coach:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Coach
	{
		public Coach()
		{}
		#region Model
		private int _coachid;
		private string _fullname;
		private string _cardpositiveurl;
		private string _cardreverseurl;
		private string _username;
		private string _pwd;
		private int? _state=0;
		private string _introduce;
		private string _nickname;
		private string _headurl;
		private string _address;
		private string _mobile;
		private int? _sex=1;
		private int? _venueid;
		/// <summary>
		/// 
		/// </summary>
		public int CoachID
		{
			set{ _coachid=value;}
			get{return _coachid;}
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string FullName
		{
			set{ _fullname=value;}
			get{return _fullname;}
		}
		/// <summary>
		/// 身份证正面图片地址
		/// </summary>
		public string CardPositiveUrl
		{
			set{ _cardpositiveurl=value;}
			get{return _cardpositiveurl;}
		}
		/// <summary>
		/// 身份证反面图片地址
		/// </summary>
		public string CardReverseUrl
		{
			set{ _cardreverseurl=value;}
			get{return _cardreverseurl;}
		}
		/// <summary>
		/// 用户名（手机号）
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Pwd
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		/// <summary>
		/// 0待审核1审核通过，2删除
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 介绍
		/// </summary>
		public string Introduce
		{
			set{ _introduce=value;}
			get{return _introduce;}
		}
		/// <summary>
		/// 昵称
		/// </summary>
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 头像
		/// </summary>
		public string HeadUrl
		{
			set{ _headurl=value;}
			get{return _headurl;}
		}
		/// <summary>
		/// 住址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 手机号
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 性别 1男0女
		/// </summary>
		public int? Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 场馆ID
		/// </summary>
		public int? VenueID
		{
			set{ _venueid=value;}
			get{return _venueid;}
		}
		#endregion Model

	}
}

