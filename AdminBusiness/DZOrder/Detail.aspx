<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZOrder_Detail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>订单详情</h3>
            <a class="btn btn-gray-light fr" role="button" href="/dzorder/default.aspx?">返回</a>
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
                                        <div class="col-md-12">
                                            <div class="order-ctrl t-r">    
                                                <asp:HyperLink runat="server" ID="PayDepositAmount"></asp:HyperLink>
                                                <asp:Button runat="server" CommandName="ConfirmOrder" CommandArgument='<%#Eval("Id") %>' ID="btnConfimOrder" CssClass="btn btn-info btn-xs" Text="确认订单"/>
                                                <asp:TextBox runat="server" CommandName="txtConfirmPrice" CommandArgument='<%#Eval("Id") %>' ID="txtConfirmPrice" Width="100"></asp:TextBox>
                                                <asp:Button runat="server" CommandName="ConfirmPrice" CommandArgument='<%#Eval("Id") %>' ID="btnConfirmPrice" CssClass="btn btn-info btn-xs" Text="确认价格"/>-
                                                <asp:Button runat="server" CommandName="ConfirmPriceCustomer" CommandArgument='<%#Eval("Id") %>' ID="btnConfirmPriceCustomer" CssClass="btn btn-info btn-xs" Text="用户确认价格并开始服务"/>
                                                <asp:Button runat="server" CommandName="IsEndOrder" CommandArgument='<%#Eval("Id") %>' ID="btnIsEndOrder" CssClass="btn btn-info btn-xs" Text="订单完成"/>
                                                <asp:Button runat="server" CommandName="IsEndOrderCustomer" CommandArgument='<%#Eval("Id") %>' ID="btnIsEndOrderCustomer" CssClass="btn btn-info btn-xs" Text="用户确认订单完成"/>
                                                <asp:HyperLink runat="server" ID="PayFinalPayment"></asp:HyperLink>
                                                <asp:Button runat="server" ID="Button3" CssClass="btn btn-info btn-xs" Text="指派"/>
                                            </div>
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
                                            <div class="model-pra no-wrap">
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
                                            <div class="model-pra no-wrap">
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
                                    <div class="order-status-list">
                                        <div class="status-list-item">
                                            <div class="status-tip">
                                                <div class="status-icon">
                                                    <i class="icon"></i>
                                                </div>
                                                <div class="status-time">
                                                    12月8日 11：25
                                                </div>
                                            </div>
                                            <div class="status-h">订单已提交</div>
                                            <div class="status-p">用户已经提交订单，等待商户接单。</div>

                                        </div>
                                        <div class="status-list-item">
                                            <div class="status-tip">
                                                <div class="status-icon">

                                                </div>
                                                <div class="status-time">
                                                    12月8日 11：25
                                                </div>
                                            </div>
                                            <div class="status-h">订单已提交</div>
                                            <div class="status-p">用户已经提交订单，等待商户接单。</div>

                                        </div>
                                        <div class="status-list-item">
                                            <div class="status-tip">
                                                <div class="status-icon">

                                                </div>
                                                <div class="status-time">
                                                    12月8日 11：25
                                                </div>
                                            </div>
                                            <div class="status-h">订单已提交</div>
                                            <div class="status-p">用户已经提交订单，等待商户接单。</div>

                                        </div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>