<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
     <p>
         <asp:Label ID="username" runat="server" Text=""></asp:Label></p>
     <p>商家名称：<asp:TextBox ID="businessName" runat="server"></asp:TextBox></p>
     <p>经度：<asp:TextBox ID="Longitude" runat="server"></asp:TextBox></p>
     <p>纬度：<asp:TextBox ID="Latitude" runat="server"></asp:TextBox></p>
     <p>公司介绍：<asp:TextBox ID="Description" runat="server"></asp:TextBox></p>
     <p>公司地址：<asp:TextBox ID="Address" runat="server"></asp:TextBox></p>  
     <p>
         <asp:Button ID="Button1" runat="server" Text="确认提交" OnClick="dataSub" /></p>    
    </div>
</asp:Content>