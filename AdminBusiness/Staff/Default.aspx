<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Staff_Default" %>
   <%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/rotating-card.css" rel="stylesheet" type="text/css" />
    <link href="/css/employee.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="cont-wrap">
        <div class="mh-in">
            <div class="cont-container animated fadeInUpSmall">
                <div class="mh-ctnr">
                    <div class="dis-n" id="emp-list">
                        <div class="cont-row">
                            <div class="cont-col-12 m-b20">
                                <a class="btn btn-info" href="Edit.aspx?businessid=<%=Request["businessid"] %>"><span class="add-inco">+</span>添加新员工</a>
                            </div>
                        </div>
                        <div class="cont-row">
                            <asp:Repeater runat="server" ID="rptStaff" OnItemCommand="rptStaff_ItemCommand">
                                <ItemTemplate>
                                    <div class="cont-col-3">
                                        <div class="card-container manual-flip">
                                            <div class="card">
                                                <div class="card-front">
                                                    <div class="card-cover">
                                                        <img src="/image/employee/rotating_card_thumb.png"/>
                                                    </div>
                                                    <div class="card-user" >
                                                        <img class="img-circle" src=' <%# ((Dianzhu.Model.BusinessImage)Eval("AvatarCurrent")) == null ? "/image/emp-headinco.png" : "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(((Dianzhu.Model.BusinessImage)Eval("AvatarCurrent")).ImageName) + "&width=120&height=120&tt=3)"%>  '/>
                                                    </div>
                                                    <div class="card-content">
                                                        <div class="card-main">
                                                            <h3 class="card-name"><%#Eval("Name") %></h3>
                                                            <p class="card-profession"><%#Eval("Code") %></p>
                                                            <h5>姓名：<%#Eval("NickName") %></h5>
                                                            <h5>性别：<%#Eval("Gender")%></h5>
                                                            <h5>电话：<%#Eval("Phone")%></h5>
                                                            <div class='png-assign <%# (bool)Eval("IsAssigned")?"assigned":"noassign" %>'></div>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                        <div class="cont-row">
                                                            <div class="cont-col-6">
                                                                <input type="button" staffid='<%#Eval("id") %>' class='btnAssign btn <%# (bool)Eval("IsAssigned")?" btn-text-cancel":" btn-text-info" %>' value='<%# (bool)Eval("IsAssigned")?"取消指派":"指派" %>'/>
                                                            </div>
                                                            <div class="cont-col-6">
                                                                <a class="btn btn-text-info btn-flip">
                                                                    详情 <i class="glyphicon glyphicon-share-alt"></i>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div> <!-- end front panel -->
                                                <div class="card-back">
                                                    <div class="card-header">
                                                        <h5 class="card-motto">员工详情</h5>
                                                    </div>
                                                    <div class="card-content">
                                                        <div class="card-main">
                                                            <h4 class="text-center text-remark">备注</h4>
                                                            <p></p>
                                                        </div>
                                                    </div>
                                                    <div class="card-footer">
                                                        <div class="cont-row">
                                                            <div class="cont-col-4">
                                                                <div class="btn btn-text-cancel btn-flip">
                                                                    <i class="glyphicon glyphicon-arrow-left"></i> 返回
                                                                </div>
                                                            </div>
                                                            <div class="cont-col-4" >
                                                                <div class="btn btn-text-info" onclick='listHref("edit.aspx?id=<%#Eval("id") %>&businessid=<%=Request["businessid"] %>")'>
                                                                <i class="fa fa-reply"></i> 编辑
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col-4">
                                                            <asp:Button runat="server" CssClass="btn btn-text-delete"  CommandArgument='<%#Eval("Id") %>' OnClientClick="javascript:return confirm('确认删除该员工?');" CommandName="delete" Text="删除"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div> <!-- end back panel -->
                                        </div> <!-- end card -->
                                    </div> <!-- end card-container -->
                        </ItemTemplate>
                        </asp:Repeater>
                        <div class="pageNum">
                            <UC:AspNetPager runat="server" FirstPageText="首页" NextPageText="下一页" PrevPageText="上一页" id="pager" PageSize="10" UrlPaging="true" LastPageText="尾页"></UC:AspNetPager>
                        </div>
                    </div>
                </div>
                <div class="dis-n" id="add-view-emp">
                    <div class="new-box">
                        <div class="t-c">
                            <img src="/image/service-new.png"/>
                        </div>
                        <div class="service-new-add">
                            <a class="new-add-btn" href="Edit.aspx?businessid=<%=Request["businessid"] %>">添加新员工</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <asp:GridView runat="server" ID="gvStaff">
        <Columns>
            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" />
        </Columns>
    </asp:GridView>
        <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
        <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
        <script type="text/javascript" src="/js/TabSelection.js" ></script>
        <script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
        <script type="text/javascript">
            function listHref(url) {
//                var eve = window.event || arguments.callee.caller.arguments[0];
//                var $target = $(eve.target);
//                if ($target.hasClass("btn")) {
//                    return false
//                } else if (eve.target == eve.target) {
                    window.location.href = url;
//                };
            }

            $().ready(function(){
                $('[rel="tooltip"]').tooltip();

            });

            $(".btn-flip").bind("click",function(){
                var $this = $(this);
                var $card = $this.parents(".card-container");
                var $cardBack = $card.find(".card-back");
                if ($card.hasClass('hover')) {
                    $cardBack.removeClass('card-back-hover');
                    $card.removeClass('hover')
                } else {
                    $cardBack.addClass('card-back-hover');
                    $card.addClass('hover')
                }
                return false;
            });

            $(function () {

                if ($("#emp-list").find(".card").length == 0) {
                    $("#add-view-emp").removeClass("dis-n");
                } else {
                    $("#emp-list").removeClass("dis-n");
                }


                /* 当鼠标移到表格上是，当前一行背景变色 */
                $(".emp-table tr td").mouseover(function () {
                    $(this).parent().find("td").css("background-color", "#b0d3f5");
                });
                /* 当鼠标在表格上移动时，离开的那一行背景恢复 */
                $(".emp-table tr td").mouseout(function () {
                    var bgc = $(this).parent().attr("bg");
                    $(this).parent().find("td").css("background-color", bgc);
                });
                var color = "#f1f4f7"
                $(".emp-table tr:odd td").css("background-color", color);  //改变奇数行背景色
                /* 把背景色保存到属性中 */
                $(".emp-table tr:odd").attr("bg", color);
                $(".emp-table tr:even").attr("bg", "#fff");

            });

            $(".btnAssign").click(function () {
                var $this = $(this);
                $.post("/ajaxservice/changestaffInfo.ashx",
                    {
                        "changed_field": "assign",
                        "changed_value": false,
                        "id": $this.attr("staffid")
                    }, function (data) {
                        var enabled = data.data;
                        var $card = $this.parents(".card-container");
                        var $assign = $card.find(".png-assign");
                        if (enabled == "True") {
                            $this.siblings("span").html("已指派");
                            $this.val("取消指派");
                            $this.addClass("btn-text-cancel").removeClass("btn-text-info");
                            $assign.addClass("assigned").removeClass("noassign");
                        }
                        else {
                            $this.siblings("span").html("未指派");
                            $this.val("指派");
                            $this.addClass("btn-text-info").removeClass("btn-text-cancel");
                            $assign.removeClass("assigned").addClass("noassign");
                        }

                    });
            });


        </script>

</asp:Content>
