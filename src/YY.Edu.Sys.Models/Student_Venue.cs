using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// Student_Venue:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class Student_Venue
	{
		public Student_Venue()
		{}
		#region Model
		private int _svid;
		private int? _studentid;
		private int? _venueid;
		private DateTime? _addtime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int SVID
		{
			set{ _svid=value;}
			get{return _svid;}
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
		/// 场馆ID
		/// </summary>
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
		#endregion Model

	}
}

