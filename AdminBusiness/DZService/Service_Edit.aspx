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
                        <li><a href="/DZService/Default.aspx"><i class="side-btn side-btn-service"></i></a></li>
                        <li><a href="/DZService/Service_Edit.aspx"><i class="side-btn side-btn-serviceSet"></i></a>
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
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
    <script src="/js/setname.js" type="text/javascript"></script> 
   
    <script type="text/javascript" src="/js/TabSelection.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script type="text/javascript" src="/js/CityList.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script type="text/javascript" src="/js/service.js"></script>
   <script type="text/javascript">
       var name_prefix = 'ctl00$ContentPlaceHolder1$ctl00$';
   
      
   </script>
    <script src="/js/validation_service_edit.js" type="text/javascript"></script>
    <script src="/js/validation_invalidHandler.js" type="text/javascript"></script>
</asp:Content>

