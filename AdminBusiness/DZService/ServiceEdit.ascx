<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceEdit.ascx.cs" Inherits="DZService_ServiceEdit" %>
 <table>
        <tr>
            <td colspan="2">
                <div id="tabsServiceType">
                    <ul>
                    </ul>
                </div>
                <input type="hidden" id="hiTypeId" clientidmode="Static" runat="server" />
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
                起步价
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxMinPrice"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                单价
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxUnitPrice"></asp:TextBox>
                每
                <asp:RadioButtonList runat="server" ID="rblChargeUnit">
                    <asp:ListItem Value="0" Text="小时"></asp:ListItem>
                    <asp:ListItem Value="1" Text="天"></asp:ListItem>
                    <asp:ListItem Value="2" Text="次"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                是否上门
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="rblServiceMode">
                    <asp:ListItem Value="0" Text="是"></asp:ListItem>
                    <asp:ListItem Value="1" Text="否"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                服务时间
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxServiceTimeBegin">8:30</asp:TextBox>
                至
                <asp:TextBox runat="server" ID="tbxServiceTimeEnd">21:00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                日最大接单量
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxMaxOrdersPerDay">50</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                每小时最大接单量
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxMaxOrdersPerHour">20</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                服务范围
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxServiceScope">王府井商圈</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                收款方式
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem Value="1" Text="线下"></asp:ListItem>
                    <asp:ListItem Value="2" Text="支付宝"></asp:ListItem>
                    <asp:ListItem Value="3" Text="微支付"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                服务保障
            </td>
            <td>
                <asp:CheckBox runat="server" ID="cbxIsCompensationAdvance" Text="加入先行赔付" />
            </td>
        </tr>
        <tr>
            <td>
                提前预约时长
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbxOrderDelay">60</asp:TextBox>分钟
            </td>
        </tr>
        <tr>
            <td>
                服务对象:
            </td>
            <td>
                <asp:CheckBoxList runat="server" ID="cblIsForBusiness">
                    <asp:ListItem Value="1" Text="对公"></asp:ListItem>
                    <asp:ListItem Value="2" Text="对私"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                平台标准认证
            </td>
            <td>
                <asp:CheckBox runat="server" ID="cbxIsCertificated" Text="已通过" />
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
            <td>
                详情描述
            </td>
            <td>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="tbxDescription"> </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="保存" />
            </td>
        </tr>
    </table>
    <script src="/js/TabSelection.js" type="text/javascript"></script>
    <script   type="text/javascript">
        $(function () {
            $("#tabsServiceType").TabSelection({
                "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
                "leaf_clicked": function (id) {
                    $("#hiTypeId").val(id);
                }

            });
        });
    </script>