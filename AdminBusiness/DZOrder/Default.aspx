<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZOrder_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content" id="service-list">
        <div class="content-head normal-head">
            <h3>订单列表</h3>
        </div>
        <div class="content-main">
            <div class="animated fadeInUpSmall">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-10">
                            <div class="">
                                <div class="model">
                                    <div class="model-h">
                                        <h4>订单列表</h4>
                                    </div>
                                    <div class="model-m no-padding">
                                        <div class="order-list-head">
                                            <div class="custom-grid">
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        服务时间
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        服务项目
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        订单号
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        客户名称
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-3">
                                                    <div class="l-b">
                                                        服务地址
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="l-b">
                                                        员工指派
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-2">
                                                    <div class="l-b">
                                                        订单状态
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="order-list" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="order-row">
                                                <div class="custom-grid">
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            2015-9-12
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            洗车
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            xxxxxx
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            林先生
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-3">
                                                        <div class="order-li">
                                                            北京市XXXXXXXXXX
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-1">
                                                        <div class="order-li">
                                                            <input type="button" value="指派员工" data-role="staffAptToggle" data-appointOrderId="8787984984613481846">
                                                        </div>
                                                    </div>
                                                    <div class="custom-col col-10-2">
                                                        <div class="order-li">
                                                            <span>处理中</span>
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
            </div>
        </div>
    </div>
    <div id="staffAppointLight" class="appointWindow">
        <div class="model">
            <div class="model-h">
                <h4>员工指派</h4>
            </div>
            <div class="model-m">
                <div id="staffsContainer" class="staffs-container">
                    <!-- 注入#staffs_temlate模版内容 -->
                </div>
            </div>
        </div>
        <div>
            <input id="appointSubmit" type="button" value="确定" data-role="appointSubmit" >
        </div>
    </div>
    <script type="text/template" id="staffs_template">
        <div class="container-fluid">
            <div class="row">
                {% _.each(staffId, function(staff){ %}
                <div class="col-md-4">
                    <div class="emp-model">
                        <div class="emp-model-h">
                            <span>员工编号:&nbsp;</span><span class="emp-code"></span>
                            <div class='emp-assign-flag'></div>
                        </div>
                        <div class="emp-model-m">
                            <img class="emp-headImg" src=''/>
                            <div class="emp-info">
                                <p>姓名：</p>
                                <p>工龄：</p>
                                <p>性别：</p>
                                <p>电话：</p>
                                <p>特长技能：</p>
                            </div>
                        </div>
                        <div class="emp-model-b">
                            <input type="checkbox" value="指派" data-role="staff" data-staffId="{% staff %}" >
                        </div>
                    </div>
                </div>

                {% }) %}
            </div>
        </div>
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
    <script src="/js/shelf/underscore.js"></script>
    <script src="/js/shelf/mock.js"></script>
    <script src="/js/jquery.lightBox_me.js"></script>
    <script src="/js/jquery.lightBox_me.js"></script>
    <script src="/js/staffAppoint.js"></script>
    <script>
        Mock.mockjax(jQuery);
        Mock.mock(/staff.json/, function(){
            return Mock.mock({
                'staffId|10-20' : [
                    '@guid'
                ]
            })
        })
    </script>
    <script>
        $('[data-role="staffAptToggle"]').staffAppoint({
            lightBox : function (){
                return $("#staffAppointLight").lightbox_me({
                    centered : true
                });
            },
            staffContainer : '#staffsContainer',
            appointSubmit : '#appointSubmit',
            single : true,
            pullReqData : { businessId : '132131321331' }
        })
    </script>
</asp:Content>