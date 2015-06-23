<%@ Page Title="" Language="C#" MasterPageFile="~/m/m.master" AutoEventWireup="true"
    CodeFile="~/register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/login.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div data-role="page" id="regpage" data-theme="mya" data-title="会员注册" style="background: none;">
        <div class="main-content">
            <div class="main-logo">
                <div class="logo-inco">
                    <img src="images/logo-2.png" width="100%" /></div>
                <p style="text-align: center;">
                    欢迎来到点助商户会员注册页面开启移动智能O2O新历程</p>
            </div>
            <br />
            <label for="iphone-tyle">
                2种注册方式</label>
            <select name="iphone-tyle" id="iphone-tyle" onchange="getSelectVal()" data-theme="a">
                <option value="iphone-num">手机号码</option>
                <option value="email">电子邮箱</option>
            </select>
            <div id="iphone-div">
                <label for="iphone">
                    输入号码：</label>
                <div style="position: relative;">
                    <input type="text" class="username" name="iphone" id="iphone" data-inline="true">
                    <span style="position: absolute; top: 10px; left: 8px; color: #333;">+86</span>
                </div>
            </div>
            <div id="email-div">
                <label for="email">
                    输入邮箱：</label>
                <div style="position: relative;">
                    <input type="text" class="username" name="email" id="email" data-inline="true">
                </div>
            </div>
            <div style="display:none">
           <asp:TextBox runat="server" ID="tbxUserName" ClientIDMode="Static"></asp:TextBox></div>
            <div style="width: 100%; height: 60.375px; line-height: 60.375px; position: relative;
                top: 20px;">
                <input type="checkbox" name="agree" id="agree" value="agree" checked data-inline="true" />
                <span style="left: 30px; top: -32px; position: absolute; font-size: 12px;">我已经仔细阅读过点助服务协议,并同意所有条款。
                </span>
            </div>
            <a data-role="button" href="#okpage" id="btn_next" disabled="disabled" data-transition="slideup">
                下一步</a>
            <br />
            <a href="#loginpage" data-transition="slidedown" class="my-a-2">返回登录页</a>
        </div>
    </div>
    <div data-role="page" style="background: none;" id="okpage" data-theme="mya" data-title="会员注册密码确认">
        <div class="main-content">
            <div class="main-logo">
                <div class="logo-inco">
                    <img src="images/logo-2.png" width="100%" /></div>
                <p style="text-align: center;">
                    欢迎来到点助商户会员注册页面开启移动智能O2O新历程</p>
            </div>
            <br />
            <p>
                登录名 <span id="rs_username"></span></p>
            <p>
                设置登录密码 此密码可以登录点助</p>
            <form method="post" action="" enctype="multipart/form-data" onsubmit="goRegOk()">
            <label for="login-pwd">
                登录密码：</label>
            <asp:TextBox runat="server" ClientIDMode="Static" ID="regPs" TextMode="Password"></asp:TextBox>
            <label for="login-rpwd">
                确认密码：</label>
            <asp:TextBox runat="server" ClientIDMode="Static" ID="regPsConf" TextMode="Password"></asp:TextBox>
            <br />
             <asp:Button runat="server" ID="regPsSubmit" ClientIDMode="Static" OnClick="regPsSubmit_OnClick"
                                   Text="确定"      CssClass="regBtn" />
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" runat="Server">
    <script type="text/javascript">

        function getSelectVal() {
            var txt = $("#iphone-tyle  option:selected").text();
            if (txt == "电子邮箱") {
                $("#email-div").css("display", "block");
                $("#iphone-div").css("display", "none");
            } else {
                $("#email-div").css("display", "none");
                $("#iphone-div").css("display", "block");
            }

        }
        $(document).ready(function () {
            $("#email-div").css("display", "none");
            $("#iphone-div").css("display", "block");

            //检测重复用户名
            $('.username').blur(function () {
                var that = this;
                var username = $(that).val();
                if (username == "") return;
                $.ajax({
                    type: "get",
                    url: "/ajaxservice/is_username_duplicate.ashx?username=" + username,
                    success: function (result) {
                        if (result == "Y") {
                            //如果同名,阻止跳转下一步
                            $("#btn_next").attr("href", "#");
                            $(that).parent().append("<span>重名</span>");
                            
                        }
                        else {
                            $("#tbxUserName").val(username);
                            $("#rs_username").text(username);
                            $("#btn_next").attr("href", "#okpage");
                        }
                    },
                    async: false
                });

            })

        })
    </script>
</asp:Content>
