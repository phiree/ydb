<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="DZService_Edit" %>

<%@ Import Namespace="Dianzhu.Model" %>
 
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
              <%=ServiceType.ToString()%>
            </td>
        </tr>
         <asp:Repeater runat="server" ID="rptProperties">
         <ItemTemplate>
        <tr>
            <td>
             <%#Eval("Name") %>   
            </td>
            <td>
                <asp:RadioButtonList   runat="server" id="rblValues"/>
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
      
        <tr>
            <td colspan="2">
            <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="保存" />
            </td>
            
        </tr>
    </table>
</asp:Content>
