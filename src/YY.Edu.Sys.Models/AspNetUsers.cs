using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Models
{
    public class AspNetUsers
    {

        public AspNetUsers()
        { }
        #region Model
        private string _id;
        private string _nameidentifier;
        private string _email;
        private bool _emailconfirmed;
        private string _passwordhash;
        private string _securitystamp;
        private string _phonenumber;
        private bool _phonenumberconfirmed;
        private bool _twofactorenabled;
        private DateTime? _lockoutenddateutc;
        private bool _lockoutenabled;
        private int _accessfailedcount;
        private string _username;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NameIdentifier
        {
            set { _nameidentifier = value; }
            get { return _nameidentifier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool EmailConfirmed
        {
            set { _emailconfirmed = value; }
            get { return _emailconfirmed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PasswordHash
        {
            set { _passwordhash = value; }
            get { return _passwordhash; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SecurityStamp
        {
            set { _securitystamp = value; }
            get { return _securitystamp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber
        {
            set { _phonenumber = value; }
            get { return _phonenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool PhoneNumberConfirmed
        {
            set { _phonenumberconfirmed = value; }
            get { return _phonenumberconfirmed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool TwoFactorEnabled
        {
            set { _twofactorenabled = value; }
            get { return _twofactorenabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LockoutEndDateUtc
        {
            set { _lockoutenddateutc = value; }
            get { return _lockoutenddateutc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool LockoutEnabled
        {
            set { _lockoutenabled = value; }
            get { return _lockoutenabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AccessFailedCount
        {
            set { _accessfailedcount = value; }
            get { return _accessfailedcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        #endregion Model

    }
}
