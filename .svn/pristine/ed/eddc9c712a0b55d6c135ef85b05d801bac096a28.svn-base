using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Comm.Helper
{
    public class ExcelHelper
    {
        /// <summary>
        /// 解析路径中的excel表格
        /// </summary>
        /// <param name="strFileName">文件绝对路径</param>
        /// <returns></returns>
        /// <exception cref="Exception">解析异常</exception>
        public static DataTable Import(string strFileName)
        {

            DataTable dt = new DataTable();
            try
            {
                //string strFileName = @"D:\junfu.zhao\project\yyedusys\src\YY.Edu.Sys.Admin\App_Data\学生.xls";
                HSSFWorkbook hssfworkbook;

                using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
                ISheet sheet = hssfworkbook.GetSheetAt(0);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    dt.Columns.Add(cell.ToString());
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = dt.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) == null)
                        {
                            dataRow[j] = null;
                            continue;
                        }

                        if (row.GetCell(j).ToString().Contains("/"))
                        {
                            string[] a = row.GetCell(j).ToString().Split('/');
                            dataRow[j] = "20" + a[2] + "-" + a[0] + "-" + a[1];
                        }
                        else
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }

                    dt.Rows.Add(dataRow);
                }
            }
            catch
            {
                throw;
            }
            return dt;

        }


        public static DataTable ImportNotNull(string strFileName)
        {

            DataTable dt = new DataTable();

            dt = Import(strFileName);

            if (dt == null || dt.Rows.Count == 0)
                throw new Exception("解析失败");

            return dt;
        }
    }
}
