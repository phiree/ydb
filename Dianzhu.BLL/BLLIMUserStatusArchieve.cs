using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLIMUserStatusArchieve
    {
        public IDAL.IDALIMUserStatusArchieve DALIMUserStatusArchieve;

        public BLLIMUserStatusArchieve(IDAL.IDALIMUserStatusArchieve dal)
        {
            DALIMUserStatusArchieve = dal;
        }

        public void Save(IMUserStatusArchieve im)
        {
            DALIMUserStatusArchieve.Add(im);
        }
    }
}
