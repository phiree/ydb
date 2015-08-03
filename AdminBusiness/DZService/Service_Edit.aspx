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
        <div class="mainContent clearfix">
            <div class="leftContent" id="leftCont">
                <div class="side-bar">
                    <ul>
                        <li class="side-btn-br">
                            <a href="/DZService/Default.aspx" target="_self">
                                <div class="side-btn-bg d-inb">
                                    <i class="icon side-btn-icon side-icon-service"></i>
                                    <h4 class="side-btn-t ">服务管理</h4>
                                </div>
                            </a>
                        </li>
                        <li>
                            <a href="/DZService/Service_Edit.aspx" target="_self">
                                <div class="side-btn-bg d-inb">
                                    <i class="icon side-btn-icon side-icon-serviceSet"></i>
                                    <h4 class="side-btn-t ">服务添加</h4>
                                </div>
                            </a>
                        </li>
                    </ul>
                    <i class="icon side-arrow"></i>
                </div>
            </div>
            <div class="rightContent" id="rightCont">
                <div class="cont-wrap">
                    <div class="cont-container">
                        <div class="cont-row">
                            <div class="cont-col col-2">
                                <div class="headInfoArea">
                                    <div class="headImage">
                                        <img src="..\image\myshop\touxiangkuang_11.png" alt="头像" />
                                    </div>
                                </div>
                            </div>
                            <div class="cont-col col-10">
                                <div class="headInfo">
                                    <span class="ServiceShops"><%=CurrentBusiness.Name %></span> <span class="InfoCompletetxt">信誉度</span>
                                    <div class="Servicexing">
                                        <i class="icon service-icon-star"></i><i class="icon service-icon-star"></i><i class="icon service-icon-star">
                                        </i><i class="icon service-icon-star"></i><i class="icon service-icon-star"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div id="userInfoAreaid">--%>
                        <%--<div class="serviceInfoArea">--%>
                            <%--<div class="serviceInfoTilte">--%>
                                <%--<span>服务等级</span>--%>
                            <%--</div>--%>
                            <%--<div class="headInfoArea">--%>
                                <%--<div class="headImage">--%>
                                    <%--<img src="..\image\myshop\touxiangkuang_11.png" alt="头像" />--%>
                                <%--</div>--%>
                                <%--<div class="headInfo">--%>
                                    <%--<span class="ServiceShops"><%=CurrentBusiness.Name %></span> <span class="InfoCompletetxt">信誉度</span>--%>
                                    <%--<div class="Servicexing">--%>
                                        <%--<i class="icon service-icon-star"></i><i class="icon service-icon-star"></i><i class="icon service-icon-star">--%>
                                        <%--</i><i class="icon service-icon-star"></i><i class="icon service-icon-star"></i>--%>
                                    <%--</div>--%>
                                <%--</div>--%>
                                <%--<!--<div class="headEditImg">-->--%>
                                    <%--<!--<span class="satisfaction">100%</span>-->--%>
                                <%--<!--</div>-->--%>
                            <%--</div>--%>
                        <%--</div>--%>
                    <%--</div>--%>
                        <!--<div class="service-title">-->
                            <!--<span>添加服务信息</span>-->
                        <!--</div>-->
                        <div class="serviceMain clearfix">
                             <UC:ServiceEdit runat="server" />
                            <!--<div class="bottomArea">-->
                            <!--</div>-->
                        </div>
                </div>

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

