﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagControl.ascx.cs" Inherits="TagControl" %>
<asp:Repeater runat="server" ID="rptTags">
<ItemTemplate>
<span class="sptag" tagid='<%#Eval("Id") %>'><%#Eval("Text") %></span>
</ItemTemplate>
</asp:Repeater>
<p class="m-b10"><input type="text" id="ipTag" class="iptag" serviceId="<%=ServiceId %>" placeholder="添加新标签" />
   <input class="btn btn-info" type="button" id="ipTagAdd" value="添加标签"/></p>

<p class="ipTagContainer"></p>
