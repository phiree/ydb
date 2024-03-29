﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ydb.Common;

namespace Dianzhu.ApplicationService
{
    public class complaintObj
    {
        string _id = null;
        /// <summary>
        /// 投诉的ID
        /// </summary>
        /// <type>string</type>
        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        string _senderID = "";
        /// <summary>
        /// 发送者ID
        /// </summary>
        /// <type>string</type>
        public string senderID
        {
            get
            {
                return _senderID;
            }
            set
            {
                _senderID = value;
            }
        }

        string _orderID = "";
        /// <summary>
        /// 投诉的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID
        {
            get
            {
                return _orderID;
            }
            set
            {
                _orderID = value;
            }
        }

        string _target = "";
        /// <summary>
        /// 投诉的目标("customerService":[客服]，"store":[商铺])customerService
        /// </summary>
        /// <type>string</type>
        public string target
        {
            get
            {
                return _target;
            }
            set
            {
                if (value.ToLower() == "customerservice")
                {
                    _target = "customerService";
                }
                else if (value.ToLower() == "store")
                {
                    _target = "store";
                }
                else
                {
                    _target = value;
                }

            }
        }

        string _content = "";
        /// <summary>
        /// 描述
        /// </summary>
        /// <type>string</type>
        public string content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        IList<String> _resourcesUrl =new List<String>();
        /// <summary>
        /// 图片资源链接
        /// </summary>
        /// <type>array[string]</type>
        public IList<String> resourcesUrls
        {
            get
            {
                return _resourcesUrl;
            }
            set
            {
                _resourcesUrl = value;
            }
        }

       enum_ComplaintStatus _status = 0;
        /// <summary>
        /// 该投诉的状态
        /// </summary>
        /// <type>string</type>
        public enum_ComplaintStatus status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

    }
}
