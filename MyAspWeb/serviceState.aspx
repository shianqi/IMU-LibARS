<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="serviceState.aspx.cs" Inherits="MyAspWeb.serviceState" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport"
          content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
    <link rel="stylesheet" href="//bootswatch.com/journal/bootstrap.min.css">
</head>
<body>
<div id="main" class="panel-body">
    <!-- Standard button -->
    <form method="post" runat="server">
        服务状态：<asp:Button ID="Button1" runat="server" Text="操作中" CssClass="btn" OnClick="Button1_Click"/>
    </form>
</div>
</body>
<script src="//cdn.bootcss.com/jquery/1.11.3/jquery.min.js"></script>
<script src="//cdn.bootcss.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</html>