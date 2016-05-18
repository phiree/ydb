<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZOrder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content" id="service-list">
        <input type="hidden" value="<%=merchantID%>" id="merchantID"/>
        <div class="content-head normal-head">
            <h3>订单列表</h3>

        </div>
        <div class="content-main">
            <div class="animated fadeInUpSmall">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="m-b20">
                                <div class="order-total-card">
                                    <div class="order-card-t">
                                        <div class="order-card-icon icon-undone"></div>
                                        <div class="order-card-tr">
                                            <p><strong class="order-card-s"><asp:Literal runat="server" ID="liUnDoneOrderCount"></asp:Literal></strong>张</p>
                                            <p>未完成订单</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="order-total-card">
                                    <div class="order-card-t">
                                        <div class="order-card-icon icon-finish"></div>
                                        <div class="order-card-tr">
                                            <p><strong class="order-card-s"><asp:Literal runat="server" ID="liFinishOrderCount"></asp:Literal></strong>张</p>
                                            <p>已完成订单</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--<div class="model">-->
                                <!--<div class="model-h">-->
                                    <!--<h4>订单统计</h4>-->
                                <!--</div>-->
                                <!--<div class="model-m">-->
                                    <!---->
                                <!--</div>-->
                            <!--</div>-->
                        </div>
                        <div class="col-md-12">
                            <div class="model">
                                    <div class="model-h">
                                        <h4>订单列表</h4>
                                    </div>
                                    <div class="model-m no-padding">
                                        <div class="order-list-head">
                                            <div class="custom-grid">
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        下单时间
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        订单号
                                                    </div>
                                                </div>
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
                                                        客户姓名
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-2">
                                                    <div class="l-b">
                                                        服务地址
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        指派员工
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        订单状态
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        订单详情
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-list" id="accordion" role="tablist" aria-multiselectable="true">
                                            <asp:Repeater runat="server" ID="rpOrderList" OnItemDataBound="rpt_ItemDataBound" >
                                                <ItemTemplate>
                                                    <div class="order-row">
                                                        <div class="custom-grid">
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li order-li-time">
                                                                    <p><%#  Eval("OrderCreated").ToString().Split( )[0] %></p>
                                                                    <p><%#  Eval("OrderCreated").ToString().Split( )[1] %></p>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <%#Eval("ID") %>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li order-li-time">
                                                                    <p><%#  Eval("TargetTime").ToString().Split( )[0] %></p>
                                                                    <p><%#  Eval("TargetTime").ToString().Split( )[1] %></p>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <%# Eval("Title").ToString().Replace(";", "") %>
                                                                </div>
                                                            </div>

                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <%#Eval("Customer.DisplayName") %>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-2">
                                                                <div class="order-li">
                                                                    <%#Eval("TargetAddress") %>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <asp:Label runat="server" ID="assignStaffs"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <%#Eval("OrderStatusStr") %>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <a href="Detail.aspx?businessId=<%=Request["businessid"] %>&orderId=<%#Eval("Id")%>" class="btn btn-info-light btn-xs">订单详情</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    <div class="model-b">
                                        <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager" AlwaysShow="true"
                                                        CurrentPageButtonClass="cpb" PageSize="100"
                                                        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
                                                        ShowCustomInfoSection="Right">
                                        </UC:AspNetPager>
                                    </div>
                                </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>