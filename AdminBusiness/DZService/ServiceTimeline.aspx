<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="ServiceTimeline.aspx.cs" Inherits="DZService_ServiceTimeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/less/timetable.css" rel="stylesheet" type="text/css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cont-wrap">
        <div class="mh-in">
            <div class="cont-container animated fadeInUpSmall">
                <div class="mh-ctnr">
                    <div class="time-head">
                        <p><asp:Label runat="server" id="lblServiceName"></asp:Label></p>
                    </div>
                    <div class="timetable-container">
                        <div class="timetable-title">
                           <i class="icon timetable-title-icon"></i> 一周服务时间总览
                        </div>
                        <div class="timetable">
                            <div class="tt-side">
                                <div class="tt-side-background"></div>
                                <div class="tt-side-content">
                                    <div class="tt-side-head"></div>
                                    <div class="tt-side-body">
                                        <div class="tt-side-rows">
                                            <div class="tt-side-row tt-row">星期一</div>
                                            <div class="tt-side-row tt-row">星期二</div>
                                            <div class="tt-side-row tt-row">星期三</div>
                                            <div class="tt-side-row tt-row">星期四</div>
                                            <div class="tt-side-row tt-row">星期五</div>
                                            <div class="tt-side-row tt-row">星期六</div>
                                            <div class="tt-side-row tt-row">星期日</div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="tt-main">
                                <div class="tt-head-scrollable">
                                    <div class="tt-head">

                                        <div class="tt-head-columns">

                                        </div>

                                    </div>
                                </div>
                                <div class="tt-body-scrollable">
                                    <div class="tt-body">
                                        <div class="tt-body-background">

                                        </div>
                                        <div class="tt-body-foreground">


                                        </div>
                                        <div class="tt-body-columns">

                                        </div>
                                        <div class="tt-body-rows">
                                        </div>
                                    </div>
                                </div>
                                <div class="tt-main-bg">

                                    <div class="main-bg-head">


                                    </div>
                                    <div class="main-bg-body">
                                        <div class="tt-row tt-row-bg"></div>
                                        <div class="tt-row tt-row-bg"></div>
                                        <div class="tt-row tt-row-bg"></div>
                                        <div class="tt-row tt-row-bg"></div>
                                        <div class="tt-row tt-row-bg"></div>
                                        <div class="tt-row tt-row-bg"></div>
                                        <div class="tt-row tt-row-bg"></div>
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
    <script src="/js/jquery.timetable.js"></script>
    <script src="/js/json2.js"></script>
    <script>
        
        var jsonDataStr = '<%=jsonData%>';
        var jsonData = JSON.parse(jsonDataStr);

        
        $('.timetable').TimeTable({
            taskData: jsonData
        });

    </script>
</asp:Content>

