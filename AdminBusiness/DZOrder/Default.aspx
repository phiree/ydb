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
                        <div class="col-md-10">
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
                                                <div class="custom-col col-10-2">
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
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        订单详情
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-list" id="accordion" role="tablist" aria-multiselectable="true">
                                            <asp:Repeater runat="server" ID="rpOrderList" OnItemDataBound="rptOrderList_ItemDataBound"  OnItemCommand="rptOrderList_Command">
                                                <ItemTemplate>
                                                    <div class="order-row">
                                                        <div class="custom-grid">
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                               <%#  Eval("TargetTime") 
                                                                        %>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <%# Eval("Title").ToString().Replace(";", "") %>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                      <%#Eval("ID") %>
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
                                                                    <input class="btn btn-info btn-xs" type="button" value="指派员工" data-role="appointToggle" data-appointTargetId='<%#Eval("Id") %>' >
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-2">
                                                                <div class="order-li">
                                                                    <%#Eval("OrderStatus") %>
                                                                </div>
                                                            </div>
                                                            <div class="custom-col col-10-1">
                                                                <div class="order-li">
                                                                    <a href="Detail.aspx?businessId=<%=Request["businessid"] %>" class="btn btn-info-light btn-xs">订单详情</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    <div class="model-b">
                                        <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
                                                        CurrentPageButtonClass="cpb" PageSize="10"
                                                        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
                                                        ShowCustomInfoSection="Right">
                                        </UC:AspNetPager>
                                    </div>
                                </div>
                        </div>
                        <div class="col-md-2">
                            <div class="model">
                                <div class="model-h">
                                    <h4>订单列表</h4>
                                </div>
                                <div class="model-m">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="staffAppointLight" class="appointWindow dis-n">
        <div class="model">
            <div class="model-h">
                <h4>员工指派</h4>
            </div>
            <div class="model-m">
                <div class="row">
                    <div class="col-md-12">
                        <div id="staffsContainer" class="staffs-container">
                            <!-- 注入#staffs_temlate模版内容 -->
                        </div>
                    </div>
                </div>
            </div>
            <div class="model-b">
                <input id="appointSubmit" class="btn btn-info" type="button" value="确定指派" data-role="appointSubmit" >
                <input class="btn btn-cancel-light lightClose" type="button" value="取消" >
            </div>
        </div>
    </div>
    <script type="text/template" id="staffs_template">
                <div class="col-md-4">
                    <div class="emp-model">
                        <div class="emp-model-h">
                            <span>员工编号:&nbsp;</span><span class="emp-code"></span>
                            <div class='emp-assign-flag'></div>
                        </div>
                        <div class="emp-model-m">
                            <img class="emp-headImg" src='{%= imgUrl %}'/>
                            <div class="emp-info">
                                <p>昵称：{%= alias %}</p>
                                <p>姓名：{%= alias %}</p>
                                <p>性别：{%= alias %}</p>
                                <p>电话：{%= phone %}</p>
                            </div>
                        </div>
                        <div class="emp-model-b" data-mark="{%= mark %}">
                            <input class="staffCheckbox" type="checkbox" {% if (mark==="Y") { %} checked {% } %} value="指派" data-role="item" data-itemId="{%= userID %}" id="{%= userID %}" >
                            <label for="{%= userID %}"></label>
                        </div>
                    </div>
                </div>
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
    <script src="/js/libs/underscore.js"></script>
    <script src="/js/test/mock.js"></script>
    <script src="/js/jquery.lightBox_me.js"></script>
    <script src="/js/interfaceAdapter.js"></script>
    <script src="/js/appointToOrder.js"></script>
</asp:Content>