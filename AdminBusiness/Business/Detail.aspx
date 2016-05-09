<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Business_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--<link rel="Stylesheet" href="/js/lightbox/css/lightbox.css"/>-->
    <!--<link rel="Stylesheet" href="/css/business.css"/>-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-main">
            <div class="container-fluid animated fadeInUpSmall">
                <div class="row">
                    <div class="col-md-12">
                        <a class="biz-total-card" href="/DZservice/default.aspx?businessId=<%= CurrentBusiness.Id %>">
                            <div class="biz-card-t">
                                <strong class="biz-card-tl"><asp:Literal runat="server" ID="liServiceCount"></asp:Literal></strong>
                                <div class="biz-card-tr">
                                    <p>个</p>
                                    <p>服务</p>
                                </div>
                            </div>
                            <div class="biz-card-b"></div>
                        </a>
                        <a class="biz-total-card">
                            <div class="biz-card-t">
                                <strong class="biz-card-tl"><asp:Literal runat="server" ID="liAllOrderCount"></asp:Literal></strong>
                                <div class="biz-card-tr">
                                    <p>张</p>
                                    <p>订单</p>
                                </div>
                            </div>
                            <div class="biz-card-b"></div>
                        </a>
                        <a class="biz-total-card">
                            <div class="biz-card-t">
                                <strong class="biz-card-tl"><asp:Literal runat="server" ID="liDoneOrderCount"></asp:Literal></strong>
                                <div class="biz-card-tr">
                                    <p>张</p>
                                    <p>已完成订单</p>
                                </div>
                            </div>
                            <div class="biz-card-b"></div>
                        </a>
                    </div>
                </div>
                <div class="d-hr"></div>
                <div class="row">
                    <div class="col-md-12">
                        <h3 class="biz-detail-head">店铺基本信息</h3>
                    </div>
                </div>
                <div class="row m-b20">
                    <div class="col-md-9">
                        <div class="model">
                            <div class="model-h">
                                <h4>基本信息</h4>
                            </div>
                            <div class="model-m">
                                <div class="biz-detail-wrap">
                                    <div class="row m-b20">
                                        <div class="col-md-1 model-img">
                                            <div class="t-c m-b20">
                                                <img class="business-detail-face"
                                                     src='<%=CurrentBusiness.BusinessAvatar.Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(CurrentBusiness.BusinessAvatar.ImageName)+"&width=70&height=70&tt=3":"../images/common/touxiang/touxiang_70_70.png"%>'/>
                                                <!--<p class="t-c business-detail-name"><%=CurrentBusiness.Name %></p>-->
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra"><span class="model-pra-t">网址邮箱</span><%=string.IsNullOrEmpty(CurrentBusiness.WebSite)?"无" :CurrentBusiness.WebSite %></p>
                                            <p class="model-pra"><span class="model-pra-t">联系电话</span><%=CurrentBusiness.Phone %></p>
                                            <p class="model-pra"><span class="model-pra-t">店铺介绍</span><span class="text-breakWord"><%=CurrentBusiness.Description %></span></p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra"><span class="model-pra-t">从业时间</span><%=CurrentBusiness.WorkingYears %>&nbsp;年</p>
                                            <p class="model-pra"><span class="model-pra-t">员工人数</span><%=CurrentBusiness.StaffAmount %>&nbsp;人</p>
                                            <p class="model-pra"><span class="model-pra-t">店铺地址</span><%=string.IsNullOrEmpty( CurrentBusiness.Address )? "无" :
                                                CurrentBusiness.Address %></p>
                                        </div>

                                    </div>
                                    <div class="d-hr in"></div>
                                    <div class="row">
                                        <div class="col-md-1 model-img"></div>
                                        <div class="col-md-4">
                                            <p class="model-pra"><span class="model-pra-t">店主姓名</span>
                                                <%=CurrentBusiness.Contact %></p>
                                            <p class="model-pra"><span class="model-pra-t">证件类型</span>
                                                <%=CurrentBusiness.ChargePersonIdCardType.ToString() %></p>
                                        </div>
                                        <div class="col-md-4">
                                            <p class="model-pra"><span class="model-pra-t">证件号码</span><%=CurrentBusiness.ChargePersonIdCardNo %></p>
                                        </div>
                                        <div class="col-md-12">
                                            <a class="btn btn-gray-light fr" href="/Business/edit.aspx?businessId=<%=CurrentBusiness.Id %>">编辑</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="model">
                            <div class="model-h">
                                <h4>统计图表</h4>
                            </div>
                            <div class="model-m">
                                <div id="biz-total-chart">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="model">
                            <div class="model-h">
                                <h4>店铺图片</h4>
                            </div>
                            <div class="model-m">
                                <div class="p-20 detail-img">
                                    <asp:Repeater runat="server" ID="rptCharge">
                                        <ItemTemplate>
                                            <a class="m-r20" data-lightbox="lb_charge"
                                               href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img
                                                    src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3'/>
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="model">
                            <div class="model-h">
                                <h4>负责人证件照</h4>
                            </div>
                            <div class="model-m">
                                <div class="p-20 detail-img">
                                    <asp:Repeater runat="server" ID="rptShow">
                                        <ItemTemplate>
                                            <a class="" data-lightbox="lb_show"
                                               href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img
                                                    src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3'/>
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="model">
                            <div class="model-h">
                                <h4>营业执照</h4>
                            </div>
                            <div class="model-m">
                                <div class="p-20 detail-img">
                                    <asp:Repeater runat="server" ID="rptImageLicense">
                                        <ItemTemplate>
                                            <a class="m-r20" data-lightbox="lb_license"
                                               href='<%#Config.BusinessImagePath+"/original/"+Eval("ImageName") %>'> <img
                                                    src='/ImageHandler.ashx?imagename=<%#Eval("ImageName")%>&width=120&height=120&tt=3'/>
                                            </a>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
    <script src="/js/echarts.simple.min.js"></script>
    <script>
        (function (){
            var myChart = echarts.init(document.getElementById("biz-total-chart"));
            var option = {
                title : {
                    text: '南丁格尔玫瑰图',
                    subtext: '纯属虚构',
                    left:'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },
                legend: {
                    orient: 'vertical',
                    left: 'center',
                    data:['已完成订单','未完成订单']
                },
                series: [
                    {
                        name:'访问来源',
                        type:'pie',
                        radius: ['50%', '70%'],
                        avoidLabelOverlap: false,
                        label: {
                            normal: {
                                show: false,
                                position: 'center'
                            },
                            emphasis: {
                                show: true,
                                textStyle: {
                                    fontSize: '30',
                                    fontWeight: 'bold'
                                }
                            }
                        },
                        labelLine: {
                            normal: {
                                show: false
                            }
                        },
                        data:[
                            {value:0, name:'已完成订单'},
                            {value:0, name:'未完成订单'}
                        ]
                    }
                ]
            };
            myChart.setOption(option);
        })()
    </script>
</asp:Content>

