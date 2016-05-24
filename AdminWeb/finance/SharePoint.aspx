<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="SharePoint.aspx.cs" Inherits="finance_SharePoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <table>
        <tr><td>用户类型</td><td>分成比</td></tr>
    <asp:Repeater runat="server" ID="rptPoints">
        <ItemTemplate>
            <tr>
                <td><%#Eval("UserType") %></td><td><%#Eval("Point") %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
        </table>
    <fieldset>
      
        <legend>用户类型默认分成比设置</legend>
        <div>用户类型:<asp:DropDownList runat="server" ID="ddlUserType">
           
              <asp:ListItem Value="4">客服</asp:ListItem>
              
              <asp:ListItem Value="32">代理</asp:ListItem>
                  </asp:DropDownList></div>
        <div>默认分成比:<asp:TextBox runat="server" ID="tbxSharePoint"></asp:TextBox></div>
        <div><asp:Button runat="server" ID="btnSaveSharePoint" OnClick="btnSaveSharePoint_Click" Text="保存" />
            <asp:Label runat="server" ID="lblMsg"></asp:Label>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

