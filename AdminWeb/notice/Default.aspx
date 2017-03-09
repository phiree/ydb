<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="notice_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView runat="server" AutoGenerateColumns="false" ID="gvNotice" OnRowCommand="gvNotice_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="发布者" DataField="AuthorId" SortExpression="AuthorId" />
            <asp:BoundField HeaderText="公告标题" DataField="Title" SortExpression="Title" />
            <asp:BoundField HeaderText="公告内容" DataField="Body" SortExpression="Body" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Panel ID="pnlChecked"  runat="server" Visible='<%#!(bool)Eval("IsApproved")%>'>
                    <asp:Button runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="Approve" Text="审核通过,并发送" />
                        <div>
                    
                  
                    <asp:Button runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="Refuse" Text="拒绝" />
                             拒绝原因: <asp:TextBox ID="tbxRefuseReason" runat="server"></asp:TextBox>
                            </div>
                        </asp:Panel>
                    <asp:Panel runat="server" ID="pnlNotChecked"  Visible='<%#(bool)Eval("IsApproved")%>'>
                        已推送.
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="Server">
</asp:Content>

