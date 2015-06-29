<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/DZService/ServiceEdit.ascx.cs"
    Inherits="DZService_ServiceEdit" %>
<link rel="stylesheet" href="/m/css/global.css" />
<link rel="stylesheet" href="/m/css/service_info.css" />
<div data-role="content" style="background: #cadbec; color: #617e9c; margin: 0; padding: 0;">
    <div class="m-p-size pd-size">
        <div class="info-title">
            服务基本信息</div>
    </div>
    <div class="info-content">
        <div class="pd-size2">
            <ul class="panel-ul">
                <a class="hrefClass" id="open_name" style="color: #58789a;">
                    <li class="my-li6">
                        <div class="ul-left">
                            服务名称
                        </div>
                        <div class="ul-right">
                            <span id="serviceName"></span>
                            <div class="ul-right li-inco">
                            </div>
                        </div>
                    </li>
                </a>
            </ul>
            <ul class="panel-ul">
                <a class="hrefClass" id="open_servicetype" style="color: #58789a;">
                    <li class="my-li6">
                        <div class="ul-left">
                            服务类别
                        </div>
                        <div class="ul-right">
                            <span id="targetid-txt">
                                <asp:Label runat="server" ID="lblSelectedType"></asp:Label></span>
                            <div class="ul-right li-inco">
                            </div>
                        </div>
                    </li>
                </a>
            </ul>
            <ul class="panel-ul">
                <a class="hrefClass" id="open_description" style="color: #58789a;">
                    <li class="my-li3">
                        <div class="ul-left">
                            <div>
                                服务介绍</div>
                            <div id="serInfo-txt" style="margin-bottom: 5px; font-size: 14px;">
                                请输入服务介绍</div>
                        </div>
                        <div class="ul-right li-inco">
                        </div>
                    </li>
                </a>
            </ul>
             <ul class="panel-ul">
                <a  class="hrefClass" id="open_enable" style="color: #58789a;">
                    <li class="my-li3">
                        <div class="ul-left">
                             
                                启用 
                             
                        </div>
                         <div class="ul-right">
                            <span id="spanEnable">
                                 </span>
                            <div class="ul-right li-inco">
                            </div>
                        </div>
                        
                    </li>
                </a>
            </ul>
        </div>
        <div class="m-p-size pd-size">
            <div class="info-title">
                服务详情
            </div>
        </div>
        <div class="info-content">
            <div class="pd-size2">
                <ul class="panel-ul">
                    <a class="getMaphrefClass" href="#secondview" data-transition="slidedown" style="color: #58789a;">


                        <li class="my-li6">
                            <div class="ul-left">
                                服务范围
                            </div>
                            <div class="ul-right">
                                <span id="serArea-txt">请输入服务区域</span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_minprice">
                        <li class="my-li6">
                            <div class="ul-left">
                                最低服务费
                            </div>
                            <div class="ul-right">
                                <span id="serMinPrice-txt"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_unitprice" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                服务单价
                            </div>
                            <div class="ul-right">
                                <span id="spanUnitPrice"></span><span id="spanChargeUnit"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_preorder_delay" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                提前预约时长
                            </div>
                            <div class="ul-right">
                                <span id="serPreorder_delay_txt"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_openperiod">
                        <li class="my-li6">
                            <div class="ul-left">
                                接单时间
                            </div>
                            <div class="ul-right">
                                <span id="serPeriod-txt"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_maxorder_day" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                每日最大接单量
                            </div>
                            <div class="ul-right">
                                <span id="spanMax_order_day"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_maxorder_hour" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                每小时最大接单量
                            </div>
                            <div class="ul-right">
                                <span id="span_maxorder_hour"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_servicemode" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                是否上门
                            </div>
                            <div class="ul-right">
                                <span id="spanServicemode"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_is_for_business" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                可以对公
                            </div>
                            <div class="ul-right">
                                <span id="span_forbusiness"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_compensation" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                先行赔付
                            </div>
                            <div class="ul-right">
                                <span id="span_compensation"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_certificated" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                平台认证
                            </div>
                            <div class="ul-right">
                                <span id="span_certificated"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <a class="hrefClass" id="open_paytype" style="color: #58789a;">
                        <li class="my-li6">
                            <div class="ul-left">
                                支付方式
                            </div>
                            <div class="ul-right">
                                <span id="span_paytype"></span>
                                <div class="ul-right li-inco">
                                </div>
                            </div>
                        </li>
                    </a>
                </ul>
            </div>
        </div>
        <div style="padding: 10px;">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" data-theme="mybtn2" />
        </div>
    </div>
</div>
<!-- left-slided-panel -->
<!--/left-slided-panel-->
<!-- right-slided-panel -->
<div data-role="panel" id="right-panel-super" data-display="push" data-position="right">
    <div class="m-p-size pd-size">
        <div class="info-title">
            <span class="rp name">服务名称</span> <span class="rp servicetype">服务类别</span> <span
                class="rp description">服务介绍</span> <span
                class="rp enable">启用状态</span> 
            <span class="rp minprice">最低服务价</span> <span class="rp unitprice">单价</span> <span
                class="rp preorder_delay">提前预约时间</span> <span class="rp open_period">接单时间</span>
            <span class="rp maxorder_day">每日最大接单量</span> <span class="rp maxorder_hour">每小时最大接单量</span>
            <span class="rp servicemode">是否上门</span> <span class="rp is_for_business">服务对象</span>
            <span class="rp is_compensation_advance">服务保障</span> <span class="rp is_certificated">
                平台认证</span> <span class="rp paytype">付款方式</span>
        </div>
    </div>
    <div class="info-content">
        <div class="pd-size2">
            <div class="rp name">
                <asp:TextBox runat="server" ID="tbxName" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="rp servicetype">
                <input type="hidden" runat="server" clientidmode="Static" id="hiTypeId" />
                <div id="tabsServiceType">
                    <ul>
                    </ul>
                </div>
            </div>
            <div class="rp description">
                <asp:TextBox CssClass="service-input-area" ClientIDMode="Static" runat="server" TextMode="MultiLine"
                    ID="tbxDescription"> </asp:TextBox>
            </div>
              <div class="rp enable">
               <asp:CheckBox runat="server" ClientIDMode="Static" Text="启用" ID="cbxEnable" />
            </div>
            <div class="rp servicescope">
                <input id="hiBusinessAreaCode" runat="server" clientidmode="Static" type="hidden">
                <p class="p_LocationArea service-item-title">
                    <i class="icon service-icon-serLocal"></i>定位服务区域</p>
                <!--<p class="f-s13 l-h16">服务中心点定位(为您的服务区域进行定位)</p>-->
                <div class="clearfix">
                    <div class="fl clearfix">
                        <div id="setBusiness" class="setLocationMap">
                        </div>
                        <!--<p id="businessText"></p>-->
                    </div>
                    <div class="fl m-l20">
<<<<<<< HEAD
                     
                      
=======
                  
>>>>>>> 2b4b5b1d165694a17103965bbb768821e47726df
                    </div>
                </div>
            </div>
            <div class="rp minprice">
                <asp:TextBox runat="server" ID="tbxMinPrice" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="rp unitprice">
                <asp:TextBox runat="server" ID="tbxUnitPrice" ClientIDMode="Static"></asp:TextBox>
                <asp:RadioButtonList runat="server" ClientIDMode="Static" ID="rblChargeUnit">
                    <asp:ListItem Selected="True" Value="0" Text="小时"></asp:ListItem>
                    <asp:ListItem Value="1" Text="天"></asp:ListItem>
                    <asp:ListItem Value="2" Text="次"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="rp preorder_delay">
                提前<asp:TextBox runat="server" ClientIDMode="Static" CssClass="service-input-sm m-lr10"
                    ID="tbxOrderDelay">60</asp:TextBox>分钟 预约
            </div>
            <div class="rp open_period">
                <p class="service-item-title">
                    <i class="icon service-icon-serIntro"></i>服务时间
                </p>
                <div>
                    <asp:TextBox CssClass="service-input-sm m-r10" ClientIDMode="Static" runat="server"
                        ID="tbxServiceTimeBegin">8:30</asp:TextBox>至
                    <asp:TextBox CssClass="service-input-sm m-l10" ClientIDMode="Static" runat="server"
                        ID="tbxServiceTimeEnd">21:00</asp:TextBox>
                </div>
            </div>
            <div class="rp maxorder_day">
                <asp:TextBox CssClass="service-input-mid m-r10" ClientIDMode="Static" runat="server"
                    ID="tbxMaxOrdersPerDay">50</asp:TextBox>单
            </div>
            <div class="rp maxorder_hour">
                <asp:TextBox CssClass="service-input-mid m-r10" ClientIDMode="Static" runat="server"
                    ID="tbxMaxOrdersPerHour">20</asp:TextBox>单
            </div>
            <div class="rp servicemode">
                <asp:RadioButtonList CssClass="service-input-radio" ClientIDMode="Static" runat="server"
                    ID="rblServiceMode">
                    <asp:ListItem Selected="True" Value="0" Text="是"></asp:ListItem>
                    <asp:ListItem Value="1" Text="否"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="rp is_for_business">
                <asp:CheckBox CssClass="service-input-radio" runat="server" ClientIDMode="Static"
                    ID="cblIsForBusiness" Text="可以对公" />
            </div>
            <div class="rp is_compensation_advance">
                <asp:CheckBox CssClass="service-input-radio" ClientIDMode="Static" runat="server"
                    ID="cbxIsCompensationAdvance" Text="加入先行赔付" />
            </div>
            <div class="rp is_certificated">
                <asp:CheckBox CssClass="service-input-radio" ClientIDMode="Static" runat="server"
                    ID="cbxIsCertificated" Text="已通过" />
            </div>
            <div class="rp paytype">
                <asp:RadioButtonList CssClass="service-input-radio" ClientIDMode="Static" ID="rblPayType"
                    runat="server">
                    <asp:ListItem Selected="True" Value="1" Text="线上"></asp:ListItem>
                    <asp:ListItem Value="2" Text="线下"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <a href="#" id="rp_save" data-rel="close" data-role="button" data-inline="true" onClick="getMapAddrText('#serArea-txt','#hiBusinessAreaCode')">确定</a>
            <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
        </div>
    </div>
</div>


<script src="/m/js/service_edit.js" type="text/javascript"></script>
<script src="/js/TabSelection.js" type="text/javascript"></script>
<<<<<<< HEAD

=======
<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
<script src="/m/js/CityList.js" type="text/javascript"></script>
<script src="/m/js/getMap.js" type="text/javascript"></script>
<script src="/m/js/global.js" type="text/javascript"></script>
>>>>>>> 2b4b5b1d165694a17103965bbb768821e47726df
<script type="text/javascript">

    $(function () {
        $("#tabsServiceType").TabSelection({
            "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
            "leaf_clicked": function (id) {
                $("#hiTypeId").val(id);
                $("#targetid-txt").text(id);
            }

        });
    });
</script>
