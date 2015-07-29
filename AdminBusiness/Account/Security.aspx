<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Security.aspx.cs" Inherits="Account_Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/myshop.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent clearfix">
        <div class="leftContent" id="leftCont">
            <div>
                <ul>
                    <li>
                        <a href="/account/Default.aspx" target="_self">
                            <div class="side-btn-bg d-inb side-btn-myshop">
                                <h4 class="side-btn-t ">基本信息</h4>
                            </div>
                        </a>
                    </li>
                    <li>
                        <a href="/account/Security.aspx" target="_self">
                            <div class="side-btn-bg d-inb side-btn-secret">
                                <h4 class="side-btn-t ">帐号安全</h4>
                            </div>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div class="myshopInfoArea clearfix">
                <div class="myshopInfoTilte">
                    <h1>
                        商家基本信息</h1>
                </div>
                <div class="headInfoArea clearfix">
                    <div class="headImage">
                        <img src="../image/myshop/touxiangkuang_11.png" alt="头像" />
                    </div>
                    <div class="headInfo">
                        <h3 style="margin-top: 15px;">
                            <%=CurrentBusiness.Name %></h3>
                        <p class="InfoCompletetxt">
                            资料完成程度</p>
                        <div class="InfoPercentage">
                            <div class="InfoComplete">
                                <span class="progress" style="width: <%=CurrentBusiness.CompetePercent%>%"></span>
                            </div>
                            <span class="completePercentage">
                                <%=CurrentBusiness.CompetePercent%>%</span>
                        </div>
                    </div>
                    <div class="headEditImg">
                        <a href="javascript:void(0);" class="headEditBtn"></a>
                    </div>
                </div>
            </div>
            <div class="secret-title">
                <span>账号信息相关</span>
            </div>
            <div class="secret-cont">
                <div class="secret-m standard-info">
                    <div class="secret-m-title t-1">
                        基础信息</div>
                    <div class="secret-detail">
                        <div>
                            <span class="secret-d-t">登录账号&nbsp;:</span><span><%=CurrentUser.UserName%></span></div>
                        <div>
                            <span class="secret-d-t">手机号码&nbsp;:</span><p class="secret-d-a d-inb">
                                <%=CurrentUser.Phone%></p>
                            <a class="blue-a" href="default.aspx">更换号码</a> <a id="lb_changePhone" class="m-l20 blue-a"
                                href="javascript:void(0);">修改</a></div>
                        <div>
                            <span class="secret-d-t">绑定邮箱&nbsp;:</span><p class="secret-d-a d-inb">
                                <%=CurrentUser.Email%><% if (CurrentUser.IsRegisterValidated)
                                                         {%>
                                                         已验证
                                                         <%}
                                                         else
                                                         {%>
                                                         未验证<asp:Button runat="server"  Text='重新发送验证链接'  ID='btnResendEmailVerify' OnClick="btnResendEmailVerify_Click"/> 
                                                        <%}%>
                                                         </p>
                            <a class="blue-a" href="default.aspx">更换邮箱</a> <a id="lb_changeEmail" class="m-l20 blue-a"
                                href="javascript:void(0);">修改</a></div>
                        <div>
                            <span class="secret-d-t">上次登陆&nbsp;:</span><span><%=CurrentUser.LastLoginTime.ToString("yyyy年MM月dd日 HH:mm:ss")%></span>
                        </div>
                    </div>
                </div>
                <div class="secret-m secert-info">
                    <div class="secret-m-title t-2">
                        账户安全信息</div>
                    <div class="secret-detail">
                        <div class="clearfix">
                            <span class="secret-d-t">登陆密码&nbsp;:</span><span class="secret-d-p d-inb">安全性高的密码可以使账号更安全，建议您定期更换密码，且设置一个包含数字和字母，并长度超过6位数的密码</span>
                            <a id="passChange" class="m-l20 blue-a" href="javascript:void(0);">修改</a>
                            <div class="secret-d-tip">
                                <i class="icon secret-icon-1"></i>
                                <div>
                                    强</div>
                            </div>
                        </div>
                        <div class="clearfix">
                            <span class="secret-d-t">身份认证&nbsp;:</span><span class="secret-d-p d-inb">重新上传负责人证件照</span>
                            <a class="m-l20 blue-a" href="Default.aspx">修改</a> <a id="lb_ChangeIdCards" class="m-l20 blue-a"
                                href="javascript:void(0);">修改</a>
                            <div class="secret-d-tip">
                                <i class="icon secret-icon-2"></i>
                                <div>
                                    已认证</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="lightBox" class="dis-n">
            <div class="secret-change">
                <div class="secret-change-title">
                    <span>修改密码</span> <i class="icon close icon-close"></i>
                </div>
                <div class="secret-change-m">
                    <div class="m-auto" ng-hide="!CPassSuccess">
                        <asp:ChangePassword ID="ChangePassword1" runat="server" ChangePasswordTitleText="修改密码"
                            CssClass="CPBox" PasswordLabelText="旧密码" ConfirmNewPasswordLabelText="确认新密码"
                            NewPasswordLabelText="新密码" ChangePasswordButtonText="确认" ChangePasswordButtonType="Image"
                            ChangePasswordButtonImageUrl="../image/myshop/shop_tx_107.png" ChangePasswordButtonStyle-CssClass="p-20"
                            CancelButtonText="取消" CancelButtonType="Image" CancelButtonImageUrl="../image/myshop/shop_tx_108.png"
                            CancelButtonStyle-CssClass="p-20" TitleTextStyle-CssClass="CPTitle" LabelStyle-CssClass="CPLabel"
                            TextBoxStyle-CssClass="CPTextBox" ChangePasswordFailureText="" SuccessText="密码修改成功"
                            SuccessPageUrl="./ChangePassword_suc.aspx" OnChangePasswordError="change_error"
                            NewPasswordRegularExpression=".{6,}" NewPasswordRegularExpressionErrorMessage="密码至少6位数">
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
                                        <input class="close secret-btn-done" ng-click="CPassReset()" type="button" value="确认" /></div>
                                </div>
                            </SuccessTemplate>
                        </asp:ChangePassword>
                    </div>
                </div>
            </div>
        </div>
        <div id="lightBox_ChangeEmail" class="dis-n">
            <div class="secret-change">
                <div class="secret-change-title">
                    <span>修改邮箱</span> <i class="icon close icon-close"></i>
                </div>
                <div class="secret-change-m">
                    原邮箱:<%=CurrentUser.Email %>
                    新邮箱:
                    <input type="text" id="tbxNewEmail" />
                    <input type="button" class="btnChange" id="btnChangeEmail" change_field="email" value="保存" />
                </div>
            </div>
        </div>
        <div id="lightBox_ChangePhone" class="dis-n">
            <div class="secret-change">
                <div class="secret-change-title">
                    <span>修改电话</span> <i class="icon close icon-close"></i>
                </div>
                <div class="secret-change-m">
                    原电话:<%=CurrentUser.Phone %> 
                    
                    新电话:<input type="text" id="tbxNewPhone" />
                    <input type="button" class="btnChange" id="btnChangePhone" change_field="phone" value="保存" />
                </div>
            </div>
        </div>
        <div id="lightBox_ChangeIdCards" class="dis-n">
            <div class="secret-change">
                <div class="secret-change-title">
                    <span>修改证件</span> <i class="icon close icon-close"></i>
                </div>
                <div class="secret-change-m">
                    <div class="myshopRightCont HeadProfilePicture">
                        <p class="myshop-item-title">
                            <i class="icon myshop-icon-ownerPic"></i>负责人证件照上传</p>
                        <div class="clearfix">
                            <asp:Repeater runat="server" ID="rptChargePersonIdCards" OnItemCommand="rptChargePersonIdCards_ItemCommand">
                                <ItemTemplate>
                                    <div class="download-img-pre fl">
                                        <asp:Button Text=" X " ID="ibCharge" OnClientClick="javascript:return confirm('确定删除?')"
                                            CssClass="download-img-delete" runat="server" CommandName="delete" ImageUrl="/image/myshop/shop_icon_91.png"
                                            ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                        <a class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                            <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                class="imgCharge" />
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="download-img-pre fl">
                            </div>
                            <div class="input-file-box fl">
                                <input type="file" class="input-file-btn" businessid="<%=CurrentBusiness.Id %>" imagetype="businesschargeperson" />
                                <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                <img class="input-file-pre" src="..\image\00.png" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" runat="Server">
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script src="/js/jquery.form.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://apps.bdimg.com/libs/angular.js/1.2.5/angular.min.js"></script>
    <script type="text/javascript" src="/js/global.js"></script>
    <script src="/js/FileUpload.js" type="text/javascript"></script>
    <script type="text/javascript">
      
        
        $("#lb_changePhone").click(function (e) {
            $('#lightBox_ChangePhone').lightbox_me({
                centered: true
            });
            $("#lightBox_ChangePhone").appendTo($("form:first"));
        });
       


        $("#lb_changeEmail").click(function (e) {
            $('#lightBox_ChangeEmail').lightbox_me({
                centered: true
            });
            $("#lightBox_ChangeEmail").appendTo($("form:first"));
        });
      



        $("#lb_ChangeIdCards").click(function (e) {
            $('#lightBox_ChangeIdCards').lightbox_me({
                centered: true
            });
            $("#lightBox_ChangeIdCards").appendTo($("form"));
        });
      
    



        $('#passChange').click(function (e) {
            $('#lightBox').lightbox_me({
                centered: true
            });
            $("#lightBox").appendTo($("form:first"));
            });
          



            $(".btnChange").click(function () {
                var change_field = $(this).attr("change_field");
                var newValue = "";
                switch (change_field) {
                    case "phone":
                        newValue = $("#tbxNewPhone").val();
                        break;
                    case "email":
                        newValue = $("#tbxNewEmail").val();
                        break;
                }
                var changed_data = {};
                changed_data["id"] = "<%=CurrentUser.Id %>";
                changed_data["changed_field"] = change_field;
                changed_data["changed_value"] = newValue;

                $.post("/ajaxservice/changebusinessInfo.ashx", changed_data,
                        function (result) {

                            if (result.result == "True") {

                                $("#tbxNewPhone").val("");
                                $("#tbxNewEmail").val("");
                                alert("修改成功");
                            }
                            else {
                                alert(result.msg);
                            }

                        }); //post
            });     //click
    </script>
</asp:Content>
