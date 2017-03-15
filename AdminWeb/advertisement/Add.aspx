<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="advertisement_Add" %>

<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
    添加广告
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <asp:Table runat="server" ID="TableContect" CssClass="table_border">

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label1" runat="server" Text="序号"></asp:Label>                
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label2" runat="server" Text="上传图片"></asp:Label>                
            </asp:TableCell>
            <asp:TableCell>
                  <asp:Image runat="server" id="imgAdv" width="150"/>
                <asp:FileUpload ID="flupImg" runat="server" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label3" runat="server" Text="连接地址"></asp:Label>                
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label4" runat="server" Text="开始时间"></asp:Label>                
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label5" runat="server" Text="结束时间"></asp:Label>                
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label6" runat="server" Text="是否可用"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RadioButton ID="rdYes" runat="server" Text="可用" GroupName="radioIsUseful"/>
                <asp:RadioButton ID="rdNo" runat="server" Text="不可用" GroupName="radioIsUseful"/>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label8" runat="server" Text="推送目标"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RadioButton ID="rdCustomer" runat="server" Text="用户" GroupName="radioPushType"/>
                <asp:RadioButton ID="rdBusiness" runat="server" Text="商户" GroupName="radioPushType"/>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100">
                <asp:Label ID="Label7" runat="server" Text="推送目标的标签"></asp:Label>                
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtTarget" runat="server"></asp:TextBox><asp:Label runat="server">注：多个标签请用“|”分隔，如：标签甲|标签乙|标签丙</asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="btnSave" runat="server" Text="提交" OnClick="btnSave_Click"/>                
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblSaveSuccess" runat="server" Visible="false">保存成功！</asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
