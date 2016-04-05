<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="membership_roles_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr><td>角色名:</td><td><asp:TextBox runat="server" ID="tbxRoleName"></asp:TextBox></td></tr>
        <tr><td><asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="保存" /> </td>
            <td><asp:Label runat="server" ID="lblMsg"></asp:Label></td>

        </tr>
    </table>
    <a href="Default.aspx">返回列表</a></asp:Content>

