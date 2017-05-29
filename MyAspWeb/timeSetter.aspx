<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="timeSetter.aspx.cs" Inherits="MyAspWeb.timeSetter" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport"
          content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
    <link rel="stylesheet" href="//bootswatch.com/journal/bootstrap.min.css">
    <link href="./style/bootstrap-datetimepicker.min.css" rel="stylesheet" media="screen">
</head>
<body>
<div id="main" class="panel-body">
    <style>
        #dtp_input1 {
            display: none;
        }
    </style>
    <!-- Standard button -->
    

    <form method="post" runat="server" class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-4">
                <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd - HH:ii p" data-link-field="dtp_input1">
                    <span class="input-group-addon">起始时间</span>
                    <input class="form-control" type="text" value="" readonly="">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                </div>
                <asp:TextBox runat="server" ID="dtp_input1"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <div class="input-group">
                    <span class="input-group-addon">时长</span>
                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" />
                    <span class="input-group-addon">小时</span>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="input-group">
                    <span class="input-group-addon">间隔</span>
                    <asp:DropDownList ID="DropDownList2" runat="server" class="form-control"/>
                    <span class="input-group-addon">天</span>
                </div>
            </div>
            <div class="col-sm-2">
                <asp:Button class="btn btn-success form-control" type="button" ID="AddButton" runat="server" Text="添加" onclick="Add"/>
            </div>
        </div>
        
        <div class="table-responsive">
            <asp:Table runat="server" class="table" ID="MyTable">
            </asp:Table>
        </div>
        
    </form>
</div>
</body>
<script type="text/javascript" src="./javascript/lib/jquery-3.2.1.min.js" charset="UTF-8"></script>
<script type="text/javascript" src="./javascript/lib/bootstrap-datetimepicker.min.js" charset="UTF-8"></script>
<script type="text/javascript" src="./javascript/lib/bootstrap-datetimepicker.zh-CN.js" charset="UTF-8"></script>
<script src="//cdn.bootcss.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
<script type="text/javascript">
    $('.form_datetime').datetimepicker({
        language:  'zh-CN',
        weekStart: 1,
        todayBtn: 0,
        autoclose: 1,
        todayHighlight: 1,
        startView: 1,
        forceParse: 0,
        showMeridian: 1
    });
</script>
</html>