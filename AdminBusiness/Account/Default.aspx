<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/myshop.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css'
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent clearfix">
        <div class="leftContent" id="leftCont">
            <div class="leftContent" id="leftCont">
                <div>
                    <ul>
                        <li><a href="/account/Default.aspx"><i class="side-btn side-btn-myshop"></i></a></li>
                        <li><a href="/account/Security.aspx"><i class="side-btn side-btn-secret"></i></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div id="myshop-wrap" ng-app="" ng-controller="init">
                <div class="myshopInfoArea clearfix">
                    <div class="myshopInfoTilte">
                        <h1>
                            商家基本信息</h1>
                    </div>
                    <div class="headInfoArea clearfix">
                        <div class="headImage">
                            <div class="input-file-box fl">
                                <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuAvater" />
                                <i class="input-file-bg"  style='background-image:url(<%=b.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(b.BusinessAvatar.ImageName)+"&width=90&height=90&tt=2)":"../image/myshop/touxiangkuang_11.png" %>' ></i>
                                <i  class="input-file-mark"></i>
                                <img style="top:auto;left:auto;position:inherit;" class="input-file-pre" src="..\image\00.png" />
                            </div>
                        </div>
                        <div class="headInfo">
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
                        <div class="headEditImg">
                            <a href="javascript:void(0);" class="headEditBtn"></a>
                        </div>
                    </div>
                </div>
                <div class="ShopShowWrap">
                    <div class="ShopDetailsTitle">
                        <span>店铺资料编辑</span>
                    </div>
                    <div class="ShopDetailsMain">
                        <div>
                            <p>店铺介绍</p>
                            <p>联系电话</p>
                        </div>
                    </div>
                </div>
                <div class="ShopMainWrap">
                    <div class="ShopDetailsTitle">
                        <span>店铺详细资料</span>
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
                                        <input id="setAddress" class="myshop-btn-setAddress m-b10" type="button" name="setAddress" value="请放置店铺坐标" /><input type="hidden" runat="server" clientidmode="Static" id="hiAddrId" name="addressDetailHide" />
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
                                    <p>
                                        <input type="text" class="myshop-input-lg" runat="server" id="tbxBusinessYears" name="workYears"/>
                                    </p>
                                </div>
                                <div class="BusinessLicense">
                                    <p class="p_BusinessLicense myshop-item-title">
                                        <i class="icon myshop-icon-businessLic"></i>营业执照</p>
                                    <div>
                                        <div class="download-img-pre fl">
                                            <asp:HyperLink  runat="server" CssClass="download-img-show" ID="imgBusinessImage"></asp:HyperLink>

                                        </div>
                                        <div class="input-file-box fl">
                                            <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
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
                                                <div class="download-img-pre fl">
                                                    <asp:ImageButton OnClientClick="javascript:return confirm('确定删除?')" CssClass="download-img-delete" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            id="imgLicence" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="input-file-box d-inb">
                                            <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuShow1" />
                                            <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                            <img class="input-file-pre" src="..\image\00.png" />
                                        </div>
                                    </div>
                                </div>
                                <div class="myshopRightCont">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-empNum"></i>员工人数</p>
                                    <div class="d-inb select select-sm">
                                        <ul>
                                            <li><a>10人</a></li>
                                            <li><a>20人</a></li>
                                            <li><a>50人</a></li>
                                        </ul>
                                        <input type="hidden" runat="server" value="0" clientidmode="Static" id="selStaffAmount" />
                                    </div>
                                    <span>员工信息编辑</span>
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
                                        <input type="text" class="myshop-input-mid" runat="server" id="tbxCardIdNo" clientidmode="Static" />
                                    </p>
                                </div>
                                <div class="myshopRightCont HeadProfilePicture">
                                    <p class="myshop-item-title">
                                        <i class="icon myshop-icon-ownerPic"></i>负责人证件照上传</p>
                                    <div class="clearfix">
                                        <div class="download-img-pre fl">
                                        <asp:HyperLink runat="server" CssClass="download-img-show" ID="imgChargePerson"></asp:HyperLink>
                                        </div>
                                        <div class="input-file-box fl">
                                            <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuChargePerson" />
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
                                <div id="addressText" class="mapAddrsText myshop-addPrint">

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
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>-->
    <!--<script type="text/javascript" src="/js/messages_vali.js"></script>-->
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript" src="/js/account.js"></script>
</asp:Content>
