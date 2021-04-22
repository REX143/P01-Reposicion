using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace WebReposicion.Transversal
{
    public class Reporteador
    {

        public static Stream DataTableToExcel(DataTable dt)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("Stock");
            XSSFRow headerRow = headerRow = (XSSFRow)sheet.CreateRow(0);
            try
            {
                foreach (DataColumn column in dt.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                int rowIndex = 1;
                foreach (DataRow row in dt.Rows)
                {
                    XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dt.Columns)
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    ++rowIndex;
                }
                for (int i = 0; i <= dt.Columns.Count; ++i)
                    sheet.AutoSizeColumn(i);
                workbook.Write(ms);
                ms.Flush();
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                ms.Close();
                sheet = null;
                headerRow = null;
                workbook = null;
            }
            return ms;
        }

    }
}