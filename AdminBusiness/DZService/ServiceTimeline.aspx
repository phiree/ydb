<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="ServiceTimeline.aspx.cs" Inherits="DZService_ServiceTimeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/less/timetable.css" rel="stylesheet" type="text/css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="timetable-container">
        <div class="timetable">
            <div class="tt-side">
                <div class="tt-side-background"></div>
                <div class="tt-side-content">
                    <div class="tt-side-head"></div>
                    <div class="tt-side-body">
                        <div class="tt-side-rows">
                            <div class="tt-side-row tt-row">星期1</div>
                            <div class="tt-side-row tt-row">星期1</div>
                            <div class="tt-side-row tt-row">星期1</div>
                            <div class="tt-side-row tt-row">星期1</div>
                            <div class="tt-side-row tt-row">星期1</div>
                            <div class="tt-side-row tt-row">星期1</div>
                            <div class="tt-side-row tt-row">星期1</div>
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


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageDesc" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bottom" Runat="Server">
     <script src="/js/jquery.timetable.js"></script>
    <script src="/js/json2.js"></script>
    <script>
        
        var jsonDataStr ='<%=jsonData%>';
        var jsonData = JSON.parse(jsonDataStr);
        var data = {};
        data.taskData = jsondata;
        
        $('.timetable').TimeTable(taskData);
        /*
        {
            taskData : [
                {
                    weekday : 1,
                    openTime : [
                        {
                            beginTime : "9:00",
                            endTime : "12:00"
                        },
                        {
                            beginTime : "13:00",
                            endTime : "24:00"
                        }
                    ],
                    msg : {}
                },
                {
                    weekday : 5,
                    openTime : [
                        {
                            beginTime : "2:00",
                            endTime : "10:00"
                        },
                        {
                            beginTime : "11:00",
                            endTime : "12:00"
                        }
                    ],
                    msg : {}
                }
            ]
        }
        */
    </script>
</asp:Content>

