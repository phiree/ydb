<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagControl.ascx.cs" Inherits="TagControl" %>
<p class="m-b10">
    <input type="text" id="ipTag" class="input-fluid spTagInput m-b10" serviceid="<%=ServiceId %>" placeholder="添加新标签" />
    <input type="button" id="ipTagAdd" class="btn btn-info" value="添加标签" />
</p>    
<p class="ipTagContainer">
    <asp:Repeater runat="server" ID="rptTags">
        <ItemTemplate>
            <span class="spTag d-inb">
                <span class='spTagText' tagid='<%#Eval("Id") %>'><%#Eval("Text")%></span>
                <input class='spTagDel' type='button' tagid='<%#Eval("Id") %>' value='删除' />
            </span>
        </ItemTemplate>
    </asp:Repeater>
</p>

