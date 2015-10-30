<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="SendSysNotice.aspx.cs" Inherits="SendSysNotice" %>
<asp:Content runat="server" ContentPlaceHolderID="RightMain">
     <div>
         <asp:TextBox runat="server" ID="tbxContent" TextMode="MultiLine">

         </asp:TextBox>
     </div>
    <div>
        <asp:Button runat="server" ID="btnSend" Text="发送" OnClick="btnSend_Click" />
        <asp:Label runat="server" ID="lblResult"></asp:Label>
    </div>

</asp:Content>
<%-- Add content controls here --%>
