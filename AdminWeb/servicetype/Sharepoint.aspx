<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Sharepoint.aspx.cs" Inherits="servicetype_Sharepoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Repeater runat="server" ID="rptPoints">
        <HeaderTemplate><table><thead><tr><td></td><td></td></tr></thead><tbody></HeaderTemplate>
        <ItemTemplate>
             <tr>
                 <td><asp:Literal runat="server" id="liTypeName"></asp:Literal>  </td><td>
                     <asp:Literal runat="server" id="liPoint"></asp:Literal>
                                                                                      </td>
             </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody></table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

