using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Models.Response
{
    public class StudentResponse : YY.Edu.Sys.Models.Student
    {
        public string VenueName { get; set; }

        public string LinkMan { get; set; }

        public string LinkManMobile { get; set; }
    }
}