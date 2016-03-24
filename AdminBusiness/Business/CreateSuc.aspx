<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="CreateSuc.aspx.cs" Inherits="Business_CreateSuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="cont-wrap">
        <div class="mh-in">
            <div class="cont-container">
                <div class="cont-row">
                    <div class="cont-col-12">
                        <div class="cont-col-12 t-c m-b20">
                            <img src="../image/banner.png" alt="恭喜你">
                        </div>
                        <div class="cont-col-12">

                            <p class="create-suc t-c">恭喜你，成功创建新的商铺！</p>

                        </div>
                    </div>
                </div>
                <div class="cont-row">
                    <div class="cont-col-12">
                        <p class=" t-c"><a class="create-continue"
                                           href="/Business/edit.aspx?businessId=<%=CurrentBusiness.Id%>">继续完善你的店铺资料。>></a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="register-wrap">
        <form runat="server">
            <div class="register-section">
                <div class="register-brand-c">
                    <img id="register-logo" src="../images/pages/register/logo_100x100.png" alt="logo" />
                    <div class="brand-head">
                        <h1 class="cssc0a9477146a8">一点办商户管理系统</h1>
                        <p class="cssc0a50b3d46a8">静心观天下·才能发现世界的精彩</p>
                    </div>
                </div>
            </div>
            <div class="register-section">
                <div class="register-panel-c">
                    <div class="reg-model">
                        <div class="reg-model-h">

                        </div>
                        <div class="reg-model-m">

                        </div>
                    </div>
                </div>
            </div>
        </form>
        <div class="footer">
            <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights Reserved
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

