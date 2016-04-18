<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerSummary.ascx.cs" Inherits="customer_CustomerSummary" %>
<fieldset><legend>总览</legend>
        <div>
           <span>注册总数:</span><asp:Label runat="server" ID="lblTotalRegister"></asp:Label>
        </div>
         <div>
           <span>当前在线:</span><asp:Label runat="server" ID="lblTotalOnlien"></asp:Label>
        </div>
    </fieldset>