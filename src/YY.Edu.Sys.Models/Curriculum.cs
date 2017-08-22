using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// Curriculum:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class Curriculum
	{
		public Curriculum()
		{}
		#region Model
		private int _curriculumid;
		private int? _studentid;
		private int? _pkid;
		private DateTime? _addtime= DateTime.Now;
		private int? _state;
		private int? _coachid;
		/// <summary>
		/// 课程表ID
		/// </summary>
		public int CurriculumID
		{
			set{ _curriculumid=value;}
			get{return _curriculumid;}
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
		/// 排课ID
		/// </summary>
		public int? PKID
		{
			set{ _pkid=value;}
			get{return _pkid;}
		}
		/// <summary>
		/// 预约时间
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 0预约成功，1上课成功，2学生请假，3老师请假4场馆停课
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 教练ID
		/// </summary>
		public int? CoachID
		{
			set{ _coachid=value;}
			get{return _coachid;}
		}
		#endregion Model

	}
}

