<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZOrder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/order.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cont-wrap">
        <div class="mh-in">
            <div class="cont-container animated fadeInUpSmall">
                <div class="mh-ctnr">

                    <div id="order-list" class="order-list dis-n">
                        <div class="order-total">
                            <div class="cont-row">
                                <div class="cont-col-4"><i class="icon order-icon-status1"></i>未处理订单</div>
                                <div class="cont-col-4"><i class="icon order-icon-status2"></i>正在进行订单</div>
                                <div class="cont-col-4"><i class="icon order-icon-status3"></i>已处理订单</div>
                            </div>
                        </div>
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="cont-row order-head">
                                <div class="cont-col-2">订单号</div>
                                <div class="cont-col-2">服务项目</div>
                                <div class="cont-col-2">服务时间</div>
                                <div class="cont-col-1">客户名称</div>
                                <div class="cont-col-3">服务地址</div>
                                <div class="cont-col-1">订单状态</div>
                                <div class="cont-col-1">操作</div>
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
                                                        <td class="table-col-2"><%#Eval("Title") %>
                                                        </td>
                                                        <td class="table-col-2"><%#Eval("TargetTime") %>
                                                        </td>
                                                        <td class="table-col-1"><%#Eval("Customer.DisplayName") %>
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
                                                            <td class="table-col-3"><%#Eval("Customer.Phone")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="table-col-1 t-r">备&nbsp;&nbsp;注：</td>
                                                            <td class="table-col-3"><%#Eval("Memo")%></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <div class="order-ctrl t-r">
                                                    <asp:HyperLink runat="server" ID="PayDepositAmount" Visible="false"></asp:HyperLink>
                                                    <asp:Button runat="server" CommandName="ConfirmOrder" CommandArgument='<%#Eval("Id") %>' ID="btnConfimOrder" CssClass="btn btn-info" Text="确认订单" Visible="false"/>
                                                    <asp:TextBox runat="server" CommandName="txtConfirmPrice" CommandArgument='<%#Eval("Id") %>' ID="txtConfirmPrice" Width="100" Visible="false"></asp:TextBox>
                                                    <asp:Button runat="server" CommandName="ConfirmPrice" CommandArgument='<%#Eval("Id") %>' ID="btnConfirmPrice" CssClass="btn btn-info" Text="确认价格" Visible="false"/>
                                                    <asp:Button runat="server" CommandName="ConfirmPriceCustomer" CommandArgument='<%#Eval("Id") %>' ID="btnConfirmPriceCustomer" CssClass="btn btn-info" Text="用户确认价格并开始服务" Visible="false"/>
                                                    <asp:Button runat="server" CommandName="IsEndOrder" CommandArgument='<%#Eval("Id") %>' ID="btnIsEndOrder" CssClass="btn btn-info" Text="订单完成" Visible="false"/>
                                                    <asp:Button runat="server" CommandName="IsEndOrderCustomer" CommandArgument='<%#Eval("Id") %>' ID="btnIsEndOrderCustomer" CssClass="btn btn-info" Text="用户确认订单完成" Visible="false"/>
                                                    <asp:HyperLink runat="server" ID="PayFinalPayment" Visible="false"></asp:HyperLink>
                                                    <asp:Button runat="server" ID="Button3" CssClass="btn btn-info" Text="指派"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div id="order-new" class="dis-n">
                        <div class="new-box">
                            <div class="t-c">
                                <img src="/image/service-new.png"/>
                            </div>
                            <div class="order-new-add">
                                <a id="firstAddBusiness" class="new-add-btn" href="javascript:void(0);">无订单内容</a>
                            </div>
                        </div>
                    </div>

                    <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
                        CurrentPageButtonClass="cpb" PageSize="5"
                        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
                        ShowCustomInfoSection="Right">
                    </UC:AspNetPager>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
    <script>
        $(function(){
            var statusTextJson = [
                {
                    doneText: "已支付",
                    curText: "支付中",
                    waitText: "未支付"
                },
                {
                    doneText: "已支付已指派",
                    curText: "已支付未指派",
                    waitText: "未支付未指派"
                },
                {
                    doneText: "已指派",
                    curText: "指派中",
                    waitText: "未指派"
                },
                {
                    doneText: "已服务已收款",
                    curText: "已服务未收款",
                    waitText: "未服务未收款"
                },
                {
                    doneText: "已收款",
                    curText: "收款中",
                    waitText: "未收款"
                }
            ];

            $('.order-status-detail').each(function(){
                var $this = $(this);
                var currentStep = parseInt($this.find("input[type=hidden]").val());
                var step = $this.find('.order-status-step');
                var doneStep = (currentStep - 1 ) < 0 ? 0 : currentStep;

                step.eq(currentStep).addClass("cur-step").siblings().removeClass("cur-step");
                step.eq(currentStep).addClass("cur-step").siblings().removeClass("cur-step");

                step.eq(currentStep).find(".order-status-text").html(statusTextJson[0].curText);

                step.slice(0 , doneStep ).addClass("done-step");
                step.slice( doneStep , -1 ).removeClass("done-step");

                step.slice(0 , doneStep).each(function(index){
                    $(this).find('.order-status-text').html(statusTextJson[index].doneText);
                });
                step.slice(currentStep + 1).each(function(index){
                    $(this).find('.order-status-text').html(statusTextJson[currentStep + 1 + index ].waitText);
                })
            })

            if ( $("#accordion").children(".panel").length == 0 ){
                $("#order-new").removeClass("dis-n");
            } else {
                $("#order-list").removeClass("dis-n");
            }

//            $(".order-table tbody tr:even").addClass("list-item-odd");
        })
    </script>
</asp:Content>