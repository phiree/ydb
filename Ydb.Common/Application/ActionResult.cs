using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Application
{
  public  class ActionResult<T>:ActionResult
    {
       
        public T o { get; set; }
    }
    public class ActionResult
    {
        public ActionResult()
        {
            IsSuccess = true;
        }
        public bool IsSuccess { get; set; }
        public string ErrMsg { get; set; }
       
    }
}
