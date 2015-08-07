<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Business_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" href="/js/lightbox/css/lightbox.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="wrapper">
    <div class="business-detail-container">
        <div class="cont-container business-detail-head">
            <div class="cont-row">
                <div class="cont-col-9"></div>
                <div class="cont-col-3"></div>
            </div>
        </div>
        <div class="cont-container business-detail-main">
            <div class="cont-row">
                <div class="cont-col-7">
                    <div class="">
                        <div class="cont-row">
                            <div class="cont-col-12"></div>

</div>
                        <div class="cont-row">
                            <div class="cont-col-12">
                            <%=CurrentBusiness.Name %>
</div>

</div>
                        <div class="cont-row">
                            <div class="cont-col-4">商家网址:</div>
                            <div class="cont-col-8"><%=CurrentBusiness.WebSite %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-4">联系方式:</div>
                            <div class="cont-col-8"><%=CurrentBusiness.Phone %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-4">商家地址:</div>
                            <div class="cont-col-8"><%=CurrentBusiness.Address %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-4">从业时间:</div>
                            <div class="cont-col-8"><%=CurrentBusiness.WorkingYears %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-4">员工人数:</div>
                            <div class="cont-col-8"><%=CurrentBusiness.StaffAmount %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-4">店铺介绍:</div>
                            <div class="cont-col-8"><%=CurrentBusiness.Description %></div>
</div>

                    </div>
                </div>
                <div class="cont-col-5">
                    <div class="">
                        <div class="cont-row">
                            <div class="cont-col-12">
                                负责人姓名
</div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-5">负责人姓名:</div>
                            <div class="cont-col-7"><%=CurrentBusiness.Contact %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-5">证件类型:</div>
                            <div class="cont-col-7"><%=CurrentBusiness.ChargePersonIdCardType.ToString() %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-5">证件号码:</div>
                            <div class="cont-col-7"><%=CurrentBusiness.ChargePersonIdCardNo %></div>
</div>
                        <div class="cont-row">
                            <div class="cont-col-5"></div>
                            <div class="cont-col-7"></div>
</div>
                    </div>
</div>
            </div>
            <div class="cont-row">
                <div class="cont-col-7">
                    <div class="cont-row">
                        <div class="cont-col-12">店铺图片</div>
</div>
                    <div class="cont-row">
                        <div class="cont-col-12"><img src='<%=CurrentBusiness.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(CurrentBusiness.BusinessAvatar.ImageName)+"&width=90&height=90&tt=2":"../image/myshop/touxiangkuang_11.png"%>' /></div>
</div>
</div>
                <div class="cont-col-5">
                    <div class="cont-row">
                        <div class="cont-col-12">营业执照:</div>
</div>
                    <div class="cont-row">
                        <div class="cont-col-12"><%=CurrentBusiness.BusinessLicenses%></div>
</div>
</div>
            </div>
        </div>
    </div>
    <ul>

        <li>店铺名称:<%=CurrentBusiness.Name %> </li>
        <li>店铺介绍:<%=CurrentBusiness.Description %> </li>
        <li>联系方式:<%=CurrentBusiness.Phone %> </li>
        <li>商家地址: <%=CurrentBusiness.AreaBelongTo.Name %>   <%=CurrentBusiness.Address %></li>
        <li>商家网址:<%=CurrentBusiness.WebSite??"无" %></li>
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
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">

 <script  src="/js/lightbox/js/lightbox.js"></script>
</asp:Content>

