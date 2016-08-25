<%@ Page Language="C#" AutoEventWireup="true" CodeFile="formulario.aspx.cs" Inherits="formulario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        
        .style1
        {
            width: 723px;
        }
        
    </style>
</head>
<body style="background-image: url(fondo.png); background-repeat: no-repeat; background-attachment: fixed;" > 
<form id="a" runat="server">
    <table id="C" runat="server">
        <tr>
            <td class="style1">
                <asp:TextBox ID="txtPregunta" Width="99%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Button ID="Button1" runat="server" Width="99%"  OnClick="Button1_Click" 
                    Text="Preguntar" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:TextBox ID="txtResult" runat="server" Height="384px" TextMode="MultiLine" 
                    Width="220px" Visible="False"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
