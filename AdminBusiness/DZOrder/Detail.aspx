<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZOrder_Detail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>订单详情</h3>
        </div>
        <div class="content-main">
            <div class="animated fadeInUpSmall">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-9">
                            <div class="model m-b20">
                                <div class="model-h">
                                    <h4>订单信息</h4>
                                </div>
                                <div class="model-m">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务项目</span>90
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单号</span>10%
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单属性</span>10%
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">下单时间</span>预定
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单状态</span>先行
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">已付定金</span>先行
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务时间</span>www.ileechee.com
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单评分</span>www.ileechee.com
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单价格</span>www.ileechee.com
                                            </p>
                                        </div>
                                    </div>
                                    <div class="d-hr"></div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">离店时间</span>90
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">入住时间</span>预定
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">房间数</span>www.ileechee.com
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">房间数</span>www.ileechee.com
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="model m-b20">
                                <div class="model-h">
                                    <h4>客户信息</h4>
                                </div>
                                <div class="model-m">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">客户姓名</span>www.ileechee.com
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">联系方式</span>10%
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">身份信息</span>预定
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">备注</span>先付型服务
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="model m-b20">
                                <div class="model-h">
                                    <h4>订单投诉</h4>
                                </div>
                                <div class="model-m">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <p class="model-pra">
                                                <span class="model-pra-t">投诉状态</span>

                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">投诉理由</span>10%
                                            </p>
                                        </div>
                                    </div>
                                    <div class="d-hr"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="model-pra">
                                                <span class="model-pra-t">投诉处理</span>
                                                <div class="model-select-sm">
                                                    <div class="select select-fluid select-flow">
                                                        <ul>
                                                            <li><a>退款</a></li>
                                                            <li><a>不退款</a></li>
                                                        </ul>
                                                        <input type="hidden"  value="0"  />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="model-pra">
                                                <span class="model-pra-t">退款比例</span>
                                                <div class="model-select-sm">
                                                    <div class="select select-fluid select-flow">
                                                        <ul>
                                                            <li><a>10%</a></li>
                                                            <li><a>20%</a></li>
                                                            <li><a>30%</a></li>
                                                            <li><a>40%</a></li>
                                                            <li><a>50%</a></li>
                                                            <li><a>60%</a></li>
                                                            <li><a>70%</a></li>
                                                            <li><a>80%</a></li>
                                                            <li><a>90%</a></li>
                                                            <li><a>100%</a></li>
                                                        </ul>
                                                        <input type="hidden" value="0"  />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="t-r">
                                                <input class="btn btn-info-light btn-xs" type="button" value="确定">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="model">
                                <div class="model-h">
                                    <h4>订单状态</h4>
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
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>