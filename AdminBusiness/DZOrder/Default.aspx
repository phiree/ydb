<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZOrder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content" id="service-list">
        <div class="content-head normal-head">
            <h3>订单列表</h3>
        </div>
        <div class="content-main">
            <div class="animated fadeInUpSmall">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-10">
                            <div class="">
                                <div class="model">
                                    <div class="model-h">
                                        <h4>订单列表</h4>
                                    </div>
                                    <div class="model-m no-padding">
                                        <div class="order-list-head">
                                            <div class="custom-grid">
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        服务时间
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        服务项目
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        订单号
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        客户名称
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-3">
                                                    <div class="l-b">
                                                        服务地址
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        员工指派
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-2">
                                                    <div class="l-b">
                                                        订单状态
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="order-list" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="order-row">
                                                <div class="custom-grid">
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            2015-9-12
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            洗车
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            xxxxxx
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            林先生
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-3">
                                                        <div class="order-li">
                                                            北京市XXXXXXXXXX
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            <input class="btn btn-info" type="button" value="指派员工" data-role="staffAptToggle" data-appointOrderId="8787984984613481846">
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-2">
                                                        <div class="order-li">
                                                            <span>处理中</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Repeater runat="server" ID="rpOrderList" OnItemDataBound="rptOrderList_ItemDataBound"  OnItemCommand="rptOrderList_Command">
                                                <ItemTemplate>


                                                    <div class="panel panel-default">
                                                        <div class="panel-heading" role="tab" id="">
                                                            <table class="custom-table order-table" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                   href="#collapse<%# Container.ItemIndex + 1 %>" aria-expanded="true"
                                                                   aria-controls="collapse<%# Container.ItemIndex + 1 %>">
                                                                <tbody>
                                                                <tr>
                                                                    <td class="table-col-2"><%#Eval("Id") %>
                                                                    </td>
                                                                    <td class="table-col-2"><%#Eval("ServiceName") %>
                                                                    </td>
                                                                    <td class="table-col-2"><%#Eval("TargetTime") %>
                                                                    </td>
                                                                    <td class="table-col-1"><%#Eval("CustomerName") %>
                                                                    </td>
                                                                    <td class="table-col-3"><%#Eval("TargetAddress") %>
                                                                    </td>
                                                                    <td class="table-col-1 order-span-status">

                                                                        <%#Eval("OrderStatus") %>
                                                                    </td>
                                                                    <td class="table-col-1">
                                                                        <span class="order-span-ctrl">详情</span>
                                                                    </td>
                                                                </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div id="collapse<%# Container.ItemIndex + 1 %>" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading<%# Container.ItemIndex + 1 %>">
                                                            <div class="panel-body">
                                                                <div class="order-status-detail">
                                                                    <div class="order-steps">
                                                                        <div class="order-status-step mr">
                                                                            <i class="icon"></i>
                                                                            <p class="order-status-text"></p>
                                                                        </div>
                                                                        <div class="order-status-step mr">
                                                                            <i class="icon"></i>
                                                                            <p class="order-status-text"></p>
                                                                        </div>
                                                                        <div class="order-status-step mr">
                                                                            <i class="icon"></i>
                                                                            <p class="order-status-text"></p>
                                                                        </div>
                                                                        <div class="order-status-step mr">
                                                                            <i class="icon"></i>
                                                                            <p class="order-status-text mr"></p>
                                                                        </div>
                                                                        <div class="order-status-step">
                                                                            <i class="icon"></i>
                                                                            <p class="order-status-text"></p>
                                                                        </div>
                                                                    </div>
                                                                    <div class="order-lines"></div>
                                                                    <input type="hidden" value="3">
                                                                </div>
                                                                <table class="custom-table panel-detail-table">
                                                                    <tbody>
                                                                    <tr>
                                                                        <td class="table-col-1 t-r">服务订金：</td>
                                                                        <td class="table-col-3"><%#Eval("DepositAmount")%></td>
                                                                        <td class="table-col-1 t-r">服务总价：</td>
                                                                        <td class="table-col-3"><%#Eval("OrderAmount")%></td>
                                                                        <td class="table-col-1 t-r">下单时间：</td>
                                                                        <td class="table-col-3"><%#Eval("OrderCreated")%></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="table-col-1 t-r">指派员工：</td>
                                                                        <td class="table-col-3">XXX</td>
                                                                        <td class="table-col-1 t-r">客户电话：</td>
                                                                        <td class="table-col-3"><%#Eval("CustomerPhone")%></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="table-col-1 t-r">备&nbsp;&nbsp;注：</td>
                                                                        <td class="table-col-3"><%#Eval("Memo")%></td>
                                                                    </tr>
                                                                    </tbody>
                                                                </table>
                                                                <div class="order-ctrl t-r">
                                                                    <asp:HyperLink runat="server" ID="PayDepositAmount"></asp:HyperLink>
                                                                    <asp:Button runat="server" CommandName="ConfirmOrder" CommandArgument='<%#Eval("Id") %>' ID="btnConfimOrder" CssClass="btn btn-info" Text="确认订单"/>
                                                                    <asp:TextBox runat="server" CommandName="txtConfirmPrice" CommandArgument='<%#Eval("Id") %>' ID="txtConfirmPrice" Width="100"></asp:TextBox>
                                                                    <asp:Button runat="server" CommandName="ConfirmPrice" CommandArgument='<%#Eval("Id") %>' ID="btnConfirmPrice" CssClass="btn btn-info" Text="确认价格"/>-
                                                                    <asp:Button runat="server" CommandName="ConfirmPriceCustomer" CommandArgument='<%#Eval("Id") %>' ID="btnConfirmPriceCustomer" CssClass="btn btn-info" Text="用户确认价格并开始服务"/>
                                                                    <asp:Button runat="server" CommandName="IsEndOrder" CommandArgument='<%#Eval("Id") %>' ID="btnIsEndOrder" CssClass="btn btn-info" Text="订单完成"/>
                                                                    <asp:Button runat="server" CommandName="IsEndOrderCustomer" CommandArgument='<%#Eval("Id") %>' ID="btnIsEndOrderCustomer" CssClass="btn btn-info" Text="用户确认订单完成"/>
                                                                    <asp:HyperLink runat="server" ID="PayFinalPayment"></asp:HyperLink>
                                                                    <asp:Button runat="server" ID="Button3" CssClass="btn btn-info" Text="指派"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
                    CurrentPageButtonClass="cpb" PageSize="5"
                    CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
                    ShowCustomInfoSection="Right">
    </UC:AspNetPager>
    <div id="staffAppointLight" class="appointWindow dis-n">
        <div class="model">
            <div class="model-h">
                <h4>员工指派</h4>
            </div>
            <div class="model-m">
                <div id="staffsContainer" class="staffs-container">
                    <!-- 注入#staffs_temlate模版内容 -->
                </div>
            </div>
            <div class="model-b">
                <input id="appointSubmit" class="btn btn-info" type="button" value="确定指派" data-role="appointSubmit" >
                <input class="btn btn-info lightClose" type="button" value="取消" >
            </div>
        </div>

    </div>
    <script type="text/template" id="staffs_template">
        <div class="container-fluid">
            <div class="row">
                {% _.each(staffId, function(staff){ %}
                <div class="col-md-4">
                    <div class="emp-model">
                        <div class="emp-model-h">
                            <span>员工编号:&nbsp;</span><span class="emp-code"></span>
                            <div class='emp-assign-flag'></div>
                        </div>
                        <div class="emp-model-m">
                            <img class="emp-headImg" src=''/>
                            <div class="emp-info">
                                <p>姓名：</p>
                                <p>工龄：</p>
                                <p>性别：</p>
                                <p>电话：</p>
                                <p>特长技能：</p>
                            </div>
                        </div>
                        <div class="emp-model-b">
                            <input class="staffCheckbox" type="checkbox" value="指派" data-role="staff" data-staffId="{%= staff %}" id="{%= staff %}" >
                            <label for="{%= staff %}"></label>
                        </div>
                    </div>
                </div>

                {% }) %}
            </div>
        </div>
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
    <script src="/js/shelf/underscore.js"></script>
    <script src="/js/shelf/mock.js"></script>
    <script src="/js/jquery.lightBox_me.js"></script>
    <script src="/js/jquery.lightBox_me.js"></script>
    <script src="/js/staffAppoint.js"></script>
    <script>
        Mock.mockjax(jQuery);
        Mock.mock(/staff.json/, function(){
            return Mock.mock({
                'staffId|10-20' : [
                    '@guid'
                ]
            })
        })
    </script>
    <script>
        $('[data-role="staffAptToggle"]').staffAppoint({
            beforePullFunc : function (){
                return $("#staffAppointLight").lightbox_me({
                    centered : true
                });
            },
            staffContainer : '#staffsContainer',
            appointSubmit : '#appointSubmit',
            single : true,
            pullReqData : { businessId : '132131321331' }
        })
    </script>
</asp:Content>