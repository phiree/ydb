<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="withdraw.aspx.cs" Inherits="Finance_withdraw" %>
   <%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-head normal-head">
        <h3>财务管理</h3>
    </div>
    <div class="content-main">
        <div class="animated fadeInUpSmall">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="model m-b20">
                            <div class="model-h">
                                <h4>提现金额</h4>
                            </div>
                            <div class="model-m">
                                <div class="row">
                                    <div class="col-md-12">
                                        提现金额 <input class="input-sm" id="withdraw" type="text">&nbsp;元
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="model">
                            <div class="model-h">
                                <h4>选择提现方式</h4>
                            </div>
                            <div class="model-m">
                                <div class="recharge-meths">
                                    <div class="_method">
                                        <input id="AliPay" name="RechargeMethod" type="radio" title="支付宝" value="1">
                                        <label for="AliPay">支付宝</label>
                                    </div>
                                    <div class="_method">
                                        <input id="WeiPay" name="RechargeMethod" type="radio" title="微支付" value="2">
                                        <label for="WeiPay">微支付</label>
                                    </div>
                                    <div class="_method">
                                        <input id="BindCard" name="RechargeMethod" type="radio" title="已绑定银行卡"
                                               value="3">
                                        <label for="BindCard">已绑定银行卡</label>
                                        <div class="bind-cards" id="bindCardBox">
                                            <div class="_card_w">
                                                <input id="bindABC" class="_card_radio" name="bindCard" type="radio" />
                                                <label for="bindABC" class="_card">
                                                    <i class="_icon"></i>
                                                    <div class="_info">
                                                        <p>中国农业银行</p>
                                                        <p>储蓄卡</p>
                                                        <p>**** **** **** 7321</p>
                                                    </div>
                                                </label>
                                            </div>
                                            <div class="_card_w">
                                                <input id="bindCCB" class="_card_radio" name="bindCard" type="radio" />
                                                <label for="bindCCB" class="_card">
                                                    <i class="_icon"></i>
                                                    <div class="_info">
                                                        <p>中国建设银行</p>
                                                        <p>储蓄卡</p>
                                                        <p>**** **** **** 7321</p>
                                                    </div>
                                                </label>
                                            </div>
                                            <div class="_card _more">
                                                <a class="btn btn-gray-light" href="">+&nbsp;绑定新银行卡</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="model-global-bottom">
                            <a class="btn btn-info" value="下一步">下一步</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bottom" runat="server">
</asp:Content>