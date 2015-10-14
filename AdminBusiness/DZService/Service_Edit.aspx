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
                             <UC:ServiceEdit runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/additional-methods.js" type="text/javascript"></script>
    <script src="/js/jquery.form.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="/js/ServiceType.js"></script>
    <script type="text/javascript" src="/js/ServiceSelect.js"></script>
    <script type="text/javascript" src="/js/StepByStep.js"></script>
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script src="/js/serviceTimeSelect.js" type="text/javascript"></script>
    <script type="text/javascript">
        var name_prefix = 'ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$ctl00$';
    </script>
    <script src="/js/validation_service_edit.js" type="text/javascript"></script>
    <script src="/js/validation_invalidHandler.js" type="text/javascript"></script>
    <script>
        $(function () {
            (function (){
                var hiTypeValue = $("#hiTypeId").attr("value");
                if ( hiTypeValue == undefined ) {
                    return;
                } else {
                    $("#lblSelectedType").removeClass("dis-n");
                    $("#lblSelectedType").addClass("d-inb");
                }
            })();

            $(".service-time-table tbody tr:even").addClass("list-item-odd");

            $("#serList").ServiceSelect({
                "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
                "choiceContainer": "serChoiceContainer",
                "choiceOutContainer": "lblSelectedType",
                "printInputID": "hiTypeId",
                "choiceConfBtn" : "serChoiceConf",
                "localdata": typeList
            });

            $("#setSerType").click(function (e) {
                $('#serLightContainer').lightbox_me({
                    centered: true,
                    preventScroll: true
                });
                e.preventDefault();
            });

            $(".time-select-wrap").timeSelect();

            $(function () {
                $('[data-toggle="tooltip"]').tooltip(
                    {
                        delay: {show : 500, hide : 100},
                        trigger: 'hover'
                    }
                );
            });

            function setTime(date,timeString){
                var arr=timeString.split(":");
                var hour=parseInt(arr[0]);
                var minites = arr[1]?parseInt(arr[1]):0;
                var seconds=arr[2]?parseInt(arr[2]):0;
                return date.setHours(hour,minites,seconds);
            }

            $.validator.addMethod("endtime_should_greater_starttime", function (value, element) {
                var x_date = new Date();
                var start = $("#tbxServiceTimeBegin").val();
                var end = $("#tbxServiceTimeEnd").val();

                var date_start = setTime(x_date,start);
                var date_end = setTime(x_date,end);
                return date_end > date_start;

            }, "结束时间应该大于开始时间");

            $.validator.addMethod("totalday_should_greater_totalhour", function (value, element) {

                var day = parseInt( $("#tbxMaxOrdersPerDay").val());
                var hour = parseInt( $("#tbxMaxOrdersPerHour").val());

                return day >= hour;

            }, "每日接单量应该大于每小时最大接单量");


            $($("form")[0]).validate(
                {
                    ignore:[],
                    errorElement: "p",
                    errorPlacement: function(error, element) {
                        error.appendTo( element.parent() );
                    },
                    rules: service_validate_rules,
                    messages: service_validate_messages,
//                    invalidHandler: invalidHandler,
//                    showErrors: showErrorsHandler
                }
            );

            $(".steps-wrap").StepByStep({
                stepNextFunc : function(){
                    return $('.steps-wrap').find('.cur-step').find('input,textarea,select').valid();
                }
            });
        });
    </script>
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
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/baiduMapLib.js"></script>
    <script type="text/javascript" src="/js/service.js"></script>
    <script type="text/javascript" src="/js/iptag.js"></script>
</asp:Content>

