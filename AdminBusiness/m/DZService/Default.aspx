<%@ Page Title="" Language="C#" MasterPageFile="~/m/c.master" AutoEventWireup="true"
    CodeFile="~/DZService/Default.aspx.cs" Inherits="DZService_Default" %>

<%@ Register Src="~/m/DZService/ServiceEdit.ascx" TagPrefix="UC" TagName="ServiceEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <div data-role="header" style="background: #4b6c8b; border: none;">
        <a data-role="none" href="#left-panel" data-iconpos="notext" data-shadow="false"
            data-iconshadow="false">
            <div style="width: 42.375px; height: 42.375px;">
                <img src="/m/images/my-more.png" /></div>
        </a>
        <h1 style="color: #FFF;">
            <span style="background: url(/m/images/my-o-icon2.png) no-repeat; padding-left: 25px;">
                服务列表</span></h1>
        <a href="#" data-theme="d" data-icon="arrow-l" data-iconpos="notext" data-shadow="false"
            data-iconshadow="false" data-role="none">
            <img src="/m/images/my-r-icon.png" /></a>
           
        <nav data-role="navbar" data-theme="myb">
        <ul>
         <li><a href="/m/DZService/" target="_parent" data-theme="mytile-active">新建服务</a></li>
          <li><a href="/m/DZService/Service_Edit.aspx" target="_parent" data-theme="mytile">信息设置</a></li>
          </ul>
     </nav>
     <!---遮罩层-->
     <script type="text/javascript">
         function maskHide() {
             $('.maskdiv').hide();
         };
         function maskShow() {
             $('.maskdiv').show();


         };
     </script>
      <div class="maskdiv">
         <div class="maskContent">
            <div class="maskC_top">请选择服务项<span onclick="maskHide()" style=" float:right; cursor:pointer;">关闭</span></div>
            <div class="maskC_main">
                <ul style=" margin:0; padding:0;">
                <asp:Repeater runat="server" ID="rptServiceList">
                   <ItemTemplate>
                      <li class="mask_li"><a href='default.aspx?id=<%#Eval("Id")%>' rel='external' data-role="button" data-theme="mya" data-inline="true" onclick="maskHide()"> <%#Eval("Name") %></a></li>  
                   </ItemTemplate>
                </asp:Repeater>       
                </ul>
            </div>
           
         </div>
      </div>
      
      <div style="background:#54789a; height:60px; line-height:60px; ">
         
            <a href="#" onclick="maskShow();" data-icon="arrow-d" data-role="button" data-inline="true" data-iconpos="right" data-shadow="false" style="background:none; border:none;"> 服务选择</a
           
       </div>
    </div>
        </div>
    <UC:ServiceEdit runat="server" ID="UC_ServiceEdit" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="menu_status_service" runat="Server">
    my-ui-btn-active2
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" runat="Server">
 
        <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    
    <script src="../js/CityList.js" type="text/javascript"></script>
    <script src="../js/getMap.js" type="text/javascript"></script>
</asp:Content>
