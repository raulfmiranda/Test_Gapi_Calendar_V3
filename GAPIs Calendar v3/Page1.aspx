<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="GAPIs_Calendar_v3.Page1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="ErrorPan" style="background: yellow; color: red"/>
    <div runat="server" id="InputAccountNamePanel">
        Enter Account Name
        <asp:TextBox runat="server" ID="AccountNameInput" TextMode="SingleLine" />
    </div>
    </form>
</body>
</html>
