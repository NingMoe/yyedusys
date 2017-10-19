using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Models.RequestModel
{
    public class CurriculumRequest
    {
        public int? VenueID
        {
            get;set;
        }

        public int StudentID { get; set; }

        public int CampusID { get; set; }

        public int State { get; set; }

        public int PKType { get; set; }

        public string CurriculumBeginTime { get; set; }

        public string CurriculumEndTime { get; set; }



    }
}