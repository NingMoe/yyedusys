using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Models.StateEnum
{
    public enum ClassHoursDetailed
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 1约课
        /// </summary>
        BookTeachingScheDone,
        /// <summary>
        /// 3学生请假
        /// </summary>
        StudentLeaveTeachingScheDone,
        /// <summary>
        /// 4教练请假
        /// </summary>
        CoachLeaveTeachingScheDone,
        /// <summary>
        /// 5学校停课
        /// </summary>
        StopTeachingSche,

    }
}
