using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Comm.ResponseModel
{
    public class ResponseModel4Res<T> : ResponseModelBase
    {
        /// <summary>
        /// 分页返回
        /// </summary>
        public IList<T> data { get; set; }

        /// <summary>
        /// 单条信息返回
        /// </summary>
        public T Info { get; set; }

        public ResponseModel4Res()
        {
            ResponseModelBase res = Comm.ResponseModel.ResponseModelBase.Success();
            Code = res.Code;
            Msg = res.Msg;
            Error = true;
        }
    }
}
