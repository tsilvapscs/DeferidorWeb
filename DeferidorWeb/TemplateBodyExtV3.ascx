<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN http://www.w3.org/TR/html4/strict.dtd">
<%--<meta id="FirstCtrlID" runat="server" http-equiv="X-UA-Compatible" content="IE=9" />--%>
<%@ Register TagPrefix="pwc" Namespace="psc.Blocks.UI.WebForms" Assembly="psc.Blocks.UI.WebForms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TemplateBodyExtV3.ascx.cs"
    Inherits="RCPJ.Application.TemplateBodyExtV3" %>
<%--<link type="text/css" rel="stylesheet" href="Style/Style.css" />--%>
<%--<script language="javascript" id="Script3" src="Script\jquery-1.9.1.js"></script>--%>
<link href="css/styleV2.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    var the_timeout;
    var segundos = 59;
    var minutos = 59;
    var chave = true;


    function writeTime() {
        var today = new Date();
        var hours = today.getHours();
        var minutes = today.getMinutes();
        var seconds = today.getSeconds();
        var path = location.pathname.split('/');
        segundos = segundos - 1;
        if (minutos == 0 && segundos == 0 && chave) {
            var newPath = "/";
            chave = false;
            alert("Esta sessão expirou. Você será direcionado para a tela inicial.");
            //window.close();
            window.location.href = "EntradaDeferidor.aspx";
        }

        if (segundos == 0) {
            minutos = minutos - 1;
            segundos = 59;
        }
        minutes = fixTime(minutos);
        secunds = fixTime(segundos);
        var the_time = minutes + ":" + secunds;
        //alert(the_time);
        document.getElementById("the_text").value = the_time;
        //window.document.form1.the_text.value = the_time;
        the_timeout = setTimeout('writeTime();', 1000);
    }
    function fixTime(the_time) {
        if (the_time < 10) {
            the_time = "0" + the_time;
        }
        return the_time;
    }
    function zeraTimer() {
        minutos = 59;
        segundos = 59;
    }
</script>
<style type="text/css">
    body
    {
        background-image: url(img/viabilidadetopofundo.jpg);
        background-repeat: no-repeat;
        -moz-background-size: 100% auto;
        -webkit-background-size: 100% auto;
        background-size: 100% auto;
    }
</style>
<body onload="writeTime()">
    <div class="geral2">
        <div class="logoReginTemp">
            <asp:HyperLink ID="Image1" ImageUrl="img\logo_topo.png" runat="server" Width="174px"
                Height="80px"></asp:HyperLink>
        </div>
        <div id="viab2" runat="server" class="tituloTemplate2">
        </div>
        <div class="barraAjuda2">
            <div class="dataHoje2">
                <div class="tempoRelogio2">
                    <input type="text" id="lblHoraAtual" name="lblHoraAtual" runat="server"  style="border: 0; background:none; color: red; font-weight: 600;
                            width: 42px; padding: 0px 0px 0px 0px" value="15:55" />
                    
                </div>
                <div class="dataConteudo">
                    <asp:Label ID="LblDia" runat="server" Text="25" CssClass="dia2"></asp:Label>
                    <asp:Label ID="LblMes" runat="server" Text="JUN" CssClass="mes2"></asp:Label>
                    <asp:Label ID="lblAno" runat="server" Text="2014" CssClass="ano2"></asp:Label>
                </div>
            </div>
            <div class="versaoTemplate">
                <asp:Label ID="lblVersao" runat="server" Text="Versão 2.515" CssClass="ano2"></asp:Label>
                <br />
                <asp:Label ID="lblDataVersao" runat="server" Text="05/12/2014" CssClass="ano2"></asp:Label>
                
            </div>
            <a href="javascript:ChamaHelp()" class="botaoAjudaConsulta" style="border: solid 1px #FFFFFF;
                text-decoration: none;">
                <img src="img/btn_ajuda.gif" width="82px" height="26px" style="border: solid 1px #FFFFFF;
                    text-decoration: none;" />
            </a>
        </div>
        <div class="tempo2" style="float: left; margin-top: 15px; margin-bottom: 7px;">
            <input type="text" id="the_text" name="the_text" style="text-align: left; border: 0;
                width: 36px; color: red; font-weight: 600; font-size: 11px; font-family: Verdana;" />
            <asp:Label ID="lblTime" runat="server" Text="  Tempo restante da sessão"></asp:Label>
        </div>
        <div style="clear: both; width: 100%; height: auto;">
        </div>
    </div>
</body>
