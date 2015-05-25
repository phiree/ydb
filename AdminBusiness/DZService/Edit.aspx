<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="DZService_Edit" %>
    <%@ import  Namespace="Dianzhu.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                服务名称
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                详细描述
            </td>
            <td>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="tbxDescription"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                服务分类
            </td>
            <td>
               <asp:Label runat="server" ID="lblServiceCate"></asp:Label>
            </td>
        </tr>
         <% foreach (ServiceProperty tp in TypeProperties)
            { %>
        <tr>
            <td><%=tp.Name %>
            </td>
            <td>
            <asp:DropDownList  runat="server"></asp:DropDownList>
            </td>
        </tr>
        <%} %>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
