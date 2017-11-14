using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class ApplyTryoutCurriculumResponse : Sys.Models.ApplyTryoutCurriculum
    {

        public string StudentFullName { get; set; }

        public string StudentParentFullName { get; set; }

        public string StudentMobile { get; set; }

        public string StudentParentMobile { get; set; }

        public string CoachFullName { get; set; }

        public string CoachMobile { get; set; }
    }
}