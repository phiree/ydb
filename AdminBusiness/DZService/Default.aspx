
<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="DZService_Default" %>
    <%@ Register  Src="~/DZService/ServiceEdit.ascx" TagName="ServiceEdit" TagPrefix="UC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--服务列表-->
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css'
        rel="stylesheet" type="text/css" />
    <link href="/css/validation.css" rel="stylesheet" type="text/css">
    <link href="/css/ServiceSelect.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
            <div class="cont-wrap">
                <div class="mh-in">
                    <div class="cont-container animated fadeInUpSmall">
                        <div class="mh-ctnr">
                            <div id="service-list" class="service-list dis-n">
                                <div class="cont-row">
                                    <div class="cont-col-12 m-b20">
                                        <a class="btn btn-default btn-info" role="button" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>" >+&nbsp;添加新服务</a>
                                    </div>
                                </div>
                                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                    <asp:Repeater runat="server" ID="rptServiceList"  >
                                        <ItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="heading<%# Container.ItemIndex + 1 %>" >
                                                    <table class="custom-table" role="button" data-toggle="collapse" data-parent="#accordion"
                                                           href="#collapse<%# Container.ItemIndex + 1 %>" aria-expanded="true"
                                                           aria-controls="collapse<%# Container.ItemIndex + 1 %>" >
                                                        <tbody>
                                                        <tr>
                                                            <td class="table-col-1">
                                                                <div class="icon service-icon svcType-s-icon-<%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.TopType.Id  %>">

                                                                </div>
                                                            <input type="hidden"  value="<%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.TopType.Id  %>"   />

                                                            </td>
                                                            <td class="table-col-2 bord-r-d">
                                                                <div class="panel-info-td">
                                                                    <p class="panel-title panel-title-imp"><%#Eval("Name") %></p>
                                                                    <p><%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.Name  %></p>
                                                                </div>

                                                            </td>
                                                            <td class="table-col-3 bord-r-d">
                                                                <div class="panel-info-td">
                                                                    <p class="panel-title">
                                                                    <span class="spServiceArea text-ellipsis panel-title-local" ></span><input type="hidden" id="hiServiceArea" class="hiServiceArea" value='<%#((Dianzhu.Model.DZService)GetDataItem()).BusinessAreaCode %>' /></p>
                                                                    <p class="panel-title-tips"><i class="icon service-icon-local"></i>服务区域</p>
                                                                </div>
                                                            </td>
                                                            <td class="table-col-2 bord-r-d">
                                                                <div class="panel-info-td">
                                                                    <p class="panel-title">
                                                                        <a class="panel-title-link" href="ServiceTimeline.aspx?businessid=<%=Request.Params["businessId"] %>&serviceId=<%#Eval("Id") %>" collapse-ignore="true">详细服务时间</a>
                                                                    </p>

                                                                    <p class="panel-title-tips"><i class="icon service-icon-time"></i>服务时间</p>
                                                                </div>

                                                            </td>
                                                            <td class="table-col-2 bord-r-d">
                                                                <div class="panel-info-td">
                                                                    <p class="panel-title"><sapn class="panel-title-time"><%#Eval("OrderDelay")%></sapn>分钟</p>
                                                                    <p class="panel-title-tips"><i class="icon service-icon-data"></i>提前预约时间</p>
                                                                </div>

                                                            </td>
                                                            <td class="table-col-2">
                                                                <div class="panel-info-td">
                                                                    <p><p class="t-c service-status <%#Eval("Id") %>'> <%# ((bool)Eval("Enabled"))?"theme-color-right":"theme-color-delete" %>" serid='<%#Eval("Id") %>'> <%# ((bool)Eval("Enabled"))?"已启用":"已禁用" %></p>
                                                                    <p collapse-ignore="true" class="t-c <%# ((bool)Eval("Enabled"))?"btn btn-down-info":"btn btn-info" %> enable-service" serid='<%#Eval("Id") %>' > <%# ((bool)Eval("Enabled"))?"禁用":"启用" %></p></p>
                                                                    <p>
                                                                        <a href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>&serviceid=<%#Eval("Id") %>" class="m-r10" ><i class="icon service-icon-edit" title="编辑" collapse-ignore="true"></i></a>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" class="btn-test-1" CommandArgument='<%# Eval("Id")%>' OnCommand="delbt_Command" OnClientClick="javascript: return confirm('警告：\n数据一旦被删除将无法还原！')" data-target="ture" collapse-ignore="true"><i class="icon service-icon-delete" title="删除" collapse-ignore="true"></i></asp:LinkButton>

                                                                    </p>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div id="collapse<%# Container.ItemIndex + 1 %>" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading<%# Container.ItemIndex + 1 %>">
                                                    <div class="panel-body">
                                                        <table class="custom-table panel-detail-table">
                                                            <tbody>
                                                            <tr>
                                                                <td class="table-col-1 t-r">服务介绍：</td>
                                                                <td colspan="3"><%#Eval("Description") %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="table-col-1 t-r">起步价：</td>
                                                                <td class="table-col-1 "><span class="panel-table-num"><%# ((decimal)Eval("MinPrice")).ToString("#") %></span>&nbsp;元</td>
                                                                <td class="table-col-2 t-r">每日最大接单量：</td>
                                                                <td class="table-col-2 "><span class="panel-table-num"><%# Eval("MaxOrdersPerDay") %></span>&nbsp;单</td>
                                                                <td class="table-col-1 t-r">服务对象：</td>
                                                                <td class="table-col-1 "><%#((bool)Eval("IsForBusiness"))?"可以对公":"对私" %></td>
                                                                <td class="table-col-1 t-r">服务方式：</td>
                                                                <td class="table-col-1 "><%#  (Dianzhu.Model.Enums.enum_ServiceMode)Eval("ServiceMode")==Dianzhu.Model.Enums.enum_ServiceMode.NotToHouse?"不上门":"上门" %></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="table-col-1 t-r">单价：</td>
                                                                <td class="table-col-1 t-l"><span class="panel-table-num"><%#((decimal)Eval("UnitPrice")).ToString("#") %></span>&nbsp;元/单</td>
                                                                <td class="table-col-2 t-r">每时最大接单量：</td>
                                                                <td class="table-col-2 t-l"><span class="panel-table-num"><%# Eval("MaxOrdersPerHour") %></span>&nbsp;单</td>
                                                                <td class="table-col-1 t-r">服务保障：</td> 
                                                                <td class="table-col-1 t-l"><%# ((bool)Eval("IsCompensationAdvance"))?"有":"无" %></td>
                                                                <td class="table-col-1 t-r">平台认证：</td>
                                                                <td class="table-col-1 t-l"><%# ((bool)Eval("IsCertificated"))?"有":"无" %></td>
                                                            </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div id="service-new" class="dis-n">
                                <div class="new-box">
                                    <div class="t-c">
                                        <img src="/image/service-new.png"/>
                                    </div>
                                    <div class="service-new-add">
                                        <a id="firstAddBusiness" class="new-add-btn" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>">点击创建新服务</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/js/ServiceType.js?v=20150901"></script>
    <script type="text/javascript" src="/js/ServiceSelect.js"></script>
    <!--<script type="text/javascript" src="/js/TabSelection.js"></script>-->
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script >
//    function listhref(url){
//        var e = window.event || arguments.callee.caller.arguments[0];
//        var target = e.srcElement || e.target;
//        var $target = $(target);
//        console.log($target);
//
//        if($target.hasClass("btn")){
//            return false
//        }else if(e.target == e.target){
////            window.location.href = url
//            return true
//        };
//    }

    $(function(){
        if ( $("#accordion").children(".panel").length == 0 ){
            $("#service-new").removeClass("dis-n");
        } else {
            $("#service-list").removeClass("dis-n");
        }

       $(".service-table tbody tr:even").addClass("list-item-odd");
    })
    </script>
    <script type="text/javascript">
        var name_prefix = 'ctl00$ContentPlaceHolder1$ServiceEdit1$';
       $(function () {
           $("#serList").ServiceSelect({
               "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
               "choiceContainer": "serChoiceContainer",
               "choiceOutContainer": "lblSelectedType",
               "printInputID": "hiTypeId",
               "choiceConfBtn": "serChoiceConf",
               "localdata": typeList
           });

           $(".enable-service").click(function () {
               var that = this;
               $.post("/ajaxservice/changeserviceInfo.ashx",
                        {
                            "changed_field": "enabled",
                            "changed_value": false,
                            "id": $(that).attr("serid")
                        }, function (data) {
                            var enabled = data.data;
                            if (enabled == "True") {
                                $(that).html("禁用");
                                $($(that).parent().parent()).find(".service-status").html("已启用");
                                $($(that).parent().parent()).find(".service-status").removeClass("theme-color-delete").addClass("theme-color-right");
                                $(that).removeClass("btn-info").addClass("btn-down-info");
                            }
                            else {
                                $(that).html("启用");
                                $($(that).parent().parent()).find(".service-status").html("已禁用");
                                $($(that).parent().parent()).find(".service-status").removeClass("theme-color-right").addClass("theme-color-delete");
                                $(that).addClass("btn-info").removeClass("btn-down-info");

                            }

                        });
           });

           $("#setSerType").click(function (e) {
               $('#serLightContainer').lightbox_me({
                   centered: true
               });
               e.preventDefault();
           });

           function readTypeData() {
               var hiTypeValue = $("#hiTypeId").attr("value");
               if (hiTypeValue != undefined) {
                   $("#lblSelectedType").removeClass("dis-n");
                   $("#lblSelectedType").addClass("d-inb");
               } else {
                   return;
               }
           };

           readTypeData();

           $(".spServiceArea").each(function () {
               var jsonServiceArea = $.parseJSON($(this).siblings(".hiServiceArea").val());
           $(this).html(jsonServiceArea.serPointAddress);
           });
                            

       });
    </script>
    <script src="/js/validation_service_edit.js" type="text/javascript"></script>
    <script src="/js/validation_invalidHandler.js" type="text/javascript"></script>
    <script>
        function loadBaiduMapScript() {
          var script = document.createElement("script");
          script.src = "http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW&callback=initializeService";
          document.body.appendChild(script);
        }

        $(document).ready(function(){
            loadBaiduMapScript();
        })
    </script>
    <!--<script type="text/javascript" src="/js/CityList.js"></script>-->
    <!--<script type="text/javascript" src="/js/service.js"></script>-->
</asp:Content>
