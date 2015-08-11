<%@ Page Title="" Language="C#" MasterPageFile="~/m/c.master" AutoEventWireup="true"
    CodeFile="~/business/edit.aspx.cs" Inherits="Business_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/m/css/myshop.css" />
    <link href="../css/mobiscroll-1.6.css" rel="stylesheet" type="text/css" />
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
                       <div class="myche">
                          <div class="myche1">店铺名:</div>
                          <div class="myche2"><span class="display_holder"><%=b.Name %></span></div>
                          <div class="myche3"></div>
                        </div>
                        
                    </li>
                   
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="description">


                        <div class="myche">
                          <div class="myche1">店铺介绍:</div>
                          <div class="myche2"><span class="display_holder" id="shopDetailedTxt" style="font-size: 14px;"><%=b.Description %></span></div>
                          <div class="myche3"></div>
                        </div>
                        
                    </li>
                </ul>
                 <ul class="panel-ul">
                    <li class="my-li2">

                         <div class="myche">
                          <div class="myche1">店铺头像:</div>
                          <div class="myche2">
                               <div class="input-file-box d-inb">
                              <asp:FileUpload data-role="none"  CssClass="input-file-btn" runat="server" ID="fuAvater" />
                                  <i class="input-file-bg"  style='background:url(<%=b.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(b.BusinessAvatar.ImageName)+"&width=80&height=79&tt=2) ":"../image/myshop/touxiangkuang_11.png" %> ' ></i>
                                      
                                <i class="input-file-mark"></i>
                                <img class="input-file-pre" id="fuAvaterImg"/>
                            </div>
                          </div>
                          
                        </div>
                        
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="phone">

                       <div class="myche">
                          <div class="myche1">联系电话:</div>
                          <div class="myche2"> <span class="display_holder"><%=b.Phone %></span></div>
                          <div class="myche3"></div>
                        </div>
                        
                    </li>
                </ul>
   
                <ul class="panel-ul">
                    <a class="getMaphrefClass" href="#secondview" data-transition="slidedown" style="color: #58789a;"> <li class="my-li">
                         <div class="myche">
                          <div class="myche1">详细店址:</div>
                          <div class="myche2"><span id="serArea-txt"><%=b.Address %></span></div>
                          <div class="myche3"></div>
                           <input type="hidden" runat="server" clientidmode="Static" id="hiAddrId" /> 

                        </div>    

                    </li>
                    </a>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="email">
                         <div class="myche">
                          <div class="myche1">邮箱:</div>
                          <div class="myche2"> <span class="display_holder"><%=b.Email%></span></div>
                          <div class="myche3"></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li2">
                   <div class="myche">
                          <div class="myche1">图片展示:</div>
                          <div class="myche2">
                               <asp:Repeater runat="server" ID="rpt_show" OnItemCommand="rpt_show_ItemCommand">
                                <ItemTemplate>
                                   <div class="picture">
                                        <asp:ImageButton data-role="none" ID="ImageButton1" CssClass="itemDele-img" runat="server" CommandName="delete" ImageUrl="/image/shared/delete_1.png"
                                            ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                        <a data-ajax="false" href='<%#Config.BusinessImagePath+"original/"+Eval("ImageName") %>'>
                                            <img width=80 height=79 src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=80&height=79&tt=2'
                                                id="imgLicence" />
                                        </a>
                                        
                                  </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        
                                <div class="input-file-box">
                                <asp:FileUpload data-role="none" CssClass="input-file-btn" runat="server" ID="fuShow1" />
                                <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                <img class="input-file-pre" id="imgshow" />
                                </div>
                          
                          </div>
                          
                        </div>
                       
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="workingyears">

                        <div class="myche">
                          <div>店铺从业时间:</div>
                          <div class="myche2"> <span class="display_holder"><%=b.WorkingYears%></span></div>
                          <div class="myche3"></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="staffamount">
                         <div class="myche">
                          <div class="myche1">员工人数:</div>
                          <div class="myche2"> <span class="display_holder"><%=b.StaffAmount%></span></div>
                          <div class="myche3"></div>
                        </div>
                        
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li4">

                       <div class="myche">
                          <div class="myche1">营业执照:</div>
                          <div class="myche2">
                               <div class="BusinessImageDiv">
                               <asp:Repeater runat="server" ID="rptLicenseImages" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                           
                                                <div class="download-img-pre fl">
                                                    <asp:ImageButton ID="ibCharge" OnClientClick="javascript:return confirm('确定删除?')" CssClass="download-img-delete itemDele-img" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a data-ajax="false" class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            class="imgCharge" />
                                                    </a>
                                                </div>
                                            
                                            </ItemTemplate>
                                        </asp:Repeater>
                               <asp:HyperLink  runat="server" ID="imgBusinessImage"></asp:HyperLink></div>
                                <div class="input-file-box d-inb">
                                <asp:FileUpload data-role="none" CssClass="input-file-btn" runat="server" ID="fuBusinessLicence" />
                                <i class="input-file-bg"></i><i class="input-file-mark"></i>
                                <img class="input-file-pre" id="BusinessImage" />
                            </div>
                          
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

                        <div class="myche">
                          <div>负责人姓名:</div>
                          <div class="myche2"> <span class="display_holder"><%=b.Contact%></span></div>
                          <div class="myche3"></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="idtype">
                        <div class="myche">
                          <div class="myche1">证件类型:</div>
                          <div class="myche2"> <span class="display_holder"><%=b.ChargePersonIdCardType%></span></div>
                          <div class="myche3"></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li" target="idno">
                        <div class="myche">
                          <div class="myche1">证件号码:</div>
                          <div class="myche2"> <span class="display_holder"><%=b.ChargePersonIdCardNo%></span></div>
                          <div class="myche3"></div>
                        </div>
                    </li>
                </ul>
                <ul class="panel-ul">
                    <li class="my-li4">
                             <div class="myche">
                          <div class="myche1">证件照:</div>
                          <div class="myche2"> 
                                 <div class="BusinessImageDiv">
                                  <asp:Repeater runat="server" ID="rptChargePersonIdCards" OnItemCommand="rpt_show_ItemCommand">
                                            <ItemTemplate>
                                                <div class="download-img-pre fl">
                                                    <asp:ImageButton ID="ibCharge" OnClientClick="javascript:return confirm('确定删除?')" CssClass="download-img-delete itemDele-img" runat="server" CommandName="delete"
                                                        ImageUrl="/image/myshop/shop_icon_91.png" ClientIDMode="Static" CommandArgument='<%#Eval("Id") %>' />
                                                    <a data-ajax="false" class="download-img-show" href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'>
                                                        <img src='/ImageHandler.ashx?imagename=<%#HttpUtility.UrlEncode(Eval("ImageName").ToString())%>&width=90&height=90&tt=2'
                                                            class="imgCharge" />
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                 <asp:HyperLink runat="server" ID="imgChargePerson"></asp:HyperLink></div>
                                    
                                <div class="input-file-box d-inb">
                                <asp:FileUpload data-role="none" CssClass="input-file-btn" runat="server" ID="fuChargePerson" />
                                <i class="input-file-bg"></i>
                                <i class="input-file-mark"></i>
                                <img class="input-file-pre" id="licenceimg"/>
                            </div>
                          </div>
                          
                        </div
                        
                    </li>
                </ul>
                <div style="text-align: right; margin-top: 10px;">
                    <asp:Button runat="server" ID="btnSave" OnClientClick="return MyshopSaveFun()" OnClick="btnSave_Click" Text="保存" />
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
                 <span id="errTxtName" class="erroTxt"></span>
                    <input id="tbxName" runat="server" class="inputShopName" clientidmode="Static" name="inputShopName"
                        type="text" value="请输入您的店铺名称" placeholder="可输入30个字" onfocus="myFoucuss('#tbxName')" onBlur="myshopInputEmpty('#tbxName','#errTxtName','店铺名称不能为空')" maxlength=30/>
                </div>
                <div class="rp description">

                   <span id="erroTxtIntroduced" class="erroTxt"></span>
                    <textarea class="myshop-input-textarea" clientidmode="Static" id="tbxIntroduced"
                        runat="server" name="shopIntroduced" placeholder="可输入60个字" onfocus="myFoucuss('#tbxIntroduced')" onBlur="myshopInputEmpty('#tbxIntroduced','#erroTxtIntroduced','店铺介绍不能为空')" maxlength=60></textarea>

                     
                </div>
                <div class="rp phone">
                    <span id="vregPhonetxt" class="erroTxt"></span>
                    <input type="text" id="tbxContactPhone" clientidmode="Static" runat="server" name="ContactPhone" onfocus="myFoucuss('#iphone')" onBlur="myShopvalidatemobile('#tbxContactPhone','#vregPhonetxt')" />
                </div>
                <div class="rp address">
                    <input type="text" id="tbxAddress" clientidmode="Static" runat="server" name="addressDetail" />
                </div>
                <div class="rp email">
                    <span id="vregEmailtxt" class="erroTxt"></span>
                    <input type="text" runat="server" clientidmode="Static" id="tbxWebSite" name="tbxWebSite" onfocus="myFoucuss('#tbxEmail')" onBlur="myShopemailCheck('#tbxEmail','#vregEmailtxt')"/>
                </div>
                <div class="rp workingyears">
                    <input type="text" runat="server" clientidmode="Static" id="tbxBusinessYears" readonly="true" />
                </div>
                <div class="rp staffamount">

                    <select id="selStaffAmount" clientidmode="Static" runat="server">
                        <option value="10">10</option>
                        <option value="20">20</option>
                        <option value="50">50</option>
                    </select>
                </div>
                <div class="rp chargename">
                    <span id="erroTxtContact" class="erroTxt"></span>
                    <input type="text" runat="server" clientidmode="Static" id="tbxContact"  onfocus="myFoucuss('#tbxContact')" onBlur="myshopInputEmpty('#tbxContact','#erroTxtContact','负责人姓名不能为空')" />
                </div>
                <div class="rp idtype">
                    <select runat="server" id="selCardType" clientidmode="Static" name="card-tyle" data-theme="a">
                        <option value="1">学生证</option>
                        <option value="2">身份证</option>
                    </select>
                </div>
                <div class="rp idno">
                <span id="erroTxtCardIdNo" class="erroTxt"></span>
                    <input type="text" runat="server" id="tbxCardIdNo" clientidmode="Static" onfocus="myFoucuss('#tbxContact')" onBlur="myshopCardCheck('#tbxCardIdNo','#erroTxtCardIdNo')" />
                </div>
                 <div style="text-align: right; ">
                <a href="#" id="rp_save" data-rel="close" data-role="button" data-inline="true">确定</a>
                <a href="#" data-rel="close" data-role="button" data-inline="true">取消</a>
                </div>
            </div>
        </div>
        <!--/super right panel-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menu_status_shop" runat="Server">
    my-ui-btn-active1
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">


    <script src="../js/mobiscroll-1.6.js" type="text/javascript"></script>
    <script src="../js/shop_edit.js" type="text/javascript"></script>
        <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW"></script>
    <script src="../js/CityList.js" type="text/javascript"></script>
    <script src="../js/getMySopMap.js" type="text/javascript"></script>
</asp:Content>
