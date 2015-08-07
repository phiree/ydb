<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Business_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" href="/js/lightbox/css/lightbox.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="wrapper">
    <ul>
        <li>店铺名称:<%=CurrentBusiness.Name %> </li>
        <li>店铺介绍:<%=CurrentBusiness.Description %> </li>
        <li>联系方式:<%=CurrentBusiness.Phone %> </li>
        <li>商家地址: <%=CurrentBusiness.AreaBelongTo.Name %>   <%=CurrentBusiness.Address %></li>
        <li>商家网址:<%=CurrentBusiness.WebSite %></li>
        <li>商家邮箱:<%=CurrentBusiness.Email %></li>
        <li>店铺图片:<img src='<%=CurrentBusiness.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(CurrentBusiness.BusinessAvatar.ImageName)+"&width=90&height=90&tt=2":"../image/myshop/touxiangkuang_11.png"%>' /></li>
        
     
        <li>从业时间:<%=CurrentBusiness.WorkingYears %></li>
        <li>员工人数:<%=CurrentBusiness.StaffAmount %></li>
        <li>负责人姓名:<%=CurrentBusiness.Contact %></li>
        <li>证件类型:<%=CurrentBusiness.ChargePersonIdCardType.ToString() %></li>
        <li>证件号码:<%=CurrentBusiness.ChargePersonIdCardNo %></li>
        <li>营业执照:    <asp:Repeater runat="server" ID="rptImageLicense">
         <ItemTemplate>
         <a   data-lightbox="lb_license" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=90&height=90&tt=2' />
        </a>
           </ItemTemplate>
         </asp:Repeater></li>
        <li></li>
        <li><a href="/Business/edit.aspx?businessId=<%=CurrentBusiness.Id %>">信息编辑</a></li>
        <li></li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">

 <script  src="/js/lightbox/js/lightbox.js"></script>
</asp:Content>

