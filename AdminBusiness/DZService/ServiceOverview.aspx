<%@ Page Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="ServiceOverview.aspx.cs" Inherits="DZService_ServiceOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
        <div class="content-head normal-head">
            <h3>服务货架总览</h3>
        </div>
        <div class="content-main">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="model">
                            <div class="model-h">
                                <input type="text" value="搜索">
                            </div>
                            <div class="model-m no-padding">
                                <div class="shelf-overview">
                                    <div class="shelf-box">
                                        <div class="shelf-box-bg">
                                            <div class="-bg-t"></div>
                                            <div class="box-bg-b"></div>
                                        </div>
                                        <div class="shelf-box-m">
                                            <div class="box-m-bg"></div>
                                            <div class="box-m-t"></div>
                                            <div class="box-m-icon"></div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server" >

</asp:Content>