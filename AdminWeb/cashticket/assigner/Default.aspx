<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="cashticket_assigner_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
手动分配现金券
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

选择分配的区域: <asp:DropDownList runat="server" ID="ddlArea" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
<br />
商家总数:<asp:Label runat="server" ID="lblBusinessAmount"></asp:Label>
现金券总数:<asp:Label runat="server" ID="lblCashticketAmount"></asp:Label>
<br />
<asp:Button runat="server" ID="btnAssign" OnClick="btnAssign_Click" Text="开始分配" />
</asp:Content>

