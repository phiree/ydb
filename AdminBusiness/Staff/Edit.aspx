<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="Staff_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>员工管理</h3>
        </div>
        <div class="content-main">
            <div class="container-fuild animated fadeInUpSmall">
                <div class="row">
                    <div class="col-md-12">
                        <div class="model">
                            <div class="model-h">
                                <h4>员工资料修改</h4>
                            </div>
                            <div class="model-m">
                                <div class="col-md-4">
                                    <div class="emp-head-upload">
                                        <div class="emp-head-text">上传新头像</div>
                                        <div class="headImage">
                                            <div class="input-file-box headFile">
                                                <input type=file class="input-file-btn file-default headFileBtn" runat="server"
                                                       id="empheadimg" name="empheadimg"/>
                                                <i class="input-file-bg" style='background-image:url(<%=StaffAvatarUrl%>)'></i>
                                                <i class="input-file-mark"></i>
                                                <!--<i class="input-file-hover">修改头像</i>-->
                                                <img class="input-file-pre" src="../images/components/inputFile/input_transparent.png"/>
                                            </div>
                                        </div>
                                        <p class="img-tips">图片格式为PNG/JPG大小限制为2M一下</p>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="model-form model-form-noTop">
                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">员工昵称</div>
                                            <div class="col-md-8 model-input">
                                                <asp:TextBox ID="NickName" CssClass="input-fluid" runat="server" data-toggle="tooltip"
                                                             data-placement="top" title="请填写员工昵称"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">员工编号</div>
                                            <div class="col-md-8 model-input">
                                                <asp:TextBox ID="Code" CssClass="input-fluid" runat="server" data-toggle="tooltip"
                                                             data-placement="top" title="请填写员工编号"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">员工姓名</div>
                                            <div class="col-md-8 model-input">
                                                <asp:TextBox ID="Name" CssClass="input-fluid" runat="server" data-toggle="tooltip"
                                                             data-placement="top" title="请填写员工姓名"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row model-form-group">
                                            <div class="col-md-4 model-label">员工性别</div>
                                            <div class="col-md-8 model-input">
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
                                            <div class="col-md-4 model-label">联系电话</div>
                                            <div class="col-md-8 model-input">
                                                <asp:TextBox ID="Phone" CssClass="input-fluid" runat="server" data-toggle="tooltip"
                                                             data-placement="top" title="请填写员工联系电话"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="model-global-bottom">
                            <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-info btn-big" OnClick="btnOK_Click"/>
                            <a class="btn btn-cancel btn-big m-l10" href="default.aspx?businessid=<%=Request["businessid"] %>">取消</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" runat="server">
<script type="text/javascript" src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js"></script>
<script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/additional-methods.js" type="text/javascript"></script>
<script src="/js/jquery.form.min.js" type="text/javascript"></script>
<script type="text/javascript" src="/js/TabSelection.js"></script>
<script type="text/javascript" src="/js/imgLocalPrev.js" ></script>
<script type="text/javascript">
    var name_prefix = 'ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$';
</script>
<script src="/js/validation_emp_edit.js" type="text/javascript"></script>
<script src="/js/validation_invalidHandler.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(function () {
            $('[data-toggle="tooltip"]').tooltip(
                {
                delay: {show : 500, hide : 100},
                trigger: 'hover'
                }
            )
        });

        $($("form")[0]).validate(
                {
                    errorElement: "p",
                    errorPlacement: function (error, element) {
                        if ($(element).attr("id") == "ContentPlaceHolder1_Gender1") {
                            error.appendTo((element.parent()).parent());
                        } else {
                            error.appendTo(element.parent());
                        }
                    },
                    rules: service_validate_rules,
                    messages: service_validate_messages,
                    invalidHandler: invalidHandler
                }
            );
    });
    $(".headFileBtn").imgLocalPrev();
    </script>
</asp:Content>
