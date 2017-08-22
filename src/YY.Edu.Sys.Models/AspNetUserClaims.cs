using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Models
{
    public class AspNetUserClaims
    {

        public AspNetUserClaims()
        { }
        #region Model
        private int _id;
        private string _userid;
        private string _claimtype;
        private string _claimvalue;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClaimType
        {
            set { _claimtype = value; }
            get { return _claimtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClaimValue
        {
            set { _claimvalue = value; }
            get { return _claimvalue; }
        }
        #endregion Model

    }
}
