<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="ThirdParty_Edit.aspx.cs" Inherits="Finance_ThirdParty" %>

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
                                    <div class="model-form model-form-noTop">
                                        <div class="row model-form-group no-p-b">
                                            <div class="col-md-4 model-label">支付宝账号</div>
                                            <div class="col-md-4 model-context">licdream@126.com</div>
                                        </div>
                                        <div class="d-hr in"></div>

                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">联系电话</div>
                                            <div class="col-md-4 model-input">
                                                <asp:TextBox ID="Code" CssClass="input-fluid" runat="server" data-toggle="tooltip"
                                                             data-placement="top" title="请填写员工编号"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">身份证</div>
                                            <div class="col-md-4 model-input">
                                                <asp:TextBox ID="Name" CssClass="input-fluid" runat="server" data-toggle="tooltip"
                                                             data-placement="top" title="请填写员工姓名"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="d-hr in"></div>
                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">支付宝账号</div>
                                            <div class="col-md-4 model-input">
                                                <div class="select select-fluid">
                                                    <ul>
                                                        <li><a>男</a></li>
                                                        <li><a>女</a></li>
                                                    </ul>
                                                     <input type="hidden" class="input-fluid" runat="server" id="hiGender" name="Gender1"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">支付宝姓名</div>
                                            <div class="col-md-4 model-input">
                                                <asp:TextBox ID="Phone" CssClass="input-fluid" runat="server" data-toggle="tooltip"
                                                             data-placement="top" title="请填写员工联系电话"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="model-global-bottom">
                            <a class="btn btn-confirm btn-info m-l10" href="default.aspx?businessid=<%=Request["businessid"] %>">保存</a>
                            <a class="btn btn-cancel btn-big m-l10" href="default.aspx?businessid=<%=Request["businessid"] %>">取消</a>
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