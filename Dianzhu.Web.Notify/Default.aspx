<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightMain" Runat="Server">
     <a href="IMServerAPI.ashx?type=systemnotice&body=body&group=customer">systemnotice</a>
         <a href="IMServerAPI.ashx?type=ordernotice&orderid=orderId&ordertitle=ordertitle&orderstatus=orderstatus&ordertype=ordertype&orderstatusfriendly=orderstatusfriendly&userid=userid&toresource=toresource">ordernotice</a>
   
</asp:Content>

