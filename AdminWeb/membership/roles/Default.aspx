<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="membership_roles_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gvRoleList" runat="server">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" DataTextField="Name" />
            <asp:BoundField />
        </Columns>
       
    </asp:GridView>
    <a href="Edit.aspx">增加角色</a>
</asp:Content>

