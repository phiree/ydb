﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    public class DALFactory
    {
        static bool forTest = false;
        
        static DALCashTicketTemplate dalCashTicketTemplate = null;
        static DALCashTicket dalCashTicket = null;
        static DALMembership dalMembership = null;
  
         
        static DALArea dalArea;
        static DALBusiness dalBusiness;
        static DALBusinessImage dalBusinessImage;
        static DALCashTicketCreateRecord dalCashTicketCreateRecord;
        static DALDZService dalDZService;
        static DALServiceProperty dalServiceProperty;
        static DALServicePropertyValue dalServicePropertyValue;
        static DALServiceType dalServiceType;
        static DALStaff dalStaff;
        static DALDeviceBind dalDeviceBind;
        static DALServiceOrder dalServiceOrder;
        static DALDZTag dalDZTag;
        static DALReception dalReception;
        static DALReceptionStatus dalReceptionStatus;
        static DALReceptionChat dalReceptionChat;
        static DALPaymentLog dalPaymentLog;
        static DALReceptionChatDD dalReceptionChatDD;
        static DALIMUserStatus dalIMUserStatus;
        static DALIMUserStatusArchieve dalIMUserStatusArchieve;
        static DALAdvertisement dalAdvertisement;
        static DALReceptionStatusArchieve dalReceptionStatusArchieve;
        static DALServiceOpenTime dalServiceOpenTime;
        static DALServiceOrderStateChangeHis dalServiceOrderStateChangeHis;
        static DALComplaint dalComplaint;
        static DALServiceOrderAppraise dalServiceOrderAppraise;
        static DALServiceOrderRemind dalServiceOrderRemind;
        static DALClaims dalClaims;
        public static DALClaims DALClaims
        {
            get { return dalClaims ?? new DALClaims(); }
            set { dalClaims = value; }
        }
        public static DALServiceOrderRemind DALServiceOrderRemind
        {
            get { return dalServiceOrderRemind ?? new DALServiceOrderRemind(); }
            set { dalServiceOrderRemind = value; }
        }
        public static DALServiceOrderAppraise DALServiceOrderAppraise
        {
            get { return dalServiceOrderAppraise ?? new DALServiceOrderAppraise(); }
            set { dalServiceOrderAppraise = value; }
        }
        public static DALComplaint DALComplaint
        {
            get { return dalComplaint ?? new DALComplaint(); }
            set { dalComplaint = value; }
        }
        public static DALServiceOrderStateChangeHis DALServiceOrderStateChangeHis
        {
            get { return dalServiceOrderStateChangeHis ?? new DALServiceOrderStateChangeHis(); }
            set { dalServiceOrderStateChangeHis = value; }
        }
        public static DALServiceOpenTime DALServiceOpenTime
        {
            get { return dalServiceOpenTime ?? new DALServiceOpenTime(); }
            set { dalServiceOpenTime = value; }
        }
        static DALOrderAssignment dalOrderAssignment;
        public static DALOrderAssignment DALOrderAssignment
        {
            get { return dalOrderAssignment ?? new DALOrderAssignment(); }
            set { dalOrderAssignment = value; }
        }
        static DALServiceOpenTimeForDay dalServiceOpenTimeForDay;
        public static DALServiceOpenTimeForDay DALServiceOpenTimeForDay
        {
            get { return dalServiceOpenTimeForDay ?? new DALServiceOpenTimeForDay(); }
            set { dalServiceOpenTimeForDay = value; }
        }
        public static DALReceptionStatusArchieve DALReceptionStatusArchieve
        {
            get { return dalReceptionStatusArchieve ?? new DALReceptionStatusArchieve(); }
            set { dalReceptionStatusArchieve = value; }
        }
        //public static DALAdvertisement DALAdvertisement
        //{
        //    get { return dalAdvertisement ?? new DALAdvertisement(); }
        //    set { dalAdvertisement = value; }
        //}
        public static DALIMUserStatusArchieve DALIMUserStatusArchieve
        {
            get { return dalIMUserStatusArchieve ?? new DALIMUserStatusArchieve(); }
            set { dalIMUserStatusArchieve = value; }
        }
        public static DALIMUserStatus DALIMUserStatus
        {
            get { return dalIMUserStatus ?? new DALIMUserStatus(); }
            set { dalIMUserStatus = value; }
        }
        public static DALReceptionChatDD DALReceptionChatDD
        {
            get { return dalReceptionChatDD ?? new DALReceptionChatDD(); }
            set { dalReceptionChatDD = value; }
        }
        public static DALPaymentLog DALPaymentLog
        {
            get { return dalPaymentLog ?? new DALPaymentLog(); }
            set { dalPaymentLog = value; }
        }
        public static DALReceptionChat DALReceptionChat
        {
            get { return dalReceptionChat ?? new DALReceptionChat(); }
            set { dalReceptionChat = value; }
        }
        public static DALReceptionStatus DALReceptionStatus
        {
            get { return dalReceptionStatus ?? new DALReceptionStatus(); }
            set { dalReceptionStatus = value; }
        }
        public static DALReception DALReception
        {
            get
            {
                return dalReception == null ? forTest ? new DALReception("") : new DALReception() : dalReception;
            }
            set { dalReception = value; }
        }
        public static DALDZTag DALDZTag
        {
            get { return dalDZTag ?? new DALDZTag(); }
            set { dalDZTag = value; }
        }
        public static DALServiceOrder DALServiceOrder
        {
            get { return dalServiceOrder ?? new DALServiceOrder(); }
            set { dalServiceOrder = value; }
        }
        public static DALDeviceBind DALDeviceBind
        {
            get { return dalDeviceBind ?? new DALDeviceBind(); }
            set { dalDeviceBind = value; }
        }
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
            get
            {
                return dalDZService == null ? forTest ? new DALDZService(): new DALDZService(): dalDZService;
            }
            set { dalDZService = value; }
        }
        public static DALCashTicketCreateRecord DALCashTicketCreateRecord
        {
            get { return dalCashTicketCreateRecord ?? new DALCashTicketCreateRecord(); }
            set { dalCashTicketCreateRecord = value; }
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
