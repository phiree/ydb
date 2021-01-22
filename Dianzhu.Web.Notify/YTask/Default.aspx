<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="YTask_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightMain" Runat="Server">

<asp:GridView runat="server" ID="gvJobs">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Container.DisplayIndex %>_ <%# Container.DataItemIndex %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    
</asp:Content>

