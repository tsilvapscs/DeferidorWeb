<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeferidorGenerico.aspx.cs"
    Inherits="RCPJ.Application.DeferidorGenerico" %>

<%@ Register TagPrefix="pwc" Namespace="psc.Blocks.UI.WebForms" Assembly="psc.Blocks.UI.WebForms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<%@ Register TagPrefix="body" TagName="Template" Src="TemplateBodyExtV3.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Conferência e Análise do Requerimento</title>
    <link href="css/style2.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/PopUpStyle.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" id="Script2" src="Scripts/jquery-1.9.1.js"></script>

    <script language="javascript" type="text/javascript" id="funcvbs" src="Scripts/Funcoes.js"></script>

    <script type="text/javascript">

        function fecharDivTelaTransparente() {
            document.getElementById("divAvisos").style.display = "none";
            document.getElementById("telaCarregandoMensagens").style.display = "none";
        }


    </script>

</head>
<body>
    <body:Template runat="server" ID="myBody"></body:Template>
    <form id="Form1" runat="server" method="post">
        <asp:TextBox ID="hidOrigemMensagem" runat="server" Visible="false" Text="1060"></asp:TextBox>
        <asp:TextBox ID="hidTamPag" runat="server" Visible="false" Text="1060"></asp:TextBox>
        <asp:ScriptManager ID="scriptmanager1" runat="server" AsyncPostBackErrorMessage="Problema na atualização dos dados tente novamente">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="Req2ECorpo">
                    <%--<div class="logo2">
                        <asp:Image ID="logoCabecalho" runat="server" ImageUrl="" Width="117" Height="126" />
                    </div>
                    <div class="titulo">
                        Deferidor
                    </div>
                    <div class="tempo">
                        <input type="text" name="the_text" style="text-align: left; border: 0; width: 80px;
                            color: red; font-weight: 600; font-size: 11px; font-family: Verdana;" />
                        <asp:Label ID="lblTime" runat="server" Text="  Tempo restante da sessão"></asp:Label>
                    </div>--%>
                    <div class="Req2Econteudo">
                        <fieldset>
                            <legend class="font1">Informações do Protocolo </legend>
                            <div id="divAlertas" runat="server" visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAlerta1" runat="server" Visible="true" CssClass="botao_alerta" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMensagemAlerta" CssClass="font1vermelho" Text="Existem alertas neste requerimento."
                                                runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAlerta2" runat="server" Visible="true" CssClass="botao_alerta" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table cellspacing="1" cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td class="dados" align="center">
                                        <pwc:ValidationSummary class="erro" ID="ErrorSummary" runat="server"></pwc:ValidationSummary>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td class="fontReq2E">
                                                    <p class="font1">
                                                        Evento:
                                                        <asp:Label ID="lblEvento" CssClass="fontReq2E" runat="server"></asp:Label>
                                                    </p>
                                                    <div runat="server" id="divNumeroServentia">
                                                        <p class="font1">
                                                            <asp:Label ID="lblNumeroServentia" Text="Número Serventia" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblNumeroServentiaValor" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divProcesso" runat="server">
                                                        <p class="font1">
                                                            <asp:Label ID="lblTituloProcessoOR" Text="Número de Processo" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblNumProcesso" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divViabilidade" runat="server">
                                                        <p class="font1">
                                                            <asp:Label ID="lblTitViabilidade" Text="Viabilidade" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblNumViabilidade" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divNIRE" runat="server">
                                                        <p class="font1">
                                                            <asp:Label ID="lblNire" Text="Número Órgao de Registro (NIRE/Matrícula):" CssClass="font1" runat="server"></asp:Label>
                                                            <pwc:TextBox ID="txtNire" TipoLetra="Maiuscula" CssClass="fontReq2E" runat="server"></pwc:TextBox>
                                                        </p>
                                                    </div>
                                                    <div id="divNIRE246" runat="server" visible="false">
                                                        <p class="font1">
                                                            <asp:Label ID="Label1" Text="Número Órgao de Registro Evento 246" CssClass="font1" runat="server"></asp:Label>
                                                            <pwc:NumericTextBox ID="txtNire246" CssClass="fontReq2E" runat="server"></pwc:NumericTextBox>
                                                        </p>
                                                    </div>
                                                    <div id="divFataAutenticacao" runat="server">
                                                        <p class="font1">
                                                            <asp:Label ID="lblDataRegistro" Text=" Data do Registro:" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblAutenticacao" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divCNPJ" runat="server">
                                                        <p class="font1">
                                                            <asp:Label ID="lbltitCNPJ" Text=" CNPJ:" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblCNPJ" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divDBE" runat="server">
                                                        <p class="font1">
                                                            DBE:
                                                            <asp:Label ID="lblDBE" CssClass="fontReq2E" runat="server"></asp:Label>
                                                            <asp:Label ID="lblStatusDBE" CssClass="fontReq2E" ForeColor="blue" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="div1MotivoBaixa" runat="server" visible="false">
                                                        <p class="font1">
                                                            Motivo da Baixa:
                                                            <asp:Label ID="lblMotivoBaixa" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divNomeFantasia" runat="server">
                                                        <p class="font1">
                                                            Nome Fantasia:
                                                            <asp:Label ID="lblNomeFantasia" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divResponsavel" runat="server" visible="false">
                                                        <p class="font1">
                                                            <asp:Label ID="Label2" Text=" Cpf do Responsavel:" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblCpfResponsavel" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                        <p class="font1">
                                                            <asp:Label ID="Label3" Text=" Nome do Responsavel:" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblNomeResponsavel" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                        <p class="font1">
                                                            <asp:Label ID="Label5" Text=" Qualificação do Responsavel:" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblQualificacaoResponsavel" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divFuncionarioDBE" runat="server">
                                                        <p class="font1">
                                                            <asp:Label ID="lblResposavel" Text=" CPF responsável pelo Deferimento:" CssClass="font1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblCpf" CssClass="fontReq2E" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset id="fdsNaturezaJuridica" runat="server">
                            <legend class="font1">Natureza Jurídica </legend>
                            <table width="99%" cellspacing="1" style="margin-bottom: 5px; margin-top: 5px" align="center">
                                <tr id="pnlConfNatJuridica" runat="server">
                                    <td class="BordasReq2E">
                                        <asp:Label ID="lblEmpresaNaturezaJuridica" CssClass="fontReq2E" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset id="fdsQSA" runat="server">
                            <asp:Panel runat="server" ID="pnQSA">
                                <legend class="font1">QSA - Quadro de Sócios e Administradores</legend>
                            </asp:Panel>
                        </fieldset>
                        <fieldset id="fdsNomeEmpresa" runat="server">
                            <legend class="font1">Nome Empresarial </legend>
                            <table width="99%" cellspacing="1" style="margin-bottom: 5px; margin-top: 5px" align="center">
                                <tr>
                                    <td class="BordasReq2E">
                                        <asp:Label ID="lblNomeEmpresa" CssClass="fontReq2E" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset runat="server" id="fdsEndereco">
                            <legend class="font1">Endereço da Empresa </legend>
                            <table width="99%" cellspacing="1" style="margin-bottom: 5px; margin-top: 5px" align="center">
                                <tr>
                                    <td class="BordasReq2E">
                                        <asp:Label ID="lblEmpresaEndereco" CssClass="fontReq2E" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset runat="server" id="fdsCapital">
                            <legend class="font1">Capital Social </legend>
                            <table width="99%" cellspacing="1" style="margin-bottom: 5px; margin-top: 5px" align="center">
                                <tr class="fontReq2E">
                                    <td>
                                        <asp:Label ID="lblCapitalDistribuicao" CssClass="fontReq2E" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset runat="server" id="fdsObjeto">
                            <legend class="font1" runat="server" id="legObjeto">Objeto Social e Atividades Econômicas</legend>
                            <table width="99%" cellspacing="1" style="margin-bottom: 5px; margin-top: 5px" align="center">
                                <tr>
                                    <td class="BordasReq2E">
                                        <asp:Label ID="lblOBJSocial" CssClass="fontReq2E" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <legend class="font1" runat="server" id="legCNAE">Código Nacional de Atividade Econômica
                                (CNAE) </legend>
                            <br />
                            <table id="CNAEtable" runat="server" cellpadding="4" cellspacing="0">
                                Principal
                                <tr class="font1" style="background-color: #5080a2; color: White;" align="center">
                                    <td align="center">Código
                                    </td>
                                    <td align="center">Tipo
                                    </td>
                                    <td align="center">Descrição
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset runat="server" id="fldEnquadramento" visible="false">
                            <legend class="font1">Porte da empresa</legend>
                            <table width="99%" cellspacing="1" style="margin-bottom: 5px; margin-top: 5px" align="center">
                                <tr>
                                    <td class="BordasReq2E">
                                        <asp:Label ID="lblEnquadramento" CssClass="fontReq2E" runat="server"></asp:Label><br />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </div>
                <table cellspacing="1" cellpadding="2" width="100%" border="0">
                    <tr>
                        <td class="dados" align="center">
                            <pwc:ValidationSummary class="erro" ID="ValidationSummary1" runat="server"></pwc:ValidationSummary>
                        </td>
                    </tr>
                </table>
                <table width="100%" id="Table6">
                    <tr align="center">
                        <td class="tabela" width="50%" align="center">
                            <asp:Button runat="server" ID="btnGravarRequerimento" Visible="false" Text=" " CssClass="btnEnviarXml"></asp:Button>
                            <asp:Button runat="server" ID="btnVoltar" Visible="false" Text=" " CssClass="btnVoltarGrande"></asp:Button>
                            <pwc:Button runat="server" ID="btnDeferirDBEMatriz" Visible="false" Text=" " CssClass="btnDeferirGrande"></pwc:Button>
                            <pwc:Button runat="server" ID="btnInDeferirDBEMatriz" ConfirmationMessage="Confirma o Indeferimento do DBE?" ShowConfirmation="true" Visible="false" Text=" " CssClass="btnInDeferirGrande"></pwc:Button>

                        </td>
                    </tr>
                </table>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                    DynamicLayout="true">
                    <ProgressTemplate>
                        <div id="Loading" runat="server" style="position: fixed; top: 320; right: 160; z-index: 1000; left: 0; height: 100%; width: 100%;"
                            align="center">
                            <br />
                            <img src="img/ajaxloading.gif" width="100px" alt="" />
                            <asp:Label ID="Label10" runat="server" Text="Processando..." Font-Size="XX-Large"></asp:Label>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="rodape" visible="false">
                    <asp:Image ID="logoRodape" runat="server" Visible="true" />
                    <div class="rodape2" visible="false">
                    </div>
                    <div class="rodape3" visible="false">
                    </div>
                    <table class="tabela_requerimento">
                        <tr>
                            <td class="tab_requerimento2" align="left">
                                <asp:Label runat="server" ID="lblVersao" Text="Versão 1.4.4 25/02/2015"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <input id="hidExaminador" type="hidden" value="1" runat="server" />
        <input id="hidUsuario" type="hidden" value="" runat="server" />
        <input id="hidCPFUsuario" type="hidden" value="" runat="server" />
        <input id="hidConsulta" runat="server" type="hidden" value="0" />
        <div id="divAvisos_ant" class="cent_carr" style="display: none;" runat="server">
            <div class="carregando_dentro">
                <div id="Div3">
                    <h1 id="popup_title">ATENÇÃO</h1>
                    <div id="divAvisosMensagem" runat="server" style="font-size: 14px; color: #444444;">
                    </div>
                    <fieldset style="width: auto; border: 0; color: #444444;">
                        <div class="btnFechar" style="color: #444444; width: 100%;">
                            <a href="javascript:fecharDivTelaTransparente();" style="text-decoration: none; color: #000000;"></a>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <div id="divAvisos" class="cent_carr2" style="display: none;" runat="server">
            <div id="divCarregandoDentro" runat="server" class="carregando_dentro">
                <div id="Div5">
                    <h1 id="H1_1">ATENÇÃO</h1>
                    <div id="divAlertaPergunta" runat="server" style="font-size: 14px; color: #444444;">
                    </div>
                    <div style="width: auto; border: 0;">
                        <div>
                            <br />
                            <div id="divBotoesPergunta" runat="server" visible="false" class="btnFechar">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnConfirmaSim" runat="server" Text="SIM" Visible="true" OnClientClick="DoPostBack('btnConfirmaSim');" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divBotaoOK" runat="server" visible="true" class="btnFechar">
                                <a href="javascript:fecharDivTelaTransparenteAlerta();" style="text-decoration: none; color: #000000;"></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="carregandoSimNao" id="telaCarregandoMensagens" runat="server" style="display: none;">
        </div>
    </form>
</body>
</html>
