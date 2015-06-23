<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Service_Edit.aspx.cs" Inherits="DZService_Service_Edit" %>
<%@ Register  Src="~/DZService/ServiceEdit.ascx" TagName="ServiceEdit" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="/css/service.css" rel="stylesheet" type="text/css" />
 <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css'
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="mainContent clearfix">
            <div class="leftContent" id="leftCont">
                <div>
                    <ul>
                        <li><a href="../DZService"><i class="nav-btn side-btn-service"></i></a></li>
                        <li><a href="../DZService/Service_Edit.aspx"><i class="nav-btn side-btn-serviceSet"></i></a>
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
                    <span>添加服务信息</span>
                </div>
                <div class="serviceMain clearfix">
                     <UC:ServiceEdit runat="server" />
                    <div class="bottomArea">
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
      <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>

       
    <script   type="text/javascript">
        $(function () {
            $("#tabsServiceType").TabSelection({
                "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
                'enable_multi':false,
                "leaf_clicked": function (id) {
                    tabRadioShow(id);
                }

            });
        });
    </script>

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

//                    businessNode += '<span>' + businessSelet.eq(i).get(0).options[businessSelet.eq(i).get(0).selectedIndex].title + '</span>';
                    businessNode += ("<span>"  + businessSelet.eq(i).get(0).options[businessSelet.eq(i).get(0).selectedIndex].title + "&nbsp;" + "</span>");
                } else {
                    break;
                }
            }
            $('#hiBusinessAreaCode').val(businessvalue);
            businiessText.html(businessNode);
        });

        // 单选时，选择的服务类型显示
        function tabRadioShow(id){
            var radioShowBox = $('#radioShowBox');

//            var TypeNodeBox = "<div item_id=" + id + ">" + radioText + "</div>";
            var TypeNodeBox = $(document.createElement("div"));


            if ( id ) {
                var radioItem = $(event.currentTarget).find($("span[item_id=" + id + "]"));
                var radioText = radioItem.text();

                $("#hiTypeId").val(id);
                TypeNodeBox.attr("item_id",id);
                radioItem.addClass('radioCk');
                console.log(radioItem.siblings());
                $(event.currentTarget).find($("span[item_id!=" + id + "]")).each(function(){
                    $(this).removeClass('radioCk');
                });

                if ( radioShowBox.html() != "" ){
                    radioShowBox.find('div').text(radioText);
                } else {
                    TypeNodeBox.text(radioText);
                    TypeNodeBox.addClass('business-radioCk');
                    radioShowBox.append(TypeNodeBox);
                }
            } else {
                return
            }
        }

        //  多选时,选择的服务类型显示
        function tabCheckedShow(that, id, checked, level) {
            var checkedShowBox = $('#serCheckedShow');
            var v_id = id;
            var v_level = level;
            var checkedItem = $($(that).parents('.serviceTabsItem')).find('.item');
            var checkedText = checkedItem.html();
            var checkedParentId = checkedItem.attr("parent_id");

            if (checked == true) {
                createTypeBox($(that), checkedParentId, checkedText, v_level);

            } else {
                removeTypeBox($(that), checkedParentId, checkedText, v_level);
            }

            function createTypeBox(that, p_id, text, level) {
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

