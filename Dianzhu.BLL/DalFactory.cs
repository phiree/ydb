﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    public class DalFactory
    {
        static IDALCashTicketTemplate iDALCashTicketTemplate = null;
        static IDALCashTicket iDALCashTicket = null;
        static IDALMembership iDalMembership = null;
        public static IDAL.IDALMembership GetDalMembership()
        {
            return iDalMembership ?? new DALMembership();
        }
        public static IDAL.IDALCashTicketTemplate GetDalCashTicketTemplate()
        { 
            if (iDALCashTicketTemplate==null)
            {
                iDALCashTicketTemplate =new  DALCashTicketTemplate();
            }
            return iDALCashTicketTemplate;
        }

        internal static IDALCashTicket GetDalCashTicket()
        {
            if (iDALCashTicket == null)
            {
                iDALCashTicket = new DalCashTicket();
            }
            return iDALCashTicket;
        }
    }
}
