<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="GAPIs_Calendar_v3.Page2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="ErrorPan" style="background: yellow; color: red">
    </div>
    <h1 runat="server" id="AccountNameLabel" />
    <asp:Label runat="server" Text="Select calendar" />
    <asp:DropDownList runat="server" ID="AccountCalendarsList" OnDataBound="AccountCalendarsList_OnDataBound"
        AutoPostBack="true" OnSelectedIndexChanged="AccountCalendarsList_OnSelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <asp:HyperLink runat=server NavigateUrl="Page1.aspx?clear=1">Sign out</asp:HyperLink>
    </form>
</body>
</html>
