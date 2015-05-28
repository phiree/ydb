<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="membership_Default" %>
 
<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView runat="server" AutoGenerateColumns="false" ID="gvMember">
        <Columns>
            <asp:BoundField HeaderText="用户名" DataField="UserName" />
            <asp:BoundField HeaderText="创建时间" DataField="TimeCreated" />
        </Columns>
    </asp:GridView>

 <uc:AspNetPager runat=server  UrlPaging="true" ID="pager" CssClass="anpager" CurrentPageButtonClass="cpb"   PageSize="3"></uc:AspNetPager>
</asp:Content>
