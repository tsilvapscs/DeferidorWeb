using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RCPJ.BLL;
using psc.Framework;
using psc.Application.Siarco;
using psc.ApplicationBlocks.SessionState;
using RCPJ.DAL.Helper;
using System.Text;
using System.Xml;
using System.IO;

namespace RCPJ.Application
{

    [Serializable]
    public partial class EntradaDeferidor : PageBase
    {
        #region Variables Declarations
        [SessionPageState("psc.RCPJ.Requerimento")]
        protected bRequerimento req;
        [TransientPageState]
        protected bool ViabilidadeObrigatoriaConfiguracao = false;
        [TransientPageState]
        protected bool ViabilidadeObrigatoriaPeloDBE = false;
        [TransientPageState]
        protected bool RequerimentoObrigatoriaPeloDBE = false;
        [TransientPageState]
        protected bool ViabilidadeRespostaObrigaViabilidade = true;
        [TransientPageState]
        protected DataSet dtResultXml = new DataSet();
        [TransientPageState]
        protected string TipoValidaDeferidor = "BASICA";
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.txtProtocoloOR.TextChanged += new EventHandler(txtProtocoloOR_TextChanged);
            this.btnAvancar.Click += new EventHandler(btnAvancar_Click);
            this.btnSim.Click += new EventHandler(btnSim_Click);
            this.btnNao.Click += new EventHandler(btnNao_Click);
        }

        void btnNao_Click(object sender, EventArgs e)
        {
            try
            {
                tblPergunta.Visible = false;
                btnAvancar.Visible = true;
                pnlDbe1.Enabled = true;
                pnlDbe2.Enabled = true;
                ViabilidadeRespostaObrigaViabilidade = true;
            }
            catch (Exception ex)
            {
                ErroresDeSistema(General.MensajeDeError, ex, ref ErrorSummary);
            }

        }

        void btnSim_Click(object sender, EventArgs e)
        {
            try
            {
                tblPergunta.Visible = false;
                ViabilidadeRespostaObrigaViabilidade = false;
                btnAvancar.Visible = true;
                pnlDbe1.Enabled = true;
                pnlDbe2.Enabled = true;
                btnAvancar_Click(sender, e);
                ViabilidadeRespostaObrigaViabilidade = true;
            }
            catch (Exception ex)
            {
                ErroresDeSistema(General.MensajeDeError, ex, ref ErrorSummary);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                if (!IsPostBack)
                {

                    //pUsuarioSistema = "52090353287";
                    //pCNPJIntituicaoUsuario = "10054583000197";
                    //pNumeroServentia = "";
                    //req = new bRequerimento();
                    //req.ProtocoloViabilidade = "99500001107260";
                    //req.ProtocoloRCPJ = "0020153153636";
                    //req.CodigoDBE = "RJ5034873919753924000197";
                    ////req.NaturezaJuridicaCodigo = req.DBE.NaturezaJuridicaCodigo;
                    //req.nrMatricula = "33105565153";
                    ////req.Data_Assinatura = txtDataUtenticacao.Value;

                    //req.UsuarioRegin = "52090353287";
                    //Response.Redirect("comparadadosdbe.aspx", false);

                    //return;

                    Page.Header.Title = OrgaoRegistro + " ALTERAÇÃO";
                    
                    

                    if (ConfigurationManager.AppSettings["TipoValidaDeferidor"] != null && ConfigurationManager.AppSettings["TipoValidaDeferidor"].ToString() != "")
                    {
                        TipoValidaDeferidor = ConfigurationManager.AppSettings["TipoValidaDeferidor"].ToUpper();
                    }

                    if (ConfigurationManager.AppSettings["ComparaDBEViabilidade"].ToLower() != "on")
                    {
                        lblAvisoViabilidade.Visible = false;
                        lblnumViabilidade.Visible = false;
                        txtviabilidade.Visible = false;
                    }

                    lblserventia.Visible = false;
                    txtnumeroServentia.Visible = false;

                    txtNire.MaxLength = 12;
                    if (pNumeroServentia != "")
                    {
                        lblserventia.Visible = true;
                        txtnumeroServentia.Visible = true;
                        txtnumeroServentia.Text = pNumeroServentia;
                        txtNire.MaxLength = 6;
                    }

                }

            }
            catch (Exception ex)
            {
                ErroresDeSistema(General.MensajeDeError, ex, ref ErrorSummary);
            }


        }

        private string VerificaDadosDbe()
        {
            ViabilidadeObrigatoriaPeloDBE = false;
            RequerimentoObrigatoriaPeloDBE = false;
            string Dbe = retProt(txtDBE.Text);
            WsRFB.Retorno _retornoWS = getDbeRFB(Dbe);

            if (_retornoWS.status != "OK" || _retornoWS.codretorno == "08" || _retornoWS.codretorno == "05")
            {
                return _retornoWS.descricao;
            }

            string Resp = validaTipoOrgaodeRegistroDeferimento(_retornoWS);
            if (Resp != "")
            {
                return Resp;
            }

            DataSet dsDBE = CarregaDadosWebServiceDbe(Dbe);
            DataTable resultDBE = dsDBE.Tables["DadosFCPJ"];

            if (resultDBE != null && resultDBE.Rows.Count > 0)
            {
                //Carrega apropriedade DBE com os dados do DBE
                req.CarregaDadosComparacaoDBE(dsDBE);
                ViabilidadeObrigatoriaPeloDBE = VerificaEventoDBEParaObrigarViabildiade(req.DBE.ProtocoloEvento);
                RequerimentoObrigatoriaPeloDBE = VerificaEventoDBEParaObrigarRequerimento(req.DBE.ProtocoloEvento);
            }
            return "";
        }

        private string VerificaViabilidade()
        {
            DataSet result = new DataSet();
            result = CarregaDadosWebServiceViabilidade(txtviabilidade.Text.Trim());
            DataTable ViaProtocolo = result.Tables["VIA_PROTOCOLO_VIAB"];
            if (ViaProtocolo == null)
            {
                return "Protocolo de Viabilidade Inexistente.";
            }

            if (ViabilidadeConcluida == "on")
            {
                if (ViaProtocolo.Rows[0]["STATUS_VIABILIDADE"].ToString().ToUpper() != "VA")
                {
                    return ("Viabilidade não válida para trâmite.");
                }
            }
            return "";
        }

        void btnAvancar_Click(object sender, EventArgs e)
        {
            try
            {
                tblPergunta.Visible = false;

                //ViabilidadeObrigatoriaPeloDBE = true;
                //ViabilidadeObrigatoriaConfiguracao = true;


                //if (ViabilidadeObrigatoriaPeloDBE && ViabilidadeObrigatoriaConfiguracao && ViabilidadeRespostaObrigaViabilidade)
                //{
                //    if (txtviabilidade.Text.Trim() == "")
                //    {
                //        ErrorSummary.AddErrorMessage("Numero de Viabilidade Obrigatorio");
                //        tblPergunta.Visible = true;
                //        btnAvancar.Visible = false;
                //        pnlDbe1.Enabled = false;
                //        pnlDbe2.Enabled = false;
                //        return;
                //    }
                //}
                //return;

                req = new bRequerimento();

                if (txtnumeroServentia.Visible)
                {
                    if (txtnumeroServentia.Text.Trim().Length != 6)
                    {
                        ErrorSummary.AddErrorMessage("Numero de Serventia Invalido, tem que ter 6 numeros");
                        return;
                    }
                }

                if (txtProtocoloOR.Text == "")
                {
                    ErrorSummary.AddErrorMessage("Numero de Protocolo Obrigatorio");
                    return;
                }

                if (txtDataUtenticacao.Text.Trim() == "")
                {
                    ErrorSummary.AddErrorMessage("Data do Registro Obrigatorio");
                    return;
                }

                //if (txtNire.Text.Trim() == "")
                //{
                //    ErrorSummary.AddErrorMessage("Número do Órgao Registro Obrigatorio");
                //    return;
                //}

                if (!txtnumeroServentia.Visible)
                {
                    //if (!psc.Framework.General.IsValidNIRE(txtNire.Text))
                    //{
                    //    ErrorSummary.AddErrorMessage("Número do Órgao Registro Inválido");
                    //    return;
                    //}
                }

                if (txtDBE.Text == "")
                {
                    ErrorSummary.AddErrorMessage("Número de DBE Obrigatorio");
                    return;
                }

                if (txtDBE.Text.Trim().Length != 24)
                {
                    ErrorSummary.AddErrorMessage("Número de DBE invalido deve conter 24 carateres");
                    return;
                }

                //if (pCNPJInstituicaoDefault != "09280442000103")
                //{
                //    string DbeUtilizado = req.ValidaDbeUtilizado(txtDBE.Text.Trim());
                //    if (DbeUtilizado != "")
                //    {
                //        ErrorSummary.AddErrorMessage(DbeUtilizado);
                //        return;
                //    }


                //    string DBEFilialUti = req.VerificaDbeUtilizadoFilial(txtDBE.Text.Trim());
                //    if (DBEFilialUti != "")
                //    {
                //        ErrorSummary.AddErrorMessage(DBEFilialUti);
                //        return;
                //    }
                //}

                if (TipoValidaDeferidor == "PROCESSO")
                {

                    bool encontroNumero = false;
                    for (int i = 0; i < dtResultXml.Tables["NIRE"].Rows.Count; i++)
                    {
                        if (txtNire.Text == dtResultXml.Tables["NIRE"].Rows[i]["NROOG"].ToString())
                        {
                            encontroNumero = true;
                            i = dtResultXml.Tables["NIRE"].Rows.Count;
                        }
                    }
                    //encontroNumero = true;
                    if (!encontroNumero)
                    {
                        ErrorSummary.AddErrorMessage("Numero de orgão de registro, não relacionado para esse processo");
                        return;
                    }
                }

                ViabilidadeObrigatoriaConfiguracao = true;
                if (ConfigurationManager.AppSettings["ComparaDBEViabilidade"].ToLower() != "on")
                {
                    ViabilidadeObrigatoriaConfiguracao = false;
                }

                string RespDbe = VerificaDadosDbe();
                if (RespDbe != "")
                {
                    ErrorSummary.AddErrorMessage(RespDbe);
                    return;
                }

                //ViabilidadeObrigatoria = false;

                if (ConfigurationManager.AppSettings["ComparaDBEViabilidade"] == null || ConfigurationManager.AppSettings["ComparaDBEViabilidade"].ToString() == "")
                {
                    throw new Exception("Falta Parametro ComparaDBEViabilidade no web.config");
                }

                //Para nao validar o Cartorio do RJ
                //Porque ele ja faia isso antes ai pediram para nao validar
                //if (pCNPJIntituicaoUsuario != "27079821000111" && pCNPJIntituicaoUsuario != "09280442000103" && pCNPJIntituicaoUsuario != "04825329000142")
                //{
                //    if (RequerimentoObrigatoriaPeloDBE)
                //    {
                //        ErrorSummary.AddErrorMessage("O DBE desse processo possui Eventos que devem ser feito no modulo Examinador. Para isso o usuário precisa preencher o Requerimento Eletrônico.");
                //        return;
                //    }
                //}
                if (txtviabilidade.Visible)
                {
                    if (ViabilidadeObrigatoriaPeloDBE && ViabilidadeObrigatoriaConfiguracao && ViabilidadeRespostaObrigaViabilidade)
                    {
                        if (txtviabilidade.Text.Trim() == "")
                        {
                            ErrorSummary.AddErrorMessage("Numero de Viabilidade Obrigatorio");
                            tblPergunta.Visible = true;
                            btnAvancar.Visible = false;
                            pnlDbe1.Enabled = false;
                            pnlDbe2.Enabled = false;
                            return;
                        }

                        string Resp = VerificaViabilidade();
                        if (Resp != "")
                        {
                            ErrorSummary.AddErrorMessage(Resp);
                            return;
                        }
                    }
                }

                req.ProtocoloViabilidade = txtviabilidade.Text.ToUpper();
                req.ProtocoloRCPJ = txtProtocoloOR.Text;
                req.CodigoDBE = retProt(txtDBE.Text.ToUpper());
                //req.NaturezaJuridicaCodigo = req.DBE.NaturezaJuridicaCodigo;
                req.nrMatricula = txtNire.Text;
                req.Data_Assinatura = txtDataUtenticacao.Value;

                req.UsuarioRegin = pUsuarioSistema;
                if (ViabilidadeObrigatoriaPeloDBE && ViabilidadeObrigatoriaConfiguracao && ViabilidadeRespostaObrigaViabilidade)
                {
                    Response.Redirect("comparadadosdbe.aspx", false);
                }
                else
                {

                    System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
                    data.Add("pgnComparaDBE.Dbe", retProt(txtDBE.Text.ToUpper()));
                    if (txtDataUtenticacao.Value == DateTime.MinValue)
                    {
                        data.Add("pgnComparaDBE.Autenticacao", "");
                    }
                    else
                    {
                        data.Add("pgnComparaDBE.Autenticacao", ((DateTime)txtDataUtenticacao.Value).ToString("yyyyMMdd"));
                    }
                    data.Add("pgnComparaDBE.Viabilidade", txtviabilidade.Text.ToUpper());
                    data.Add("pgnComparaDBE.ProtocoloRCPJ", txtProtocoloOR.Text);
                    data.Add("pgnComparaDBE.NroOrgaoRegistro", txtNire.Text);
                    data.Add("pgnComparaDBE.UsuarioSistema", pUsuarioSistema);
                    data.Add("pgnComparaDBE.DefereDBE", "on");


                    HttpHelper.RedirectAndPOST(this.Page, "DeferidorGenerico.aspx", data);
                    return;
                }

            }
            catch (Exception ex)
            {
                ErroresDeSistema(General.MensajeDeError, ex, ref ErrorSummary);
            }
        }
        private bool VerificaEventoDBEParaObrigarViabildiade(List<bProtocoloEvento> ProtocoloEvento)
        {
            for (int i = 0; i < ProtocoloEvento.Count; i++)
            {
                bProtocoloEvento ev;
                ev = (bProtocoloEvento)ProtocoloEvento[i];
                switch (ev.CodigoEvento.ToString())
                {
                    case "101":
                    case "102":
                    case "209":
                    case "210":
                    case "211":
                    case "220":
                    case "244":
                        return true;
                }
            }
            return false;
        }

        private bool VerificaEventoDBEParaObrigarRequerimento(List<bProtocoloEvento> ProtocoloEvento)
        {
            for (int i = 0; i < ProtocoloEvento.Count; i++)
            {
                bProtocoloEvento ev;
                ev = (bProtocoloEvento)ProtocoloEvento[i];
                switch (ev.CodigoEvento.ToString())
                {
                    case "225":
                        return false;
                }
            }

            for (int i = 0; i < ProtocoloEvento.Count; i++)
            {
                bProtocoloEvento ev;
                ev = (bProtocoloEvento)ProtocoloEvento[i];
                switch (ev.CodigoEvento.ToString())
                {
                    case "101":
                    case "102":
                    case "209":
                    case "210":
                    case "211":
                    case "220":
                    case "244":
                    case "202":
                    case "247":
                    case "222":
                    case "517":
                        return true;
                }
            }
            return false;
        }
       
        void txtProtocoloOR_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                pNumeroServentia = txtnumeroServentia.Text;

                TipoValidaDeferidor = "BASICA";

                if (pNumeroServentia == "093245" || pCNPJIntituicaoUsuario == "09280442000103")
                {
                    TipoValidaDeferidor = "PROCESSO";
                }


                dtResultXml = new DataSet();

                if (TipoValidaDeferidor == "PROCESSO")
                {
                    //RJ Mandou retirar isso e pode mudar a data, mantis 8104 da Tatiana
                    if (pCNPJIntituicaoUsuario != "09280442000103")
                        txtDataUtenticacao.ReadOnly = true;
                    //txt
                }

                if (txtProtocoloOR.Text != "")
                {
                    if (TipoValidaDeferidor == "PROCESSO")
                    {
                        string XmlProtocolo = "";
                        try
                        {
                            XmlProtocolo = GeraXmlProtocolo();
                            XmlProtocolo = XmlProtocolo.Replace("<EnderecoCompleto />", "");
                            if (pNumeroServentia == "093245")
                            {
                                TipoValidaDeferidor = "BASICA";
                                txtDataUtenticacao.ReadOnly = false;
                            }
                        }
                        catch 
                        {
                            TipoValidaDeferidor = "BASICA";
                            txtDataUtenticacao.ReadOnly = false;
                            ErrorSummary.AddErrorMessage("Comunicação com o Orgao de Registro esta invalida no momento, por favor entre em contato com suporte ou continua digitando os dados");
                            return;
                        }
                        //XmlProtocolo = "<ROOT><STATUS>03</STATUS><DESCRICAO><![CDATA[Protocolo com formato inválido]]></DESCRICAO></ROOT>";
                        

                        XmlTextReader reader = new XmlTextReader(new StringReader(XmlProtocolo));
                        dtResultXml.ReadXml(reader);

                        string status = dtResultXml.Tables["ROOT"].Rows[0]["STATUS"].ToString();
                        string messagem = dtResultXml.Tables["ROOT"].Rows[0]["DESCRICAO"].ToString();

                        if (status != "01")
                        {
                            txtNire.Text = "";
                            txtDataUtenticacao.Text = "";
                            lblNome.Text = "";
                            ErrorSummary.AddErrorMessage(messagem);
                            return;
                        }

                        txtNire.Text = "";
                        txtNire.ReadOnly = false;
                        if (dtResultXml.Tables["NIRE"].Rows.Count == 1)
                        {
                            //txtNire.ReadOnly = true;
                            txtNire.Text = dtResultXml.Tables["NIRE"].Rows[0]["NROOG"].ToString();
                        }
                        txtDataUtenticacao.Value = ConvertStringDateTime(dtResultXml.Tables["PROTOCOLO"].Rows[0]["DATAAUTENTICACAO"].ToString());
                        lblNome.Text = dtResultXml.Tables["PROTOCOLO"].Rows[0]["NOME"].ToString();
                    }

                    //DataTable Nire = dtResultXml.Tables["NIRE"];
                }
            }
            catch (Exception ex)
            {
                ErroresDeSistema(ex.Message, ex, ref ErrorSummary);
            }
        }


        private string GeraXmlProtocolo()
        {
            StringBuilder pXml = new StringBuilder();

            if (TipoValidaDeferidor == "PROCESSO")
            {
                if (pNumeroServentia == "093245")
                {
                    RCPJRJ_WSProcesso.RCPJService wsprocesso = new RCPJRJ_WSProcesso.RCPJService();
                    wsprocesso.Url = "http://consulta.rcpjrj.com.br/rcpj/query/call/soap?WSDL";
                    return wsprocesso.getProcesso(txtProtocoloOR.Text);

                }

                if (pCNPJIntituicaoUsuario == "09280442000103")
                {
                    RJ_JUCERJA.SvcDeferidor ws = new RJ_JUCERJA.SvcDeferidor();
                    ws.Url = "http://www.jucerja.rj.gov.br/SvcJucerja.Regin/SvcDeferidor.svc?wsdl";
                    ws.Url = "http://www.jucerja.rj.gov.br:8099/SvcJucerja.Regin/SvcDeferidor.svc?wsdl";
                    return ws.DadosProtocolo(txtProtocoloOR.Text);
                }
                
                return "Tipo de processo não programado";

            }
            //return "";
            //if (TipoValidaDeferidor == "PROCESSO")
            //{
            //    pXml = new StringBuilder();
            //    pXml = new StringBuilder();

            //    pXml.AppendLine("<ROOT>");
            //    pXml.AppendLine("   <STATUS>01</STATUS>");
            //    pXml.AppendLine("   <DESCRICAO>Mensagem</DESCRICAO>");
            //    pXml.AppendLine("   <PROTOCOLO>");
            //    pXml.AppendLine("	    <NOME><![CDATA[RAZAO > & SOCIAL]]></NOME>");
            //    //pXml.AppendLine("	    <NOME2>RAZAO & SOCIAL</NOME2>"); 
            //    pXml.AppendLine("	    <DATAAUTENTICACAO>20150312</DATAAUTENTICACAO>");
            //    pXml.AppendLine("	    <GRUPONIRE>");
            //    pXml.AppendLine("		    <Nire> ");
            //    pXml.AppendLine("		        <NROOG>33105565153</NROOG>");
            //    pXml.AppendLine("		        <CNPJ>11111111000191</CNPJ>	");
            //    pXml.AppendLine("		    </Nire> ");
            //    //pXml.AppendLine("		    <Nire> ");
            //    //pXml.AppendLine("		        <NROOG>51201082945</NROOG>");
            //    //pXml.AppendLine("		        <CNPJ>22222222000191</CNPJ>	");
            //    //pXml.AppendLine("		    </Nire> ");
            //    pXml.AppendLine("	    </GRUPONIRE>");
            //    pXml.AppendLine("   </PROTOCOLO> ");
            //    pXml.AppendLine("</ROOT>");


            //    //XmlDocument doc2 = new XmlDocument();
            //    //doc2.Load(new StringReader(pXml.ToString()));


            //    //XmlNode nodeRe2 = doc2.SelectSingleNode("//ROOT//PROTOCOLO");
            //    //pCNPJ = nodeRe2["NOME"].InnerText.Trim();

            //    return pXml.ToString();
            //}

            return "";

        }
       
    }
}