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
                            <span class="ServiceShops"><%=CurrentBusiness.Name %></span> <span class="InfoCompletetxt">信誉度</span>
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
                <div class="serviceRightWrap">
                    <UC:ServiceEdit ID="ServiceEdit1" runat="server" />
                </div>
                <div class="bottomArea">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
     <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
   
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript" src="/js/service.js"></script>
</asp:Content>
