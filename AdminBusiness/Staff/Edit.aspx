<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="Staff_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
         
        <tr>
            <td>
                编号
            </td>
            <td>
                <asp:TextBox ID="Code" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                姓名
            </td>
            <td>
                <asp:TextBox ID="Name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                昵称
            </td>
            <td>
                <asp:TextBox ID="NickName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                性别
            </td>
            <td>
                <asp:TextBox ID="Gender" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                电话
            </td>
            <td>
                <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                相片
            </td>
            <td>
                <asp:TextBox ID="Photo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                所属服务分类
            </td>
            <td>
                <asp:CheckBoxList runat="server" ID="cbxServices" DataTextField="Name" DataValueField="Id">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" Text="确认" OnClick="btnOK_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
