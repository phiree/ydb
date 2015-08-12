<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="CreateSuc.aspx.cs" Inherits="Business_CreateSuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" href="/css/business.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="cont-wrap">
    <div class="cont-container min-height700">
        <div class="cont-row">
            <div class="cont-col-12">
                <div class="cont-col-12 t-c m-b20">
                    <img src="../image/banner.png" alt="恭喜你">
</div>
                <div class="cont-col-12">

                    <p class="create-suc t-c">恭喜你，成功创建新的商铺！</p>

</div>
</div>
</div>
        <div class="cont-row">
            <div class="cont-col-12">
                <p class=" t-c"><a class="create-continue" href="/Business/edit.aspx?businessId=<%=CurrentBusiness.Id%>">继续完善你的店铺资料。>></a></p></div>
</div>
</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

