<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="DZOrder_Detail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <input type="hidden" value="<%=merchantID%>" id="merchantID"/>
        <div class="content-head normal-head">
            <h3>订单详情</h3>
            <a class="btn btn-gray-light fr" role="button" href="/dzorder/default.aspx?businessId=<%= CurrentBusiness.Id %>">返回</a>
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

                                                <span class="model-pra-t">服务项目</span><%=CurrentOrder.Details[0].ServiceSnapShot.ServiceName %>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单号</span><%=CurrentOrder.SerialNo %>
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">下单时间</span><%= CurrentOrder.OrderCreated %>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单状态</span><%= CurrentOrder.GetStatusTitleFriendly(CurrentOrder.OrderStatus) %>
                                            </p>

                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">服务时间</span><%= CurrentOrder.OrderServerStartTime %>
                                            </p>
                                            <p class="model-pra">
                                                <span class="model-pra-t">订单备注</span><%= CurrentOrder.Memo %>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="d-hr in"></div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="order-price">
                                                <span class="order-price-t">已付订金:</span><em class="order-price-m"><%= CurrentOrder.DepositAmount.ToString("f2") %>&nbsp;元</em>&nbsp;
                                                <span class="order-price-t">订单尾款:</span><em class="order-price-m"><%= (CurrentOrder.NegotiateAmount - CurrentOrder.DepositAmount).ToString("f2") %>&nbsp;元</em>&nbsp;
                                                <span class="order-total-price"><span class="order-price-t">订单总价:</span><em class="order-price-m"><%= CurrentOrder.NegotiateAmount.ToString("f2") %></em>&nbsp;元</span>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel runat="server" ID="panelOrderStatus" Visible="false">
                                    <div class="d-hr in"></div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="order-ctrl t-r">
                                                <!--<asp:HyperLink runat="server" ID="PayDepositAmount"></asp:HyperLink>-->
                                                <!-- 确认订单控制 -->
                                                <asp:Panel runat="server" ID="panelConfirmOrder" Visible="false">
                                                    <input class="btn btn-info btn-xs" type="button" value="指派员工" data-role="appointToggle" data-appointTargetId='<%= CurrentOrder.Id %>' >
                                                    <asp:Button runat="server" CommandName="ConfirmOrder" OnClick="btnOrderStatusChange_Click"   ID="btnConfirmOrder" CssClass="btn btn-info btn-xs" Visible="false" Text="确认订单"/>
                                                </asp:Panel>
                                                <!-- 确认价格控制 -->
                                                <asp:Panel runat="server" ID="panelConfirmPrice" Visible="false">
                                                    修改订单价格为：
                                                    <asp:TextBox runat="server" CommandName="txtConfirmPrice" class="order-confirm-txt" OnClick="btnOrderStatusChange_Click" ID="txtConfirmPrice" Width="100" Visible="false"></asp:TextBox>
                                                    元
                                                    <asp:Button runat="server" CommandName="ConfirmPrice" OnClick="btnOrderStatusChange_Click" ID="btnConfirmPrice" CssClass="btn btn-info btn-xs order-confirm-btn" Visible="false" Text="确认价格"/>
                                                </asp:Panel>
                                                <!--服务开始控制-->
                                                <asp:Button runat="server" CommandName="Assigned"  OnClick="btnOrderStatusChange_Click"  Visible="false"  ID="btnBegin" CssClass="btn btn-info btn-xs" Text="开始服务"/>
                                                <!--订单完成控制-->
                                                <asp:Button runat="server" CommandName="Begin"  OnClick="btnOrderStatusChange_Click"  Visible="false"  ID="btnIsEndOrder" CssClass="btn btn-info btn-xs" Text="完成订单"/>
                                            </div>
                                        </div>
                                    </div>
                                    </asp:Panel>
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
                                                <span class="model-pra-t">客户姓名</span><asp:Literal runat="server" ID="liCustomerName"></asp:Literal> 
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra">
                                                <span class="model-pra-t">联系方式</span> <asp:Literal runat="server" ID="liCustomerPhone"></asp:Literal> 
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="model m-b20 dis-n">
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
                                                <span class="model-pra-t">投诉理由</span>
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
                                        <asp:Repeater runat="server" ID="rptOrderDoneStatus" >
                                        <ItemTemplate>
                                            <div class="status-list-item">
                                                <div class="status-tip">
                                                    <div class="status-icon">
                                                        <i class="icon"></i>
                                                    </div>
                                                    <div class="status-time">
                                                        <%# Eval("CreatTime") %>
                                                    </div>
                                                </div>
                                                <div class="status-h"><%#Eval("NewStatusStr") %></div>
                                            </div>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="staffAppointLight" class="appointWindow dis-n">
        <div class="model">
            <div class="model-h">
                <h4>员工指派</h4>
            </div>
            <div class="model-m">
                <div class="row">
                    <div class="col-md-12">
                        <div id="staffsContainer" class="staffs-container">
                            <!-- 注入#staffs_temlate模版内容 -->
                        </div>
                    </div>
                </div>
            </div>
            <div class="model-b">
                <input id="appointSubmit" class="btn btn-info" type="button" value="确定指派" data-role="appointSubmit" >
                <input class="btn btn-cancel-light lightClose" type="button" value="取消" >
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
    <script type="text/template" id="staffs_template">
        <div class="col-md-4">
            <div class="emp-model">
                <div class="emp-model-h">
                    <span>员工编号:&nbsp;</span><span class="emp-code"></span>
                    <div class='emp-assign-flag'></div>
                </div>
                <div class="emp-model-m">
                    <img class="emp-headImg" src='{%= imgUrl %}'/>
                    <div class="emp-info">
                        <p>昵称：{%= alias %}</p>
                        <p>姓名：{%= realName %}</p>
                        <p>性别：{% sex ? "女" : "男" %}</p>
                        <p>电话：{%= phone %}</p>
                    </div>
                </div>
                <div class="emp-model-b" data-mark="{%= mark %}">
                    <input class="staffCheckbox" type="checkbox" {% if (mark) { %} checked {% } %} value="指派" data-role="item" data-itemId="{%= id %}" id="{%= id %}" >
                    <label for="{%= id %}"></label>
                </div>
            </div>
        </div>
    </script>
    <script src="/js/libs/underscore.js"></script>
    <script src="/js/mock/mock.js"></script>
    <script src="/js/plugins/jquery.lightbox_me.js"></script>
    <script src="/js/core/YDBan.lib.js?v=1.0.0"></script>
    <script src="/js/core/interfaceAdapter.js?v=1.0.0"></script>
    <script src="/js/core/RestfulProxyAdapter.js?v=1.0.0"></script>
    <script src="/js/apps/assignToOrder.js?v=1.0.0"></script>
    <!--<script src="/js/apps/appointToOrder.js?v=1.0.0"></script>-->
    <script src="/js/components/select.js?v=1.0.0"></script>
    <script>
        $(function(){
            $(".select").customSelect();
        });
    </script>
</asp:Content>