<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="area_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Repeater runat="server" ID="rp_province">
        <ItemTemplate>
            <h2>
                <%# DataBinder.Eval(Container.DataItem, "Name") %></h2>
            <div>
                <asp:Repeater runat="server" ID="rp_City">
                    <ItemTemplate>
                        <span style="font-weight:900;">
                            <%# DataBinder.Eval(Container.DataItem, "Name") %></span>
                        <div>
                            <asp:Repeater runat="server" ID="rpt_County">
                                <ItemTemplate>
                                    <span>
                                        <%# DataBinder.Eval(Container.DataItem, "Name") %></span>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
