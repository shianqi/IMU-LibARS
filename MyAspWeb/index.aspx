<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MyAspWeb.index" %>

<!DOCTYPE html>
<html>
  <head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<meta http-equiv="Cache-Control" content="no-siteapp" />
	<meta name="keywords" content="图书管理系统 内蒙古大学 自动预约">
	<meta name="description" content="内蒙古大学图书馆自动预约系统">
    <title>首页</title>
	<link href="../style/sccl.css" type=text/css rel=stylesheet />
    <link href="../style/skin.css" type=text/css rel=stylesheet id="layout-skin"/>
  </head>
  
  <body>
	<div class="layout-admin">
		<header class="layout-header">
			<span class="header-logo">内蒙古大学图书馆自动预约系统</span> 
			<a class="header-menu-btn" href="javascript:;"><i class="icon-font">&#xe600;</i></a>
			<ul class="header-bar">
<%--				<li class="header-bar-role"><a href="javascript:;">超级管理员</a></li>--%>
				<li class="header-bar-nav">
					<a href="javascript:;">
					    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <i class="icon-font" style="margin-left:5px;">&#xe60c;</i>
					</a>
				    <form method="post" id="helloform" runat="server">
					    <ul class="header-dropdown-menu">
						    <li>
						        <asp:LinkButton runat="server" onclick="Logout">注销</asp:LinkButton>
						    </li>
					    </ul>
                    </form>
				</li>
			</ul>
		</header>
		<aside class="layout-side">
			<ul class="side-menu">
			  
			</ul>
		</aside>
		
		<div class="layout-side-arrow"><div class="layout-side-arrow-icon"><i class="icon-font">&#xe60d;</i></div></div>
		
		<section class="layout-main">
			<div class="layout-main-tab">
				<button class="tab-btn btn-left"><i class="icon-font">&#xe60e;</i></button>
                <nav class="tab-nav">
                    <div class="tab-nav-content">
                        <a href="javascript:;" class="content-tab active" data-id="home.html">首页</a>
                    </div>
                </nav>
                <button class="tab-btn btn-right"><i class="icon-font">&#xe60f;</i></button>
			</div>
			<div class="layout-main-body">
				<iframe class="body-iframe" name="iframe0" width="100%" height="99%" src="about.aspx" frameborder="0" data-id="home.html" seamless></iframe>
			</div>
		</section>
		<div class="layout-footer">©2017 CopyRight shianqi</div>
	</div>
	<script type="text/javascript" src="../javascript/lib/jquery-1.9.0.min.js"></script>
	<script type="text/javascript" src="../javascript/sccl.js"></script>
	<script type="text/javascript" src="../javascript/sccl-util.js"></script>
  </body>
</html>

