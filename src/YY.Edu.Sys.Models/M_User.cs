using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// M_User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class M_User
	{
		public M_User()
		{}
		#region Model
		private int _id;
		private string _username;
		private string _pwd;
		/// <summary>
		/// 运营系统ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pwd
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		#endregion Model

	}
}

