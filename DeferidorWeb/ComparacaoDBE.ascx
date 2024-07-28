<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ComparacaoDBE.ascx.cs"
    Inherits="RCPJ.Application.ComparacaoDBE" EnableViewState="true"  %>
<div class="corpo">
    <asp:TextBox ID="hidOrigemComparacao" runat="server" CssClass="invisivel" Text=""></asp:TextBox>
    <asp:Label ID="lblComparacaoComplmentoOK" runat="server" CssClass="invisivel" Text=""></asp:Label>
    <asp:TextBox ID="hidComparacaoOK" runat="server" CssClass="invisivel" Text=""></asp:TextBox>
    <asp:Label ID="lblDbeOK" Visible="false" runat="server" Text=""></asp:Label>
    <div class="titulo3">
        Confirmação<br />
        <font color="#dfb22e">VIABILIDADE X DBE</font>
    </div>
    <div class="conteudo_confirmacao">
        <table style="margin: 0 auto; margin-bottom: 20px; width: 960px;">
            <tr>
                <td style="width: 140px;">
                </td>
                <td class="titulo_viab" style="width: 402px;">
                </td>
                <td class="titulo_dbe2" style="width: 402px;">
                </td>
            </tr>
            <tr>
                <td class="titTabelaConfirmacao3">
                </td>
                <td class="titTabelaConfirmacao" align="center">
                    <asp:Label ID="lblNumViabilidade" runat="server"></asp:Label>
                </td>
                <td class="titTabelaConfirmacao2" align="center">
                    <asp:Label ID="lblNumDBE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titTabelaConfirmacao3">
                </td>
                <td class="titTabelaConfirmacao" align="left">
                    Pessoa Jurídica - Empresa
                </td>
                <td class="titTabelaConfirmacao2" align="left">
                    Pessoa Jurídica - Empresa
                </td>
            </tr>
            <tr>
                <td class="tabelaConfirmacao2">
                    Evento
                </td>
                <td class="tabelaConfirmacao2" id="tdEventoViab" runat="server">
                    <asp:Label ID="lblEventoViab" runat="server" Text="" ForeColor="Black"></asp:Label>
                </td>
                <td class="tabelaConfirmacao2" id="tdEventoDBE" runat="server">
                    <asp:Label ID="lblEventoDbe" runat="server" Text="[[Evento]]" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tabelaConfirmacao1">
                    Nome
                </td>
                <td class="tabelaConfirmacao1" id="tdNomeEmpresaViab" runat="server">
                    <asp:Label ID="lblNomeEmpresaViab" runat="server" Text=""></asp:Label>
                </td>
                <td class="tabelaConfirmacao1" id="tdNomeEmpresaDBE" runat="server">
                    <asp:Label ID="lblNomeEmpresaDbe" runat="server" Text="[[Nome]]"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tabelaConfirmacao2">
                    Natureza Jurídica
                </td>
                <td class="tabelaConfirmacao2" id="tdNaturezaEmpresaViab" runat="server">
                    <asp:Label ID="lblTipoEmpresaViab" runat="server" Text=""></asp:Label>
                </td>
                <td class="tabelaConfirmacao2" id="tdNaturezaEmpresaDBE" runat="server">
                    <asp:Label ID="lblTipoEmpresaDbe" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <asp:Panel ID="pnlEndereco" runat="server">
                <tr>
                    <td class="tabelaConfirmacao1">
                        Tipo de Logradouro
                    </td>
                    <td class="tabelaConfirmacao1" id="tdTipoLogradouroViab" runat="server">
                        <asp:Label ID="lblStreetTypeViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao1" id="tdTipoLogradouroDBE" runat="server">
                        <asp:Label ID="lblStreetTypeDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao2">
                        Logradouro
                    </td>
                    <td class="tabelaConfirmacao2" id="tdLogradouroViab" runat="server">
                        <asp:Label ID="lblLogradouroViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao2" id="tdLogradouroDBE" runat="server">
                        <asp:Label ID="lblLogradouroDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao1">
                        Número
                    </td>
                    <td class="tabelaConfirmacao1" id="tdNumeroViab" runat="server">
                        <asp:Label ID="lblNumeroViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao1" id="tdNumeroDBE" runat="server">
                        <asp:Label ID="lblNumeroDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao2">
                        Complemento
                    </td>
                    <td class="tabelaConfirmacao2" id="tdComplementoViab" runat="server">
                        <asp:Label ID="lblComplementoViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao2" id="tdComplementoDBE" runat="server">
                        <asp:Label ID="lblComplementoDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao1">
                        Município
                    </td>
                    <td class="tabelaConfirmacao1" id="tdMunicipioViab" runat="server">
                        <asp:Label ID="lblMunicipioViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao1" id="tdMunicipioDBE" runat="server">
                        <asp:Label ID="lblMunicipioDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao2">
                        Bairro/Loteamento</td>
                    <td class="tabelaConfirmacao2" id="tdBairroViab" runat="server">
                        <asp:Label ID="lblBairroViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao2" id="tdBairroDBE" runat="server">
                        <asp:Label ID="lblBairroDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao1">
                        CEP
                    </td>
                    <td class="tabelaConfirmacao1" id="tdCEPViab" runat="server">
                        <asp:Label ID="lblZipViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao1" id="tdCEPDBE" runat="server">
                        <asp:Label ID="lblZipDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao2">
                        UF
                    </td>
                    <td class="tabelaConfirmacao2" id="tdUFViab" runat="server">
                        <asp:Label ID="lblUFViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao2" id="tdUFDBE" runat="server">
                        <asp:Label ID="lblUFDbe" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao1">
                        País
                    </td>
                    <td class="tabelaConfirmacao1">
                        <asp:Label ID="lblPaisViab" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao1">
                        <asp:Label ID="lblPaisDBE" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tabelaConfirmacao2">
                        Email
                    </td>
                    <td class="tabelaConfirmacao2">
                        <asp:Label ID="lblEmailViab" runat="server" Text="[[Email]]"></asp:Label>
                    </td>
                    <td class="tabelaConfirmacao2">
                        <asp:Label ID="lblEmailDbe" runat="server" Text="[[Email]]"></asp:Label>
                    </td>
                </tr>
            </asp:Panel>
        </table>
        <table style="margin: 40px 145px 20px; width: 813px;" cellpadding="0" cellspacing="1">
            <tr>
                <td width="50%" valign="top">
                    <asp:DataList ID="ListCNAEViab" runat="server" ShowHeader="true" RepeatLayout="Table"
                        Width="100%">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left" class="titTabelaConfirmacao" width="100%">
                                        Classificação Nacional de Atividades Econômicas
                                        <br />
                                        (CNAE)
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <SelectedItemTemplate>
                            <table>
                                <tr class="rowTableSelected">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CodigoCNAE")%>
                                        <%#  DataBinder.Eval(Container.DataItem,"TipoAtividade").ToString() == "36"?" Principal":""%>
                                    </td>
                                </tr>
                            </table>
                        </SelectedItemTemplate>
                        <ItemTemplate>
                            <table width="100%">
                                <tr class="tabelaConfirmacao2">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CodigoCNAE")%>
                                        <%#  DataBinder.Eval(Container.DataItem,"TipoAtividade").ToString() == "36"?" Principal":""%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table width="100%">
                                <tr class="tabelaConfirmacao1">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CodigoCNAE")%>
                                        <%#  DataBinder.Eval(Container.DataItem,"TipoAtividade").ToString() == "36"?" Principal":""%>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:DataList>
                </td>
                <td width="50%" valign="top">
                    <asp:DataList ID="ListCNAEDBE" runat="server" ShowHeader="true" RepeatLayout="Table"
                        Width="100%">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left" class="titTabelaConfirmacao2">
                                        Classificação Nacional de Atividades Econômicas
                                        <br />
                                        (CNAE)
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <SelectedItemTemplate>
                            <table width="100%">
                                <tr class="rowTableSelected">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CodigoCNAE")%>
                                        <%#  DataBinder.Eval(Container.DataItem,"TipoAtividade").ToString() == "36"?" Principal":""%>
                                    </td>
                                </tr>
                            </table>
                        </SelectedItemTemplate>
                        <ItemTemplate>
                            <table width="100%">
                                <tr class="tabelaConfirmacao2">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CodigoCNAE")%>
                                        <%#  DataBinder.Eval(Container.DataItem,"TipoAtividade").ToString() == "36"?" Principal":""%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table width="100%">
                                <tr class="tabelaConfirmacao1">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CodigoCNAE")%>
                                        <%#  DataBinder.Eval(Container.DataItem,"TipoAtividade").ToString() == "36"?" Principal":""%>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <table style="margin: 40px 145px 20px; width: 813px;" cellpadding="0" cellspacing="1">
            <tr>
                <td width="50%" valign="top">
                    <asp:DataList ID="ListSociosViab" runat="server" ShowHeader="true" Width="100%">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left" class="titTabelaConfirmacao" width="100%">
                                        Quadro Societário (Viabilidade)
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <SelectedItemTemplate>
                            <table width="100%">
                                <tr class="rowTableSelected">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "fCPFCNPJ")%>
                                    </td>
                                </tr>
                            </table>
                        </SelectedItemTemplate>
                        <ItemTemplate>
                            <table width="100%">
                                <tr id="iViab" class="tabelaConfirmacao2" runat="server">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "fCPFCNPJ")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table width="100%">
                                <tr class="tabelaConfirmacao1">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "fCPFCNPJ")%>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:DataList>
                </td>
                <td width="50%" valign="top">
                    <asp:DataList ID="ListSociosDBE" runat="server" ShowHeader="true" RepeatLayout="Table"
                        Width="100%">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left" class="titTabelaConfirmacao2">
                                        Quadro Societário (DBE)
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <SelectedItemTemplate>
                            <table width="100%">
                                <tr class="rowTableSelected">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "fCPFCNPJ")%>
                                        <%#  DataBinder.Eval(Container.DataItem,"Qualificacao").ToString() == "5"?" - Administrador":""%>
                                    </td>
                                </tr>
                            </table>
                        </SelectedItemTemplate>
                        <ItemTemplate>
                            <table width="100%">
                                <tr class="tabelaConfirmacao2">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "fCPFCNPJ")%>
                                        <%#  DataBinder.Eval(Container.DataItem, "Qualificacao").ToString() == "5" ? " - Administrador" : ""%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table width="100%">
                                <tr class="tabelaConfirmacao1">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "fCPFCNPJ")%>
                                        <%#  DataBinder.Eval(Container.DataItem, "Qualificacao").ToString() == "5" ? " - Administrador" : ""%>
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <div class="textopasso1">
            <asp:Label ID="lblPasso2Inferior" runat="server" Text="" Font-Bold="true"></asp:Label>
        </div>
        <div class="textopasso1">
            <asp:Label ID="lblErroComplemento" runat="server" Text="" Font-Bold="true"></asp:Label>
        </div>
    </div>
   <%-- <div id="divAvisos" class="cent_carr2" style="display: none;" runat="server">
        <div class="carregando_dentro">
            <div id="Div1">
                <h1 id="popup_title">
                    ATENÇÃO</h1>
                <div id="divAlertaPergunta" runat="server" style="font-size: 14px; color: #444444;">
                </div>
                <div style="width: auto; border: 0;">
                    <div class="btnFechar3">
                        <br />
                        <div id="divBotoesPergunta" runat="server" visible="true" class="btnFechar" style="color: #444444;
                            width: 100%;">
                            <p>
                                <asp:Button ID="btnConfirmaSim" runat="server" Text="SIM" CssClass="bigbuttonLaranja"
                                    Visible="true" OnClientClick="DoPostBack('btnConfirmaSim');" />
                                <asp:Button ID="btnConfirmaNao" runat="server" Text="NÂO" CssClass="bigbuttonVermelho"
                                    Visible="true" OnClientClick="DoPostBack('btnConfirmaNao');" />
                            </p>
                        </div>
                        <div id="divBotaoOK" runat="server" visible="false" class="btnFechar">
                            <a href="javascript:fecharDivTelaTransparenteAlerta();" style="text-decoration: none;
                                color: #000000;"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="carregandoSimNao" id="telaCarregandoMensagens" runat="server" style="display: none;">
    </div>--%>
</div>
