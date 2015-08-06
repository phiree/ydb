<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagControl.ascx.cs" Inherits="TagControl" %>
<asp:Repeater runat="server" ID="rptTags">
<ItemTemplate>
<span class="sptag" tagid='<%#Eval("Id") %>'><%#Eval("Text") %></span>
</ItemTemplate>
</asp:Repeater>
<input type="text"  class="iptag" placeholder="添加新标签" />
<script type="text/javascript">
    $(function () {
        $(".iptag").keypress(function (ev) {
            if (ev.which == 13) {
                ev.preventDefault();
                var that = this;
                var newTag = $(this).val();
                var serviceId = "<%=ServiceId %>"
                $.post(
                    "/ajaxservice/taghandler.ashx",
                    {
                        "tagText": newTag,
                        "serviceId": serviceId
                    },
                    function (result) {
                        var newtag = $("<span class='sptag' tagid='" + result + "'>" + newTag + "</span>");
                        newtag.insertAfter(that);
                    }
                ); //post
            } //if (ev.which == 13) {
        }); // $(".iptag").keypress(function (ev) {
    });
</script>