<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Finance_Default" %>
   <%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-head normal-head">
    <h3>财务管理</h3>
</div>
    <div class="content-main">
        <div class="animated fadeInUpSmall">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="model m-b20">
                            <div class="model-h">
                                <h4>账户信息</h4>
                            </div>
                            <div class="model-m">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="fi-remain-txt">当前账户余额：<strong>888,888.00</strong>元</div>
                                        <!--<div>当前可用余额： 0.00元</div>-->
                                        <div class="fi-remain-btns m-l50">
                                            <a href="/Finance/Recharge.aspx" class="btn btn-info m-r20">充值</a>
                                            <a href="/Finance/withDraw.aspx" class="btn btn-create">提现</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="fi-model-wrap">
                            <div class="fi-h-tabs">
                                <div class="fi-h-tab active">交易记录</div>
                                <div class="fi-h-tab">充值记录</div>
                                <div class="fi-h-tab">提现记录</div>
                            </div>
                            <div class="fi-model">
                                <div class="fi-model-h">
                                    <div class="fi-h-ctrl">
                                        <span class="fi-ctrl-h">时间</span>
                                        <div class="fi-ctrl-m">
                                            <input class="fi-h-txt" type="text" id="datePickerStart"/>&nbsp;-&nbsp;<input class="fi-h-txt" type="text" id="datePickerEnd"/>
                                        </div>
                                    </div>
                                    <div class="fi-h-ctrl">
                                        <label for="tradeType" class="fi-ctrl-h">交易类型</label>
                                        <div class="fi-ctrl-m">
                                            <select class="fi-h-txt" name="" id="tradeType">
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="fi-h-ctrl">
                                        <label for="tradeStatus" class="fi-ctrl-h">交易状态</label>
                                        <div class="fi-ctrl-m">
                                            <select class="fi-h-txt" name="" id="tradeStatus">
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="fi-h-ctrl">
                                        <input class="fi-h-btn" type="button" value="搜索"/>
                                    </div>
                                </div>
                                <div class="model-m no-padding">
                                    <div class="order-list-head">
                                        <div class="custom-grid">
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    时间
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    流水号
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    订单号
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    类型
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-2">
                                                <div class="l-b">
                                                    行为概述
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    金额
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    扣点比例
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    收入
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-1">
                                                <div class="l-b">
                                                    等待付款
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="order-list" >
                                    </div>
                                </div>
                                <div class="model-b">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bottom" runat="Server">
    <script src="/js/pikaday.js"></script>
    <script>
        $(function(){
            var pickerStart = new Pikaday(
                    {
                        field: document.getElementById('datePickerStart'),
                        firstDay: 1,
                        minDate: new Date(2000, 0, 1),
                        maxDate: new Date(2020, 12, 31),
                        yearRange: [2000,2020],
                        i18n: {
                            previousMonth : '上个月',
                            nextMonth     : '下个月',
                            months        : ['一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'],
                            weekdays      : ['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],
                            weekdaysShort : ['日','一','二','三','四','五','六']
                        }
                    });
            var pickerEnd = new Pikaday(
                    {
                        field: document.getElementById('datePickerEnd'),
                        firstDay: 1,
                        minDate: new Date(2000, 0, 1),
                        maxDate: new Date(2020, 12, 31),
                        yearRange: [2000,2020],
                        i18n: {
                            previousMonth : '上个月',
                            nextMonth     : '下个月',
                            months        : ['一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'],
                            weekdays      : ['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],
                            weekdaysShort : ['日','一','二','三','四','五','六']
                        }
                    });

            pickerStart.setDate('2015-01-01');
            pickerEnd.setDate('2015-01-01');
        });

    </script>
</asp:Content>