using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class VenueInfoResponse : Sys.Models.VenueInfo
    {
        public string VenueCode { get; set; }
    }
}