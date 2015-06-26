<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="business_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px">
    </asp:DetailsView>
    <asp:Button runat="server" ID="btnApprove"  OnClick="btnApprove_Click"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

