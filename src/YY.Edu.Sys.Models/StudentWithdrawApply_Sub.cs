using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// StudentWithdrawApply_Sub:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class StudentWithdrawApply_Sub
	{
		public StudentWithdrawApply_Sub()
		{}
		#region Model
		private int _subid;
		private int? _coachid;
		private int? _classnumber;
		private decimal? _price;
		private int? _applyid;
		/// <summary>
		/// 
		/// </summary>
		public int SubID
		{
			set{ _subid=value;}
			get{return _subid;}
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
		/// 课时
		/// </summary>
		public int? ClassNumber
		{
			set{ _classnumber=value;}
			get{return _classnumber;}
		}
		/// <summary>
		/// 应退金额
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 申请表ID
		/// </summary>
		public int? ApplyID
		{
			set{ _applyid=value;}
			get{return _applyid;}
		}
		#endregion Model

	}
}

