﻿using System;
namespace YY.Edu.Sys.Api.Models
{
	/// <summary>
	/// Student:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Student
	{
		public Student()
		{}
		#region Model
		private int _studentid;
		private string _username;
		private string _pwd;
		private string _fullname;
		private string _nickname;
		private string _mobile;
		private string _address;
		private string _parentfullname;
		private string _parentmobile;
		private string _headurl;
		private int? _venueid;
		private DateTime? _birthdate;
		/// <summary>
		/// 学生表主键ID
		/// </summary>
		public int StudentID
		{
			set{ _studentid=value;}
			get{return _studentid;}
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
		/// 姓名
		/// </summary>
		public string FullName
		{
			set{ _fullname=value;}
			get{return _fullname;}
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
		/// 手机号
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
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
		/// 家长姓名
		/// </summary>
		public string ParentFullName
		{
			set{ _parentfullname=value;}
			get{return _parentfullname;}
		}
		/// <summary>
		/// 家长的手机号
		/// </summary>
		public string ParentMobile
		{
			set{ _parentmobile=value;}
			get{return _parentmobile;}
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
		/// 场馆ID
		/// </summary>
		public int? VenueID
		{
			set{ _venueid=value;}
			get{return _venueid;}
		}
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? BirthDate
		{
			set{ _birthdate=value;}
			get{return _birthdate;}
		}
		#endregion Model

	}
}
