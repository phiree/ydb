<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZService_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cont-wrap mh">
        <div class="cont-container">
            <div class="service-info info-top">
                <div class="cont-row">
                    <div class="cont-col-12"><p class="cont-h4">基本服务信息</p></div>
                    <div class="cont-col-3">
                        <div class="cont-row">
                            <div class="cont-col-4"><p >服务名称:</p></div>
                            <div class="cont-col-8"><p class="text-ellipsis"><%=CurrentService.Name %></p></div>
                        </div>
                    </div>
                    <div class="cont-col-9">
                        <div class="cont-row">
                            <div class="cont-col-2"><p >服务类型:</p></div>
                            <div class="cont-col-10"><%=CurrentService.ServiceType.ToString() %></div>
                        </div>
                    </div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3">
                        <div class="cont-row">
                            <div class="cont-col-4"><p>服务标签:</p></div>
                            <div class="cont-col-8"></div>
                        </div>
                    </div>
                    <div class="cont-col-9">
                        <div class="cont-row">
                            <div class="cont-col-2"><p>服务区域:</p></div>
                            <div class="cont-col-10"><span id="spServiceArea"   class="spServiceArea text-ellipsis">
                            </span>    <input type="hidden" id="hiServiceArea" class="hiServiceArea" value='<%=CurrentService.BusinessAreaCode %>' />
                       
                            
                            </div>
                         </div>
                    </div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-1"><p>服务介绍:</p></div>
                    <div class="cont-col-7"><p class="text-ellipsis"><%=CurrentService.Description %></p></div>
                </div>
            </div>
            <div class="service-info info-bottom">
                <div class="cont-row">
                    <div class="cont-col-12"><p class="cont-h4">详细服务信息</p></div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3"><span>服务起步价:</span>&nbsp;<%=CurrentService.MinPrice.ToString("#.#") %>元</div>
                    <div class="cont-col-3"><span>服务单价:</span>&nbsp;<%=CurrentService.UnitPrice.ToString("#.#") %></div>
                    <div class="cont-col-3"><span>提前预约:</span>&nbsp;<%=CurrentService.OrderDelay %></div>
                    <div class="cont-col-3"><span>服务时间:</span>&nbsp;<%=CurrentService.ServiceTimeBegin %>-<%=CurrentService.ServiceTimeEnd %></div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3"><span>每日最大接单量:</span>&nbsp;<%=CurrentService.MaxOrdersPerDay %></div>
                    <div class="cont-col-3"><span>每小时最大接单量:</span>&nbsp;<%=CurrentService.MaxOrdersPerHour%></div>
                    <div class="cont-col-3"><span>是否上门服务:</span>&nbsp;<%=CurrentService.ServiceMode==Dianzhu.Model.Enums.enum_ServiceMode.NotToHouse?"不上门":"上门" %></div>
                    <div class="cont-col-3"><span>服务对象:</span>&nbsp;<%=CurrentService.IsForBusiness?"可以对公":"对私" %></div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3"><span>服务保障</span>:&nbsp;<%=CurrentService.IsCompensationAdvance?"有":"无" %></div>
                    <div class="cont-col-3"><span>付款方式:</span>&nbsp;<%=(CurrentService.PayType & Dianzhu.Model.Enums.PayType.Offline) == Dianzhu.Model.Enums.PayType.Offline?"线下":string.Empty%>
                                                                <%=(CurrentService.PayType & Dianzhu.Model.Enums.PayType.Online) == Dianzhu.Model.Enums.PayType.Online ? "线上" : string.Empty%></div>
                    <div class="cont-col-3"><span>平台认证:</span>&nbsp;<%=CurrentService.IsCertificated?"已认证":"未认证" %></div>
                    <div class="cont-col-3"></div>
                </div>
            </div>
            <div class="t-r">
                <a class="btn btn-default btn-info" role="button" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>&serviceid=<%=CurrentService.Id %>">修改服务信息</a></div>
           </div>
</div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
<script>
    $(function () {

        $(".spServiceArea").each(function () {
            var jsonServiceArea = $.parseJSON($(this).siblings(".hiServiceArea").val());
            $(this).html(jsonServiceArea.serPointAddress);
        });
     });
</script>
</asp:Content>

