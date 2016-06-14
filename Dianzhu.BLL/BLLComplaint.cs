using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLComplaint
    {
        //public DALComplaint DALComplaint = DALFactory.DALComplaint;

        public IDALComplaint DALComplaint;
        public BLLComplaint(IDALComplaint DALComplaint)
        {
            this.DALComplaint = DALComplaint;
            // this.iuw = iuw;
        }

        public void SaveOrUpdate(Complaint ad)
        {
            DALComplaint.Update(ad);
        }
    }
}
