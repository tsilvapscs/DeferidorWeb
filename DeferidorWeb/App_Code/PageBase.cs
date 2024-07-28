using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using psc.Blocks.UI.WebForms;
using psc.Framework;
using RCPJ.DAL.Entities;
using psc.ApplicationBlocks.SessionState;
using RCPJ.DAL.Helper;
using RCPJ.BLL;
using RJ_Viabilidade;
using System.Xml;
using System.IO;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Ruc;
using System.Data.OracleClient;

namespace RCPJ.Application
{
	/// <summary>
	/// Summary description for PageBase: Classe Base.
	/// </summary>
	public class PageBase : StateManagingPage
    {


        #region variaveis globais
        //public const string CODEVENTO_CONSTITUICAO_EMPRESA = "101";
        //public const string CODEVENTO_CONSTITUICAO_FILIAL = "102";
        //public const string CODEVENTO_ALTERACAO_NOME_EMPRESARIAL = "220";
        //public const string CODEVENTO_ALTERACAO_ENDERECO_MUNICIPIOS_DENTRO_DO_ESTADO = "209";
        //public const string CODEVENTO_ALTERACAO_ENDERECO_DENTRO_DO_MESMO_MUNICIPIO = "211";
        //public const string CODEVENTO_ALTERACAO_ATIVIDADES_ECONOMICAS = "244";
        //public const string CODEVENTO_ALTERACAO_ENDERECO_ENTRE_ESTADOS = "210";
        //public const string CODEVENTO_INSCRICAO_NO_MUNICIPIO = "801";

        public const string CODEVENTO_CONSTITUICAO_EMPRESA = "101";
        public const string CODEVENTO_CONSTITUICAO_FILIAL = "102";
        public const string CODEVENTO_ALTERACAO_NOME_EMPRESARIAL = "220";
        public const string CODEVENTO_ALTERACAO_ENDERECO_MUNICIPIOS_DENTRO_DO_ESTADO = "209";
        public const string CODEVENTO_ALTERACAO_ENDERECO_DENTRO_DO_MESMO_MUNICIPIO = "211";
        public const string CODEVENTO_ALTERACAO_ATIVIDADES_ECONOMICAS = "244";
        public const string CODEVENTO_ALTERACAO_ENDERECO_ENTRE_ESTADOS = "210";
        public const string CODEVENTO_ALTERACAO_DATA_INICIO_ATIVIDADES = "415";
        public const string CODEVENTO_INSCRICAO_NO_MUNICIPIO = "801";
        public const string CODEVENTO_CAPITAL_SOCIAL = "247";
        public const string CODEVENTO_NATUREZA_JURIDICA = "225";
        public const string CODEVENTO_ALTERPORTE_EMPRESA = "222";
        public const string CODEVENTO_ALTER_QSA = "202";
        public const string CODEVENTO_CLAUSULA_ESPECIAL = "999";
        public const string VersaoSistema = "Versão 2.7";
        public const string VersaoDataSistema = "07/10/2015";
        private bOrgaoRegistro _pOrgaoRegistro;


        #endregion

        #region Implements
        public PageBase()
		{
			
		}
		/// <summary>
		///   Opa Poup aqui chama a JavaScript poptastic
		/// </summary>
		/// <param name="pagina">Pagina que chama</param>
		/// <param name="address">Endereco a ser chamado</param>
		/// <param name="Tipo"> Tipo</param>
		/// <param name="pdialogWidth"> Largura</param>
		/// <param name="pdialogHeight"></param>
		public static void Popup(Page pagina, string address, string Tipo, int pdialogWidth, int pdialogHeight)
		{			
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			builder.Append("<script>");
			builder.Append("poptastic('" + address + "','" + Tipo + "','" + pdialogWidth + "','" + pdialogHeight + "')");
			builder.Append("</script>");

			pagina.RegisterStartupScript("popup", builder.ToString());
			
		}
        public static void ComandoJavaString(Page pagina, string comando)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            builder.Append("<script>");
            builder.Append(comando);
            builder.Append("</script>");

            pagina.RegisterStartupScript("popup", builder.ToString());

        }
		public static void PopupBarra(Page pagina, string address, string Tipo, int pdialogWidth, int pdialogHeight)
		{			
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			builder.Append("<script>");
			builder.Append("poptasticbarra('" + address + "','" + Tipo + "','" + pdialogWidth + "','" + pdialogHeight + "')");
			builder.Append("</script>");

			pagina.RegisterStartupScript("popup", builder.ToString());
			
		}
		#endregion
		
		#region Properties

        public string retProt(string valor)
        {
            string temp = valor;
            temp = temp.Replace("-", "");
            temp = temp.Replace("/", "");
            temp = temp.Replace("_", "");
            temp = temp.Replace(".", "");
            return temp;

        }

        public string pTituloSistemaJUCESC
        {
            get
            {
                return Session["pTituloSistemaJUCESC"] == null ? String.Empty : Session["pTituloSistemaJUCESC"].ToString();
            }
            set
            {
                Session["pTituloSistemaJUCESC"] = value;
            }
        }

        public string pExigenciasComFundamentoLegal
        {
            get
            {
                string retorno;
                retorno = (ConfigurationManager.AppSettings["pExigenciasComFundamentoLegal"] != null) ? ConfigurationManager.AppSettings["pExigenciasComFundamentoLegal"].ToString() : "";
                if (retorno == "")
                {
                    retorno = "Parametro pExigenciasComFundamentoLegal não configurado!!";
                }
                return retorno;
            }
        }

        //public string pUsuarioSistema
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Session.IsNewSession || Session["pUsuarioSistemaJUCESC"] == null ? String.Empty : Session["pUsuarioSistemaJUCESC"].ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            return "";
        //        }
        //    }
        //    set
        //    {
        //        Session["pUsuarioSistemaJUCESC"] = value;
        //    }
        //}


        //public bool pUsuarioExterno
        //{
        //    get
        //    {
        //        return Session.IsNewSession || Session["pUsuarioExterno"]==null ? false : (bool)Session["pUsuarioExterno"];
        //    }
        //    set
        //    {
        //        Session["pUsuarioExterno"] = value;
        //    }
        //}
        public string pFormatoProcessoOR
        {
            get
            {
                switch (OrgaoRegistro)
                {
                    case "JUCERJA":
                        return "{0:0#-####/######-#}";
                        break;

                    case "JUCEPE":
                        return "{0:0#/######-#}";
                        //exemplo 13/689168-3
                        break;

                    case "JUCEES":
                        return "{0:0#/######-#}";
                        //exemplo 13/999999-9
                        break;
                    default:
                        return "{0:0#/######-#}";
                        break;
                }
            }
        
        }

        public string pMascaraProcessoOR
        { //"99-9999/999999-9"
            get
            {
                switch (OrgaoRegistro)
                {
                    case "JUCERJA":
                        return "99-9999/999999-9";
                        break;

                    case "JUCEPE":
                        return "99/999999-9";
                        //exemplo 13/689168-3
                        break;
                    case "RCPJ":
                        return "9-9999-99999999999";
                        break;
                    default:
                        return "99/999999-9";
                        break;
                }
            }
        

        }

		public DateTime pDataUltimaMovimentacaoSistema
		{
			get
			{
               try{
				return Session.IsNewSession || Session["pDataUltimaMovimentacaoSistema"]==null ? General.DataMinValue() : (DateTime)Session["pDataUltimaMovimentacaoSistema"];
                }
                catch (Exception ex)
                {
                    return new DateTime();
                }
			}
			set
			{
				Session["pDataUltimaMovimentacaoSistema"] = value;
			}
		}
		public string pUsuarioSistema
		{
			get
			{  	
                return Session["pUsuarioSistema"]==null ? String.Empty : Session["pUsuarioSistema"].ToString();
            }
			set
			{
				Session["pUsuarioSistema"] = value;
			}
		}
        public string pNumeroServentia
        {
            get
            {
                return Session["pNumeroServentia"] == null ? String.Empty : Session["pNumeroServentia"].ToString();
            }
            set
            {
                Session["pNumeroServentia"] = value;
            }
        }
		public string pXmlXls
		{
			get
			{
				return Session.IsNewSession || Session["pXmlXls"]==null ? String.Empty : Session["pXmlXls"].ToString();
			}
			set
			{
				Session["pXmlXls"] = value;
			}
		}

		public string pNomeUsuario
		{
			get
			{
				return Session.IsNewSession || Session["pNomeUsuario"]==null ? String.Empty : Session["pNomeUsuario"].ToString();
			}
			set
			{
				Session["pNomeUsuario"] = value;
			}
		}
		public string pDateAcessoSistema
		{
			get
			{
				return Session.IsNewSession || Session["pDateAcessoSistema"]==null ? String.Empty : Session["pDateAcessoSistema"].ToString();
			}
			set
			{
				Session["pDateAcessoSistema"] = value;
			}
		}
		public string pNomeInstituicao
		{
			get
			{
				return Session.IsNewSession || Session["pNomeInstituicao"]==null ? String.Empty : Session["pNomeInstituicao"].ToString();
			}
			set
			{
				Session["pNomeInstituicao"] = value;
			}
		}
		public string pTituloSistema
		{
			get
			{
				return Session["pTituloSistema"]==null ? String.Empty : Session["pTituloSistema"].ToString();
			}
			set
			{
				Session["pTituloSistema"] = value;
			}
		}
        public string pHelpCaminhoArquivo
        {
            get
            {
                return Session["pHelpCaminhoArquivo"] == null ? String.Empty : Session["pHelpCaminhoArquivo"].ToString();
            }
            set
            {
                Session["pHelpCaminhoArquivo"] = value;
            }
        }
        public string pUFDefault
        {
            get
            {
                return ConfigurationManager.AppSettings["UFPrincipal"].ToString();
            }
        }
        public string pPageInicial
		{
			get
			{
                return Session.IsNewSession || Session["pPageInicial"] == null ? String.Empty : Session["pPageInicial"].ToString();
			}
			set
			{
                Session["pPageInicial"] = value;
			}
		}
		public DataTable pPagesAcesso
		{
			get
			{
				return Session.IsNewSession || Session["pPagesAcesso"]==null ? new DataTable() : (DataTable)Session["pPagesAcesso"];
			}
			set
			{
				Session["pPagesAcesso"] = value;
			}
		}
		public DataTable pDtParaExportarSession
		{
			get
			{
				return Session.IsNewSession || Session["pDtParaExportarSession"]==null ? new DataTable() : (DataTable)Session["pDtParaExportarSession"];
			}
			set
			{
				Session["pDtParaExportarSession"] = value;
			}
		}
		public ArrayList pAlColunasExportarSession
		{
			get
			{
				return Session.IsNewSession || Session["pAlColunasExportarSession"]==null ? new ArrayList() : (ArrayList)Session["pAlColunasExportarSession"];
			}
			set
			{
				Session["pAlColunasExportarSession"] = value;
			}
		}
		public string pCNPJIntituicaoUsuario
		{
			get
			{
				return Session["pCNPJIntituicao"]==null ? String.Empty : Session["pCNPJIntituicao"].ToString();
			}
			set
			{
				Session["pCNPJIntituicao"] = value;
			}
		}
		public decimal pRegionalIntituicao
		{
			get
			{
				return Session.IsNewSession || Session["pRegionalIntituicao"]==null ? int.MinValue : decimal.Parse(Session["pRegionalIntituicao"].ToString());
			}
			set
			{
				Session["pRegionalIntituicao"] = value;
			}
		}
		public string pTipoAcessoUsuario
		{
			get
			{
				string pTipo = Session.IsNewSession || Session["pTipoAcessoUsuario"]==null ? String.Empty : Session["pTipoAcessoUsuario"].ToString();
				//Codigo Temporario
                return pUsuarioSistema == "52090353287" ? "1" : pTipo;
			}
			set
			{
				Session["pTipoAcessoUsuario"] = value;
			}
		}
		public decimal pMunicipioUsuarioConectado
		{
			get
			{
				return Session.IsNewSession || Session["pMunicipio"]==null ? int.MinValue : (decimal)Session["pMunicipio"];
			}
			set
			{
				Session["pMunicipio"] = value;
			}
		}
		public string pUsuarioRedeTERMINALSERVER
		{
			get
			{
				return Session.IsNewSession || Session["pUsuarioRedeTERMINALSERVER"]==null ? String.Empty : Session["pUsuarioRedeTERMINALSERVER"].ToString();
			}
			set
			{
				Session["pUsuarioRedeTERMINALSERVER"] = value;
			}
		}
        public string pAmbiente
        {
            get
            {
                return (ConfigurationManager.AppSettings["Ambiente"]!=null) ? ConfigurationManager.AppSettings["Ambiente"].ToString() : "";
            }
        }

        public string pSiteLoginOrgaoRegistro
        {
            get
            {
                string retorno;
                retorno=(ConfigurationManager.AppSettings["SiteLoginOrgaoRegistro"] != null) ? ConfigurationManager.AppSettings["SiteLoginOrgaoRegistro"].ToString() : "";
                if(retorno=="")
                {
                    retorno = "exame1.aspx";
                }
                return retorno;
            }
        }

        public string pEnderecoConsultaDadosEmpresa
        {
            get
            {
                string retorno;
                retorno = (ConfigurationManager.AppSettings["EnderecoConsultaDadosEmpresa"] != null) ? ConfigurationManager.AppSettings["EnderecoConsultaDadosEmpresa"].ToString() : "";
                if (retorno == "")
                {
                    retorno = "Parametro EnderecoConsultaDadosEmpresa não configurado!!";
                }
                return retorno;
            }
        }
        
        public string pCNPJInstituicaoFixo
        {
            get
            {
                return (ConfigurationManager.AppSettings["pCNPJInstituicaoFixo"] != null) ? ConfigurationManager.AppSettings["pCNPJInstituicaoFixo"].ToString() : "";
            }
        }
        public string pCNPJInstituicaoDefault
        {
            get
            {
                return (ConfigurationManager.AppSettings["pCNPJInstituicaoDefault"] != null) ? ConfigurationManager.AppSettings["pCNPJInstituicaoDefault"].ToString() : "";
            }
        }
        public string validaTipoOrgaodeRegistroDeferimento(WsRFB.Retorno retornoWS)
        {
            if (retornoWS.oWs35Response.dadosRedesim != null && retornoWS.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento != null)
            {
                string _ret = "";
                if (retornoWS.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento == "")
                {
                    _ret = "O dbe está direcionado para a RFB. Você deve cancelá-lo e transmitir um novo pedido para o Órgão de Registro.";
                    return _ret;

                }
                if (retornoWS.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento == "RFB" && retornoWS.oWs35Response.dadosRedesim.fcpj.codTipoOrgaoRegistro != "3")
                {
                    _ret = "O dbe está direcionado para a RFB. Você deve cancelá-lo e transmitir um novo pedido para o Órgão de Registro.";
                    return _ret;
                }


                if (OrgaoRegistro != "RCPJ")
                {
                    if (retornoWS.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento.Substring(0, 2) != "JC")
                    {
                        if (retornoWS.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento != "SFCTA")
                            _ret = "O dbe está direcionado para a RFB. Você deve cancelá-lo e transmitir um novo pedido para a Junta Comercial.";
                    }
                }
                else
                {
                    if ((POrgaoRegistro.CodigoDBE != retornoWS.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento)
                        && retornoWS.oWs35Response.dadosRedesim.fcpj.codTipoOrgaoRegistro != "3")
                    {
                        _ret = "O dbe não está direcionado para a Cartório. Você deve cancelá-lo e transmitir um novo pedido para o Órgão de Registro.";
                    }
                }
            }
            return "";
        }
        public bOrgaoRegistro POrgaoRegistro
        {
            get
            {
                if (_pOrgaoRegistro == null)
                {
                    _pOrgaoRegistro = new bOrgaoRegistro(pCNPJInstituicaoDefault);
                }
                return _pOrgaoRegistro;
            }
            set { _pOrgaoRegistro = value; }
        }
        public string OrgaoRegistro
        {
            get { return ConfigurationManager.AppSettings["TipoOrgaoRegistro"].ToString(); }
        }
        public string pLogoCabecalho
        {
            get { return "img/" +OrgaoRegistro+"Cabecalho.gif"; }
        }
        public string pLogoRodape
        {
            get { return "img/" + OrgaoRegistro + "rodape.gif"; }
        }
		#endregion
	
		#region TratarErroresDeSistema
		public void ErroresDeSistema(Exception ex)
		{
			using (PSC_ERRORLOG_SISTEMA c = new PSC_ERRORLOG_SISTEMA())
			{
				c.pes_message = ex.Message;
				c.pes_source = ex.Source;
				c.pes_stacktrace = ex.StackTrace + " Data Ultima:  " + pDataUltimaMovimentacaoSistema.ToString("dd/MM/yyyy hh:mm:ss");
				c.pes_targetsite = ex.TargetSite.Name;
				c.pes_usuario = pUsuarioSistema;

				c.Insert();
			}
            if (ex.Message == "Object reference not set to an instance of an object.")
            {
                //Response.Redirect("ErrorPage.aspx" + "?id=Usuário foi desconectado, ou a sessão expirou. ", false);
            }
			
		}

		public void ErroresDeSistema(Exception ex, ref ValidationSummary ErrorSummary)
		{
			ErrorSummary.AddErrorMessage("Erro: " + ex.Message);
			ErroresDeSistema(ex);
		}

		public void ErroresDeSistema(string msg, Exception ex, ref ValidationSummary ErrorSummary)
		{
			ErrorSummary.AddErrorMessage("");
            ErrorSummary.AddErrorMessage(msg + " Erro: " + ex.Message);
            ErroresDeSistema(ex);
		}
		#endregion

		#region Outros
		public void SetFocus(string pControl)
		{
			string strScript = "<script>foco(document.forms[0]." + pControl + ");</script>";
			Page.RegisterStartupScript("ClientScript", strScript);

		}
		public string pCaminodoSistema()
		{
			string pHost = Request.Url.Host;
			string pScheme = Request.Url.Scheme;
			string pApplicationPath = Request.ApplicationPath;

			return pScheme + "://" + pHost + pApplicationPath + "/";
		}
		public void ConectarNoSIARCO ()
		{
            string pServerTS = ConfigurationManager.AppSettings["EnderecoTSServer"].ToLower();

            string PortaTSServer = ConfigurationManager.AppSettings["PortaTSServer"].ToLower();

            string pServerSite = "http://" + ConfigurationManager.AppSettings["EnderecoTSServerSite"].ToLower();

      		string pConecao="";

			//pConecao = pServer2 + "/TsWebLogin/login.aspx?Username=" + pUsuarioRedeTERMINALSERVER + "&Server=" + pServer + "&Domain=''";
            pConecao = pServerSite + "?usuario=" + pUsuarioRedeTERMINALSERVER + "&servidor=" + pServerTS + "&dominio=" + "&porta=" + PortaTSServer;
			
			//Response.Redirect(pConecao, false);
			Popup(this, pConecao, "CONECAOTERMINALSEVERVIAWEB", 500, 700);
		}
		#endregion

		#region Validacao
		public bool ValidaMunicipioConvenio(decimal pMunicipio)
		{
			return dHelperQuery.ValidaMunicipioConvenio(pMunicipio);
//			string[] Municipios = ConfigurationSettings.AppSettings["MunicipiosNoConvenio"].ToString().Split('|');
//			for (int a = 0; a < Municipios.Length ; a++)
//			{
//				if (pMunicipio == decimal.Parse(Municipios[a].ToString()))
//				{
//					return true;
//				}
//			}
			//return false;
		}
		#endregion

		#region  ConvertArrayList em DataTable
		public static DataTable ConvertArrayListToDataTable(ArrayList arrayList)
		{
			DataTable dt = new DataTable();

			if (arrayList.Count != 0)
			{
				dt = ConvertObjectToDataTableSchema(arrayList[0]);
				FillData(arrayList, dt);
			}

			return dt;
		}

		public static DataTable ConvertObjectToDataTableSchema(Object o)
		{
			DataTable dt = new DataTable();
			PropertyInfo[] properties = properties = o.GetType().GetProperties();

			foreach (PropertyInfo property in properties)
			{
				if (property.CanRead)
				{
					DataColumn dc = new DataColumn(property.Name);
					dc.DataType = property.PropertyType; 
					dt.Columns.Add(dc);
				}
			}
			return dt;
		}

		private static void FillData(ArrayList arrayList, DataTable dt)
		{
			foreach (Object o in arrayList)
			{
				DataRow dr = dt.NewRow();
				PropertyInfo[] properties = o.GetType().GetProperties();

				foreach (PropertyInfo property in properties)
				{
					if (property.CanRead)
					{
						dr[property.Name] = property.GetValue(o, null);
					}
				}
				dt.Rows.Add(dr);
			}
		}
		#endregion

        public string TiraAcento(string pValue)
        {

            string pResult = pValue;

            pResult = pResult.Replace('À', 'A');
            pResult = pResult.Replace('Á', 'A');
            pResult = pResult.Replace('Â', 'A');
            pResult = pResult.Replace('Ã', 'A');
            pResult = pResult.Replace('Ä', 'A');

            pResult = pResult.Replace('à', 'a');
            pResult = pResult.Replace('á', 'a');
            pResult = pResult.Replace('â', 'a');
            pResult = pResult.Replace('ã', 'a');
            pResult = pResult.Replace('ä', 'a');

            pResult = pResult.Replace('È', 'E');
            pResult = pResult.Replace('É', 'E');
            pResult = pResult.Replace('Ê', 'E');
            pResult = pResult.Replace('Ë', 'E');

            pResult = pResult.Replace('è', 'e');
            pResult = pResult.Replace('é', 'e');
            pResult = pResult.Replace('ê', 'e');
            pResult = pResult.Replace('ë', 'e');

            pResult = pResult.Replace('Ì', 'I');
            pResult = pResult.Replace('Í', 'I');
            pResult = pResult.Replace('Î', 'I');
            pResult = pResult.Replace('Ï', 'I');

            pResult = pResult.Replace('ì', 'i');
            pResult = pResult.Replace('í', 'i');
            pResult = pResult.Replace('î', 'i');
            pResult = pResult.Replace('ï', 'i');

            pResult = pResult.Replace('Ò', 'O');
            pResult = pResult.Replace('Ó', 'O');
            pResult = pResult.Replace('Ô', 'O');
            pResult = pResult.Replace('Õ', 'O');
            pResult = pResult.Replace('Ö', 'O');

            pResult = pResult.Replace('ò', 'o');
            pResult = pResult.Replace('ó', 'o');
            pResult = pResult.Replace('ô', 'o');
            pResult = pResult.Replace('õ', 'o');
            pResult = pResult.Replace('ö', 'o');

            pResult = pResult.Replace('Ù', 'U');
            pResult = pResult.Replace('Ú', 'U');
            pResult = pResult.Replace('Û', 'U');
            pResult = pResult.Replace('Ü', 'U');

            pResult = pResult.Replace('ù', 'u');
            pResult = pResult.Replace('ú', 'u');
            pResult = pResult.Replace('û', 'u');
            pResult = pResult.Replace('ü', 'u');

            pResult = pResult.Replace('Ç', 'C');
            pResult = pResult.Replace('ç', 'c');

            return pResult;


        }


        protected DataSet CarregaDadosWebServiceEmpresa(string pNire)
        {

            DataSet result = new DataSet();
            ServicosPortal.ServicosPortal wsControle = new ServicosPortal.ServicosPortal();
            wsControle.Url = ConfigurationManager.AppSettings["ServicosPortal"].ToString(); 
            return wsControle.getXmlEmpresaDs(pNire);

        }

        protected bool getEventoAvalia(List<bProtocoloEvento> ProtocoloEvento, string TipoDeEvento)
        {
            for (int i = 0; i < ProtocoloEvento.Count; i++)
            {
                bProtocoloEvento ev;
                ev = (bProtocoloEvento)ProtocoloEvento[i];
                if (ev.CodigoEvento == decimal.Parse(TipoDeEvento))
                {
                    return true;
                }
            }
            return false;
        }
        public Boolean ValidaCampoTexto(System.Web.UI.Control controle, string msg)
        {
            Boolean wChave = true;
            if (controle is System.Web.UI.WebControls.TextBox)
            {
                System.Web.UI.WebControls.TextBox tb = controle as System.Web.UI.WebControls.TextBox;
                tb.Text = tb.Text.Trim();
                if (tb.Text.Equals("") || tb.Text.Equals("____-____"))
                {
                    tb.BorderColor = System.Drawing.Color.Red;
                    tb.BorderWidth = 2;
                    wChave = false;
                }
                else
                {
                    tb.BorderColor = System.Drawing.Color.Gray;
                    tb.BorderWidth = 1;
                }
            }
            if (controle is System.Web.UI.WebControls.DropDownList)
            {
                System.Web.UI.WebControls.DropDownList dl = controle as System.Web.UI.WebControls.DropDownList;

                if (dl.SelectedValue.Equals(""))
                {
                    dl.BorderColor = System.Drawing.Color.Red;
                    dl.BorderWidth = 2;
                    wChave = false;
                }
                else
                {
                    dl.BorderColor = System.Drawing.Color.Gray;
                    dl.BorderWidth = 1;
                }
            }
            return wChave;
        }
        public string FormataDBE(string wDBE)
        {
            string oRetorno = "";
            if (!string.IsNullOrEmpty(wDBE) && wDBE.Length == 24)
            {
                oRetorno = wDBE.Substring(0, 2) + "." + wDBE.Substring(2, 2) + "." + wDBE.Substring(4, 2) + "." + wDBE.Substring(6, 2) + "." + wDBE.Substring(8, 2);
                oRetorno += "-" + wDBE.Substring(10, 2) + "." + wDBE.Substring(12, 3) + "." + wDBE.Substring(15, 3) + "." + wDBE.Substring(18, 3) + "." + wDBE.Substring(21, 3);
            }
            else
            {
                oRetorno = wDBE;
            }
            return oRetorno;
        }
        public string FormataCEP(string wCEP)
        {
            string oRetorno = "";
            if (!string.IsNullOrEmpty(wCEP))
            {
                oRetorno = wCEP.Substring(0, 2) + "." + wCEP.Substring(2, 3) + "-" + wCEP.Substring(5,3);
            }
            return oRetorno;
        }

        #region DBE
        public WsRFB.Retorno getDbeRFB(string pDbe)
        {

            //ES58995068 - recibo
            //00013525387768 - identificacao

            string _identificacao = "";
            string _recibo = "";

            _recibo = pDbe.Substring(0, 10);
            _identificacao = pDbe.Substring(10, 14);


            WsRFB.ServiceReginRFB regin = new WsRFB.ServiceReginRFB();

            WsRFB.Retorno resulRegin = new WsRFB.Retorno();

            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

            resulRegin = regin.ServiceWs35Regin(_identificacao, _recibo);

            return resulRegin;

        }

        #region Carrega Dados Wb Viabilidade
        protected DataSet CarregaDadosWebServiceDbe(string pDbe)
        {

            ServicosRequerimento.Services wsControle = new ServicosRequerimento.Services();
            wsControle.Url = ConfigurationManager.AppSettings["wsServicoRequerimento"].ToString();
            wsControle.Timeout = 10000;
            return wsControle.GetDbeDS(pDbe);

        }
        public string ViabilidadeConcluida
        {
            get
            {
                return (ConfigurationManager.AppSettings["ViabilidadeConcluida"] != null) ? ConfigurationManager.AppSettings["ViabilidadeConcluida"].ToString() : "on";
            }
        }
        public static DataSet CarregaDadosWebServiceViabilidade(string protocolo)
        {

            DataSet result = new DataSet();
            WsControleViab wsControle = new WsControleViab();
            wsControle.Url = ConfigurationManager.AppSettings["RJ_Viabilidade.WsControleViab"].ToString();
            System.Xml.XmlNode no = wsControle.GetXmlViabilidade(protocolo);
            if (no == null)
            {
                throw new Exception("Erro no ws " + wsControle.Url);
            }
            else
            {
                XmlTextReader reader = new XmlTextReader(new StringReader(no.OuterXml));
                result.ReadXml(reader);
                return result;
            }

        }
        #endregion
        #endregion

        #region Preenche Req com Ws35
        public bRequerimento ProcessaDadosDbe(WsRFB.serviceResponse pReponse)
        {

            bRequerimento c = new bRequerimento();
            bool eventoConstituicao = false;


            #region Empresa
            WsRFB.redesim DadosDbe = pReponse.dadosRedesim;
            //pj.t73302_caixa_postal = Global.valNuloBranco(DadosDbe.fcpj.caixaPostal);
            if (!Global.valNulo(DadosDbe.fcpj.capitalSocial))
            {
                c.CapitalSocial = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
            }


            if (DadosDbe.fcpj != null)
            {
                if (DadosDbe.numViabilidadeAssociada != null && DadosDbe.numViabilidadeAssociada != "")
                {
                    c.ProtocoloViabilidade = DadosDbe.numViabilidadeAssociada;
                }

                if (DadosDbe.fcpj.codMotivoSituacaoCadastral != null && DadosDbe.fcpj.codMotivoSituacaoCadastral != "")
                {
                    //Motivo Baixa
                    //01	Extinção, pelo encerramento da liquidação voluntária
                    //02	Incorporação
                    //03	Fusão
                    //04	Cisão Total
                    //05	Encerramento do processo de falência
                    //06	Encerramento do processo de liquidação extrajudicial
                    //07	Extinção, por unificação da inscrição da filial
                    //13	Omissa e não localizada
                    //14	Omissa Contumaz
                    //15	Inexistente de fato
                    //28	Transformação do órgão regional à condição de matriz
                    //31	Elevação de filial à condição de matriz
                    //33	Transformação do órgão local à condição de filial do órgão regional
                    //37	Baixa de Produtor Rural
                    //54	Extinção - Tratamento diferenciado dado às ME e EPP (Lei Complementar nº 123/2006)

                    c.TextoRestituicaoBaixa = DadosDbe.fcpj.codMotivoSituacaoCadastral + " - ";
                    switch (DadosDbe.fcpj.codMotivoSituacaoCadastral.Trim())
                    {
                        case "01":
                            c.TextoRestituicaoBaixa += "Extinção, pelo encerramento da liquidação voluntária";
                            break;
                        case "02":
                            c.TextoRestituicaoBaixa += "Incorporação";
                            break;
                        case "03":
                            c.TextoRestituicaoBaixa += "Fusão";
                            break;
                        case "04":
                            c.TextoRestituicaoBaixa += "Cisão Total";
                            break;
                        case "05":
                            c.TextoRestituicaoBaixa += "Encerramento do processo de falência";
                            break;
                        case "06":
                            c.TextoRestituicaoBaixa += "Encerramento do processo de liquidação extrajudicial";
                            break;
                        case "07":
                            c.TextoRestituicaoBaixa += "Extinção, por unificação da inscrição da filial";
                            break;
                        case "13":
                            c.TextoRestituicaoBaixa += "Omissa e não localizada";
                            break;
                        case "14":
                            c.TextoRestituicaoBaixa += "Omissa Contumaz";
                            break;
                        case "15":
                            c.TextoRestituicaoBaixa += "Inexistente de fato";
                            break;
                        case "28":
                            c.TextoRestituicaoBaixa += "Transformação do órgão regional à condição de matriz";
                            break;
                        case "31":
                            c.TextoRestituicaoBaixa += "Elevação de filial à condição de matriz";
                            break;
                        case "33":
                            c.TextoRestituicaoBaixa += "Transformação do órgão local à condição de filial do órgão regional";
                            break;
                        case "37":
                            c.TextoRestituicaoBaixa += "Baixa de Produtor Rural";
                            break;
                        case "54":
                            c.TextoRestituicaoBaixa += "Extinção - Tratamento diferenciado dado às ME e EPP (Lei Complementar nº 123/2006)";
                            break;
                        default:
                            c.TextoRestituicaoBaixa += "Motivo não encontrado";
                            break;
                    }
                }

                if (DadosDbe.fcpj.endereco != null)
                {
                    c.SedeBairro = Global.valNuloBranco(DadosDbe.fcpj.endereco.bairro);
                    c.SedeCEP = Global.valNuloBranco(DadosDbe.fcpj.endereco.cep);
                    c.SedeMunicipio = Global.valNuloBranco(DadosDbe.fcpj.endereco.codMunicipio);
                    if (c.SedeMunicipio != "")
                    {
                        c.SedeMunicipio += psc.Framework.General.CalculateVerificationDigit(c.SedeMunicipio, 11).ToString();
                        c.SedeMunicipio = int.Parse(c.SedeMunicipio).ToString();
                    }
                    //pj.t73302_cod_pais = Global.valNuloBranco(DadosDbe.fcpj.endereco.codPais);
                    c.SedeComplemento = Global.valNuloBranco(DadosDbe.fcpj.endereco.complementoLogradouro);
                    //c.seded = Global.valNuloBranco(DadosDbe.fcpj.endereco.distrito);
                    c.SedeLogradouro = Global.valNuloBranco(DadosDbe.fcpj.endereco.logradouro);
                    c.SedeNumero = Global.valNuloBranco(DadosDbe.fcpj.endereco.numLogradouro);
                    //c.Sede = Global.valNuloBranco(DadosDbe.fcpj.endereco.referencia);
                    c.SedeTipoLogradouro = Global.valNuloBranco(DadosDbe.fcpj.endereco.codTipoLogradouro);
                    c.SedeDsTipoLogradouro = c.SedeTipoLogradouro;
                    //pj.t73302_cidade_exterior = Global.valNuloBranco(DadosDbe.fcpj.endereco.cidadeExterior);
                    c.SedeUF = Global.valNuloBranco(DadosDbe.fcpj.endereco.uf);
                    //pj.t73302_cod_tipo_unidade = Global.valNuloBranco(DadosDbe.atividadeEconomica.codTipoUnidade);
                }
            }

            //c.Sedeca = Global.valNuloBranco(DadosDbe.fcpj.cepCaixaPostal);
            if (DadosDbe.cnpj.Trim().Length > 12)
            {
                c.nrEmpresaCNPJ = Global.valNuloBranco(DadosDbe.cnpj);
            }
            if (Global.valNuloBranco(DadosDbe.fcpj.nire) != "")
            {
                c.nrMatricula = Global.valNuloBranco(decimal.Parse(DadosDbe.fcpj.nire));
            }
            //pj.t73302_tip_org_registro = Global.valNuloBranco(DadosDbe.fcpj.codTipoOrgaoRegistro);
            c.Porte = Global.valNuloBranco(DadosDbe.fcpj.codPorteEmpresa);

            c.Nome_Fantasia = Global.valNuloBranco(DadosDbe.fcpj.nomeFantasia);
            c.NaturezaJuridicaCodigo = int.Parse(Global.valNuloBranco(DadosDbe.fcpj.codNaturezaJuridica));

            DataTable tdNt = dHelperQuery.getNaturezaJuridicaRequerimento(decimal.Parse(c.NaturezaJuridicaCodigo.ToString()));

            if (tdNt.Rows.Count > 0)
            {
                c.NaturezaJuridicaDescricao = tdNt.Rows[0]["t009_ds_natureza_juridica"].ToString();
            }
            else
            {
                c.NaturezaJuridicaDescricao = "Natureza não encontrada na tabela";
            }

            c.SedeNome = Global.valNuloBranco(DadosDbe.fcpj.nomeEmpresarial);


            //PORTE DA EMPRESA (DE-PARA)
            //DBE ==>   01 – MICRO-EMPRESA, 03 – EMPRESA PEQUENO PORTE, 05 – DEMAIS
            //REQ ==>   1119 - ME                   1118 - EPP          1120 - NORMAL  
            string porte = Global.valNuloBranco(DadosDbe.fcpj.codPorteEmpresa);
            if (porte != "")
            {
                switch (porte)
                {
                    case "01":
                        c.Enquadramento = 1119;
                        //c._EnquadramentoDESCRICAO = "MICRO EMPRESA";
                        break;
                    case "03":
                        c.Enquadramento = 1118;
                        //c.EnquadramentoDESCRICAO = "PEQUENO PORTE";
                        break;
                    case "05":
                        c.Enquadramento = 1120;
                        break;
                    default:
                        c.Enquadramento = 1120;
                        //c.EnquadramentoDESCRICAO = "DEMAIS";
                        break;
                }

            }


            if (DadosDbe.atividadeEconomica != null)
            {
                c.ObjetoSocial = Global.valNuloBranco(DadosDbe.atividadeEconomica.objetoSocial);
                //c.CNAEs = Global.valNuloBranco(DadosDbe.atividadeEconomica.codCnaeFiscal);
            }

            if (DadosDbe.fcpj.contato != null)
            {
                c.SedeEmail = Global.valNuloBranco(DadosDbe.fcpj.contato.correioEletronico);
                c.SedeTelefone = Global.valNuloBranco(DadosDbe.fcpj.contato.telefone1);
                //pj.t73302_telefone_2 = Global.valNuloBranco(DadosDbe.fcpj.contato.telefone2);
                //c.sedef = Global.valNuloBranco(DadosDbe.fcpj.contato.dddFax);
                c.SedeDDD = Global.valNuloBranco(DadosDbe.fcpj.contato.dddTelefone1);
                //pj.t73302_ddd_telefone_2 = Global.valNuloBranco(DadosDbe.fcpj.contato.dddTelefone2);
                //pj.t73302_fax = Global.valNuloBranco(DadosDbe.fcpj.contato.fax);
            }

            //Pegar dados do representante
            if (DadosDbe.fcpj.cpfResponsavel != null && DadosDbe.fcpj.cpfResponsavel != "")
            {
                c.Requerente.Cpf = DadosDbe.fcpj.cpfResponsavel;
                c.Requerente.Nome = DadosDbe.fcpj.nomeResponsavel;
                c.Requerente.DDD = DadosDbe.fcpj.codQualificResponsavel;
            }

            if (DadosDbe.fcpj.codNaturezaJuridica == "2135")
            {
                bSocios cSocio = new bSocios();

                #region Preenche Socio caso seja naturea Empresaio
                if (DadosDbe.fcpj.cpfResponsavel != "")
                {
                    WsRFB.endereco enderecoResponsavel = new WsRFB.endereco();
                    WsRFB.contato contatoResponsavel = new WsRFB.contato();

                    /*
                        Comença com o dado da emporesa mesmo, caso venha o endereço do responsavel pego de la
                     * comentado para nao levar o endereço da empresa para o socio, solicitado por xico 28/10/2014
                     * alegando que os estados falaram que a pessoa quasi sempre chega com o memso endereço, por isso
                     * sera forçado a digitar ele novamente se for o caso.
                     */
                    //enderecoResponsavel = DadosDbe.fcpj.endereco;
                    //contatoResponsavel = DadosDbe.fcpj.contato;

                    if (DadosDbe.fcpj.endResponsavel != null)
                    {
                        if (!Global.valNulo(DadosDbe.fcpj.endResponsavel.bairro) && !Global.valNulo(DadosDbe.fcpj.endResponsavel.cep))
                        {
                            enderecoResponsavel = DadosDbe.fcpj.endResponsavel;
                            contatoResponsavel = DadosDbe.fcpj.contatoResponsavel;
                        }
                    }

                    cSocio.EndBairro = enderecoResponsavel.bairro;
                    if (!Global.valNulo(DadosDbe.fcpj.capitalSocial))
                    {
                        //qsa.t73303_capital_social_empresa = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                        //qsa.t73303_capital_social_qsa = qsa.t73303_capital_social_empresa;

                    }

                    cSocio.PercentualCapital = 100;

                    cSocio.Qualificacao = "50";
                    cSocio.EndCEP = Global.valNulo(enderecoResponsavel.cep) ? "" : enderecoResponsavel.cep;
                    cSocio.tipoacao = 1;
                    //qsa.t73303_cod_evento = "001";
                    if (!eventoConstituicao)
                    {
                        cSocio.tipoacao = 3;
                    }

                    cSocio.EndMunicipio = Global.valNulo(enderecoResponsavel.codMunicipio) ? "" : enderecoResponsavel.codMunicipio;
                    cSocio.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cSocio.EndMunicipio, 11).ToString();
                    //qsa.t73303_cod_munic_qsa = int.Parse(qsa.t73303_cod_munic_qsa).ToString();
                    //qsa.t73303_cod_pais = Global.valNulo(enderecoResponsavel.codPais) ? "" : enderecoResponsavel.codPais;
                    cSocio.EndComplemento = Global.valNulo(enderecoResponsavel.complementoLogradouro) ? "" : enderecoResponsavel.complementoLogradouro;
                    cSocio.Email = Global.valNulo(contatoResponsavel.correioEletronico) ? "" : contatoResponsavel.correioEletronico;
                    //qsa.t73303_ind_cpf_cnpj_qsa = "2"; //Sempre cpf
                    int De = 11;
                    int Ate = DadosDbe.fcpj.cpfResponsavel.Length - De;
                    cSocio.CPFCNPJ = DadosDbe.fcpj.cpfResponsavel.Substring(Ate);

                    //qsa.t73303_dat_evento = DateTime.Now;
                    //qsa.t73303_dat_inicio_mandato = DateTime.Now;
                    //qsa.t73303_ddd_fax_qsa = contatoResponsavel.dddFax;
                    cSocio.DDD = Global.valNulo(contatoResponsavel.dddTelefone1) ? "" : contatoResponsavel.dddTelefone1;
                    //qsa.t73303_distrito_qsa = enderecoResponsavel.distrito;
                    //qsa.t73303_fax_qsa = contatoResponsavel.fax;
                    cSocio.EndLogradouro = Global.valNulo(enderecoResponsavel.logradouro) ? "" : enderecoResponsavel.logradouro;
                    //qsa.t73303_nacionalidade_qsa = Socio.nacionalidadeSocioPf;
                    //qsa.t73303_nire_qsa = Socio.;
                    cSocio.Nome = DadosDbe.fcpj.nomeResponsavel;
                    cSocio.EndNumero = Global.valNulo(enderecoResponsavel.numLogradouro) ? "" : enderecoResponsavel.numLogradouro;
                    cSocio.EndBairro = Global.valNulo(enderecoResponsavel.bairro) ? "" : enderecoResponsavel.bairro;
                    cSocio.Qualificacao = "50";
                    cSocio.Telefone = Global.valNulo(contatoResponsavel.telefone1) ? "" : contatoResponsavel.telefone1;
                    cSocio.EndTipoLogradouro = Global.valNulo(enderecoResponsavel.codTipoLogradouro) ? "" : enderecoResponsavel.codTipoLogradouro;
                    cSocio.EndUF = Global.valNulo(enderecoResponsavel.uf) ? "" : enderecoResponsavel.uf;
                    cSocio.EndPais = Global.valNulo(enderecoResponsavel.codPais) ? "" : enderecoResponsavel.codPais;

                    c.Socios.Add(cSocio);

                }
                #endregion
            }

            #endregion

            #region Eventos
            if (DadosDbe.fcpj.codEvento != null)
            {
                int i = 0;
                foreach (string pCodEvento in DadosDbe.fcpj.codEvento)
                {
                    bProtocoloEvento cEvento = new bProtocoloEvento();
                    if (pCodEvento == "")
                    {
                        break;
                    }
                    if (pCodEvento.Trim() == "101" || pCodEvento.Trim() == "102")
                    {
                        eventoConstituicao = true;
                    }
                    cEvento.CodigoEvento = decimal.Parse(pCodEvento);
                    //ev.t73301_tip_evento = DadosDbe.fcpj.tipoEvento[i].ToString();
                    i += i;
                    c.ProtocoloEvento.Add(cEvento);
                }
            }
            #endregion

            #region Cnae

            if (DadosDbe.atividadeEconomica != null)
            {
                bCNAE cCnae = new bCNAE();

                if (DadosDbe.atividadeEconomica != null)
                {
                    if (DadosDbe.atividadeEconomica.codCnaeFiscal != null && DadosDbe.atividadeEconomica.codCnaeFiscal != "")
                    {
                        cCnae = new bCNAE();
                        cCnae.CodigoCNAE = Global.valNuloBranco(DadosDbe.atividadeEconomica.codCnaeFiscal);
                        cCnae.TipoAtividade = 36;
                        cCnae.Descricao = bCNAE.GetDescricao(cCnae.CodigoCNAE);
                        c.CNAEs.Add(cCnae);
                    }
                }
                if (DadosDbe.atividadeEconomica.codCnaeSecundaria != null)
                {
                    foreach (string pCodEvento in DadosDbe.atividadeEconomica.codCnaeSecundaria)
                    {
                        if (pCodEvento.Length < 5)
                        {
                            break;
                        }
                        cCnae = new bCNAE();
                        cCnae.CodigoCNAE = pCodEvento;
                        cCnae.TipoAtividade = 37;
                        cCnae.Descricao = bCNAE.GetDescricao(cCnae.CodigoCNAE);
                        c.CNAEs.Add(cCnae);

                    }
                }
            }

            #endregion

            #region Socios
            if (DadosDbe.socios != null)
            {
                foreach (WsRFB.socio Socio in DadosDbe.socios)
                {
                    if (Socio.cnpjCpfSocio != "")
                    {
                        bSocios cSocio = new bSocios();
                        bSocios cRepre = new bSocios();
                        if (Socio.endSocio != null)
                        {
                            cSocio.EndBairro = Global.valNulo(Socio.endSocio.bairro) ? "" : Socio.endSocio.bairro;
                            cSocio.EndCEP = Global.valNulo(Socio.endSocio.cep) ? "" : Socio.endSocio.cep;
                            cSocio.EndMunicipio = Global.valNulo(Socio.endSocio.codMunicipio) ? "" : Socio.endSocio.codMunicipio;
                            cSocio.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cSocio.EndMunicipio, 11).ToString();
                            cSocio.EndMunicipio = int.Parse(cSocio.EndMunicipio).ToString();

                            cSocio.EndPais = Global.valNulo(Socio.endSocio.codPais) ? "" : Socio.endSocio.codPais;
                            cSocio.EndComplemento = Global.valNulo(Socio.endSocio.complementoLogradouro) ? "" : Socio.endSocio.complementoLogradouro;
                            cSocio.EndLogradouro = Socio.endSocio.logradouro;
                            //qsa.t73303_distrito_qsa = Socio.endSocio.distrito;
                            cSocio.EndNumero = Socio.endSocio.numLogradouro;
                            cSocio.EndTipoLogradouro = Socio.endSocio.codTipoLogradouro;
                            cSocio.EndUF = Socio.endSocio.uf;
                            cSocio.EndPais = Socio.endSocio.codPais == "" ? "" : Socio.endSocio.codPais;

                        }

                        if (!Global.valNulo(Socio.cpfRepresentanteLegal) && Socio.cpfRepresentanteLegal != "")
                        {
                            cRepre.CPFCNPJ = Global.valNulo(Socio.cpfRepresentanteLegal) ? "" : Socio.cpfRepresentanteLegal;
                            cRepre.Qualificacao = Global.valNulo(Socio.codQualificacaoRepresentanteLegal) ? "" : Socio.codQualificacaoRepresentanteLegal;

                            if (Socio.endRepLegal != null)
                            {
                                cRepre.EndBairro = Global.valNulo(Socio.endRepLegal.bairro) ? "" : Socio.endRepLegal.bairro;
                                cRepre.EndCEP = Global.valNulo(Socio.endRepLegal.cep) ? "" : Socio.endRepLegal.cep;
                                cRepre.EndMunicipio = Global.valNulo(Socio.endRepLegal.codMunicipio) ? "" : Socio.endRepLegal.codMunicipio;
                                cRepre.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cRepre.EndMunicipio, 11).ToString();
                                cRepre.EndMunicipio = int.Parse(cRepre.EndMunicipio).ToString();
                                cRepre.EndComplemento = Global.valNulo(Socio.endRepLegal.complementoLogradouro) ? "" : Socio.endRepLegal.complementoLogradouro;
                                //qsa.t73303_distrito_rep_legal = Socio.endRepLegal.distrito;
                                cRepre.EndLogradouro = Socio.endRepLegal.logradouro;
                                cRepre.EndNumero = Socio.endRepLegal.numLogradouro;
                                cRepre.EndUF = Socio.endRepLegal.uf;
                                cRepre.EndTipoLogradouro = Socio.endRepLegal.codTipoLogradouro;
                                cRepre.EndPais = Socio.endRepLegal.codPais == "" ? "" : Socio.endRepLegal.codPais;
                            }

                            if (Socio.contatoRepLegal != null)
                            {
                                cRepre.Email = Global.valNulo(Socio.contatoRepLegal.correioEletronico) ? "" : Socio.contatoRepLegal.correioEletronico;
                                //cRepre. = Global.valNulo(Socio.contatoRepLegal.dddFax) ? "" : Socio.contatoRepLegal.dddFax;
                                cRepre.DDD = Global.valNulo(Socio.contatoRepLegal.dddTelefone1) ? "" : Socio.contatoRepLegal.dddTelefone1;
                                //cRepre. = Global.valNulo(Socio.contatoRepLegal.fax) ? "" : Socio.contatoRepLegal.fax;
                                cRepre.Telefone = Global.valNulo(Socio.contatoRepLegal.telefone1) ? "" : Socio.contatoRepLegal.telefone1;
                            }
                        }



                        if (!Global.valNulo(DadosDbe.fcpj.capitalSocial))
                        {
                            c.CapitalSocial = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                            //qsa.t73303_capital_social_empresa = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                        }

                        if (!Global.valNulo(Socio.capitalSocialSocio))
                        {
                            //qsa.t73303_perc_partic_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                            cSocio.CapitalIntegralizado = decimal.Parse(Socio.capitalSocialSocio) / 100;
                        }

                        /*
                            Isto aqui e porque antes o percentual vinha no campo capitalSocialSocio
                         * mas foi enviado uma ou iam colocar uma atualização nova para mudar o percentual para
                         * percentualCapitalSocialSocio, entao se esse campo vier considero este mesmo para o calculo
                         */
                        if (!Global.valNulo(Socio.percentualCapitalSocialSocio))
                        {
                            cSocio.PercentualCapital = decimal.Parse(Socio.percentualCapitalSocialSocio) / 100;
                        }

                        //
                        //Alterado em 03/11/2014 para pegar o capital social do socio caso o campo Socio.capitalSocialSocio > 0
                        //

                        //if ((!Global.valNulo(c.CapitalSocial) && c.CapitalSocial > 0)
                        //    && cSocio.PercentualCapital != 0)
                        //{
                        //    cSocio.CapitalIntegralizado = (c.CapitalSocial * cSocio.PercentualCapital) / 100;
                        //    //qsa.t73303_perc_partic_qsa = (qsa.t73303_capital_social_empresa * qsa.t73303_perc_partic_qsa) / 100;
                        //}
                        //else
                        //{
                        //    if (!Global.valNulo(Socio.capitalSocialSocio) && decimal.Parse(Socio.capitalSocialSocio) > 0)
                        //    {
                        //        cSocio.CapitalIntegralizado = decimal.Parse(Socio.capitalSocialSocio) / 100;
                        //    }
                        //}


                        cSocio.Qualificacao = Global.valNulo(Socio.codQualificacaoSocio) ? "" : Socio.codQualificacaoSocio;
                        cSocio.EventoDBE = Global.valNulo(Socio.codEvento) ? "" : Socio.codEvento;
                        cSocio.tipoacao = Global.valNulo(cSocio.EventoDBE) ? 3 : int.Parse(cSocio.EventoDBE);
                        cSocio.Nome_Mae = "";
                        cSocio.Nome_Pai = "";
                        cSocio.RG = "";
                        cSocio.OrgaoExpedidorNome = "";
                        cSocio.OrgaoExpedidorUF = "";


                        if (Socio.contatoSocio != null)
                        {
                            cSocio.Email = Global.valNulo(Socio.contatoSocio.correioEletronico) ? "" : Socio.contatoSocio.correioEletronico;
                            //qsa.t73303_ddd_fax_qsa = Socio.contatoSocio.dddFax;
                            cSocio.DDD = Socio.contatoSocio.dddTelefone1;
                            //qsa.t73303_fax_qsa = Socio.contatoSocio.fax;
                            cSocio.Telefone = Socio.contatoSocio.telefone1;

                        }


                        //qsa.t73303_ind_cpf_cnpj_qsa = Global.valNulo(Socio.indCnpjCpfSocio) ? "" : Socio.indCnpjCpfSocio;
                        int De = 14;
                        if (Socio.indCnpjCpfSocio == "2")
                        {
                            De = 11;
                        }
                        int Ate = (Socio.cnpjCpfSocio.Length - De);
                        cSocio.CPFCNPJ = Socio.cnpjCpfSocio.Substring(Ate);

                        //qsa.t73303_dat_emis_ident_rep_lega = Socio.representanteLegal.;
                        //qsa.t73303_dat_emissao_ident = Socio.d;
                        //qsa.t73303_dat_evento = dHelperQuery.convertStringDateYYYMMDD(Global.valNulo(Socio.dataEvento) ? "" : Socio.dataEvento);
                        //qsa.t73303_dat_inicio_mandato = dHelperQuery.convertStringDateYYYMMDD(Global.valNulo(Socio.dataInclusaoCorreta) ? "" : Socio.dataInclusaoCorreta);
                        //qsa.t73303_dat_termino_mandato;
                        //qsa.t73303_dt_nascimento_socio_pf = Socio.d;
                        //qsa.t73303_ident_passap_qsa = Socio.i;
                        //qsa.t73303_ident_rep_legal;

                        //qsa.t73303_matricula_rcpj = Socio.contatoSocio.ma
                        cSocio.NacionalidadeCodigo = Global.valNulo(Socio.nacionalidadeSocioPf) ? 0 : int.Parse(Socio.nacionalidadeSocioPf);
                        //qsa.t73303_nire_qsa = Socio.;
                        cSocio.Nome = Global.valNulo(Socio.socio1) ? "" : Socio.socio1;

                        //qsa.t73303_or_emis_ident_rep_legal = Socio.;
                        //qsa.t73303_orgao_emissor_ident;
                        //qsa.t73303_orig_inf_lograd;
                        //qsa.t73303_origem_endereco_rep_leg;

                        //qsa.t73303_uf_or_emissor_rep_legal = Socio.;
                        //qsa.t73303_uf_orgao_emissor_ident;
                        //qsa.t73303_uso_firma_administrador = "";

                        if (cRepre != null && (Socio.cpfRepresentanteLegal != ""))
                        {
                            cRepre.Nome = Global.valNulo(Socio.representanteLegal) ? "" : Socio.representanteLegal;
                            cSocio.Representantes.Add(cRepre);
                        }

                        c.Socios.Add(cSocio);

                    }
                }
            }
            #endregion

            #region
            if (Global.valNuloBranco(DadosDbe.fcpj.cnpjEmpresaContabil) != "" || Global.valNuloBranco(DadosDbe.fcpj.cpfContadorPF) != "")
            {
                c.Contabilista.ds_Pessoa = Global.valNuloBranco(DadosDbe.fcpj.nomeContadorPF);

                c.Contabilista.uf_CRC_Resp = Global.valNuloBranco(DadosDbe.fcpj.ufContadorPF);
                c.Contabilista.tip_CRC_Resp = Global.valNuloBranco(DadosDbe.fcpj.codTipoCRCcontadorPF);
                c.Contabilista.co_CRC_Resp = Global.valNuloBranco(DadosDbe.fcpj.numSeqContadorPF);
                c.Contabilista.cpfCnpj = Global.valNuloBranco(DadosDbe.fcpj.cpfContadorPF);
                if (DadosDbe.fcpj.codClassificEmpresaContabil != null && DadosDbe.fcpj.codClassificEmpresaContabil != "")
                {
                    c.Contabilista.tip_Class_Empresa = int.Parse(Global.valNuloBranco(DadosDbe.fcpj.codClassificEmpresaContabil));
                }
                if (DadosDbe.fcpj.codClassificCRCcontadorPF != null && DadosDbe.fcpj.codClassificCRCcontadorPF != "")
                {
                    c.Contabilista.tip_Class_Resp = int.Parse(Global.valNuloBranco(DadosDbe.fcpj.codClassificCRCcontadorPF));
                }
                //c.Contabilista.t73305_tip_contador = "1";

                if (Global.valNuloBranco(DadosDbe.fcpj.cnpjEmpresaContabil) != "")
                {
                    c.Contabilista.cpfCnpj = Global.valNuloBranco(DadosDbe.fcpj.cnpjEmpresaContabil);


                    //co.t73305_tip_contador = "2";
                    //enderecoContador = DadosDbe.fcpj.endEmpresaContabilComplementar;
                    //contatocontador = DadosDbe.fcpj.contatoContadorPf;

                    c.Contabilista.ds_Pessoa = Global.valNuloBranco(DadosDbe.fcpj.nomeEmpresaContabil);

                    c.Contabilista.uf_CRC_Empresa = Global.valNuloBranco(DadosDbe.fcpj.ufCRCempresaContabil);
                    c.Contabilista.tip_CRC_Empresa = Global.valNuloBranco(DadosDbe.fcpj.codTipoCRCempresaContabil);
                    c.Contabilista.co_CRC_Empresa = Global.valNuloBranco(DadosDbe.fcpj.seqCRCempresaContabil);



                    /*
                       Responsavel e a pessoa Fisica quando vem o cnpj do contador
                     */
                    c.Contabilista.cpf_resp = Global.valNuloBranco(DadosDbe.fcpj.cpfContadorPF);
                    c.Contabilista.uf_CRC_Resp = Global.valNuloBranco(DadosDbe.fcpj.ufContadorPF);
                    c.Contabilista.co_CRC_Resp = Global.valNuloBranco(DadosDbe.fcpj.numSeqContadorPF);
                    c.Contabilista.tip_CRC_Resp = Global.valNuloBranco(DadosDbe.fcpj.codTipoCRCcontadorPF);

                }

                DateTime dt;
                if (DadosDbe.fcpj.dataEmissaoIdContadorPF == null)
                {
                    dt = new DateTime();
                }
                else
                {
                    dt = ConvertStringDateTime(DadosDbe.fcpj.dataEmissaoIdContadorPF.ToString());
                }
                c.Contabilista.DataInscricao = dt;

                if (DadosDbe.fcpj.dataRegistroCRCcontadorPF == null)
                {
                    dt = new DateTime();
                }
                else
                {
                    dt = ConvertStringDateTime(DadosDbe.fcpj.dataRegistroCRCcontadorPF.ToString());
                }
                c.Contabilista.DataInscricao = dt;

            }
            #endregion
            return c;
        }

        public static string CalculaDigitoNire(string nire)
        {
            string result = "";
            const int digitPosition = 10;
            try
            {
                if (nire != null && nire.Length == 10)
                {
                    int sum = 0;
                    //int digit = Int32.Parse(nire[digitPosition].ToString());
                    int[] digits = { 2, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                    for (int i = 0; i < digits.Length; i++)
                    {
                        sum += Int32.Parse(nire[i].ToString()) * digits[i];
                    }

                    int remainder = sum % 11;
                    int difference = 11 - remainder;
                    int expected = difference > 9 ? difference - 10 : difference;

                    result = expected.ToString();
                }
            }
            catch { }

            return result;
        }

        public DateTime ConvertStringDateTime(string _data)
        {
            if (_data.Trim() != "")
            {
                int dia = int.Parse(_data.Substring(6, 2));
                int mes = int.Parse(_data.Substring(4, 2));
                int ano = int.Parse(_data.Substring(0, 4));

                return new DateTime(ano, mes, dia);
            }
            return new DateTime(1, 1, 1);
        }

        #endregion

        #region Processa e Mostrar Dados ws11 
        public bRequerimento ProcessaDadosWs11ToReq(WsRFB.retornoWS11Redesim pReponse)
        {

            bRequerimento c = new bRequerimento();
            bool eventoConstituicao = false;


            #region Empresa
            WsRFB.dadosCNPJ DadosDbe = pReponse.dadosCNPJ[0];
            //pj.t73302_caixa_postal = Global.valNuloBranco(DadosDbe.caixaPostal);
            if (!Global.valNulo(DadosDbe.capitalSocial))
            {
                c.CapitalSocial = decimal.Parse(DadosDbe.capitalSocial) / 100;
            }


            if (DadosDbe != null)
            {
                if (DadosDbe.endereco != null)
                {
                    c.SedeBairro = Global.valNuloBranco(DadosDbe.endereco.bairro);
                    c.SedeCEP = Global.valNuloBranco(DadosDbe.endereco.cep);
                    c.SedeMunicipio = Global.valNuloBranco(DadosDbe.endereco.codMunicipio);
                    if (c.SedeMunicipio != "")
                    {
                        c.SedeMunicipio += psc.Framework.General.CalculateVerificationDigit(c.SedeMunicipio, 11).ToString();
                        c.SedeMunicipio = int.Parse(c.SedeMunicipio).ToString();
                    }
                    //pj.t73302_cod_pais = Global.valNuloBranco(DadosDbe.endereco.codPais);
                    c.SedeComplemento = Global.valNuloBranco(DadosDbe.endereco.complementoLogradouro);
                    //c.seded = Global.valNuloBranco(DadosDbe.endereco.distrito);
                    c.SedeLogradouro = Global.valNuloBranco(DadosDbe.endereco.logradouro);
                    c.SedeNumero = Global.valNuloBranco(DadosDbe.endereco.numLogradouro);
                    //c.Sede = Global.valNuloBranco(DadosDbe.endereco.referencia);
                    c.SedeTipoLogradouro = Global.valNuloBranco(DadosDbe.endereco.codTipoLogradouro);
                    c.SedeDsTipoLogradouro = c.SedeTipoLogradouro;
                    //pj.t73302_cidade_exterior = Global.valNuloBranco(DadosDbe.endereco.cidadeExterior);
                    c.SedeUF = Global.valNuloBranco(DadosDbe.endereco.uf);
                    //pj.t73302_cod_tipo_unidade = Global.valNuloBranco(DadosDbe.atividadeEconomica.codTipoUnidade);
                }
            }

            //c.Sedeca = Global.valNuloBranco(DadosDbe.cepCaixaPostal);
            if (DadosDbe.cnpj.Trim().Length > 12)
            {
                c.nrEmpresaCNPJ = Global.valNuloBranco(DadosDbe.cnpj);
            }

            c.nrMatricula = Global.valNuloBranco(DadosDbe.numeroOrgaoRegistro);

            if (Global.valNuloBranco(DadosDbe.numeroOrgaoRegistro) != "")
            {
                c.nrMatricula = Global.valNuloBranco(decimal.Parse(DadosDbe.numeroOrgaoRegistro));
            }
            //pj.t73302_tip_org_registro = Global.valNuloBranco(DadosDbe.codTipoOrgaoRegistro);
            c.Porte = Global.valNuloBranco(DadosDbe.porte);

            int pNatureza = int.Parse(Global.valNuloBranco(DadosDbe.naturezaJuridica));
            string nomeEmpresa = Global.valNuloBranco(DadosDbe.nomeEmpresarial);

            if (DadosDbe.indMatrizFilial == "2")
            {

                WsRFB.ServiceReginRFB ws = new WsRFB.ServiceReginRFB();
                WsRFB.dadosCNPJ dadosMartriz = new WsRFB.dadosCNPJ();
                WsRFB.Retorno resp = new WsRFB.Retorno();
                
                ws.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                resp = ws.ServiceWs11(DadosDbe.cnpjMatriz);

                if (resp.status == "NOK")
                {
                    throw new Exception(resp.descricao);
                }
                dadosMartriz = resp.oCNPJResponse.dadosCNPJ[0];

                pNatureza = int.Parse(Global.valNuloBranco(dadosMartriz.naturezaJuridica));
                nomeEmpresa = Global.valNuloBranco(dadosMartriz.nomeEmpresarial);
            }

            c.NaturezaJuridicaCodigo = pNatureza;
            c.SedeNome = nomeEmpresa;

            DataTable tdNt = dHelperQuery.getNaturezaJuridicaRequerimento(decimal.Parse(c.NaturezaJuridicaCodigo.ToString()));

            if (tdNt.Rows.Count > 0)
            {
                c.NaturezaJuridicaDescricao = tdNt.Rows[0]["t009_ds_natureza_juridica"].ToString();
            }
            else
            {
                c.NaturezaJuridicaDescricao = "Natureza não encontrada na tabela";
            }

            


            //PORTE DA EMPRESA (DE-PARA)
            //DBE ==>   01 – MICRO-EMPRESA, 03 – EMPRESA PEQUENO PORTE, 05 – DEMAIS
            //REQ ==>   1119 - ME                   1118 - EPP          1120 - NORMAL  
            string porte = Global.valNuloBranco(DadosDbe.porte);
            if (porte != "")
            {
                switch (porte)
                {
                    case "01":
                        c.Enquadramento = 1119;
                        //c._EnquadramentoDESCRICAO = "MICRO EMPRESA";
                        break;
                    case "03":
                        c.Enquadramento = 1118;
                        //c.EnquadramentoDESCRICAO = "PEQUENO PORTE";
                        break;
                    case "05":
                        c.Enquadramento = 1120;
                        break;
                    default:
                        c.Enquadramento = 1120;
                        //c.EnquadramentoDESCRICAO = "DEMAIS";
                        break;
                }

            }


            if (DadosDbe.objetoSocial != null)
            {
                c.ObjetoSocial = Global.valNuloBranco(DadosDbe.objetoSocial);
                //c.CNAEs = Global.valNuloBranco(DadosDbe.atividadeEconomica.codCnaeFiscal);
            }

            //if (DadosDbe.contato != null)
            //{
            //    c.SedeEmail = Global.valNuloBranco(DadosDbe.contato.correioEletronico);
            //    c.SedeTelefone = Global.valNuloBranco(DadosDbe.contato.telefone1);
            //    c.SedeDDD = Global.valNuloBranco(DadosDbe.contato.dddTelefone1);
            //}

            if (DadosDbe.naturezaJuridica == "2135")
            {
                bSocios cSocio = new bSocios();

                #region Preenche Socio caso seja naturea Empresaio
                if (DadosDbe.cpfRepresentante != "")
                {
                    WsRFB.endereco enderecoResponsavel = new WsRFB.endereco();
                    WsRFB.contato contatoResponsavel = new WsRFB.contato();

                    /*
                        Comença com o dado da emporesa mesmo, caso venha o endereço do responsavel pego de la
                     * comentado para nao levar o endereço da empresa para o socio, solicitado por xico 28/10/2014
                     * alegando que os estados falaram que a pessoa quasi sempre chega com o memso endereço, por isso
                     * sera forçado a digitar ele novamente se for o caso.
                     */
                    //enderecoResponsavel = DadosDbe.endereco;
                    //contatoResponsavel = DadosDbe.contato;

                    //if (DadosDbe.endResponsavel != null)
                    //{
                    //    if (!Global.valNulo(DadosDbe.endResponsavel.bairro) && !Global.valNulo(DadosDbe.endResponsavel.cep))
                    //    {
                    //        enderecoResponsavel = DadosDbe.endResponsavel;
                    //        contatoResponsavel = DadosDbe.contatoResponsavel;
                    //    }
                    //}

                    cSocio.EndBairro = enderecoResponsavel.bairro;
                    if (!Global.valNulo(DadosDbe.capitalSocial))
                    {

                    }

                    cSocio.PercentualCapital = 100;

                    cSocio.Qualificacao = "50";
                    cSocio.EndCEP = Global.valNulo(enderecoResponsavel.cep) ? "" : enderecoResponsavel.cep;
                    cSocio.tipoacao = 1;
                    //qsa.t73303_cod_evento = "001";
                    if (!eventoConstituicao)
                    {
                        cSocio.tipoacao = 3;
                    }

                    cSocio.EndMunicipio = Global.valNulo(enderecoResponsavel.codMunicipio) ? "" : enderecoResponsavel.codMunicipio;
                    cSocio.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cSocio.EndMunicipio, 11).ToString();
                    cSocio.EndComplemento = Global.valNulo(enderecoResponsavel.complementoLogradouro) ? "" : enderecoResponsavel.complementoLogradouro;
                    cSocio.Email = Global.valNulo(contatoResponsavel.correioEletronico) ? "" : contatoResponsavel.correioEletronico;
                    //int De = 11;
                    //int Ate = DadosDbe.cpfResponsavel.Length - De;
                    cSocio.CPFCNPJ = DadosDbe.cpfRepresentante;

                    //cSocio.DDD = Global.valNulo(contatoResponsavel.dddTelefone1) ? "" : contatoResponsavel.dddTelefone1;
                    //cSocio.EndLogradouro = Global.valNulo(enderecoResponsavel.logradouro) ? "" : enderecoResponsavel.logradouro;
                    cSocio.Nome = DadosDbe.nomeRepresentante;
                    //cSocio.EndNumero = Global.valNulo(enderecoResponsavel.numLogradouro) ? "" : enderecoResponsavel.numLogradouro;
                    //cSocio.EndBairro = Global.valNulo(enderecoResponsavel.bairro) ? "" : enderecoResponsavel.bairro;
                    cSocio.Qualificacao = "50";
                    //cSocio.Telefone = Global.valNulo(contatoResponsavel.telefone1) ? "" : contatoResponsavel.telefone1;
                    //cSocio.EndTipoLogradouro = Global.valNulo(enderecoResponsavel.codTipoLogradouro) ? "" : enderecoResponsavel.codTipoLogradouro;
                    //cSocio.EndUF = Global.valNulo(enderecoResponsavel.uf) ? "" : enderecoResponsavel.uf;
                    //cSocio.EndPais = Global.valNulo(enderecoResponsavel.codPais) ? "" : enderecoResponsavel.codPais;

                    c.Socios.Add(cSocio);

                }
                #endregion
            }

            #endregion

            

            #region Cnae

            
                bCNAE cCnae = new bCNAE();

                if (DadosDbe.cnaePrincipal != null)
                {
                    if (DadosDbe.cnaePrincipal != null && DadosDbe.cnaePrincipal != "")
                    {
                        cCnae = new bCNAE();
                        cCnae.CodigoCNAE = Global.valNuloBranco(DadosDbe.cnaePrincipal);
                        cCnae.TipoAtividade = 36;
                        cCnae.Descricao = bCNAE.GetDescricao(cCnae.CodigoCNAE);
                        c.CNAEs.Add(cCnae);
                    }
                }
                if (DadosDbe.cnaeSecundaria != null)
                {
                    foreach (string pCodEvento in DadosDbe.cnaeSecundaria)
                    {
                        if (pCodEvento == "0000000")
                        {
                            break;
                        }
                        cCnae = new bCNAE();
                        cCnae.CodigoCNAE = pCodEvento;
                        cCnae.TipoAtividade = 37;
                        cCnae.Descricao = bCNAE.GetDescricao(cCnae.CodigoCNAE);
                        c.CNAEs.Add(cCnae);

                    }
                }
           

            #endregion

            #region Socios
            if (DadosDbe.dadosSocio != null)
            {
                foreach (WsRFB.dadosSocio Socio in DadosDbe.dadosSocio)
                {
                    if (Socio.cpfCnpj != "")
                    {
                        bSocios cSocio = new bSocios();
                        bSocios cRepre = new bSocios();
                        //if (Socio.endSocio != null)
                        //{
                        //    cSocio.EndBairro = Global.valNulo(Socio.endSocio.bairro) ? "" : Socio.endSocio.bairro;
                        //    cSocio.EndCEP = Global.valNulo(Socio.endSocio.cep) ? "" : Socio.endSocio.cep;
                        //    cSocio.EndMunicipio = Global.valNulo(Socio.endSocio.codMunicipio) ? "" : Socio.endSocio.codMunicipio;
                        //    cSocio.EndMunicipio += psc.Framework.General.CalculateVerificationDigit(cSocio.EndMunicipio, 11).ToString();
                        //    cSocio.EndMunicipio = int.Parse(cSocio.EndMunicipio).ToString();
                        //    cSocio.EndPais = Global.valNulo(Socio.endSocio.codPais) ? "" : Socio.endSocio.codPais;
                        //    cSocio.EndComplemento = Global.valNulo(Socio.endSocio.complementoLogradouro) ? "" : Socio.endSocio.complementoLogradouro;
                        //    cSocio.EndLogradouro = Socio.endSocio.logradouro;
                        //    cSocio.EndNumero = Socio.endSocio.numLogradouro;
                        //    cSocio.EndTipoLogradouro = Socio.endSocio.codTipoLogradouro;
                        //    cSocio.EndUF = Socio.endSocio.uf;
                        //    cSocio.EndPais = Socio.endSocio.codPais == "" ? "" : Socio.endSocio.codPais;
                        //}

                        if (!Global.valNulo(Socio.cpfCnpj) && Socio.cpfCnpj != "")
                        {
                            cRepre.CPFCNPJ = Global.valNulo(Socio.cpfCnpj) ? "" : Socio.cpfCnpj;
                            cRepre.Qualificacao = Global.valNulo(Socio.qualificacao) ? "" : Socio.qualificacao;

                        }



                        if (!Global.valNulo(DadosDbe.capitalSocial))
                        {
                            c.CapitalSocial = decimal.Parse(DadosDbe.capitalSocial) / 100;
                            //qsa.t73303_capital_social_empresa = decimal.Parse(DadosDbe.capitalSocial) / 100;
                        }

                        if (!Global.valNulo(Socio.valorPartCapitalSocialString))
                        {
                            //qsa.t73303_perc_partic_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                            cSocio.CapitalIntegralizado = decimal.Parse(Socio.valorPartCapitalSocialString) / 100;
                        }

                        cSocio.Qualificacao = Global.valNulo(Socio.qualificacao) ? "" : Socio.qualificacao;
                        //cSocio.EventoDBE = Global.valNulo(Socio.codEvento) ? "" : Socio.codEvento;
                        cSocio.tipoacao = 3;
                        cSocio.Nome_Mae = "";
                        cSocio.Nome_Pai = "";
                        cSocio.RG = "";
                        cSocio.OrgaoExpedidorNome = "";
                        cSocio.OrgaoExpedidorUF = "";

                        //qsa.t73303_ind_cpf_cnpj_qsa = Global.valNulo(Socio.indCnpjCpfSocio) ? "" : Socio.indCnpjCpfSocio;
                        cSocio.CPFCNPJ = Socio.cpfCnpj;

                        cSocio.Nome = Global.valNulo(Socio.nome) ? "" : Socio.nome;


                        if (cRepre != null && (Socio.cpfRepresentanteLegal != ""))
                        {
                            cRepre.CPFCNPJ = Socio.cpfRepresentanteLegal;
                            cRepre.Nome = Global.valNulo(Socio.nomeRepresentanteLegal) ? "" : Socio.nomeRepresentanteLegal;
                            cRepre.Qualificacao = Socio.qualificacao;
                            cSocio.Representantes.Add(cRepre);
                        }

                        c.Socios.Add(cSocio);

                    }
                }
            }
            #endregion

            
            return c;
        }
        #endregion

        #region CarregaRucws11

        public string GravaWsRFB11Ruc(List<string> pEventos, string pProtocoloJunta, string pCNPJ, string nire, string pNroViabilidade, string NroRequerimento, string NroServentia, string pCNPJOrgaoRegistro, WsRFB.redesim pDados35, string pNroDBE, string pUsuarioSistema)
        {
            try
            {
                WsRFB.ServiceReginRFB ws = new WsRFB.ServiceReginRFB();
                WsRFB.dadosCNPJ dados = new WsRFB.dadosCNPJ();
                WsRFB.Retorno resp = new WsRFB.Retorno();
                ws.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                resp = ws.ServiceWs11(pCNPJ);

                dados = resp.oCNPJResponse.dadosCNPJ[0];


                //Isto aqui e porque caso seja de outro estado (isso e porque agora podem ser deferidos DBE de filiais de outro estado
                //Nao grava nas tabelas do REGIN
                if (validaNulo(dados.endereco.uf.ToUpper()) != pUFDefault)
                {
                    return "";
                }

                DateTime DtHoje = dHelperORACLE.SysdateOracle();

                //List<string> pEventos = new List<string>();

                using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    string _vTipRegistro = GetTipoEmpresaPorNaturezaJuridica(dados.naturezaJuridica); //Verificar

                    decimal CodMuniEmpresa = decimal.Parse(CalDvMunicipio(dados.endereco.codMunicipio));

                    #region PSC_PROTOCOLO

                    int pProTipOperacao = 2; //Alteração
                    for (int i = 0; i < pEventos.Count; i++)
                    {
                        if (pEventos[i].ToString() == "101" || pEventos[i].ToString() == "102")
                        {
                            pProTipOperacao = 1; //Constituição
                        }

                    }



                    using (PSC_PROTOCOLO p = new PSC_PROTOCOLO())
                    {
                        p.MainConnectionProvider = cp;
                        p.Pro_protocolo = pProtocoloJunta;
                        p.Pro_status = 1;
                        p.Pro_fec_inc = DtHoje;
                        p.Pro_tmu_tuf_uf = dados.endereco.uf;
                        p.Pro_tmu_cod_mun = int.Parse(CodMuniEmpresa.ToString());
                        p.Pro_tip_operacao = pProTipOperacao; //Verificar
                        p.Pro_env_sef = 2;
                        p.Pro_flag_vigilancia = 2;
                        p.Pro_fec_atualizacao = DtHoje;
                        p.Pro_tge_tgacao = 700;
                        p.Pro_tge_vgacao = Int32.Parse("2");
                        p.Pro_cnpj_org_reg = pCNPJOrgaoRegistro;
                        p.PRO_NR_REQUERIMENTO = NroRequerimento;
                        p.PRO_VPV_COD_PROTOCOLO = pNroViabilidade;
                        p.PRO_NR_DBE = pNroDBE;
                        p.pro_origem_dado = 3;
                        p.Update();

                    }
                    #endregion

                    #region PSC_IDENT_PROTOCOLO
                    StringBuilder sqlIdent = new StringBuilder();
                    sqlIdent.AppendLine(@" Insert Into PSC_IDENT_PROTOCOLO 
                                    (PIP_PRO_PROTOCOLO, 
                                     PIP_CNPJ, 
                                     PIP_NIRE, 
                                     PIP_NOME_RAZAO_SOCIAL, 
                                     PIP_CPF_RESPONSAVEL ) 
                                    Values ( ");
                    sqlIdent.Append("'" + pProtocoloJunta + "'");
                    sqlIdent.Append(", '" + dados.cnpj + "'");
                    sqlIdent.Append(", '" + nire + "'");
                    sqlIdent.Append(", '" + dados.nomeEmpresarial.Replace("'", " ") + "'");
                    sqlIdent.Append(", '" + pUsuarioSistema + "'");
                    sqlIdent.Append(" )");

                    using (dHelperORACLE c = new dHelperORACLE())
                    {
                        c.MainConnectionProvider = cp;
                        c.ExecuteNonQuery(sqlIdent);
                    }

                    #endregion

                    #region MAC_LOG_CARGA_JUNTA_HOMOLOG
                    using (MAC_LOG_CARGA_JUNTA_HOMOLOG m = new MAC_LOG_CARGA_JUNTA_HOMOLOG())
                    {
                        m.MainConnectionProvider = cp;

                        m.MLC_PROTOCOLO = pProtocoloJunta;
                        m.MLC_CPF_HOMOLOGADOR = pUsuarioSistema;
                        m.MLC_DTA_HOMOLOGACAO = DtHoje;
                        m.MLC_DATA_CARREGA_WS11 = DateTime.MinValue;
                        m.Update();

                    }
                    #endregion

                    #region RUC_GENERAL
                    using (Ruc_General rg = new Ruc_General())
                    {
                        rg.MainConnectionProvider = cp;
                        rg.rge_pra_protocolo = pProtocoloJunta;
                        rg.rge_ruc = "";
                        rg.rge_tge_ttip_reg = 257;
                        rg.rge_tge_vtip_reg = Int32.Parse(_vTipRegistro);
                        rg.rge_tge_ttip_ctrib = 153;
                        rg.rge_tge_vtip_ctrib = 9999;
                        rg.rge_tge_ttip_pers = 233;
                        rg.rge_tge_vtip_pers = 1;
                        rg.rge_cgc_cpf = dados.cnpj;
                        rg.rge_tge_ttamanho = 21;
                        rg.rge_tge_vtamanho = 3;
                        rg.rge_tge_vtamanho = 3;
                        if (dados.porte != null)
                        {
                            if (dados.porte == "01") //ME
                            {
                                rg.rge_tge_vtamanho = 1;
                            }
                            if (dados.porte == "03")//EPP
                            {
                                rg.rge_tge_vtamanho = 2;
                            }
                        }
                        rg.rge_nomb = dados.nomeEmpresarial;
                        rg.rge_codg_mun = CodMuniEmpresa;
                        //rg.rge_tae_cod_actvd = dados.cnaePrincipal;
                        rg.rge_tuf_cod_uf = dados.endereco.uf;

                        rg.Update();

                    }
                    #endregion

                    #region RUC_ESTAB
                    using (Ruc_Estab rb = new Ruc_Estab())
                    {
                        rb.MainConnectionProvider = cp;
                        rb.res_rge_pra_protocolo = pProtocoloJunta;
                        rb.res_ide_estab = 0;
                        rb.res_tip_estab = 1;
                        rb.res_tge_ttip_reg = 155;
                        rb.res_tge_vtip_reg = 9999;
                        rb.res_tge_tcond_uso = 152;
                        rb.res_tge_vcond_uso = 9999;
                        rb.res_sigl = "";
                        rb.res_area = 0;
                        rb.res_tge_tuni_medid = 156;
                        rb.res_tge_vuni_medid = 9999;
                        //rb.res_fec_inic;
                        //rb.res_fec_fin;
                        rb.res_nume = validaNulo(dados.endereco.numLogradouro);
                        //rb.res_caja_po = ""; ;
                        //rb.res_zona_caja_po = "";
                        rb.res_tus_cod_usr = "REGIN";
                        rb.res_nom_estab = validaNulo(dados.nomeEmpresarial);
                        //rb.res_num_reg_prop;
                        //rb.res_quad_lote;
                        rb.res_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                        rb.res_refer = validaNulo(dados.endereco.referencia);
                        rb.res_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                        rb.res_direccion = validaNulo(dados.endereco.logradouro);
                        rb.res_urbanizacion = validaNulo(dados.endereco.bairro);
                        rb.res_tes_cod_estado = validaNulo(dados.endereco.uf);
                        rb.res_zona_postal = validaNulo(dados.endereco.cep);
                        rb.res_tmu_cod_mun = CodMuniEmpresa;
                        rb.res_nire_sede = "";
                        rb.res_cnpj_sede = validaNulo(dados.cnpjMatriz);

                        rb.Update();
                    }
                    #endregion

                    #region RUC_COMP

                    using (Ruc_Comp rc = new Ruc_Comp())
                    {
                        rc.MainConnectionProvider = cp;
                        //rc.rco_fec_const_nasc = null;
                        rc.rco_num_reg_merc = nire;
                        //rc.rco_fec_reg_merc = null;
                        rc.rco_tge_ttip_doc = 151;
                        rc.rco_tge_vtip_doc = 9999;
                        //rc.rco_num_doc_ident
                        //rc.rco_fec_emi_doc_id
                        rc.rco_tnc_cod_natur = decimal.Parse(dados.naturezaJuridica);
                        rc.rco_domic_pais = 1;
                        rc.rco_fec_incorp = DtHoje;
                        rc.rco_val_cap_soc = decimal.Parse(dados.capitalSocial) / 100;
                        //rc.rco_fec_rg_cap_soc
                        //rc.rco_sexo 
                        rc.rco_nume = validaNulo(dados.endereco.numLogradouro);
                        //rc.rco_caja_po
                        //rc.rco_zona_caja_po
                        rc.rco_tge_tpais = 22;
                        rc.rco_tge_vpais = 105;
                        //rc.rco_ruc_ext_uf
                        rc.rco_tus_cod_usr = "REGIN";
                        //rc.rco_emis_doc_ident
                        //rc.rco_quad_lote
                        rc.rco_ident_comp = validaNulo(dados.endereco.complementoLogradouro);
                        rc.rco_refer = validaNulo(dados.endereco.referencia);
                        rc.rco_lic_mun = "";
                        rc.rco_ttl_tip_logradoro = validaNulo(dados.endereco.codTipoLogradouro);
                        rc.rco_direccion = validaNulo(dados.endereco.logradouro);
                        rc.rco_urbanizacion = validaNulo(dados.endereco.bairro);
                        rc.rco_tes_cod_estado = validaNulo(dados.endereco.uf);
                        rc.rco_zona_postal = validaNulo(dados.endereco.cep);
                        //rc.rco_tge_tcier_bal
                        //rc.rco_tge_vcier_bal
                        //rc.rco_tge_treg_trib
                        //rc.rco_tge_vreg_trib
                        rc.rco_tmu_cod_mun = CodMuniEmpresa;
                        rc.rco_rge_pra_protocolo = pProtocoloJunta;
                        //rc.rco_num_reg_merc_sede
                        rc.Update();
                    }

                    #endregion

                    #region RUC_ACTV_ECON

                    Ruc_Actv_Econ cav = new Ruc_Actv_Econ();
                    cav.MainConnectionProvider = cp;
                    cav.rae_rge_pra_protocolo = pProtocoloJunta;
                    cav.rae_tae_cod_actvd = dados.cnaePrincipal;
                    cav.rae_calif_actv = "1";
                    cav.rae_porcent = 100;
                    cav.rae_tus_cod_usr = "REGIN";
                    cav.rae_fec_actl = DtHoje;
                    cav.Update();

                    if (dados.cnaeSecundaria != null)
                    {
                        foreach (string pCNAE in dados.cnaeSecundaria)
                        {
                            if (pCNAE != null && pCNAE != "0000000")
                            {
                                using (cav = new Ruc_Actv_Econ())
                                {
                                    cav.MainConnectionProvider = cp;

                                    cav.rae_rge_pra_protocolo = pProtocoloJunta;
                                    cav.rae_tae_cod_actvd = pCNAE;
                                    cav.rae_calif_actv = "2";
                                    cav.rae_porcent = 0;
                                    cav.rae_tus_cod_usr = "REGIN";
                                    cav.rae_fec_actl = DtHoje;
                                    cav.Update();
                                }
                            }
                        }
                    }
                    #endregion

                    #region RUC_GEN_PROTOCOLO
                    using (Ruc_Gen_Protocolo gc = new Ruc_Gen_Protocolo())
                    {
                        gc.MainConnectionProvider = cp;
                        gc.rgp_rge_pra_protocolo = pProtocoloJunta;
                        gc.rgp_tge_tip_tab = 902;
                        gc.rgp_tge_cod_tip_tab = 1;
                        gc.rgp_valor = validaNulo(dados.objetoSocial);
                        gc.rgp_tus_cod_usr = "REGIN";
                        gc.rgp_fec_actl = DtHoje;
                        if (gc.rgp_valor != "")
                        {
                            gc.Update();
                        }
                    }

                    #endregion

                    #region PSC_PROT_EVENTO_RFB
                    StringBuilder sqlEvento = new StringBuilder();
                    for (int i = 0; i < pEventos.Count; i++)
                    {
                        sqlEvento = new StringBuilder();
                        sqlEvento.AppendLine("Insert Into PSC_PROT_EVENTO_RFB ");
                        sqlEvento.AppendLine(" (PEV_PRO_PROTOCOLO, PEV_COD_EVENTO ) ");
                        sqlEvento.AppendLine(" Values ( ");
                        sqlEvento.AppendLine("'" + pProtocoloJunta + "'");
                        sqlEvento.AppendLine(" ," + pEventos[i].ToString());
                        sqlEvento.AppendLine(" ) ");
                        using (dHelperORACLE cpe = new dHelperORACLE())
                        {
                            cpe.MainConnectionProvider = cp;
                            cpe.ExecuteNonQuery(sqlEvento);
                        }
                    }


                    #endregion

                    //Isto e para nao carregar socio pelo deferidor web, ja que vou carregar 
                    //no programa do completa QSA do wsrfbRegin que esta mais completo e melhor testado.
                    //Feito pelo Raul 21/12/2017
                    if (2 == 1)
                    {
                        #region RUC_RELAT_PROF e RUC_PROF

                        foreach (WsRFB.dadosSocio socio in dados.dadosSocio)
                        {
                            WsRFB.dadosCPF dados09 = new WsRFB.dadosCPF();

                            if (socio.cpfCnpj.Length < 12)
                            {
                                WsRFB.ServiceReginRFB ws09 = new WsRFB.ServiceReginRFB();
                                WsRFB.Retorno resp09 = new WsRFB.Retorno();
                                ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                                resp09 = ws09.ServiceWs09(socio.cpfCnpj);

                                if (resp09.status == "OK")
                                {
                                    dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                                }
                            }

                            using (Ruc_Relat_Prof rp = new Ruc_Relat_Prof())
                            {
                                rp.MainConnectionProvider = cp;
                                rp.rrp_rge_pra_protocolo = pProtocoloJunta;
                                rp.rrp_cgc_cpf_secd = socio.cpfCnpj;
                                rp.rrp_tge_ttip_relac = 24;
                                rp.rrp_tge_vtip_relac = 2;
                                rp.rrp_fec_inic_part = ConvertStringDateTime(socio.dataInclusao);
                                rp.rrp_tge_tcod_qual = 23;
                                rp.rrp_tge_vcod_qual = decimal.Parse(socio.qualificacao);
                                rp.rrp_desc_doc = "";
                                rp.rrp_tus_cod_usr = "JUCESC";
                                rp.rrp_cnpj_vacio = 0;
                                rp.Update();
                            }

                            using (Ruc_Prof rp = new Ruc_Prof())
                            {
                                rp.MainConnectionProvider = cp;
                                rp.rpr_rge_pra_protocolo = pProtocoloJunta;
                                rp.rpr_tge_tpais = 22;
                                rp.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                                rp.rpr_cgc_cpf_secd = socio.cpfCnpj;
                                rp.rpr_tge_ttip_pers = 233;
                                rp.rpr_tge_vtip_pers = socio.cpfCnpj.Length < 12 ? 1 : 2;
                                rp.rpr_nomb = socio.nome;

                                if (dados09 != null && dados09.numCPF != "")
                                {
                                    rp.rpr_nome_mae = dados09.nomeMae;

                                    rp.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                                    rp.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                                    rp.rpr_nume = validaNulo(dados09.endereco.numLogradouro);
                                    rp.rpr_ident_comp = validaNulo(dados09.endereco.complementoLogradouro);
                                    rp.rpr_refer = validaNulo(dados09.endereco.referencia);
                                    rp.rpr_direccion = validaNulo(dados09.endereco.logradouro);
                                    string pRPR_TTL_TIP_LOGRADORO = "";
                                    try
                                    {

                                        if (dados09.endereco.codTipoLogradouro != null || dados09.endereco.codTipoLogradouro == "")
                                        {
                                            pRPR_TTL_TIP_LOGRADORO = dados09.endereco.codTipoLogradouro;
                                        }
                                        else
                                        {
                                            string[] tipos = dados09.endereco.logradouro.Split(' ');
                                            pRPR_TTL_TIP_LOGRADORO = tipos[0].Trim();
                                            rp.rpr_direccion = rp.rpr_direccion.Substring(pRPR_TTL_TIP_LOGRADORO.Length).Trim();

                                        }
                                    }
                                    catch
                                    {
                                        throw new Exception("Erro ao tentar buscar o Tipo de logradouro RFB ");
                                    }
                                    rp.rpr_ttl_tip_logradoro = validaNulo(pRPR_TTL_TIP_LOGRADORO);

                                    rp.rpr_urbanizacion = validaNulo(dados09.endereco.bairro);
                                    rp.rpr_tes_cod_estado = validaNulo(dados09.endereco.uf);
                                    rp.rpr_zona_postal = validaNulo(dados09.endereco.cep);

                                    rp.rpr_tmu_cod_mun = Decimal.Parse(CalDvMunicipio(dados09.endereco.codMunicipio));

                                }
                                rp.Update();
                            }

                        }
                        #endregion
                    }



                    #region TAB_INFORM_EXTRA_JUNTA
                    using (Tab_Inform_Extra_juntacs ex = new Tab_Inform_Extra_juntacs())
                    {
                        ex.MainConnectionProvider = cp;
                        ex.tie_protocolo = pProtocoloJunta;
                        ex.tie_cpf_cnpj = dados.cnpj;
                        ex.tie_tipo_relacao = 4;
                        ex.tie_ddd_fone1 = "";
                        ex.tie_fone1 = "";
                        ex.tie_ddd_fone2 = "";
                        ex.tie_fone2 = "";
                        ex.tie_ddd_fax = "";
                        ex.tie_fax = "";
                        ex.tie_tipo_unidade = "";
                        string TipoOrgaoRegistro = "1";//Junta Comercial
                        if (NroServentia != "")
                        {
                            TipoOrgaoRegistro = "2";//Cartorio
                        }

                        ex.tie_orgao_registro = TipoOrgaoRegistro;
                        ex.tie_cnpj_registro = pCNPJOrgaoRegistro;
                        ex.tie_forma_atuacao = "";
                        ex.tie_email = "";
                        ex.tie_distrito = "";
                        ex.tie_in_centro_distribuicao = 0;
                        ex.tie_in_franqueado = 0;
                        ex.tie_cnpj_franqueador = "";
                        ex.tie_nr_ato_legal = "";
                        ex.tie_tipo_propriedade = 0;

                        ex.UpdateDeferidor();
                    }

                    #endregion


                    cp.CommitTransaction();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string validaNulo(object pValue)
        {
            if (pValue == null)
            {
                return "";
            }
            return pValue.ToString();
        }

        public static string GetTipoEmpresaPorNaturezaJuridica(String pNatureza)
        {


            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append(@"SELECT TNJ_CO_GRUPO
                         FROM TAB_NATUREZA_JURIDCA
                         WHERE TNJ_CO_NATUREZA_JURIDICA = :pNatureza");


                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandText = sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                DataTable toReturn = new DataTable("Pessoa_Juridica");
                cmdToExecute.Connection = _conn;

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    cmdToExecute.Parameters.Add(new OracleParameter("pNatureza", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pNatureza));

                    cmdToExecute.Connection.Open();
                    // Execute query.

                    adapter.Fill(toReturn);

                    if (toReturn.Rows.Count > 0)
                    {
                        return toReturn.Rows[0]["TNJ_CO_GRUPO"].ToString();
                    }
                    else
                    {
                        return "11";
                    }

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                }
            }
        }


        //public DateTime ConvertStringDateTime(string _data)
        //{
        //    if (_data.Trim() != "")
        //    {
        //        int dia = int.Parse(_data.Substring(6, 2));
        //        int mes = int.Parse(_data.Substring(4, 2));
        //        int ano = int.Parse(_data.Substring(0, 4));

        //        return new DateTime(ano, mes, dia);
        //    }
        //    return new DateTime(1, 1, 1);
        //}

        private string CalDvMunicipio(string CodMUnicipio)
        {
            if (CodMUnicipio != "")
            {
                string codMuni = psc.Framework.General.CalculateVerificationDigit(CodMUnicipio, 11).ToString();

                return CodMUnicipio + codMuni;
            }

            return "";

        }

        #endregion

    }
}


