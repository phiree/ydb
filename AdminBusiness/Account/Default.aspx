<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/myshop.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/base/minified/jquery-ui.min.css' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent clearfix">
        <div class="leftContent" id="leftCont">
            <!--<div>-->
                <!--<ul>-->
                    <!--<li><a href="/account/">-->
                        <!--<img id="me1" src="/image/jibenxinxi_3.png" /></a></li>-->
                    <!--<li><a href="/account/security.aspx">-->
                        <!--<img id="me2" src="/image/zhanghaoanquan_2.png" /></a></li>-->
                <!--</ul>-->
            <!--</div>-->
            <div class="leftContent" id="leftCont">
                <div>
                    <ul>
                        <li><a href="myshop.html"><i class="nav-btn side-btn-myshop"></i></a></li>
                        <li><a href="secret.html"><i class="nav-btn side-btn-secret"></i></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div id="userInfoAreaid">
                <div class="userInfoArea">
                    <div class="infoTilte1">
                        <span>商家基本信息</span>
                        <img src="/image/incotou.jpg" />
                    </div>
                    <div class="headInfoArea">
                        <div class="headDecoration1">
                        </div>
                        <div class="headImage">
                            <img src="/image/userheadimg.png" />
                        </div>
                        <div class="headInfo">
                            <input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称"
                                class="inputShopName" />
                            <br />
                            <span class="InfoCompletetxt">资料完成程度</span>
                            <div class="InfoPercentage">
                                <div class="InfoComplete">
                                    <span class="progress"></span>
                                </div>
                                <span class="completePercentage">90%</span>
                            </div>
                        </div>
                        <div class="headEditImg">
                        </div>
                    </div>
                </div>
                <div class="decoration3">
                    <img src="/image/shopicon.jpg" />
                    <span class="ShopDetails">店铺详细资料</span>
                </div>
                <div class="ShopDetailsArea clearfix">
                    <div class="ShopDetailsAreaLeft">
                        <div class="LeftClearance1 shopIntroduced">
                            <p class="p_introduced">店铺介绍</p>
                            <p>
                                <textarea id="tbxIntroduced" runat="server" name="shopIntroduced">(可输入60个字)</textarea>
                            </p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_ContactPhone">
                                联系电话</p>
                            <p>
                                <input type="text" id="tbxContactPhone" runat="server" name="ContactPhone" /></p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_addressDetail">
                                详细店址</p>
                            <p>
                                <input type="text" id="tbxAddress" runat="server" name="addressDetail" /></p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_email">
                                邮箱地址</p>
                            <p>
                                <input type="text" runat="server" id="tbxEmail" name="email" /></p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_WorkingTime">
                                从业时间</p>
                            <p>
                                <input type="text" runat="server" id="tbxBusinessYears" name="email" /></p>
                        </div>
                        <div class="LeftClearance1 BusinessLicense">
                            <p class="p_BusinessLicense">
                                营业执照</p>
                            <div>
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
                                    <i class="input-file-bg"></i>
                                    <i class="input-file-mark"></i>
                                </div>
                            <a href='<%=Config.BusinessImagePath+"/original/"+b.BusinessLicence.ImageName %>'>
                                <img runat="server" id="imgLicence" src="/image/dianjishangchuan_1.png" />
                            </a>
                            </div>
                        </div>
                    </div>
                    <div class="ShopDetailsAreaRight">
                        <div class="RightClearance1 ShopFigure">
                            <p class="p_ShopFigure">
                                店铺图片展示</p>
                            <div>
                                <div class="picture">
                                    <asp:Repeater runat="server" ID="rpt_show"  OnItemCommand="rpt_show_ItemCommand">
                                        <ItemTemplate>
                                            <img class="picEditBtn" src="/image/bianjji_1.png" />
                                            <!--<img class="picDelBtn" src="/image/delete_1.png" runat="server" CommandArgument="Id"  />-->
                                            <asp:ImageButton runat="server" CommandName="delete" ImageUrl="/image/delete_1.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>'/>
                                          
                                             <a href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>

                                             
                                             <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=50&height=50&tt=2'  id="imgLicence"  />
                                             </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuShow1" />
                                    <i class="input-file-bg"></i>
                                    <i class="input-file-mark"></i>
                                </div>
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuShow2" />
                                    <i class="input-file-bg"></i>
                                    <i class="input-file-mark"></i>
                                </div>
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuShow3" />
                                    <i class="input-file-bg"></i>
                                    <i class="input-file-mark"></i>
                                </div>
                                <!--<img src="/image/dianjishangchuan_1.png" />
                                <img src="/image/dianjishangchuan_1.png" />-->
                            </div>
                        </div>
                        <div class="RightClearance1 employees">
                            <p class="p_employees">
                                员工人数</p>
                            <div class="select select-sm">
                                <ul>
                                    <li><a>10人</a></li>
                                    <li><a>20人</a></li>
                                    <li><a>50人</a></li>
                                </ul>
                                <input type="hidden" />
                            </div>
                            <span>员工信息编辑</span>
                        </div>
                        <div class="RightClearance1 pinput2">
                            <p class="p_DirectorName">负责人姓名</p>
                            <p>
                                <input type="text" name="DirectorName" />
                            </p>
                        </div>
                        <div class="RightClearance1 employees">
                            <p class="p_DocumentType">证件类型</p>
                            <div class="select select-sm">
                                <ul>
                                    <li><a>身份证</a></li>
                                    <li><a>学生证</a></li>
                                    <li><a>其它</a></li>
                                </ul>
                                <input type="hidden" />
                            </div>
                        </div>
                        <div class="RightClearance1 pinput3">
                            <p class="p_DocumentNumber">证件号码</p>
                            <p>
                                <input type="text" name="DocumentNumber" />
                            </p>
                        </div>
                        <div class="RightClearance1 HeadProfilePicture">
                            <p class="p_HeadProfilePicture">
                                负责人证件照上传</p>

                            <div>
                            <a href='<%=Config.BusinessImagePath+"/original/"+b.ChargePersonIdCard.ImageName %>'>
                            <img runat="server" id="imgChargePerson" src="/image/dianjishangchuan_1.png" /></a>
                            <div class="input-file-box d-inb">
                                <asp:FileUpload cssclass="input-file-btn" runat="server" ID="fuChargePerson" />
                                <i class="input-file-bg"></i>
                                <i class="input-file-mark"></i>
                            </div>
                            </div>
                        </div>
                        <div id="tabsServiceType">
                            <ul> </ul>
                             
                        </div>
                    </div>
                </div>
                <div class="bottomArea">
                    <input name="imageField" runat="server" onserverclick="btnSave_Click" type="image"
                        id="imageField1" src="/image/baocun_1.png" />
                    <input name="imageField" type="image" id="imageField2" src="/image/baocun_2.png" />
                </div>
            </div>
            <div id="account" class="account">
                账号安全
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>
    <script src="/js/TabSelection.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript">
        $(function () {

            //            $("#tabsServiceType").TabSelection({
            //                "datasource":
            //                [
            //                    { "name": "维修", "id": 1, "parentid": 0 },
            //                    { "name": "家电维修", "id": 2, "parentid": 1 },
            //                     { "name": "冰箱维修", "id":3, "parentid": 2 },
            //                    { "name": "冰箱维修", "id": 6, "parentid": 2 },
            //                    { "name": "冰箱维修", "id": 7, "parentid": 2 },
            //                    { "name": "冰箱维修", "id": 8, "parentid": 2 },

            //                    { "name": "更换氟利昂", "id": 4, "parentid": 3 },
            //                    { "name": "交通服务", "id": 5, "parentid": 0 }
            //                ]
            //            });
            //        });
            $("#tabsServiceType").TabSelection({
                "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
                "enable_multiselect":true,
                'check_changed': function (id, checked) {
                    alert(id + '' + checked);
                },
                'leaf_ clicked': function (id, checked) { alert(jid); }

            });
        });
    </script>
    <script type="text/javascript">
        var m1 = true;
        var m2 = false;
        function meinit(str) {
            m1 = false;
            m2 = false;
            str = true;
            $("#me1").attr("src", "/image/jibenxinxi_4.png");
            $("#me2").attr("src", "/image/zhanghaoanquan_2.png");

        }
        function myselect(divselectid, inputselectid) {
            // alert("ddd");


            var list = $(divselectid + " ul");
            var inputselect = $(inputselectid);
            $(divselectid + " cite").click(function () {
                if (list.css("display") == "none") {
                    list.slideDown("fast");
                } else {
                    list.slideUp("fast");
                }
            });
            var mouseisout = false;
            $(divselectid).mouseover(function () {
                mouseisout = false;
            });
            $(divselectid).mouseout(function () {
                mouseisout = true;
            });
            $(document).click(function () {
                if (mouseisout) {
                    list.hide();
                }
            });

            $(divselectid + " ul li a").click(function () {
                var txt = $(this).text();
                $(divselectid + " cite").html(txt);
                var value = $(this).attr("value");
                inputselect.val(value);
                list.hide();
            });
        }
        $(document).ready(function () {
            $("#me1").click(function () {
                meinit(m1);

                $("#me1").attr("src", "/image/jibenxinxi_3.png");
                $("#userInfoAreaid").css("display", "block");
                $("#account").css("display", "none");


            });
            $("#me2").click(function () {
                meinit(m2);
                $("#me2").attr("src", "/image/zhanghaoanquan_1.png");
                $("#userInfoAreaid").css("display", "none");
                $("#account").css("display", "block");

            });
            $(".picture").hover(function () {
                    $(".picDelBtn").css("display", "block");
                    $(".picEditBtn").css("display", "block");

                }, function () {
                    $(".picDelBtn").css("display", "none");
                    $(".picEditBtn").css("display", "none");

                }


            );

            $(".picDelBtn").css("display", "none");
            $(".picEditBtn").css("display", "none");

            $(".progress").css("width", "90%");
            $(".completePercentage").html("90%");

            myselect("#WorkingTimeid", "#WorkingTime");
            myselect("#employeesid", "#employees");
            myselect("#DocumentTypeid", "#DocumentType");

        });
    </script>
</asp:Content>
