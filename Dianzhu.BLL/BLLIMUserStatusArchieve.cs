﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLIMUserStatusArchieve
    {
        public DALIMUserStatusArchieve DALIMUserStatusArchieve = DALFactory.DALIMUserStatusArchieve;

        public void Save(IMUserStatusArchieve im)
        {
            DALIMUserStatusArchieve.Save(im);
        }
    }
}
