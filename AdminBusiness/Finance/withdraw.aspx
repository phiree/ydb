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
                                    <div class="_w-count">
                                        <asp:Label  class="_money" ID="lblTotalAmount" runat="server" Text="555555"></asp:Label>元。注：每次提现至少提1.1元；体现手续费率为0.005，至少1元，最多25元</div>
                                </div>
                                <div class="d-hr in"></div>
                                <div class="model-form model-form-noTop">
                                    <div class="row model-form-group">
                                        <div class="col-md-4 model-label">支付账户：</div>
                                        <div class="col-md-4 model-context">
                                            <asp:Label ID="lblAccount" runat="server" Text="licdream@126.com"></asp:Label>
                                            <asp:Label ID="lblAccountType" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblBusinessId" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row model-form-group">
                                        <div class="col-md-4 model-label">提现金额：</div>
                                        <div class="col-md-4 model-input-unit">
                                            <asp:TextBox ID="txtAmount" CssClass="input-fluid" runat="server"
                                                         data-toggle="tooltip"
                                                         data-placement="top" title="请填写提现金额" onkeyup="this.value=this.value.replace(/\D/g,'')"  onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox><em class="unit">元</em>
                                        </div>
                                    </div>
                                </div>

                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="model-global-bottom">
                            <asp:Button ID="btnSave" runat="server" Text="保存" class="btn btn-confirm btn-info m-l10" OnClick="btnSave_Click"  />
                            <asp:Button ID="btnConcel" runat="server" Text="取消" class="btn btn-confirm btn-info m-l10" OnClick="btnConcel_Click"  />
                            <%--<a class="btn btn-confirm btn-info m-l10" href='default.aspx?businessid=<%=Request["businessid"] %>'>保存</a>
                            <a class="btn btn-cancel btn-big m-l10" href='default.aspx?businessid=<%=Request["businessid"] %>'>取消</a>--%>
                        </div>
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
