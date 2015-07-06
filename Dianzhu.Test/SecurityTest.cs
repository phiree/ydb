using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
using Dianzhu.BLL;
using NUnit.Framework;
using Rhino.Mocks;
using PHSuit;
using FluentNHibernate.Testing;
using NHibernate;
using TestStack.Dossier.Factories;
using FizzWare.NBuilder;


namespace Dianzhu.Test
{
    [TestFixture]
    public class securitytester
    {
        
         

      
        [Test]
        public void securitytest()
        {
           
            string original = "name";
            Console.Write(Security.Encrypt(original, false));
            Assert.AreEqual(original, Security.Decrypt( Security.Encrypt(original,false),false) );
             
        }
       

    }
}
