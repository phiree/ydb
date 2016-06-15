﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
CodeFile="Edit.aspx.cs" Inherits="Business_Edit"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <div class="content-head full-head">
            <h3 class="cont-h2">
                店铺资料填写
            </h3>
        </div>
        <div class="content-main">
            <div class="container-fluid animated fadeInUpSmall">
                <div class="steps-wrap steps-2 business-steps">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="steps-show">
                                <div class="steps-tips steps-tips-2 clearfix">
                                    <div class="steps-tip">
                                        <div class="steps-tip-bg step-tip-1">
                                            <div class="step-tip-h">第一步</div>
                                            <p>填写基本信息</p>
                                        </div>
                                    </div>
                                    <div class="steps-tip">
                                        <div class="steps-tip-bg">
                                            <div class="step-tip-h">第二步</div>
                                            <p>填写营业信息</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="d-hr"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="steps-list">
                                <div class="steps-step cur-step animated fadeInUpSmall">
                                    <div class="model">
                                        <div class="model-h">
                                            <h4>店铺基本信息</h4>
                                        </div>
                                        <div class="model-m">
                                            <div class="model-form">
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">店铺名称</div>
                                                            <div class="col-md-8 model-input">
                                                                <input runat="server" type="text" id="tbxName" name="inputShopName"
                                                                       value="请输入您的店铺名称" class="input-fluid" data-toggle="tooltip"
                                                                       data-placement="top" title="请填写您的店铺名称"/>
                                                            </div>
                                                        </div>
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">联系电话</div>
                                                            <div class="col-md-8 model-input">
                                                                <input type="text" class="input-fluid" id="tbxContactPhone" runat="server" name="ContactPhone"
                                                                       data-toggle="tooltip" data-placement="top" title="请填写有效的电话号码"/>
                                                            </div>
                                                        </div>
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">从业时间</div>
                                                            <div class="col-md-8 model-input col-md-8 model-input-unit">
                                                                <div id="yearsSelect" class="select select-fluid select-static" data-type="num" data-toggle="tooltip"
                                                                     data-placement="top" title="选择你从事该行业的时间">
                                                                    <input type="text" class="input-fluid dis-n" runat="server" focusID="yearsSelect"
                                                                           id="tbxBusinessYears" name="workYears"/>
                                                                </div>
                                                                <em class="unit">年</em>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">公司网站</div>
                                                            <div class="col-md-8 model-input">
                                                                <input type="text" class="input-fluid" runat="server" id="tbxWebSite" name="website"
                                                                       data-toggle="tooltip" data-placement="top" title="请填写公司网站"/>
                                                            </div>
                                                        </div>
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">店铺地址</div>
                                                            <div class="col-md-8 model-input">
                                                                <p>
                                                                    <input type="text" class="input-fluid" id="tbxAddress" runat="server" name="addressDetail"
                                                                           data-toggle="tooltip" data-placement="top" title="请填写您详细有效的店铺地址"/>
                                                                </p>
                                                                <p><input type="hidden" focusID="setAddress" runat="server" clientidmode="Static" id="hiAddrId"
                                                                          name="addressDetailHide"/></p>
                                                            </div>
                                                        </div>
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">员工人数</div>
                                                            <div class="col-md-8 model-input col-md-8 model-input-unit">
                                                                <input type="text" class="input-fluid" runat="server" value="0" clientidmode="Static"
                                                                       id="selStaffAmount" data-toggle="tooltip" data-placement="top" title="请填写店铺员工人数"/>
                                                                <em class="unit">人</em>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-10">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">店铺介绍</div>
                                                            <div class="col-md-10 model-input">
                                                        <textarea class="input-textarea-fluid" id="tbxIntroduced" runat="server" name="shopIntroduced"
                                                                  data-toggle="tooltip" data-placement="top" title="请将您的店铺简介在此输入（字数限制在0~200个字符之间）">(可输入200个字符)</textarea>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">店铺头像</div>
                                                            <div class="col-md-8 model-input">
                                                                <div id="businessavater">
                                                                    <input type="file" class="input-file-btn" data-list="#businessavater" data-single="true" data-limit="1" data-size="2" data-params='{"businessId":"<%=b.Id %>", "imageType":"businessavater"}' data-preview="<%=b.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(b.BusinessAvatar.ImageName)+"&width=140&height=140&tt=3":"../images/components/inputFile/input_bg_140_140.png" %>"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">店铺图片</div>
                                                            <div class="col-md-8 model-input">
                                                                <div id="buiniessShow" class="clearfix">
                                                                    <asp:Repeater runat="server" ID="rpt_show" OnItemCommand="rpt_show_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <div class="download-img-pre" data-count="input-file">
                                                                                <asp:Button Text=" " formnovalidate CssClass="cancel download-img-delete"
                                                                                            runat="server" CommandName="delete"
                                                                                            ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static"
                                                                                            CommandArgument='<%#Eval("Id") %>'/>
                                                                                <a class="download-img-show"
                                                                                   href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                                                    <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=140&height=140&tt=3'
                                                                                         id="imgLicence"/>
                                                                                </a>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <input type="file" class="input-file-btn" data-list="#buiniessShow" data-limit="6" data-size="2" data-params='{"businessId":"<%=b.Id %>", "imageType":"businessshow"}' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="steps-step animated fadeInUpSmall">
                                    <div class="model">
                                        <div class="model-h">
                                            <h4>商户营业资质资料</h4>
                                        </div>
                                        <div class="model-m">
                                            <div class="model-form">
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">负责人姓名</div>
                                                            <div class="col-md-8 model-input">
                                                                <input type="text" class="input-fluid" runat="server" clientidmode="Static" id="tbxContact"
                                                                       data-toggle="tooltip" data-placement="top" title="请填写店铺负责人姓名"/>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">证件类型</div>
                                                            <div class="col-md-8 model-input">
                                                                <div class="select select-fluid" data-toggle="tooltip" data-placement="top" title="请选择店铺负责人证件类型">
                                                                    <ul>
                                                                        <li><a>身份证</a></li>
                                                                        <li><a>其它</a></li>
                                                                    </ul>
                                                                    <input type="hidden" id="selCardType" value="0" runat="server" clientidmode="Static"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">证件号码</div>
                                                            <div class="col-md-8 model-input">
                                                                <input type="text" class="input-fluid" runat="server" id="tbxCardIdNo" name="tbxCardIdNo"
                                                                       clientidmode="Static" data-toggle="tooltip" data-placement="top"
                                                                       title="请填写店铺负责人证件号码"/>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">负责人证件</div>
                                                            <div class="col-md-8 model-input">
                                                                <div id="chargePerson" class="clearfix">
                                                                    <asp:Repeater runat="server" ID="rptChargePersonIdCards"
                                                                                  OnItemCommand="rpt_show_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <div class="download-img-pre" data-count="input-file">

                                                                                <asp:Button Text=" " ID="ibCharge" CssClass="cancel download-img-delete"
                                                                                            runat="server" CommandName="delete"
                                                                                            ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static"
                                                                                            CommandArgument='<%#Eval("Id") %>'/>
                                                                                <a class="download-img-show"
                                                                                   href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                                                    <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=140&height=140&tt=3'
                                                                                         class="imgCharge"/>
                                                                                </a>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                        <input type="file" class="input-file-btn" data-list="#chargePerson" data-limit="2" data-size="2" data-params='{"businessId":"<%=b.Id %>", "imageType":"businesschargeperson"}' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="row model-form-group">
                                                            <div class="col-md-4 model-label">营业执照</div>
                                                            <div class="col-md-8 model-input">
                                                                <div id="businessLicense" class="clearfix">
                                                                    <asp:Repeater runat="server" ID="rptLicenseImages" OnItemCommand="rpt_show_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <div class="download-img-pre" data-count="input-file">
                                                                                <asp:Button ID="img" Text=" " formnovalidate
                                                                                            CssClass="cancel download-img-delete" runat="server"
                                                                                            CommandName="delete"

                                                                                            ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>'/>
                                                                                <a class="download-img-show"
                                                                                   href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                                                    <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=140&height=140&tt=3'
                                                                                         class="imgCharge"/>
                                                                                </a>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <input type="file" class="input-file-btn" data-list="#businessLicense" data-limit="2" data-size="2" data-params='{"businessId":"<%=b.Id %>", "imageType":"businesslicense"}' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <p class="cont-input-tip"><i class="icon icon-tip"></i>上传负责人的证件照，限制数量为两张，并且图片大小为2M以下</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="step-ctrl">
                                <div class="model-global-bottom">
                                    <a class="step-prev btn btn-info btn-big m-r10" value="prev"  >上一步</a>
                                    <a class="step-next btn btn-info btn-big m-r10" value="next"  >下一步</a>
                                    <input class="step-save btn btn-info btn-big m-r10" name="imageField" runat="server" onserverclick="btnSave_Click" type="submit" id="imageField1" value="保存"/>
                                    <a class="step-cancel btn btn-cancel btn-big" id="btnCancel" href="/business/detail.aspx?businessId=<%=Request["businessid"]%>">取消</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="addrlightBox" class="dis-n">
                <div class="mapWrap">
                    <div id="addressMap" class="mapMain">
                    </div>
                    <div id="addressCity" class="mapCity">
                    </div>
                    <div id="addressText" class="mapAddrsText myshop-addPrint-map">

                    </div>
                    <div class="mapButton">
                        <input id="confBusiness" class="lightClose myshop-sm-input" type="button" value="确定"><span
                            class="myshop-locTip">点击地图放置地点哦！</span></div>
                    <input id="businessValue" type="hidden" value=""/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js"></script>
    <script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/additional-methods.js" ></script>
    <script src="/js/plugins/jquery.form.min.js"></script>
    <script src="/js/plugins/jquery.lightbox_me.js"></script>
    <script src="/js/widgets/stepByStep.js?v=1.0.0"></script>
    <script src="/js/components/imageUpload.js?v=1.0.0"></script>
    <script src="/js/apps/validation/validation_shop_edit.js?v=1.0.0"></script>
    <script src="/js/components/select.js?v=1.0.0"></script>
    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip(
                {
                    delay: {show : 500, hide : 100},
                    trigger: 'hover'
                }
            );

            $('.input-file-btn').imageUpload();


            $('#headImgTrigger').click(function(){
                return $('#headImgBtn').click();
            });

            $(".select").customSelect();

            $(".steps-wrap").stepByStep({
                stepValid : function(){
                    return $('.steps-wrap').find('.cur-step').find('input,textarea,select').valid();
                }
            });
        });
    </script>
</asp:Content>
