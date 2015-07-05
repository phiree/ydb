<%@ Page Title="" Language="C#" MasterPageFile="~/m/c.master" AutoEventWireup="true"
    CodeFile="~/Account/default.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/m/css/myshop.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div data-role="header" style="background: #4b6c8b; border: none;">
        <a data-role="none" href="#left-panel" data-iconpos="notext" data-shadow="false"
            data-iconshadow="false">
            <div style="width: 42.375px; height: 42.375px;">
                <img src="/m/images/my-more.png" /></div>
        </a>
        <h1 style="color: #FFF;">
            <span style="background: url(/m/images/my-o-icon2.png) no-repeat; padding-left: 25px;">
                我的店铺</span></h1>
        <a href="#" data-theme="d" data-icon="arrow-l" data-iconpos="notext" data-shadow="false"
            data-iconshadow="false" data-role="none">
            <img src="/m/images/my-r-icon.png" /></a>
        <nav data-role="navbar" data-theme="myb">
            <ul>
                <li><a href="#" target="_parent" data-theme="mytile-active">店铺信息</a></li>
                <li><a href="#" target="_parent" data-theme="mytile">账号安全</a></li>
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
                    <li class="my-li" target="name">
                        <div class="ul-left">
                             <div class="m-top">店铺名:<span class="display_holder"><%=b.Name %></span></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="description">
                        <div class="ul-left">
                            <span class="m-top">店铺介绍</span>
                            <div class="display_holder" id="shopDetailedTxt" style="font-size: 14px;">
                                <%=b.Description %>
                            </div>
                        </div>
                    </li>
                </ul>
                 <ul class="panel-ul">
                    <li class="my-li2">
                        <div class="ul-left2">
                            店铺头像
                        </div>
                        <div class="ul-left">
                               
                                    
                            <div class="input-file-box d-inb">
                              <asp:FileUpload data-role="none"  CssClass="input-file-btn" runat="server" ID="fuAvater" />
                                  <i class="input-file-bg"  style='background-image:url(<%=b.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(b.BusinessAvatar.ImageName)+"&width=90&height=90&tt=2)":"../image/myshop/touxiangkuang_11.png" %>' ></i>
                                      
                                <i class="input-file-mark"></i>
                                <img class="input-file-pre" id="fuAvaterImg"/>
                            </div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="phone">
                        <div class="ul-left">
                            <div class="m-top"> 联系电话: <span class="display_holder">
                                <%=b.Phone %></span></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="address">
                        <div class="ul-left">
                            <div class="m-top"> 详细店址</div>
                            <div id="shopAddrTxt" class="display_holder" style="font-size: 14px;">
                                <%=b.Address %>
                            </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <a class="getMaphrefClass" href="#secondview" data-transition="slidedown" style="color: #58789a;"> <li class="my-li">
                        <div class="ul-left">
                                <div class="m-top"> 服务范围:
                                 <span id="serArea-txt">请输入服务区域</span></div>
                          </div>
                           
                               

                    </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="email">
                        <div class="ul-left">
                             <div class="m-top">邮箱: <span class="display_holder">
                                <%=b.Email %>
                            </span>
                            </div>
                        </div>
                    </li>
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
                                        <asp:ImageButton data-role="none" ID="ImageButton1" runat="server" CommandName="delete" ImageUrl="/image/shared/delete_1.png"
                                            ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                        <a href='<%#Config.BusinessImagePath+"original/"+Eval("ImageName") %>'>
                                            <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                id="imgLicence" />
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="input-file-box d-inb">
                                <asp:FileUpload data-role="none" CssClass="input-file-btn" runat="server" ID="fuShow1" />
                                <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                <img class="input-file-pre" />
                            </div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="workingyears">
                        <div class="ul-left">
                             <div class="m-top">店铺从业时间:<span class="display_holder"><%=b.WorkingYears %></span></div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="staffamount">
                        <div class="ul-left">
                            <div class="m-top"> 员工人数 <span class="display_holder">
                                <%=b.StaffAmount %></span>
                                </div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li3">
                        <div class="ul-left2">
                            营业执照
                        </div>
                        <div class="ul-left">
                             <asp:HyperLink  runat="server" ID="imgBusinessImage"></asp:HyperLink>
                            <div class="input-file-box d-inb">
                                <asp:FileUpload data-role="none" CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
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
                    <li class="my-li" target="chargename">
                        <div class="ul-left">
                             <div class="m-top">负责人姓名:<span class="display_holder"><%=b.Contact %></span></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="idtype">
                        <div class="ul-left">
                            <div class="m-top"> 证件类型 :<span class="display_holder"><%=b.ChargePersonIdCardType %></span></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="idno">
                        <div class="ul-left">
                           <div class="m-top">  证件号码: <span class="display_holder">
                                <%=b.ChargePersonIdCardNo %></span>
                                </div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li2">
                        <div class="ul-left2">
                            证件照
                        </div>
                        <div class="ul-left">
                              <asp:HyperLink runat="server" ID="imgChargePerson"></asp:HyperLink>
                                    
                            <div class="input-file-box d-inb">
                                <asp:FileUpload data-role="none" CssClass="input-file-btn" runat="server" ID="fuChargePerson" />
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
    <!--super right panel-->
    <div data-role="panel" id="super_right_panel" data-display="push" data-position="right">
        <div class="m-p-size pd-size">
            <div class="info-title">
                <span class="rp name">店铺名称</span> <span class="rp description">店铺介绍</span> <span
                    class="rp phone">联系电话</span> <span class="rp address">详细店址</span> <span class="rp email">
                        邮箱</span> <span class="rp show">图片展示</span> <span class="rp workingyears">店铺从业时间</span>
                <span class="rp staffamount">员工人数</span> <span class="rp businesscertphoto">营业执照</span>
                <span class="rp chargename">负责人姓名</span> <span class="rp idtype">证件类型</span> <span
                    class="rp idno">证件号码</span> <span class="rp idphoto">证件照</span>
            </div>
        </div>
        <div class="info-content">
            <div class="pd-size2">
                <div class="rp name">
                    <input id="tbxName" runat="server" class="inputShopName" clientidmode="Static" name="inputShopName"
                        type="text" value="请输入您的店铺名称" />
                </div>
                <div class="rp description">
                    <textarea class="myshop-input-textarea" clientidmode="Static" id="tbxIntroduced"
                        runat="server" name="shopIntroduced">(可输入60个字)</textarea>
                </div>
                <div class="rp phone">
                    <input type="text" id="tbxContactPhone" clientidmode="Static" runat="server" name="ContactPhone" /></textarea>
                </div>
                <div class="rp address">
                    <input type="text" id="tbxAddress" clientidmode="Static" runat="server" name="addressDetail" />
                </div>
                <div class="rp email">
                    <input type="text" runat="server" clientidmode="Static" id="tbxEmail" name="email" />
                </div>
                <div class="rp workingyears">
                    <input type="text" runat="server" clientidmode="Static" id="tbxBusinessYears" />
                </div>
                <div class="rp staffamount">
                    <select id="selStaffAmount" clientidmode="Static" runat="server">
                        <option value="10">10</option>
                        <option value="20">20</option>
                        <option value="50">50</option>
                    </select>
                </div>
                <div class="rp chargename">
                    <input type="text" runat="server" clientidmode="Static" id="tbxContact" />
                </div>
                <div class="rp idtype">
                    <select runat="server" id="selCardType" clientidmode="Static" name="card-tyle" data-theme="a">
                        <option value="1">学生证</option>
                        <option value="2">身份证</option>
                    </select>
                </div>
                <div class="rp idno">
                    <input type="text" runat="server" id="tbxCardIdNo" clientidmode="Static" />
                </div>
                <a href="#" id="rp_save" data-rel="close" data-role="button" data-inline="true">确定</a>
                <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
            </div>
        </div>
        <!--/super right panel-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu_status_shop" runat="Server">
    my-ui-btn-active
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">


    <script src="../js/shop_edit.js"></script>
       
        <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    
    <script src="../js/CityList.js" type="text/javascript"></script>
    <script src="../js/getMySopMap.js" type="text/javascript"></script>
</asp:Content>
