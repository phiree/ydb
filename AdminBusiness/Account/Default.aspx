<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/myshop.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css'
        rel="stylesheet" type="text/css" />
    <link href="/css/validation.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent clearfix">
        <div class="leftContent" id="leftCont">
            <div class="side-bar">
                <ul>
                    <li class="side-btn-br">
                        <a href="/account/Default.aspx" target="_self">
                            <div class="side-btn-bg d-inb">
                                <i class="icon side-btn-icon side-icon-myshop"></i>
                                <h4 class="side-btn-t ">基本信息</h4>
                            </div>
                        </a>
                    </li>
                    <li>
                        <a href="/account/Security.aspx" target="_self">
                            <div class="side-btn-bg d-inb">
                                <i class="icon side-btn-icon side-icon-secret"></i>
                                <h4 class="side-btn-t ">帐号安全</h4>
                            </div>
                        </a>
                    </li>
                </ul>
                <i class="icon side-arrow"></i>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div id="myshop-wrap" class="cont-wrap">
                <div class="cont-container">
                    <div class="cont-row">
                        <div class="cont-title">
                            <h1 class="cont-head-one">商家基本信息</h1>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-2">
                            <div class="headImage">
                                <div class="input-file-box headFile">
                                  <input type=file class="input-file-btn"  name="input-file-btn-avater" businessId="<%=CurrentBusiness.Id %>" imageType="businessavater" />
                                    <i class="input-file-bg"  style='background-image:url(<%=b.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(b.BusinessAvatar.ImageName)+"&width=90&height=90&tt=2)":"../image/myshop/touxiangkuang_11.png" %>' ></i>
                                    <i  class="input-file-mark"></i>
                                    <img style="top:auto;left:auto;position:inherit;" class="input-file-pre" src="..\image\00.png" />
                                </div>
                            </div>
                        </div>
                        <div class="cont-col col-4">
                            <div class="myshop-name">
                                <p class="vm"><input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称" class="myshop-name-input input-mid" /></p>
                            </div>
                        </div>
                        <div class="cont-col col-6">
                            <div class="text-anno myshop-name-anno vmBox">
                                <p class="vm">请上传你的店铺商标，并填写店铺名称。<span class="anno-r">（必填选项）</span></p>
                            </div>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-12">
                            <div class="myshop-intro">
                                <p class="cont-sub-title"><span>店铺介绍</span><span class="title-tips shop-tips">（必填选项）</span></p>
                                <div class="cont-row">
                                    <div class="cont-col col-6">
                                        <textarea class="input-textarea" id="tbxIntroduced" runat="server" name="shopIntroduced">(可输入60个字)</textarea>
                                    </div>
                                    <div class="cont-col col-6">
                                        <div class="text-anno myshop-intro-anno vmBox">
                                            <p class="vm">请将您的店铺简介在此输入<br/><span class="anno-r">（字数限制在0~80个字符之间）</span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont-container">
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-12">
                            <p class="cont-sub-title"><span>联系电话</span></p>
                            <div><input type="text" class="input-lg" id="tbxContactPhone" runat="server" name="ContactPhone"/></div>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-12">
                            <p class="cont-sub-title"><span>公司网站</span></p>
                            <div><input type="text" class="input-lg" runat="server" id="tbxEmail" name="email" /></div>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-12">
                            <p class="cont-sub-title">店铺图片展示</p>
                            <div class="cont-row">
                                <div class="cont-col col-6">
                                    <div class="clearfix">
                                        <asp:Repeater runat="server" ID="rpt_show" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre m-b10 m-r10 fl">
                                                    <asp:Button Text=" " formnovalidate OnClientClick="javascript:return confirm('确定删除?')" CssClass="download-img-delete" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            id="imgLicence" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box fl m-b10 m-r10">
                                            <input type=file class="input-file-btn"  name="input-file-btn-show"  businessId="<%=CurrentBusiness.Id %>" imageType="businessshow" />
                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                </div>
                                <div class="cont-col col-6">
                                    <div class="text-anno myshop-pic-anno vmBox">
                                        <p class="vm">请至少上传一张图片<br/><span class="anno-r">（图片大小限制在2M以下）</span></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont-container">
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-12">
                            <p class="cont-sub-title">店铺地址</p>
                            <div>
                                <p><input type="text" class="input-lg m-b20" id="tbxAddress" runat="server" name="addressDetail" /></p>
                                <div class="cont-row">
                                    <div class="cont-col col-3">
                                        <p><input id="setAddress" class="myshop-btn-setAddress m-b10" type="button" name="setAddress" value="点击放置店铺坐标" /></p>
                                    </div>
                                    <div class="cont-col col-9">
                                        <div class="addPrint-vm">
                                            <div id="addPrintBox"></div>
                                        </div>
                                    </div>
                                </div>
                                <p><input type="hidden" focusID="setAddress" runat="server" clientidmode="Static" id="hiAddrId" name="addressDetailHide" /></p>
                            </div>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-6">
                            <p class="cont-sub-title">从业时间</p>
                            <div class="d-inb select select-sm years-select">
                                <ul></ul>
                                <input type="hidden" class="input-lg" runat="server" id="tbxBusinessYears" name="workYears"/>
                            </div>
                            <span class="myshop-span">年</span>
                        </div>
                        <div class="cont-col col-6">
                            <p class="cont-sub-title">员工人数</p>
                            <div>
                                <input type="text" class="input-mid" runat="server" value="0" clientidmode="Static" id="selStaffAmount" />
                                <span class="myshop-span">人</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont-container">
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-4">
                            <p class="cont-sub-title">负责人姓名</p>
                            <div>
                                <input type="text" class="input-mid" runat="server" clientidmode="Static" id="tbxContact" />
                            </div>
                        </div>
                        <div class="cont-col col-4">
                            <p class="cont-sub-title">证件类型</p>
                            <div>
                                <div class="select select-sm">
                                    <ul>
                                        <li><a>身份证</a></li>
                                        <li><a>其它</a></li>
                                    </ul>
                                    <input type="hidden" id="selCardType" value="0" runat="server" clientidmode="Static" />
                                </div>
                            </div>
                        </div>
                        <div class="cont-col col-4">
                            <p class="cont-sub-title">证件号码</p>
                            <div>
                                <input type="text" class="input-mid" runat="server" id="tbxCardIdNo" name="tbxCardIdNo" clientidmode="Static" />
                            </div>
                        </div>
                    </div>
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-12">
                            <p class="cont-sub-title">负责人证件照上传</p>
                            <div class="cont-row">
                                <div class="cont-col col-6">
                                    <div class="clearfix">
                                        <asp:Repeater runat="server" ID="rptChargePersonIdCards" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre m-b10 m-r10 fl">
                                                    <asp:Button Text=" " ID="ibCharge" OnClientClick="javascript:return confirm('确定删除?')" CssClass="download-img-delete" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            class="imgCharge" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box m-b10 m-r10 m-r10 fl">
                                            <input type=file class="input-file-btn" businessId="<%=CurrentBusiness.Id %>" imageType="businesschargeperson" />
                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                </div>
                                <div class="cont-col col-6">
                                    <div class="text-anno myshop-pic-anno vmBox">
                                        <p class="vm">请至少上传一张图片<br/><span class="anno-r">（图片大小限制在2M以下）</span></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cont-container">
                    <div class="cont-row myshop-cont-row">
                        <div class="cont-col col-12">
                            <p class="cont-sub-title">营业执照</p>

                            <div class="cont-row">
                                <div class="cont-col col-6">
                                    <div class="clearfix">
                                        <asp:Repeater runat="server" ID="rptLicenseImages" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre m-r10 fl">
                                                 <asp:Button ID="img" OnClientClick="javascript:return confirm('确定删除?')"  Text=" "   formnovalidate CssClass="download-img-delete" runat="server" CommandName="delete"
                                                          ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                      <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            class="imgCharge" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box fl">
                                            <input type=file class="input-file-btn"   name="input-file-btn-license" businessId="<%=CurrentBusiness.Id %>" imageType="businesslicense" />
                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                </div>
                                <div class="cont-col col-6">
                                    <div class="text-anno myshop-pic-anno vmBox">
                                        <p class="vm">请至少上传一张图片<br/><span class="anno-r">（图片大小限制在2M以下）</span></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--                <div class="myshopInfoArea clearfix">
                    <div class="myshopInfoTilte">
                        <h1>
                            商家基本信息</h1>
                    </div>
                    <div class="headInfoArea clearfix">


                        <div class="headInfo fl">
                            <div class="headInfoVc">

                                    资料完成程度</p>
                                <div class="InfoPercentage">
                                    <div class="InfoComplete">
                                        <span  style='width:<%=b.CompetePercent %>%' class="progress"></span>
                                    </div>
                                    <span class="completePercentage"><%=b.CompetePercent %>%</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ShopMainWrap">
                    <div class="ShopDetailsTitle">
                        <span>店铺资料编辑</span>
                    </div>
                    <div class="ShopDetailsMain">
                        <div class="clearfix">
                            <div class="ShopDetailsAreaLeft">

                                <div class="myshopLeftCont">

                                </div>
                                <div class="myshopLeftCont">

                                </div>
                                <div class="myshopLeftCont">

                                </div>
                                <div class="myshopLeftCont">

                                </div>
                                <div class="BusinessLicense">

                                </div>
                            </div>
                            <div class="ShopDetailsAreaRight">
                                <div class="myshopRightCont ShopFigure">

                                </div>
                                <div class="myshopRightCont">

                                </div>

                                <div class="myshopRightCont">

                                </div>
                                <div class="myshopRightCont">

                                </div>
                                <div class="myshopRightCont">

                                </div>
                                <div class="myshopRightCont HeadProfilePicture">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>-->
                <div id="addrlightBox" class="dis-n">
                    <div class="mapWrap">
                        <div id="addressMap" class="mapMain">
                        </div>
                        <div id="addressCity" class="mapCity">
                        </div>
                        <div id="addressText" class="mapAddrsText myshop-addPrint-map">

                        </div>
                        <div class="mapButton">
                            <input id="confBusiness" class="close myshop-sm-input" type="button" value="确定"><span class="myshop-locTip">点击地图放置地点哦！</span></div>
                        <input id="businessValue" type="hidden" value="" />
                    </div>
                </div>
                <div class="bottomArea">
                    <input name="imageField" runat="server" onserverclick="btnSave_Click" type="image"
                        id="imageField1" src="../image/myshop/shop_tx_107.png" />
                    <input name="imageField" type="image" id="imageField2" src="../image/myshop/shop_tx_108.png" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/additional-methods.js" type="text/javascript"></script>
    <script src="/js/jquery.form.min.js" type="text/javascript"></script>
     <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript">
         var name_prefix = 'ctl00$ContentPlaceHolder1$';
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            // set_name_as_id('snsi');
            $.validator.setDefaults({
                ignore: []
            });

            $.validator.addMethod("phone", function (value, element) {
                return /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/.test(value);
            }, "请输入有效的电话号码");
            $.validator.addMethod("idcard", function (value, element) {
                return /^(\d{15}$)|(^\d{17}([0-9]|X))$/.test(value);
            }, "请输入有效的身份证号码");
            $.validator.addMethod('filesize', function (value, element, param) {
                // param = size (en bytes) 
                // element = element to validate (<input>)
                // value = value of the element (file name)
                return this.optional(element) || (element.files[0].size <= param)
            });

            $($("form")[0]).validate(
                {
                errorElement: "p",
                errorPlacement: function(error, element) {
                    error.appendTo( element.parent() );
                  },
                rules: service_validate_rules,
                    messages: service_validate_messages,
                    invalidHandler: invalidHandler
                }
            );

            });
    </script>
    <script src="/js/FileUpload.js" type="text/javascript"></script>
    <script src="/js/validation_shop_edit.js" type="text/javascript"></script>
    <script src="/js/validation_invalidHandler.js" type="text/javascript"></script>
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
