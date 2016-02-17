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
                                <input type="text" value="搜索"/>
                                <input type="button" value="搜索"/>
                                <span>当前共10服务</span>
                            </div>
                            <div class="model-m no-padding">
                                <div class="shelf-overview-wrap">
                                    <div class="shelf-overview" id="shelfOverview">
                                        <asp:Repeater runat="server" ID="rptServiceList">
                                            <ItemTemplate>
                                            <div class="shelf-box">
                                                <div class="shelf-box-bg">
                                                    <div class="box-bg-t"></div>
                                                    <div class="box-bg-b"></div>
                                                </div>
                                                <div class="shelf-box-m">
                                                    <div class="box-m-t"><%# Eval("Name") %></div>
                                                    <a href='/DZService/ServiceShelf.aspx?serviceid=<%# Eval("Id") %>' class="box-m-b">
                                                        <div class="box-m-bg"></div>
                                                        <div class='box-m-icon svcType-l-icon-<%#((Dianzhu.Model.DZService)GetDataItem()).ServiceType.TopType.Id  %>'></div>
                                                    </a>
                                                </div>
                                            </div>
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
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server" >
    <script>
        $(function(){
            /* 每行补空样式 */
            var goodsLength = $('#shelfOverview').find('.shelf-box').length;
            var restGoods = 6 - goodsLength % 6;

            if ( restGoods != 0 ) {
                for ( var i = 0 ; i < restGoods ; i ++ ){
                    var emptyGood = $('<div class="shelf-box empty-box"><div class="shelf-box-bg"><div class="box-bg-t"></div><div class="box-bg-b"></div></div></div>');
                    $('.shelf-overview').append(emptyGood);
                }
            }
        })
    </script>
</asp:Content>