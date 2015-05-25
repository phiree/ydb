<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="service_property.aspx.cs" Inherits="servicetype_service_property" %>
<%@ import  Namespace="Dianzhu.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div><%=CurrentServiceType.Name %></div>
<table>
<tr><td>属性名称</td><td><asp:TextBox runat="server" ID="tbxPropertyName"></asp:TextBox></td></tr>
<tr><td>属性值</td><td><asp:TextBox runat="server" ID="tbxPropertyValues"></asp:TextBox></td></tr>
<tr><td colspan="2"><asp:Button runat="server" ID="btnSave"   Text="保存" OnClick="btnSave_Click"/>
<asp:Button runat="server" ID="btnDelete"    Text="删除该属性" OnClick="btnDelete_Click"/></td></tr>
</table>
     
    <asp:GridView runat="server" ID="gvProperties">
    <Columns>
    <asp:HyperLinkField   DataTextField="Name" DataNavigateUrlFields="id" DataNavigateUrlFormatString="service_property.aspx?propertyId={0}" />
    <asp:TemplateField>
    <ItemTemplate>
    <%#((ServiceProperty)GetDataItem()).GetValuesStringFormat()%>
    </ItemTemplate>
    </asp:TemplateField>
    
    </Columns>
    </asp:GridView>
</asp:Content>
