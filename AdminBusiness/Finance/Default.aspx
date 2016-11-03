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
                                        <div class="fl-remain-module">
                                            <div class="_remain-col">
                                                <div class="_remain-title">账户余额</div>
                                                <div class="_remain-count"><span class="_remain-count-s"><strong class="_s-red">56,00.00</strong>元</span></div>
                                                <a href='/Finance/withDraw.aspx?businessid=<%=Request["businessid"]%>' class="btn _remain-btn _btn-fin-green">提现</a>
                                            </div>
                                            <div class="_remain-col">
                                                <div class="_remain-title">先行赔付余额</div>
                                                <div class="_remain-count"><span class="_remain-count-s"><strong class="_s">600,00.00</strong>元</span></div>
                                                <a href='/Finance/recharge.aspx?businessid=<%=Request["businessid"]%>' class="btn _remain-btn _btn-fin-blue">充值</a>
                                            </div>
                                            <div class="_remain-col">
                                                <div class="_remain-title">绑定账户</div>
                                                <div class="_remain-count"><span class="_remain-count-s"><strong class="_s">licdream@126.com</strong>元</span></div>
                                                <a href='/Finance/thirdParty_Edit.aspx?businessid=<%=Request["businessid"]%>' class="btn _remain-btn _btn-fin-yellow">变更/绑定</a>
                                            </div>
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
                            </div>
                            <div class="fi-model">
                                <div class="fi-model-h">
                                    <div class="fi-h-ctrl">
                                        <span class="fi-ctrl-h">时间</span>
                                        <div class="fi-ctrl-m">
                                            <!--<input class="fi-h-txt" type="text" id="datePickerStart"/>&nbsp;-&nbsp;<input class="fi-h-txt" type="text" id="datePickerEnd"/>-->
                                        </div>
                                    </div>
                                    <!--<div class="fi-h-ctrl">-->
                                        <!--<label for="tradeType" class="fi-ctrl-h">交易类型</label>-->
                                        <!--<div class="fi-ctrl-m">-->
                                            <!--<select class="fi-h-txt" name="" id="tradeType">-->
                                                <!--<option value="1">1</option>-->
                                                <!--<option value="2">2</option>-->
                                            <!--</select>-->
                                        <!--</div>-->
                                    <!--</div>-->
                                    <!--<div class="fi-h-ctrl">-->
                                        <!--<label for="tradeStatus" class="fi-ctrl-h">交易状态</label>-->
                                        <!--<div class="fi-ctrl-m">-->
                                            <!--<select class="fi-h-txt" name="" id="tradeStatus">-->
                                                <!--<option value="1">1</option>-->
                                                <!--<option value="2">2</option>-->
                                            <!--</select>-->
                                        <!--</div>-->
                                    <!--</div>-->
                                    <!--<div class="fi-h-ctrl">-->
                                        <!--<input class="fi-h-btn" type="button" value="搜索"/>-->
                                    <!--</div>-->
                                </div>
                                <div class="model-m no-padding">
                                    <div class="order-list-head finance-list-head">
                                        <div class="custom-grid">

                                            <div class="custom-col col-10-2">
                                                <div class="l-b">
                                                    流水号
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-2">
                                                <div class="l-b">
                                                    类型
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-2">
                                                <div class="l-b">
                                                    订单号
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-2">
                                                <div class="l-b">
                                                    收入
                                                </div>
                                            </div>
                                            <div class="custom-col col-10-2">
                                                <div class="l-b">
                                                    时间
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="order-list finance-list" >
                                        <asp:Repeater runat="server" ID="rpFinanceList" OnItemDataBound="rpFinanceList_ItemDataBound" >
                                            <ItemTemplate>
                                                <div class="order-row">
                                                    <div class="custom-grid">

                                                        <div class="custom-col col-10-2">
                                                            <div class="order-li">
                                                                <%#Eval("id") %>
                                                            </div>
                                                        </div>
                                                        <div class="custom-col col-10-2">
                                                            <div class="order-li">
                                                                <%# ( Eval("FlowType").ToString() == "OrderShare") ? "订单账单":"其他" %>
                                                            </div>
                                                        </div>
                                                        <div class="custom-col col-10-2">
                                                            <div class="order-li">
                                                                <a class="order-href" href='/DZOrder/Detail.aspx?businessId=<%= Request["businessid"] %>&orderId=<%#Eval("RelatedObjectId").ToString()%>'><asp:Literal runat="server" ID="liSerialNo"></asp:Literal></a>
                                                            </div>
                                                        </div>
                                                        <div class="custom-col col-10-2">
                                                            <div class="order-li finance-amount">
                                                                <%# String.Format( "{0:F}", Eval("Amount")) %>
                                                            </div>
                                                        </div>
                                                        <div class="custom-col col-10-2">
                                                            <div class="order-li">
                                                                <%#Eval("OccurTime") %>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

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
    <script src="/js/plugins/pikaday.js"></script>
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

            var amount=0;
            $(".finance-amount").each(function(){
                amount += parseFloat($(this).html());
            });
            $("#fi-amount").html(amount.toFixed(2));
        });

    </script>
</asp:Content>