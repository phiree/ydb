<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="servicetype_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #mainContent span{
            margin: 5px;
            display: inline-block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <%--   <asp:GridView runat="server" ID="gvServiceType">
        <Columns>
            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" />
            <asp:HyperLinkField Text=" 属性" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="service_property.aspx?typeid={0}" />
        </Columns>
    </asp:GridView>--%>
    <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
            <h1>
                <asp:HyperLink ID="hy" runat="server"></asp:HyperLink></h1>
            <asp:Repeater runat="server">
                <ItemTemplate>
                    <span>
                        <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink></span>
                    <asp:Repeater runat="server">
                        <ItemTemplate>
                            <span>
                                <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink> </span>
                            <asp:Repeater runat="server">
                                <ItemTemplate>
                                  <span>
                                        <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink></span>
                                    <asp:Repeater runat="server">
                                        <ItemTemplate>
                                          <span>
                                                <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink></span>
                                            <asp:Repeater runat="server">
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
    <hr/>
    <a href="Edit.aspx">增加新类别</a>
    <asp:FileUpload runat="server" ID="fu" /><asp:Button runat="server" ID="btnUpload"
        Text="导入" OnClick="btnUpload_Click" />
</asp:Content>
