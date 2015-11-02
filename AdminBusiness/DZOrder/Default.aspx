<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZOrder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/order.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cont-wrap">
        <div class="mh-in">
            <div class="cont-container animated fadeInUpSmall">
                <div class="mh-ctnr">

                    <div id="order-list" class="order-list dis-n">
                        <div  class="order-total">
                            <div class="cont-row">
                                <div class="cont-col-4"><i class="icon order-icon-status1"></i>未处理订单</div>
                                <div class="cont-col-4"><i class="icon order-icon-status2"></i>正在进行订单</div>
                                <div class="cont-col-4"><i class="icon order-icon-status3"></i>已处理订单</div>
                            </div>
                        </div>
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="cont-row order-head">
                                <div class="cont-col-2">订单号</div>
                                <div class="cont-col-2">服务项目</div>
                                <div class="cont-col-2">服务时间</div>
                                <div class="cont-col-1">客户名称</div>
                                <div class="cont-col-3">服务地址</div>
                                <div class="cont-col-1">订单状态</div>
                                <div class="cont-col-1">操作</div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="" >
                                    <table class="custom-table order-table" role="button" data-toggle="collapse" data-parent="#accordion"
                                           href="#collapse1" aria-expanded="true"
                                           aria-controls="collapse1" >
                                        <tbody>
                                        <tr>
                                            <td class="table-col-2">
                                                123132
                                            </td>
                                            <td class="table-col-2">

                                                修车
                                            </td>
                                            <td class="table-col-2">
                                                2015-09-10 18：00
                                            </td>
                                            <td class="table-col-1">
                                                王先生
                                            </td>
                                            <td class="table-col-3">
                                                海南省海口市国贸路28号
                                            </td>
                                            <td class="table-col-1 order-span-status">
                                                已完成
                                            </td>
                                            <td class="table-col-1">
                                                <span class="order-span-ctrl">详情</span>
                                            </td>
                                        </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div id="collapse1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading1">
                                    <div class="panel-body">
                                        <div class="order-status-detail">
                                            <div class="order-steps">
                                                <div class="order-status-step mr">
                                                    <i class="icon"></i>
                                                    <p class="order-status-text"></p>
                                                </div>
                                                <div class="order-status-step mr">
                                                    <i class="icon"></i>
                                                    <p class="order-status-text"></p>
                                                </div>
                                                <div class="order-status-step mr">
                                                    <i class="icon"></i>
                                                    <p class="order-status-text"></p>
                                                </div>
                                                <div class="order-status-step mr">
                                                    <i class="icon"></i>
                                                    <p class="order-status-text mr"></p>
                                                </div>
                                                <div class="order-status-step">
                                                    <i class="icon"></i>
                                                    <p class="order-status-text"></p>
                                                </div>
                                            </div>
                                            <div class="order-lines"></div>
                                            <input type="hidden" value="3">
                                        </div>
                                        <table class="custom-table panel-detail-table">
                                            <tbody>
                                            <tr>
                                                <td class="table-col-1 t-r">服务价钱：</td>
                                                <td class="table-col-3">10</td>
                                                <td class="table-col-1 t-r">下单时间：</td>
                                                <td class="table-col-3">2015-10-10 15:30</td>
                                                <td class="table-col-1 t-r">客户电话：</td>
                                                <td class="table-col-3">15555555555</td>
                                            </tr>
                                            <tr>
                                                <td class="table-col-1 t-r">指派员工：</td>
                                                <td class="table-col-3">某某某 某某某</td>
                                            </tr>
                                            <tr>
                                                <td class="table-col-1 t-r">备&nbsp;&nbsp;注：</td>
                                                <td class="table-col-3">222222222</td>
                                            </tr>
                                            </tbody>
                                        </table>
                                        <div class="order-ctrl t-r">
                                            <input type="button" class="btn btn-info" value="指派"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="2" >
                            <table class="custom-table order-table" role="button" data-toggle="collapse" data-parent="#accordion"
                                   href="#collapse2" aria-expanded="true"
                                   aria-controls="collapse2" >
                                <tbody>
                                <tr>
                                    <td class="table-col-2">
                                        123132
                                    </td>
                                    <td class="table-col-2">

                                        修车
                                    </td>
                                    <td class="table-col-2">
                                        2015-09-10 18：00
                                    </td>
                                    <td class="table-col-1">
                                        王先生
                                    </td>
                                    <td class="table-col-3">
                                        海南省海口市国贸路28号
                                    </td>
                                    <td class="table-col-1 order-span-status">
                                        已完成
                                    </td>
                                    <td class="table-col-1">
                                        <span class="order-span-ctrl">详情</span>
                                    </td>
                                </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="collapse2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading2">
                            <div class="panel-body">
                                <div class="order-status-detail">
                                    <div class="order-steps">
                                        <div class="order-status-step mr">
                                            <i class="icon"></i>
                                            <p class="order-status-text"></p>
                                        </div>
                                        <div class="order-status-step mr">
                                            <i class="icon"></i>
                                            <p class="order-status-text"></p>
                                        </div>
                                        <div class="order-status-step mr">
                                            <i class="icon"></i>
                                            <p class="order-status-text"></p>
                                        </div>
                                        <div class="order-status-step mr">
                                            <i class="icon"></i>
                                            <p class="order-status-text mr"></p>
                                        </div>
                                        <div class="order-status-step">
                                            <i class="icon"></i>
                                            <p class="order-status-text"></p>
                                        </div>
                                    </div>
                                    <div class="order-lines"></div>
                                    <input type="hidden" value="0   ">
                                </div>
                                <table class="custom-table panel-detail-table">
                                    <tbody>
                                    <tr>
                                        <td class="table-col-1 t-r">服务价钱：</td>
                                        <td class="table-col-3">10</td>
                                        <td class="table-col-1 t-r">下单时间：</td>
                                        <td class="table-col-3">2015-10-10 15:30</td>
                                        <td class="table-col-1 t-r">客户电话：</td>
                                        <td class="table-col-4">15555555555</td>
                                    </tr>
                                    <tr>
                                        <td class="table-col-1 t-r">指派员工：</td>
                                        <td class="table-col-3">某某某 某某某</td>
                                    </tr>
                                    <tr>
                                        <td class="table-col-1 t-r">备&nbsp;&nbsp;注：</td>
                                        <td class="table-col-4">222222222</td>
                                    </tr>
                                    </tbody>
                                </table>
                                <div class="order-ctrl t-r">
                                    <input type="button" class="btn btn-info" value="重新指派"/>
                                </div>
                            </div>
                        </div>
                    </div>
                        </div>
                    </div>
                    <div id="order-new" class="dis-n">
                        <div class="new-box">
                            <div class="t-c">
                                <img src="/image/service-new.png"/>
                            </div>
                            <div class="order-new-add">
                                <a id="firstAddBusiness" class="new-add-btn" href="javascript:void(0);">无订单内容</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
    <script>
        $(function(){
            var statusTextJson = [
                {
                    doneText: "已支付",
                    curText: "支付中",
                    waitText: "未支付"
                },
                {
                    doneText: "已支付已指派",
                    curText: "已支付未指派",
                    waitText: "未支付未指派"
                },
                {
                    doneText: "已指派",
                    curText: "指派中",
                    waitText: "未指派"
                },
                {
                    doneText: "已服务已收款",
                    curText: "已服务未收款",
                    waitText: "未服务未收款"
                },
                {
                    doneText: "已收款",
                    curText: "收款中",
                    waitText: "未收款"
                }
            ];

            $('.order-status-detail').each(function(){
                var $this = $(this);
                var currentStep = parseInt($this.find("input[type=hidden]").val());
                var step = $this.find('.order-status-step');
                var doneStep = (currentStep - 1 ) < 0 ? 0 : currentStep;

                step.eq(currentStep).addClass("cur-step").siblings().removeClass("cur-step");
                step.eq(currentStep).addClass("cur-step").siblings().removeClass("cur-step");

                step.eq(currentStep).find(".order-status-text").html(statusTextJson[0].curText);

                step.slice(0 , doneStep ).addClass("done-step");
                step.slice( doneStep , -1 ).removeClass("done-step");

                step.slice(0 , doneStep).each(function(index){
                    $(this).find('.order-status-text').html(statusTextJson[index].doneText);
                });
                step.slice(currentStep + 1).each(function(index){
                    $(this).find('.order-status-text').html(statusTextJson[currentStep + 1 + index ].waitText);
                })
            })

            if ( $("#accordion").children(".panel").length == 0 ){
                $("#order-new").removeClass("dis-n");
            } else {
                $("#order-list").removeClass("dis-n");
            }

//            $(".order-table tbody tr:even").addClass("list-item-odd");
        })
    </script>
</asp:Content>