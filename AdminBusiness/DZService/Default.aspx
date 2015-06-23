<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="DZService_Default" %>
    <%@ Register  Src="~/DZService/ServiceEdit.ascx" TagName="ServiceEdit" TagPrefix="UC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css'
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent clearfix">
        <div class="leftContent" id="leftCont">
            <div>
                <ul>
                    <li><a href="../DZService"><i class="nav-btn side-btn-service"></i></a></li>
                    <li><a href="../DZService/Edit.aspx"><i class="nav-btn side-btn-serviceSet"></i></a>
                    </li>
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
                            <span class="ServiceShops">点助的服务店铺</span> <span class="InfoCompletetxt">信誉度</span>
                            <div class="Servicexing">
                                <i class="icon service-icon-star"></i><i class="icon service-icon-star"></i><i class="icon service-icon-star">
                                </i><i class="icon service-icon-star"></i><i class="icon service-icon-star"></i>
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
                            <asp:Repeater runat="server" ID="rptServiceList">
                                <ItemTemplate>
                                    <li><a href='default.aspx?id=<%#Eval("id") %>'>
                                        <%#Eval("Name") %></a></li></ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                     
                </div>
                 <UC:ServiceEdit ID="ServiceEdit1" runat="server" />
                <div class="bottomArea">
                    <!--<input name="imageSave" type="image" id="imageSave" src="image/baocun_1.png" />-->
                    <!--<input name="imageCancel" type="image" id="imageCancel" src="image/baocun_2.png" />-->
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
     <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.10.2.js"></script>
     <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript">
        var map = new BMap.Map("businessMap");
        var cityListObject = new BMapLib.CityList({ container: "businessCity" });
        map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
        map.enableScrollWheelZoom();

        //    商圈设置
        $("#setBusiness").click(function (e) {
            $('#mapLightBox').lightbox_me({
                centered: true
            });
            e.preventDefault();
        });

        $('#confBusiness').click(function () {
            var businessSelet = $('#businessCity').find('select');
            var businiessText = $('#businessText');
            var businessvalue = "";
            var businessNode = "";

            for (var i = 0; i < businessSelet.length; i++) {
                if (businessSelet.eq(i).val() != null) {
                    console.log(businessSelet.eq(i).val());
                    businessvalue += "m/" + businessSelet.eq(i).val(); //获取商圈个段的code,以“/m”区分各字段

                    businessNode += '<span>' + businessSelet.eq(i).get(0).options[businessSelet.eq(i).get(0).selectedIndex].title + '</span>'; //
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
                'enable_multi': false,
                "leaf_clicked": function (id) {
                    $("#hiTypeId").val(id);
                }

            });
        });

        //    选择的服务类型显示
        function tabCheckedShow(that, id, checked, level) {
            var checkedShowBox = $('#serCheckedShow');
            var v_id = id;
            var v_level = level;
            var checkedItem = $($(that).parents('.serviceTabsItem')).find('.item');
            var checkedText = checkedItem.html();
            var checkedParentId = checkedItem.attr("parent_id");

            if (checked == true) {

                //            console.log(checkedParentId);
                createTypeBox($(that), checkedParentId, checkedText, v_level);

                //            var checkedTextNode = "<span id=" + v_id + " level=" + level + " >" + checkedText + "</span>";

                //            checkedShowBox.append(checkedTextNode);
            } else {
                removeTypeBox($(that), checkedParentId, checkedText, v_level);
                //            checkedShowBox.children('span').remove("#" + v_id + "");
            }
            //        return checkedTextNode;

            function createTypeBox(that, p_id, text, level) {
                //            var p_id = that.attr("parent_id");
                console.log(level);
                var printBox = checkedShowBox;
                var TypeNodeBox = "<div v_id=" + v_id + ">" + text + "</div>";

                switch (level) {
                    case "0":
                        printBox = checkedShowBox;
                        printBox.append(TypeNodeBox);
                        $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level0");
                        break;
                    case "1":
                        printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                        printBox.append(TypeNodeBox);
                        $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level1");
                        break;
                    case "2":
                        printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                        printBox.append(TypeNodeBox);
                        $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level2");
                        break;
                    case "3":
                        printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                        printBox.append(TypeNodeBox);
                        $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level3");
                        break;
                    default:
                        break;
                    //                    console.log("break");    
                };
            };

            function removeTypeBox(that, p_id, text, level) {
                $(checkedShowBox.find($("div[v_id=" + v_id + "]"))).remove();
            }
        }

        $("#setSerType").click(function (e) {
            $('#SerlightBox').lightbox_me({
                centered: true
            });
            e.preventDefault();
        })
      
    </script>
</asp:Content>
