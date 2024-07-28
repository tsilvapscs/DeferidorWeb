<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EntradaDeferidor.aspx.cs"
    Inherits="RCPJ.Application.EntradaDeferidor" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="body" TagName="Template" Src="TemplateBodyExtV3.ascx" %>
<%@ Register TagPrefix="pwc" Namespace="psc.Blocks.UI.WebForms" Assembly="psc.Blocks.UI.WebForms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="css/style2.css" rel="stylesheet" type="text/css" />
    <link href="css/PopUpStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" id="Script1" src="Scripts\funciones.js"></script>

    <script language="javascript" type="text/javascript" id="jquery" src="Scripts/jquery-1.9.1.js"></script>

    <script type="text/javascript" language="javascript" id="funcvbs" src="Scripts/Funcoes.js"></script>

    <script language="javascript" type="text/javascript">
        function disableBackButton(){window.history.forward(-1);}
        function Fechar()
        {
        window.close("Requerimento1.aspx");
        }

        function Confirmar(wTexto)
        {

        }
        $(document).ready(function () {
			    VerificaAlturaPagina();
		    });
		    function VerificaAlturaPagina() {
		    $('#hidTamPag').val($(document).height().toString());
		   
		    }
		    function DoPostBack(obj) {

			    __doPostBack(obj, '');

		    }
		    function fecharDivTelaTransparenteAlerta() {
    		    document.getElementById("divAvisos").style.display = "none";
			    document.getElementById("telaCarregandoMensagens").style.display = "none";
		    }
    </script>

</head>
<body>
    <body:Template runat="server" ID="myBody"></body:Template>
    <form id="form1" runat="server">
        <div class="corpo">
            <asp:Panel ID="pnlDbe1" runat="server" Visible="true">
                <table width="99%" align="center" >
                    <tr>
                        <td class="tituloViabOpcao2 ">
                            INFORME:
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td class="ViabOpcoes3" width="30%">
                            <asp:Label ID="lblserventia" runat="server" Text="Número de Serventia"></asp:Label>
                        </td>
                        <td class="font_observ_req2" width="70%">
                            <pwc:TextBox ID="txtnumeroServentia" ReadOnly="true" MaxLength="14" TipoLetra="Maiuscula"
                                Columns="20" runat="server" CssClass="campoFormulario"></pwc:TextBox>
                            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td class="ViabOpcoes3" width="30%">
                            <asp:Label ID="Label1" runat="server" Text="Número do Protocolo Órgão de Registro"></asp:Label>
                        </td>
                        <td class="ViabOpcoes3" width="70%">
                            <pwc:TextBox ID="txtProtocoloOR" AutoPostBack="true" MaxLength="30" TipoLetra="Maiuscula"
                                Columns="25" runat="server" CssClass="campoFormulario" AutoCompleteType="Disabled" EnableTheming="True"></pwc:TextBox>
                            <asp:Label ID="lblNome" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%" runat="server" id="tblPergunta" visible="false">
                <tr id="Tr1" align="center" runat="server">
                    <td align="center" class="tabela" width="30%">
                        <asp:Panel ID="Panel1" runat="server" BackColor="gainsboro" Height="100px" Width="350px"
                            HorizontalAlign="Center" BorderWidth="1">
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="Viabilidade é obrigatória, caso precise <br> avançar sem viabilidade clique em SIM." CssClass="textoFormulario"></asp:Label>
                            <br />
                            <br />
                            <pwc:Button runat="server" ID="btnSim" Text="SIM" CssClass="btnLaranja"></pwc:Button>
                            <pwc:Button runat="server" ID="btnNao" Text="NÃO" CssClass="btnLaranja"></pwc:Button>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlDbe2" runat="server" Visible="true">
                <table width="99%" align="center">
                    <tr style="height: 30px;">
                        <td class="ViabOpcoes3" width="30%">
                            <asp:Label ID="Label3" runat="server" Text="Data do Registro"></asp:Label>
                        </td>
                        <td class="font_observ_req2" width="70%">
                            <pwc:DateTextBox ID="txtDataUtenticacao" ReadOnly="false" MaxLength="14" Columns="12"
                                runat="server" CssClass="campoFormulario"></pwc:DateTextBox>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td class="ViabOpcoes3" width="30%">
                            <asp:Label ID="Label2" runat="server" Text="Número Órgao Registro"></asp:Label>
                        </td>
                        <td class="font_observ_req2" width="70%">
                            <pwc:TextBox ID="txtNire" MaxLength="25" Columns="25" runat="server" CssClass="campoFormulario"></pwc:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td class="ViabOpcoes3">
                            <asp:Label ID="lblnumDBE" runat="server" Text="Código de Acesso do DBE"></asp:Label>
                        </td>
                        <td class="font_observ_req2">
                            <asp:TextBox ID="txtDBE" runat="server" MaxLength="24" Columns="40" onkeypress="AlfaNumerico(this,event);"
                                CssClass="campoFormulario"></asp:TextBox>
                            <%--<asp:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDBE"
                                Mask="??\.99\.99\.99\.99\-99\.999\.999\.999\.999" MessageValidatorTip="false"
                                MaskType="none" InputDirection="LeftToRight" AcceptNegative="None" DisplayMoney="None"
                                ClearMaskOnLostFocus="false" />--%>
                            <asp:Label ID="lblAvisoDbe" runat="server" Text="* alterações Cadastrais da Empresa"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td class="ViabOpcoes3" width="30%">
                            <asp:Label ID="lblnumViabilidade" runat="server" Text="Número do Pedido de Viabilidade"></asp:Label>
                        </td>
                        <td class="font_observ_req2" width="70%">
                            <pwc:TextBox ID="txtviabilidade" MaxLength="14" TipoLetra="Maiuscula" Columns="20"
                                runat="server" CssClass="campoFormulario"></pwc:TextBox>
                            <asp:Label ID="lblAvisoViabilidade" runat="server" Text="* alterações de Nome Empresarial, Endereço e Objeto Social"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td align="center" width="100%">
                        <pwc:ValidationSummary CssClass="erro" ID="ErrorSummary" runat="server" ShowSummary="true"
                            ShowMessageBox="true" DisplayMode="BulletList"></pwc:ValidationSummary>
                    </td>
                </tr>
            </table>
            
            <table width="100%" id="Table6">
                <tr>
                    <td class="tabela" width="60%" align="center">
                        <pwc:Button runat="server" ID="btnAvancar" Text=" " CssClass="btnAvancarGrande"></pwc:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
