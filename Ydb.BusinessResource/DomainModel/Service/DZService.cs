using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;
using Ydb.Common;
using Ydb.BusinessResource.DomainModel;
namespace Ydb.BusinessResource.DomainModel
{
    /// <summary>
    /// 具体某项服务的定义
    /// </summary>
    public class DZService: Entity<Guid>
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model.DZService");
        public DZService()
        {
            
            Enabled = true;
            IsDeleted = false;
            InitOpenTimes();

        }
        private void InitOpenTimes()
        {
            OpenTimes = new List<ServiceOpenTime>();
            for (int i = 0; i < 7; i++)
            {
                ServiceOpenTimeForDay sotd = new ServiceOpenTimeForDay
                {
                    TimePeriod = new TimePeriod(new Time("08:00"), new Time("12:00"))
                    
                };
                ServiceOpenTimeForDay sotd2 = new ServiceOpenTimeForDay
                {
                    TimePeriod = new TimePeriod(new Time("14:00"), new Time("18:00"))
                   
                };
                var sotdlist = new List<ServiceOpenTimeForDay>();
                sotdlist.Add(sotd);
                sotdlist.Add(sotd2);
                ServiceOpenTime sto = new ServiceOpenTime
                {
                    DayOfWeek = (DayOfWeek)i,

                    OpenTimeForDay = sotdlist
                };
                if (OpenTimes != null)
                {
                    OpenTimes.Add(sto);
                }
            }

        }
 
        #region  属性
        /// <summary>
        /// 服务项目所属类别 
        /// </summary>
        public virtual ServiceType ServiceType { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 所属商家
        /// </summary>
        public virtual  Business Business { get; set; }
        /// <summary>
        /// 服务范围 
        /// </summary>
        public virtual string Scope { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public virtual string Description { get; set; }
        
        /// <summary>
        /// 每日接单总量
        /// </summary>       
        public virtual int MaxOrdersPerDay { get; set; }


        /// <summary>
        /// 允许的支付方式
        /// </summary>
        public virtual enum_PayType AllowedPayType { get; set; }

       

        /// <summary>
        /// 服务保障:是否先行赔付
        /// </summary>
        public virtual bool IsCompensationAdvance
        { get; set; }
        /// <summary>
        /// 服务准备时长(提前多长时间预约).单位分钟.
        /// </summary>
        public virtual int OrderDelay { get; set; }
        /// <summary>
        /// 是否可以对公. 否:只能为私人提供
        /// </summary>
        public virtual bool IsForBusiness { get; set; }
        /// <summary>
        /// 是否通过平台标准认证"
        /// </summary>
        public virtual bool IsCertificated { get; set; }
        /// <summary>
        /// 最低服务费
        /// </summary>
        public virtual decimal MinPrice { get; set; }
        /// <summary>
        /// 单位时间费用 25/小时.
        /// </summary>
        public virtual decimal UnitPrice { get; set; }
        /// <summary>
        /// 计费单位: 小时, 天,次等
        /// </summary>
        public virtual enum_ChargeUnit ChargeUnit { get; set; }
        /// <summary>
        /// 计费单位英文转化为中文 refactor: 应用层的职责.
        /// </summary>
        public virtual string ChargeUnitFriendlyName
        {
            get
            {
                string unit = string.Empty;
                switch (this.ChargeUnit)
                {
                    case enum_ChargeUnit.Day: unit = "天"; break;
                    case enum_ChargeUnit.Hour: unit = "时"; break;
                    case enum_ChargeUnit.Month: unit = "月"; break;
                    case enum_ChargeUnit.Times: unit = "次"; break;
                    default: unit = "未知计费单位类型"; break;
                }
                return unit;
            }
            set
            {
                try
                {
                    this.ChargeUnit = ( enum_ChargeUnit)Enum.Parse(typeof(  enum_ChargeUnit), value);
                }
                catch(Exception ex)
                {
                    switch (value.ToLower())
                    {
                        case "day": 
                        case "天": this.ChargeUnit = enum_ChargeUnit.Day ; break;
                        case "hour":
                        case "时": this.ChargeUnit = enum_ChargeUnit.Hour; break;
                        case "month":
                        case "月": this.ChargeUnit = enum_ChargeUnit.Month; break;
                        case "times":
                        case "次": this.ChargeUnit = enum_ChargeUnit.Times; break;
                        default: throw ex; 
                    }
                }
            }
        }

        public virtual void CopyTo(DZService newService)
        {
            newService.Id = Id;
            newService.Name = Name;
            newService.Business = Business;
            newService.ServiceType = ServiceType;
            newService.Description = Description;

          //  newService.BusinessAreaCode = BusinessAreaCode;
            newService.MinPrice = MinPrice;
            newService.UnitPrice = UnitPrice;
            newService.DepositAmount = DepositAmount;
            newService.OrderDelay = OrderDelay;
            newService.OpenTimes = OpenTimes;
            newService.ServiceMode = ServiceMode;
            newService.IsForBusiness = IsForBusiness;
            newService.AllowedPayType = AllowedPayType;
            newService.IsCompensationAdvance = IsCompensationAdvance;
            newService.IsCertificated = IsCertificated;
            newService.ChargeUnit = ChargeUnit;
            newService.FixedPrice = FixedPrice;
            newService.Enabled = Enabled;
            newService.OverTimeForCancel = OverTimeForCancel;
            newService.CancelCompensation = CancelCompensation;
        }


        /// <summary>
        /// 一口价
        /// </summary>
        public virtual decimal FixedPrice { get; set; }

        /// <summary>
        /// 服务提供方式:上门, 不上门..等.
        /// </summary>
        public virtual enum_ServiceMode ServiceMode { get; set; }

        public virtual DateTime CreatedTime { get; set; }
        public virtual DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public virtual bool Enabled { get; set; }
        public virtual IList<ServiceOpenTime> OpenTimes { get; set; }
        /// <summary>
        /// 是否为先付
        /// </summary>
        [Obsolete]
        public virtual bool PayFirst { get; set; }

        /// <summary>
        /// 取消超时:用户申请取消订单时,如果超过该时间则需要支付赔偿金
        /// 单位:分钟
        /// </summary>
        public virtual int OverTimeForCancel { get; set; }
        /// <summary>
        /// 超时取消需要支付的赔偿金
        /// </summary>
        public virtual decimal CancelCompensation { get; set; }
        /// <summary>
        /// 定金
        /// </summary>
        public virtual decimal DepositAmount { get; set; }
        #endregion

        /// <summary>
        /// 解析服务区域字符串
        /// </summary>
        /// <returns></returns>
        public virtual string GetServiceArea()
        {
            System.Text.RegularExpressions.Regex RegExp = new System.Text.RegularExpressions.Regex(@"(?<=""serPointAddress"":"").*?(?=""})");
            System.Text.RegularExpressions.Match mach = RegExp.Match(this.Scope);
            return mach.Value;
        }

         

         string errMsg;
        /// <summary>
        /// 获取给定时间(预约时间)的时间项快照
        /// </summary>
        /// <param name="targetTime"></param>
        /// <returns></returns>

        /*public virtual ServiceOpenTimeForDay  GetOpenTimeSnapShot(DateTime targetTime)
        {
            var targetOpenTime = OpenTimes.Where(x => x.DayOfWeek == targetTime.DayOfWeek);
            int count = targetOpenTime.Count();
            errMsg = "时间项应该有且只有一项";
            System.Diagnostics.Debug.Assert(count == 1, errMsg);
            if (count != 1)
            {
                log.Error(errMsg);
                throw new Exception(errMsg);
            }

            return targetOpenTime.ToList()[0]
                .GetItem(targetTime);//.GetSnapShop(targetTime);
              
            
            
        }*/
        public virtual ServiceOpenTime  GetWorkDay (DayOfWeek dayOfWeek,out string errMsg)
        {
            errMsg = string.Empty;
            var targetOpenTime = OpenTimes.Where(x => x.DayOfWeek == dayOfWeek);
            int count = targetOpenTime.Count();
            if (count != 1)
            {
                if (count > 1)
                {
                    errMsg = "时间项应该有且只有一项";
                    log.Error(errMsg);
                }
                return null;
            }
            else if (count == 0)
            {
                throw new Exception("没有对应的工作日");
            }
            return targetOpenTime.ToList()[0];
         //   return new ServiceOpenTimeSnapshot { MaxOrderForDay=targetOpenTime.ToList()[0].MaxOrderForDay };

        }
        public virtual ServiceOpenTimeForDay GetWorkTime(DateTime dateTime)
        { string errMsg;
            ServiceOpenTime workDay = GetWorkDay(dateTime.DayOfWeek, out errMsg);
           return workDay.GetItem(dateTime.ToString("HH:mm"));
        }
        //添加一天的定义服务时间
        public virtual bool AddOpenTime(DayOfWeek dayOfWeek,int maxOrder, out string errMsg)
        {
            errMsg = string.Empty;
            var existed = GetWorkDay(dayOfWeek,out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return false;
            }
            if (existed == null)
            {
                ServiceOpenTime openTime = new ServiceOpenTime();
                openTime.MaxOrderForDay = maxOrder;
                openTime.DayOfWeek = dayOfWeek;
                this.OpenTimes.Add(openTime);
                return true;

            }

            if ((int)existed.DayOfWeek < 1 || (int)existed.DayOfWeek > 7)
            {
                errMsg = "星期数有误,只能是1到7";
                return false;

            }
            if (OpenTimes.Any(x => x.DayOfWeek == existed.DayOfWeek))
            {
                errMsg = "已经定义了星期" + existed.DayOfWeek + "的时间";
                return false;
            }
            return true;
        }
        public virtual ServiceOpenTimeForDay AddWorkTime(DayOfWeek dayOfWeek,string startTime, string endTime, int maxOrder,string tag)
        {
            string errMsg;
            ServiceOpenTime openTime = GetWorkDay(dayOfWeek,out errMsg);
            TimePeriod workTime = new TimePeriod(new Time(startTime), new Time(endTime));

            ServiceOpenTimeForDay openTimeForDay = new ServiceOpenTimeForDay(tag, maxOrder, openTime, workTime);
            openTime.AddServicePeriod(openTimeForDay);
            return openTimeForDay;
        }   
        /// <summary>
        /// 修改一个工作时间.
        /// </summary>
        public virtual void ModifyWorkTimePeriod(DayOfWeek dayOfWeek,TimePeriod oldPeriod,TimePeriod newPeriod, out string errMsg)
        {
            errMsg = string.Empty;
            //获取要修改的时间段
            ServiceOpenTime serviceOpenTime = GetWorkDay(dayOfWeek, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }
          
            ServiceOpenTimeForDay existedPeriod = serviceOpenTime.GetItem(oldPeriod);
            PeriodValidator periodList = new PeriodValidator(serviceOpenTime.OpenTimeForDay.Select(x=>x.TimePeriod).ToList());
            bool canModify= periodList.CanModify(oldPeriod, newPeriod, out errMsg);
            if (canModify)
            {
                serviceOpenTime.OpenTimeForDay.Remove(existedPeriod);
                serviceOpenTime.OpenTimeForDay.Add(new ServiceOpenTimeForDay
                {
                    Enabled = existedPeriod.Enabled,
                    MaxOrderForOpenTime = existedPeriod.MaxOrderForOpenTime,
                    Tag=existedPeriod.Tag,
                    ServiceOpenTime = serviceOpenTime,
                    TimePeriod = newPeriod,
                
                });
            }
            else
            {
                throw new Exception("工作时间冲突");
            }

        }

        public virtual void DeleteWorkTime(DayOfWeek dayOfWeek, TimePeriod period)
        {
            string errmsg;
            var workDay = GetWorkDay(dayOfWeek,out errMsg);
            workDay.DeleteWorkTime(period);
            
        }
        public virtual void ModifyWorkTimeEnable(DayOfWeek dayOfWeek, TimePeriod oldPeriod,bool isEnabled)
        {
            ServiceOpenTimeForDay workTime = GetWorkTime(dayOfWeek, oldPeriod);
            workTime.Enabled = isEnabled;
        }

     
        public virtual void ModifyWorkTimeMaxOrder(DayOfWeek dayOfWeek, TimePeriod oldPeriod,int maxOrder)
        {
            ServiceOpenTimeForDay workTime = GetWorkTime(dayOfWeek, oldPeriod);
            workTime.MaxOrderForOpenTime =maxOrder;

        }
        public virtual void ModifyWorkTimeTag(DayOfWeek dayOfWeek, TimePeriod oldPeriod, string tag)
        {
            ServiceOpenTimeForDay workTime = GetWorkTime(dayOfWeek, oldPeriod);
            workTime.Tag = tag;

        }
        public virtual ServiceOpenTimeForDay GetWorkTime(DayOfWeek dayOfWeek, TimePeriod period)
        {
            ServiceOpenTime serviceOpenTime = GetWorkDay(dayOfWeek, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            ServiceOpenTimeForDay workTime = serviceOpenTime.GetItem(period);
            return workTime;
        }


    }
}
