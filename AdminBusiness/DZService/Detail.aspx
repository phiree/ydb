<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZService_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
               <div class="cont-container">
                <ul>
                    <li>服务名称:<%=CurrentService.Name %></li>
                    <li>服务类型:<%=CurrentService.ServiceType.ToString() %></li>
                    <li>服务介绍:<%=CurrentService.Description %></li>
                    <li>服务区域:<%=CurrentService.BusinessAreaCode %></li>
                    <li>服务起步价:<%=CurrentService.MinPrice.ToString("#.#") %></li>
                    <li>服务单价:<%=CurrentService.UnitPrice.ToString("#.#") %></li>
                    <li>提前预约:<%=CurrentService.OrderDelay %></li>
                    <li>服务服务时间:<%=CurrentService.ServiceTimeBegin %>-<%=CurrentService.ServiceTimeEnd %></li>
                    <li>每日最大接单量:<%=CurrentService.MaxOrdersPerDay %></li>
                    <li>每小时最大接单量:<%=CurrentService.MaxOrdersPerHour%></li>
                    <li>是否上门服务:<%=CurrentService.ServiceMode %></li>
                    <li>服务对象:<%=CurrentService.IsForBusiness %></li>
                    <li>服务保障:<%=CurrentService.IsCompensationAdvance %></li>
                    <li>付款方式:<%=CurrentService.PayType %></li>
                    <li>认证平台:<%=CurrentService.IsCertificated %></li>
                    <li><a href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>&serviceid=<%=CurrentService.Id %>"> 编辑</a></li>
</ul>

               </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

