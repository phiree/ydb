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
                    <h2>店铺基本信息</h2></div>
                <div class="cont-col-3"><a class="btn btn-info" href="/Business/edit.aspx?businessId=<%=CurrentBusiness.Id %>">信息编辑</a></div>
            </div>
        </div>
        <div class="cont-container business-detail-main">
            <div class="cont-row m-b20">
                <div class="cont-col-7">
                    <div class="business-detail-stand">
                        <div class="cont-row">
                            <div class="cont-col-12">
                                <div class="t-c">
                                   <img src='<%=CurrentBusiness.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(CurrentBusiness.BusinessAvatar.ImageName)+"&width=90&height=90&tt=2":"../image/myshop/touxiangkuang_11.png"%>' />
</div>
                            </div>
                        </div>
                        <div class="cont-row">
                            <div class="cont-col-12">
                                <p class="t-c"><%=CurrentBusiness.Name %></p>

</div>

</div>
                        <div class="detail-pra">
                            <div class="cont-row">
                                                        <div class="cont-col-3">商家网址:</div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.WebSite??"无" %></div>
                            </div>
                            <div class="cont-row">
                                                        <div class="cont-col-3">店铺邮箱:</div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.Email %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3">联系方式:</div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.Phone %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3">商家地址:</div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.AreaBelongTo==null?"无":CurrentBusiness.AreaBelongTo.Name %><%=CurrentBusiness.Address %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3">从业时间:</div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.WorkingYears %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3">员工人数:</div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.StaffAmount %></div>
                            </div>
                                                    <div class="cont-row">
                                                        <div class="cont-col-3">店铺介绍:</div>
                                                        <div class="cont-col-9"><%=CurrentBusiness.Description %></div>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="cont-col-5">
                    <div class="business-detail-iden">
                        <p class="business-detail-h2">负责人信息</p>
                        <div class="detail-pra">
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
                        <p class="business-detail-h2">营业执照</p>
                        <div >
                            <asp:Repeater runat="server" ID="rptImageLicense">
                              <ItemTemplate>
                              <a   data-lightbox="lb_license" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=90&height=90&tt=2' />
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
                       <p class="business-detail-h2">店铺图片</p>
                       <div class="detail-pra"></div>
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

