<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="membership_roles_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField DataField="Length" HeaderText="Length" ReadOnly="True" SortExpression="Length" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="DeleteRole" InsertMethod="CreateRole" OnSelecting="ObjectDataSource1_Selecting" SelectMethod="GetAllRoles" TypeName="Dianzhu.BLL.IdentityAccess.RoleService">
        <DeleteParameters>
            <asp:Parameter Name="roleName" Type="String" />
            <asp:Parameter Name="throwOnPopulatedRole" Type="Boolean" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="roleName" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>

