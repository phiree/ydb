<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="Static" CodeFile="ServiceEdit.ascx.cs"
    Inherits="DZService_ServiceEdit" %>
<%@ Register Src="~/TagControl.ascx" TagName="Tag" TagPrefix="DZ" %>
<div class="cont-wrap theme-color-58789a">
    <div class="cont-container animated fadeInUpSmall">
        <div class="cont-row step-row">
            <div class="cont-col-2">
                <h3 class="step-head step-1">
                    <img src="../image/shop-icon-step1.png" /></h3>
            </div>
            <div class="cont-col-10">
                <p class="step-text step-1">
                    基本服务信息</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务名称</p>
            </div>
            <div class="cont-col-10">
                <div class="clearfix">
                    <div>
                        <div>
                            <asp:TextBox runat="server" CssClass="input-mid" ID="tbxName" data-toggle="tooltip" data-placement="top" title="填写服务名称"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>填写服务名称</p>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务类型</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <input id="setSerType" class="ser-btn-SerType" type="button" value="请选择服务信息" />
                    <input class="dis-n" type="text" runat="server" focusid="setSerType" id="hiTypeId" />
                    <asp:Label CssClass="business-radioCf dis-n m-l10" runat="server" ID="lblSelectedType"></asp:Label>
                    <div id="serLightContainer" class="serviceTabs dis-n">
                        <div class="serChoiceTitle">
                            选择服务信息
                        </div>
                        <div class="serChoiceInfo clearfix">
                            <div id="serChoiceContainer" class="serChoiceContainer fl">
                                <em>已选择</em>
                            </div>
                            <div class="serChoiceBtnContainer fr">
                                <div class="d-inb">
                                    <div id="serChoiceConf" class="serChoiceBtn btn-confirm lightClose dis-n">
                                        确认</div>
                                </div>
                                <div id="serChoiceCancel" class="serChoiceBtn btn-cancel lightClose d-inb">
                                    取消</div>
                            </div>
                        </div>
                        <div id="serList" class="serListContainer lightFluid clearfix">
                        </div>
                    </div>
                </div>
                <p class="cont-input-tip"><i class="icon icon-tip"></i>请选择该服务的类型</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务介绍</p>
            </div>
            <div class="cont-col-10">
                <div class="cont-row">
                    <div class="cont-col-12">
                        <div>
                            <asp:TextBox CssClass="input-textarea" runat="server" TextMode="MultiLine" ID="tbxDescription" data-toggle="tooltip" data-placement="right" title="请填写该服务的简单介绍"></asp:TextBox>
                        </div>
                        <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务的特色介绍</p>-->
                    </div>
                </div>
                <!--<div class="cont-row">-->
                <!--<div class="cont-col-12">-->
                <!--<p>-->
                <!--<asp:CheckBox runat="server" ID="cbxEnable" Text="启用" />-->
                <!--</p>-->
                <!--</div>-->
                <!--</div>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    您的服务区域</p>
            </div>
            <div class="cont-col-10">
                <div class="cont-row">
                    <div class="map-container">
                        <div id="allmap">
                        </div>
                        <!--<p>添加点击地图监听事件，点击地图后显示当前经纬度</p>-->
                        <!--<p>&#45;&#45;点击地图放置服务点，拖拽服务圆设置圆（服务）半径&#45;&#45;</p>-->
                        <!--<input id="LocalAddrJson" type="text" value="">-->
                        <!--<p>百度地图API商圈功能:</p>-->
                        <!--<div id="city-container"></div>-->
                        <!--<p>百度地图API输入功能:</p>-->
                        <div id="r-result" class="map-result dis-n">
                            请输入服务点位置：<input type="text" id="suggestId" class="map-result-input" size="20" value="百度" /></div>
                        <div id="searchResultPanel" style="border: 1px solid #C0C0C0; width: 150px; height: auto;
                            display: none;">
                        </div>
                        <div id="radius-container" class="map-radius-result dis-n">
                            <span>服务半径：</span>
                            <select id="ser-radius" class="map-radius-select">
                                <option value="1000">1000</option>
                                <option value="1500">1500</option>
                                <option value="2000">2000</option>
                                <option value="3000">3000</option>
                                <option value="4000">4000</option>
                            </select>
                            <span>m</span>
                        </div>
                        <!--<div id="add-sp">添加新服务点<input type="button" id="addSP" value="+" /><span id="addError">当前服务点未设置，无法添加新服务点</span></div>-->
                        <!--<div id="del-sp">删除服务点<input type="button" id="delSP" value="删除" /><span id="delError">请至少设置一个服务点</span></div>-->
                        <div class="sp-btn">
                            <input type="button" class="dis-n btn btn-info" id="saveSP" value="确定" /><input type="button"
                                class="btn btn-delete" id="editSP" value="设置服务区域" /></div>
                        <div class="sp-msg">
                            <span id="saveMsg" class="dis-n">请设置服务区域</span><div>
                                <span id="saveAddress"></span><span id="saveRadius"></span>
                            </div>
                        </div>
                        <!--<div id="SPContainer"></div>-->

                    </div>
                    <input id="hiBusinessAreaCode" class="dis-n" runat="server" snsi type="text">
                </div>
                <!--<div class="cont-row">-->
                <!--<div class="cont-col-4">-->
                <!--<div class="fl clearfix m-b20">-->
                <!--<div id="setBusiness" class="setLocationMap">-->
                <!--<div id="businessMapSub" class="mapSub"></div>-->
                <!--</div>-->
                <!--</div>-->
                <!--</div>-->
                <!--<div class="cont-col-8">-->
                <!--<div>-->
                <!--<p class="m-b20">您选择的区域：</p>-->
                <!--<p id="businessText" class="business-text"></p>-->
                <!--</div>-->
                <!--</div>-->
                <!--<div class="cont-col-12">-->
                <!--<p><span class="text-anno-r">（点击地图为您的服务区域进行定位）</span></p>-->
                <!--</div>-->
                <!--</div>-->
                <p class="cont-input-tip">
                    <i class="icon icon-tip"></i>点击按钮，设置该服务的服务区域。</p>
            </div>
        </div>
        <div class="cont-row step-row">
            <div class="cont-col-2">
                <h3 class="step-head step-2">
                    <img src="../image/shop-icon-step2.png" /></h3>
            </div>
            <div class="cont-col-10">
                <p class="step-text step-2">
                    完善服务信息</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务起步价</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <asp:TextBox CssClass="input-sm" required runat="server" snsi ID="tbxMinPrice" data-toggle="tooltip" data-placement="top" title="请填写该服务的起步价"></asp:TextBox>&nbsp;&nbsp;元
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务的起步价</p>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务单价</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxUnitPrice" data-toggle="tooltip" data-placement="top" title="请填写该服务的服务单价"></asp:TextBox>&nbsp;&nbsp;元&nbsp;/&nbsp;每&nbsp;&nbsp;（
                    <asp:RadioButtonList CssClass="serviceDataRadio d-inb" runat="server" ID="rblChargeUnit">
                        <asp:ListItem Selected="True" Value="0" Text="小时"></asp:ListItem>
                        <asp:ListItem Value="1" Text="天"></asp:ListItem>
                        <asp:ListItem Value="2" Text="次"></asp:ListItem>
                    </asp:RadioButtonList>
                    ）
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务的服务价</p>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    提前预约时间</p>
            </div>
            <div class="cont-col-10">
                <div>
                    至少&nbsp;&nbsp;
                    <div class="d-inb select select-xm hour-select" data-toggle="tooltip" data-placement="top" title="请填写该服务的提前预约的时间（0为无需预约）">
                        <ul>
                        </ul>
                        <input type="text" class="dis-n" id="tbxBusinessYears" name="workYears" />
                    </div>
                    小时
                    <!--<asp:TextBox runat="server" snsi CssClass="input-sm" ID="tbxOrderDelay">60</asp:TextBox>-->
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>请填写该服务是否需要提前预约的时间</p>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务时间</p>
            </div>
            <div class="cont-col-10 time-select-all">
                <table class="custom-table service-time-table">
                    <tbody>
                    <asp:Repeater runat="server" ID="rptOpenTimes">
                        <ItemTemplate>
                        <tr>
                        <td class="table-col-3">
                        <span runat="server" id="spDayOfWeek"><%# System.Globalization.DateTimeFormatInfo.CurrentInfo.GetDayName((DayOfWeek)Convert.ToInt32( Eval("DayOfWeek")))%></span>
                         <input type="hidden" id="hiDayOfWeek" runat="server" value='<%#Eval("DayOfWeek") %>'/></td>
                            <asp:Repeater runat="server" ID="rptTimesOneDay">
                                <ItemTemplate>
                                    <td class="table-col-3">
                                        <div>
                                            <div class="time-select-wrap">
                                                <a class="time-trigger" /></a>
                                                <input class="dis-n time-value" runat="server" id="tbxTimeBegin" value='<%#Eval("TimeStart") %>'
                                                    type="text" />
                                            </div>
                                            &nbsp;&nbsp;至&nbsp;&nbsp;
                                            <div class="time-select-wrap">
                                                <a class="time-trigger" ><%#Eval("TimeEnd") %></a>
                                                <input class="dis-n time-value" runat="server" id="tbxTimeEnd" value='<%#Eval("TimeEnd") %>'
                                                    type="text" />
                                            </div>
                                        </div>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>

                            <td class="table-col-3">
                                <input type="checkbox" runat="server" id="cbxChecked" /><label>启用</label>
                            </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

                <!---------------------------------------------服务时间原值----------------------------------->
                <div style="display: none;">
                    <div class="time-select-wrap">
                        <a class="time-trigger" /></a>
                        <asp:TextBox CssClass="dis-n time-value" time-role="value" snsi runat="server" ID="tbxServiceTimeBegin"></asp:TextBox>
                    </div>
                    &nbsp;&nbsp;至&nbsp;&nbsp;
                    <div class="time-select-wrap">
                        <a class="time-trigger" /></a>
                        <asp:TextBox CssClass="dis-n time-value" snsi runat="server" ID="tbxServiceTimeEnd"></asp:TextBox>
                    </div>
                </div>
                <p class="cont-input-tip">
                    <i class="icon icon-tip"></i>请选择该服务的服务时段</p>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-6">
                <div class="cont-row">
                    <div class="cont-col-4">
                        <p class="cont-sub-title">
                            每日最大接单量:</p>
                    </div>
                    <div class="cont-col-8">
                        <div>
                            <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxMaxOrdersPerDay" data-toggle="tooltip" data-placement="top" title="该服务的每小时最大接单量">50</asp:TextBox>&nbsp;&nbsp;单
                        </div>
                        <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>该服务的每日或每小时最大接单量</p>-->
                    </div>
                </div>
            </div>
            <div class="cont-col-6">
                <div class="cont-row">
                    <div class="cont-col-6">
                        <p class="cont-sub-title">
                            每小时最大接单量</p>
                    </div>
                    <div class="cont-col-6">
                        <div>
                            <asp:TextBox CssClass="input-sm" snsi runat="server" ID="tbxMaxOrdersPerHour" data-toggle="tooltip" data-placement="top" title="该服务的每日最大接单量">20</asp:TextBox>&nbsp;&nbsp;单
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    是否上门</p>
            </div>
            <div class="cont-col-10">
                <div>
                    <asp:RadioButtonList CssClass="service-input-radio" runat="server" ID="rblServiceMode" data-toggle="tooltip" data-placement="top" title="选择是否提供上门服务">
                        <asp:ListItem Selected="True" Value="0" Text="是"></asp:ListItem>
                        <asp:ListItem Value="1" Text="否"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>选择是否提供上门服务</p>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务对象</p>
            </div>
            <div class="cont-col-10">
                <div class="service-checkBox d-inb" data-toggle="tooltip" data-placement="top" title="请填写店铺负责人证件号码">
                    <asp:CheckBox CssClass="service-input-radio" runat="server" ID="cblIsForBusiness"
                        Text="选择是否可以给公司提供该服务" />
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>选择是否可以给公司提供该服务</p>-->
            </div>
        </div>
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务保障</p>
            </div>
            <div class="cont-col-10">
                <div class="service-checkBox d-inb" data-toggle="tooltip" data-placement="top" title="请填写店铺负责人证件号码">
                    <asp:CheckBox CssClass="service-input-radio" runat="server" ID="cbxIsCompensationAdvance"
                        Text="选择是否加入先行赔付" />
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>选择是否加入先行赔付</p>-->
            </div>
        </div>
        <!--<div class="cont-row service-cont-row">-->
        <!--<div class="cont-col-2">-->
        <!--<p class="cont-sub-title">平台认证</p>-->
        <!--</div>-->
        <!--<div class="cont-col-10">-->
        <!--<div class="service-checkBox">-->
        <!--<asp:CheckBox CssClass="service-input-radio" runat="server" ID="cbxIsCertificated" Text="已通过" />-->
        <!--</div>-->
        <!--<p class="cont-input-tip"><i class="icon icon-tip"></i></p>-->
        <!--</div>-->
        <!--</div>-->
        <div class="cont-row service-cont-row">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    付款方式</p>
            </div>
            <div class="cont-col-10">
                <div class="d-inb" data-toggle="tooltip" data-placement="top" title="付款方式选择">
                    <asp:CheckBoxList CssClass="service-input-radio" ID="rblPayType" runat="server">
                        <asp:ListItem Selected="True" Value="1" Text="线上支付"></asp:ListItem>
                        <asp:ListItem Value="2" Text="当面支付"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <!--<p class="cont-input-tip"><i class="icon icon-tip"></i>付款方式选择</p>-->
            </div>
        </div>
        <div class="cont-row service-cont-row" runat="server" id="dvTag">
            <div class="cont-col-2">
                <p class="cont-sub-title">
                    服务标签</p>
            </div>
            <div class="cont-col-10">
                <div>
                   <div class="d-inb">
                       <DZ:Tag runat="server" ID="dzTag" />
                   </div>
                </div>

                <p class="cont-input-tip"><i class="icon icon-tip"></i>添加该服务的特色标签</p>
            </div>
        </div>
    </div>

    <div class="service-saveSubmit">
        <asp:Button Text="保存" CssClass="btn btn-info btn-big" runat="server" ID="btnSave"
            OnClick="btnSave_Click" />
        <a class="btn btn-cancel btn-big m-l10" href="/DZService/default.aspx?businessId=<%=Request["businessid"] %>">
            取消</a>
    </div>
</div>
