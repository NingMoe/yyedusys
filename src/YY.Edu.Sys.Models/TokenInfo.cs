using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Models
{
    public class TokenInfo
    {

        //{
        //  "access_token": "9sx2ojliKmWgoi9QzcccOUVACyFliVp1HnRUDnI8u7X-VsjqesakAtqWyzYxWxo4dXSnV374VAb5p42NOkWqLCRWNruqmzjA_WzrBHF9pBcB7Pw33hW5lAkNwwS6Y-lG8xZ0zGPdjMzIPNSPTt89u2fMfZODJ7GR_uG9pP8kEv3ef51prx-AqlWAkD9xDIqxh5ZnkMmQDhMN4bnIItmZKuhXJLbGPu0WYbFBtoKULdQ",
        //  "token_type": "bearer",
        //  "expires_in": 1209599,
        //  "refresh_token": "667a9cff7ad74447aa703da97a73114b"
        //}
        public TokenInfo() { }

        public TokenInfo(string token)
        {

            TokenInfo oData = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenInfo>(token);
            this.access_token = oData.access_token;
            this.token_type = oData.token_type;
            this.expires_in = oData.expires_in;
            this.refresh_token = oData.refresh_token;
        }

        /// <summary>
        /// token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// token_type
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// refresh_token
        /// </summary>
        public string refresh_token { get; set; }



    }
}
