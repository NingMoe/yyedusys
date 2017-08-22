using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Models
{
    public class AspNetUserLogins
    {
        public AspNetUserLogins()
        { }
        #region Model
        private string _loginprovider;
        private string _providerkey;
        private string _userid;
        /// <summary>
        /// 
        /// </summary>
        public string LoginProvider
        {
            set { _loginprovider = value; }
            get { return _loginprovider; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProviderKey
        {
            set { _providerkey = value; }
            get { return _providerkey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        #endregion Model

    }
}
