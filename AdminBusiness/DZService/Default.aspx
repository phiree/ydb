
<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="DZService_Default" %>
    <%@ Register  Src="~/DZService/ServiceEdit.ascx" TagName="ServiceEdit" TagPrefix="UC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content hide" id="service-list">
        <div class="content-head normal-head">
            <h3>我的服务</h3>
            <a class="btn btn-gray-light fr" role="button" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>" >+&nbsp;添加新服务</a>
        </div>
        <div class="content-main">
            <div class="animated fadeInUpSmall">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="service-model-wrap">
                                <div class="model">
                                    <div class="model-h">
                                        <h4>服务列表</h4>
                                    </div>
                                    <div class="model-m no-padding">
                                        <div class="service-list-head">
                                            <div class="custom-grid">
                                                <div class="custom-col col-static-5">
                                                    <div class="l-b">
                                                        编号
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-10">
                                                    <div class="l-b">
                                                        服务名称
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-20">
                                                    <div class="l-b">
                                                        服务类型
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-20">
                                                    <div class="l-b">
                                                        服务区域
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-10">
                                                    <div class="l-b">
                                                        服务货架图
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-10">
                                                    <div class="l-b">
                                                        服务货架图
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-20">
                                                    <div class="l-b">
                                                        服务操作
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="service-list scrollbar-inner" >
                                            <asp:Repeater runat="server" ID="rptServiceList"  >
                                                <ItemTemplate>
                                                    <div class="service-row">
                                                        <div class="custom-grid" >
                                                            <div class="custom-col col-static-5">
                                                                <span><%# String.Format("{0:0000}", (Container.ItemIndex + 1)) %></span>
                                                            </div>
                                                            <div class="custom-col col-static-10">
                                                                <span class="text-ellipsis"><%#Eval("Name") %></span>
                                                            </div>
                                                            <div class="custom-col col-static-20">
                                                                <i class="icon service-icon svcType-s-icon-<%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.TopType.Id  %>"></i>                                                                <span class=""><%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.Name  %></span>

                                                            </div>
                                                            <div class="custom-col col-static-20">
                                                                <span class="spServiceArea text-ellipsis" ></span><input type="hidden" id="hiServiceArea" class="hiServiceArea" value='<%#((Dianzhu.Model.DZService)GetDataItem()).BusinessAreaCode %>' />
                                                            </div>
                                                            <div class="custom-col col-static-10">
                                                                <a class="btn btn-info btn-xs" href="ServiceShelf.aspx?businessId=<%=Request.Params["businessId"]%>&serviceId=<%#Eval("Id") %>" collapse-ignore="true">查看货架</a>
                                                                <!--通过修改boostrap中的collapse模块功能，实现collapse标签中指定忽略指定target的功能-->
                                                            </div>
                                                            <div class="custom-col col-static-10">
                                                                <a class="btn btn-info-light btn-xs" href="Detail.aspx?businessid=<%=Request.Params["businessId"]%>&serviceId=<%#Eval("Id") %>" collapse-ignore="true">服务详情</a>
                                                            </div>
                                                            <div class="custom-col col-static-20">
                                                                <a collapse-ignore="true" class="t-c <%# ((bool)Eval("Enabled"))?"btn btn-delete-light btn-xs":"btn btn-info-light btn-xs" %> enable-service" serid='<%#Eval("Id") %>' > <%# ((bool)Eval("Enabled"))?"禁用":"启用" %></a>

                                                                <a class="btn btn-info-light btn-xs" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>&serviceid=<%#Eval("Id") %>" >编辑
                                                                </a>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-cancel-light btn-xs" CommandArgument='<%# Eval("Id")%>' OnCommand="delbt_Command" OnClientClick="javascript: return confirm('警告：\n数据一旦被删除将无法还原！')" >删除
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="content hide" id="service-new">
        <div class="animated fadeInUpSmall">
            <div class="container-fluid">
                <div class="empty-svc">
                    <a id="firstAddBusiness" class="empty-svc-add" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>">点击创建新服务 <strong>+</strong></a>
                    <i class="empty-svc-icon"></i>
                    <p class="empty-svc-msg">点击创建新服务，让您服务专业化！</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script src='<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js'></script>
    <script src="/js/core/ServiceType.js?v=20160517"></script>
    <script src="/js/plugins/jquery.lightbox_me.js"></script>
    <script src="/js/plugins/jquery.scrollbar.js"></script>
    <script>
       $(function () {

           if ( $(".service-list").children(".service-row").length == 0 ){
               $("#service-new").removeClass("hide");
           } else {
               $("#service-list").removeClass("hide");
           }

           $(".scrollbar-inner").scrollbar();


           $(".enable-service").click(function () {
               var that = this;
               $.post("/ajaxservice/changeserviceInfo.ashx",
                {
                    "changed_field": "enabled",
                    "changed_value": false,
                    "id": $(that).attr("serid")
                }, function (data) {
                    var enabled = data.data;
                    if (enabled == "True") {
                        $(that).html("禁用");
                        $(that).removeClass("btn-info-light").addClass("btn-delete-light");
                    }
                    else {
                        $(that).html("启用");
                        $(that).addClass("btn-info-light").removeClass("btn-delete-light");
                    }

                });
           });

           $("#setSerType").click(function (e) {
               $('#serLightContainer').lightbox_me({
                   centered: true
               });
               e.preventDefault();
           });

           // 显示已设置服务类型
           (function() {
               var hiTypeValue = $("#hiTypeId").attr("value");
               if ( typeof hiTypeValue !== "undefined") {
                   $("#lblSelectedType").removeClass("dis-n").addClass("d-inb");
               }
           })();

           $(".spServiceArea").each(function () {
               var jsonServiceArea = $.parseJSON($(this).siblings(".hiServiceArea").val());
           $(this).html(jsonServiceArea.serPointAddress);
           });
                            

       });
    </script>
</asp:Content>
