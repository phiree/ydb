<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="test_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset>
        <legend>创建订单</legend>
        <div>
            客户名称:<asp:TextBox runat="server" ID="tbxCustomerName"></asp:TextBox>
            <asp:Button runat="server" OnClick="btnCreateOrder_Click" Text="生成" />
            <asp:Label runat="server" ID="lblCreateOrderResult"></asp:Label>
        </div>
    </fieldset>
</asp:Content>

