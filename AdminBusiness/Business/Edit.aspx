<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="Business_Edit"  %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/myshop.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css'
        rel="stylesheet" type="text/css" />
    <link href="/css/validation.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
            <div class="cont-wrap theme-color-58789a">
                <div class="cont-container">
                
                    <div class="cont-row step-row">
                        <div class="cont-col-2">
                            <h3 class="step-head step-1">
                                <img src="../image/shop-icon-step1.png" /></h3>
                        </div>
                        <div class="cont-col-10"><p class="step-text step-1">基本信息填写</p></div>
                    </div>

                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <div class="clearfix">
                                <div class="headImage fr m-r20">
                                    <div class="input-file-box headFile">
                                      <input type=file class="input-file-btn file-default"  name="upload_file" businessId="<%=b.Id %>" imageType="businessavater" />
                                        <i class="input-file-bg"  style='background-image:url(<%=b.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(b.BusinessAvatar.ImageName)+"&width=90&height=90&tt=3)":"../image/myshop/touxiang_90_90.png" %>' ></i>
                                        <i  class="input-file-mark"></i>
                                        <img style="top:auto;left:auto;position:inherit;" class="input-file-pre" src="..\image\00.png" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="cont-col-5">
                            <div class="myshop-name">
                                <p class="vm"><input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称" class="myshop-name-input input-mid" /><span class="text-anno-r">（必填选项）</span></p>
                                <p class="cont-input-tip"><i class="icon icon-tip"></i>请上传您的店铺商标，并填写您的店铺名称</p>
                            </div>
                        </div>

                    </div>
                    <div class="cont-row myshop-cont-row">

                        <div class="cont-col-2">
                            <p class="cont-sub-title"><span>店铺介绍</span></p>
                        </div>
                        <div class="cont-col-10">
                            <div class="cont-row">
                                <div class="cont-col-6">
                                    <textarea class="input-textarea" id="tbxIntroduced" runat="server" name="shopIntroduced">(可输入200个字符)</textarea>
                                </div>
                                <div class="cont-col-6">
                                    <div class="text-anno myshop-intro-anno vmBox">
                                        <div class="vm">
                                            <p><span class="cont-title-tips">（必填选项）</span></p>
                                            <p class="cont-input-tip"><i class="icon icon-tip"></i>请将您的店铺简介在此输入（字数限制在0~200个字符之间）</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <p class="cont-sub-title"><span>联系电话</span></p>

                        </div>
                        <div class="cont-col-10">
                            <div><input type="text" class="input-lg" id="tbxContactPhone" runat="server" name="ContactPhone"/><span class="text-anno-r">（必填选项）</span></div>
                            <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写有效的电话号码</p>
                        </div>
                    </div>
                        <div class="cont-row myshop-cont-row">
                            <div class="cont-col-2">
                                <p class="cont-sub-title">店铺地址</p>
                            </div>
                            <div class="cont-col-10">

                                <div>
                                    <p><input type="text" class="input-lg" id="tbxAddress" runat="server" name="addressDetail" /><span class="text-anno-r">（必填选项）</span></p>
                                    <p class="cont-input-tip"><i class="icon icon-tip"></i>请填写您详细有效的店铺地址</p>
                                    <!--<p class="cont-input-tip  m-b10"><i class="icon icon-tip"></i>请点击按钮放置店铺坐标</p>-->
                                    <!--<div class="cont-row">-->
                                        <!--<div class="cont-col-3">-->
                                            <!--&lt;!&ndash;<p><input id="setAddress" class="myshop-btn-setAddress m-b10" type="button" name="setAddress" value="点击放置店铺坐标" /></p>&ndash;&gt;-->
                                        <!--</div>-->
                                        <!--<div class="cont-col-9">-->
                                            <!--<div class="addPrint-vm">-->
                                                <!--<div id="addPrintBox"></div>-->
                                            <!--</div>-->
                                        <!--</div>-->
                                    <!--</div>-->
                                    <p><input type="hidden" focusID="setAddress" runat="server" clientidmode="Static" id="hiAddrId" name="addressDetailHide" /></p>
                                </div>

                            </div>
                        </div>
                        <div class="cont-row myshop-cont-row">
                            <div class="cont-col-2">
                                <p class="cont-sub-title">从业时间</p>
    </div>
                            <div class="cont-col-10">
                                <div>
                                    <div id="yearsSelect" class="d-inb select select-sm years-select">
                                        <ul></ul>
                                        <input type="text" class="input-lg dis-n" runat="server" focusID="yearsSelect" id="tbxBusinessYears" name="workYears"/>
                                    </div>
                                    <span class="myshop-span">年</span><span class="text-anno-r">（必填选项）</span>
                                </div>
                                <p class="cont-input-tip"><i class="icon icon-tip"></i>选择你从事该行业的时间</p>

                            </div>
                        </div>
                        <div class="cont-row myshop-cont-row">
                            <div class="cont-col-2">
                                <p class="cont-sub-title">员工人数</p>
    </div>
                            <div class="cont-col-10">
                                <div>
                                    <input type="text" class="input-mid" runat="server" value="0" clientidmode="Static" id="selStaffAmount" />
                                    <span class="myshop-span">人</span><span class="text-anno-r">（必填选项）</span>
                                </div>
                                <p class="cont-input-tip"><i class="icon icon-tip"></i>店铺的员工数量</p>
    </div>

                        </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <p class="cont-sub-title"><span>公司网站</span></p>
                        </div>
                        <div class="cont-col-10">
                            <div><input type="text" class="input-lg" runat="server" id="tbxWebSite" name="website" /></div>
                            <p class="cont-input-tip"><i class="icon icon-tip"></i>填写公司网站</p>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2"><p class="cont-sub-title">店铺图片展示</p></div>
                        <div class="cont-col-10">
                            <div class="cont-row">
                                <div class="cont-col-12">
                                    <div class="img-list img-list-limit6 clearfix">
                                        <asp:Repeater runat="server" ID="rpt_show" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre m-b10 m-r10 fl">
                                                    <asp:Button Text=" " formnovalidate CssClass="cancel download-img-delete" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            id="imgLicence" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box fl m-b10 m-r10 dis-n">
                                            <input type=file class="input-file-btn file-limit-6"  name="input-file-btn-show"  businessId="<%=b.Id %>" imageType="businessshow" />
                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                    <p class="cont-input-tip"><i class="icon icon-tip"></i>上传您的店铺图片，限制数量为六张，并且图片大小为2M以下</p>
                                </div>
                            </div>

                        </div>
                    </div>




                    <div class="cont-row step-row">
                        <div class="cont-col-2"><h3 class="step-head step-2">
                            <img src="../image/shop-icon-step2.png" /></h3></div>
                        <div class="cont-col-10"><p class="step-text step-2">完善商户营业资质资料</p></div>
                    </div>

                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <p class="cont-sub-title">负责人姓名</p>
</div>
                        <div class="cont-col-10">

                            <div>
                                <input type="text" class="input-mid" runat="server" clientidmode="Static" id="tbxContact" />
                            </div>
                            <p class="cont-input-tip"><i class="icon icon-tip"></i>店铺负责人姓名</p>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <p class="cont-sub-title">证件类型</p>
                        </div>
                        <div class="cont-col-10">
                            <div>
                                <div class="select select-sm">
                                    <ul>
                                        <li><a>身份证</a></li>
                                        <li><a>其它</a></li>
                                    </ul>
                                    <input type="hidden" id="selCardType" value="0" runat="server" clientidmode="Static" />
                                </div>
                            </div>
                            <p class="cont-input-tip"><i class="icon icon-tip"></i>店铺负责人证件类型</p>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <p class="cont-sub-title">证件号码</p>
</div>
                        <div class="cont-col-10">
                            <div>
                                <input type="text" class="input-mid" runat="server" id="tbxCardIdNo" name="tbxCardIdNo" clientidmode="Static" />
                            </div>
                            <p class="cont-input-tip"><i class="icon icon-tip"></i>店铺负责人证件号码</p>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <p class="cont-sub-title">负责人证件照</p>
                        </div>
                        <div class="cont-col-10">
                            <div class="cont-row">
                                <div class="cont-col-12">
                                    <div class="img-list img-list-limit2 clearfix">
                                        <asp:Repeater runat="server" ID="rptChargePersonIdCards" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre m-b10 m-r10 fl">
 
                                                    <asp:Button Text=" " ID="ibCharge"   CssClass="cancel download-img-delete" runat="server" CommandName="delete"
   ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            class="imgCharge" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box m-b10 m-r10 fl dis-n">
                                            <input type=file class="input-file-btn file-limit-2" businessId="<%=b.Id %>" imageType="businesschargeperson" />
                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>

                                    <p class="cont-input-tip"><i class="icon icon-tip"></i>上传负责人的证件照，限制数量为两张，并且图片大小为2M以下</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col-2">
                            <p class="cont-sub-title">营业执照</p>
                        </div>
                        <div class="cont-col-10">
                            <div class="cont-row">
                                <div class="cont-col-12">
                                    <div class="img-list img-list-limit2 clearfix">
                                        <asp:Repeater runat="server" ID="rptLicenseImages" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre m-r10 m-b10 fl">
 
                                                 <asp:Button ID="img"   Text=" "   formnovalidate CssClass="cancel download-img-delete" runat="server" CommandName="delete"
 
                                                          ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                      <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            class="imgCharge" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box m-r10 m-b10 fl dis-n">
                                            <input type=file class="input-file-btn file-limit-2"   name="input-file-btn-license" businessId="<%=b.Id %>" imageType="businesslicense" />
                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                    <p class="cont-input-tip"><i class="icon icon-tip"></i>上传您的营业执照图片，限制数量为两张，并且图片大小为2M以下</p>
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
                            <input id="confBusiness" class="lightClose myshop-sm-input" type="button" value="确定"><span class="myshop-locTip">点击地图放置地点哦！</span></div>
                        <input id="businessValue" type="hidden" value="" />
                    </div>
                </div>
                <div class="bottomArea">
                    <input class="btn btn-info btn-big" name="imageField" runat="server" onserverclick="btnSave_Click" type="submit" id="imageField1" value="保存"/>
                    <a class="btn btn-cancel btn-big m-l10" href="/business/detail.aspx?businessId=<%=Request["businessid"] %>">取消</a>
                </div>
            </div>
        <!--</div>-->
    <!--</div>-->
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/additional-methods.js" type="text/javascript"></script>
    <script src="/js/jquery.form.min.js" type="text/javascript"></script>
    <script src="/js/navigator.sayswho.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script src="/js/FileUpload.js" type="text/javascript"></script>
    <script type="text/javascript">
         var name_prefix = 'ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$';
    </script>
    <script src="/js/validation_shop_edit.js" type="text/javascript"></script>
    <script src="/js/validation_invalidHandler.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $($("form")[0]).validate(
                {
                    errorElement: "p",
                    errorPlacement: function (error, element) {
                        if ( $(element).attr("name") == name_prefix + "tbxBusinessYears" ) {
                            error.appendTo((element.parent()).parent());
                        } else {
                            error.appendTo(element.parent());
                        }

                    },
                    rules: service_validate_rules,
                    messages: service_validate_messages,
                    invalidHandler: invalidHandler
                }
            );

        });
    </script>
    <script>
        function loadBaiduMapScript() {
          var script = document.createElement("script");
          script.src = "http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW&callback=initialize";
          document.body.appendChild(script);
        }

        $(document).ready(function(){
            loadBaiduMapScript();
        })
    </script>
    <!--<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>-->
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/account.js"></script>

</asp:Content>
