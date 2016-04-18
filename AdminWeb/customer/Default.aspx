<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="membership_Default" %>

<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
    用户管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset><legend>总览</legend>
        <div>
           <span>注册总数:</span><asp:Label runat="server" ID="lblTotalRegister"></asp:Label>
        </div>
         <div>
           <span>当前在线:</span><asp:Label runat="server" ID="lblTotalOnline"></asp:Label>
        </div>
    </fieldset>
    
     <asp:GridView runat="server" AllowSorting="true" OnSorting="gvMember_Sorting" AutoGenerateColumns="false" ID="gvMember">
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

    <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
        CurrentPageButtonClass="cpb" PageSize="10"
        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
        ShowCustomInfoSection="Right">
    </UC:AspNetPager>
</asp:Content>
