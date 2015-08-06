<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Service_Edit.aspx.cs" Inherits="DZService_Service_Edit" %>
<%@ Register  Src="~/DZService/ServiceEdit.ascx" TagName="ServiceEdit" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/css/custom-theme/jquery-ui-1.10.4.custom.css'
        rel="stylesheet" type="text/css" />
    <link href="/css/validation.css" rel="stylesheet" type="text/css">
    <link href="/css/ServiceSelect.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <div class="cont-wrap">
                        <div class="serviceMain clearfix">
                             <UC:ServiceEdit runat="server" />
                        </div>
                </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript" src="/js/ServiceSelect.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript">
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
    <script type="text/javascript">
        var name_prefix = 'ctl00$ContentPlaceHolder1$ctl00$';
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
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/service.js"></script>
</asp:Content>

