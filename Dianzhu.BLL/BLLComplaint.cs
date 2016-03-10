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
        public DALComplaint DALComplaint = DALFactory.DALComplaint;

        public void SaveOrUpdate(Complaint ad)
        {
            DALComplaint.SaveOrUpdate(ad);
        }
    }
}
