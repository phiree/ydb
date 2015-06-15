<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="DZService_Edit" %>

<%@ Import Namespace="Dianzhu.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <link href='<% = ConfigurationManager.AppSettings["cdnroot"] %>/static/Scripts/jqueryui/themes/base/minified/jquery-ui.min.css' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td colspan="2">
             <div id="tabsServiceType">
                            <ul> </ul>
                             
                        </div>
                        <input type=hidden id="hiTypeId" clientidmode="Static" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                服务名称
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                详细描述
            </td>
            <td>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="tbxDescription"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                服务分类
            </td>
            <td>
               
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptProperties">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("Name") %>
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblValues" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="保存" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="bottom" runat="server">
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>
    
 <script src="/js/TabSelection.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#tabsServiceType").TabSelection({
            "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
            "leaf_clicked": function (id) {
                $("#hiTypeId").val(id);
            }

        });
    });
</script>
</asp:Content>