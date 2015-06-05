<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Staff_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/employee.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainContent clearfix">
        <div id="leftCont" class="leftContent">
            <div>
                <ul>
                    <li><a href="#">
                        <img id="me1" src="/image/button_6.png" /></a></li>
                </ul>
            </div>
        </div>
        <div id="rightCont" class="rightContent">
            <div class="con-wrap">
                <div class="emp-wrap">
                    <div class="emp-con">
                        <div class="emp-bar clearfix">
                            <div class="emp-bar-t fl">
                                员工管理</div>
                            <div class="emp-bar-btn fr">
                                <input class="emp-btn-bg1" type="button" /><input class="emp-btn-bg2" type="button" />
                            </div>
                        </div>
                        <div class="emp-nav">
                            <div class="emp-nav-type clearfix">
                                <ul class="nav-type-l clearfix">
                                    <li><span>保姆类</span></li>
                                    <li><span>家政类</span></li>
                                    <li><span>通信服务类</span></li>
                                    <li><span>汽车维修类</span></li>
                                    <li><span>保洁类</span></li>
                                    <li><span>家政类</span></li>
                                    <li><span>维修类</span></li>
                                </ul>
                                <div class="nav-type-switch clearfix fr">
                                    <input class="emp-btn-bg3" id="switchPre" type="button" />
                                    <input class="emp-btn-bg4" id="switchNext" type="button" />
                                </div>
                            </div>
                            <div class="emp-nav-d">
                                <ul class="nav-d-l  clearfix">
                                    <li>月嫂</li>
                                    <li>保姆</li>
                                    <li>育儿嫂</li>
                                    <li>陪护</li>
                                    <li>涉外家政</li>
                                    <li>月保姆</li>
                                </ul>
                            </div>
                        </div>
                        <div class="emp-d">
                            <div class="emp-d-title">
                                <ul class="clearfix">
                                    <li class="col col-1">
                                        <input class="d-checkbox v-m" type="checkbox" value="全选" /><span>全选</span></li>
                                    <li class="col col-2">姓名</li>
                                    <li class="col col-3">性别</li>
                                    <li class="col col-4">年龄</li>
                                    <li class="col col-5">工作年限</li>
                                    <li class="col col-6">服务类别</li>
                                    <li class="col col-7">
                                        <div>
                                            <span><i class="icon search-btn"></i>
                                                <input type="button" value="搜索" /></span><span><i class="icon filter-btn"></i><input
                                                    type="button" value="筛选" /></span>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="emp-d-item">
                                <ul class="emp-d-list">
                                    <li class="d-list-i clearfix">
                                        <ul class="clearfix">
                                            <asp:Repeater runat="server" ID="rpt">
                                                <ItemTemplate>
                                                    <li class="col col-1">
                                                        <input type="checkbox" /><i class="icon user-pic"></i></li>
                                                    <li class="col col-2">
                                                        <%#Eval("Name") %></li>
                                                    <li class="col col-3">
                                                        <%#Eval("Gender") %></li>
                                                    <li class="col col-4">
                                                        <%#Eval("Age") %></li>
                                                    <li class="col col-5">
                                                        <%#Eval("WorkingYears") %></li>
                                                    <li class="col col-6"></li>
                                                    <li class="col col-7">
                                                        <div>
                                                            <input class="d-list-btn" type="button" /></div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                            <div class="emp-d-switch">
                                <div id="tagSwitch">
                                    <input class="d-switch-btn" type="button" value="0"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <p>
        职员列表</p>
    <asp:GridView runat="server" ID="gvStaff">
        <Columns>
            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" />
        </Columns>
    </asp:GridView>
    <a href="Edit.aspx">增加职员</a>
</asp:Content>
