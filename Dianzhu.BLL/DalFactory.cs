using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    public class DALFactory
    {
        static DALCashTicketTemplate dalCashTicketTemplate = null;
        static DALCashTicket dalCashTicket = null;
        static DALMembership dalMembership = null;
       
        static DALBusiness iDALCashTicket = null;
        static DALBusinessImage iDalMembership = null;
         
        static DALArea dalArea;
        static DALBusiness dalBusiness;
        static DALBusinessImage dalBusinessImage;
        static DALCashTicketCreateRecord dalCashTicketCreateRecord;
        static DALDZService dalDZService;
        static DALServiceProperty dalServiceProperty;
        static DALServicePropertyValue dalServicePropertyValue;
        static DALServiceType dalServiceType;
        static DALStaff dalStaff;

        public static DALStaff DALStaff
        {
            get { return dalStaff ?? new DALStaff(); }
            set { dalStaff = value; }
        }
        public static DALServiceType DALServiceType
        {
            get { return dalServiceType ?? new DALServiceType(); }
            set { dalServiceType = value; }
        }
        public static DALServicePropertyValue DALServicePropertyValue
        {
            get { return dalServicePropertyValue ?? new DALServicePropertyValue(); }
            set { dalServicePropertyValue = value; }
        }
        public static DALServiceProperty DALServiceProperty
        {
            get { return dalServiceProperty ?? new DALServiceProperty(); }
            set { dalServiceProperty = value; }
        }
        public static DALDZService DALDZService
        {
            get { return dalDZService ?? new DALDZService(); }
            set { dalDZService = value; }
        }
        public static DALCashTicketCreateRecord DALCashTicketCreateRecord
        {
            get { return dalCashTicketCreateRecord ?? new DALCashTicketCreateRecord(); }
            set { dalCashTicketCreateRecord = value; }
        }
        public static DALArea DALArea
        {
            get{
                return dalArea ?? new DALArea();
            }
            set { dalArea = value; }
        }

        public static DALBusiness DALBusiness
        {
            get
            {
                return dalBusiness ?? new DALBusiness();
            }
            set { dalBusiness = value; }
        }
        public static DALBusinessImage DALBusinessImage
        {
            get
            {
                return dalBusinessImage ?? new DALBusinessImage();
            }
            set { dalBusinessImage = value; }
        }
        public static DALMembership DALMembership
        {
            get{
                return dalMembership ?? new DALMembership();
            }
            set { dalMembership = value; }
        }
        public static DALCashTicketTemplate DALCashTicketTemplate
        { 
           get{
               return dalCashTicketTemplate ?? new DALCashTicketTemplate();
           }
            set { dalCashTicketTemplate = value; }
   
        }
        internal static DALCashTicket DALCashTicket
        {
            get
            {
                return dalCashTicket ?? new DALCashTicket();
            }
            set { dalCashTicket = value; }
        }
    }
}
