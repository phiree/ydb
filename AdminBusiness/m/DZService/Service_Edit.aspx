<%@ Page Title="" Language="C#" MasterPageFile="~/m/c.master" AutoEventWireup="true"
    CodeFile="Service_Edit.aspx.cs" Inherits="m_service" %>

<%@ Register Src="~/m/DZService/ServiceEdit.ascx" TagPrefix="UC" TagName="ServiceEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--page-->
    <!--header-->
   
    <div data-role="header" style="background: #4b6c8b; border: none;">
        <a data-role="none" href="#left-panel" data-iconpos="notext" data-shadow="false"
            data-iconshadow="false">
            <div style="width: 42.375px; height: 42.375px;">
                <img src="/m/images/my-more.png" /></div>
        </a>
        <h1 style="color: #FFF;">
            <span style="background: url(/m/images/my-o-icon2.png) no-repeat; padding-left: 25px;">
                服务信息</span></h1>
        <a href="#" data-theme="d" data-icon="arrow-l" data-iconpos="notext" data-shadow="false"
            data-iconshadow="false" data-role="none">
            <img src="/m/images/my-r-icon.png" /></a>
            
        <nav data-role="navbar" data-theme="myb">
        <ul>
         <li><a href="/m/DZService/" target="_parent" data-theme="mytile">信息管理</a></li>
          <li><a href="/m/DZService/Service_Edit.aspx" target="_parent" data-theme="mytile-active">信息设置</a></li>
          </ul>
     </nav>
    </div> 
    
    <!--/header-->
    <!--customer control--> 
    <UC:ServiceEdit runat="server" ID="UC_ServiceEdit" />
  
     
    <!--/customer control-->
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu_status_service" runat="Server">
    my-ui-btn-active
</asp:Content>


