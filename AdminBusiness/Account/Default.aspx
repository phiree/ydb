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
            <div class="leftContent" id="leftCont">
                <div>
                    <ul>
                        <li>
                            <a href="/account/Default.aspx" target="_self">
                                <div class="side-btn-bg d-inb side-btn-myshop">
                                    <h4 class="side-btn-t ">基本信息</h4>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="/account/Security.aspx" target="_self">
                                <div class="side-btn-bg d-inb side-btn-secret">
                                    <h4 class="side-btn-t ">帐号安全</h4>
                                </div>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div id="myshop-wrap">
                <div class="myshopInfoArea clearfix">
                    <div class="myshopInfoTilte">
                        <h1>
                            商家基本信息</h1>
                    </div>
                    <div class="headInfoArea clearfix">

                        <div class="headImage fl m-r10">
 
                            <div class="input-file-box headFile">
                              <input type=file class="input-file-btn"  name="input-file-btn-avater" businessId="<%=CurrentBusiness.Id %>" imageType="businessavater" />
                                         
                                         
 
                                <i class="input-file-bg"  style='background-image:url(<%=b.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(b.BusinessAvatar.ImageName)+"&width=90&height=90&tt=2)":"../image/myshop/touxiangkuang_11.png" %>' ></i>
                                <i  class="input-file-mark"></i>
                                <img style="top:auto;left:auto;position:inherit;" class="input-file-pre" src="..\image\00.png" />
                            </div>
                        </div>
                        <div class="headInfo fl">
                            <div class="headInfoVc">
                                <p>
                                    <input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称" class="inputShopName" /></p>
                                <p class="InfoCompletetxt">
                                    资料完成程度</p>
                                <div class="InfoPercentage">
                                    <div class="InfoComplete">
                                        <span  style='width:<%=b.CompetePercent %>%' class="progress"></span>
                                    </div>
                                    <span class="completePercentage"><%=b.CompetePercent %>%</span>
                                </div>
                            </div>
                        </div>
                        <!--<div class="headEditImg">-->
                            <!--<a href="javascript:void(0);" class="headEditBtn"></a>-->
                        <!--</div>-->
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
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-shopIntro"></i>店铺介绍</p>
                                    <div>
                                        <textarea class="myshop-input-textarea" id="tbxIntroduced" runat="server" name="shopIntroduced">(可输入60个字)</textarea>
                                    </div>
                                </div>
                                <div class="myshopLeftCont">
                                    <p class="p_ContactPhone myshop-item-title">
                                        <i class="icon myshop-icon-phone"></i>联系电话</p>
                                    <p>
                                        <input type="text" class="myshop-input-lg" id="tbxContactPhone" runat="server" name="ContactPhone"/></p>
                                </div>
                                <div class="myshopLeftCont">
                                    <p class="p_addressDetail myshop-item-title">
                                        <i class="icon myshop-icon-address"></i>详细店址</p>
                                        <input id="setAddress" class="myshop-btn-setAddress m-b10" type="button" name="setAddress" value="请放置店铺坐标" /><input type="hidden" focusID="setAddress" runat="server" clientidmode="Static" id="hiAddrId" name="addressDetailHide" />
                                    <div id="addPrintBox"></div>
                                    <p><input type="text" class="myshop-input-lg" id="tbxAddress" runat="server" name="addressDetail" /></p>
                                </div>
                                <div class="myshopLeftCont">
                                    <p class="p_email myshop-item-title">
                                        <i class="icon myshop-icon-email"></i>邮箱地址</p>
                                    <p>
                                        
                                        <input type="text" class="myshop-input-lg" runat="server" id="tbxEmail" name="email" /></p>
                                </div>
                                <div class="myshopLeftCont">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-workTime"></i>从业时间</p>
                                        <div class="d-inb select select-sm time-select">
                                            <ul></ul>
                                            <input type="hidden" class="myshop-input-lg" runat="server" id="tbxBusinessYears" name="workYears"/>
                                        </div>
                                </div>
                                <div class="BusinessLicense">
                                    <p class="p_BusinessLicense myshop-item-title">
                                        <i class="icon myshop-icon-businessLic"></i>营业执照</p>
                                    <div>
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

                                         
                                             <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ShopDetailsAreaRight">
                                <div class="myshopRightCont ShopFigure">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-shopFigure"></i>店铺图片展示</p>
                                    <div class="clearfix">
                                        <asp:Repeater runat="server" ID="rpt_show" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre  m-r10 fl">
                                                    <asp:Button Text=" " formnovalidate OnClientClick="javascript:return confirm('确定删除?')" CssClass="download-img-delete" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            id="imgLicence" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="input-file-box d-inb">
                                            <input type=file class="input-file-btn"  name="input-file-btn-show"  businessId="<%=CurrentBusiness.Id %>" imageType="businessshow" />

                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                </div>
                                <div class="myshopRightCont">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-empNum"></i>员工人数</p>
                                    <p>
                                        <input type="text" class="myshop-input-mid" runat="server" value="0" clientidmode="Static" id="selStaffAmount" />
                                    </p>
                                </div>

                                <div class="myshopRightCont">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-owner"></i>负责人姓名</p>
                                    <p>
                                        <input type="text" class="myshop-input-mid" runat="server" clientidmode="Static" id="tbxContact" />
                                    </p>
                                </div>
                                <div class="myshopRightCont">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-licenseType"></i>证件类型</p>
                                    <div class="select select-sm">
                                        <ul>
                                            <li><a>身份证</a></li>
                                            <li><a>其它</a></li>
                                        </ul>
                                        <input type="hidden" id="selCardType" value="0" runat="server" clientidmode="Static" />

                                    </div>
                                </div>
                                <div class="myshopRightCont">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-licenseNum"></i>证件号码</p>
                                    <p>
                                        <input type="text" class="myshop-input-mid" runat="server" id="tbxCardIdNo" name="tbxCardIdNo" clientidmode="Static" />
                                    </p>
                                </div>
                                <div class="myshopRightCont HeadProfilePicture">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-ownerPic"></i>负责人证件照上传</p>
                                    <div class="clearfix">
                                    <asp:Repeater runat="server" ID="rptChargePersonIdCards" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre  m-r10 fl">
                                                    <asp:Button Text=" " ID="ibCharge" OnClientClick="javascript:return confirm('确定删除?')" CssClass="download-img-delete" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            class="imgCharge" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box m-r10 fl">
                                        <input type=file class="input-file-btn" businessId="<%=CurrentBusiness.Id %>" imageType="businesschargeperson" />

                                               <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
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
                                    <input id="confBusiness" class="close myshop-sm-input" type="button" value="确定"><span class="myshop-locTip">点击地图放置地点哦！</span></div>
                                <input id="businessValue" type="hidden" value="" />
                            </div>
                        </div>
                    </div>
                    <div class="bottomArea">
                        <input name="imageField" runat="server" onserverclick="btnSave_Click" type="image"
                            id="imageField1" src="../image/myshop/shop_tx_107.png" />
                        <input name="imageField" type="image" id="imageField2" src="../image/myshop/shop_tx_108.png" />
                    </div>
                </div>
            </div>
            <div id="account" class="account">
                账号安全
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
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript" src="/js/account.js"></script>
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
                { rules: service_validate_rules,
                    messages: service_validate_messages,
                    invalidHandler: invalidHandler
                }
            );

            });

            //图片上传
 
            
    </script>
        <script src="/js/FileUpload.js" type="text/javascript"></script>
       <script src="/js/validation_shop_edit.js" type="text/javascript"></script>
 
    <script src="/js/validation_invalidHandler.js" type="text/javascript"></script>

</asp:Content>
