using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLStaff
    {
      IDALStaff iDALStaff;

      public BLLStaff(IDALStaff iDALStaff)
      {
          this.iDALStaff = iDALStaff;
      }
      public BLLStaff()
          : this(new DALStaff())
      { }
       
    }
}
