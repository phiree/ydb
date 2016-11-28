using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Infrastructure
{
    /// <summary>
    /// 讀取Excel
    /// </summary>
   public interface IExcelReader
    {
        DataTable ReadFromExcel(Stream excelFileStream);
    }
}
