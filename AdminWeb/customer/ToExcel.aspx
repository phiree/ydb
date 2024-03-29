﻿<%@ Page Language="C#"  MasterPageFile="~/admin.master"  AutoEventWireup="true" CodeFile="ToExcel.aspx.cs" Inherits="customer_ToExcel" %>

<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
    用户管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <legend>总览</legend>
        <div>
            <span>注册总数:</span><asp:Label runat="server" ID="lblTotalRegister"></asp:Label>
        </div>
        <div>
            <span>当前在线:</span><asp:Label runat="server" ID="lblTotalOnline"></asp:Label>
        </div>
    </fieldset>

    <asp:GridView CssClass="tbList" OnRowDataBound="gvMember_RowDataBound" runat="server"  AutoGenerateColumns="false" ID="gvMember">
        <Columns>
            <asp:BoundField HeaderText="用户名" DataField="UserName" SortExpression="UserName" />
            <asp:BoundField HeaderText="电话" DataField="Phone" SortExpression="Phone" />
            <asp:BoundField HeaderText="邮箱" DataField="Email" SortExpression="Email" />
            <asp:BoundField HeaderText="注册时间" DataField="TimeCreated" SortExpression="TimeCreated" />
            <asp:BoundField HeaderText="登录天数" DataField="LoginDates" SortExpression="LoginDates" />
            <asp:BoundField HeaderText="上线率" DataField="LoginRate" SortExpression="LoginRate" />
            <asp:BoundField HeaderText="登录次数" DataField="LoginTimes" SortExpression="LoginTimes" />
            <asp:BoundField HeaderText="用户类型" DataField="FriendlyUserType" SortExpression="FriendlyUserType" />
            <asp:BoundField HeaderText="呼叫次数" DataField="CallTimes" SortExpression="CallTimes" />
            <asp:BoundField HeaderText="下单次数" DataField="OrderCount" SortExpression="OrderCount" />
            <asp:BoundField HeaderText="下单金额" DataField="OrderAmount" SortExpression="OrderAmount" />


        </Columns>
    </asp:GridView>

    
    <div>
        <input type="button" id="btnExport2Excel" value="导出Excel" /><asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" /></div>
</asp:Content>
<asp:Content ContentPlaceHolderID="foot" runat="server">
    <script>
       
        $(function () {
            $("#btnExport2Excel").click(
                function () {
                    $(".tbList").table2excel({
                        exclude: ".noExl",
                        name: "客户列表",
                        filename: "客户列表.xls",
                        fileext: ".xls",

                    });
                }
            );

            
        });
    </script>
</asp:Content>
