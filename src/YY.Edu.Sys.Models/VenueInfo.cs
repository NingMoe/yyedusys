﻿using System;
using System.ComponentModel.DataAnnotations;

namespace YY.Edu.Sys.Models
{
    /// <summary>
    /// VenueInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    //[Serializable]
    public partial class VenueInfo
    {
        public VenueInfo()
        { }
        #region Model
        //private int _vinfoid;
        //private string _introduce;
        //private string _businesstime;
        //private DateTime? _addtime= DateTime.Now;
        //private int? _adduserid;
        //private DateTime? _modifytime;
        //private int? _modifyuserid;
        /// <summary>
        /// 
        /// </summary>
        public int VInfoID
        {
            get; set;
        }

        /// <summary>
        /// 场馆编号
        /// </summary>
        [Required(ErrorMessage = "场馆编号不能为空")]
        public int VenueID { get; set; }
        /// <summary>
        /// 场地介绍
        /// </summary>
        [Required(ErrorMessage = "场地介绍不能为空")]
        public string Introduce
        {
            get; set;
        }
        /// <summary>
        /// 营业时间介绍
        /// </summary>
        [Required(ErrorMessage = "营业时间介绍不能为空")]
        public string BusinessTime
        {
            get; set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime
        {
            get; set;
        }
        /// <summary>
        /// 添加人ID
        /// </summary>
        public int? AddUserID
        {
            get; set;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime
        {
            get; set;
        }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public int? ModifyUserID
        {
            get; set;
        }

        #endregion Model

    }
}

