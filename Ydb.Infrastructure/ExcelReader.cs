using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using Ydb.Common.Infrastructure;

namespace Ydb.Infrastructure
{
    public class ExcelReader : IExcelReader
    {

        public DataTable ReadFromExcel(Stream excelFilesStream)
        {
            return ReadFromExcel(excelFilesStream,0,0,false);
        }
        public DataTable ReadFromExcel(Stream excelFilesStream,int sheetIndex,int rowIndexBegin,bool onlySchema)
        {
            
            HSSFWorkbook book = new HSSFWorkbook(excelFilesStream);
            var sheet = book.GetSheetAt(sheetIndex);
            DataTable dt = new DataTable();
            //起始行单元格内的值作为datatable的列名.
            var row = sheet.GetRow(rowIndexBegin);
            for (int i = 0; i < row.LastCellNum; i++)
            {
                var columnName = row.GetCell(i);
                //空白列导致出错
                string strColName = string.Empty;
                if (columnName == null)
                {
                    strColName = Guid.NewGuid().ToString();
                }
                else
                {
                    strColName = columnName.ToString();
                }
                DataColumn col = new DataColumn(strColName, typeof(String));
                dt.Columns.Add(col);
            }
            if (!onlySchema)
            {
                IEnumerator rowEnumer = sheet.GetRowEnumerator();
                while (rowEnumer.MoveNext())
                {

                    var currentRow = (HSSFRow)rowEnumer.Current;
                    if (currentRow.RowNum < rowIndexBegin + 1) continue;
                    //防止其遍历到没有数据的row
                    if (currentRow.LastCellNum < row.Cells.Count)
                    {
                        //    break;
                    }
                    //空白行判断:如果所有的cell都没有可见数据,则为空白行.
                    bool isEnd = true;
                    foreach (var cell in currentRow.Cells)
                    {
                        if (!string.IsNullOrEmpty(StringHelper.ReplaceSpace(cell.ToString())))
                        {
                            isEnd = false;
                            break;
                        }
                    }
                    if (isEnd)
                    {

                        break;
                    }

                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        var cell = currentRow.GetCell(i);
                        if (cell == null)
                        {
                            dr[i] = null;
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
           
            return dt;
        }

    }
}
