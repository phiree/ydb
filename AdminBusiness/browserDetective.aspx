﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="browserDetective.aspx.cs" Inherits="browserDetective" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html {
            background: #263650;
        }

        .conten {
            margin: 0 auto;
            width: 786px;
            height: 790px;
            background: url(images/Browserbj.jpg) no-repeat;
            position: absolute;
            left: 50%;
            top: 50%;
            margin-top: -393px;
            margin-left: -395px;
            color: #FFF;
            font-size: 16px;
        }

        .txt {
            margin: 0 auto;
            width: 700px;
            margin-top: 230px;
            text-align: center;
            line-height: 40px;
            height: 40px;
            margin-bottom: 60px;
        }

        .txt span {
            font-size: 26px;
            margin-left: 5px;
            margin-right: 5px;
        }

        .btn {
            text-align: center;
        }

        .btn img {
            border: none;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="conten">
        <div class="txt">
            访客您好，一点半商户后台系统暂时不支持<span>IE8</span>及以下及其它低版本浏览器访问。 请您升级浏览器或使用<span>谷歌、火狐</span>等现代浏览器。如果您使用的是<span>360、傲游</span>等浏览器，请开启极速模式访问。
        </div>
    </div>
</form>
</body>
</html>
