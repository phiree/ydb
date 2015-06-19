<%@ Page Title="" Language="C#" MasterPageFile="~/m/m.master" AutoEventWireup="true"
    CodeFile="service.aspx.cs" Inherits="m_service" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/service_info.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <!--page-->
    <div data-role="page" data-title="服务信息" data-theme="myb">
       <!--header-->
        <div data-role="header" style="background: #4b6c8b; border: none;">
            <a data-role="none" href="#left-panel" data-iconpos="notext" data-shadow="false"
                data-iconshadow="false">
                <div style="width: 42.375px; height: 42.375px;">
                    <img src="images/my-more.png" /></div>
            </a>
            <h1 style="color: #FFF;">
                <span style="background: url(images/my-o-icon2.png) no-repeat; padding-left: 25px;">
                    服务信息</span></h1>
            <a href="#" data-theme="d" data-icon="arrow-l" data-iconpos="notext" data-shadow="false"
                data-iconshadow="false" data-role="none">
                <img src="images/my-r-icon.png" /></a>
            <nav data-role="navbar" data-theme="myb">
        <ul>
         <li><a href="service_info.html" target="_parent" data-theme="mytile">信息管理</a></li>
          <li><a href="service_info_set.html" target="_parent" data-theme="mytile-active">信息设置</a></li>
          </ul>
     </nav>
        </div>
        <!--/header-->
     <div data-role="content" style="background: #cadbec; color: #617e9c; margin: 0; padding: 0;">
            <div class="m-p-size pd-size">
                <div class="info-title">
                    服务基本信息</div>
                <div class="info-content">
                    <div class="pd-size2">
                        <ul class="panel-ul">
                            <a class="hrefClass" href="#right-panel-servicetype" onclick="goToRightPanel('#tbxName','#serviceName')">
                                <li class="my-li6">
                                    <div class="ul-left">
                                        服务名称
                                    </div>
                                    <div class="ul-right">
                                        <span id="serviceName">请输入服务名称</span>
                                        <input type="hidden" name="inputTargetid" id="Hidden1" value="" />
                                        <div class="ul-right li-inco">
                                            <asp:TextBox runat="server" ID="tbxName" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </li>
                            </a>
                        </ul>
                        <ul class="panel-ul">
                            <a class="hrefClass" href="#right-panel-servicetype" onclick="goToRightPanel('#hiTypeId','#targetid-txt')">
                                <li class="my-li6">
                                    <div class="ul-left">
                                        服务类别
                                    </div>
                                    <div class="ul-right">
                                        <span id="targetid-txt">请选择服务类别</span>
                                        <input type="hidden" name="inputTargetid" id="inputTargetid" value="" />
                                        <div class="ul-right li-inco">
                                            <input type="hidden" id="hiTypeId" clientidmode="Static" runat="server" />
                                        </div>
                                    </div>
                                </li>
                            </a>
                        </ul>
                        <ul class="panel-ul">
                            <a class="hrefClass" href="#right-panel" onclick="goToRightPanel('#tbxDescription','#serInfo-txt')">
                                <li class="my-li">
                                    <div class="ul-left">
                                        <div>
                                            服务介绍</div>
                                        <div id="serInfo-txt" style="margin-bottom: 5px; font-size: 14px;">
                                            请输入服务介绍</div>
                                    </div>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="tbxDescription" ClientIDMode="Static"> </asp:TextBox>
                                    <div class="ul-right li-inco">
                                    </div>
                                </li>
                            </a>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="m-p-size pd-size">
                <div class="info-title">
                    定位服务区域信息
                </div>
                <div class="info-content">
                    <div class="pd-size2">
                        <ul class="panel-ul">
                            <a class="hrefClass" href="#right-panel" onclick="goToRightPanel('#serArea','#serArea-txt')">
                                <li class="my-li6">
                                    <div class="ul-left">
                                        服务区域选择
                                    </div>
                                    <div class="ul-right">
                                        <span id="serArea-txt">请输入服务区域</span>
                                        <div class="ul-right li-inco">
                                        </div>
                                    </div>
                                    <input type="hidden" name="serArea" id="serArea" value="" />
                                </li>
                            </a>
                        </ul>
                        <ul class="panel-ul">
                            <a class="hrefClass" href="#right-panel" onclick="goToRightPanel('#serAddr','#serAddr-txt')">
                                <li class="my-li6">
                                    <div class="ul-left">
                                        服务中心点定位
                                    </div>
                                    <div class="ul-right">
                                        <span id="serAddr-txt">请输入服务中心</span>
                                        <div class="ul-right li-inco">
                                        </div>
                                    </div>
                                    <input type="hidden" name="serAddr" id="serAddr" value="" />
                                </li>
                            </a>
                        </ul>
                        <ul class="panel-ul">
                            <a class="hrefClass" href="#right-panel" onclick="goToRightPanel('#serRange','#serRange-txt')">
                                <li class="my-li6">
                                    <div class="ul-left">
                                        服务范围选择
                                    </div>
                                    <div class="ul-right">
                                        <span id="serRange-txt">请输入服务范围选</span>
                                        <div class="ul-right li-inco">
                                        </div>
                                    </div>
                                    <input type="hidden" name="serRange" id="serRange" value="" />
                                </li>
                            </a>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="m-p-size pd-size">
                <div class="info-title">
                    定位服务区域信息
                </div>
                <div class="info-content">
                    <div class="pd-size2">
                        <ul class="panel-ul">
                            <a class="TimehrefClass" href="#right-panel3" onclick="goToRightPanel3('#seriveTime','#seriveTime2','#seriveTime-txt','#seriveTime2-txt')">
                                <li class="my-li6">
                                    <div class="ul-left">
                                        时段一
                                    </div>
                                    <div class="ul-right">
                                        请选择时段
                                        <div class="ul-right li-inco">
                                        </div>
                                    </div>
                                </li>
                            </a>
                        </ul>
                        <ul class="panel-ul">
                            <li class="my-li">
                                <div class="ul-left">
                                    服务时间
                                </div>
                                <div class="ul-right">
                                    <span id="seriveTime-txt"></span>
                                    <input type="hidden" name="seriveTime" id="seriveTime" value="" />
                                </div>
                            </li>
                        </ul>
                        <ul class="panel-ul">
                            <li class="my-li">
                                <div class="ul-left">
                                    服务时段
                                </div>
                                <div class="ul-right">
                                    <span id="seriveTime2-txt"></span>
                                    <input type="hidden" name="seriveTime2" id="seriveTime2" value="" />
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <!--服务字段-->
            <div style="text-align: right; margin-top: -10px;">
                <asp:Button runat="server" OnClick="btnSave_Click" Text="保存" />
            </div>
        </div>


    <!-- left-slided-panel -->
    <div data-role="panel" id="left-panel" style="background: #4c6f8f; color: #80abd2;">
        <div class="lefe-top">
            <div>
                <img src="images/left-user-inco.png" /></div>
            <div style="padding-left: 15%; padding-right: 15%;">
                <a href="#" data-role="button" data-theme="mya">立即登录</a></div>
            <div>
                快登陆，抢客源咯~</div>
        </div>
        <div class="lefe-bottom">
            <ul>
                <li><a href="message.html" target="_parent">消息</a></li>
                <li><a href="employee-management.html" target="_parent">员工管理</a></li>
                <li><a href="main.html" target="_parent">订单</a></li>
                <li class="my-ui-btn-active"><a href="service_info.html" target="_parent">服务信息</a></li>
                <li><a href="myshop.html" target="_parent">店铺信息</a></li>
            </ul>
        </div>
    </div>
    <!--/left-slided-panel-->
     <!-- right-slided-panel -->
    <div data-role="panel" id="right-panel" data-display="push" data-position="right">
        <div class="m-p-size pd-size">
            <div class="info-title">
                账号安全</div>
        </div>
        <div class="info-content">
            <div class="pd-size2">
                <textarea name="rightInputName" id="rightInputName"></textarea>
                <a href="#" data-rel="close" data-role="button" data-inline="true" onclick="saveInputVal()">
                    确定</a> <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
            </div>
        </div>
    </div>

    <div data-role="panel" id="right-panel-servicetype" data-display="push" data-position="right">
        <div class="m-p-size pd-size">
            <div class="info-title">
                服务类别</div>
        </div>
        <div class="info-content">
            <div class="pd-size2">
                <div id="tabsServiceType">
                    <ul>
                    </ul>
                </div>
                <a href="#" data-rel="close" data-role="button" data-inline="true" onclick="saveInputVal()">
                    确定</a> <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
            </div>
        </div>
    </div>

    <div data-role="panel" id="right-panel2" data-display="push" data-position="right">
        <div class="m-p-size pd-size">
            <div class="info-title">
                账号安全</div>
        </div>
        <div class="info-content">
            <div class="pd-size2">
                <input type="file" name="fileName" id="fileName" />
                <a href="#" data-rel="close" data-role="button" data-inline="true" onclick="saveInputVal()">
                    确定</a> <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
            </div>
        </div>
    </div>

    <div data-role="panel" id="right-panel3" data-display="push" data-position="right">
        <div class="m-p-size pd-size">
            <div class="info-title">
                时段一</div>
        </div>
        <div class="info-content">
            <div class="pd-size2">
                <label for="serTime">
                    服务时间</label>
                <input type="text" name="serTime" id="serTime" />
                <label for="serTime2">
                    服务时段</label>
                <input type="text" name="serTime2" id="serTime2" />
                <a href="#" data-rel="close" data-role="button" data-inline="true" onclick="saveInputVal()">
                    确定</a> <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
            </div>
        </div>
    </div>
      <!-- /right-slided-panel -->
    </div>
    <!--/page-->
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
    <script src="/js/TabSelection.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tabsServiceType").TabSelection({
                "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
                "leaf_clicked": function (id) {
                    $("#hiTypeId").val(id);
                }

            });

        }); //ready
           
      
    </script>
</asp:Content>
