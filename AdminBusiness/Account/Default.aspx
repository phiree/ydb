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
                        <li><a href="./Default.aspx"><i class="nav-btn side-btn-myshop"></i></a></li>
                        <li><a href="./Security.aspx"><i class="nav-btn side-btn-secret"></i></a></li>
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
                        <!--<img src="../image/touxiangkuang11.png" alt="头像"/>-->
                    </div>
                    <div class="headInfoArea clearfix">
                        <!--<div class="headDecoration1">-->
                        <!--</div>-->
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
                                <input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称"
                                    class="inputShopName" /></p>
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
                <div class="ShopDetailsTitle">
                    <!--<img src="/image/shopicon.jpg" />-->
                    <!--<i class="icon shop-icon-Title"></i>-->
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
                                    <input type="text" class="myshop-input-lg" id="tbxContactPhone" runat="server" name="ContactPhone" /></p>
                            </div>
                            <div class="myshopLeftCont">
                                <p class="p_addressDetail myshop-item-title">
                                    <i class="icon myshop-icon-address"></i>详细店址</p>
                                    <input id="setAddress" class="myshop-btn-setAddress m-b10" type="button" value="请放置店铺坐标" /><input type="hidden" runat="server" clientidmode="Static" id="hiAddrId" />
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
                                    <input type="text" class="myshop-input-lg" runat="server" id="tbxBusinessYears" />
                                </p>
                            </div>
                            <div class="BusinessLicense">
                                <p class="p_BusinessLicense myshop-item-title">
                                    <i class="icon myshop-icon-businessLic"></i>营业执照</p>
                                <div>
                                    <div class="download-img-pre fl">
                                        <asp:HyperLink  runat="server" ID="imgBusinessImage"></asp:HyperLink>
                                        
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
                                        <li><a>学生证</a></li>
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
                                    <asp:HyperLink runat="server" ID="imgChargePerson"></asp:HyperLink>
                                         
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
            <div id="account" class="account">
                账号安全
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript">
        var map = new BMap.Map("addressMap");
        var cityListObject = new BMapLib.CityList({ container: "addressCity", map : map});
        var geoc = new BMap.Geocoder();
        var myGeo = new BMap.Geocoder();

//        map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
        map.enableScrollWheelZoom();
        map.disableDoubleClickZoom();
        map.clearOverlays();

        var myCity = new BMap.LocalCity();

//        function G(id) {
//                return document.getElementById(id);
//            }
//
//            var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
//                    {"input" : "suggestId"
//                        ,"location" : map
//                    });
//
//            ac.addEventListener("onhighlight", function(e) {  //鼠标放在下拉列表上的事件
//                var str = "";
//                var _value = e.fromitem.value;
//                var value = "";
//                if (e.fromitem.index > -1) {
//                    value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                }
//                str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;
//
//                value = "";
//                if (e.toitem.index > -1) {
//                    _value = e.toitem.value;
//                    value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                }
//                str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
//                G("searchResultPanel").innerHTML = str;
//            });
//
//            var myValue;
//            ac.addEventListener("onconfirm", function(e) {    //鼠标点击下拉列表后的事件
//                var _value = e.item.value;
//                myValue = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                G("searchResultPanel").innerHTML ="onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue;
//
//                setPlace();
//            });
//
//            function setPlace(){
//                map.clearOverlays();    //清除地图上所有覆盖物
//                function myFun(){
//                    var pp = local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
//                    map.centerAndZoom(pp, 18);
//                    map.addOverlay(new BMap.Marker(pp));    //添加标注
//
//                }
//                var local = new BMap.LocalSearch(map, { //智能搜索
//                    onSearchComplete: myFun
//                });
//                local.search(myValue);
//            }

//        // 将地址解析结果显示在地图上,并调整地图视野
//        myGeo.getPoint("海南省海口市龙华区海秀中路125号", function(point){
//            if (point) {
//                map.centerAndZoom(point, 18);
//                map.addOverlay(new BMap.Marker(point));
//            }else{
//                alert("您选择地址没有解析到结果!");
//            }
//        }, "北京市");

        map.addEventListener("click",setAddressPoint);
        function setAddressPoint (e){
            map.clearOverlays();

            var addPrintBox = $("#addPrintBox");
            var addressText = $("#addressText");
            var addressP = new BMap.Point(e.point.lng, e.point.lat);
            var addressMark = new BMap.Marker(addressP);
            var addrNodeBox = $(document.createElement("div"));


            geoc.getLocation(addressP, function(rs){
                var addComp = rs.addressComponents;
                var addJson = {
                    "province": addComp.province,
                    "city": addComp.city,
                    "district": addComp.district,
                    "street" : addComp.street,
                    "streetNumber" : addComp.streetNumber,
                    "lng" : rs.point.lng,
                    "lat" : rs.point.lat
                };

                console.log(JSON.stringify(addJson));
                $('#hiAddrId').attr("value",JSON.stringify(addJson));
                var addressNode = "<span>" + addComp.province + "</span><span>" + addComp.city + "</span><span>" + addComp.district + "</span><span>" + addComp.street + "</span><span>" + addComp.streetNumber + "</span>"
                addressText.html(addressNode);

            if ( addPrintBox.html() != "" ){
                    addPrintBox.find('div').html(addressNode);
                } else {
                    addrNodeBox.html(addressNode);
                    addrNodeBox.addClass('myshop-addPrint');
                    addPrintBox.append(addrNodeBox);
                }
            });
            map.addOverlay(addressMark);
//            addressMark.setAnimation(BMAP_ANIMATION_BOUNCE);

        }

        //信息载入时读取地图信息
        (function readAddressLoc() {
            if ( $('#hiAddrId').attr("value") ){
                var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));
                var addrNodeBox = $(document.createElement("div"));
                var addressNode = "<span>" + readAddrJson.province + "</span><span>" + readAddrJson.city + "</span><span>" + readAddrJson.district + "</span><span>" + readAddrJson.street + "</span><span>" + readAddrJson.streetNumber + "</span>"
                var addPrintBox = $("#addPrintBox");
                var addressText = $("#addressText");


//                var nPoint = new BMap.Point(readAddrJson.lng, readAddrJson.lat)
//                map.panTo(nPoint);

                addressText.html(addressNode);
                addrNodeBox.html(addressNode);
                addrNodeBox.addClass('myshop-addPrint m-b10');
                addPrintBox.append(addrNodeBox);
            }
        })();



        $("#setAddress").click(function (e) {
            $('#addrlightBox').lightbox_me({
                centered: true,
                onLoad : function(){
                    if( !$("#hiAddrId").attr("value") ) {
                        console.log($("#hiAddrId").attr("value"))
                        myCity.get(function(result){
                            map.panTo(result.center);
                        });
                    } else {
                        console.log($("#hiAddrId").attr("value") + "2")
                        var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));
                        var nPoint = new BMap.Point(readAddrJson.lng, readAddrJson.lat);
                        var addressMark = new BMap.Marker(nPoint);

                        console.log(nPoint);
                        map.centerAndZoom(nPoint,13);
                        map.addOverlay(addressMark);
                    }

                }
            });
            e.preventDefault();
        })


    </script>
</asp:Content>
