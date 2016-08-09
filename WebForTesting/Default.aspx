<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater runat="server" ID="rptBusinessList"
                                                              >
                                                    <ItemTemplate>
                                                        <li class="biz-list-item">
                                                            <div class="biz-head">店铺</div>
                                                            <div class="biz-item-main">
                                                                <div class="biz-item-h"><%#Eval("Name")%></div>
                                                                <div class="biz-item-m">
                                                                    <div class="biz-img">
                                                                        <img src='<%# ((Dianzhu.Model.BusinessImage)Eval("BusinessAvatar")).Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(((Dianzhu.Model.BusinessImage)Eval("BusinessAvatar")).ImageName)+"&width=70&height=70&tt=3":"../images/common/touxiang/touxiang_70_70.png" %>'/>
                                                                    </div>
                                                                    <div class="biz-info m-b10">
                                                                        <div class="m-b10">
                                                                            <div class="biz-info-h">店铺地址</div>
                                                                            <div class="biz-info-d">
                                                                                <%#Eval("Address")%></div>
                                                                        </div>
                                                                        <div class="m-b10">
                                                                            <div class="biz-info-h">店铺电话</div>
                                                                            <div class="biz-info-d d-p">
                                                                                <%#Eval("Phone")%></div>
                                                                        </div>
                                                                        <div>
                                                                            <div class="biz-info-h">店铺介绍</div>
                                                                            <div class="biz-info-d l-h18 biz-intro-fixed">
                                                                                <%#Eval("Description")%></div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="biz-href">
                                                                        <a class="biz-into" href='Detail.aspx?businessId=<%#Eval("Id") %>'>进入店铺</a>
                                                                        <asp:Button CssClass="biz-delete"
                                                                                    text="删除店铺"
                                                                                    title="删除店铺"
                                                                                    runat="server"
                                                                                    OnClientClick="javascript:return confirm('确定要删除该店铺么?')"
                                                                                    CommandArgument='<%#Eval("Id") %>'
                                                                                    CommandName="delete"/>
                                                                    </div>

                                                                </div>
                                                                <div class="biz-item-svc">
                                                                    <ul class="svc-list clearfix">
                                                                        <asp:Repeater runat="server"
                                                                                      ID="rptServiceType">
                                                                            <ItemTemplate>
                                                                                <li class='svc-li'>
                                                                                    <a class='svc-icon svcType-b-icon-<%#Eval("Id") %>'></a>
                                                                                    <div class="svc-item-h">
                                                                                        <%#Eval("Name") %>
                                                                                    </div>
                                                                                </li>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <!--</div>-->
                                                            <div class="d-hr"></div>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
    </div>
    </form>
</body>
</html>
