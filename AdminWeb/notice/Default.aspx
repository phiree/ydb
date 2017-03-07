<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="notice_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView runat="server" ID="gvNotice"  OnRowCommand="gvNotice_RowCommand">
        <Columns>
              <asp:BoundField HeaderText="用户名" DataField="UserName" SortExpression="UserName" />
               <asp:ButtonField CommandName="Approve"  Text="审核通过并发送"/>
            <asp:TemplateField>
                <ItemTemplate>
                  <label> 拒绝原因:</label>  <asp:TextBox runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:ButtonField CommandName="Refuse"  Text="拒绝"/>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

