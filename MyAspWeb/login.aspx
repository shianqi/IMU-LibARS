<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MyAspWeb.login" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=0.6,maximum-scale=0.8,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <title>S-Killer Project</title>
    <link href="../style/login.css" type=text/css rel=stylesheet />
    <script type="text/javascript">
        (function() {
            document.forms[0].TextBox1.focus();
        }());
        // 在被嵌套时就刷新上级窗口
        if (window.parent !== window) {
            alert("请重新登录");
            window.parent.location.reload(true);
        }
    </script>
</head>
<body>

<div class="login" >
    <div class="message">内蒙古大学图书馆自动预约系统</div>
    <div id="darkbannerwrap"></div>
 

    <form method="post" id="helloform" runat="server">
        <!--这里是登陆信息-->
        <font color="red">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </font>
        <hr class="hr15">
        <asp:TextBox ID="TextBox1" placeholder="Username" runat="server"></asp:TextBox><br />
        <hr class="hr15">
        <asp:TextBox ID="TextBox2" placeholder="Password" runat="server" TextMode="password"></asp:TextBox><br />
        <hr class="hr15">
        <asp:Button ID="Button1" runat="server" Text="Login" onclick="Button1_Click" style="width:100%;"/>
        <hr class="hr20">
        <!--<a class="forgetPassword">忘记密码</a>-->
        <!--<a class="register" href="register">注册账号</a>-->
    </form>


</div>

<div class="copyright">©2017 CopyRight shianqi</div>

</body>
</html>
