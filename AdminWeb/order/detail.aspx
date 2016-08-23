<%@ Page MasterPageFile="~/admin.master" Language="C#" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="order_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
订单详情
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td>订单标题：</td>
            <td>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>商家名称：</td>
            <td>
                <asp:Label ID="lblServiceBusinessName" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>商家描述：</td>
            <td>
                <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>客户：</td>
            <td>
                <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label></td>
        </tr>
         <tr>
            <td>创建此订单的客服：</td>
            <td>
                <asp:Label ID="lblCustomerService" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>下单时间：</td>
            <td>
                <asp:Label ID="lblOrderCreated" runat="server" Text=""></asp:Label></td>
        </tr>
         <tr>
            <td>确认时间：</td>
            <td>
                <asp:Label ID="lblOrderConfirmTime" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>最近的更新时间：</td>
            <td>
                <asp:Label ID="lblLatestOrderUpdated" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>订单结束的时间：</td>
            <td>
                <asp:Label ID="lblOrderFinished" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>订单服务开始时间：</td>
            <td>
                <asp:Label ID="lblOrderServerStartTime" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>订单服务结束的时间：</td>
            <td>
                <asp:Label ID="lblOrderServerFinishedTime" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>订单备注：</td>
            <td>
                <asp:Label ID="lblMemo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>订单状态：</td>
            <td>
                <asp:Label ID="lblOrderStatus" runat="server" Text=""></asp:Label></td>
        </tr>
         <tr>
            <td>服务的目标地址：</td>
            <td>
                <asp:Label ID="lblTargetAddress" runat="server" Text=""></asp:Label></td>
        </tr>
         <tr>
            <td>用户预定的服务时间：</td>
            <td>
                <asp:Label ID="lblTargetTime" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>分配的职员：</td>
            <td>
                <asp:Label ID="lblStaff" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>服务总数：</td>
            <td>
                <asp:Label ID="lblUnitAmount" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>预期总价：</td>
            <td>
                <asp:Label ID="lblOrderAmount" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>订金：</td>
            <td>
                <asp:Label ID="lblDepositAmount" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>协商总价：</td>
            <td>
                <asp:Label ID="lblNegotiateAmount" runat="server" Text=""></asp:Label></td>
        </tr>
       <tr>
            <td>支付总额：</td>
            <td>
                <asp:Label ID="lblGetPayAmount" runat="server" Text="0"></asp:Label></td>
        </tr>
         <tr>
            <td>分成总额：</td>
            <td>
                <asp:Label ID="lblShareAmount" runat="server" Text="0"></asp:Label></td>
        </tr>
         <tr>
            <td>助理分成：</td>
            <td>
                <asp:Label ID="lblCustomerServiceShare" runat="server" Text="0"></asp:Label></td>
        </tr>
         <tr>
            <td>代理分成：</td>
            <td>
                <asp:Label ID="lblAgentShare" runat="server" Text="0"></asp:Label></td>
        </tr>
         <tr>
            <td>平台分成：</td>
            <td>
                <asp:Label ID="lblPlatformShare" runat="server" Text="0"></asp:Label></td>
        </tr>
    </table>
    <center>
        <a href='index.aspx'>返回</a></center>
</asp:Content>