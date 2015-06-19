<%@ Page Title="" Language="C#" MasterPageFile="~/m/m.master" AutoEventWireup="true"
    CodeFile="~/Account/default.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/myshop.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div data-role="page" data-title="我的店铺" data-theme="myb">
        <div data-role="header" style="background: #4b6c8b; border: none;">
            <a data-role="none" href="#left-panel" data-iconpos="notext" data-shadow="false"
                data-iconshadow="false">
                <div style="width: 42.375px; height: 42.375px;">
                    <img src="images/my-more.png" /></div>
            </a>
            <h1 style="color: #FFF;">
                <span style="background: url(images/my-o-icon2.png) no-repeat; padding-left: 25px;">
                    我的店铺</span></h1>
            <a href="#" data-theme="d" data-icon="arrow-l" data-iconpos="notext" data-shadow="false"
                data-iconshadow="false" data-role="none">
                <img src="images/my-r-icon.png" /></a>
            <nav data-role="navbar" data-theme="myb">
        <ul>
         <li><a href="myshop.html" target="_parent" data-theme="mytile-active">店铺信息</a></li>
          <li><a href="account.html" target="_parent" data-theme="mytile" >账号安全</a></li>
          </ul>
     </nav>
        </div>
        <div data-role="content" style="background: #cadbec; color: #617e9c; margin: 0; padding: 0;">
            <div class="m-p-size pd-size">
                <div class="info-title">
                    基本信息</div>
            </div>
            <div class="info-content">
                <div class="pd-size2">
                    <ul class="panel-ul">
                        <a style="color: Red;" href="#right-panel" onclick="goToRightPanel('#tbxName','#shopName-txt')">
                            <li class="my-li">
                                <div class="ul-left">
                                    <div class="m-top">
                                        店铺名:<span id="shopName-txt"><%=b.Name %></span></div>
                                    <input runat="server" type="text" data-role="none" style="display: none" clientidmode="Static"
                                        id="tbxName" name="inputShopName" value="请输入您的店铺名称" class="inputShopName" />
                                </div>
                                <div class="ul-right li-inco">
                                </div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <a href="#right-panel" onclick="goToRightPanel('#tbxIntroduced','#shopDetailedTxt')">
                            <li class="my-li">
                                <div class="ul-left">
                                    <span class="m-top">店铺介绍</span>
                                    <div id="shopDetailedTxt" style="font-size: 14px;">
                                        <%=b.Description %></div>
                                </div>
                                <textarea class="myshop-input-textarea" style="display: none" clientidmode="Static"
                                    id="tbxIntroduced" data-role="none" runat="server" name="shopIntroduced">(可输入60个字)</textarea>
                                <div class="ul-right li-inco">
                                </div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <a href="#right-panel" onclick="goToRightPanel('#phoneName','#phone-txt')">
                            <li class="my-li">
                                <div class="ul-left">
                                    <div class="m-top">
                                        联系电话:</span><span id="phone-txt"><%=b.Phone %></span></div>
                                    <input type="text" id="tbxContactPhone" data-role="none" style="display: none" runat="server"
                                        name="ContactPhone" />
                                </div>
                                <div class="ul-right li-inco">
                                </div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <a href="#right-panel" onclick="goToRightPanel('#shopAddr','#shopAddrTxt')">
                            <li class="my-li">
                                <div class="ul-left">
                                    <span class="m-top">详细店址</span>
                                    <div id="shopAddrTxt" style="font-size: 14px;">
                                        <%=b.Address %></div>
                                    <input type="text" data-role="none" style="display: none" id="tbxAddress" runat="server"
                                        name="addressDetail" />
                                </div>
                                <div class="ul-right li-inco">
                                </div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <a href="#right-panel" onclick="goToRightPanel('#emailName','#email-txt')">
                            <li class="my-li">
                                <div class="ul-left">
                                    <div class="m-top">
                                        邮箱:</span><span id="email-txt"><%=b.Email %></span></div>
                                    <input type="text" data-role="none" style="display: none" runat="server" id="tbxEmail"
                                        name="email" />
                                </div>
                                <div class="ul-right li-inco">
                                </div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <li class="my-li2">
                            <div class="ul-left2">
                                图片展示
                            </div>
                            <div class="ul-left">
                                <asp:Repeater runat="server" ID="rpt_show" OnItemCommand="rpt_show_ItemCommand">
                                    <ItemTemplate>
                                        <div class="picture">
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="delete" ImageUrl="/image/shared/delete_1.png"
                                                ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                            <a href='<%#Config.BusinessImagePath+"original/"+Eval("ImageName") %>'>
                                                <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                    id="imgLicence" />
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuShow1" />
                                    <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                    <img class="input-file-pre" />
                                </div>
                            </div>
                        </li>
                    </ul>
                    <ul class="panel-ul">
                        <a href="#" target="_parent">
                            <li class="my-li">
                                <div class="ul-left">
                                    <div class="m-top">
                                        店铺从业时间</div>
                                </div>
                                <div class="ul-right li-inco">
                                    <input type="hidden" runat="server" id="tbxBusinessYears" name="email" /></div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <li class="my-li3">
                            <div class="ul-left2">
                                营业执照
                            </div>
                            <div class="ul-left">
                                <img runat="server" id="imgLicence" src="/image/dianjishangchuan_1.png" />
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
                                    <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                    <img class="input-file-pre" />
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="m-p-size pd-size">
                <div class="info-title">
                    负责人信息</div>
            </div>
            <div class="info-content">
                <div class="pd-size2">
                    <ul class="panel-ul">
                        <a href="#right-panel" onclick="goToRightPanel('#personName','#personName-txt')">
                            <li class="my-li">
                                <div class="ul-left">
                                    <div class="m-top">
                                        负责人姓名:</span><span id="personName-txt">张三</span></div>
                                    <input type="hidden" name="personName" id="personName" value="" />
                                </div>
                                <div class="ul-right li-inco">
                                </div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <li class="my-li">
                            <div class="ul-left">
                                <div style="margin-top: 20px;">
                                    证件类型</div>
                            </div>
                            <div style="float: right">
                                <select name="card-tyle" id="iphone-tyle" data-theme="a">
                                    <option value="1">学生证</option>
                                    <option value="2">身份证</option>
                                </select>
                            </div>
                        </li>
                    </ul>
                    <ul class="panel-ul">
                        <a href="#right-panel" onclick="goToRightPanel('#carNum','#carNum-txt')">
                            <li class="my-li">
                                <div class="ul-left">
                                    <div class="m-top">
                                        证件号码:</span><span id="carNum-txt">12345678</span></div>
                                    <input type="hidden" name="carNum" id="carNum" value="" />
                                </div>
                                <div class="ul-right li-inco">
                                </div>
                            </li>
                        </a>
                    </ul>
                    <ul class="panel-ul">
                        <li class="my-li2">
                            <div class="ul-left2">
                                证件照
                            </div>
                            <div class="ul-left">
                                <img runat="server" id="imgChargePerson" src="/image/dianjishangchuan_1.png" />
                                <div class="input-file-box d-inb">
                                    <asp:FileUpload CssClass="input-file-btn" runat="server" ID="fuChargePerson" />
                                    <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                    <img class="input-file-pre" />
                                </div>
                            </div>
                        </li>
                    </ul>
                    <div style="text-align: right; margin-top: 10px;">
                        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="保存" />
                    </div>
                </div>
            </div>
        </div>
        <!-- /content -->
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
                    <li><a href="service_info.html" target="_parent">服务信息</a></li>
                    <li class="my-ui-btn-active"><a href="myshop.html" target="_parent">店铺信息</a></li>
                </ul>
            </div>
        </div>
        <!-- /panel -->
        <div data-role="panel" id="right-panel" data-display="push" data-position="right">
            <div class="m-p-size pd-size">
                <div class="info-title">
                    店铺信息</div>
            </div>
            <div class="info-content">
                <div class="pd-size2">
                    <textarea name="rightInputName" id="rightInputName"></textarea>
                    <a href="#" data-rel="close" data-role="button" data-inline="true" onclick="saveInputVal()">
                        确定</a> <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
                </div>
            </div>
        </div>
        <!-- /panel -->
    </div>
</asp:Content>
