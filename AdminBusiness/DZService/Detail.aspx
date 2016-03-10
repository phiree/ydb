<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZService_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/service.css" rel="stylesheet" type="text/css" />
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
                                                <span class="model-pra-t">服务名称</span>www.ileechee.com
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">先付定金</span>10%
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务类型</span>预定
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">支付要求</span>先付型服务
                                            </p>
                                        </div>
                                    </div>
                                    <div class="d-hr"></div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务介绍</span>www.ileechee.com
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
                                                <span class="model-pra-t">服务起步价</span>90
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务对象</span>10%
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">是否上门</span>10%
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务单价</span>预定
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务保障</span>先行
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">每日最大接单量</span>先行
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">付款方式</span>www.ileechee.com
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">提前预约时间</span>www.ileechee.com
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

