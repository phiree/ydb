<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="business_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
商家管理
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <!--商户列表-->
    <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvBusiness">
        <Columns>
            <asp:HyperLinkField Text="详情" DataNavigateUrlFields="id" DataNavigateUrlFormatString="detail.aspx?id={0}" HeaderStyle-Width="30px"/>
            <asp:BoundField HeaderText="商家名称" DataField="Name" HeaderStyle-Width="150px"/>
            <asp:BoundField HeaderText="商家地址" DataField="Address" HeaderStyle-Width="300px"/>
            <asp:BoundField HeaderText="联系人" DataField="Contact" HeaderStyle-Width="50px"/>
            <asp:BoundField HeaderText="联系电话" DataField="Phone" HeaderStyle-Width="120px"/>
            <asp:BoundField HeaderText="创建时间" DataField="CreatedTime" HeaderStyle-Width="130px"/>
        </Columns>
    </asp:GridView>

    <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
        CurrentPageButtonClass="cpb" PageSize="10"
        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
        ShowCustomInfoSection="Right">
    </UC:AspNetPager>
</asp:Content>

