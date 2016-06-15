<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="business_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
商家管理
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <fieldset><legend>总览</legend>
        <div>
           <span>注册总数:</span><asp:Label runat="server" ID="lblTotalRegister"></asp:Label>
        </div>
         <div>
           <span>当前在线:</span><asp:Label runat="server" ID="lblTotalOnline"></asp:Label>
        </div>
    </fieldset>
   
    <div>
    <asp:GridView CssClass="tbList" AutoGenerateColumns="false" runat="server" ID="gvBusiness"  AllowSorting="true" OnSorting="gvBusiness_Sorting" >
        <Columns>
           
            <asp:BoundField HeaderText="商家名称" DataField="BusinessName" HeaderStyle-Width="150px" SortExpression="BusinessName"/>
             <asp:BoundField HeaderText="所在城市" DataField="CityName" HeaderStyle-Width="50px" SortExpression="CityName"/>
            <asp:BoundField HeaderText="得分" DataField="Score" HeaderStyle-Width="120px" SortExpression="Score"/>
            <asp:BoundField HeaderText="服务类别" DataField="ServiceTypesDisplay" HeaderStyle-Width="130px" SortExpression="ServiceTypesDisplay"/>
            <asp:BoundField HeaderText="注册时间" DataField="RegisterTime" HeaderStyle-Width="130px" SortExpression="RegisterTime"/>
            <asp:BoundField HeaderText="订单总数" DataField="OrderCount" HeaderStyle-Width="130px" SortExpression="OrderCount"/>
            <asp:BoundField HeaderText="完成总数" DataField="OrderCompleteCount" HeaderStyle-Width="130px" SortExpression="OrderCompleteCount"/>
            <asp:BoundField HeaderText="取消总数" DataField="OrderCancelCount" HeaderStyle-Width="130px" SortExpression="OrderCancelCount"/>

          </Columns>
    </asp:GridView>

    <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
        CurrentPageButtonClass="cpb" PageSize="10"
        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
        ShowCustomInfoSection="Right">
    </UC:AspNetPager></div>

     <%--<input type="button" id="btnExport2Excel" value="导出Excel" />--%>
   <asp:Button ID="btnToExcel" runat="server" Text="ToExcel" OnClick="btnToExcel_Click" />
</asp:Content>


<asp:Content ContentPlaceHolderID="foot" runat="server">
    <script>
        $(function () {
            $("#btnExport2Excel").click(
                function () {
                    $(".tbList").table2excel({
                        exclude: ".noExl",
                        name: "商家列表",
                        filename: "商家列表.xls",
                        fileext: ".xls",

                    });
                }
            );

            
        });
    </script>
</asp:Content>

