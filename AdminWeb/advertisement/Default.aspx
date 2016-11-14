<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="advertisement_Default" %>
 
<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
    广告管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <a href="/advertisement/Add.aspx">添加广告</a>
    <asp:GridView runat="server" AutoGenerateColumns="false" ID="gvMember" >
        <Columns>
            <asp:BoundField HeaderText="序号" DataField="Num" />
            <asp:TemplateField HeaderText="是否激活">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="litType"></asp:Literal>
                    <asp:HyperLink runat="server" ID="hlRelative"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="图片预览">
                <ItemTemplate>
                    <img runat="server" id="imgAdv" width="100"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="连接地址" DataField="Url" />
            <asp:BoundField HeaderText="推送目标的标签" DataField="PushTarget" />
            <asp:BoundField HeaderText="开始时间" DataField="StartTime" />
            <asp:BoundField HeaderText="结束时间" DataField="EndTime" />
            <asp:TemplateField HeaderText="显示平台">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="litViewType"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>         
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href='Add.aspx?id=<%#Eval("Id") %>' >编辑</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
        CurrentPageButtonClass="cpb" PageSize="10"
        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
        ShowCustomInfoSection="Right">
    </UC:AspNetPager>
</asp:Content>
