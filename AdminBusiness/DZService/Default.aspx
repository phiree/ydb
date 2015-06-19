<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZService_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView runat="server" ID="gvServices">
    <Columns>
    <asp:HyperLinkField  DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" DataTextField="Name"/>
    </Columns>
    </asp:GridView>
    <!--<a href="SelectType.aspx">增加服务</a>-->
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
                                <img src="..\image\myshop\touxiangkuang_11.png" alt="头像" />
                            </div>
                            <div class="headInfo">
                                <span class="ServiceShops">点助的服务店铺</span>
                                <span class="InfoCompletetxt">信誉度</span>
                                <div class="Servicexing">
                                    <i class="icon service-icon-star"></i>
                                    <i class="icon service-icon-star"></i>
                                    <i class="icon service-icon-star"></i>
                                    <i class="icon service-icon-star"></i>
                                    <i class="icon service-icon-star"></i>
                                </div>
                            </div>
                            <div class="headEditImg">
                                <span class="satisfaction">100%</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="service-title">
                    <span>详细服务信息</span>
                </div>
                <div class="serviceMain clearfix">
                    <div class="serviceLeft">
                        <div class="serviceChoice">
                            <ul>
                                <li><a>服务一</a></li>
                                <li><a>服务二</a></li>
                                <li><a>服务三</a></li>
                            </ul>
                        </div>

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
                    <div class="serviceRightWrap">
                    <div class="serviceRight clearfix">
                        <div class="service-md">
                            <div class="service-md-title">基本信息<i class="icon servie-icon-mdStand"></i></div>
                            <div class="service-md-main clearfix">
                                <div class="service-md-mleft">
                                    <div class="service-m">
                                        <p class="p_ServiceType service-item-title"><i class="icon service-icon-serType"></i>请选择您的服务类型</p>
                                        <div class="clearfix">
                                            <div>
                                                <div>
                                                    <input id="setSerType" class="ser-btn-SerType" type="button" value="选择服务信息" />
                                                    <div id="setSerTypeShow">

                                                    </div>
                                                </div>
                                                <div id="SerlightBox" class="serviceTabs dis-n">
                                                    <div id="tabsServiceType" class="" >
                                                        <ul></ul>
                                                    </div>
                                                    <p class="ser-chk-title">已选中的服务</p>
                                                    <div id="serCheckedShow" class="ser-chk-show">

                                                    </div>
                                                    <div>
                                                        <input id="confirmSer" class="close ser-sm-input" type="button" value="确定" />
                                                        <input class="close ser-sm-input" type="button" value="取消" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="service-m">
                                        <p class="p_serviceIntroduced service-item-title"><i class="icon service-icon-serIntro"></i>服务介绍</p>
                                        <p><textarea class="serviceIntroduced" name="tbxDescription" placeholder="(可输入60个字)"></textarea></p>
                                    </div>
                                </div>
                                <div class="service-md-mright">
                                    <div class="service-m">
                                        <p class="p_QualificationCertificate service-item-title"><i class="icon service-icon-LIC"></i>资质证书</p>
                                        <div>
                                            <div class="input-file-box d-inb">
                                                <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
                                                <i class="input-file-bg"></i>
                                                <i class="input-file-mark"></i>
                                                <img class="input-file-pre" src="" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="service-md">
                            <div class="service-md-title">服务范围<i class="icon servie-icon-mdRound"></i></div>
                            <div class="service-md-main">
                                <div class="service-m">

                                    <p class="p_LocationArea service-item-title"><i class="icon service-icon-serLocal"></i>定位服务区域</p>
                                    <!--<p class="f-s13 l-h16">服务中心点定位(为您的服务区域进行定位)</p>-->
                                    <div class="clearfix">
                                        <div  class="fl clearfix">
                                            <div id="setBusiness" class="setLocationMap">

                                            </div>
                                            <input id="businiessSeletValue" type="hidden">
                                            <!--<p id="businessText"></p>-->
                                        </div>
                                        <div class="fl m-l20">
                                            <p class="m-b20">您选择的位置：</p>
                                            <p id="businessText" class="m-b50"></p>
                                            <p class="l-h16">←点击选择商圈范围</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="service-md">
                            <div class="service-md-title">服务信息<i class="icon servie-icon-mdItem"></i></div>
                            <div class="service-md-main">
                                <div class="service-md-box clearfix">
                                    <div class="service-md-mleft">
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务起步价</p>
                                            <div>
                                                <input class="service-input-mid" type="text" name="tbxMinPrice">&nbsp;&nbsp;元
                                            </div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务单价</p>
                                            <div>
                                                <input class="service-input-mid" type="text" name="tbxUnitPrice">&nbsp;&nbsp;元&nbsp;/&nbsp;每&nbsp;( <input type="radio" name="tbxUnit">&nbsp;小时&nbsp;<input type="radio" name="tbxUnit">&nbsp;天&nbsp;<input type="radio" name="tbxUnit">&nbsp;次 )
                                            </div>
                                        </div>
                                    </div>
                                    <div class="service-md-mright">
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>提前预约时间</p>
                                            <div>至少&nbsp;&nbsp;<input class="service-input-mid" type="text" name="tbxOrderDelay">&nbsp;&nbsp;分钟</div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务时间</p>
                                            <div><input class="service-input-sm" type="text" name="tbxServiceTimeBegin">&nbsp;&nbsp;至&nbsp;&nbsp;<input class="service-input-sm" type="text" name="tbxServiceTimeEnd"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="service-md-box clearfix">
                                    <div class="service-md-mleft">
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>每日最大接单量</p>
                                            <div><input class="service-input-mid" type="text" name="tbxMaxOrdersPerDay">&nbsp;&nbsp;单</div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>每小时最大接单量</p>
                                            <div><input class="service-input-mid" type="text" name="tbxMaxOrdersPerHour">&nbsp;&nbsp;单</div>
                                        </div>
                                    </div>
                                    <div class="service-md-mright">
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>是否上门</p>
                                            <div><input type="radio" name="rblServiceMode" value="y"/>&nbsp;&nbsp;是&nbsp;&nbsp;<input type="radio" name="rblServiceMode" value="n"/>&nbsp;&nbsp;否</div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务对象</p>
                                            <div><input type="radio" name="cblIsForBusiness" value="1"/>&nbsp;&nbsp;对公&nbsp;&nbsp;<input type="radio" name="cblIsForBusiness" value=""/>&nbsp;&nbsp;对私</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="service-md-box clearfix">
                                    <div class="service-md-mleft">
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>服务保障</p>
                                            <input type="checkbox" name="tbxIsCompensationAdvance">&nbsp;&nbsp;加入先行赔付
                                        </div>
                                    </div>
                                    <div class="service-md-mright">
                                        <div class="service-m">
                                            <p class="service-item-title"><i class="icon service-icon-serIntro"></i>平台认证</p>
                                            <input type="checkbox" name="cbxIsCertificated">&nbsp;&nbsp;已通过平台认证
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--<div class="serviceDetailsLeft">-->
                            <!--<div class="p-20">-->
                            <!--</div>-->
                        <!--</div>-->
                        <!--<div class="serviceDetailsRight">-->
                            <!--<div class="p-20">-->
                                <!--百度地图选择商圈-->
                                <div id="mapLightBox" class="dis-n">
                                    <div class="mapWrap">
                                        <div id="businessMap" class="mapMain"></div>
                                        <div id="businessCity" class="mapCity"></div>
                                        <div class="mapButton"><input id="confBusiness" class="close ser-sm-input" type="button" value="确定"></div>
                                        <input id="businessValue" type="hidden" value=""/>
                                    </div>
                                </div>
                            <!--</div>-->
                        <!--</div>-->
                    </div>
                    </div>
                    <div class="bottomArea">
                    <!--<input name="imageSave" type="image" id="imageSave" src="image/baocun_1.png" />-->
                    <!--<input name="imageCancel" type="image" id="imageCancel" src="image/baocun_2.png" />-->
                    </div>
                </div>
        </div>
    </div>
    </body>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.9.1.min.js"></script>-->
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.10.2.js"></script>
    <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>-->
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js" ></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript">
    var map = new BMap.Map("businessMap");
    var cityListObject = new BMapLib.CityList({container : "businessCity"});
    map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
    map.enableScrollWheelZoom();

//    商圈设置
    $("#setBusiness").click(function(e){
        $('#mapLightBox').lightbox_me({
            centered: true
        });
        e.preventDefault();
    });

    $('#confBusiness').click(function(){
        var businessSelet = $('#businessCity').find('select');
        var businiessText = $('#businessText');
        var businessvalue = "";
        var businessNode = "";

        for (var i = 0 ; i < businessSelet.length; i++ ){
            if (businessSelet.eq(i).val() != null ){
                    console.log(businessSelet.eq(i).val());
                    businessvalue += "m/" + businessSelet.eq(i).val();//获取商圈个段的code,以“/m”区分各字段

                    businessNode += '<span>' + businessSelet.eq(i).get(0).options[businessSelet.eq(i).get(0).selectedIndex].title + '</span>';//
            } else {
                break;
            }
        }
        $('#businiessSeletValue').val(businessvalue);
        businiessText.html(businessNode);
    });


    $(function () {
        $("#tabsServiceType").TabSelection({
           "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
           "enable_multiselect":true,
           'check_changed': function (that,id, checked,level) {
        //                    alert(id + '' + checked);
                 tabCheckedShow(that,id,checked,level);
           },

           'leaf_ clicked': function (id, checked) {
        //                alert(id);
           }
        });
    });

//    选择的服务类型显示
    function tabCheckedShow(that,id,checked,level){
        var checkedShowBox = $('#serCheckedShow');
        var v_id = id;
        var v_level = level;
        var checkedItem = $($(that).parents('.serviceTabsItem')).find('.item');
        var checkedText = checkedItem.html();
        var checkedParentId = checkedItem.attr("parent_id");

        if (checked == true) {

//            console.log(checkedParentId);
            createTypeBox($(that),checkedParentId,checkedText,v_level);

//            var checkedTextNode = "<span id=" + v_id + " level=" + level + " >" + checkedText + "</span>";

//            checkedShowBox.append(checkedTextNode);
        } else {
            removeTypeBox($(that),checkedParentId,checkedText,v_level);
//            checkedShowBox.children('span').remove("#" + v_id + "");
        }
//        return checkedTextNode;

        function createTypeBox (that,p_id,text,level) {
//            var p_id = that.attr("parent_id");
            console.log(level);
            var printBox = checkedShowBox;
            var TypeNodeBox = "<div v_id="+ v_id + ">" + text + "</div>";

            switch (level)
            {
                case "0":
                    printBox = checkedShowBox;
                    printBox.append(TypeNodeBox);
                    $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class","level0");
                    break;
                case "1":
                    printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                    printBox.append(TypeNodeBox);
                    $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class","level1");
                    break;
                case "2":
                    printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                    printBox.append(TypeNodeBox);
                    $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class","level2");
                    break;
                case "3":
                    printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                    printBox.append(TypeNodeBox);
                    $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class","level3");
                    break;
                default :
                    break;
//                    console.log("break");
            };
        };

        function removeTypeBox(that,p_id,text,level){
            $(checkedShowBox.find($("div[v_id=" + v_id + "]"))).remove();
        }
    }

    $("#setSerType").click(function(e){
        $('#SerlightBox').lightbox_me({
            centered: true
        });
        e.preventDefault();
    })
// $(document).ready(function(){

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

//        });
    </script>

</asp:Content>
