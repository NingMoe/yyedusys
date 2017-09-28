using System;
namespace YY.Edu.Sys.Models
{
    /// <summary>
    /// CoachWages:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    //[Serializable]
    public partial class CoachWages
    {
        public CoachWages()
        { }
        #region Model
        private int _wagesid;
        private int? _coachid;
        private DateTime? _addtime = DateTime.Now;
        private decimal? _price;
        private string _remark;
        private int? _venueid;
        /// <summary>
        /// 
        /// </summary>
        public int WagesID
        {
            set { _wagesid = value; }
            get { return _wagesid; }
        }
        /// <summary>
        /// 教练ID
        /// </summary>
        public int? CoachID
        {
            set { _coachid = value; }
            get { return _coachid; }
        }

        /// <summary>
        /// 教练名字
        /// </summary>
        public string CoachFullName { get; set; }

        /// <summary>
        /// 发放时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 发放金额
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 工作日期(发放的是哪个月的工资)
        /// </summary>
        public DateTime? WorkDate { get; set; }
        /// <summary>
        /// 工资备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 发放场馆
        /// </summary>
        public int? VenueID
        {
            set { _venueid = value; }
            get { return _venueid; }
        }
        /// <summary>
        /// 工资是否发送完成 0未发送 1发送完成
        /// </summary>
        public int State { get; set; }

        public DateTime? PayTime { get; set; }

        #endregion Model

    }
}

