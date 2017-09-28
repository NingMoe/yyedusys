using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// CurriculumEvaluate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class CurriculumEvaluate
	{
		public CurriculumEvaluate()
		{}
		#region Model
		private int _pjid;
		private int? _curriculumid;
		private int? _studentid;
		private string _info;
		private DateTime? _addtime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int PJID
		{
			set{ _pjid=value;}
			get{return _pjid;}
		}
		/// <summary>
		/// 课程ID
		/// </summary>
		public int? CurriculumID
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
		/// 评价内容
		/// </summary>
		public string Info
		{
			set{ _info=value;}
			get{return _info;}
		}
		/// <summary>
		/// 评价时间
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
        #endregion Model

        public int StarLevel { get; set; }

        /// <summary>
        /// 效果评分
        /// </summary>
        public int EffectStarLevel { get; set; }
    }
}

