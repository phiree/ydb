<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="notice_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView runat="server" ID="gvNotice"  OnRowCommand="gvNotice_RowCommand">
        <Columns>
              <asp:BoundField HeaderText="发布者" DataField="AuthorId" SortExpression="AuthorId" />
            
              <asp:TemplateField>
                <ItemTemplate>
                 <asp:Button runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="Approve" Text="审核通过,并发送" />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                  <label> 拒绝原因:</label>  <asp:TextBox ID="tbxRefuseReason" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField>
               <ItemTemplate>
                 <asp:Button runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="Refuse" Text="拒绝" />
                </ItemTemplate>
            </asp:TemplateField>
              
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

