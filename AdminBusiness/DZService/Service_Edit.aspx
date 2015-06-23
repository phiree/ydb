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
    <UC:ServiceEdit runat="server" />
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
                    $("#hiTypeId").val(id);
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

                    businessNode += '<span>' + businessSelet.eq(i).get(0).options[businessSelet.eq(i).get(0).selectedIndex].title + '</span>'; //
                } else {
                    break;
                }
            }
            $('#businiessSeletValue').val(businessvalue);
            businiessText.html(businessNode);
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

