<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Staff_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/employee.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <div class="cont-wrap secret-wrap ">
      <div class="emp-backage">
         <div class="cont-row">
              <div class="add-user-div">
               <a class="btn btn-info" href="Edit.aspx?businessid=<%=Request["businessid"] %>"><span class="add-inco">+</span>添加新员工</a>
              </div>
         </div>
         <div class="emp-content-div">
            <table class="emp-table">
                <tr class="emp-head-tr">
                   <th class="emp-th-style epm-th-2"></th>
                   <th class="emp-th-style epm-th-2">编号</th>
                   <th class="emp-th-style epm-th-2">姓名</th>
                   <th class="emp-th-style epm-th-2">昵称</th>
                   <th class="emp-th-style epm-th-2">性别</th>
                   <th class="emp-th-style epm-th-2">联系电话</th>
                   <th class="emp-th-style epm-th-2"></th>
                </tr>
                <asp:Repeater runat="server" ID="rptStaff" OnItemCommand="rptStaff_ItemCommand">
                <ItemTemplate>
                 <tr>
                  <td class="emp-td-style epm-th-2"><img src="../image/emp-headinco.png" width="49" height="49" /></td>
                  <td class="emp-td-style epm-th-2"><%#Eval("Code") %></td>
                  <td class="emp-td-style epm-th-2"><a href='edit.aspx?id=<%#Eval("id") %>&businessid=<%=Request["businessid"] %>'><%#Eval("Name") %></a></td>
                  <td class="emp-td-style epm-th-2"><%#Eval("NickName") %></td>
                  <td class="emp-td-style epm-th-2"><%#Eval("Gender")%></td>
                  <td class="emp-td-style epm-th-2"><%#Eval("Phone")%></td>
                  <td class="emp-td-style epm-btn epm-th-1"><span class='<%# ((bool)Eval("IsAssigned"))?"assigned":"notassigned" %> none'><%# (bool)Eval("IsAssigned")?"已指派":"未指派" %></span> <input type="button" staffid='<%#Eval("id") %>' class=' btnAssign btn <%# (bool)Eval("IsAssigned")?"btn-down-info":"btn-info" %>' value='<%# (bool)Eval("IsAssigned")?"取消指派":"指派" %>'/> 
                  
                  <asp:Button runat="server" CssClass="btn btn-delete" CommandArgument='<%#Eval("Id") %>' OnClientClick="javascript:return confirm('确认删除该员工?');" CommandName="delete" Text="删除"/></td>
                
                </tr></ItemTemplate>
                </asp:Repeater>
             
                
             
            
            </table>
         </div>
          <div class="pageNum">
         
             <a href="#">首页</a>|
             <a href="#">上一页</a>|
             <a href="#">第2页</a>|
             <a href="#">第3页</a>|
             <a href="#">第4页</a>|
             <a href="#">下一页</a>|
            <a href="#">尾页</a>
          
          
          </div>
      </div>
   </div>

   <div class="emp-d-item none">
                                <ul class="emp-d-list">
                                    <li class="d-list-i clearfix">
                                        <ul class="clearfix">
                                            <asp:Repeater runat="server" ID="rpt">
                                                <ItemTemplate>
                                                    <li class="col col-1">
                                                        <input type="checkbox" /><i class="icon user-pic"></i></li>
                                                    <li class="col col-2">
                                                        <%#Eval("Name") %></li>
                                                    <li class="col col-3">
                                                        <%#Eval("Gender") %></li>
                                                    <li class="col col-4">
                                                        <%#Eval("Age") %></li>
                                                    <li class="col col-5">
                                                        <%#Eval("WorkingYears") %></li>
                                                    <li class="col col-6"></li>
                                                    <li class="col col-7">
                                                        <div>
                                                            <input class="d-list-btn" type="button" />
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ul>
                            </div>

    <asp:GridView runat="server" ID="gvStaff">
        <Columns>
            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" />
        </Columns>
    </asp:GridView>
        <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.9.1.min.js"></script>-->
        <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
        <!--<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>-->
        <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/themes/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js"></script>
        <script type="text/javascript" src="/js/TabSelection.js" ></script>
        <script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
        <script type="text/javascript" src="../js/global.js"></script>
        <script type="text/javascript">
            var tabCheckedShow = function(that,checked){
    //        console.log($('.item').html());
                if (checked == true) {
                    var checkedShowBox = $('#serCheckedShow');
                    var checkedItem = $($(that).parents('.serviceTabsItem')).find('.item');
                    var checkText = checkedItem.html();
                    var checkTextNode = "<span>" + ' ' + checkText + ' ' + "</span>";
                    checkedShowBox.append(checkTextNode);
                } else {
                    return;
                }
            }

            $(function () {

               
            /* 当鼠标移到表格上是，当前一行背景变色 */

                $(".emp-table tr td").mouseover(function () {
                    $(this).parent().find("td").css("background-color", "#b0d3f5");
                });
     
          /* 当鼠标在表格上移动时，离开的那一行背景恢复 */

              $(".emp-table tr td").mouseout(function () {
                var bgc = $(this).parent().attr("bg");
                $(this).parent().find("td").css("background-color",bgc);
               });
               var color = "#f1f4f7"
               $(".emp-table tr:odd td").css("background-color", color);  //改变奇数行背景色
            /* 把背景色保存到属性中 */
               $(".emp-table tr:odd").attr("bg", color);
               $(".emp-table tr:even").attr("bg", "#fff");


     
    











                //            $("#tabsServiceType").TabSelection({
                //                "datasource":
                //                [
                //                    { "name": "维修", "id": 1, "parentid": 0 },
                //                    { "name": "家电维修", "id": 2, "parentid": 1 },
                //                     { "name": "冰箱维修", "id":3, "parentid": 2 },
                //                    { "name": "冰箱维修", "id": 6, "parentid": 2 },
                //                    { "name": "冰箱维修", "id": 7, "parentid": 2 },
                //                    { "name": "冰箱维修", "id": 8, "parentid": 2 },

                //                    { "name": "更换氟利昂", "id": 4, "parentid": 3 },
                //                    { "name": "交通服务", "id": 5, "parentid": 0 }
                //                ]
                //            });
                //        });
                $("#tabsServiceType").TabSelection({
                    "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
                    "enable_multiselect":true,
                    'check_changed': function (that,id, checked) {
    //                    alert(id + '' + checked);
                          tabCheckedShow(that,checked);
                    },

                    'leaf_ clicked': function (id, checked) {
    //                alert(id);
                    }
                });
            });

            $(".btnAssign").click(function () {
                var that = this;
                $.post("/ajaxservice/changestaffInfo.ashx",
                        {
                            "changed_field": "assign",
                            "changed_value": false,
                            "id": $(that).attr("staffid")
                        }, function (data) {
                            var enabled = data.data;
                            if (enabled == "True") {
                                $(that).siblings("span").html("已指派");
                                $(that).removeClass("btn-info");
                                $(that).addClass("btn-down-info");
                                $(that).val("取消指派");
                            }
                            else {
                                $(that).siblings("span").html("未指派");
                                $(that).val("指派");
                                $(that).removeClass("btn-down-info");
                                $(that).addClass("btn-info");
                            }

                        });
            });
            $("#addEmployee").click(function(e){
                $('#SerlightBox').lightbox_me({
                    centered: true
                });
                e.preventDefault();
            })


        </script>

</asp:Content>
