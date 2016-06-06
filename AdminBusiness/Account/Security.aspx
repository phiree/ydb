<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Security.aspx.cs" Inherits="Account_Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="secret-layout-fixed">
        <div class="content">
            <input id="currentUserId" type="hidden" value="<%=CurrentUser.Id %>"/>
            <div class="content-head normal-head">
                <h3>账号安全</h3>
            </div>
            <div class="content-main">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-9">
                            <div class="model m-b20">
                                <div class="model-h">
                                    <h4>您的基础信息</h4>
                                </div>
                                <div class="model-m">
                                    <div class="secret-pra">
                                        <div><span class="secret-t">登录账号:</span><span class="secret-m"><%=CurrentUser.UserName%></span></div>
                                    </div>
                                    <div class="secret-pra">
                                        <div><span class="secret-t">手机号码:</span><span class="secret-m"><%=CurrentUser.Phone%></span><a id="lb_changePhone" class="m-l20 secret-btn" href="javascript:void(0);">修改</a></div>
                                        <div class="secret-tip"><span>（点击“修改”按钮可对手机号进行修改）</span></div>
                                    </div>
                                    <div class="secret-pra">
                                        <div><span class="secret-t">绑定邮箱:</span><span class="secret-m" id="currentUserEmail"><span class="m-r20"><%=CurrentUser.Email%></span><span id="currentUserEmailVali" class="secret-d-a d-inb dis-n"><% if (CurrentUser.IsRegisterValidated){%>已验证<%}else{%><span class="text-r m-r10">未验证</span><asp:Button runat="server" CssClass="reVali  secret-btn"  Text='发送验证链接'  ID='btnResendEmailVerify' OnClick="btnResendEmailVerify_Click"/><%}%></span></span><a id="lb_changeEmail" class="m-l20 secret-btn" href="javascript:void(0);">修改</a></div>
                                        <div class="secret-tip"><span>（点击“修改”按钮对邮箱进行修改，点击“重新发送连接”按钮重新发送连接）</span></div>
                                    </div>
                                    <div class="secret-pra">
                                        <div><span class="secret-t">上次登陆:</span><span class="secret-m text-blue"><%=CurrentUser.LastLoginTime.ToString("yyyy年MM月dd日 HH:mm:ss")%></span></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            <div class="model">
                                <div class="model-h">
                                    <h4>您的账安全信息</h4>
                                </div>
                                <div class="model-m">
                                    <div>
                                        <div><span class="secret-t">登陆密码&nbsp;: </span><span class="secret-m">****</span><a id="passChange" class="m-l20 secret-btn" href="javascript:void(0);">修改</a></div>
                                        <div class="secret-tip"><span>（点击“修改”按钮可对手机号进行修改）</span></div>
                                    </div>
                                    <!--<p class="secret-d-p d-inb">设置安全性高的密码可以使账号更安全，建议您定期更换密码，且设置一个包含数字和字母，并长度超过6位数的密码</p>-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <div id="lightBox" class="dis-n">
            <div class="model">
                <div class="model-h">
                    <span>修改密码</span> <i class="icon lightClose icon-close"></i>
                </div>
                <div class="model-m">
                    <div class="m-auto" ng-hide="!CPassSuccess">
                        <asp:ChangePassword ID="ChangePassword1" runat="server"
                                            ChangePasswordTitleText="修改密码"
                                            CssClass="CPBox" PasswordLabelText="旧密码"
                                            ConfirmNewPasswordLabelText="确认新密码"
                                            NewPasswordLabelText="新密码"

                                            ChangePasswordButtonText="确认"
                                            ChangePasswordButtonType="button"
                                            ChangePasswordButtonImageUrl="../image/myshop/shop_tx_107.png"
                                            ChangePasswordButtonStyle-CssClass="secret-btn"

                                            CancelButtonText="取消"
                                            CancelButtonType="button"
                                            CancelButtonImageUrl="../image/myshop/shop_tx_108.png"
                                            CancelButtonStyle-CssClass="m-l20 secret-btn"

                                            TitleTextStyle-CssClass="CPTitle"
                                            LabelStyle-CssClass="CPLabel"
                                            TextBoxStyle-CssClass="CPTextBox"
                                            ChangePasswordFailureText=""
                                            SuccessText="密码修改成功"
                                            SuccessPageUrl="./ChangePassword_suc.aspx"
                                            OnChangePasswordError="change_error"
                                            NewPasswordRegularExpression=".{6,}"
                                            NewPasswordRegularExpressionErrorMessage="密码至少6位数">
<CancelButtonStyle CssClass="m-l20 secret-btn"></CancelButtonStyle>

<ChangePasswordButtonStyle CssClass="secret-btn"></ChangePasswordButtonStyle>

                            <ChangePasswordTemplate>
                                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" style="line-height:1.6em">
                                                <tr>
                                                    <td align="right" class="CPLabel">
                                                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">旧密码</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="CPTextBox" TextMode="Password" style="margin-left: 5px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="必须填写“密码”。" ToolTip="必须填写“密码”。" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="CPLabel">
                                                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">新密码</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="NewPassword" runat="server" CssClass="CPTextBox" TextMode="Password" style="margin-left: 5px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="必须填写“新密码”。" ToolTip="必须填写“新密码”。" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="CPLabel">
                                                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">确认新密码</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="CPTextBox" TextMode="Password" style="margin-left: 5px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="必须填写“确认新密码”。" ToolTip="必须填写“确认新密码”。" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="“确认新密码”与“新密码”项必须匹配。" ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:RegularExpressionValidator ID="NewPasswordRegExp" runat="server" ControlToValidate="NewPassword" Display="Dynamic" ErrorMessage="密码至少6位数" ValidationExpression=".{6,}" ValidationGroup="ChangePassword1"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="color:Red;">
                                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" >
                                                        <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" CssClass="secret-btn" Text="确认" ValidationGroup="ChangePassword1" />
                                                        <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="m-l20 secret-btn" Text="取消" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ChangePasswordTemplate>

<LabelStyle CssClass="CPLabel"></LabelStyle>
                            <SuccessTemplate>
                                <div id="CPResult" class="m-auto" ng-hide="CPassSuccess">
                                    <div class="t-c">
                                        <i class="icon secret-icon-done"></i>
                                        <div class="secret-change-done d-inb">
                                            <p>
                                                恭喜你</p>
                                            <p>
                                                修改密码成功</p>
                                        </div>
                                    </div>
                                    <div class="secret-change-sub">
                                    <input class="lightClose secret-btn-done" ng-click="CPassReset()" type="button" value="确认" /></div>
                                </div>
                            </SuccessTemplate>

<TextBoxStyle CssClass="CPTextBox"></TextBoxStyle>

<TitleTextStyle CssClass="CPTitle"></TitleTextStyle>
                        </asp:ChangePassword>
                    </div>
                </div>
            </div>
        </div>
        <div id="lightBox_ChangeEmail" class="dis-n">
            <div class="model">
                <div class="model-h">
                    <span>修改邮箱</span> <i class="lightClose icon delete-icon secret-cancel-icon"></i>
                </div>
                <div class="model-m">
                    新邮箱:
                    <input type="text" id="tbxNewEmail" />
                    <input type="button" class="btnChange secret-btn" id="btnChangeEmail" change_field="email" value="保存" />
                </div>
            </div>
        </div>
        <div id="lightBox_ChangePhone" class="dis-n">
            <div class="model">
                <div class="model-h">
                    <span>修改电话</span> <i class="lightClose icon delete-icon secret-cancel-icon"></i>
                </div>
                <div class="model-m">
                    新电话:&nbsp;<input type="text" id="tbxNewPhone" />
                    <input type="button" class="btnChange secret-btn" id="btnChangePhone" change_field="phone" value="保存"  />
                </div>
            </div>
        </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" runat="Server">
    <script src="/js/plugins/jquery.lightbox_me.js"></script>
    <script src="/js/plugins/jquery.form.min.js"></script>
    <script>
    $(function(){
        $('.secret-layout-fixed').parent('.content-layout').css({marginLeft:0});

        $("#lb_changePhone").click(function (e) {
            $('#lightBox_ChangePhone').lightbox_me({
                centered: true
            }).appendTo($("form:first"));
        });

        $("#lb_changeEmail").click(function (e) {
            $('#lightBox_ChangeEmail').lightbox_me({
                centered: true
            }).appendTo($("form:first"));
        });

        $('#passChange').click(function (e) {
            $('#lightBox').lightbox_me({
                centered: true
            }).appendTo($("form:first"));
        });

    });
    </script>
    <script src="/js/apps/pages/security.js"></script>
</asp:Content>
