<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Staff_Default" %>
   <%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="UC" %>
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
            <div id="employee-list" class="animated fadeInUpSmall">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Repeater runat="server" ID="rptStaff" OnItemCommand="rptStaff_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                        <div class="emp-model">
                                            <div class="emp-model-h">
                                                <span>员工编号:&nbsp;</span><span class="emp-code"><%#Eval("Code") %></span>
                                                <div class='emp-assign-flag <%# (bool)Eval("IsAssigned")?"assigned":"noAssign" %>'></div>
                                                <asp:Button runat="server" CssClass="emp-ctrl delete-icon"  CommandArgument='<%#Eval("Id") %>' OnClientClick="javascript:return confirm('确认删除该员工?');" CommandName="delete" title="删除"/>
                                                <a class="emp-ctrl edit-icon" href='edit.aspx?id=<%# Eval("id") %>&businessid=<%=Request["businessid"] %>' title="编辑" ></a>
                                            </div>
                                            <div class="emp-model-m">
                                                <img class="emp-headImg" src=' <%# ((Dianzhu.Model.BusinessImage)Eval("AvatarCurrent")) == null ? "/images/common/emp-headicon.png" : "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(((Dianzhu.Model.BusinessImage)Eval("AvatarCurrent")).ImageName) + "&width=120&height=120&tt=3)"%>  '/>
                                                <div class="emp-info">
                                                    <p>姓名：<%#Eval("Name") %></p>
                                                    <p>工龄：</p>
                                                    <p>性别：<%#Eval("Gender")%></p>
                                                    <p>电话：<%#Eval("Phone")%></p>
                                                    <p>特长技能：</p>
                                                </div>
                                            </div>
                                            <div class="emp-model-b">
                                                <input type="button" staffId='<%#Eval("id") %>' class='btnAssign emp-assign <%# (bool)Eval("IsAssigned")?"assigned":"noAssign" %>' value='<%# (bool)Eval("IsAssigned")?"取消指派":"指派" %>'/>
                                            </div>
                                        </div>
                                        <!--end emp-model-->
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="col-md-3">
                                <div class="emp-no-model">
                                    <div class="emp-no-model-h"></div>
                                    <div class="emp-no-model-m">
                                        <a class="btn" href="Edit.aspx?businessid=<%=Request["businessid"] %>"><span class="add-inco">+</span>添加新员工</a>
                                    </div>
                                    <div class="emp-no-model-b"></div>
                                </div>
                            </div>
                        </div>
                        <div class="pageNum">
                            <UC:AspNetPager runat="server" FirstPageText="首页" NextPageText="下一页" PrevPageText="上一页" id="pager" PageSize="10" UrlPaging="true" LastPageText="尾页"></UC:AspNetPager>
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
        <script type="text/javascript" src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js"></script>
        <script type="text/javascript">
//            function listHref(url) {
//                var eve = window.event || arguments.callee.caller.arguments[0];
//                var $target = $(eve.target);
//                if ($target.hasClass("btn")) {
//                    return false
//                } else if (eve.target == eve.target) {
//                    window.location.href = url;
//                };
//            }

            $().ready(function(){
                $('[rel="tooltip"]').tooltip();

            });

            $(".btnAssign").click(function () {
                var $this = $(this);
                $.post("/ajaxservice/changestaffInfo.ashx",
                    {
                        "changed_field": "assign",
                        "changed_value": false,
                        "id": $this.attr("staffId")
                    }, function (data) {
                        var enabled = data.data;
                        var $card = $this.parents(".emp-model");
                        var $assignFlag = $card.find(".emp-assign-flag");
                        if ( enabled == "True" ) {
                            $this.val("取消指派");
                            $this.addClass("assigned").removeClass("noAssign");
                            $assignFlag.addClass("assigned").removeClass("noAssign");
                        }
                        else {
                            $this.val("指派");
                            $this.removeClass("assigned").addClass("noAssign");
                            $assignFlag.removeClass("assigned").addClass("noAssign");
                        }
                    });
            });
        </script>

</asp:Content>
