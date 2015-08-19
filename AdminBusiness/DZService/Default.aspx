﻿
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
                <div class="cont-container mh-in service-list-container dis-n">
                    <div class="cont-row">
                        <div class="cont-col-12">
                            <div class="cont-row row-fix">
                                <div class="cont-col-12"><a class="btn btn-default btn-add m-b20" role="button" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>" >+&nbsp;添加新服务</a> </div>
                            </div>
                            <div class="service-default-titles">
                                <div class="cont-row">
                                    <div class="cont-col-1"><p class="t-c">服务名称</p></div>
                                    <div class="cont-col-3"><p class="t-c">服务类别</p></div>
                                    <div class="cont-col-1"><p class="t-c">服务时间</p></div>
                                    <div class="cont-col-3"><p class="t-c">服务范围</p></div>
                                    <div class="cont-col-2"><p class="t-c">提前预约时间</p></div>
                                    <div class="cont-col-2"><p class="t-c">服务启用</p></div>
                                </div>
                            </div>
                            <div class="service-default-list">
                                <asp:Repeater runat="server" ID="rptServiceList"  >
                                    <ItemTemplate>
                                     <div  onclick="listhref('/DZService/detail.aspx?businessid=<%=Request["businessid"]%>&serviceId=<%#Eval("Id") %>')" class="cont-row service-list-item">
                                        <div class="cont-col-12">
                                            <div class="cont-row">
                                                <div class="cont-col-1"><p class="t-c text-ellipsis"><div class="text-ellipsis" ><%#Eval("Name") %></div></p>
                     </div>
                                                <div class="cont-col-3"><p class="t-c"><%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.Name  %></p></div>
                                                 <div class="cont-col-1"><p class="t-c"><%#Eval("ServiceTimeBegin")%>~<%#Eval("ServiceTimeEnd")%></p></div>
                                                <div class="cont-col-3">
                                                <p class="spServiceArea text-ellipsis t-c"></p>
                                                <input type="hidden" id="hiServiceArea" class="hiServiceArea" value='<%#((Dianzhu.Model.DZService)GetDataItem()).BusinessAreaCode %>' />
                                                </div>
                                                <div class="cont-col-2"><p class="t-c"><%#Eval("OrderDelay")%></p></div>

                                                      <div class="cont-col-2"><div class="t-c"><p class="t-c <%# ((bool)Eval("Enabled"))?"btn btn-delete":"btn btn-info" %> enable_service" serid='<%#Eval("Id") %>'> <%# ((bool)Eval("Enabled"))?"禁用":"启用" %></p>
                                                          <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-delete" CommandArgument='<%# Eval("Id")%>' OnCommand="delbt_Command" OnClientClick="javascript:return confirm('警告：\n数据一旦被删除将无法还原！')">删除</asp:LinkButton>
                                                     </div></div>

                                            </div>
                                        </div>
                                    </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>



                        </div>
                    </div>
                </div>
                <div class="service-new dis-n">
                    <div class="cont-container mh-in">
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

</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/js/ServiceType.js"></script>
    <script type="text/javascript" src="/js/ServiceSelect.js"></script>
    <!--<script type="text/javascript" src="/js/TabSelection.js"></script>-->
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script >
    function listhref(url){
        var $target = $(event.target)
        if($target.hasClass("btn")){
            return false
        }else if(event.target == event.target){
            window.location.href=url
        };
    }


    $(function(){
        if ( $(".service-default-list").find(".service-list-item").length == 0 ){
            $(".service-new").removeClass("dis-n");
        } else {
            $(".service-list-container").removeClass("dis-n");
        }

       $(".service-default-list .service-list-item:odd").addClass("list-item-odd");
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

           $(".enable_service").click(function () {
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
                                $(that).removeClass("btn-info").addClass("btn-delete");
                            }
                            else {
                                $(that).html("启用");
                                $(that).addClass("btn-info").removeClass("btn-delete");

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
