using System;
namespace YY.Edu.Sys.Models
{
	/// <summary>
	/// MenuManage:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	//[Serializable]
	public partial class MenuManage
	{
		public MenuManage()
		{}
		#region Model
		private int _menuid;
		private string _menuname;
		private int? _parentid;
		private int? _menutype;
		private string _iconcls;
		private int? _mstate;
		private int? _sort=50;
		private DateTime? _addtime= DateTime.Now;
		private string _pageurl;
		/// <summary>
		/// 菜单表ID
		/// </summary>
		public int MenuID
		{
			set{ _menuid=value;}
			get{return _menuid;}
		}
		/// <summary>
		/// 菜单名称
		/// </summary>
		public string MenuName
		{
			set{ _menuname=value;}
			get{return _menuname;}
		}
		/// <summary>
		/// 父ID 一级是为0
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 菜单类型，0运营，1场馆，2教练，3学员
		/// </summary>
		public int? MenuType
		{
			set{ _menutype=value;}
			get{return _menutype;}
		}
		/// <summary>
		/// 菜单图标
		/// </summary>
		public string IconCls
		{
			set{ _iconcls=value;}
			get{return _iconcls;}
		}
		/// <summary>
		/// 1有效　２删除
		/// </summary>
		public int? MState
		{
			set{ _mstate=value;}
			get{return _mstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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
		/// 页面地址
		/// </summary>
		public string PageUrl
		{
			set{ _pageurl=value;}
			get{return _pageurl;}
		}
		#endregion Model

	}
}

