<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZService_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>服务详情</h3>
        </div>
        <div class="content-main">
            <div class="animated fadeInUpSmall">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-9">
                            <div class="model m-b20">
                                <div class="model-h">
                                    <h4>服务基本信息</h4>
                                </div>
                                <div class="model-m">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务名称</span><%=CurrentService.Name %>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">先付定金</span><%=CurrentService.DepositAmount.ToString("0.00") %>
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务类型</span><%=CurrentService.ServiceType.ToString() %>
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">支付要求</span><%= CurrentService.AllowedPayType== Dianzhu.Model.Enums.enum_PayType.AliPay?"支付宝"
                                                                                              :CurrentService.AllowedPayType== Dianzhu.Model.Enums.enum_PayType.Offline?"线下支付":
                                                                                              CurrentService.AllowedPayType== Dianzhu.Model.Enums.enum_PayType.WePay?"微支付":
                                                                                              CurrentService.AllowedPayType== Dianzhu.Model.Enums.enum_PayType.Online?"线上支付":
                                                                                              "不限"%>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="d-hr"></div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务介绍</span><%=CurrentService.Description %>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="model">
                                <div class="model-h">
                                    <h4>服务交易信息</h4>
                                </div>
                                <div class="model-m">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务起步价</span><%=CurrentService.MinPrice.ToString("#.##") %>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务对象</span><%=CurrentService.IsForBusiness?"不限":"对私" %>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">是否上门</span><%=CurrentService.ServiceMode== Dianzhu.Model.Enums.enum_ServiceMode.ToHouse?"上门":"不上门" %>
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务单价</span><%=CurrentService.UnitPrice%>/<%=CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Day?"天":
                                                                                                                            CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Hour?"小时":
                                                                                                                             CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Month?"月":
                                                                                                                              CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Times?"次":"(次)"%>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务保障</span><%=CurrentService.IsCompensationAdvance?"是":"否"%>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">每日最大接单量</span><%=CurrentService.MaxOrdersPerDay%>
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">付款方式</span>(?)
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">提前预约时间</span><%=CurrentService.OrderDelay%>分钟
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="model">
                                <div class="model-h">
                                    <h4>服务范围</h4>
                                </div>
                                <div class="model-m">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
<script>
//    $(function () {
//
//        $(".spServiceArea").each(function () {
//            var jsonServiceArea = $.parseJSON($(this).siblings(".hiServiceArea").val());
//            $(this).html(jsonServiceArea.serPointAddress);
//        });
//     });
</script>
</asp:Content>

