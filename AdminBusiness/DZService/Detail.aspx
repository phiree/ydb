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
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务类型</span><%=CurrentService.ServiceType.ToString().Replace(">", "-") %>
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
                                                <span class="model-pra-t">服务起步价</span><%=CurrentService.MinPrice.ToString("f2") %>元
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">先付订金</span><%=CurrentService.DepositAmount.ToString("f2") %>元
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务单价</span><%=CurrentService.UnitPrice.ToString("f2") %>元/<%=CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Day?"天":
                                                                                                                            CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Hour?"小时":
                                                                                                                             CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Month?"月":
                                                                                                                              CurrentService.ChargeUnit== Dianzhu.Model.Enums.enum_ChargeUnit.Times?"次":"(次)"%>
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务保障</span><%=CurrentService.IsCompensationAdvance?"已加入服务保障":"未参加服务保障"%>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务对象</span><%=CurrentService.IsForBusiness?"不限":"私人/个体" %>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">是否上门</span><%=CurrentService.ServiceMode== Dianzhu.Model.Enums.enum_ServiceMode.ToHouse?"提供上门服务":"不提供上门" %>
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">付款方式</span><%= CurrentService.AllowedPayType== Dianzhu.Model.Enums.enum_PayType.Offline?"线下支付":
                                                                                              CurrentService.AllowedPayType== Dianzhu.Model.Enums.enum_PayType.Online?"线上支付":
                                                                                              "线上/线下"%>
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
                                <div class="model-m no-padding">
                                    <input type="hidden" name="hiServiceArea" id="hiServiceArea" value=<%= CurrentService.BusinessAreaCode %> />
                                    <div id="serviceArea">

                                    </div>
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
    $(function () {
        function loadBaiduMapScript() {
            var script = document.createElement("script");
            script.src = "http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW&callback=initializeServiceDetailMap";
            document.body.appendChild(script);
        }

        $(document).ready(function(){
            loadBaiduMapScript();
        })
     });
</script>
<script src="/js/baiduMapLib.js"></script>
<script>
    function initializeServiceDetailMap(){
        var objServiceArea = $.parseJSON($("#hiServiceArea").val());
        var $serviceMap = $("#serviceArea");
        var  map = new BMap.Map("serviceArea", {enableMapClick: false});
        var point = new  BMap.Point(objServiceArea.serPointCirle.lng, objServiceArea.serPointCirle.lat);
        var marker = new BMap.Marker(point); // 创建点
        var circle = new BMap.Circle(point,objServiceArea.serPointCirle.radius, {strokeColor:"blue", strokeWeight:2, strokeOpacity:0.5});
        map.disableDoubleClickZoom();
        map.disableDragging();

        map.centerAndZoom(point, 15);
        map.addOverlay(marker);
        map.addOverlay(circle);
    }
</script>
</asp:Content>

