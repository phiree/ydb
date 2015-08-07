<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZService_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cont-wrap">
        <div class="cont-container">
            <div class="service-info info-top">
                <div class="cont-row">
                    <div class="cont-col-3">
                        <div class="cont-row">
                            <div class="cont-col-4"><p class="service-cont-title">服务名称:</p></div>
                            <div class="cont-col-8"><%=CurrentService.Name %></div>
                        </div>
                    </div>
                    <div class="cont-col-9">
                        <div class="cont-row">
                            <div class="cont-col-2"><p class="service-cont-title">服务类型:</p></div>
                            <div class="cont-col-10"><%=CurrentService.ServiceType.ToString() %></div>
                        </div>
                    </div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3">
                        <div class="cont-row">
                            <div class="cont-col-4"><p class="service-info-title">服务标签:</p></div>
                            <div class="cont-col-8"></div>
                        </div>
                    </div>
                    <div class="cont-col-9">
                        <div class="cont-row">
                            <div class="cont-col-2"><p class="service-info-title">服务区域:</p></div>
                            <div class="cont-col-10"><span id="spServiceArea" class="text-ellipsis">
                            </span>
                            
                            </div>
                            <input type="hidden" id="hiServiceArea" value='<%=CurrentService.BusinessAreaCode %>' />
                        </div>
                    </div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-1"><p class="service-info-title">服务介绍:</p></div>
                    <div class="cont-col-7"><%=CurrentService.Description %></div>
                </div>
            </div>
            <div class="service-info info-bottom">
                <div class="cont-row">
                    <div class="cont-col-12"><p class="service-info-title">服务信息</p></div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3"><span class="service-info-text">服务起步价:</span><%=CurrentService.MinPrice.ToString("#.#") %>元</div>
                    <div class="cont-col-3"><span class="service-info-text">服务单价:</span><%=CurrentService.UnitPrice.ToString("#.#") %></div>
                    <div class="cont-col-3"><span class="service-info-text">提前预约:</span><%=CurrentService.OrderDelay %></div>
                    <div class="cont-col-3"><span class="service-info-text">服务时间:</span><%=CurrentService.ServiceTimeBegin %>-<%=CurrentService.ServiceTimeEnd %></div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3"><span class="service-info-text">每日最大接单量:</span><%=CurrentService.MaxOrdersPerDay %></div>
                    <div class="cont-col-3"><span class="service-info-text">每小时最大接单量:</span><%=CurrentService.MaxOrdersPerHour%></div>
                    <div class="cont-col-3"><span class="service-info-text">是否上门服务:</span>
                    <%=CurrentService.ServiceMode==Dianzhu.Model.Enums.enum_ServiceMode.NotToHouse?"不上门":"上门" %></div>
                    <div class="cont-col-3"></div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-3"><span class="service-info-text">服务对象:</span><%=CurrentService.IsForBusiness?"可以对公":"对私" %></div>
                    <div class="cont-col-3"><span class="service-info-text">服务保障</span>:<%=CurrentService.IsCompensationAdvance?"有":"无" %></div>
                    <div class="cont-col-3"><span class="service-info-text">付款方式:</span><%=(CurrentService.PayType & Dianzhu.Model.Enums.PayType.Offline) == Dianzhu.Model.Enums.PayType.Offline?"线下":string.Empty%>
                    <%=(CurrentService.PayType & Dianzhu.Model.Enums.PayType.Online) == Dianzhu.Model.Enums.PayType.Online ? "线上" : string.Empty%>
                    </div>
                    <div class="cont-col-3"><span class="service-info-text">平台认证:</span><%=CurrentService.IsCertificated?"已认证":"未认证" %></div>
                </div>
            </div>
            <div class="t-r">
                <a class="btn btn-default" role="button" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>&serviceid=<%=CurrentService.Id %>"> 编辑</a></div>
           </div>
</div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
<script>
    $(function () {
    var jsonServiceArea=$.parseJSON($("#hiServiceArea").val());
       $("#spServiceArea").html(jsonServiceArea.provinceName
                               +jsonServiceArea.cityName
                               +jsonServiceArea.boroughName
                               +jsonServiceArea.businessName);
     });
</script>
</asp:Content>

