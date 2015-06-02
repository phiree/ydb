using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
using Dianzhu.BLL;
using NUnit.Framework;
using Rhino.Mocks;
using Dianzhu.IDAL;
using NUnit.Framework;
using FluentNHibernate.Testing;
using NHibernate;
using Dianzhu.Model;
using Dianzhu.BLL;
using PHSuit;
using System.IO;
namespace Dianzhu.Test
{
    [TestFixture]
    public class ExcelImportTest
    {
        [Test]
        public void ImportTypeServiceTest()
        {
          FileStream fs=  File.Open(Environment.CurrentDirectory + "/TestFiles/个人服务类最新版-表格.xls", FileMode.Open);
            ReadExcelToDataTable rtd=new ReadExcelToDataTable(fs,false,false,0);
            string outMsg;
            System.Data.DataTable table=rtd.Read(out outMsg);
            Assert.AreEqual("一、缴费充值", table.Rows[0][0].ToString());
            BLLServiceType importor = new BLL.BLLServiceType();
            IList<ServiceType> topTypeList = importor.ObjectAdapter(table);
            Assert.AreEqual(16,topTypeList.Count);
            Assert.AreEqual(10,topTypeList[0].Children[0].Children.Count);
            
        }

    }
}
