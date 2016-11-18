<%@ Page Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="PayWithdrawCash.aspx.cs" Inherits="finance_PayWithdrawCash" %>

<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
    提现付款
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView runat="server" AutoGenerateColumns="false" ID="gvWithdrawApply" >
        <Columns>
            <asp:TemplateField >
                <HeaderTemplate>
                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkOne" runat="server" />
                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="提现用户Id">
                <ItemTemplate>
                   <%-- <a href='Add.aspx?id=<%#Eval("ApplyUserId") %>' ><%#Eval("ApplyUserId") %></a>--%>
                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("ApplyUserId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="收款账户" DataField="ReceiveAccount.Account" />
            <asp:BoundField HeaderText="账户姓名" DataField="ReceiveAccount.AccountName" />
            <asp:BoundField HeaderText="提现金额" DataField="ApplyAmount" />
            <asp:BoundField HeaderText="手续费" DataField="ServiceFee" />
            <asp:BoundField HeaderText="转账金额" DataField="TransferAmount" />  
            <asp:BoundField HeaderText="申请时间" DataField="ApplyTime" />          
           <%-- <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href='Add.aspx?id=<%#Eval("Id") %>' >编辑</a>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
    <asp:Button ID="Button1" runat="server" Text="支付" Width="128px" OnClick="Button1_Click" />
    <%--<UC:AspNetPager runat="server" UrlPaging="true" ID="pager" CssClass="anpager"
        CurrentPageButtonClass="cpb" PageSize="10"
        CustomInfoHTML="第 %CurrentPageIndex% / %PageCount%页 共%RecordCount%条"
        ShowCustomInfoSection="Right">
    </UC:AspNetPager>--%>
</asp:Content>
