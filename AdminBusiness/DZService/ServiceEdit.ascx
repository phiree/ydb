<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceEdit.ascx.cs" Inherits="DZService_ServiceEdit" %>

                    <div class="serviceRight clearfix">
                        <div class="service-md">
                            <div class="service-md-title">
                                基本信息<i class="icon servie-icon-mdStand"></i></div>

                            <div class="service-md-main clearfix">
                                <div class="service-md-mleft">
                                    <div class="service-m">
                                        <p class="p_ServiceType service-item-title">
                                            <i class="icon service-icon-serType"></i>服务名称</p>
                                        <div class="clearfix">
                                            <div>
                                                <div>
                                                    <asp:TextBox runat="server" CssClass="service-input-mid" ID="tbxName"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="service-m">
                                        <p class="p_ServiceType service-item-title">
                                            <i class="icon service-icon-serType"></i>请选择您的服务类型</p>
                                        <div class="clearfix">
                                            <div>
                                                <div>
                                                    <div class="m-b10">
                                                        <input id="setSerType" class="ser-btn-SerType" type="button" value="选择服务信息" /><input type="hidden" runat="server" clientidmode="Static" id="hiTypeId" />
                                                        <div id="radioShowBox" class="d-inb"></div>
                                                    </div>
                                                    <div id="setSerTypeShow" >
                                                        <asp:Label CssClass="business-radioCk" runat="server" ID="lblSelectedType"></asp:Label>
                                                    </div>
                                                </div>
                                                <div id="SerlightBox" class="serviceTabs dis-n">
                                                    <div id="tabsServiceType">
                                                        <ul>
                                                        </ul>
                                                    </div>
                                                    <div class="m-t10">
                                                        <input class="ser-btn-SerType close" type="button" value="确定">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="service-md-mright">
                                    <div class="service-m">
                                        <p class="p_serviceIntroduced service-item-title">
                                            <i class="icon service-icon-serIntro"></i>服务介绍</p>
                                        <p>
                                            <asp:TextBox CssClass="service-input-area" runat="server" TextMode="MultiLine" ID="tbxDescription"> </asp:TextBox></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="service-md">
                            <div class="service-md-title">
                                服务范围<i class="icon servie-icon-mdRound"></i></div>
                            <div class="service-md-main">
                                <div class="service-m">
                                    <p class="p_LocationArea service-item-title">
                                        <i class="icon service-icon-serLocal"></i>定位服务区域</p>
                                    <!--<p class="f-s13 l-h16">服务中心点定位(为您的服务区域进行定位)</p>-->
                                    <div class="clearfix">
                                        <div class="fl clearfix">
                                            <div id="setBusiness" class="setLocationMap">
                                                <div id="businessMapSub" class="mapSub">
                                                                                </div>
                                            </div>
                                            <input id="hiBusinessAreaCode" runat="server" clientidmode="Static" type="hidden">
                                            <!--<p id="businessText"></p>-->
                                        </div>
                                        <div class="fl m-l20">
                                            <p class="m-b20">
                                                您选择的位置：</p>
                                            <p id="businessText" class="business-text m-b50">
                                            </p>
                                            <p class="l-h16">
                                                ←点击选择商圈范围</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="service-md">
                            <div class="service-md-title">
                                服务信息<i class="icon servie-icon-mdItem"></i></div>
                            <div class="service-md-main">
                                <div class="service-md-box clearfix">
                                    <div class="service-md-mleft">
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>服务起步价</p>
                                            <div>
 
                                                <asp:TextBox  CssClass="service-input-mid m-r10" runat="server" ID="tbxMinPrice"></asp:TextBox>&nbsp;&nbsp;元 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ErrorMessage="*" ControlToValidate="tbxMinPrice"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                        runat="server" ErrorMessage="数字" ControlToValidate="tbxMinPrice" 
                                                    ValidationExpression="\d+(\.\d+)?"  ></asp:RegularExpressionValidator>
 
                                            </div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>服务单价</p>
                                            <div>
                                                <asp:TextBox CssClass="service-input-mid m-r10" runat="server" ID="tbxUnitPrice"></asp:TextBox>元&nbsp;/&nbsp;每(
                                                 <asp:RadioButtonList CssClass="serviceDataRadio d-inb" runat="server" ID="rblChargeUnit">
                                                    <asp:ListItem  Selected="True" Value="0" Text="小时"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="天"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="次"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                )
                                            </div>
                                        </div>
                                    </div>
                                    <div class="service-md-mright">
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>提前预约时间</p>
                                            <div>
                                                至少<asp:TextBox runat="server" CssClass="service-input-sm m-lr10" ID="tbxOrderDelay">60</asp:TextBox>分钟</div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>服务时间</p>
                                            <div>
                                                <asp:TextBox CssClass="service-input-sm m-r10" runat="server" ID="tbxServiceTimeBegin">8:30</asp:TextBox>至
                                                <asp:TextBox CssClass="service-input-sm m-l10" runat="server" ID="tbxServiceTimeEnd">21:00</asp:TextBox></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="service-md-box clearfix">
                                    <div class="service-md-mleft">
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>每日最大接单量</p>
                                            <div>
                                                <asp:TextBox CssClass="service-input-mid m-r10" runat="server" ID="tbxMaxOrdersPerDay">50</asp:TextBox>单</div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>每小时最大接单量</p>
                                            <div>
                                                <asp:TextBox CssClass="service-input-mid m-r10" runat="server" ID="tbxMaxOrdersPerHour">20</asp:TextBox>单</div>
                                        </div>
                                    </div>
                                    <div class="service-md-mright">
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>是否上门</p>
                                            <div>
                                                <asp:RadioButtonList CssClass="service-input-radio" runat="server" ID="rblServiceMode">
                                                    <asp:ListItem Selected="True" Value="0" Text="是"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="否"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>服务对象</p>
                                            <div>
 
                                                <asp:CheckBox CssClass="service-input-radio" runat="server" ID="cblIsForBusiness" Text="可以对公"/>
                                                    
 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="service-md-box clearfix">
                                    <div class="service-md-mleft">
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>服务保障</p>
                                           <asp:CheckBox CssClass="service-input-radio" runat="server" ID="cbxIsCompensationAdvance" Text="加入先行赔付" />
                                        </div>
                                    </div>
                                    <div class="service-md-mright">
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>平台认证</p>
                                              <asp:CheckBox CssClass="service-input-radio" runat="server" ID="cbxIsCertificated" Text="已通过" />
                                        </div>
                                    </div>
                                </div>
                                <div class="service-md-box clearfix">
                                    <div class="service-md-mleft">
                                        <div class="service-m">
                                            <p class="service-item-title">
                                                <i class="icon service-icon-serIntro"></i>付款方式</p>
                                            <div>
                                                <asp:RadioButtonList CssClass="service-input-radio" id="rblPayType" runat="server" >
                                                    <asp:ListItem Selected="True" Value="1" Text="线上"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="线下"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--<div class="serviceDetailsLeft">-->
                        <!--<div class="p-20">-->
                        <!--</div>-->
                        <!--</div>-->
                        <!--<div class="serviceDetailsRight">-->
                        <!--<div class="p-20">-->
                        <!--百度地图选择商圈-->
                        <div id="mapLightBox" class="dis-n">
                            <div class="mapWrap">
                                <div id="businessMap" class="mapMain">
                                </div>
                                <div id="businessCity" class="mapCity">
                                </div>
                                <div class="mapButton">
                                    <input id="confBusiness" class="close ser-sm-input" type="button" value="确定"></div>
                                <input id="businessValue" type="hidden" value="" />
                            </div>
                        </div>
                        <!--</div>-->
                        <!--</div>-->
                    </div>
                    <div class="service-saveSubmit">
                        <asp:Button CssClass="service-saveSubmit-btn" runat="server" ID="btnSave" OnClick="btnSave_Click" />
                    </div>

   
    