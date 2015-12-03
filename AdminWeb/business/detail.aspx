<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="business_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="600px"
        AutoGenerateRows="false" DataKeyNames="Id" FieldHeaderStyle-Width="100px">
        <Fields>
            <asp:BoundField DataField="Name" HeaderText="名称" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="Address" HeaderText="地址" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="Contact" HeaderText="联系人" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="Phone" HeaderText="联系电话" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="Email" HeaderText="邮箱" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="CreatedTime" HeaderText="创建时间" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="Description" HeaderText="简介" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="WebSite" HeaderText="网址" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="Owner.NickName" HeaderText="商家所属者" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="AreaBelongTo.Name" HeaderText="所属辖区" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="AreaServiceTo" HeaderText="服务范围" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="StaffAmount" HeaderText="员工总人数" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="WorkingYears" HeaderText="从业时长" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="DateApply" HeaderText="申请时间" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="IsApplyApproved" HeaderText="是否通过审核" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="DateApproved" HeaderText="审核通过时间" ReadOnly="true" InsertVisible="false"/>
            <asp:BoundField DataField="ApplyRejectMessage" HeaderText="审核拒绝信息" ReadOnly="true" InsertVisible="false"/>
        </Fields>
    </asp:DetailsView>
    <asp:Button runat="server" ID="btnApprove"  OnClick="btnApprove_Click"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

