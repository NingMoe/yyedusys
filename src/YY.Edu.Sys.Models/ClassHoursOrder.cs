﻿using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// ClassHoursOrder:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ClassHoursOrder
	{
		public ClassHoursOrder()
		{}
		#region Model
		private int _orderid;
		private int? _ordertype=1;
		private string _channelorderid;
		private int? _studentid;
		private decimal? _paymoney;
		private DateTime? _addtime= DateTime.Now;
		private string _payinfo;
		private int? _paytype=1;
		private int? _coachid;
		private int? _hoursnum;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 1课时订单2退课订单
		/// </summary>
		public int? OrderType
		{
			set{ _ordertype=value;}
			get{return _ordertype;}
		}
		/// <summary>
		/// 渠道订单号（微信）
		/// </summary>
		public string ChannelOrderID
		{
			set{ _channelorderid=value;}
			get{return _channelorderid;}
		}
		/// <summary>
		/// 学生ID
		/// </summary>
		public int? StudentID
		{
			set{ _studentid=value;}
			get{return _studentid;}
		}
		/// <summary>
		/// 支付金额
		/// </summary>
		public decimal? PayMoney
		{
			set{ _paymoney=value;}
			get{return _paymoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayInfo
		{
			set{ _payinfo=value;}
			get{return _payinfo;}
		}
		/// <summary>
		/// 1微信
		/// </summary>
		public int? PayType
		{
			set{ _paytype=value;}
			get{return _paytype;}
		}
		/// <summary>
		/// 教练ID
		/// </summary>
		public int? CoachID
		{
			set{ _coachid=value;}
			get{return _coachid;}
		}
		/// <summary>
		/// 课时数
		/// </summary>
		public int? HoursNum
		{
			set{ _hoursnum=value;}
			get{return _hoursnum;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

