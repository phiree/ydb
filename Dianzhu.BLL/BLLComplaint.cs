using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLComplaint
    {
        public IDAL.IDALComplaint DALComplaint;

        public BLLComplaint(IDAL.IDALComplaint dal)
        {
            DALComplaint = dal;
        }

        public void Save(Complaint c)
        {
            DALComplaint.Add(c);
        }
    }
}
