<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="advertisement_Default" %>
 
<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
    客服与用户分配关系列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView runat="server" AutoGenerateColumns="false" ID="gvMember" >
        <Columns>
            <asp:BoundField HeaderText="客服ID" DataField="CSId" />
            <asp:BoundField HeaderText="客服名" DataField="CSName" />
            <asp:BoundField HeaderText="用户ID" DataField="CustomerId" />
            <asp:BoundField HeaderText="用户名" DataField="CustomerName" />
        </Columns>
    </asp:GridView>

    <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
        CurrentPageButtonClass="cpb" PageSize="10"
        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
        ShowCustomInfoSection="Right">
    </UC:AspNetPager>
</asp:Content>
