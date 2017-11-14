
namespace YY.Edu.Sys.Models
{
    public class ClassHoursDetailedTSMShip
    {
        public int CHDTID { get; set; }

        public int TSMID { get; set; }

        public int DetailedID { get; set; }

        /// <summary>
        /// 课时数
        /// </summary>
        public int DNumber { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal CMoney { get; set; }
    }
}
