<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Account_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
     <p>
         <asp:Label ID="username" runat="server" Text=""></asp:Label></p>
     <p>商家名称：<asp:Label ID="businessName" runat="server" Text=""></asp:Label></p>
     <p>经度：<asp:Label ID="Longitude" runat="server" Text=""></asp:Label></p>
     <p>纬度：<asp:Label ID="Latitude" runat="server" Text=""></asp:Label></p>
     <p>公司介绍：<asp:Label ID="Description" runat="server" Text=""></asp:Label></p>
     <p>公司地址：<asp:Label ID="Address" runat="server" Text=""></asp:Label></p>  
     <p><a href="Edit.aspx">修改</a></p>    
    </div>
</asp:Content>


