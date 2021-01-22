<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="servicetype_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                名称
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxName"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>
                分成比例
            </td>
            <td>
                <asp:Label runat="server" ID="lblPoint"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                父类
            </td>
            <td>
                已选父类:<asp:Label runat="server" ID="lblParentName"></asp:Label>
                <br />
                修改父类:
                <asp:RadioButtonList runat="server" ID="rbl_Parent" DataTextField="Name" DataValueField="Id">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
            <td>
                <%--<asp:Button runat="server" ID="btnOK" OnClick="btnOK_Click" Text="确定" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
