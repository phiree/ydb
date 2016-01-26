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
                                        <div class="service-panel-head">
                                            <div class="custom-grid">
                                                <div class="custom-col col-10-1">
                                                    <div class="service-p-h-t">
                                                        服务时间
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="service-p-h-t">
                                                        服务项目
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="service-p-h-t">
                                                        订单号
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="service-p-h-t">
                                                        客户名称
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-3">
                                                    <div class="service-p-h-t">
                                                        服务地址
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-1">
                                                    <div class="service-p-h-t">
                                                        员工指派
                                                    </div>
                                                </div>
                                                <div class="custom-col col-10-2">
                                                    <div class="service-p-h-t">
                                                        订单状态
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="panel-list service-panel-list" id="accordion" role="tablist" aria-multiselectable="true">

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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
    <script>

    </script>
</asp:Content>