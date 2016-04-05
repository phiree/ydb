<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="Static" CodeFile="ServiceEdit.ascx.cs"
    Inherits="DZService_ServiceEdit" %>
<%@ Register Src="~/TagControl.ascx" TagName="Tag" TagPrefix="DZ" %>
<div class="content">
    <div class="content-head full-head">
        <input type="hidden" value="<%=merchantID%>" id="merchantID"/>
        <h3 class="cont-h2">
            服务资料填写
        </h3>
    </div>
    <div class="content-main">
        <div class="container-fluid animated fadeInUpSmall">
            <div class="steps-wrap steps-3 service-steps">
                <div class="row">
                    <div class="col-md-12">
                        <div class="steps-show">
                            <div class="steps-tips steps-tips-2 clearfix">
                                <div class="steps-tip">
                                    <div class="steps-tip-bg step-tip-1">
                                        <div class="step-tip-h">第一步</div>
                                        <p>基本服务信息</p>
                                    </div>
                                </div>
                                <div class="steps-tip">
                                    <div class="steps-tip-bg">
                                        <div class="step-tip-h">第二步</div>
                                        <p>服务价格信息</p>
                                    </div>
                                </div>
                                <div class="steps-tip">
                                    <div class="steps-tip-bg">
                                        <div class="step-tip-h">第三步</div>
                                        <p>服务时间设置</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="d-hr"></div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="steps-list">
                            <!--服务设置第一步-->
                            <div class="steps-step cur-step" >
                                <div class="model">
                                    <div class="model-h">
                                        <h4>基本服务信息</h4>
                                    </div>
                                    <div class="model-m">
                                        <div class="model-form">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label">服务名称</div>
                                                        <div class="col-md-8 model-input">
                                                            <asp:TextBox runat="server" CssClass="input-fluid" ID="tbxName" data-toggle="tooltip" data-placement="top" title="填写服务名称"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label">服务类型</div>
                                                        <div class="col-md-8 model-input">
                                                            <div>
                                                                <input id="setSerType" class="btn btn-info" type="button" value="请选择服务信息" />
                                                                <input class="dis-n" type="text" runat="server" focusid="setSerType" id="hiTypeId" />
                                                                <asp:Label CssClass="business-radioCf text-ellipsis hide" runat="server" ID="lblSelectedType"></asp:Label>
                                                                <div id="serLightContainer" class="serLightContainer dis-n">
                                                                    <div class="serChoiceTitle">
                                                                        选择服务信息
                                                                    </div>
                                                                    <div class="serChoiceMain">
                                                                        <div class="serChoiceSearch">
                                                                            <div class="checkSearchContainer">
                                                                                <input id="checkSearch" class="checkSearch" type="text"/>
                                                                                <input id="checkSearchClose" type="button" class="checkSearchClose" />
                                                                            </div>
                                                                            <input id="checkSearchConf" type="button" class="btn btn-cancel-light btn-xs checkSearchConf " value="搜索" />
                                                                        </div>
                                                                        <div id="serChoiceContainer" class="serChoiceContainer checklists" data-searchInput="#checkSearch" data-searchConf="#checkSearchConf" data-searchClose="#checkSearchClose"></div>
                                                                    </div>
                                                                    <div id="serChoiceResult" class="serChoiceResult dis-n">

                                                                    </div>
                                                                    <div class="serChoiceBtn">
                                                                        <input type="button" id="serChoiceConf" class="btn btn-info btn-xs lightClose" value="确认"/>
                                                                        <input type="button" id="serChoiceCancel" class="btn btn-info btn-xs lightClose " value="取消" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label">服务介绍</div>
                                                        <div class="col-md-8 model-input">
                                                            <asp:TextBox CssClass="input-textarea-fluid" runat="server" TextMode="MultiLine" ID="tbxDescription" data-toggle="tooltip" data-placement="top" title="请填写该服务的简单介绍"></asp:TextBox>
                                                            <!--<asp:CheckBox runat="server" ID="cbxEnable" Text="启用" />-->
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label">
                                                            服务范围
                                                        </div>
                                                        <div class="col-md-9 model-input">
                                                            <div class="map-container">
                                                                <div id="allmap"></div>
                                                                <!--百度地图API商圈功能-->
                                                                <!--<div id="city-container"></div>-->
                                                                <!--百度地图API输入功能-->
                                                                <div id="r-result" class="map-search dis-n">
                                                                    请输入服务点位置：<input type="text" id="suggestId" class="map-search-input" size="20" value="百度" /></div>
                                                                <div id="searchResultPanel" style="border: 1px solid #C0C0C0; width: 150px; height: auto;display: none;"></div>
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
                                                                <div class="map-btn">
                                                                    <input type="button" class="dis-n btn btn-info" id="saveSP" value="确定" />
                                                                    <input type="button" class="btn btn-info" id="editSP" value="设置服务区域" />
                                                                </div>
                                                                <div class="map-msg">
                                                                    <span id="saveMsg" class="dis-n">请设置服务区域</span>
                                                                    <div>
                                                                        <span id="saveAddress"></span><span id="saveRadius"></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <input id="hiBusinessAreaCode" class="dis-n" runat="server" snsi type="text">
                                                            <p class="cont-input-tip input-tip-fluid"><i class="icon icon-tip m-r10"></i>点击设置按钮，并放置该服务的服务区域。</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--服务设置第二步-->
                            <div class="steps-step" >
                                <div class="model">
                                    <div class="model-h">
                                        <h4>详细服务信息</h4>
                                    </div>
                                    <div class="model-m">
                                        <div class="model-form">
                                            <div class="row">
                                                <div class="col-md-5">
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">服务起步价</div>
                                                        <div class="col-md-8 model-input">
                                                            <asp:TextBox CssClass="input-fluid" required runat="server" snsi ID="tbxMinPrice" data-toggle="tooltip" data-placement="top" title="请填写该服务的起步价"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">服务单价</div>
                                                        <div class="col-md-8 model-input-unit-sm">
                                                            <asp:TextBox CssClass="input-fluid" sn  si runat="server" ID="tbxUnitPrice" data-toggle="tooltip" data-placement="top" title="请填写该服务的服务单价"></asp:TextBox>

                                                            <!--&nbsp;&nbsp;元&nbsp;/&nbsp;每&nbsp;&nbsp;-->
                                                            <div class="model-select-unit">
                                                                <div class="select select-fluid">
                                                                    <ul>
                                                                        <li><a>元&nbsp;/&nbsp;小时</a></li>
                                                                        <li><a>元&nbsp;/&nbsp;天</a></li>
                                                                        <li><a>元&nbsp;/&nbsp;次</a></li>
                                                                    </ul>
                                                                    <input type="hidden" id="rblChargeUnit" value="0" runat="server" snsi />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">服务定金</div>
                                                        <div class="col-md-8 model-input-unit">
                                                            <asp:TextBox CssClass="input-fluid" snsi runat="server" ID="tbxDespoist" data-toggle="tooltip" data-placement="top" title="请填写该服务需支付的定金"></asp:TextBox>
                                                            <em class="unit">元</em>
                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">提前预约时间</div>
                                                        <div class="col-md-8 model-input-unit">
                                                            <div class="select select-fluid min-select" data-toggle="tooltip" data-placement="top" title="请填写该服务的提前预约的时间（0为无需预约）">
                                                                <ul>
                                                                </ul>
                                                                <asp:TextBox runat="server" snsi CssClass="input-fluid dis-n" ID="tbxOrderDelay">60</asp:TextBox>
                                                            </div>
                                                            <em class="unit">分钟</em>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <!--<div class="row model-form-group">-->
                                                        <!--<div class="col-md-4 model-label-lg">每小时最大接单量</div>-->
                                                        <!--<div class="col-md-8 model-input-unit">-->
                                                            <!--<asp:TextBox CssClass="input-fluid" snsi runat="server" ID="tbxMaxOrdersPerHour" data-toggle="tooltip" data-placement="top" title="该服务的每小时最大接单量">50</asp:TextBox>-->
                                                            <!--<em class="unit">单</em>-->
                                                        <!--</div>-->
                                                    <!--</div>-->
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">是否上门</div>
                                                        <div class="col-md-8 model-input">
                                                            <asp:RadioButtonList RepeatDirection="Horizontal" CellSpacing="32" runat="server" ID="rblServiceMode" data-toggle="tooltip" data-placement="top" title="选择是否提供上门服务">
                                                                <asp:ListItem Selected="True" Value="0" Text="是" Class="model-radio-item m-r10"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="否" Class="model-radio-item"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">服务对象</div>
                                                        <div class="col-md-8 model-input">
                                                            <div class="service-checkBox d-inb" data-toggle="tooltip" data-placement="top" title="是否对公司提供该服务">
                                                                <asp:CheckBox CssClass="model-radio-item" runat="server" ID="cblIsForBusiness"
                                                                              Text="提供公司服务" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">服务保障</div>
                                                        <div class="col-md-8 model-input">
                                                            <div class="service-checkBox d-inb" data-toggle="tooltip" data-placement="top" title="选择是否加入先行赔付">
                                                                <asp:CheckBox CssClass="model-radio-item" runat="server" ID="cbxIsCompensationAdvance"
                                                                              Text="先行赔付" />
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">付款方式</div>
                                                        <div class="col-md-8 model-input">
                                                            <div class="d-inb" data-toggle="tooltip" data-placement="top" title="付款方式选择">
                                                                <asp:CheckBoxList RepeatDirection="Horizontal" CellSpacing="32" CssClass="service-input-radio" ID="rblPayType" runat="server">
                                                                    <asp:ListItem Selected="True" Value="1" Text="线上支付" Class="model-radio-item m-r20"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="当面支付" Class="model-radio-item"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row model-form-group">
                                                        <div class="col-md-4 model-label-lg">服务标签</div>
                                                        <div class="col-md-8 model-input">
                                                            <div>
                                                                <asp:TextBox CssClass="input-fluid" runat="server" ID="tbxTag"></asp:TextBox>
                                                            </div>
                                                            <div runat="server" id="dvTag">
                                                                <DZ:Tag runat="server" ID="dzTag" />
                                                            </div>
                                                            <p class="cont-input-tip input-tip-fluid"><i class="icon icon-tip"></i>添加该服务的特色标签，多个标签用空格隔开</p>
                                                        </div>
                                                    </div>
                                                    <!--<p class="cont-sub-title">平台认证</p>-->
                                                    <!--<asp:CheckBox CssClass="service-input-radio" runat="server" ID="cbxIsCertificated" Text="已通过" />-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--服务设置第三部-->
                            <div class="steps-step">
                                <div class="model">
                                    <div class="model-h">
                                        <h4>服务时间设置</h4>
                                    </div>
                                    <div class="model-m">
                                        <div class="model-form">
                                            <div class="row">
                                                <div class="col-md-12">

                                                    <div class="row model-form-group">
                                                        <div class="col-md-12">
                                                            <div id="workTimeSet">

                                                            </div>
                                                            <p class="cont-input-tip"><i class="icon icon-tip"></i>请选择该服务的服务时段</p>

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
                </div>
                <div class="step-ctrl">
                    <div class="model-global-bottom">
                        <a class="step-prev btn btn-info" value="prev"  >上一步</a>
                        <a class="step-next btn btn-info m-l10" value="next"  >下一步</a>
                        <asp:Button Text="下一步" CssClass="step-next-save btn btn-info m-l10 " runat="server" ID="btnSave" OnClick="btnSave_Click" />
                        <a class="step-complete btn btn-info m-l10 " href="/DZService/default.aspx?businessId=<%=Request["businessid"] %>">完成</a>
                        <a class="step-cancel btn btn-cancel m-l10" id="btnCancel" href="/DZService/default.aspx?businessId=<%=Request["businessid"] %>">取消</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>