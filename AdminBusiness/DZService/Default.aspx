<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="DZService_Default" %>

<%@ Register Src="~/DZService/ServiceEdit.ascx" TagName="ServiceEdit" TagPrefix="UC" %>
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
        <div class="cont-container mh-in service-list-container dis-n">
            <div class="cont-row">
                <div class="cont-col-12">
                    <div class="cont-row row-fix">
                        <div class="cont-col-12 m-b10">
                            <a class="btn btn-default btn-info" role="button" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>">
                                +&nbsp;添加新服务</a>
                        </div>
                    </div>
                    <table class="custom-table service-table">
                        <thead>
                            <tr>
                                <th>
                                    服务名称
                                </th>
                                <th>
                                    服务类别
                                </th>
                                <th>
                                    服务时间
                                </th>
                                <th>
                                    服务范围
                                </th>
                                <th>
                                    提前预约
                                </th>
                                <th>
                                    服务状态
                                </th>
                                <th>
                                    服务操作
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptServiceList">
                                <ItemTemplate>
                                    <tr onclick="listhref('/DZService/detail.aspx?businessid=<%=Request["businessid"]%>&serviceId=<%#Eval("Id") %>')">
                                        <td class="table-col-1">
                                            <%#Eval("Name") %>
                                        </td>
                                        <td class="table-col-2">
                                            <%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.Name  %>
                                        </td>
                                        <td class="table-col-2">
                                            <%#Eval("ServiceTimeBegin")%>~<%#Eval("ServiceTimeEnd")%>
                                        </td>
                                        <td class="table-col-3">
                                            <p class="spServiceArea l-h16 t-c">
                                            </p>
                                            <input type="hidden" id="hiServiceArea" class="hiServiceArea" value='<%#((Dianzhu.Model.DZService)GetDataItem()).BusinessAreaCode %>' />
                                        </td>
                                        <td class="table-col-1">
                                            <%#Eval("OrderDelay")%>小时
                                        </td>
                                        <td class="table-col-1">
                                            <p class="t-c service-status <%#Eval("Id") %>'> <%# ((bool)Eval("Enabled"))?"theme-color-right":"theme-color-delete" %>"
                                                serid='<%#Eval("Id") %>'>
                                                <%# ((bool)Eval("Enabled"))?"已启用":"已禁用" %></p>
                                        </td>
                                        <td class="table-col-2">
                                            <p class="t-c <%# ((bool)Eval("Enabled"))?"btn btn-down-info":"btn btn-info" %> enable-service"
                                                serid='<%#Eval("Id") %>'>
                                                <%# ((bool)Eval("Enabled"))?"禁用":"启用" %></p>
                                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-delete m-l10" CommandArgument='<%# Eval("Id")%>'
                                                OnCommand="delbt_Command" OnClientClick="javascript:return confirm('警告：\n数据一旦被删除将无法还原！')">删除</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="cont-col-12">
                    <div class="pageNum">
                         <UC:AspNetPager runat="server" FirstPageText="首页" NextPageText="下一页" PrevPageText="上一页"
                             ID="pager" PageSize="10" UrlPaging="true" LastPageText="尾页">
                         </UC:AspNetPager>
                     </div>
                </div>
            </div>
        </div>
        <div class="service-new dis-n">
            <div class="cont-container mh-in">
                <div class="new-box">
                    <div class="t-c">
                        <img src="/image/service-new.png" />
                    </div>
                    <div class="service-new-add">
                        <a id="firstAddBusiness" class="new-add-btn" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>">
                            点击创建新服务</a>
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
    <script>
        function listhref(url) {
            var e = window.event || arguments.callee.caller.arguments[0];
            var target = e.srcElement || e.target;
            var $target = $(target);

            if ($target.hasClass("btn")) {
                return false
            } else if (e.target == e.target) {
                window.location.href = url
            };
        }


        $(function () {
            if ($(".service-table tbody").find("tr").length == 0) {
                $(".service-new").removeClass("dis-n");
            } else {
                $(".service-list-container").removeClass("dis-n");
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

        $(document).ready(function () {
            loadBaiduMapScript();
        })
    </script>
    <!--<script type="text/javascript" src="/js/CityList.js"></script>-->
    <!--<script type="text/javascript" src="/js/service.js"></script>-->
</asp:Content>
