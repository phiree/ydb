<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Generator.aspx.cs" Inherits="CashTicket_Generator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
 
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
选择现金券:<asp:DropDownList  ID="ddlTemplate" runat="server">
</asp:DropDownList>
生成数量:<asp:TextBox runat="server" id="tbxTotal"></asp:TextBox>
<asp:Label runat="server" ID="lblAmount"></asp:Label>
<asp:Button runat="server"  ID="btnGenerate" OnClick="btnGenerate_Click" Text="确定生成"/>
<a href="Default.aspx">返回生成列表</a>
</asp:Content>

