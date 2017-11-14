using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Comm.Helper
{
    public class TConvertHelper
    {

        public static T Mapper<T, T1>(T1 t1)
        {
            T d = Activator.CreateInstance<T>();
            try
            {
                var sType = t1.GetType();
                var dType = typeof(T);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(t1));
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return d;
        }

    }
}
