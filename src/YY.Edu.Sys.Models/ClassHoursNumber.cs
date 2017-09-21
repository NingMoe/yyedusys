using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// ClassHoursNumber:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class ClassHoursNumber
	{
		public ClassHoursNumber()
		{}
		#region Model
		private int _chnid;
		private int? _studentid;
		private int? _coachid;
		private int? _classnumber=0;
		private DateTime? _addtime= DateTime.Now;
		/// <summary>
		/// 学员课时表主键ID
		/// </summary>
		public int CHNID
		{
			set{ _chnid=value;}
			get{return _chnid;}
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
		/// 教练ID
		/// </summary>
		public int? CoachID
		{
			set{ _coachid=value;}
			get{return _coachid;}
		}
		/// <summary>
		/// 课时数量
		/// </summary>
		public int? ClassNumber
		{
			set{ _classnumber=value;}
			get{return _classnumber;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}

        /// <summary>
        /// 场馆ID
        /// </summary>
        public int VenueID { get; set; }
		#endregion Model

	}
}

