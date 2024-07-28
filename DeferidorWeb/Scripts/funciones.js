/*

Funções disponíveis:

fnTrapKD(btn, ev)
foco(campo)
OutrosCodigosParaDigitar(e)
IsNulo(pValor)
KeyCodeMaster(e)
detectaBrowser() 
isOblig(pObject, msg, pFocus)
FrameBaseOpen(frm, ControlReturn, pdialogWidth, pdialogHeight)
DatePeriodTextBoxKeyPress(srcElement, ev) 
FrameBaseOpenIE(frm, ControlReturn, pdialogWidth, pdialogHeight)
ValidarEmail(pValor, mens) 
poptastic(url, nome, pdialogWidth, pdialogHeight)
poptasticbarra(url, nome, pdialogWidth, pdialogHeight)
VerificaNIRE(nire, mens)
VerificaRuc(pValor, mens)
CalculateVerificationDigit(rif, xBase)
FormataValidaCodAcesso(pValor, mens)
PasteNumericOnly(ev)
EscreveNada(e)
SoloNumero(pValor, e)
FormataNumericDecimal(pValor, cantdecimal, e)
ValidaData(pValor, mens)
ValidaPeriodo(pValor, mens)
IsNumericStr(pValor)
IsNumericKeyCode(pValor)
FormataFecha(pValor, e)
UpperCase(pValor)
LowerCase(pValor)
vNuloNumeric(pValor)
tiratudostr(pValor)
tiratudo(pValor)
VerificCPFCNPJ(campo, tipVerific, mens)
validaCPFCNPJ(CNUMB,CTYPE)
TestDigit(CNUMB,CTYPE,g)
enviaAtivEcon(pCodigo, pDescricao, pTipo)
saltaCampo(campo,tamanhoMaximo,indice,evt)
checkElemento(elemento, bool)
verificaDigitoProtocolo(protocolo)
*/

function fnTrapKD(btn, ev)
	{
		if (document.all)
		{
			if (ev.keyCode == 13)
			{
				ev.returnValue=false;
				ev.cancel = true;
				btn.click();
			}
		}
		else if (document.getElementById)
		{
			if (ev.which == 13)
			{
				ev.returnValue=false;
				ev.cancel = true;
				btn.click();
			}	
		}
		else if(document.layers)
		{
			if(ev.which == 13)
			{
				ev.returnValue=false;
				ev.cancel = true;
				btn.click();
			}
		}
	}
function foco(campo)
{
	if (detectaBrowser()=='firefox')
	{
		globalvar = campo;
		setTimeout("globalvar.focus()",250);
	}
	else
	{
		campo.focus();
	}
}

function OutrosCodigosParaDigitar(e)
{
	if (KeyCodeMaster(e) == 46 || 
		KeyCodeMaster(e) == 8  || 
		KeyCodeMaster(e) == 9  ||
		KeyCodeMaster(e) == 39 ||
		KeyCodeMaster(e) == 37)
	{
		return true ;
	}
	else
	{
		return false;
	}
}

function IsNulo(pValor)
{
	if (pValor.trim() == '' || pValor == null || pValor.trim().length == 0)
	{
		return true;
	}
	return false;
}
function KeyCodeMaster(e)
{
	if (!e) e = window.event;   
	var code;   
	if (e.keyCode) code = e.keyCode;   
	       
	else if (e.which) code = e.which; // Netscape 4.?   
	
	return code;
}

function detectaBrowser() 
{
	if (navigator.appName.toLowerCase().indexOf('microsoft internet explorer') > -1)
		return('ie');
	else
	if (navigator.appName.toLowerCase().indexOf('netscape') > -1)
		return('firefox');
}

String.prototype.trim = function()
{
	return this.replace(/^\s*/, "").replace(/\s*$/, "");
}

function isOblig(pObject, msg, pFocus)
{
	if (pObject.value==null || pObject.value.trim() == '')
	{
		if (msg!=null & msg != '')
		{
	   		alert(msg);
		}
		else
		{
	   		alert('Campo Obrigatório');
		}
		if (pFocus!='nao')
		{
			foco(pObject);
		}
		return(true);
	}
}

function FrameBaseOpen(frm, ControlReturn, pdialogWidth, pdialogHeight)
{
	poptastic(frm, pdialogWidth, pdialogHeight);
}

function DatePeriodTextBoxKeyPress(srcElement, ev) 
{
	var key = KeyCodeMaster(ev);
	vr = srcElement.value;
	vr = vr.replace(".", "");
	vr = vr.replace("/", "");
	vr = vr.replace("/", "");
	len = vr.length + 1;
    if ((key < 48) || (key > 57)) 
    {
		if (detectaBrowser()=='firefox')
		{
			if (!OutrosCodigosParaDigitar(ev))
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}
	if (key != 9 && key != 8)
	{
		if (len > 2 && len < 5)
			srcElement.value = vr.substr(0, len - 2) + '/' + vr.substr(len - 2, len);
	}
    return true;
}

function FrameBaseOpenIE(frm, ControlReturn, pdialogWidth, pdialogHeight)
{
	var retorno=""; 
	retorno = window.showModalDialog("FrameBase.aspx?frm=" + frm, "sdasdas", 'help:no;status:no;scroll:yes;edge:raised;dialogWidth:' + pdialogWidth + 'px;edge:raised;dialogHeight:' + pdialogHeight + 'px')
	if(retorno!="" && retorno!=null)
	{
		document.getElementById(ControlReturn).value = retorno;
	} 
}

function ValidarEmail(pValor, mens) 
{
	var valor, mens;
	valor = pValor.value;
	if (valor=="") return;
	
	if (!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(valor))
	{
		alert(mens);
		pValor.value = '';
		foco(pValor);
		return;
	} 
}

function poptastic(url, nome, pdialogWidth, pdialogHeight)
{

    if (nome != "CONECAOTERMINALSEVERVIAWEB")
    {
	    if (url.substring(0,7) == "http://")
	    {
		    var newWindow = window.open(url, '_blank'); 
		    newWindow.focus(); 
		    return true;
	    }
	}
	
	var largura, altura;
	if (nome==null)
	{
		nome="TelaPorDefectoJUCESC";
		nome=url.split('.')[0];
	}
	
	var pdialogWidth, pdialogHeight;
	
	if (pdialogHeight==null)
	{
		pdialogHeight = 580;
	}
	
	if (pdialogWidth==null)
	{
		pdialogWidth = 780;
	}
	
	largura = screen.width - 10;
	altura = screen.height - 80;
	newwindow = window.open(url,nome,'width='+largura+',height='+altura+',top=0,left=0,screenX=0,screenY=0,status=yes,scrollbars=yes,toolbar=no,resizable=yes,maximized=yes,menubar=no,location=no');
	
//	newwindow = window.open(url,nome,'fullscreen=yes, scrollbars=auto,maximized=yes,resizable=yes,status=yes,dependent=yes,titlebar=no,menubar=no,location=no');
	
	//newwindow=window.open(url,nome,'height=' + pdialogHeight + ',width=' + pdialogWidth + ', scrollbars=yes,resizable=1,status=yes,dependent=yes,titlebar=no, menubar=no, location=no');
	//newwindow=window.open(url,nome, 'maximized=yes,scrollbars=yes,resizable=yes,status=yes,dependent=yes,titlebar=no, menubar=no, location=no');
	newwindow.moveTo(2,2);
	
	
	if (nome=='help' || nome=='xslt' || nome=='CONSULTA' || nome=='CONSULTA2' || nome=='CONSULTASINSTITUICAO' || nome=='CONSULTA3') newwindow.moveTo(60,10)
	
	if (window.focus) {newwindow.focus()}
	
}

function poptasticbarra(url, nome, pdialogWidth, pdialogHeight)
{
	if (nome==null)
	{
		nome="TelaPorDefectoJUCESC";
	}
	
	var pdialogWidth, pdialogHeight;
	
	if (pdialogHeight==null)
	{
		pdialogHeight = 580;
	}
	
	if (pdialogWidth==null)
	{
		pdialogWidth = 780;
	}
	
	newwindow=window.open(url,nome,'height=' + pdialogHeight + ',width=' + pdialogWidth + ', scrollbars=yes,resizable=1,status=yes,dependent=yes,titlebar=no, menubar=yes, location=no');
	newwindow.moveTo(10,10);
	
	if (nome=='help' || nome=='xslt' || nome=='CONSULTA' || nome=='CONSULTA2' || nome=='CONSULTASINSTITUICAO' || nome=='CONSULTA3') newwindow.moveTo(60,10)
	
	if (window.focus) {newwindow.focus()}
	
}

function VerificaNIRE(nire, mens)
{
//Alterado porque a validação estava dando problema com Nire com final Zero, isso foi discutido com Pablo e Raul. ver solução.

	if (nire.value=="") return;
	
	var sum, digit, digits, i, remainder, difference, expected;
	var result = false;
	var digitPosition = 10;
	if (nire.value != null && nire.value.length == 11)
	{
		sum = 0;
		digit = parseFloat(nire.value.substring(10,11));
		arr = [1,2,3];
		digits = [2, 10, 9, 8, 7, 6, 5, 4, 3, 2];
		for (i = 0; i < digits.length; i++)
		{
			sum += parseFloat(nire.value.substring(i,i + 1)) * digits[i];
		}
		
		remainder  = sum % 11;
		difference = 11 - remainder;
		expected   = difference > 9 ? difference - 10 : difference;
		
		result = (expected == digit);
	}
	if (!result)
	{
		alert(mens);
		nire.value = '';
		foco(nire);
		return;
	}
	
	return true;
}

function VerificaRuc(pValor, mens)
{
	var digit, result;
	if (pValor.value=="") return;
	result = pValor.value.length == 9;

	if (result)
	{
		digit = CalculateVerificationDigit(pValor.value.substring(0,8), 9);
		result = digit == pValor.value.substring(8,9)
	}

	if (!result)
	{
		alert(mens);
		pValor.value = '';
		foco(pValor);
		return;
	}
	
	pValor.value = pValor.value.substring(0, 2)
					+ "." + pValor.value.substring(2, 5) 
					+ "." + pValor.value.substring(5, 8) 
					+ "-" + pValor.value.substring(8, 9) 
	
}

function CalculateVerificationDigit(rif, xBase)
{
	var result = 0;
	var i;
	rif = String.prototype.lPad("0", 12 - rif.length) + rif;
	for (i = 0; i < rif.length; i++)
	{
		result += parseFloat(rif.substring(i, i + 1)) * (((rif.length - (i + 1)) % (xBase - 1)) + 2);
	}
	
	result %= 11;
	
	result = result > 1 ? 11 - result : 0;
	
	return result;
}

function FormataValidaCodAcesso(pValor, mens)
{
	tiratudo(pValor);
	
	if (pValor.value.length == 0) return;
	

	if (pValor.value.length != 24)
	{
		alert(mens);
		pValor.value = '';
		foco(pValor);
		return;
	}
	pValor.value = pValor.value.substring(0, 2)
					 + "." + pValor.value.substring(2, 4) 
					 + "." + pValor.value.substring(4, 6) 
					 + "." + pValor.value.substring(6, 8) 
					 + "." + pValor.value.substring(8, 10)
					 + " - " + pValor.value.substring(10, 12)
					 + "." + pValor.value.substring(12, 15)
					 + "." + pValor.value.substring(15, 18)
					 + "." + pValor.value.substring(18, 21)
					 + "." + pValor.value.substring(21, 24);
					 
   /*pValor.value = pValor.value.substring(0, 2)
					 + "." + pValor.value.substring(2, 4) 
					 + "." + pValor.value.substring(4, 6) 
					 + "." + pValor.value.substring(6, 8) 
					 + " - " + pValor.value.substring(8, 10)
					 + "." + pValor.value.substring(10, 13)
					 + "." + pValor.value.substring(13, 16)
					 + "." + pValor.value.substring(16, 19)
					 + "." + pValor.value.substring(19, 22);*/
}

function PasteNumericOnly(ev)
{
	var strPasteData = window.clipboardData.getData("Text");
	if (detectaBrowser()=='firefox')
	{
		if (!IsNumericStr(strPasteData))
		{
			ev.preventDefault();
			return;
		}
	}
	else
	{
		event.returnValue = IsNumericStr(strPasteData);
	}
}

function EscreveNada(e)
{
	if (detectaBrowser()=='firefox')
	{
	    e.preventDefault();
		return;
	}
	else
	{
		event.returnValue = false;
	}
}

function SoloNumero(pValor, e)
{
	if (detectaBrowser()=='firefox')
	{
		if (OutrosCodigosParaDigitar(e))
		{
			return;
		}
		if (!IsNumericKeyCode(KeyCodeMaster(e)))
		{
			e.preventDefault();
			return;
		}
	}
	else
	{
		event.returnValue = IsNumericKeyCode(event.keyCode);
	}
}

function FormataNumericDecimal(pValor, cantdecimal, e)
{
	var parentera, pardecimal;
	
	if (detectaBrowser()=='firefox')
	{
		if (OutrosCodigosParaDigitar(e))
		{
			return;
		}
		if (!IsNumericKeyCode(KeyCodeMaster(e)))
		{
			e.preventDefault();
			return;
		}
	}
	else
	{
		if (!IsNumericStr(String.fromCharCode(event.keyCode)))
		{
			event.returnValue = false;
			return;
		}
	}
    
    cantdecimal = cantdecimal - 1;

	tiratudo(pValor);
	pValor.value = "0" + pValor.value.substring(0, pValor.value.length - cantdecimal)  + "," + pValor.value.substring(pValor.value.length - cantdecimal, pValor.value.length);
	
	parentera = pValor.value.split(',')[0];
	pardecimal = pValor.value.split(',')[1];
	
	pValor.value = parseFloat(parentera) + "," + pardecimal;
}

function ValidaData(pValor, mens)
{
	var varDia, varMes, varAno, divisorBissexto=4, Fecha;
	
	
	if (pValor.value == "") return;
	
	Fecha = pValor.value.split('/');
	
	varDia = Fecha[0];
	varMes = Fecha[1];
	varAno = Fecha[2];

	var ValidaDatatmp = false;
	
	if ( IsNumericStr(varDia) & IsNumericStr(varMes) & IsNumericStr(varAno) )
	{
		if (pValor.value.length == 10 )
		{
		    if ( varDia >= "01" )
		    {
			    if ( varMes=="01" || varMes=="03" || varMes=="05" || varMes=="07" || varMes=="08" || varMes=="10" || varMes=="12" )
			    {
				    if (varDia <= "31")	ValidaDatatmp = true;
			    }

			    if ( varMes=="04" || varMes=="06" || varMes=="09" || varMes=="11" )
			    {
				    if (varDia <= "30")	ValidaDatatmp = true;
			    }
			    if ( varMes=="02" )
			    {
				    if (varDia == "29" )
				    {
					    if (varAno.substr(3, 2) == "00")
					    {
						    divisorBissexto = 400;
					    }
    					
					    if ((varAno % divisorBissexto) == 0)
					    {
						    ValidaDatatmp = true;
					    }
				    }
				    if (varDia <= "28") ValidaDatatmp = true;
			    }
			}
			else
			{
			    mens = "Data Invalida - Formato dd/mm/yyyy"	
			}
		}
		else
		{
			mens = "Data Invalida - Formato dd/mm/yyyy"	
		}
	}
	else
	{
		mens = "Data Invalida - Formato dd/mm/yyyy o Campo nao Numerico"
	}
	
	if (!ValidaDatatmp)
	{
		alert(mens);
		pValor.value = '';
		foco(pValor);
		return;
	}
}

function ValidaPeriodo(pValor, mens)
{
	var varMes, varAno, Fecha;
	
	if (pValor.value == "") return;
	
	Fecha = pValor.value.split('/');
	
	var ValidaDatatmp = true;
	
	varMes = Fecha[0];
	if (varMes.length > 4)
	{
		alert(mens);
		pValor.value = '';
		foco(pValor);
		return;
	}
	varAno = Fecha[1];
	if (IsNumericStr(varMes) & IsNumericStr(varAno) )
	{
		if (pValor.value.length == 7 )
		{
			if (parseFloat(varMes) > 12 || parseFloat(varMes) < 1)
			{
				ValidaDatatmp = false;
			}
		}
		else
		{
			ValidaDatatmp = false;
		}
	}
	else
	{
		ValidaDatatmp = false;
	}
	
	if (!ValidaDatatmp)
	{
		alert(mens);
		pValor.value = '';
		foco(pValor);
		return false;
	}
}

function IsNumericStr(pValor)
{
	for (var i = 0; i < pValor.length; i++)
	{
		var ch = pValor.substring(i, i + 1);
		if (ch < "0" || "9" < ch)
		{
		  return false;
		}
	}
	return true;
}

function IsNumericKeyCode(pValor)
{
	if (pValor > 47 & pValor < 58)
	{
		return true;
	}
	return false;
}

function FormataFecha(pValor, e)
{
	if (OutrosCodigosParaDigitar(e))
	{
		return;
	}
	else
	{
		if (KeyCodeMaster(e) < 48 || KeyCodeMaster(e) > 57)
		{
			if (detectaBrowser()=='firefox')
			{
				e.preventDefault();
				return;
			}
			else
			{
				event.returnValue = false;
				return;
			}
		}
	}
    
    if  (pValor.value.length == 2 || pValor.value.length == 5 )
    {
		pValor.value = pValor.value + "/"
	}
}

function UpperCase(pValor)
{
   
  	if (detectaBrowser()!='firefox')
	    {	
	        try{
		    event.keyCode = String.fromCharCode(window.event.keyCode).toUpperCase().charCodeAt(0);
		    }catch(err){
		    }
	    }else{			    	
    	 pValor.value = pValor.value.toUpperCase();  	    	
    	 
	    }
    	
  
}
function LowerCase(pValor)
{
	if (detectaBrowser()!='firefox')
	{
		event.keyCode = String.fromCharCode(window.event.keyCode).toLowerCase().charCodeAt(0);
    }else{	
    	pValor.value = pValor.value.toLowerCase();    	
	}
}

function vNuloNumeric(pValor)
{
	if (pValor == "" || pValor == null)
	{
		return 0;
	}
	return pValor;
}

function tiratudostr(pValor)
{
    var st, FUNCAO;
    st = "";
    
    if (pValor=="") return;
    
    for (var i = 0; i < pValor.length ; i++)
    {
        if ( IsNumericStr(pValor.substring(i, i + 1)) )
        {
           st = st + pValor.substring(i, i + 1);
        }
    }
    return st;
}

function tiratudo(pValor)
{
	if (pValor.value=="") return;
	pValor.value = tiratudostr(pValor.value);
}

String.prototype.lPad = function(pChar, pLen) 
{ 
	var tmp; 

	tmp = new String(this); 
	while (tmp.length < pLen) 
	{ 
		tmp = pChar + tmp; 
	} 
	return(tmp); 
} 

function VerificCPFCNPJ(campo, tipVerific, mens)
{
	var pValor, lonfor
	
	if (campo.value == "") return;
	
	if (tipVerific =="CNPJ")
	{
		lonfor = 14;
	}
	if (tipVerific =="CPF")
	{
		lonfor = 11;
	}

	tiratudo(campo);

	campo.value = String.prototype.lPad("0", lonfor - campo.value.length) + campo.value;
	
	if (!validaCPFCNPJ(campo.value, tipVerific) )
	{
		alert(mens);
		campo.value = '';
		foco(campo);
		return;
	}
	pValor = campo.value;

	if (tipVerific =="CNPJ")
	{
		campo.value = pValor.substring(0, 2) + "." + pValor.substring(2, 5) + "." + pValor.substring(5, 8) + "/" + pValor.substring(8, 12) + "-" + pValor.substring(12, 14);
	}
	if (tipVerific =="CPF")
	{
		campo.value = pValor.substring(0, 3) + "." + pValor.substring(3, 6) + "." + pValor.substring(6, 9) + "-" + pValor.substring(9, 11);
	}
}

function validaCPFCNPJ(CNUMB,CTYPE)
{
	CNUMB = tiratudostr(CNUMB);

	if (CNUMB == 0)	return(false);
	else
	{
		g = CNUMB.length-2;

		if (TestDigit(CNUMB,CTYPE,g))
		{
			g = CNUMB.length-1;

			if (TestDigit(CNUMB,CTYPE,g))
				return(true);
		else
			return(false);
		}
	else
		return(false);
	}
}

function TestDigit(CNUMB,CTYPE,g)
{
	var dig=0;
	var ind=2;

	for (f=g;f>0;f--)
	{
		dig += parseInt(CNUMB.charAt(f-1)) * ind;

		if (CTYPE == 'CNPJ')
		{ 
			if (ind > 8)
			ind = 2;
		else 
			ind++;
		}
		else
			ind++; 
		}

		dig %= 11;

		if (dig < 2)
			dig = 0;
		else
			dig = 11-dig;

		if (dig != parseInt(CNUMB.charAt(g)))
			return(false);
		else
		return(true);
}

function enviaAtivEcon(pCodigo, pDescricao, pTipo) 
{

	var pPagina, pQuat;
	var pai = window.opener;
	pPagina = pai.location.href.toLowerCase();
	
	pQuat = pPagina.split('/');
	
	//pPagina = 'CON_ActividadEconomicaGeralInstituicao.aspx';//pQuat[pQuat.length - 1];
	pPagina = pQuat[pQuat.length - 1];
	
	if(pPagina.indexOf("?")>0){
	    pPagina = pPagina.substring(0,pPagina.indexOf("?"));
	}	


	if (pPagina == "man_atividade_complemento.aspx")
	{
	    pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;
	}
	
	if (pPagina == "man_instituicion_generica.aspx")
	{
		pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;
	} 
	
	
	if (pPagina == "man_informacaogeralintituicaoviabilidade.aspx" && pTipo!=4)
	{
		pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;	
		
		pai.document.all.txtCodActiEconAlv.value = pCodigo;
		pai.document.all.txtDescActiEconAlv.value = pDescricao;	
		
		pai.document.all.txtCodActiEconVISA.value = pCodigo;
		pai.document.all.txtDescActiEconVISA.value = pDescricao;
		
	}
	
	if (pPagina == "man_informacaogeralintituicaoviabilidade.aspx" && pTipo==4)
	{
		pai.document.all.txtAtividadeBusca.value = pCodigo;
		pai.document.all.txtInstrucaoBusca.value = pDescricao;
	}
	
	
	
	if (pPagina == "man_instituicion.aspx")
	{
		pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;
	}
	
	if (pPagina == "viabilidadepedido.aspx")
	{
		pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;
	}
	
	if (pPagina == "viabilidadepedidoalteracao.aspx")
    {
	    pai.document.getElementById('Tabs_pnObjCnae_txtCodActiEcon').value = pCodigo;		     
	    pai.document.getElementById('Tabs_pnObjCnae_txtDescActiEcon').value = pDescricao;		    
	}
	if (pPagina == 'viabilidadepedidoavaliacao.aspx')
	{
		pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;
	}

	if (pPagina == "man_informacaogeralintituicao.aspx")
	{
		if (pTipo==0)
		{
			pai.document.all.txtCodActiEcon.value = pCodigo;
			pai.document.all.txtDescActiEcon.value = pDescricao;
		}
		if (pTipo==1)
		{
			pai.document.all.txtCodActiEconAlv.value = pCodigo;
			pai.document.all.txtDescActiEconAlv.value = pDescricao;
		}
		if (pTipo==2)
		{
			pai.document.all.txtCodActiEconVISA.value = pCodigo;
			pai.document.all.txtDescActiEconVISA.value = pDescricao;
		}
		if (pTipo==3)
		{
			pai.document.all.txtCodActiNoVisa.value = pCodigo;
			pai.document.all.txtDescActiNoVisa.value = pDescricao;
		}
		if (pTipo==4)
		{
		    pai.document.all.txtAtividadeBusca.value = pCodigo;
		    pai.document.all.txtInstrucaoBusca.value = pDescricao;
		}
	}
	this.close();
}
function enviaAtivEcon(pCodigo, pDescricao, pTipo, pSeq, pCnpj) 
{

	var pPagina, pQuat;
	var pai = window.opener;
	pPagina = pai.location.href.toLowerCase();
	
	pQuat = pPagina.split('/');
	
	//pPagina = 'CON_ActividadEconomicaGeralInstituicao.aspx';//pQuat[pQuat.length - 1];
	pPagina = pQuat[pQuat.length - 1];
	
	if(pPagina.indexOf("?")>0){
	    pPagina = pPagina.substring(0,pPagina.indexOf("?"));
	}		
	
	if (pPagina == "viabilidadepedidoalteracao.aspx")
    {
	    pai.document.getElementById('Tabs_pnObjCnae_txtCodActiEcon').value = pCodigo;		     
	    pai.document.getElementById('Tabs_pnObjCnae_txtDescActiEcon').value = pDescricao;		  
	    pai.document.getElementById('Tabs_pnObjCnae_txtSequenciaActiEcon').value = pSeq;	
	    pai.document.getElementById('Tabs_pnObjCnae_txtCnpjActiEcon').value = pCnpj;	  
	}
	if (pPagina == "viabilidadepedidoalteracaov2.aspx")
  {
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtCodActiEcon').value = pCodigo;		     
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtDescActiEcon').value = pDescricao;		  
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtSequenciaActiEcon').value = pSeq;	
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtCnpjActiEcon').value = pCnpj;	  
	}

	this.close();
}

function saltaCampo(campo,tamanhoMaximo,indice,evt)
{
	var vr = campo.value;
	var tam = vr.length;
	var elements = document.forms.aapf.elements;
	if (tam>=tamanhoMaximo && typeof(elements[indice])!='undefined'){
		for (i=0;i<elements.length;i++) {
			if (elements[i].tabIndex==indice+1){
				foco(elements[i]);
				return;
			}
		}
	}
}
function checkElemento(elemento, bool){
	if(document.getElementById(elemento)!= null){
		document.getElementById(elemento).checked = bool;
	}
}
function disableElemento(elemento, bool){
	if(document.getElementById(elemento)!= null){
		document.getElementById(elemento).disabled = bool;
	}
}
function show(elemento){
	if(document.getElementById(elemento)!= null){
		document.getElementById(elemento).style.display = '';
	}
}
function hidden(elemento){
	if(document.getElementById(elemento)!= null){
		document.getElementById(elemento).style.display = 'none';
	}
}
function valueElemento(elemento, value){
	if(document.getElementById(elemento)!= null){
		document.getElementById(elemento).value = value;
	}
}
function verificaDigitoProtocolo(protocolo)
{
    if(protocolo.length == 14 ){

		var valor;
		var digit;
		var retorno;
		var bit;
		var soma = 0;
		var peso = 2;
		var basee = 11;
		var resto = 0;
		var contador = 0;
		var digito =  protocolo.substr(protocolo.length-1, 1);
		protocolo = protocolo.substr(0, protocolo.length-1);
		valor = protocolo;
		contador = valor.length;
		while (contador >= 1)
		{
		   var aux = valor.substr(contador-1, 1);
			soma =  soma + (aux*peso);
			if(peso < basee)
			{
				peso++;
			}else
			{
				peso = 2;

			}
			contador--;
		}

		if(resto==1)
		{
		  retorno = soma % 11;
		}
		else
		{
		digit = 11 - (soma % 11);
		if (digit > 9) {
			digit = 0;
		}
		retorno = digit;
		}
		if(retorno!=digito || retorno==0){
		  alert("Digito invalido");
		}else
		{
		//alert("Digito válido");
		}
	}
	
 }
 function IsLetraKeyCode(pValor)

{

      if ((pValor >= 65 && pValor <= 90)  ||(pValor >= 97 && pValor <= 122))

      {

            return true;

      }

      return false;

}

function CodigoSetas(e)

{

      if (KeyCodeMaster(e) == 46  ||

            KeyCodeMaster(e) == 8  || 

            KeyCodeMaster(e) == 9  ||

            KeyCodeMaster(e) == 39 ||

            KeyCodeMaster(e) == 37 ||

            KeyCodeMaster(e) == 36 ||

            KeyCodeMaster(e) == 35)

      {

            return true ;

      }

      else

      {

            return false;

      }

}

 

function AlfaNumerico(pValor, e)

{

     if (detectaBrowser()=='firefox')

      {

         // alert(KeyCodeMaster(e));

            if (CodigoSetas(e))

            {

                return;

            }

            if (!IsNumericKeyCode(KeyCodeMaster(e)))

            {

                    if( !IsLetraKeyCode(KeyCodeMaster(e)) )

                    {

                        //alert(KeyCodeMaster(e));

                        e.preventDefault();

                          return;

                    }

        }

      }

      else

      {

            if( IsNumericKeyCode(event.keyCode) == true)

            {

                event.returnValue = true;

            }

            else

                {

                    if( IsLetraKeyCode(event.keyCode) == true)

                    {

                        event.returnValue = true;

                    }

                    else

                    {

                       event.returnValue = false;

                    }

            }

      }

}


