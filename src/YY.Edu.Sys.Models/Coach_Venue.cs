﻿using System;
using System.ComponentModel.DataAnnotations;

namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// Coach_Venue:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class Coach_Venue
	{
		public Coach_Venue()
		{}
		#region Model
		private int _cvid;
		private int? _coachid;
		private int? _venueid;
		private DateTime? _addtime= DateTime.Now;
		private decimal? _wage;
		private decimal? _price;
		/// <summary>
		/// 
		/// </summary>
		public int CVID
		{
			set{ _cvid=value;}
			get{return _cvid;}
		}
		/// <summary>
		/// 
		/// </summary>
        [Required(ErrorMessage = "教练编号不能为空")]
        public int? CoachID
		{
			set{ _coachid=value;}
			get{return _coachid;}
		}
		/// <summary>
		/// 场馆ID
		/// </summary>
        [Required(ErrorMessage = "场馆编号不能为空")]
        public int? VenueID
		{
			set{ _venueid=value;}
			get{return _venueid;}
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
		/// 工资
		/// </summary>
        [RegularExpression(@"[\d]{1,5}",ErrorMessage = "一对一工资必须是整数")]
        public decimal? Wage
		{
			set{ _wage=value;}
			get{return _wage;}
		}
		/// <summary>
		/// 课时费用
		/// </summary>
        [RegularExpression(@"[\d]{1,5}", ErrorMessage = "一对一课时费用必须是整数")]
        public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}

        /// <summary>
		/// 一对多工资
		/// </summary>
        [RegularExpression(@"[\d]{1,5}", ErrorMessage = "一对多工资必须是整数")]
        public decimal? WageMore
        {
            get;set;
        }
        /// <summary>
        /// 一对多课时费用
        /// </summary>
        [RegularExpression(@"[\d]{1,5}", ErrorMessage = "一对多课时费用必须是整数")]
        public decimal? PriceMore
        {
            get; set;
        }
        #endregion Model

    }
}

