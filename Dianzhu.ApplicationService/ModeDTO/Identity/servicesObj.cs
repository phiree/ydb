using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class servicesObj
    {
        string _id = null;
        /// <summary>
        /// 服务ID
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

        string _name = "";
        /// <summary>
        /// 服务项的名称
        /// </summary>
        /// <type>string</type>
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        serviceTypeObj _serviceType = new serviceTypeObj();
        /// <summary>
        /// 服务项的类型（“>”分级）
        /// </summary>
        /// <type>string</type>
        public serviceTypeObj serviceType
        {
            get
            {
                return _serviceType;
            }
            set
            {
                _serviceType = value;
            }
        }

        string _introduce = "";
        /// <summary>
        /// 简介
        /// </summary>
        /// <type>string</type>
        public string introduce
        {
            get
            {
                return _introduce;
            }
            set
            {
                _introduce = value;
            }
        }

        locationObj _location = new locationObj();
        /// <summary>
        ///地位信息模型
        /// </summary>
        /// <type>locationObj</type>
        public locationObj location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        string _startAt = "0";
        /// <summary>
        ///起步价
        /// </summary>
        /// <type>string</type>
        public string startAt
        {
            get
            {
                return _startAt;
            }
            set
            {
                _startAt = value;
            }
        }

        string _unitPrice = "0";
        /// <summary>
        ///单价
        /// </summary>
        /// <type>string</type>
        public string unitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                _unitPrice = value;
            }
        }

        string _deposit = "0";
        /// <summary>
        ///订金
        /// </summary>
        /// <type>string</type>
        public string deposit
        {
            get
            {
                return _deposit;
            }
            set
            {
                _deposit = value;
            }
        }

        string _appointmentTime = "0";
        /// <summary>
        ///提前预约的时间（分）
        /// </summary>
        /// <type>string</type>
        public string appointmentTime
        {
            get
            {
                return _appointmentTime;
            }
            set
            {
                _appointmentTime = value;
            }
        }

        bool _bDoorService = false;
        /// <summary>
        ///是否提供上门服务（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool bDoorService
        {
            get
            {
                return _bDoorService;
            }
            set
            {
                _bDoorService = value;
            }
        }

        string _eServiceTarget = "";
        /// <summary>
        ///服务面向的对象：“all”：[全部]，“company”：[公司]，“personal”：[个人]
        /// </summary>
        /// <type>string</type>
        public string eServiceTarget
        {
            get
            {
                return _eServiceTarget;
            }
            set
            {
                _eServiceTarget = value;
            }
        }

        string _eSupportPayWay = "";
        /// <summary>
        ///支持的支付方式：“all”：[全部]，“wePay”：[微支付]，“aliPay”：[支付宝]
        /// </summary>
        /// <type>string</type>
        public string eSupportPayWay
        {
            get
            {
                return _eSupportPayWay;
            }
            set
            {
                _eSupportPayWay = value;
            }
        }

        string _tag = "";
        /// <summary>
        ///标签（“|”区分）
        /// </summary>
        /// <type>string</type>
        public string tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        bool _bOpen = true;
        /// <summary>
        ///是否开启（true Or false）
        /// </summary>
        /// <type>bool</type>
        public bool bOpen
        {
            get
            {
                return _bOpen;
            }
            set
            {
                _bOpen = value;
            }
        }

        string _maxCount = "0";
        /// <summary>
        ///该服务最大接单量
        /// </summary>
        /// <type>string</type>
        public string maxCount
        {
            get
            {
                return _maxCount;
            }
            set
            {
                _maxCount = value;
            }
        }

        string _chargeUnit = "";
        /// <summary>
        ///服务计费单位
        /// </summary>
        /// <type>string</type>
        public string chargeUnit
        {
            get
            {
                return _chargeUnit;
            }
            set
            {
                _chargeUnit = value;
            }
        }

        public string serviceTypeID
        {
            set
            {
                _serviceType.id = value;
            }
        }
    }
}
