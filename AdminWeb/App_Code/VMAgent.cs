using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VMAgent 的摘要说明
/// </summary>
public class VMAgent
{
    public VMAgent()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 注册时间
    /// </summary>
    public DateTime RegisterTime { get; set; }
    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }
    /// <summary>
    /// 系统帐号
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 所在城市
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// 在线时长(分钟)
    /// </summary>
    public int TotalMinutesOnline { get; set; }
    /// <summary>
    /// 每小时平均接单量
    /// </summary>
    public decimal OrderAmountPerHour { get; set; }
    /// <summary>
    /// 订单总数
    /// </summary>
    public int OrderAmount { get; set; }
    /// <summary>
    /// 代理得分
    /// </summary>
    public decimal Score { get; set; }
    /// <summary>
    /// 分账总收益
    /// </summary>
    public decimal SharedProfit { get; set; }

}