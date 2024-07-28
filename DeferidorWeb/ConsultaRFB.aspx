<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaRFB.aspx.cs"
    Inherits="RCPJ.Application.ConsultaRFB" MasterPageFile="~/MasterFormulario.master" %>

<%@ MasterType VirtualPath="~/MasterFormulario.master" %>
<%@ Register TagPrefix="pwc" Namespace="psc.Blocks.UI.WebForms" Assembly="psc.Blocks.UI.WebForms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<%@ Register TagPrefix="body" TagName="Template" Src="TemplateBodyExtV3.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" ClientIDMode="Static" runat="server">

    <asp:TextBox ID="hidOrigemMensagem" runat="server" Visible="false" Text="1060"></asp:TextBox>
    <asp:TextBox ID="hidTamPag" runat="server" Visible="false" Text="1060"></asp:TextBox>
    <asp:ScriptManager ID="scriptmanager1" runat="server" AsyncPostBackErrorMessage="Problema na atualização dos dados tente novamente">
    </asp:ScriptManager>
    <script type="text/javascript" id="Script2" src="Scripts/jquery-1.9.1.js"></script>

    <script language="javascript" type="text/javascript" id="funcvbs" src="Scripts/Funcoes.js"></script>

    <script type="text/javascript">

        function fecharDivTelaTransparente() {
            document.getElementById("divAvisos").style.display = "none";
            document.getElementById("telaCarregandoMensagens").style.display = "none";
        }


    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="container shadow" style="padding: 20px 20px 20px 20px">
                    <div class="card border-0" >
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3 form-group" id="divNIRE" runat="server">
                                <asp:Label ID="lblNire" Text="NIRE/Matrícula:" Style="font-weight:600" runat="server"></asp:Label><br />
                                <asp:Label ID="txtNire" Style="font-weight:400"   runat="server"></asp:Label>
                            </div>
                            <div class="col-md-4 form-group" id="divCNPJ" runat="server">
                                <asp:Label ID="lbltitCNPJ" Text=" CNPJ:" Style="font-weight:600" runat="server" ></asp:Label><br />
                                                        <asp:Label ID="lblCNPJ" Style="font-weight:400"  runat="server"></asp:Label>
                            </div>
                                 <div class="col-md-12 form-group" id="divNomeFantasia" runat="server">
                               <asp:Label ID="Label1" Text=" Nome Fantasia:" Style="font-weight:600" runat="server" ></asp:Label><br />  
                                                            <asp:Label ID="lblNomeFantasia" Style="font-weight:400" runat="server"></asp:Label>
                            </div>
                            </div>
                            
                           
                        </div>
                    </div>
                   <div class="card border-0"  id="fdsNomeEmpresa" runat="server" >
                         <div class="card-body">
                                <div class="row">
                            <div class="col-md-12 form-group">
                        <h6>Nome Empresarial </h6><hr />
                       <asp:Label ID="lblNomeEmpresa" Style="font-weight:400" runat="server"></asp:Label>
                              </div> </div> </div>
                    </div>
                    <div class="card border-0" id="fdsNaturezaJuridica" runat="server" >
                         <div class="card-body">
                              <div class="row">
                             <div class="col-md-12 form-group">
                                  <h6>Natureza Jurídica </h6><hr />
                       <asp:Label ID="lblEmpresaNaturezaJuridica" Style="font-weight:400" runat="server"></asp:Label>
                             </div>
                       
                             </div> </div>
                    </div>
                    <div id="fdsQSA" runat="server" class="card border-0" >
                        <div class="card-body">
                             <div class="row">
                            <div class="col-md-12 form-group">
                                 <h6>QSA </h6><hr />
                             <asp:Panel runat="server" ID="pnQSA">
                            <legend><h6>QSA - Quadro de Sócios e Administradores</h6><hr /></legend>
                        </asp:Panel>
                                </div> </div>
                        </div>
                       
                    </div>
                 
                    <div class="card border-0"  runat="server" id="fdsEndereco">
                          <div class="card-body">
                               <div class="row">
                            <div class="col-md-12 form-group">
                        <h6>Endereço da Empresa </h6><hr />
                        <asp:Label ID="lblEmpresaEndereco" Style="font-weight:400" runat="server"></asp:Label>
                    </div></div></div></div>
                    <div class="card border-0"  runat="server" id="fdsCapital">
                        <div class="card-body"> 
                              <div class="row">
                            <div class="col-md-12 form-group">
                            <h6>Capital Social </h6><hr />
                        <asp:Label ID="lblCapitalDistribuicao" Style="font-weight:400" runat="server"></asp:Label>
                    </div></div></div></div>
                    <div class="card border-0"   runat="server" id="fdsObjeto">
                       <div class="card-body">  
                              <div class="row">
                            <div class="col-md-12 form-group">
                           <h6>Objeto Social e Atividades Econômicas</h6><hr />
                        <asp:Label ID="lblOBJSocial" Style="font-weight:400" runat="server"></asp:Label>
                        <br />
                        <h6 runat="server" id="legCNAE" style="margin-top:20px">Código Nacional de Atividade Econômica
                                (CNAE) </h6>
                        <table id="CNAEtable" class="table table-responsive-lg" runat="server" cellpadding="4" cellspacing="0">
                                <tr >
                                    <td align="center">Código
                                    </td>
                                    <td align="center">Tipo
                                    </td>
                                    <td align="center">Descrição
                                    </td>
                                </tr>
                        </table>
                    </div>   </div></div></div>
                    <div class="card border-0"  runat="server" id="fldEnquadramento" visible="false">
                       <div class="card-body"><div class="row">
                            <div class="col-md-12 form-group"> <h6>Porte da empresa</h6><hr />
                    <asp:Label ID="lblEnquadramento" Style="font-weight:400" runat="server"></asp:Label>
                    </div </div> </div </div>
            </div>
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
</asp:Content>
