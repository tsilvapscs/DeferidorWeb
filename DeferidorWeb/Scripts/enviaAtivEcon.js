function enviaAtivEcon2(pCodigo, pDescricao, pTipo, pSeq, pCnpj) 
{

  
    
	var pPagina, pQuat;
	var pai = window.opener;
	pPagina = pai.location.href.toLowerCase();
	
	pQuat = pPagina.split('/');
	
	//	pPagina = 'CON_ActividadEconomicaGeralInstituicao.aspx';//pQuat[pQuat.length - 1];
	pPagina = pQuat[pQuat.length - 1];
	if(pPagina.indexOf("?")>0){
	    pPagina = pPagina.substring(0,pPagina.indexOf("?"));
	}
	
	
		/*Funcionalidade para lipeza de parametro*/
	if(pai.location.href.toLowerCase().indexOf('?')>0)
	{	
	    pPagina = pai.location.href.toLowerCase();        
        url_elementos = pPagina.split('?');	   
        pPagina = url_elementos[0];//pega a primeira parte sem os parametros
        pPagina = pPagina.substring(pPagina.lastIndexOf("/")+1,pPagina.indexOf(".aspx")); // pega a partir da ultima barra até o aspx;   
        pPagina = pPagina+".aspx"; // recompoe o aspx    
	}
	
	if (pPagina == "solicitacaoinscricao.aspx")
	{
	    pai.document.getElementById('TabContainer1_TabPanel1_txtCNAECodigo').value = pCodigo;		     
	    pai.document.getElementById('TabContainer1_TabPanel1_txtCNAEDescricao').value = pDescricao;	
	    
		//pai.document.all.TabContainer1_TabPanel1_txtCNAECodigo.value = pCodigo;
		//pai.document.all.TabContainer1_TabPanel1_txtCNAEDescricao.value = pDescricao;
	}
	if (pPagina == "requerimento2i.aspx")
	{
	    pai.document.getElementById('TabContainer1_TabPanel1_txtCNAECodigo').value = pCodigo;		     
	    pai.document.getElementById('TabContainer1_TabPanel1_txtCNAEDescricao').value = pDescricao;	
		// pai.document.all.TabContainer1_TabPanel1_txtCNAECodigo.value = pCodigo;
		// pai.document.all.TabContainer1_TabPanel1_txtCNAEDescricao.value = pDescricao;
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
	    pai.document.getElementById('Tabs_pnObjCnae_txtSequenciaActiEcon').value = pSeq;	
	    pai.document.getElementById('Tabs_pnObjCnae_txtCnpjActiEcon').value = pCnpj;	
	    pai.document.getElementById('Tabs_pnObjCnae').style.display = '';	   
	    
	}
	
	if (pPagina == 'viabilidadepedidoavaliacaoalter.aspx')
	{
		pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;
	}
	if (pPagina == 'viabilidadepedidoavaliacao.aspx')
	{
		pai.document.all.txtCodActiEcon.value = pCodigo;
		pai.document.all.txtDescActiEcon.value = pDescricao;
	}
	if (pPagina == 'viabilidadepedidoalteracaov2.aspx')
    {
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtCodActiEcon').value = pCodigo;		     
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtDescActiEcon').value = pDescricao;		  
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtSequenciaActiEcon').value = pSeq;	
	    pai.document.getElementById('Tabs_pnNomeEmpresarialObjCnae_txtCnpjActiEcon').value = pCnpj;	  
	}
	if (pPagina == 'con_actividadeconomicageralinstituicao.aspx')
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
