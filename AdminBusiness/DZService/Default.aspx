
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
            <a class="btn btn-default btn-info" role="button" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>" >+&nbsp;添加新服务</a>
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
                                        <div class="service-panel-head">
                                            <div class="custom-grid">
                                                <div class="custom-col col-static-3">
                                                    <div class="l-b">
                                                        <input type="checkbox" />
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-5">
                                                    <div class="l-b">
                                                        编号
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-10">
                                                    <div class="l-b">
                                                        服务图标
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-10">
                                                    <div class="l-b">
                                                        服务名称
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-10">
                                                    <div class="l-b">
                                                        服务类型
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-20">
                                                    <div class="l-b">
                                                        服务区域
                                                    </div>
                                                </div>
                                                <div class="custom-col col-static-20">
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
                                        <div class="panel-list service-panel-list" id="accordion" role="tablist" aria-multiselectable="true">
                                            <asp:Repeater runat="server" ID="rptServiceList"  >
                                                <ItemTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading" role="tab" id="heading<%# Container.ItemIndex + 1 %>" >
                                                            <div class="custom-grid" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                 href="#collapse<%# Container.ItemIndex + 1 %>" aria-expanded="true"
                                                                 aria-controls="collapse<%# Container.ItemIndex + 1 %>">
                                                                <div class="custom-col col-static-3">
                                                                    <input type="checkbox" collapse-ignore="true" />
                                                                </div>
                                                                <div class="custom-col col-static-5"></div>
                                                                <div class="custom-col col-static-10">
                                                                    <i class="icon service-icon svcType-s-icon-<%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.TopType.Id  %>"></i>
                                                                </div>
                                                                <div class="custom-col col-static-10">
                                                                    <%#Eval("Name") %>
                                                                </div>
                                                                <div class="custom-col col-static-10">
                                                                    <%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.Name  %>
                                                                </div>
                                                                <div class="custom-col col-static-20">
                                                                    <span class="spServiceArea text-ellipsis" ></span><input type="hidden" id="hiServiceArea" class="hiServiceArea" value='<%#((Dianzhu.Model.DZService)GetDataItem()).BusinessAreaCode %>' />
                                                                </div>
                                                                <div class="custom-col col-static-20">
                                                                    <a  href="ServiceTimeline.aspx?businessid=<%=Request.Params["businessId"]%>&serviceId=<%#Eval("Id") %>" collapse-ignore="true">详细服务时间</a>
                                                                    <!--通过修改boostrap中的collapse模块功能，实现collapse标签中指定忽略指定target的功能-->
                                                                </div>
                                                                <div class="custom-col col-static-20">

                                                                    <!--<span class="t-c service-status <%#Eval("Id") %>'> <%# ((bool)Eval("Enabled"))?"theme-color-right":"theme-color-delete" %>" serid='<%#Eval("Id") %>'> <%# ((bool)Eval("Enabled"))?"已启用":"已禁用" %></span>-->
                                                                    <p collapse-ignore="true" class="t-c <%# ((bool)Eval("Enabled"))?"btn btn-down-info":"btn btn-info" %> enable-service" serid='<%#Eval("Id") %>' > <%# ((bool)Eval("Enabled"))?"禁用":"启用" %></p>

                                                                    <a href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>&serviceid=<%#Eval("Id") %>" class="m-r10" ><i class="icon service-icon-edit" title="编辑" collapse-ignore="true"></i></a>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn-test-1" CommandArgument='<%# Eval("Id")%>' OnCommand="delbt_Command" OnClientClick="javascript: return confirm('警告：\n数据一旦被删除将无法还原！')" data-target="ture" collapse-ignore="true"><i class="icon service-icon-delete" title="删除" collapse-ignore="true"></i></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="collapse<%# Container.ItemIndex + 1 %>" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading<%# Container.ItemIndex + 1 %>">
                                                            <div class="panel-body">
                                                                <div class="service-panel-detail">
                                                                    <div class="">服务介绍：<%#Eval("Description") %></div>
                                                                    <div class="">
                                                                        <div class="service-p-d">
                                                                            <div>起步价：<span ><%# ((decimal)Eval("MinPrice")).ToString("#") %></span>&nbsp;元</div>
                                                                            <div>单价：<span><%#((decimal)Eval("UnitPrice")).ToString("#") %></span>&nbsp;元/
                                                                                <%# ((Dianzhu.Model.Enums.enum_ChargeUnit)Eval("ChargeUnit")).ToString()=="Hour"?"小时":(((Dianzhu.Model.Enums.enum_ChargeUnit)Eval("ChargeUnit")).ToString()=="Day"? "天":"次") %></div>
                                                                        </div>
                                                                        <div class="service-p-d">
                                                                            <div>服务对象：<%#((bool)Eval("IsForBusiness"))?"可以对公":"对私" %></div>
                                                                            <div>服务保障：<%# ((bool)Eval("IsCompensationAdvance"))?"有":"无" %></div>
                                                                        </div>
                                                                        <div class="service-p-d">
                                                                            <div> 平台认证：<%# ((bool)Eval("IsCertificated"))?"有":"无" %></div>
                                                                            <div></div>
                                                                        </div>
                                                                    </div>
                                                                    <div class=""></div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">

                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-3">

                                                                    </div>
                                                                    <div class="col-md-3">

                                                                    </div>
                                                                    <div class="col-md-3">

                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-3">

                                                                    </div>
                                                                    <div class="col-md-3">

                                                                    </div>
                                                                    <div class="col-md-3"></div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        服务方式：<%#  (Dianzhu.Model.Enums.enum_ServiceMode)Eval("ServiceMode")==Dianzhu.Model.Enums.enum_ServiceMode.NotToHouse?"不上门":"上门" %>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        每日最大接单量：<span><%# Eval("MaxOrdersPerDay") %></span>&nbsp;单
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        每时最大接单量：<span><%# Eval("MaxOrdersPerHour") %></span>&nbsp;单
                                                                    </div>
                                                                </div>
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
                    <a id="firstAddBusiness" class="empty-svc-add" href="/dzservice/service_edit.aspx?businessid=<%=Request["businessid"]%>">点击创建新服务</a>
                    <i class="empty-svc-icon"></i>
                    <p class="empty-svc-msg">点击创建新服务，让您服务专业化！</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script type="text/javascript" src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/js/ServiceType.js?v=20150901"></script>
    <script type="text/javascript" src="/js/ServiceSelect.js"></script>
    <script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
    <script >
    $(function(){
        if ( $("#accordion").children(".panel").length == 0 ){
            $("#service-new").removeClass("hide");
        } else {
            $("#service-list").removeClass("hide");
        }
    })
    </script>
    <script type="text/javascript">
       $(function () {

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
                        $($(that).parent().parent()).find(".service-status").html("已启用");
                        $($(that).parent().parent()).find(".service-status").removeClass("theme-color-delete").addClass("theme-color-right");
                        $(that).removeClass("btn-info").addClass("btn-down-info");
                    }
                    else {
                        $(that).html("启用");
                        $($(that).parent().parent()).find(".service-status").html("已禁用");
                        $($(that).parent().parent()).find(".service-status").removeClass("theme-color-right").addClass("theme-color-delete");
                        $(that).addClass("btn-info").removeClass("btn-down-info");

                    }

                });
           });

           $("#setSerType").click(function (e) {
               $('#serLightContainer').lightbox_me({
                   centered: true
               });
               e.preventDefault();
           });

           function readTypeData() {
               var hiTypeValue = $("#hiTypeId").attr("value");
               if (hiTypeValue != undefined) {
                   $("#lblSelectedType").removeClass("dis-n");
                   $("#lblSelectedType").addClass("d-inb");
               } else {
                   return;
               }
           };

           readTypeData();

           $(".spServiceArea").each(function () {
               var jsonServiceArea = $.parseJSON($(this).siblings(".hiServiceArea").val());
           $(this).html(jsonServiceArea.serPointAddress);
           });
                            

       });
    </script>
    <script src="/js/validation_service_edit.js" type="text/javascript"></script>
    <script src="/js/validation_invalidHandler.js" type="text/javascript"></script>
    <script>
        function loadBaiduMapScript() {
          var script = document.createElement("script");
          script.src = "http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW&callback=initializeService";
          document.body.appendChild(script);
        }

        $(document).ready(function(){
            loadBaiduMapScript();
        })
    </script>
</asp:Content>
