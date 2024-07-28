<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComparaDadosDBE.aspx.cs"
    Inherits="RCPJ.Application.ComparaDadosDBE" %>

<%@ Register TagPrefix="sdbe" TagName="Template" Src="~/ComparacaoDBE.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="css/style2.css" rel="stylesheet" type="text/css" />
    <link href="css/PopUpStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td align="center">
                    <div id="divComparaDBE" runat="server" visible="true">
                        <sdbe:Template runat="server" DbeOK="false" ID="pgnComparaDBE" Visible="false" />
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="div15" runat="server" visible="false" style="width: 100%;">
                        <asp:Button ID="btnCompViaVoltar" runat="server" CssClass="btnVoltarGrande" Text="" />
                        <asp:Button ID="btnCompViabDBE" runat="server" CssClass="btnAvancarGrande" Text="" />
                    </div>
                    <div id="div1" runat="server" visible="true" style="width: 100%;">
                        
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
