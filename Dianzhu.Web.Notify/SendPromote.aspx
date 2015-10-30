<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="SendPromote.aspx.cs" Inherits="SendPromote" %>



<asp:Content ID="Content1" ContentPlaceHolderID="RightMain" Runat="Server">
 <div>
        促销链接: <asp:TextBox runat="server" ID="tbxContent" Width="447px"></asp:TextBox>
     </div>
    <div>
        <asp:Button runat="server" ID="btnSend" Text="发送" OnClick="btnSend_Click" />
        <asp:Label runat="server" ID="lblResult"></asp:Label>
    </div>
</asp:Content>

