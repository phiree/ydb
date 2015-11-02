<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="Staff_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="/css/employee.css" rel="stylesheet" type="text/css"/>
    <link href="/css/validation.css" rel="stylesheet" type="text/css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="cont-wrap mh">
        <div class="cont-container animated fadeInUpSmall">
            <div class="cont-row emp-cont-row emp-mar-top">
                <div class="cont-col-2"><p class="cont-sub-title"></p></div>
                <div class="cont-col-10">
                    <div class="clearfix">
                        <div class="headImage">
                            <div class="input-file-box headFile">
                                <input type=file class="input-file-btn file-default headFileBtn" runat="server"
                                       id="empheadimg" name="empheadimg"/>
                                <i class="input-file-bg" style='background-image:url(<%=StaffAvatarUrl%>)'></i>
                                <i class="input-file-mark"></i>
                                <i class="input-file-hover">修改头像</i>
                                <img class="input-file-pre" src="..\image\00.png"/>
                            </div>
                        </div>
                    </div>


                </div>
            </div>

            <div class="cont-row emp-cont-row">
                <div class="cont-col-2">
                    <p class="cont-sub-title">编号</p>
                </div>
                <div class="cont-col-10">

                    <div class="clearfix">
                        <div>
                            <div>
                                <asp:TextBox ID="Code" CssClass="input-lg" runat="server" data-toggle="tooltip"
                                             data-placement="top" title="请填写员工编号"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>填写编号</p>-->
                </div>
            </div>

            <div class="cont-row emp-cont-row">
                <div class="cont-col-2">
                    <p class="cont-sub-title">姓名</p>
                </div>
                <div class="cont-col-10">

                    <div class="clearfix">
                        <div>
                            <div>
                                <asp:TextBox ID="Name" CssClass="input-lg" runat="server" data-toggle="tooltip"
                                             data-placement="top" title="请填写员工姓名"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>填写姓名</p>-->
                </div>
            </div>
            <div class="cont-row emp-cont-row">
                <div class="cont-col-2">
                    <p class="cont-sub-title">昵称</p>
                </div>
                <div class="cont-col-10">

                    <div class="clearfix">
                        <div>
                            <div>
                                <asp:TextBox ID="NickName" CssClass="input-lg" runat="server" data-toggle="tooltip"
                                             data-placement="top" title="请填写员工昵称"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>填写昵称</p>-->
                </div>
            </div>

            <div class="cont-row emp-cont-row">
                <div class="cont-col-2">
                    <p class="cont-sub-title">性别</p>
                </div>
                <div class="cont-col-10">
                    <div>
                        <div class="d-inb select select-sm sex-select">
                            <ul>
                                <li><a>男</a></li>
                                <li><a>女</a></li>
                            </ul>
                            <input type="hidden" class="input-lg" runat="server" id="hiGender" name="Gender1"/>
                        </div>

                    </div>
                    <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>选择你性别</p>-->

                </div>
            </div>
            <div class="cont-row emp-cont-bottom-row">
                <div class="cont-col-2">
                    <p class="cont-sub-title">联系电话</p>
                </div>
                <div class="cont-col-10">
                    <div class="clearfix">
                        <div>
                            <div>
                                <asp:TextBox ID="Phone" CssClass="input-lg" runat="server" data-toggle="tooltip"
                                             data-placement="top" title="请填写员工联系电话"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>填写联系电话</p>-->
                </div>
            </div>

        </div>
        <div class="bottomArea emp-cont-bottom-row">
            <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-info btn-big" OnClick="btnOK_Click"/>

            <a class="btn btn-cancel btn-big m-l10" href="default.aspx?businessid=<%=Request["businessid"] %>">取消</a>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" runat="server">
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
<script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/additional-methods.js" type="text/javascript"></script>
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
