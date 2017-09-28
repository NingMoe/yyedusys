using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Comm.Helper
{
    public class ParamHelper<T>
    {

        public static T GetParam(object obj, object defObj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (Exception e)
            {
                if (defObj is T)
                {
                    return (T)defObj;
                }
                else
                {
                    //todo 判断类型
                    return (T)defObj;
                }
            }
        }

    }
}
