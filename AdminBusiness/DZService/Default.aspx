
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
                
                <div class="cont-container">
                    <div class="cont-row">
                        <div class="cont-col-12">
                            <div class="cont-row row-fix">
                                <div class="cont-col-12"><a href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>">添加服务</a> </div>
                            </div>
                            <div class="cont-row row-fix">
                                <div class="cont-col-2">服务名称</div>
                                <div class="cont-col-2">服务类别</div>
                                <div class="cont-col-4">服务范围</div>
                                <div class="cont-col-2">服务时间</div>
                                <div class="cont-col-2">是否启用</div>
                            </div>
                            <asp:Repeater runat="server" ID="rptServiceList">
                            <ItemTemplate>
                             <div class="cont-row">
                                <div class="cont-col-12">
                                    <div class="cont-row">
                                        <div class="cont-col-2">
             <a href='/DZService/detail.aspx?businessid=<%=Request["businessid"]%>&serviceId=<%#Eval("Id") %>'><%#Eval("Name") %></a></div>
                                        <div class="cont-col-2"><%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.ToString()  %></div>
                                        <div class="cont-col-4"><%#Eval("BusinessAreaCode")%></div>
                                        <div class="cont-col-2"><%#Eval("ServiceTimeBegin")%>-<%#Eval("ServiceTimeEnd")%></div>
                                        <div class="cont-col-2"><%# ((bool)Eval("Enabled"))?"启用":"禁用" %></div>
                                    </div>
                                </div>
                            </div>
                            </ItemTemplate>
                            </asp:Repeater>
                           
                      
                        </div>
                    </div>
                </div>
            </div>
        <!--</div>-->
    <!--</div>-->

</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript" src="/js/ServiceSelect.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript">
       var name_prefix = 'ctl00$ContentPlaceHolder1$ServiceEdit1$';
       $(function () {
           $("#serList").ServiceSelect({
               "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
               "choiceContainer": "serChoiceContainer",
               "choiceOutContainer": "lblSelectedType",
               "printInputID": "hiTypeId",
               "choiceConfBtn" : "serChoiceConf"
           });

           $("#setSerType").click(function (e) {
               $('#serLightContainer').lightbox_me({
                   centered: true
               });
               e.preventDefault();
           });

           function readTypeData(){
               var hiTypeValue = $("#hiTypeId").attr("value");
               if ( hiTypeValue != undefined ) {
                   $("#lblSelectedType").removeClass("dis-n");
                   $("#lblSelectedType").addClass("d-inb");
               } else {
                   return;
               }
           };

           readTypeData();
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
    <!--<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>-->
    <!--<script type="text/javascript" src="/js/CityList.js"></script>-->
    <!--<script type="text/javascript" src="/js/service.js"></script>-->
</asp:Content>
