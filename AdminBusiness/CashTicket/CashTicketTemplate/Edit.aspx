﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="CashTicketTemplateEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<thead>
<tr><td></td></tr>
</thead>
<tbody>
<tr><th>名称</th><td><asp:TextBox runat="server" ID="tbx_name"></asp:TextBox></td></tr>
<tr><th>面额</th><td><asp:TextBox runat="server" ID="tbx_amount"></asp:TextBox></td></tr>
<tr><th>失效日期</th><td><asp:TextBox runat="server" ID="tbx_expiredDate"></asp:TextBox></td></tr>
<tr><th>使用条件</th><td><asp:TextBox runat="server" ID="tbx_conditions"></asp:TextBox></td></tr>
<tr><th>覆盖范围</th><td><asp:TextBox runat="server" ID="tbx_coverage"></asp:TextBox></td></tr>
<tr><th>启用</th><td><asp:CheckBox runat="server" ID="cbxEnable"  Text="启用" /></td></tr>
<tr><td colspan=2><asp:Button runat="server" OnClick="btnOK_Click" Text="确定" /></td></tr>
</tbody>
<tfoot></tfoot>
</table>
<a href="default.aspx">返回列表</a>
</asp:Content>

