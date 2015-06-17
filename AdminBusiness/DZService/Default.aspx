<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZService_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView runat="server" ID="gvServices">
    <Columns>
    <asp:HyperLinkField  DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" DataTextField="Name"/>
    </Columns>
    </asp:GridView>
    <a href="SelectType.aspx">增加服务</a>
    <div class="mainContent clearfix">
        <div class="leftContent" id="leftCont">
            <div>
                <ul>
                    <li><a href="../DZService"><i class="nav-btn side-btn-service"></i></a></li>
                    <li><a href="../DZService/Edit.aspx"><i class="nav-btn side-btn-serviceSet"></i></a></li>
                </ul>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div id="userInfoAreaid">
                <div class="serviceInfoArea">
                    <div class="serviceInfoTilte">
                        <span>服务等级</span>
                    </div>
                    <div class="headInfoArea">
                        <div class="headImage">
                            <img src="image/touxiangkuang_11.png" />
                        </div>
                        <div class="headInfo">
                            <span class="ServiceShops">点助的服务店铺</span>
                            <span class="InfoCompletetxt">信誉度</span>
                            <div class="Servicexing">
                                <img src="image/icon_10.png" />
                                <img src="image/icon_10.png" />
                                <img src="image/icon_10.png" />
                                <img src="image/icon_10.png" />
                            </div>
                        </div>
                        <div class="headEditImg">
                            <span class="satisfaction">98%</span>
                        </div>
                    </div>
                </div>
                <div class="service-title">
                    <span>详细服务信息</span>
                </div>
                <div class="serviceDetailsMain clearfix">
                    <div class="serviceDetailsAreaLeft">
                        <!--<div id="AddServiceArea" >-->
                        <!--<div >-->
                            <!--<img src="image/tianjia_3.png" />-->
                            <!--<span>点击添加新的服务项目</span>-->
                        <!--</div>-->
                        <!--<div id="leftMeunList" class="leftMeunList">-->
                        <!--<div>-->
                            <!--<a href="javascript:;" onclick="meliFun('#meli1');">-->
                                <!--<div id="meli1" class="newServicebtn LeftClearance listColor1">-->
                                    <!--新服务-->
                                <!--</div>-->
                            <!--</a>-->
                        <!--</div>-->
                    </div>
                    <div class="serviceDetailsAreaRight">
                        <div class="ServiceLeft">
                            <div class="p-20">
                                <div class="service-m">
                                    <p class="p_ServiceType service-item-title"><i class="icon service-icon-serType"></i>请选择您的服务类型</p>
                                    <div class="clearfix">
                                        <div class="select fl select-sm">
                                            <ul>
                                                <li><a>照顾1</a></li>
                                                <li><a>照顾2</a></li>
                                                <li><a>照顾3</a></li>
                                            </ul>
                                            <input type="hidden" />
                                        </div>
                                        <!--<i class="icon service-icon-hr fl"></i>-->
                                        <!--<div class="select fl select-sm">-->
                                            <!--<ul>-->
                                                <!--<li><a>月嫂</a></li>-->
                                                <!--<li><a>家庭保洁</a></li>-->
                                                <!--<li><a>家电清洗</a></li>-->
                                            <!--</ul>-->
                                            <!--<input type="hidden" />-->
                                        <!--</div>-->
                                    </div>
                                </div>
                                <div class="service-m">
                                    <p class="p_serviceIntroduced service-item-title"><i class="icon service-icon-serIntro"></i>服务介绍</p>
                                    <p><textarea class="serviceIntroduced" name="tbxDescription" placeholder="(可输入60个字)"></textarea></p>
                                </div>
                                <!--<div class="QualificationCertificate">-->
                                <div class="service-m">
                                    <p class="p_QualificationCertificate service-item-title"><i class="icon service-icon-LIC"></i>资质证书</p>
                                    <div>
                                        <div class="input-file-box d-inb">
                                            <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
                                            <i class="input-file-bg"></i>
                                            <i class="input-file-mark"></i>
                                            <img class="input-file-pre" />
                                        </div>
                                    </div>
                                </div>
                                <!--<div>-->
                                    <!--<p class="p_areaChoose service-item-title"><i class="icon service-icon-serArea"></i>服务区域选择</p>-->
                                    <!--<div id="areaChooseid" class="select select-sm">-->
                                        <!--<ul>-->
                                            <!--<li><a>东城区</a></li>-->
                                            <!--<li><a>中城区</a></li>-->
                                            <!--<li><a>西城区</a></li>-->
                                        <!--</ul>-->
                                        <!--<input type="hidden" />-->
                                    <!--</div>-->
                                <!--</div>-->
                                <div class="setLocation service-m">
                                    <p class="p_LocationArea service-item-title"><i class="icon service-icon-serLocal"></i>定位服务区域</p>
                                    <!--<p class="f-s13 l-h16">服务中心点定位(为您的服务区域进行定位)</p>-->
                                    <p class="f-s13 l-h16">(点击选择商圈范围)</p>
                                    <div class="clearfix">
                                        <!--<div class="setLocationDate fl">-->
                                            <!--<div class="select select-sm">-->
                                                <!--<ul>-->
                                                    <!--<li><a>1</a></li>-->
                                                    <!--<li><a>2</a></li>-->
                                                    <!--<li><a>3</a></li>-->
                                                <!--</ul>-->
                                                <!--<input type="hidden" />-->
                                            <!--</div>-->
                                            <!--<p class="setLocationHint"> 点击打开地图，在地图上选取您的服务中心点</p>-->
                                            <!--<p class="f-s13 l-h16">服务半径（服务范围）</p>-->
                                            <!--<div class="select select-sm">-->
                                                <!--<ul>-->
                                                    <!--<li><a>1000M</a></li>-->
                                                    <!--<li><a>2000M</a></li>-->
                                                    <!--<li><a>3000M</a></li>-->
                                                <!--</ul>-->
                                                <!--<input type="hidden" />-->
                                            <!--</div>-->

                                        <!--</div>-->
                                        <div class="setLocationMap">

                                        </div>
                                        <p><span>上海</span></p>
                                    </div>
                                </div>

                                </div>
                            </div>
                        </div>
                        <div class="ServiceRight">
                            <div class="p-20">

                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务起步价</p>
                                    <div>
                                        <input class="service-input-mid" type="text" name="tbxMinPrice">&nbsp;元
                                    </div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务单价</p>
                                    <div>
                                        <input class="service-input-mid" type="text" name="tbxUnitPrice">&nbsp;元&nbsp;/&nbsp;每&nbsp;( <input type="radio" name="tbxUnit">&nbsp;小时&nbsp;&nbsp;<input type="radio" name="tbxUnit">&nbsp;天&nbsp;&nbsp;<input type="radio" name="tbxUnit">&nbsp;次 )
                                    </div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>是否上门</p>
                                    <div><input type="radio" name="rblServiceMode" value="y"/>&nbsp;是&nbsp;&nbsp;<input type="radio" name="rblServiceMode" value="n"/>&nbsp;否</div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务时间</p>
                                    <div><input class="service-input-sm" type="text" name="tbxServiceTimeBegin">&nbsp;至&nbsp;<input class="service-input-sm" type="text" name="tbxServiceTimeEnd"></div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>每日最大接单量</p>
                                    <div><input class="service-input-mid" type="text" name="tbxMaxOrdersPerDay">&nbsp;单</div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>每小时最大接单量</p>
                                    <div><input class="service-input-mid" type="text" name="tbxMaxOrdersPerHour">&nbsp;单</div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>提前预约时间</p>
                                    <div>至少&nbsp;<input class="service-input-mid" type="text" name="tbxOrderDelay">&nbsp;分钟</div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务对象</p>
                                    <div><input type="radio" name="cblIsForBusiness" value="1"/>&nbsp;对公&nbsp;&nbsp;<input type="radio" name="cblIsForBusiness" value=""/>&nbsp;对私</div>
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>平台认证</p>
                                    <input type="checkbox" name="cbxIsCertificated">&nbsp;已通过平台认证
                                </div>
                                <div class="service-m">
                                    <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务保障</p>
                                    <input type="checkbox" name="tbxIsCompensationAdvance">&nbsp;加入先行赔付
                                </div>
                                <!--<div class="service-m">-->
                                    <!--<p class="p_ServiceTimeSet service-item-title"><i class="icon service-icon-serTimeSet"></i>服务时间设置</p>-->
                                    <!--<div class="select select-sm">-->
                                        <!--<ul>-->
                                            <!--<li><a>单独设置</a></li>-->
                                            <!--<li><a>单独设置2</a></li>-->
                                            <!--<li><a>单独设置3</a></li>-->
                                        <!--</ul>-->
                                        <!--<input type="hidden" />-->
                                    <!--</div>-->
                                <!--</div>-->

                                    <!--<div id="PeriodTime" class="clearfix">
                                        <ul class="PeriodTimeul">
                                            <li id="pt1"><a href="javascript:;" onclick="PeriodTimeFun('#pt1');">时段一</a></li>
                                            <li id="pt2"><a href="javascript:;" onclick="PeriodTimeFun('#pt2');">时段二</a></li>
                                            <li id="pt3"><a href="javascript:;" onclick="PeriodTimeFun('#pt3');">时段三</a></li>
                                            <li id="pt4"><a href="javascript:;" onclick="PeriodTimeFun('#pt4');">时段四</a></li>
                                        </ul>
                                    </div>
                                    <div class="clearfix">
                                        <span>该时段不受主界面的时间开关限制，为可接单状态。</span>
                                        <p class="p_SeDate"><i class="icon service-icon-dateSet"></i>日期设定</p>
                                        <div class="select select-sm">
                                            <ul>
                                                <li><a>2015-04-07</a></li>
                                                <li><a>2015-04-08</a></li>
                                                <li><a>2015-04-09</a></li>
                                            </ul>
                                            <input type="hidden" />
                                        </div>
                                        <p class="p_SetTheTime"><i class="icon service-icon-timeSet"></i>时段设定</p>
                                        <div class="select select-sm">
                                            <ul>
                                                <li><a>8:00</a></li>
                                                <li><a>9:00</a></li>
                                                <li><a>10:00</a></li>
                                            </ul>
                                            <input type="hidden" />
                                        </div>
                                        <div class="jiangexian"></div>
                                        <div class="select select-sm">
                                            <ul>
                                                <li><a>8:00</a></li>
                                                <li><a>9:00</a></li>
                                                <li><a>10:00</a></li>
                                            </ul>
                                            <input type="hidden" />
                                        </div>
                                        <p class="delAndUpdate"> <input name="imagedelete" type="image" id="imagedelete" src="image/button_3.png" /> <input name="imageupdate" type="image" id="imageupdate" src="image/button_4.png" /> </p>
                                    </div>-->

                            </div>
                        </div>
                    </div>
                </div>
                <div class="bottomArea">
                    <input name="imageSave" type="image" id="imageSave" src="image/baocun_1.png" />
                    <input name="imageCancel" type="image" id="imageCancel" src="image/baocun_2.png" />
                </div>
            </div>
            <div id="account" class="account">
                活动设置
            </div>
        </div>
    </div>
    </body>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.9.1.min.js"></script>-->
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.10.2.js"></script>
    <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>-->
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js" ></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
    <script type="text/javascript" src="../js/global.js"></script>
    <script type="text/javascript">
 $(document).ready(function(){

//            function addleftmenu(divid,v){
//                //alert($("#leftMeunList").children().length);
//                $("#leftMeunList").prepend("<a href='javascript:;' onClick='meliFun("+divid+");'><div id='"+divid+"' class='newServicebtn2 LeftClearance listColor2'>"+v+"</div></a>");
//            }

//            function meliFun(e){
//
//                var divlength=$("#leftMeunList").children().length;
//                for(i=1;i<=divlength;i++){
//                    $("#meli"+i).css("background","url(image/weixuan_1.png)")
//                    $("#meli"+i).removeClass("listColor1").addClass("listColor2")
//                }
//                $(e).css("background","url(image/weixuan_2.png)")
//                $(e).removeClass("listColor2").addClass("listColor1")
//                //var tarname=$(e).parent();
//                //$($(e).parent()+" a").css("color","#F00");
//                //alert($(e))
//            }

//            function addlilist(divid,arr,inputid){
//
//                $(divid+" cite").html(arr[0]);
//                //alert(arr.length)
//                for(i=1;i<=arr.length;i++){
//                    $(divid+" ul").append("<li><a href='javascript:;' value="+i+">"+arr[i-1]+"</a></li>");
//                }
//                //alert(arr.length)
//                myselect(divid,inputid);
//            }

//            function PeriodTimeFun(e){
//                var divelength=$("#PeriodTime ul>li").length;
//                //alert(divelength)
//                for(i=1;i<=divelength;i++){
//                    $("#PeriodTime ul>li a").css("border-bottom","none");
//                }
//                $(e+" a").css("border-bottom","#0FF 2px solid");
//
//            }

//            var rheight=$("#rightContent").height()-18;
//            $("#me1").click(function() {
//                meinit(m1);
//                $("#me1").attr("src", "image/button_6.png");
//                $("#userInfoAreaid").css("display", "block");
//                $("#account").css("display", "none");
//            });
//
//            $("#me2").click(function() {
//                meinit(m2);
//                $("#me2").attr("src", "image/button_8.png");
//                $("#userInfoAreaid").css("display", "none");
//                $("#account").css("display", "block");
//            });

//            $("#AddServiceArea ").click(function() {
//                if(liid<uldata1.length+1){
//
//                    liid++;
//                    var divid="meli"+liid;
//                    var isadd=true;
//
//                    if(leftlistM.length>0){
//                        for(j=0;j<leftlistM.length;j++){
//                            if(leftlistM[j]==addtxt){
//                                isadd=false
//                                liid--;
//                                alert("列表已经有了，不能再添加了")
//                            }else{
//                                isadd=true;
//                            }
//
//                        }
//
//                        if(isadd){
//                            addleftmenu(divid,addtxt);
//                            leftlistM[leftlistM.length]=addtxt;
//                        }
//
//                    }else if(leftlistM.length==0){
//                        addleftmenu(divid,addtxt);
//                        leftlistM[leftlistM.length]=addtxt;
//                    }
//
//                    var divlength=$("#leftMeunList").children().length;
//                    for(i=1;i<=divlength;i++){
//                        $("#meli"+i).css("background","url(image/weixuan_1.png)")
//                        $("#meli"+i).removeClass("listColor1").addClass("listColor2")
//                    }
//                    $("#meli1").css("background","url(image/weixuan_2.png)")
//                    $("#meli1").removeClass("listColor2").addClass("listColor1")
//                }else{
//                    alert("服务类型数据已经完，不能再添加")
//                }
//
//            })

    //        addlilist("#ServiceTypeid2",uldata1,"#ServiceTypeid2");

//            $("#pt1 a").css("border-bottom","#0FF 2px solid");

        });
    </script>

</asp:Content>
