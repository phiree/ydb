﻿
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payfail.aspx.cs" Inherits="FailInvoke" %>

<html lang="en">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办支付" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>商家后台主页</title>
    <link href="css/mobilepay.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="wrapper">
    <div class="container">
        <div class="topbar">
            支付失败
        </div>
        <div class="main">
            <div class="status-wrap">
                <div class="pay-status">
                    <div class="pay-status-icon">
                        <div class="icon pay-icon-fail"></div>
                    </div>
                    <div class="pay-h">
                        支付失败
                    </div>
                    <div class="pay-text">
                        您的订单支付失败，请返回重试。
                    </div>
                </div>
                <div class="pay-status-back">
                    <a href="#" class="pay-status-button">返回一点办</a>
                </div>
            </div>
        </div>
    </div>
</div>
</body>
</html>