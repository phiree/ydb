<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="withdraw.aspx.cs" Inherits="Finance_withdraw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>支付账户设置</h3>
        </div>
        <div class="content-main">
            <div class="container-fuild animated fadeInUpSmall">
                <div class="row">
                    <div class="col-md-12">
                        <div class="model">
                            <div class="model-h">
                                <h4>绑定支付账户</h4>
                            </div>
                            <div class="model-m">
                                <div class="withDraw-ctnr">
                                    <div class="_w-heading">可提现金额</div>
                                    <div class="_w-count"><span class="_money">555555</span>元</div>
                                </div>
                                <div class="d-hr in"></div>
                                <div class="model-form model-form-noTop">
                                    <div class="row model-form-group">
                                        <div class="col-md-4 model-label">支付账户：</div>
                                        <div class="col-md-4 model-context">licdream@126.com</div>
                                    </div>
                                    <div class="row model-form-group">
                                        <div class="col-md-4 model-label">提现金额：</div>
                                        <div class="col-md-4 model-input-unit">
                                            <asp:TextBox ID="Code" CssClass="input-fluid" runat="server"
                                                         data-toggle="tooltip"
                                                         data-placement="top" title="请填写员工编号" onkeyup="this.value=this.value.replace(/\D/g,'')"  onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox><em class="unit">元</em>
                                        </div>
                                    </div>
                                </div>

                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="model-global-bottom">
                            <a class="btn btn-confirm btn-info m-l10" href='default.aspx?businessid=<%=Request["businessid"] %>'>保存</a>
                            <a class="btn btn-cancel btn-big m-l10" href='default.aspx?businessid=<%=Request["businessid"] %>'>取消</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" runat="Server"></asp:Content>
