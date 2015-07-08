<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label runat="server" ID="lblResult"></asp:Label>
<asp:GridView runat="server" ID="gvServiceList" 
       DataKeyNames="Id"  onrowcommand="gvServiceList_RowCommand" >
<Columns>
<asp:ButtonField CommandName="btnOrder" Text="预订"/>
</Columns>
</asp:GridView>

</asp:Content>

