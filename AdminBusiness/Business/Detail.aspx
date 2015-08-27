<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Business_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" href="/js/lightbox/css/lightbox.css" />
<link rel="Stylesheet" href="/css/business.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="cont-wrap">
    <div class="business-detail-container">
        <div class="cont-container business-detail-head">
            <div class="cont-row">
                <div class="cont-col-9">
                    <h2 class="cont-h2">店铺基本信息</h2></div>
                <div class="cont-col-3"><p class="t-r"><a class="btn btn-info" href="/Business/edit.aspx?businessId=<%=CurrentBusiness.Id %>">修改店铺信息</a></p></div>
            </div>
        </div>
        <div class="cont-container business-detail-main">
            <div class="cont-row m-b20">
                <div class="cont-col-7">
                    <div class="business-detail-stand">
                        <div class="cont-row">
                            <div class="cont-col-12">
                                <div class="t-c">
                                   <img class="business-detail-face" src='<%=CurrentBusiness.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(CurrentBusiness.BusinessAvatar.ImageName)+"&width=125&height=125&tt=3":"../image/myshop/touxiang_125_125.png"%>' />
</div>
                            </div>
                        </div>
                        <div class="cont-row">
                            <div class="cont-col-12">
                                <p class="t-c business-detail-name"><%=CurrentBusiness.Name %></p>

</div>

</div>
                        <div class="detail-pra">
                            <div class="cont-row">
                                                        <div class="cont-col-3"><span class="cont-h5">商家网址</span></div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.WebSite??"无" %></div>
                            </div>
                            <div class="cont-row">
                                                        <div class="cont-col-3"><span class="cont-h5">店铺邮箱</span></div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.Email %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3"><span class="cont-h5">联系方式</span></div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.Phone %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3"><span class="cont-h5">商家地址</span></div>
                                                        <div class="cont-col-9"><%=string.IsNullOrEmpty( CurrentBusiness.Address )? "无" : CurrentBusiness.Address %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3"><span class="cont-h5">从业时间</span></div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.WorkingYears %>&nbsp;年</div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3"><span class="cont-h5">员工人数</span></div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.StaffAmount %>&nbsp;人</div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3"><span class="cont-h5">店铺介绍</span></div>
                                                        <div class="cont-col-9"><p class="text-breakWord"><%=CurrentBusiness.Description %></p></div>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="cont-col-5">
                    <div class="business-detail-iden">
                        <p class="cont-h4">负责人信息</p>
                        <div class="detail-pra">
                              <div class="cont-row">
                                  <div class="cont-col-5"><span class="cont-h5">负责人姓名</span></div>
                                  <div class="cont-col-7"><%=CurrentBusiness.Contact %></div>
                              </div>
                              <div class="cont-row">
                                  <div class="cont-col-5"><span class="cont-h5">证件类型</span></div>
                                  <div class="cont-col-7"><%=CurrentBusiness.ChargePersonIdCardType.ToString() %></div>
                              </div>
                              <div class="cont-row">
                                  <div class="cont-col-5"><span class="cont-h5">证件号码</span></div>
                                  <div class="cont-col-7"><%=CurrentBusiness.ChargePersonIdCardNo %></div>
                              </div>
                        </div>
                        <p class="cont-h4">营业执照</p>
                        <div class="p-20 detail-img">
                            <asp:Repeater runat="server" ID="rptImageLicense">
                              <ItemTemplate>
                              <a class="m-r20" data-lightbox="lb_license" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3' />
                             </a>
                                </ItemTemplate>
                              </asp:Repeater>
                        </div>
                        <p class="cont-h4">负责人证件照</p>
                            <div class="p-20 detail-img">
                            <asp:Repeater runat="server" ID="rptCharge">
                              <ItemTemplate>
                              <a class="m-r20" data-lightbox="lb_charge" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3' />
                             </a>
                                </ItemTemplate>
                              </asp:Repeater>
                            </div>
                    </div>
</div>
            </div>
            <div class="cont-row">
                <div class="cont-col-12">
                    <div class="business-detail-pic">
                       <p class="cont-h4 m-b20">店铺图片</p>
                       <div class="detail-img">
                        <asp:Repeater runat="server" ID="rptShow">
                              <ItemTemplate>
                              <a class="m-r20"  data-lightbox="lb_show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3' />
                             </a>
                                </ItemTemplate>
                              </asp:Repeater>
                       </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
 <script  src="/js/lightbox/js/lightbox.js"></script>
</asp:Content>

