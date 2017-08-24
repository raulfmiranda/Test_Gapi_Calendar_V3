<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page4InserirEvento.aspx.cs" Inherits="GAPIs_Calendar_v3.Page4InserirEvento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="ErrorPan" style="background: yellow; color: red"/>
    <h1 runat="server" id="AccountNameLabel"/>
    <div runat="server" id="InputEventTitlePanel">
        Entre com o nome do Evento que deseja inserir
        <asp:TextBox runat="server" ID="EventTitleInput" TextMode="SingleLine" OnTextChanged="EventTitleInput_TextChanged" />
        <asp:Button runat="server" ID="btInserirEvento" Text="Inserir" OnClick="btInserirEvento_Click"/>
        <br />
        <asp:Label runat="server" ID="lblMensagem"/>
    </div>
    </form>
</body>
</html>
