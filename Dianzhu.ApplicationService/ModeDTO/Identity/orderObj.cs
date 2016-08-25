using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class orderObj
    {
        string _id = null;
        /// <summary>
        /// 订单ID
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

        string _title = "";
        /// <summary>
        /// 订单号
        /// </summary>
        /// <type>string</type>
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        string _createTime = "";
        /// <summary>
        /// 订单创建的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string createTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }

        

        string _closeTime = "";
        /// <summary>
        /// 订单关闭的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string closeTime
        {
            get
            {
                return _closeTime;
            }
            set
            {
                _closeTime = value;
            }
        }

        string _serviceTime = "";
        /// <summary>
        /// 订单服务预约时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string serviceTime
        {
            get
            {
                return _serviceTime;
            }
            set
            {
                _serviceTime = value;
            }
        }

        string _startTime = "";
        /// <summary>
        /// 订单服务实际开始时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string startTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }

        string _doneTime = "";
        /// <summary>
        /// 订单服务完成时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string doneTime
        {
            get
            {
                return _doneTime;
            }
            set
            {
                _doneTime = value;
            }
        }

        string _updateTime = "";
        /// <summary>
        /// 订单最后一次更新时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string updateTime
        {
            get
            {
                return _updateTime;
            }
            set
            {
                _updateTime = value;
            }
        }

        string _notes = "";
        /// <summary>
        /// 备注
        /// </summary>
        /// <type>string</type>
        public string notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
            }
        }

        string _orderAmount = "";
        /// <summary>
        /// 参考价格
        /// </summary>
        /// <type>string</type>
        public string orderAmount
        {
            get
            {
                return _orderAmount;
            }
            set
            {
                _orderAmount = value;
            }
        }

        string _negotiateAmount = "";
        /// <summary>
        /// 协商价格（未协商时与orderAmount相同）
        /// </summary>
        /// <type>string</type>
        public string negotiateAmount
        {
            get
            {
                return _negotiateAmount;
            }
            set
            {
                _negotiateAmount = value;
            }
        }

        string _serviceAddress = "";
        /// <summary>
        /// 服务的地址
        /// </summary>
        /// <type>string</type>
        public string serviceAddress
        {
            get
            {
                return _serviceAddress;
            }
            set
            {
                _serviceAddress = value;
            }
        }

        orderStatusObj _currentStatusObj = new orderStatusObj();
        /// <summary>
        /// 订单的状态对象
        /// </summary>
        /// <type>orderStatusObj</type>
        public orderStatusObj currentStatusObj
        {
            get
            {
                return _currentStatusObj;
            }
            set
            {
                _currentStatusObj = value;
            }
        }

        serviceSnapshotObj _serviceSnapshotObj = new serviceSnapshotObj();
        /// <summary>
        /// 服务项的模型
        /// </summary>
        /// <type>servicesObj</type>
        public serviceSnapshotObj serviceSnapshotObj
        {
            get
            {
                return _serviceSnapshotObj;
            }
            set
            {
                _serviceSnapshotObj = value;
            }
        }

        customerObj _customerObj = new customerObj();
        /// <summary>
        /// 用户模型
        /// </summary>
        /// <type>userObj</type>
        public customerObj customerObj
        {
            get
            {
                return _customerObj;
            }
            set
            {
                _customerObj = value;
            }
        }

        storeObj _storeObj = new storeObj();
        /// <summary>
        /// 店铺模型
        /// </summary>
        /// <type>storeObj</type>
        public storeObj storeObj
        {
            get
            {
                return _storeObj;
            }
            set
            {
                _storeObj = value;
            }
        }

        customerServicesObj _customerServicesObj = new customerServicesObj();
        /// <summary>
        /// 客服模型
        /// </summary>
        /// <type>customerServicesObj</type>
        public customerServicesObj customerServicesObj
        {
            get
            {
                return _customerServicesObj;
            }
            set
            {
                _customerServicesObj = value;
            }
        }

        staffObj _formanObj = new staffObj();
        /// <summary>
        /// 员工模型_负责人
        /// </summary>
        /// <type>customerServicesObj</type>
        public staffObj formanObj
        {
            get
            {
                return _formanObj;
            }
            set
            {
                _formanObj = value;
            }
        }

        contactObj _contactobj = new contactObj();
        /// <summary>
        /// 服务对象
        /// </summary>
        /// <type>customerServicesObj</type>
        public contactObj contactObj
        {
            get
            {
                return _contactobj;
            }
            set
            {
                _contactobj = value;
            }
        }
    }
}
