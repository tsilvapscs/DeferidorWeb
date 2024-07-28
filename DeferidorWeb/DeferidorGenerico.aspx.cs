using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using psc.ApplicationBlocks.SessionState;
using RCPJ.BLL;
using System.Collections.Generic;
using RCPJ.DAL.Helper;
using RCPJ.DAL.Entities;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
//using psc.Application.JUNTA.Entities;
using System.Text;
using RCPJ.DAL.Ruc;

namespace RCPJ.Application
{
    [Serializable]
    public partial class DeferidorGenerico : PageBase
    {
        #region Variables
        //[SessionPageState("psc.RCPJ.Requerimento")]
        [TransientPageState]
        protected bRequerimento Req;

        [TransientPageState]
        protected bExaminador _analista;

        protected int predSQPessoa;

        protected List<bSocios> AdministradoresS = new List<bSocios>();
        protected List<bSocios> AdministradoresF = new List<bSocios>();

        protected List<bProtocoloConfirmacao> tempConfirmacoes = new List<bProtocoloConfirmacao>();

        [TransientPageState]
        protected string parametrosPop = "'top=0,left=0,screenX=0,screenY=0,status=yes,scrollbars=yes,toolbar=no,resizable=yes,maximized=yes,menubar=no,location=no'";

        [TransientPageState]
        protected string UF_ORGAO_REGISTRO;

        static string[] tbEnquadramento = new string[10] { " - ME ", "- ME ", " -ME ", "-ME ", " ME ", " - EPP", "- EPP ", " -EPP ", "-EPP ", " EPP " };

        [TransientPageState]
        protected int _analisePrevia = 2;

        [TransientPageState]
        protected int _fazAnalisePrevia = 2;

        [TransientPageState]
        protected bool _confirmaSimPadrao = false;

        [TransientPageState]
        protected bool _consulta = false;

        [TransientPageState]
        protected string NroViabilidade = string.Empty;
        [TransientPageState]
        protected string NroDbe = string.Empty;
        [TransientPageState]
        protected string pXmlRequerimento = string.Empty;

        [TransientPageState]
        protected WsRFB.Retorno retorno;
        [TransientPageState]
        protected List<string> pEventos = new List<string>();

        #endregion

        #region Constructor
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
            //btnAvancar.Click += new EventHandler(btnAvancar_Click);
            btnAlerta1.Click += new EventHandler(btnAlerta_Click);
            btnAlerta2.Click += new EventHandler(btnAlerta_Click);
            this.btnConfirmaSim.Click += new EventHandler(btnConfirmaSim_Click);
            this.btnDeferirDBEMatriz.Click += new EventHandler(btnDeferirDBEMatriz_Click);
            this.btnInDeferirDBEMatriz.Click += new EventHandler(btnInDeferirDBEMatriz_Click);
            this.btnGravarRequerimento.Click += new EventHandler(btnGravarRequerimento_Click);
            this.btnVoltar.Click += new EventHandler(btnVoltar_Click);
        }



        void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EntradaDeferidor.aspx", true);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    pTituloSistemaJUCESC = "CONSULTA DBE";
                    pXmlRequerimento = "";


                    // Campos para testes e preencher na mao

                    //lblNumProcesso.Text = "1201808301119478";
                    //lblAutenticacao.Text = "12/09/2003";
                    //txtNire.Text = "204241";
                    //lblCpf.Text = "27551970720";
                    //NroDbe = "BA9082658400005774375501";
                    //pNumeroServentia = "093245";
                    //pUsuarioSistema = "27551970720";
                    //pCNPJIntituicaoUsuario = "27079821000111";

                    if (ConfigurationManager.AppSettings["DefereProcesso"] == null && ConfigurationManager.AppSettings["DefereProcesso"].ToString() != "")
                    {
                        ErrorSummary.AddErrorMessage("Variavel do web.config DefereProcesso Vazio");
                        return;
                    }

                    using (WsRFB.ServiceReginRFB c = new WsRFB.ServiceReginRFB())
                    {
                        c.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                        divViabilidade.Visible = true;
                        divProcesso.Visible = true;
                        divDBE.Visible = true;

                        #region Preencher variaveis
                        if (Req != null)
                        {
                            lblAutenticacao.Text = ((DateTime)Req.Data_Assinatura).ToString("dd/MM/yyyy");
                            NroDbe = Req.CodigoDBE;
                            lblNumProcesso.Text = Req.ProtocoloRCPJ;
                            lblNumViabilidade.Text = Req.ProtocoloViabilidade;
                            txtNire.Text = Req.nrMatricula;
                            lblCpf.Text = Req.UsuarioRegin;
                            //pUsuarioSistema = Req.UsuarioRegin;
                        }
                        else
                        {
                            lblNumProcesso.Visible = false;
                            lblTituloProcessoOR.Visible = false;

                            lblCpf.Visible = false;
                            lblResposavel.Visible = false;

                            txtNire.Visible = false;
                            lblNire.Visible = false;

                            lblNumViabilidade.Visible = false;
                            lblTitViabilidade.Visible = false;

                            lblAutenticacao.Visible = false;
                            lblDataRegistro.Visible = false;

                            if (Request.Form["pgnComparaDBE.Dbe"] != null && Request.Form["pgnComparaDBE.Dbe"].ToString() != "")
                            {
                                NroDbe = Request.Form["pgnComparaDBE.Dbe"].ToString();
                            }

                            if (Request.Form["pgnComparaDBE.Autenticacao"] != null && Request.Form["pgnComparaDBE.Autenticacao"].ToString() != "")
                            {
                                lblAutenticacao.Text = ConvertStringDateTime(Request.Form["pgnComparaDBE.Autenticacao"].ToString()).ToString();
                            }

                            if (Request.Form["pgnComparaDBE.Viabilidade"] != null && Request.Form["pgnComparaDBE.Viabilidade"].ToString() != "")
                            {
                                lblNumViabilidade.Text = Request.Form["pgnComparaDBE.Viabilidade"].ToString();
                                lblNumViabilidade.Visible = true;
                                lblTitViabilidade.Visible = true;
                            }

                            if (Request.Form["pgnComparaDBE.ProtocoloRCPJ"] != null && Request.Form["pgnComparaDBE.ProtocoloRCPJ"].ToString() != "")
                            {
                                lblNumProcesso.Text = Request.Form["pgnComparaDBE.ProtocoloRCPJ"].ToString();
                                lblNumProcesso.Visible = true;
                                lblTituloProcessoOR.Visible = true;
                            }

                            if (Request.Form["pgnComparaDBE.NroOrgaoRegistro"] != null && Request.Form["pgnComparaDBE.NroOrgaoRegistro"].ToString() != "")
                            {
                                txtNire.Text = Request.Form["pgnComparaDBE.NroOrgaoRegistro"].ToString();
                                txtNire.Visible = true;
                                lblNire.Visible = true;

                            }

                            if (Request.Form["pgnComparaDBE.UsuarioSistema"] != null && Request.Form["pgnComparaDBE.UsuarioSistema"].ToString() != "")
                            {
                                lblCpf.Text = Request.Form["pgnComparaDBE.UsuarioSistema"].ToString();
                            }
                            if (Request.Form["pgnComparaDBE.DefereDBE"] != null && Request.Form["pgnComparaDBE.DefereDBE"].ToString() != "")
                            {
                                if (Request.Form["pgnComparaDBE.DefereDBE"].ToString() == "on")
                                {
                                    btnDeferirDBEMatriz.Visible = true;
                                    if (pCNPJIntituicaoUsuario == "09280442000103")
                                    {
                                        btnInDeferirDBEMatriz.Visible = true;
                                    }
                                    btnVoltar.Visible = true;
                                    lblCpf.Visible = true;
                                    lblResposavel.Visible = true;
                                    lblAutenticacao.Visible = true;
                                    lblDataRegistro.Visible = true;
                                    pTituloSistemaJUCESC = "DEFERIDOR";
                                }
                            }
                        }
                        #endregion

                        //NroDbe = "BA9082658400005774375501";
                        //btnDeferirDBEMatriz.Visible = true;
                        //btnVoltar.Visible = true;
                        //NroDbe = "233191567200008557928920"; //Contador pf
                        //NroDbe = "242338304700056395477820"; // Contador PJ
                        //NroDbe = "233191567200008557928920"; // Contador PF SC
                        //NroDbe = "RJ1269889828792182000108"; // Qualificação 63

                        if (NroDbe == "")
                        {
                            ErrorSummary.AddErrorMessage("Falta Parametros de Numero Dbe");
                            return;
                        }


                        lblDBE.Text = NroDbe;

                        divNumeroServentia.Visible = false;
                        if (pNumeroServentia != "")
                        {
                            lblNumeroServentiaValor.Text = pNumeroServentia;
                            divNumeroServentia.Visible = true;
                        }


                        string recibo = NroDbe.Substring(0, 10);
                        string identificacao = NroDbe.Substring(10, 14);

                        retorno = c.ServiceWs35Regin(identificacao, recibo);

                        if (retorno.status == "NOK")
                        {
                            ErrorSummary.AddErrorMessage(retorno.descricao);
                            btnDeferirDBEMatriz.Visible = false;
                            btnGravarRequerimento.Visible = false;
                            return;
                        }

                        lblStatusDBE.Text = retorno.descricao;

                        Req = ProcessaDadosDbe(retorno.oWs35Response);

                        pXmlRequerimento = PreparaXMLParaGravarOR(Req);

                    }

                    if (Req == null)
                    {
                        ErrorSummary.AddErrorMessage("Erro ao preencher dados");
                        return;
                    }


                    ZeraTimer();
                    divAvisos.Style.Add("display", "none");
                    telaCarregandoMensagens.Style.Add("display", "none");

                }
                AddEventos();
                AddEmpresa();
                AddSociosQSA_JUCERJA();
                AddCNAES();

                //Para testar a gravaçõ das RUCs
                //using (WsRFB.ServiceReginRFB cc = new WsRFB.ServiceReginRFB())
                //{
                //    cc.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                //    WsRFB.redesim dados352 = new WsRFB.redesim();

                //    if (retorno.oWs35Response != null && retorno.oWs35Response.dadosRedesim != null)
                //    {
                //        dados352 = retorno.oWs35Response.dadosRedesim;
                //    }


                //    string Resp = GravaWsRFB11Ruc(pEventos, lblNumProcesso.Text, "05985187000143", txtNire.Text, "RJP1880197701", "", pNumeroServentia, pCNPJIntituicaoUsuario, dados352, "RJ1018725905985187000143");
                //}


                //btnDeferirDBEMatriz.Visible = true;
                //btnVoltar.Visible = true;
                //lblCpf.Visible = true;
                //lblResposavel.Visible = true;
                //lblAutenticacao.Visible = true;
                //lblDataRegistro.Visible = true;
                //pTituloSistemaJUCESC = "DEFERIDOR";

                //btnDeferirDBEMatriz.Visible = true;
            }

            catch (Exception ex)
            {
                ErroresDeSistema(ex, ref ErrorSummary);
                Alert(ex.Message);
                //ExibirMensagen(true, "Ocorreu um problema no processamento.");
                // ExibirMensagen(false, ex.Message);
            }
        }



        //private WsRFB.Retorno DefereProcessoRFB(string nroDBE, string nire, string nome, string dataRegistro, string UsuarioSistema, string nroViabilidade, string pProtocolo, string cnpj)
        //{
        //    //PSC_PROTOCOLO_DBE_RFB pp = new PSC_PROTOCOLO_DBE_RFB();
        //    WsRFB.ServiceReginRFB reginws05 = new WsRFB.ServiceReginRFB();
        //    WsRFB.Retorno resulRegin = new WsRFB.Retorno();
        //    if (ConfigurationManager.AppSettings["urlServicesRFBRegin"] == null || ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString() == "")
        //    {
        //        throw new Exception("Falta Parametro urlServicesRFBRegin no web.config");
        //    }
        //    reginws05.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
        //    string nroRecibo = nroDBE.Substring(0, 10);
        //    string nroIdentificacao = nroDBE.Substring(10, 14);
        //    resulRegin = reginws05.ServiceWs05(nroIdentificacao, nroRecibo, UsuarioSistema);

        //    /*
        //        Se for Ok ou retorno = 8 - Dbe disponibilizado, isso quer dizer que ou ele
        //     * fez a entrega de documento ou ele ja tinha feito a entrega de documento, por isso caso uma das duas
        //     * eu deixo entrar para fazer o deferimento.
        //     */
        //    pp.ppd_cod_ret_ws05 = resulRegin.codretorno;
        //    pp.ppd_cnpj = cnpj;
        //    pp.ppd_nire = nire;
        //    pp.ppd_numero_dbe = nroDBE;
        //    pp.ppd_protocolo = pProtocolo;
        //    pp.ppd_nro_viabilidade = nroViabilidade;
        //    pp.ppd_usuario_ws05 = UsuarioSistema;
        //    pp.ppd_usuario_ws06 = UsuarioSistema;

        //    if (resulRegin.status == "OK" || resulRegin.codretorno == "8" || resulRegin.codretorno == "12")
        //    {

        //        pp.Update();
        //        pp.UpdateWs05Ok();

        //        WsRFB.ServiceReginRFB reginws06 = new WsRFB.ServiceReginRFB();
        //        WsRFB.DadosWs06 dados = new WsRFB.DadosWs06();
        //        WsRFB.Retorno pRetorno = new WsRFB.Retorno();

        //        dados.identificacaoSolicitacao = nroIdentificacao;
        //        dados.reciboSolicitacao = nroRecibo;
        //        dados.numeroNire = nire;
        //        dados.dataRegistro = dataRegistro;
        //        dados.cpfResponsavelDeferimento = UsuarioSistema;
        //        //dados.nomeEmpresarial = nome;

        //        dados.resultadoRegistroIntegradorEstadual = "01";

        //        reginws06.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

        //        resulRegin = reginws06.ServiceWs06(dados);

        //        pp.ppd_cod_ret_ws06 = resulRegin.codretorno;

        //        if (resulRegin.status == "OK")
        //        {
        //            pp.ppd_cnpj = resulRegin.Cnpj;
        //            pp.UpdateWs06Ok();
        //        }
        //        else
        //        {
        //            pp.ppd_erro = resulRegin.descricao;
        //            pp.UpdateWs06NOk();
        //        }
        //    }
        //    else
        //    {
        //        pp.ppd_erro = resulRegin.descricao;
        //        pp.UpdateWs05NOk();
        //    }

        //    return resulRegin;
        //}



        private byte[] StreamFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            return ImageData; //return the byte data
        }

        private string RetornaSubstring(string wTexto1, int wIndice, int wCol, int wRow, out int wConta)
        {
            string wLinha = string.Empty;
            int contaLinha = 0;
            int contaColuna = wIndice;
            int teste = -1;
            psc.Blocks.UI.WebForms.TextBox wTeste = new psc.Blocks.UI.WebForms.TextBox();
            wTeste.TextMode = System.Web.UI.WebControls.TextBoxMode.MultiLine;
            wTeste.Visible = false;
            while (contaLinha <= wRow)
            {
                if (wTexto1.Length - contaColuna < wCol)
                    wCol = wTexto1.Length - contaColuna;
                wLinha = wTexto1.Substring(contaColuna, wCol);

                if (wLinha.Contains("\n"))
                {
                    teste = wLinha.IndexOf("\n");
                    wLinha = wLinha.Substring(0, teste + 1);
                    wTeste.Text += wLinha;
                    contaColuna = contaColuna + wLinha.Length; //wTeste.Text.Length; 
                }
                else
                {
                    wTeste.Text += wLinha;
                    contaColuna = contaColuna + wCol;
                }
                contaLinha++;
            }
            #region Quebra de palavra
            if (wTexto1.Length > contaColuna)
            {
                string hTeste = wTeste.Text.Substring(wTeste.Text.Length - 1, 1);
                if (hTeste != " ")
                {
                    int i = wTeste.Text.Length - 1; // contaColuna - 1;
                    hTeste = wTeste.Text.Substring(i, 1);
                    while (hTeste != " ")
                    {
                        wTeste.Text = wTeste.Text.Substring(0, i);
                        i = i - 1;
                        contaColuna = contaColuna - 1;
                        hTeste = wTeste.Text.Substring(i, 1);
                    }

                }
            }
            #endregion
            wConta = contaColuna;
            return wTeste.Text;
        }

        private string RetiraTipoEnquadramento(string wTexto)
        {
            wTexto = wTexto.Trim();
            if (!wTexto.Contains(" "))
            {
                return wTexto;
            }
            string wAux = wTexto + " ";
            string wEnquadramento = string.Empty;
            for (int wii = 0; wii <= tbEnquadramento.Length - 1; wii++)
            {
                wEnquadramento = tbEnquadramento[wii];
                int ii = wAux.IndexOf(tbEnquadramento[wii], wAux.Length - tbEnquadramento[wii].Length);
                if (ii != -1)
                {
                    wTexto = wTexto.Substring(0, wTexto.Length - tbEnquadramento[wii].Length + 1);
                    break;
                }
            }
            return wTexto;
            //string wAux = wTexto + " ";
            //int ii = wAux.IndexOf(" EPP ", wAux.Length - 5);

            //if (ii != -1)
            //{
            //    wTexto = wTexto.Substring(0, wTexto.Length - 4);
            //    return wTexto;
            //}
            //ii = wAux.IndexOf(" ME ", wAux.Length - 4);
            //if (ii != -1)
            //    wTexto = wTexto.Substring(0, wTexto.Length - 3);
            //return wTexto;
        }

        public void ZeraTimer()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ZeraFrom_" + this.ClientID.ToString(), "zeraTimer();", true);

        }

        #region Botoes
        void btnNovoRequerimento_Click(object sender, EventArgs e)
        {
            string Usuarioregin = Req.UsuarioRegin;
            Req = null;
            Response.Redirect("Exame1.aspx?usuarioid=" + Usuarioregin, false);

        }



        void btnVerificarExigencias_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Exigências", "janelaModal('Exigencias.aspx','Sua','680','1020')", true);
        }



        void btnAlerta_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Alertas.aspx", false);
        }

        #endregion


        private int AchaSocio(int item)
        {
            int wI = -1;
            for (int i = 0; i < Req.Socios.Count; i++)
            {
                if (Req.Socios[i].SQPessoa == item.ToString())
                    wI = i;
            }
            return wI;
        }

        #region Salvar analise


        private void AtualizaConfirmacao(bProtocoloConfirmacao c, bool Valor, string Motivo)
        {
            foreach (bProtocoloConfirmacao busca in Req.Confirmacoes)
            {
                if (busca.T017_grupo == c.T017_grupo
                    && busca.T017_item == c.T017_item)
                {
                    busca.T017_confirma = (Valor == true ? 1 : 0);
                    busca.t017_motivo = Motivo;
                    busca.T017_dt_confirmacao = System.DateTime.Now;
                    busca.T017_andamento_secao = Req.bAndamento.SeccaoDestino;
                    busca.T017_andamento_seq = Req.bAndamento.SequenciaAndamento;
                    break;
                }
            }


        }

        private void AddConfirmacaoItem(bool bConfirma, int Item, int Grupo, string motivo)
        {
            bProtocoloConfirmacao pc = new bProtocoloConfirmacao();
            pc.T005_nr_protocolo = Req.ProtocoloRequerimento;
            pc.T017_dt_confirmacao = System.DateTime.Now;
            pc.T017_grupo = Grupo;
            pc.T017_item = Item;
            pc.T017_confirma = bConfirma == true ? 1 : 0;
            if (Req.Confirmacoes.Count > 0)
            {
                pc.T017_sequencia = Req.Confirmacoes[0].T017_sequencia;
            }
            else
            {
                pc.T017_sequencia = 1;
            }
            pc.T017_status = 1;
            pc.t017_motivo = motivo;
            pc.T017_usuario = Req.UsuarioRegin;
            pc.T017_andamento_secao = Req.bAndamento.SeccaoDestino;
            pc.T017_andamento_seq = Req.bAndamento.SequenciaAndamento;
            //tempConfirmacoes.Add(pc);
            Req.Confirmacoes.Add(pc);

        }

        private void AtualizaExigenciasPadrao(string chave, string Descricao, bool pChecked, string grupo)
        {
            bool Encontrei = false;
            bExigencias remover = new bExigencias();
            foreach (bExigencias e in Req.ExigenciasOutras)
            {
                if (grupo == "2" || grupo == "3")
                {
                    if (e.Descricao == Descricao)
                    {
                        e.Descricao = Descricao;
                        e.Grupo = grupo;
                        e.FundamentoLegal = chave;
                        Encontrei = true;
                        //break;
                    }
                    if (Encontrei && pChecked)
                    {
                        remover = e;
                        break;
                    }
                }
                else
                {
                    if (e.Grupo == grupo)
                    {
                        e.Descricao = Descricao;
                        e.FundamentoLegal = chave;
                        e.Grupo = grupo;
                        Encontrei = true;
                        //break;
                    }
                    if (Encontrei && pChecked)
                    {
                        remover = e;
                        break;
                    }
                }

            }

            if (Encontrei && pChecked)
            {
                Req.ExigenciasOutras.Remove(remover);
            }

            if (!Encontrei && !pChecked)
            {
                AddExigenciaPadrao(chave, Descricao, grupo);
            }
        }


        private void AddExigenciaPadrao(string chave, string Descricao, string grupo)
        {
            bExigencias e = new bExigencias();
            e.CodExigencia = e.CodigoOutrasExigenciasPadrao;
            e.Descricao = Descricao;
            if (!string.IsNullOrEmpty(chave))
                e.FundamentoLegal = chave;
            else
                e.FundamentoLegal = "Gerado Automaticamente";
            e.ProtocoloRequerimento = Req.ProtocoloRequerimento;
            e.Grupo = grupo;
            Req.ExigenciasOutras.Add(e);

        }


        public bool predicateFindOneSocioConf(bProtocoloConfirmacao pc)
        {

            if (pc.T017_item == predSQPessoa)
            {
                return true;
            }
            {
                return false;
            }

        }


        public bool predicateFindOneSocioBsoc(bSocios s)
        {

            if (s.SQPessoa == predSQPessoa.ToString())
            {
                return true;
            }
            {
                return false;
            }

        }

        public bool predicateFindOneSocioBfundador(bSocios s)
        {

            if (s.SQPessoa == predSQPessoa.ToString())
            {
                return true;
            }
            {
                return false;
            }

        }

        public bool predicateFindOneFilial(bFilial f)
        {

            if (f.SqFilial == predSQPessoa)
            {
                return true;
            }
            {
                return false;
            }

        }


        public bool ExisteConfirmacao(decimal item, decimal grupo, ref bProtocoloConfirmacao c1)
        {
            if (Req.Confirmacoes.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (bProtocoloConfirmacao c in Req.Confirmacoes)
                {
                    if (c.T017_grupo == grupo && c.T017_item == item)
                    {
                        c1 = c;
                        return true;
                    }
                }
                return false;
            }

        }

        #endregion

        #region Monta dados da tela
        public void AddEventos()
        {
            #region Eventos
            lblEvento.Text = "";
            pEventos.Clear();
            bool evento202 = false;
            bool eventoConstituicao = false;
            divNIRE246.Visible = false;
            foreach (bProtocoloEvento pe in Req.ProtocoloEvento)
            {
                if (pe.CodigoEvento.ToString().Trim() == "202")
                    evento202 = true;

                if (pe.CodigoEvento.ToString().Trim() == "101" || pe.CodigoEvento.ToString().Trim() == "102")
                    eventoConstituicao = true;

                if (pe.CodigoEvento.ToString().Trim() == "246")
                {
                    divNIRE246.Visible = true;
                }

                pEventos.Add(pe.CodigoEvento.ToString());
                string eventoTabela = ObterDescricaoEvento(pe.CodigoEvento.ToString());
                lblEvento.Text += pe.CodigoEvento + " - " + ObterDescricaoEvento(pe.CodigoEvento.ToString()).ToUpper();
                lblEvento.Text += "<BR>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp";
            }
            //Caso esteja alterando socios e nao tenha esse evento no DBE e nao seja evento de constituição
            if (!evento202 && Req.Socios.Count > 0 && !eventoConstituicao)
            {
                pEventos.Add("202");
                lblEvento.Text += "202 - " + ObterDescricaoEvento("202").ToUpper();
                lblEvento.Text += "<BR>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp";

            }
            //lblEvento.Text = Req.CodigoEvento + " - " + ObterDescricaoEvento(Req.CodigoEvento.ToString()).ToUpper();

            #endregion
        }
        public void AddEmpresa()
        {



            #region Dados Iniciais
            string reqformatado = "";
            if (Req.ProtocoloRequerimento != null && Req.ProtocoloRequerimento != "")
            {
                reqformatado = String.Format("{0:##-###-###-###-###}", long.Parse(Req.ProtocoloRequerimento));
            }

            lblNumViabilidade.Visible = false;
            lblTitViabilidade.Visible = false;

            if (Req.ProtocoloViabilidade != "")
            {
                lblNumViabilidade.Text = Req.ProtocoloViabilidade;
                lblNumViabilidade.Visible = true;
                lblTitViabilidade.Visible = true;
            }


            if (Req.Nome_Fantasia.Trim() != "")
            {
                lblNomeFantasia.Text = Req.Nome_Fantasia;
                divNomeFantasia.Visible = true;
            }
            else
            {
                divNomeFantasia.Visible = false;
            }


            fdsCapital.Visible = false;
            if (Req.CapitalSocial > 0)
            {
                lblCapitalDistribuicao.Text = Req.CapitalSocial.ToString("0,0.00");
                fdsCapital.Visible = true;
            }

            if (Req.nrEmpresaCNPJ != "")
            {
                lblCNPJ.Text = Req.nrEmpresaCNPJ;
            }

            if (Req.CodigoDBE != "")
            {
                divDBE.Visible = true;
                lblDBE.Text = FormataDBE(Req.CodigoDBE);
            }


            #endregion

            #region Natureza Jurídica
            lblEmpresaNaturezaJuridica.Text = Req.NaturezaJuridicaDescricao;
            fdsNaturezaJuridica.Visible = false;
            if (Req.NaturezaJuridicaDescricao != "")
            {
                fdsNaturezaJuridica.Visible = true;
            }
            #endregion

            #region Nome Empresarial

            fdsNomeEmpresa.Visible = false;
            lblNomeEmpresa.Text = Req.SedeNome;
            if (lblNomeEmpresa.Text != "")
            {
                fdsNomeEmpresa.Visible = true;
            }

            #endregion


            #region Responsavel


            lblCpfResponsavel.Text = Req.Requerente.Cpf;
            lblNomeResponsavel.Text = Req.Requerente.Nome;
            lblQualificacaoResponsavel.Text = "Condicao nao encontrada na tabela " + Req.Requerente.DDD;

            if (Req.Requerente.Cpf != "")
                divResponsavel.Visible = true;

            if (Req.Requerente.DDD != null && Req.Requerente.DDD != "")
            {
                DataTable dtCondicao = dHelperQuery.getCondicaoDNRC(decimal.Parse(Req.Requerente.DDD));
                if (dtCondicao.Rows.Count > 0)
                {
                    lblQualificacaoResponsavel.Text = dtCondicao.Rows[0]["A009_DS_CONDICAO"].ToString();
                }
            }
            #endregion

            #region Motivo Baixa
            if (Req.TextoRestituicaoBaixa != "")
            {
                div1MotivoBaixa.Visible = true;
                lblMotivoBaixa.Text = Req.TextoRestituicaoBaixa;
            }
            #endregion

            #region Porte
            fldEnquadramento.Visible = false;
            if (Req.EnquadramentoDESCRICAO != "")
            {
                fldEnquadramento.Visible = true;
                lblEnquadramento.Text = Req.EnquadramentoDESCRICAO;
            }
            #endregion

            #region Endereço

            string endereco = "";
            fdsEndereco.Visible = false;

            if (Req.SedeDsTipoLogradouro.Trim() != "")
            {
                endereco += Global.valNuloBranco(Req.SedeDsTipoLogradouro) + " ";
            }

            if (Req.SedeLogradouro.Trim() != "")
            {
                endereco += Req.SedeLogradouro + ", ";
            }

            if (Req.SedeNumero.Trim() != "")
            {
                endereco += Req.SedeNumero + ", ";
            }
            if (Req.SedeComplemento.Trim() != "")
            {
                endereco += Req.SedeComplemento + ", ";
            }
            if (Req.SedeBairro.Trim() != "")
            {
                endereco += Req.SedeBairro + ", ";
            }

            if (Req.SedeMunicipio.Trim() != "")
            {
                endereco += ObterNomeMunicipio(Req.SedeMunicipio) + ", ";
            }
            if (Req.SedeUF.Trim() != "" && Req.SedeBairro.Trim() != "")
            {
                endereco += Req.SedeUF + ", "; ;
            }

            if (Req.SedeCEP.Trim() != "")
            {
                endereco += "CEP: " + FormataCEP(Req.SedeCEP);
            }

            lblEmpresaEndereco.Text = endereco;

            if (endereco != "")
            {
                fdsEndereco.Visible = true;
            }
            #endregion

        }
        public bool MostraDistribuicaoCapitalSocios()
        {
            bool encontrei = false;

            foreach (bProtocoloEvento pe in Req.ProtocoloEvento)
            {
                if (pe.CodigoEvento == 247 || //CODEVENTO_CAPITAL_SOCIAL
                    pe.CodigoEvento == 202 || //CODEVENTO_ALTER_QSA
                    pe.CodigoEvento == 101 || pe.CodigoEvento == 210)   //CONSTITUICAO
                {
                    encontrei = true;
                    break;
                }

            }
            return encontrei;
        }




        public void AddSociosQSA()
        {
            Table t = new Table();
            TableRow LinhaSocio;
            TableCell CelulaSocio;
            Control r;
            String IdentificaSocios = "";
            String wCnpj = "";
            string wCnpjAux = "";
            int indice = 0;
            string bordas = "border-bottom:thin solid gray;border-left:thin solid gray;border-right:thin solid gray;border-top:thin solid gray;";
            string bordasAlerta = "background-color:yellow; border-bottom:thin solid yellow;border-left:thin solid yellow;border-right:thin solid yellow;border-top:thin solid yellow;";

            pnQSA.Controls.Clear();

            #region Cria HTML Table QSAtable
            t.Attributes.Add("runat", "server");
            t.ID = "QSAtable";
            t.CellSpacing = 5;


            #endregion


            LinhaSocio = new TableRow();
            CelulaSocio = new TableCell();


            CelulaSocio.Attributes.Add("style", "background-color:#5080a2;color:White; align='center'");
            CelulaSocio.Text = "Confirma?&nbsp&nbsp";
            LinhaSocio.Cells.Add(CelulaSocio);
            CelulaSocio = new TableCell();
            CelulaSocio.Attributes.Add("style", "background-color:#5080a2;color:White; align='center'");
            CelulaSocio.Text = "Identificação do sócio, empresário ou titular";
            LinhaSocio.Cells.Add(CelulaSocio);
            LinhaSocio.Width = 800;
            t.Rows.Add(LinhaSocio);
            LinhaSocio = new TableRow();

            #region Socios
            if (Req.SociosAtivos.Count > 0)
            {
                string _bordas = bordas;

                foreach (bSocios s in Req.SociosAtivos)
                {
                    if (s.Qualificacao == "5" || s.rep_legal != 0)
                    {
                        AdministradoresS.Add(s);
                    }
                    if (s.Qualificacao != "5")
                    {

                        IdentificaSocios = "";



                        CelulaSocio = new TableCell();
                        CelulaSocio.Attributes.Add("class", "font1");


                        r = new RadioButton();
                        ((RadioButton)r).ID = "confirmaSocio" + indice + "S";
                        ((RadioButton)r).GroupName = "confirmaQSA" + indice;
                        ((RadioButton)r).Text = "Sim";
                        ((RadioButton)r).Checked = false;
                        ((RadioButton)r).SkinID = s.SQPessoa;
                        CelulaSocio.Controls.Add(r);
                        r = new RadioButton();
                        ((RadioButton)r).ID = "confirmaSocio" + indice + "N";
                        ((RadioButton)r).GroupName = "confirmaQSA" + indice;
                        ((RadioButton)r).Text = "Não";
                        ((RadioButton)r).Checked = true;
                        ((RadioButton)r).SkinID = s.SQPessoa;
                        CelulaSocio.Controls.Add(r);
                        CelulaSocio.Attributes.Add("valign", "center");


                        _bordas = bordas;

                        foreach (bAlertaRequerimento b in Req.Alertas)
                        {
                            if (b.Valor.Trim() == s.CPFCNPJ.Trim())
                            {
                                _bordas = bordasAlerta;
                            }
                        }

                        CelulaSocio.Attributes.Add("style", _bordas + ";width:15%");

                        //r = new System.Web.UI.HtmlControls.HtmlInputHidden();
                        //((HtmlInputHidden)r).Value = s.SQPessoa;
                        //((HtmlInputHidden)r).ID = "IDsocio_" + indice;


                        CelulaSocio.Controls.Add(r);


                        //CelulaSocio.Width = Unit.Pixel(300);
                        LinhaSocio.Cells.Add(CelulaSocio);

                        CelulaSocio = new TableCell();
                        CelulaSocio.Attributes.Add("class", "fontReq2E");




                        //CelulaSocio.Attributes.Add("style", "width:90%");
                        //Dados do Socio

                        CelulaSocio.Text = Convert.ToString(indice + 1) + ". " + NomeProprio(s.Nome).ToUpper();
                        if (s.DataSaidaSocio != null)
                        {
                            decimal qtdQuotasCedidas = Req.GetQtdQuotasCedidas(Int32.Parse(s.SQPessoa));
                            if (qtdQuotasCedidas == 0)
                            {
                                IdentificaSocios += "<br/>Retira-se da sociedade o sócio.";
                            }
                            else
                            {
                                List<bSociosQuotas> QuotasCedidas = Req.getQuotasCedidas(Int32.Parse(s.SQPessoa));

                                IdentificaSocios += "<br/>Retira-se da sociedade o sócio, detentor de " + qtdQuotasCedidas.ToString();
                                IdentificaSocios += "(" + GetNumeroporExtenso(qtdQuotasCedidas, false) + ") quotas, ";
                                IdentificaSocios += "no valor nominal  de R$ " + FormataReal(Req.ValorCota) + "(" + GetNumeroporExtenso(Req.ValorCota, true) + " ) cada uma, ";
                                IdentificaSocios += "correspondendo a R$ " + FormataReal(qtdQuotasCedidas) + "(" + GetNumeroporExtenso(qtdQuotasCedidas, true) + ").";
                            }
                        }
                        if (s.tipoacao == 1 && Req.AlteracaoDeEmpresa)
                        {
                            IdentificaSocios += " Incluído neste Ato";
                        }

                        //Inicio dados do Socio
                        wCnpj = s.CPFCNPJ;
                        if (wCnpj.Trim().Length == 11)
                        {
                            IdentificaSocios += "<br/>" + s.Qualificacao_Descricao + ",";
                            IdentificaSocios += " nacionalidade " + s.Nacionalidade.ToString().ToLower();

                            if (s.in_Sexo == "M")
                                IdentificaSocios += ", nascido em " + String.Format("{0:dd/MM/yyyy}", s.DataNascimento);
                            else
                                IdentificaSocios += ", nascida em " + String.Format("{0:dd/MM/yyyy}", s.DataNascimento);
                            if (!string.IsNullOrEmpty(s.TipoEmancipado.ToString()) && s.TipoEmancipado != "0")
                                if (s.in_Sexo == "M")
                                    IdentificaSocios += ", emancipado por " + s.TipoEmancipadoDS.ToLower();
                                else
                                    IdentificaSocios += ", emancipada por " + s.TipoEmancipadoDS.ToLower();
                            if (ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToUpper().Equals("CASADO"))
                                if (s.in_Sexo == "M")
                                    IdentificaSocios += ", " + ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToLower();
                                else
                                    IdentificaSocios += ", casada";
                            else
                                IdentificaSocios += ", " + VerificaGenero(ObterDescricaoEstadoCivil(s.EstadoCivil).ToString(), s.in_Sexo).ToLower();
                            if (ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToUpper().Equals("CASADO"))
                            {
                                IdentificaSocios += " em " + ObterDescricaoRegimeBens(s.EstadoCivilRegime).ToString().ToLower();
                            }

                            IdentificaSocios += ", " + s.Profissao_Descricao.ToString().ToLower();
                            wCnpjAux = string.Format("{0}.{1}.{2}-{3}", wCnpj.Substring(0, 3), wCnpj.Substring(3, 3), wCnpj.Substring(6, 3), wCnpj.Substring(9, 2));
                            IdentificaSocios += ", CPF/MF nº " + wCnpjAux;
                            IdentificaSocios += ", " + ObterDescricaoTipoDoc(s.TipoIdentidade).ToString().ToLower() + " nº " + s.RG.ToString();

                            IdentificaSocios += ", órgão expedidor " + s.OrgaoExpedidor.ToString().ToUpper();

                            if (s.OrgaoExpedidorUF.ToString() != "")
                            {
                                IdentificaSocios += " - " + s.OrgaoExpedidorUF.ToString().ToUpper();
                            }

                            if (s.NacionalidadeCodigo.ToString() != "154")
                            {
                                //IdentificaSocios += ", órgão expedidor " + s.OrgaoExpedidor.ToString().ToUpper();
                                if (s.tipo_visto != "" && s.tipo_visto != null)
                                {
                                    IdentificaSocios += ", Tipo de Visto " + s.tipo_visto.ToString().ToUpper();
                                    if (s.emissao_visto != null)
                                    {
                                        IdentificaSocios += ", Data de Emissão do Visto " + s.emissao_visto.Value.ToString("dd/MM/yyyy");
                                    }
                                    if (s.validade_visto != null)
                                    {
                                        IdentificaSocios += ", Data de Validade do Visto " + s.validade_visto.Value.ToString("dd/MM/yyyy");
                                    }
                                    if (s.Justificativa_Visto != "" && s.Justificativa_Visto != null)
                                    {
                                        IdentificaSocios += ", Justificativa (Visto) " + s.Justificativa_Visto;
                                    }
                                }

                            }
                            if (s.Email != null && s.Email != "")
                            {
                                IdentificaSocios += ", Email " + s.Email;
                            }
                            if (s.Telefone != null && s.Telefone != "")
                            {
                                IdentificaSocios += ", Telefone (" + s.DDD + ") " + s.Telefone;
                            }
                            if (s.Nome_Pai != null && s.Nome_Pai != "")
                            {
                                IdentificaSocios += ", Nome do Pai " + s.Nome_Pai;
                            }
                            if (s.Nome_Mae != null && s.Nome_Mae != "")
                            {
                                IdentificaSocios += ", Nome da Mãe " + s.Nome_Mae;
                            }

                        }
                        else
                            IdentificaSocios += " CNPJ " + wCnpj;

                        string tipoRuaAux = String.Empty;
                        string RuaAux = String.Empty;


                        if (s.EndDsTipoLogradouro != null)
                        {
                            tipoRuaAux = s.EndDsTipoLogradouro.ToString().ToUpper();
                        }
                        else
                        {
                            if (s.EndTipoLogradouro != null)
                            {
                                tipoRuaAux = s.EndTipoLogradouro.ToString().ToUpper();
                            }
                        }

                        RuaAux = NomeCaixaBaixa(RetiraComplementoLogradouro(s.EndLogradouro.ToString().ToUpper()));

                        if (wCnpj.Trim().Length == 11)
                            IdentificaSocios += ", residente e domiciliado: ";
                        else
                            IdentificaSocios += ", com sede: ";
                        if (s.EndPais.ToString().ToUpper() == "154")
                            IdentificaSocios += tipoRuaAux; //.ToUpper();
                        IdentificaSocios += " " + RuaAux; //.ToUpper();
                        if (!string.IsNullOrEmpty(s.EndNumero.ToString()))
                            IdentificaSocios += ", " + s.EndNumero.ToString().ToUpper();
                        if (!string.IsNullOrEmpty(s.EndComplemento.ToString().ToUpper()))
                            IdentificaSocios += ", " + NomeCaixaBaixa(s.EndComplemento.ToString().ToUpper());
                        IdentificaSocios += ", " + NomeCaixaBaixa(s.EndBairro.ToString().ToUpper());
                        if (s.EndPais.ToString().ToUpper() == "154")
                            IdentificaSocios += ", " + NomeCaixaBaixa(ObterNomeMunicipio(s.EndMunicipio.ToString()).ToUpper()) + ", " + s.EndUF.ToString().ToUpper() + ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(s.EndCEP.ToString()));
                        IdentificaSocios += ", " + NomeCaixaBaixa(ObterDescricaoPais(s.EndPais.ToString()));

                        if (wCnpj.Trim().Length == 11)
                        {
                            if (s.DataNascimento != null)
                            {
                                if (VerificaIdade(Convert.ToDateTime(s.DataNascimento.ToString())))
                                {
                                    if (s.RepresentantesAtivos.Count > 0)
                                    {
                                        string wSexoAux = "";
                                        int ii = 1;
                                        if (Menor16(Convert.ToDateTime(s.DataNascimento.ToString())))
                                        {
                                            if (s.in_Sexo == "M")
                                                IdentificaSocios += ", neste ato representado por ";
                                            else
                                                IdentificaSocios += ", neste ato representada por ";

                                        }
                                        else
                                            if (s.in_Sexo == "M")
                                                IdentificaSocios += ", neste ato assistido por ";
                                            else
                                                IdentificaSocios += ", neste ato assistida por ";

                                        foreach (bSocios Repre in s.RepresentantesAtivos)
                                        {
                                            //}
                                            //while (dtrRepre.Read())
                                            //{
                                            wSexoAux = Repre.in_Sexo.ToString();
                                            if (ii > 1)
                                                IdentificaSocios += " e por ";
                                            IdentificaSocios += " " + Repre.Nome.ToString().ToUpper() + ", "; ;
                                            ii = ii + 1;
                                            IdentificaSocios += Repre.Nacionalidade.ToString().ToLower() + "";
                                            if (Repre.in_Sexo.ToString() == "M")
                                                IdentificaSocios += ", nascido em ";
                                            else
                                                IdentificaSocios += ", nascida em ";

                                            IdentificaSocios += String.Format("{0:dd/MM/yyyy}", Repre.DataNascimento) + "";
                                            if (!string.IsNullOrEmpty(Repre.TipoEmancipado.ToString()))
                                                if (s.in_Sexo == "M")
                                                    IdentificaSocios += ", emancipado por " + Repre.TipoEmancipadoDS.ToLower();
                                                else
                                                    IdentificaSocios += ", emancipada por " + Repre.TipoEmancipadoDS.ToLower();
                                            IdentificaSocios += ", " + VerificaGenero(Repre.EstadoCivil.ToString(), Repre.in_Sexo).ToLower() + "";
                                            //if (Repre.EstadoCivil.ToString().ToUpper() == "CASADO" && Repre["ds_regime_bens"].ToString().ToUpper() != String.Empty)
                                            //{
                                            //    IdentificaSocios += " em " + dtrRepre["ds_regime_bens"].ToString().ToLower() + "";
                                            //}

                                            IdentificaSocios += ", " + Repre.Profissao_Descricao.ToString().ToLower() + "";
                                            wCnpj = Repre.CPFCNPJ.ToString();
                                            wCnpjAux = string.Format("{0}.{1}.{2}-{3}", wCnpj.Substring(0, 3), wCnpj.Substring(3, 3), wCnpj.Substring(6, 3), wCnpj.Substring(9, 2));
                                            IdentificaSocios += ", CPF/MF nº " + wCnpjAux + "";
                                            IdentificaSocios += ", " + ObterDescricaoTipoDoc(Repre.TipoIdentidade).ToString().ToLower() + "";
                                            IdentificaSocios += " nº " + Repre.RG.ToString().ToUpper() + "";
                                            IdentificaSocios += ", Órgão Expedidor " + Repre.OrgaoExpedidor.ToString().ToUpper() + "";
                                            if (!string.IsNullOrEmpty(Repre.OrgaoExpedidorUF.ToString()))
                                                IdentificaSocios += " - " + Repre.OrgaoExpedidorUF.ToString().ToUpper() + "";

                                            string tipoRuaAuxSocio = String.Empty;
                                            string RuaAuxSocio = String.Empty;

                                            tipoRuaAuxSocio = NomeCaixaBaixa(Repre.EndDsTipoLogradouro.ToString().ToUpper());
                                            RuaAuxSocio = NomeCaixaBaixa(RetiraComplementoLogradouro(Repre.EndLogradouro.ToString().ToUpper()));

                                            IdentificaSocios += ", Endereço: " + tipoRuaAuxSocio; //.ToUpper();
                                            IdentificaSocios += " " + RuaAuxSocio; //.ToUpper();
                                            if (!string.IsNullOrEmpty(Repre.EndNumero.ToString()))
                                                IdentificaSocios += ", " + Repre.EndNumero.ToString().ToUpper();
                                            if (!string.IsNullOrEmpty(Repre.EndComplemento.ToString().ToUpper()))
                                                IdentificaSocios += ", " + NomeCaixaBaixa(Repre.EndComplemento.ToString().ToUpper()) + "";
                                            IdentificaSocios += ", " + NomeCaixaBaixa(Repre.EndBairro.ToString().ToUpper()) + "";
                                            IdentificaSocios += ", " + NomeCaixaBaixa(ObterNomeMunicipio(Repre.EndMunicipio.ToString()).ToUpper()) + "";
                                            IdentificaSocios += ", " + Repre.EndUF.ToString().ToUpper() + "";
                                            IdentificaSocios += ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(Repre.EndCEP.ToString().ToUpper())) + " ";


                                        }
                                    }
                                }
                            }
                        }


                        if (s.RepresentantesAtivos.Count > 0)
                        {
                            string wSexoAux = "";
                            int ii = 1;

                            //if (s.in_Sexo == "M")
                            //IdentificaSocios += ", neste ato representado por seu procurador(a)";
                            IdentificaSocios += ", <br/>representante:";
                            //else
                            //    IdentificaSocios += ", neste ato representada por seu procurador(a) ";
                            foreach (bSocios Repre1 in s.Representantes)
                            {
                                wSexoAux = Repre1.in_Sexo.ToString();
                                if (ii > 1)
                                    IdentificaSocios += " e por ";
                                IdentificaSocios += " " + Repre1.Nome.ToString().ToUpper() + ", "; ;
                                ii = ii + 1;
                                IdentificaSocios += NomeCaixaBaixa(Repre1.Nacionalidade.ToString().ToUpper()) + "";
                                if (Repre1.in_Sexo.ToString() == "M")
                                    IdentificaSocios += ", nascido em ";
                                else

                                    IdentificaSocios += ", nascida em ";
                                IdentificaSocios += String.Format("{0:dd/MM/yyyy}", Repre1.DataNascimento) + "";

                                if (!string.IsNullOrEmpty(Repre1.TipoEmancipado.ToString()) && Repre1.TipoEmancipado.ToString() != "0")
                                {
                                    if (s.in_Sexo == "M")
                                    {
                                        IdentificaSocios += ", emancipado por " + Repre1.TipoEmancipado.ToString().ToLower();
                                    }
                                    else
                                    {
                                        IdentificaSocios += ", emancipada por " + Repre1.TipoEmancipado.ToString().ToLower();
                                    }
                                }
                                IdentificaSocios += ", " + VerificaGenero(ObterDescricaoEstadoCivil(Repre1.EstadoCivil).ToString(), Repre1.in_Sexo).ToLower() + "";
                                if (ObterDescricaoEstadoCivil(Repre1.EstadoCivil).ToString().ToUpper() == "CASADO" && ObterDescricaoRegimeBens(Repre1.EstadoCivilRegime).ToString().ToUpper() != String.Empty)
                                {
                                    IdentificaSocios += " em " + ObterDescricaoRegimeBens(Repre1.EstadoCivilRegime).ToString().ToLower() + "";
                                }

                                IdentificaSocios += ", " + Repre1.Profissao_Descricao.ToString().ToLower() + "";
                                //IdentificaSocios += ", CPF " + String.Format(@"{0:00\.000\.000\-00}", long.Parse(dtrRepre1["cpf_responsavel"].ToString().ToUpper())) + "";
                                wCnpj = Repre1.CPFCNPJ.ToString();
                                wCnpjAux = string.Format("{0}.{1}.{2}-{3}", wCnpj.Substring(0, 3), wCnpj.Substring(3, 3), wCnpj.Substring(6, 3), wCnpj.Substring(9, 2));
                                IdentificaSocios += ", CPF/MF nº " + wCnpjAux + "";
                                IdentificaSocios += ", " + ObterDescricaoTipoDoc(Repre1.TipoIdentidade).ToString().ToLower() + "";
                                IdentificaSocios += " nº " + Repre1.RG.ToString().ToUpper() + "";
                                IdentificaSocios += ", Órgão Expedidor " + Repre1.OrgaoExpedidor.ToString().ToUpper() + "";
                                if (!string.IsNullOrEmpty(Repre1.OrgaoExpedidorUF.ToString()))
                                {
                                    IdentificaSocios += " - " + Repre1.OrgaoExpedidorUF.ToString().ToUpper() + "";
                                }
                                if (Repre1.tipo_visto != "" && Repre1.tipo_visto != null)
                                {
                                    IdentificaSocios += ", Tipo de Visto " + Repre1.tipo_visto.ToString().ToUpper();
                                    if (Repre1.emissao_visto != null && Repre1.emissao_visto.ToString() != "")
                                    {
                                        IdentificaSocios += ", Data de Emissão do Visto " + Repre1.emissao_visto.Value.ToString("dd/MM/yyyy");
                                    }
                                    if (Repre1.validade_visto != null && Repre1.validade_visto.ToString() != "")
                                    {
                                        IdentificaSocios += ", Data de Validade do Visto " + Repre1.validade_visto.Value.ToString("dd/MM/yyyy");
                                    }
                                    if (Repre1.Justificativa_Visto != "" && Repre1.Justificativa_Visto != null)
                                    {
                                        IdentificaSocios += ", Justificativa (Visto) " + Repre1.Justificativa_Visto;
                                    }
                                }

                                if (Repre1.Email != null && Repre1.Email != "")
                                {
                                    IdentificaSocios += ", Email " + Repre1.Email;
                                }
                                if (Repre1.Telefone != null && Repre1.Telefone != "")
                                {
                                    IdentificaSocios += ", Telefone (" + Repre1.DDD + ") " + Repre1.Telefone;
                                }
                                if (Repre1.Nome_Pai != null && Repre1.Nome_Pai != "")
                                {
                                    IdentificaSocios += ", Nome do Pai " + Repre1.Nome_Pai;
                                }
                                if (Repre1.Nome_Mae != null && Repre1.Nome_Mae != "")
                                {
                                    IdentificaSocios += ", Nome da Mãe " + Repre1.Nome_Mae;
                                }


                                string tipoRuaAuxSocio = String.Empty;
                                string RuaAuxSocio = String.Empty;

                                tipoRuaAuxSocio = NomeCaixaBaixa(Repre1.EndDsTipoLogradouro.ToString().ToUpper());
                                RuaAuxSocio = NomeCaixaBaixa(RetiraComplementoLogradouro(Repre1.EndLogradouro.ToString().ToUpper()));


                                //IdentificaSocios += ", residente e domiciliado(a) no(a) " + tipoRuaAuxSocio; //.ToUpper();
                                IdentificaSocios += ", <br/>Endereço: " + tipoRuaAuxSocio;
                                IdentificaSocios += " " + RuaAuxSocio; //.ToUpper();
                                if (!string.IsNullOrEmpty(Repre1.EndNumero.ToString()))
                                    IdentificaSocios += ", " + Repre1.EndNumero.ToString().ToUpper();
                                if (!string.IsNullOrEmpty(Repre1.EndComplemento.ToString().ToUpper()))
                                    IdentificaSocios += ", " + NomeCaixaBaixa(Repre1.EndComplemento.ToString().ToUpper()) + "";
                                IdentificaSocios += ", " + NomeCaixaBaixa(Repre1.EndBairro.ToString().ToUpper()) + "";
                                IdentificaSocios += ", " + NomeCaixaBaixa(ObterNomeMunicipio(Repre1.EndMunicipio.ToString()).ToUpper()) + "";
                                IdentificaSocios += ", " + Repre1.EndUF.ToString().ToUpper() + "";
                                IdentificaSocios += ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(Repre1.EndCEP.ToString().ToUpper())) + "";

                            }
                        }




                        //Fim dados do Socio
                        CelulaSocio.Text += IdentificaSocios + "</br>";
                        CelulaSocio.Attributes.Add("style", _bordas);
                        LinhaSocio.Cells.Add(CelulaSocio);


                        t.Rows.Add(LinhaSocio);
                        LinhaSocio = new TableRow();
                        indice++;
                    }
                }
            }
            #endregion

            pnQSA.Controls.Add(t);
            pnQSA.DataBind();

        }


        public void AddSociosQSA_JUCERJA()
        {
            #region Socios
            if (Req.Socios.Count > 0)
            {

                Table t = new Table();
                TableRow LinhaSocio;
                TableCell CelulaSocio;
                String IdentificaSocios = "";
                String wCnpj = "";
                string wCnpjAux = "";
                int indice = 0;
                string bordas = "border-bottom:thin solid gray;border-left:thin solid gray;border-right:thin solid gray;border-top:thin solid gray;";

                pnQSA.Controls.Clear();

                #region Cria HTML Table QSAtable
                t.Attributes.Add("runat", "server");
                t.ID = "QSAtable";
                t.CellSpacing = 5;
                t.Width = System.Web.UI.WebControls.Unit.Percentage(100);


                #endregion


                LinhaSocio = new TableRow();
                CelulaSocio = new TableCell();


                //CelulaSocio.Attributes.Add("style", "background-color:#5080a2;color:White; font-weight:normal;align='center'");
                //CelulaSocio.Text = "Confirma?&nbsp&nbsp";
                //LinhaSocio.Cells.Add(CelulaSocio);
                CelulaSocio = new TableCell();
                CelulaSocio.Attributes.Add("style", "background-color:#5080a2;color:White; font-weight:normal;align='center'");
                CelulaSocio.Text = "Empresário, Titular ou Sócios e/ou admininstradores.";
                LinhaSocio.Cells.Add(CelulaSocio);
                LinhaSocio.Width = 800;
                t.Rows.Add(LinhaSocio);
                LinhaSocio = new TableRow();


                pnQSA.Visible = true;
                string _bordas = bordas;
                List<bSocios> _socioAtivos = Req.Socios;

                foreach (bSocios s in _socioAtivos)
                {
                    #region Socios
                    IdentificaSocios = "";

                    CelulaSocio = new TableCell();
                    CelulaSocio.Attributes.Add("class", "font1");

                    CelulaSocio = new TableCell();
                    CelulaSocio.Attributes.Add("class", "fontReq2E");

                    CelulaSocio.Text = Convert.ToString(indice + 1) + ". " + NomeProprio(s.Nome).ToUpper();
                    if (s.tipoacao == 5)
                    {
                        IdentificaSocios += "<br/>RETIRA-SE DA SOCIEDADE O SÓCIO.";
                    }
                    if (s.tipoacao == 1)
                    {
                        IdentificaSocios += "<BR/> INCLUÍDO NESTE ATO";
                    }
                    if (s.tipoacao == 0 || s.tipoacao == 3)
                    {
                        IdentificaSocios += "<BR/> SÓCIO ATUAL";
                    }
                    //Inicio dados do Socio

                    if (s.Qualificacao != null && s.Qualificacao != "")
                    {
                        DataTable dtCondicao = dHelperQuery.getCondicaoDNRC(decimal.Parse(s.Qualificacao));
                        if (dtCondicao.Rows.Count > 0)
                        {
                            s.Qualificacao_Descricao = dtCondicao.Rows[0]["A009_DS_CONDICAO"].ToString();
                        }
                        else
                        {
                            s.Qualificacao_Descricao = "Condicao nao encontrada na tabela " + s.Qualificacao;
                        }
                    }

                    if (s.Qualificacao_Descricao != null && s.Qualificacao_Descricao != "")
                    {
                        IdentificaSocios += "<BR/>Qualificação: " + s.Qualificacao_Descricao.ToUpper() + "";
                    }
                    if (s.Nacionalidade != null)
                    {
                        IdentificaSocios += "<BR/>Nacionalidade: " + s.Nacionalidade.ToString().ToUpper();
                    }
                    if (s.DataNascimento != null)
                    {
                        IdentificaSocios += "<BR/>Nascimento: " + String.Format("{0:dd/MM/yyyy}", s.DataNascimento);
                    }
                    if (s.EstadoCivil != null)
                    {
                        IdentificaSocios += "<BR/>Estado Civil: ";
                        if (ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToUpper().Equals("CASADO"))
                        {
                            if (s.in_Sexo == "M")
                                IdentificaSocios += ObterDescricaoEstadoCivil(s.EstadoCivil).ToString().ToUpper();
                            else
                                IdentificaSocios += "CASADA";
                        }
                    }

                    if (s.Profissao_Descricao != null)
                    {
                        IdentificaSocios += "<BR/>Profissão: " + s.Profissao_Descricao.ToString().ToUpper();
                    }

                    wCnpj = s.CPFCNPJ;
                    if (wCnpj.Trim().Length == 14)
                    {
                        IdentificaSocios += "<BR/>CNPJ: " + wCnpj;
                        if (s.Nire != null && s.Nire != "")
                        {
                            IdentificaSocios += "<BR/>Numero Orgão Registro: " + s.Nire;
                        }
                    }
                    else
                    {
                        wCnpjAux = string.Format("{0}.{1}.{2}-{3}", wCnpj.Substring(0, 3), wCnpj.Substring(3, 3), wCnpj.Substring(6, 3), wCnpj.Substring(9, 2));
                        IdentificaSocios += "<BR/>CPF: " + wCnpjAux;

                    }

                    if (s.Nome_Pai != null && s.Nome_Pai != "")
                    {
                        IdentificaSocios += "<BR/>Nome do Pai: " + s.Nome_Pai.ToUpper();
                    }
                    if (s.Nome_Mae != null && s.Nome_Mae != "")
                    {
                        IdentificaSocios += "<BR/>Nome da Mãe: " + s.Nome_Mae.ToUpper();
                    }



                    string tipoRuaAux = String.Empty;
                    string RuaAux = String.Empty;
                    string endereco = "";
                    tipoRuaAux = "";
                    if (s.EndDsTipoLogradouro != null)
                    {
                        tipoRuaAux = s.EndDsTipoLogradouro.ToString().ToUpper();
                    }
                    else
                    {
                        if (s.EndTipoLogradouro != null)
                        {
                            tipoRuaAux = s.EndTipoLogradouro.ToString().ToUpper();
                        }
                    }
                    RuaAux = "";
                    if (s.EndLogradouro != null && s.EndLogradouro.ToString() != "")
                    {
                        RuaAux = RetiraComplementoLogradouro(s.EndLogradouro.ToString().ToUpper());
                    }

                    //IdentificaSocios += "<BR/>Endereço: ";

                    if (tipoRuaAux != "")
                        endereco += "" + tipoRuaAux.ToUpper();


                    if (RuaAux != "")
                        endereco += " " + RuaAux.ToUpper();

                    if (s.EndNumero != null && s.EndNumero != "")
                        endereco += ", " + s.EndNumero.ToString().ToUpper();

                    if (s.EndComplemento != null && s.EndComplemento != "")
                        endereco += ", " + s.EndComplemento.ToString().ToUpper();

                    if (s.EndBairro != null && s.EndBairro != "")
                        endereco += ", " + s.EndBairro.ToString().ToUpper();

                    if (s.EndMunicipio != null && s.EndMunicipio != "0")
                        endereco += ", " + ObterNomeMunicipio(s.EndMunicipio.ToString()).ToUpper();

                    if (s.EndUF != null && s.EndUF != "")
                        endereco += ", " + s.EndUF.ToString().ToUpper();

                    if (s.EndCEP != null && s.EndCEP != "")
                        endereco += ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(s.EndCEP.ToString()));

                    if (s.EndPais != null && s.EndPais != "")
                        endereco += ", " + ObterDescricaoPais(s.EndPais.ToString()).ToUpper() + "</br>";

                    if (endereco != null && endereco.Trim() != "")
                    {
                        IdentificaSocios += "<BR/>Endereço: ";
                        IdentificaSocios += endereco;
                    }


                    if (s.CapitalIntegralizado > 0)
                    {
                        IdentificaSocios += "<BR/>Capital: " + "R$ " + s.CapitalIntegralizado.ToString("#,0.00");
                    }

                    if (s.PercentualCapital > 0)
                    {
                        IdentificaSocios += "<BR/>Percentual: " + s.PercentualCapital.ToString("#,0.00");
                    }

                    #endregion

                    #region Representante Socio
                    if (s.RepresentantesAtivos.Count > 0)
                    {
                        int ii = 1;

                        //IdentificaSocios += ", <b><br/><br/>Representante:</b>";
                        foreach (bSocios Repre1 in s.Representantes)
                        {
                            if (Repre1.Nome != null && Repre1.Nome.ToString() != "")
                            {
                                IdentificaSocios += "<br/> <br/>Representante:";

                                IdentificaSocios += " " + Repre1.Nome.ToString().ToUpper(); ;

                                if (Repre1.Qualificacao != null && Repre1.Qualificacao != "")
                                {
                                    DataTable dtCondicao = dHelperQuery.getCondicaoDNRC(decimal.Parse(Repre1.Qualificacao));
                                    if (dtCondicao.Rows.Count > 0)
                                    {
                                        Repre1.Qualificacao_Descricao = dtCondicao.Rows[0]["A009_DS_CONDICAO"].ToString();
                                    }
                                    else
                                    {
                                        Repre1.Qualificacao_Descricao = "Condicao nao encontrada na tabela " + s.Qualificacao;
                                    }
                                }
                                if (Repre1.Qualificacao_Descricao != null)
                                {
                                    IdentificaSocios += "<BR/>Tipo: " + Repre1.Qualificacao_Descricao;
                                }
                                ii = ii + 1;

                                if (Repre1.CPFCNPJ != null && Repre1.CPFCNPJ != "")
                                {

                                    wCnpj = Repre1.CPFCNPJ.ToString();
                                    wCnpjAux = string.Format("{0}.{1}.{2}-{3}", wCnpj.Substring(0, 3), wCnpj.Substring(3, 3), wCnpj.Substring(6, 3), wCnpj.Substring(9, 2));
                                    IdentificaSocios += "<BR/>CPF: " + wCnpjAux + "";
                                    if (Repre1.Nome_Pai != null && Repre1.Nome_Pai != "")
                                    {
                                        IdentificaSocios += "<BR/>Nome do Pai: " + Repre1.Nome_Pai;
                                    }
                                    if (Repre1.Nome_Mae != null && Repre1.Nome_Mae != "")
                                    {
                                        IdentificaSocios += "<BR/>Nome da Mãe: " + Repre1.Nome_Mae;
                                    }


                                    endereco = "";
                                    if (Repre1.EndDsTipoLogradouro != null)
                                    {
                                        tipoRuaAux = Repre1.EndDsTipoLogradouro.ToString().ToUpper();
                                    }
                                    else
                                    {
                                        if (Repre1.EndTipoLogradouro != null)
                                        {
                                            tipoRuaAux = Repre1.EndTipoLogradouro.ToString().ToUpper();
                                        }
                                    }

                                    if (Repre1.EndLogradouro != null)
                                    {
                                        RuaAux = RetiraComplementoLogradouro(Repre1.EndLogradouro.ToString().ToUpper());
                                    }

                                    //IdentificaSocios += "<BR/>Endereço: ";

                                    if (tipoRuaAux != "")
                                        endereco += "" + tipoRuaAux.ToUpper();

                                    if (RuaAux != "")
                                        endereco += " " + RuaAux.ToUpper();


                                    if (Repre1.EndNumero != null && Repre1.EndNumero != "")
                                        endereco += ", " + Repre1.EndNumero.ToString().ToUpper();

                                    if (Repre1.EndComplemento != null && Repre1.EndComplemento != "")
                                        endereco += ", " + Repre1.EndComplemento.ToString().ToUpper();

                                    if (Repre1.EndBairro != null && Repre1.EndBairro != "")
                                        endereco += ", " + Repre1.EndBairro.ToString().ToUpper();

                                    if (Repre1.EndMunicipio != null && Repre1.EndMunicipio != "0")
                                        endereco += ", " + ObterNomeMunicipio(Repre1.EndMunicipio.ToString()).ToUpper();

                                    if (Repre1.EndUF != null && Repre1.EndUF != "")
                                        endereco += ", " + Repre1.EndUF.ToString().ToUpper();

                                    if (Repre1.EndCEP != null && Repre1.EndCEP != "")
                                        endereco += ", CEP " + String.Format(@"{0:00\.000\-000}", long.Parse(Repre1.EndCEP.ToString()));

                                    if (Repre1.EndPais != null && Repre1.EndPais != "")
                                        endereco += ", " + ObterDescricaoPais(Repre1.EndPais.ToString()).ToUpper() + "</br>";

                                    if (endereco.Trim() != "")
                                    {
                                        IdentificaSocios += "<BR/>Endereço: ";
                                        IdentificaSocios += endereco;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    //Fim dados do Socio
                    CelulaSocio.Text += IdentificaSocios + "</br>";
                    CelulaSocio.Attributes.Add("style", _bordas + ";width=87%");
                    LinhaSocio.Cells.Add(CelulaSocio);
                    t.Rows.Add(LinhaSocio);
                    LinhaSocio = new TableRow();
                    indice++;

                }
                pnQSA.Controls.Add(t);
                pnQSA.DataBind();
                fdsQSA.Visible = true;
                pnQSA.Visible = true;
            }
            else
            {
                fdsQSA.Visible = false;
                pnQSA.Visible = false;
            }
            #endregion


        }


        public void AddCNAES()
        {


            lblOBJSocial.Text = Req.ObjetoSocial;

            HtmlTableRow Linha;
            HtmlTableCell Celula;
            string bordas = "border-bottom:thin solid gray;border-left:thin solid gray;border-right:thin solid gray;border-top:thin solid gray;";

            CNAEtable.Controls.Clear();

            CNAEtable.Attributes.Add("style", "witdh:100%;align:center");

            #region cabeçalho Grid

            Linha = new HtmlTableRow();
            Celula = new HtmlTableCell();
            Linha.Attributes.Add("class", "font1");
            Linha.Attributes.Add("style", "background-color:#5080a2;color:White;");

            Celula.InnerHtml = "Código";
            Celula.Attributes.Add("align", "center");
            Linha.Cells.Add(Celula);


            Celula = new HtmlTableCell();
            Celula.InnerHtml = "Tipo";
            Celula.Attributes.Add("align", "center");
            Linha.Cells.Add(Celula);

            Celula = new HtmlTableCell();
            Celula.InnerHtml = "Descrição";
            Celula.Attributes.Add("align", "center");
            Linha.Cells.Add(Celula);


            CNAEtable.Rows.Add(Linha);

            #endregion

            fdsObjeto.Visible = false;

            foreach (bCNAE c in Req.CNAEs)
            {
                fdsObjeto.Visible = true;
                Linha = new HtmlTableRow();
                Celula = new HtmlTableCell();
                Linha.Attributes.Add("class", "fontReq2E");

                Celula.InnerHtml = c.CodigoCNAE;
                Celula.Attributes.Add("style", bordas);
                Linha.Cells.Add(Celula);


                Celula = new HtmlTableCell();
                Celula.InnerHtml = c.TipoAtividadeDescricao;
                Celula.Attributes.Add("style", bordas);
                Linha.Cells.Add(Celula);

                Celula = new HtmlTableCell();
                Celula.InnerHtml = c.Descricao;
                Celula.Attributes.Add("style", bordas);
                Linha.Cells.Add(Celula);

                CNAEtable.Rows.Add(Linha);
            }

        }


        public string DadosFilial(int sqFilial)
        {
            string DadosFilial = "";

            foreach (bFilial f in Req.Filiais)
            {
                if (f.SqFilial == sqFilial)
                {
                    DadosFilial = "";

                    string tipoRuaAux = String.Empty;
                    string RuaAux = String.Empty;

                    if (f.FilialTipoLogradouro != null)
                    {
                        tipoRuaAux = NomeCaixaBaixa(f.FilialTipoLogradouro.ToString().ToUpper());
                    }
                    RuaAux = NomeCaixaBaixa(RetiraComplementoLogradouro(f.FilialLogradouro.ToString().ToUpper()));
                    //DadosFilial += "Filial " + (indice + 1) + " <br/>Endereço: ";
                    DadosFilial += tipoRuaAux;
                    DadosFilial += " " + RuaAux;
                    if (!string.IsNullOrEmpty(f.FilialNumero.ToString()))
                        DadosFilial += ", " + f.FilialNumero.ToString().ToUpper();
                    if (f.Nire.Trim() != "")
                        DadosFilial += " : NIRE=" + f.Nire;
                    return DadosFilial;
                }
            }
            return DadosFilial;
        }

        #endregion

        #region MontaDadosXML

        private string PreparaXMLParaGravarOR(bRequerimento req)
        {
            StringBuilder XmlOut = new StringBuilder();
            XmlOut.AppendLine("<coleta>");
            XmlOut.AppendLine(AddEmpresaXML(req) + AddCNAESXml(req) + AddSociosQSAXml(req));

            XmlOut.Append(AddConadorXML(req));




            XmlOut.AppendLine("</coleta>");

            return XmlOut.ToString();
        }

        private string AddEmpresaXML(bRequerimento req)
        {
            #region Eventos
            StringBuilder XmlEventos = new StringBuilder();
            XmlEventos.AppendLine("<GrupoEvento>");
            foreach (bProtocoloEvento pe in req.ProtocoloEvento)
            {
                XmlEventos.AppendLine("<evento>" + pe.CodigoEvento + "</evento>");
            }

            XmlEventos.AppendLine("</GrupoEvento>");

            #endregion

            #region Identificação da empresa
            StringBuilder XmlIdentEmpresa = new StringBuilder();
            XmlIdentEmpresa.AppendLine("<IdentificacaoEmpresa>");

            XmlIdentEmpresa.AppendLine("<NireMatricula>" + req.nrMatricula + "</NireMatricula>");
            XmlIdentEmpresa.AppendLine("<nomeEmpresarial><![CDATA[" + req.SedeNome + "]]></nomeEmpresarial>");


            XmlIdentEmpresa.AppendLine("<nomeFantasia>" + req.Nome_Fantasia + "</nomeFantasia>");
            XmlIdentEmpresa.AppendLine("<naturezaJuridica>" + req.NaturezaJuridicaCodigo + "</naturezaJuridica>");
            XmlIdentEmpresa.AppendLine("<dataInicioAtividade>" + req.DataInicioSociedade + "</dataInicioAtividade>");
            XmlIdentEmpresa.AppendLine("<dataAssinatura>" + req.Data_Assinatura + "</dataAssinatura>");
            XmlIdentEmpresa.AppendLine("<capitalSocial>" + (long)(req.CapitalSocial * 100) + "</capitalSocial>");
            XmlIdentEmpresa.AppendLine("<indicaIntegralizaCapital>2</indicaIntegralizaCapital>");
            XmlIdentEmpresa.AppendLine("<capitalIntegralizado>000</capitalIntegralizado>");
            XmlIdentEmpresa.AppendLine("<porte>" + req.Porte + "</porte>");
            XmlIdentEmpresa.AppendLine("<ddd>" + req.SedeDDD + "</ddd>");
            XmlIdentEmpresa.AppendLine("<telefone1>" + req.SedeTelefone + "</telefone1>");
            XmlIdentEmpresa.AppendLine("<telefone2></telefone2>");
            XmlIdentEmpresa.AppendLine("<email>" + req.SedeEmail + "</email>");

            XmlIdentEmpresa.AppendLine("</IdentificacaoEmpresa>");
            #endregion

            #region Endereço da empresa

            StringBuilder XmlEnderecoEmpresa = new StringBuilder();
            XmlEnderecoEmpresa.AppendLine("<EnderecoEmpresa>");

            XmlEnderecoEmpresa.AppendLine("<tipoLogradouro>" + req.SedeDsTipoLogradouro + "</tipoLogradouro>");
            XmlEnderecoEmpresa.AppendLine("<logradouro>" + req.SedeLogradouro + "</logradouro>");
            XmlEnderecoEmpresa.AppendLine("<numero>" + req.SedeNumero + "</numero>");
            XmlEnderecoEmpresa.AppendLine("<complemento>" + req.SedeComplemento + "</complemento>");
            XmlEnderecoEmpresa.AppendLine("<bairro>" + req.SedeBairro + "</bairro>");
            XmlEnderecoEmpresa.AppendLine("<cep>" + req.SedeCEP + "</cep>");
            XmlEnderecoEmpresa.AppendLine("<CodMunicipio>" + req.SedeMunicipio + "</CodMunicipio>");
            XmlEnderecoEmpresa.AppendLine("<uf>" + req.SedeUF + "</uf>");
            XmlEnderecoEmpresa.AppendLine("<codPais>105</codPais>");

            XmlEnderecoEmpresa.AppendLine("</EnderecoEmpresa>");
            #endregion

            return XmlEventos.ToString() + XmlIdentEmpresa.ToString() + XmlEnderecoEmpresa.ToString();
        }


        public string AddCNAESXml(bRequerimento req)
        {
            StringBuilder XmlAtuacao = new StringBuilder();
            XmlAtuacao.AppendLine("<AtuacaoEmpresa>");

            XmlAtuacao.AppendLine("<objetoSocial><![CDATA[" + req.ObjetoSocial + "]]></objetoSocial>");

            XmlAtuacao.Append("<cnaePrincipal><![CDATA[");
            foreach (bCNAE c in req.CNAEs)
            {
                if (c.TipoAtividade == 36)
                {
                    XmlAtuacao.Append(c.CodigoCNAE);
                    break;
                }
            }
            XmlAtuacao.Append("]]></cnaePrincipal>");

            XmlAtuacao.AppendLine("<cnaeSecundaria>");
            foreach (bCNAE c in req.CNAEs)
            {
                if (c.TipoAtividade == 37)
                {
                    XmlAtuacao.AppendLine("<valor><![CDATA[" + c.CodigoCNAE + "]]></valor>");
                    break;
                }
            }
            XmlAtuacao.AppendLine("</cnaeSecundaria>");


            XmlAtuacao.AppendLine("</AtuacaoEmpresa>");

            return XmlAtuacao.ToString();

        }

        private string AddSociosQSAXml(bRequerimento req)
        {

            StringBuilder XmlQsa = new StringBuilder();
            XmlQsa.AppendLine("<GrupoQSA>");

            if (req.SociosAtivos.Count > 0)
            {
                List<bSocios> _socioAtivos = req.SociosAtivos;
                foreach (bSocios s in _socioAtivos)
                {
                    if (s.Qualificacao == "49")
                    {
                        XmlQsa.AppendLine(XmlQsaaQsa(s, "22"));
                        XmlQsa.AppendLine(XmlQsaaQsa(s, "5"));
                    }
                    else
                    {
                        XmlQsa.AppendLine(XmlQsaaQsa(s, s.Qualificacao));
                    }


                }

            }

            XmlQsa.AppendLine("</GrupoQSA>");

            return XmlQsa.ToString();
        }

        private string XmlQsaaQsa(bSocios s, string Qualificacao)
        {
            StringBuilder XmlQsa = new StringBuilder();
            XmlQsa.AppendLine("<socioFundadorDiretor>");

            XmlQsa.AppendLine("<evento><![CDATA[" + s.EventoDBE + "]]></evento>");

            XmlQsa.AppendLine("<dataEvento><![CDATA[" + DateTime.Now.ToString("yyyyMMdd") + "]]></dataEvento>");
            XmlQsa.AppendLine("<nome><![CDATA[" + s.Nome + "]]></nome>");
            XmlQsa.AppendLine("<cpfCnpj><![CDATA[" + s.CPFCNPJ + "]]></cpfCnpj>");
            XmlQsa.AppendLine("<nire><![CDATA[" + s.Nire + "]]></nire>");
            XmlQsa.AppendLine("<qualificacao><![CDATA[" + Qualificacao + "]]></qualificacao>");
            XmlQsa.AppendLine("<vlParticipacao><![CDATA[" + (long)(s.CapitalIntegralizado * 100) + "]]></vlParticipacao>");
            XmlQsa.AppendLine("<tipoLogradouro><![CDATA[" + s.EndTipoLogradouro + "]]></tipoLogradouro>");
            XmlQsa.AppendLine("<logradouro><![CDATA[" + s.EndLogradouro + "]]></logradouro>");
            XmlQsa.AppendLine("<numero><![CDATA[" + s.EndNumero + "]]></numero>");
            XmlQsa.AppendLine("<complemento><![CDATA[" + s.EndComplemento + "]]></complemento>");
            XmlQsa.AppendLine("<bairro><![CDATA[" + s.EndBairro + "]]></bairro>");
            XmlQsa.AppendLine("<cep><![CDATA[" + s.EventoDBE + "]]></cep>");
            XmlQsa.AppendLine("<CodMunicipio><![CDATA[" + s.EndMunicipio + "]]></CodMunicipio>");
            XmlQsa.AppendLine("<UF><![CDATA[" + s.EndUF + "]]></UF>");
            XmlQsa.AppendLine("<codPais><![CDATA[" + s.EndPais + "]]></codPais>");
            XmlQsa.AppendLine("<ddd><![CDATA[" + s.DDD + "]]></ddd>");
            XmlQsa.AppendLine("<telefone1><![CDATA[" + s.Telefone + "]]></telefone1>");
            XmlQsa.AppendLine("<telefone2><![CDATA[]]></telefone2>");
            XmlQsa.AppendLine("<email><![CDATA[" + s.Email + "]]></email>");
            XmlQsa.AppendLine("<flagAdministrador></flagAdministrador>");
            XmlQsa.AppendLine("<gerenteUsoFirma></gerenteUsoFirma>");
            XmlQsa.AppendLine("<Firmantes></Firmantes>");
            XmlQsa.AppendLine("<UF><![CDATA[" + s.EndUF + "]]></UF>");

            XmlQsa.AppendLine("<dadosPessoaFisica>");
            XmlQsa.AppendLine(" <nomeMae><![CDATA[" + s.Nome_Mae + "]]></nomeMae>");
            XmlQsa.AppendLine(" <nomePai><![CDATA[" + s.Nome_Pai + "]]></nomePai>");
            XmlQsa.AppendLine(" <dtNascimento></dtNascimento>");
            //XmlQsa.AppendLine(" <dtNascimento><![CDATA[" + s.DataNascimento.ToString("yyyyMMdd") + "]]></dtNascimento>");
            XmlQsa.AppendLine(" <nrDocumento><![CDATA[" + s.RG + "]]></nrDocumento>");
            XmlQsa.AppendLine(" <descricaoOrgaoEmissor><![CDATA[" + s.OrgaoExpedidorNome + "]]></descricaoOrgaoEmissor>");
            XmlQsa.AppendLine(" <ufOrgaoEmissor><![CDATA[" + s.OrgaoExpedidorUF + "]]></ufOrgaoEmissor>");
            XmlQsa.AppendLine(" <dtEmissaoDocumento></dtEmissaoDocumento>");
            XmlQsa.AppendLine(" <dtVencimentoDocumento></dtVencimentoDocumento>");
            XmlQsa.AppendLine(" <codigoPais><![CDATA[" + s.EndPais + "]]></codigoPais>");
            XmlQsa.AppendLine(" <sexo></sexo>");
            XmlQsa.AppendLine(" <UfNaturalidade></UfNaturalidade>");

            XmlQsa.AppendLine(" <estadoCivil></estadoCivil>");
            XmlQsa.AppendLine(" <regimeBens></regimeBens>");
            XmlQsa.AppendLine(" <emancipacao></emancipacao>");
            XmlQsa.AppendLine(" <descricaoProfissao></descricaoProfissao>");


            XmlQsa.AppendLine("</dadosPessoaFisica>");

            XmlQsa.AppendLine("</socioFundadorDiretor>");

            XmlQsa.AppendLine("<GrupoRepresentante/>");

            return XmlQsa.ToString();

        }

        private string AddConadorXML(bRequerimento req)
        {
            StringBuilder XmlContador = new StringBuilder();
            XmlContador.AppendLine("<Contador>");
            XmlContador.AppendLine("<cpfCnpjContab>" + req.Contabilista.cpfCnpj + "</cpfCnpjContab>");
            XmlContador.AppendLine("<nomeEmpresarialContab><![CDATA[" + req.Contabilista.ds_Pessoa + "]]></nomeEmpresarialContab>");

            if (req.Contabilista.cpf_resp != "")
            {
                XmlContador.AppendLine("<classifCrcRespContab>" + req.Contabilista.tip_Class_Resp + "</classifCrcRespContab>");
                XmlContador.AppendLine("<cpfRespContab>" + req.Contabilista.cpf_resp + "</cpfRespContab>");
                XmlContador.AppendLine("<sequencialCrcRespContab>" + req.Contabilista.co_CRC_Resp + "</sequencialCrcRespContab>");
                XmlContador.AppendLine("<ufCrcRespContab>" + req.Contabilista.uf_CRC_Resp + "</ufCrcRespContab>");
                XmlContador.AppendLine("<tipoCrcRespContab>" + req.Contabilista.tip_CRC_Resp + "</tipoCrcRespContab>");


                XmlContador.AppendLine("<sequencialCrcContab>" + req.Contabilista.co_CRC_Empresa + "</sequencialCrcContab>");
                XmlContador.AppendLine("<ufCrcContab>" + req.Contabilista.uf_CRC_Empresa + "</ufCrcContab>");
                XmlContador.AppendLine("<classifCrcContab>" + req.Contabilista.tip_Class_Empresa + "</classifCrcContab>");
                XmlContador.AppendLine("<tipoCrcContab>" + req.Contabilista.tip_CRC_Empresa + "</tipoCrcContab>");
            }
            else
            {
                XmlContador.AppendLine("<sequencialCrcContab>" + req.Contabilista.co_CRC_Resp + "</sequencialCrcContab>");
                XmlContador.AppendLine("<ufCrcContab>" + req.Contabilista.uf_CRC_Resp + "</ufCrcContab>");
                XmlContador.AppendLine("<classifCrcContab>" + req.Contabilista.tip_Class_Resp + "</classifCrcContab>");
                XmlContador.AppendLine("<tipoCrcContab>" + req.Contabilista.tip_CRC_Resp + "</tipoCrcContab>");
            }



            XmlContador.AppendLine("</Contador>");

            return XmlContador.ToString();

        }

        #endregion

        #region Formatação
        private string GetNumeroporExtenso(decimal wNumero, bool comReal)
        {
            string ret = "";
            if (comReal)
            {
                NumeroExtenso.Extenso NE = new NumeroExtenso.Extenso();
                ret = NE.NumeroToExtenso(wNumero).ToString().ToUpper();
            }
            else
            {
                ret = new NumeroMyExtensoSemReal.MyExtensoSemReal().MyExtenso_Valor(wNumero);
            }

            return ret;
        }

        private string FormataValor(decimal wValor)
        {
            return String.Format("{0:0,0}", wValor);
        }

        private string FormataReal(decimal wValor)
        {
            return String.Format("{0:0,0.00}", wValor);
        }

        private string NomeProprio(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value.Length == 0) return value;
            value = value.ToLower();
            System.Text.StringBuilder result = new System.Text.StringBuilder(value);
            result[0] = char.ToUpper(result[0]);

            for (int i = 1; i < result.Length; ++i)
            {

                if (char.IsWhiteSpace(result[i - 1]))
                {
                    result[i] = char.ToUpper(result[i]);
                }
            }
            return result.ToString();
        }

        private string NomeCaixaBaixa(string value)
        {
            string Retorno = string.Empty;
            if (string.IsNullOrEmpty(value))
                return "";
            string[] wValue = value.Split(' ');
            for (int i = 0; i < wValue.Length; i++)
            {
                string aux = wValue[i].ToLower();
                if (aux.Length <= 3)
                {
                    if (aux == "da" || aux == "de" || aux == "do" || aux == "das" || aux == "dos" || aux == "e")
                    {
                        if (string.IsNullOrEmpty(Retorno))
                            Retorno = aux;
                        else
                            Retorno += " " + aux;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Retorno))
                            Retorno = NomeProprio(aux);
                        else
                            Retorno += " " + NomeProprio(aux);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Retorno))
                        Retorno = NomeProprio(aux);
                    else
                        Retorno += " " + NomeProprio(aux);
                }
            }
            return Retorno;

        }

        private string VerificaGenero(string texto, string wGenero)
        {
            string wTexto = "";
            int _tamTexto = 0;
            int _pos = 0;
            if (texto.Trim() != "")
            {
                _tamTexto = texto.Length;
                _pos = texto.IndexOf(" ", 0);

                if (_pos > 0)
                {
                    if (wGenero == "M")
                        wTexto = texto.Substring(0, _pos - 1) + "O" + texto.Substring(_pos, texto.Length - _pos);
                    else
                        wTexto = texto.Substring(0, _pos - 1) + "A" + texto.Substring(_pos, texto.Length - _pos);
                }
                else
                {
                    if (wGenero == "M")
                        wTexto = texto.Substring(0, texto.Length - 1) + "O";
                    else
                        wTexto = texto.Substring(0, texto.Length - 1) + "A";
                }

            }
            return wTexto.ToUpper();
        }

        public static string TrimAll(string s)
        {
            s = s.Trim();
            while (s.IndexOf("\r\n\r\n") != -1)
                s = s.Replace("\r\n\r\n", "\r\n");


            while (s.IndexOf("  ") != -1)
                s = s.Replace("  ", " ");
            return s;


        }

        private Boolean VerificaIdade(DateTime wData)
        {

            DateTime dataNascimento = new DateTime();
            dataNascimento = wData;

            TimeSpan diff;

            // Usando Subtract
            diff = DateTime.Now.Subtract(dataNascimento);

            if ((diff.Days / 365) < 18)
                return true;
            else
                return false;
        }

        private bool VerificaIdadeSocioSeMenor(string data)
        {
            Boolean wAux = false;
            try
            {
                wAux = (dHelperQuery.DataSistema().Year - Convert.ToDateTime(data).Year) < 18;
            }
            catch (Exception ex)
            {
                //Alert("Problema na conexão. Tente mais tarde.");
                ExibirMensagen(false, "Problema na conexão. Tente mais tarde.");
                //ErroresDeSistema("Problemas no sistema ao gerar contrato.", ex, ref ErrorSummary);
            }
            return wAux;
        }

        private Boolean Menor16(DateTime wData)
        {
            DateTime dataNascimento = new DateTime();
            dataNascimento = wData;

            TimeSpan diff;

            // Usando Subtract
            diff = DateTime.Now.Subtract(dataNascimento);

            if ((diff.Days / 365) < 16)
                return true;
            else
                return false;
        }

        private static string ObterNomeMunicipio(string tmu_cod_mun)
        {
            string saida = "";
            if (!string.IsNullOrEmpty(tmu_cod_mun))
            {
                dTab_Munic municObj = new dTab_Munic();
                municObj.tmu_cod_mun = decimal.Parse(tmu_cod_mun);
                DataTable dtMunicipio = municObj.Query();
                DataTableReader dtrMunicipio = dtMunicipio.CreateDataReader();
                dtrMunicipio.Read();
                if (dtrMunicipio.HasRows)
                {
                    saida = dtrMunicipio["descricao"].ToString().ToUpper();
                }
            }

            return saida;
        }

        private static string ObterDescricaoPais(string CodPais)
        {

            if (CodPais != "")
            {
                return dHelperQuery.BuscarDescricaoPais(CodPais);
            }
            else
                return "";
        }

        private static string ObterDescricaoTipoDoc(string CodDoc)
        {

            if (CodDoc != "")
            {
                return dHelperQuery.BuscarDescricaoTipoDocumento(CodDoc);
            }
            else
                return "";
        }

        private static string ObterDescricaoEstadoCivil(string Codigo)
        {

            if (Codigo != "")
            {
                return dHelperQuery.BuscarDescricaoEstadoCivil(Codigo);
            }
            else
                return "";
        }

        private static string ObterDescricaoRegimeBens(string Codigo)
        {

            if (Codigo != "")
            {
                return dHelperQuery.BuscarDescricaoRegimeBens(Codigo);
            }
            else
                return "";
        }

        private static string ObterDescricaoEvento(string Codigo)
        {

            if (Codigo != "")
            {
                return dHelperQuery.BuscarDescricaoEvento(Codigo);
            }
            else
                return "";
        }

        private static string ObterDescricaoEmancipado(string Codigo)
        {

            if (Codigo != "")
            {
                return dHelperQuery.BuscarDescricaoTipoEmancipado(Codigo);
            }
            else
                return "";
        }

        private string RetiraComplementoLogradouro(string wTexto)
        {
            string wAux = wTexto;
            int wInicio = 0;
            if (wAux.IndexOf(" (-") != -1)
            {
                wInicio = wAux.IndexOf(" (-");
                wAux = StringDireita(wAux, wInicio);
            }
            return wAux;
        }

        private static string StringDireita(string param, int tamanho)
        {
            string result = param.Substring(0, tamanho); //param.Length - tamanho);
            return result;

        }

        #endregion

        public void Alert(string msg)
        {
            string Message = "";
            if (msg == "")
                Message = "Os Campos Realçados em Vermelho São de Preenchimento Obrigatório.";
            else
                Message = msg;
            //Message = Message.Replace(@"\", "");

            //Message = Message.Replace("'", "");

            //Message = Message.Replace("\"", "");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "" + this.ClientID.ToString(), "alert('" + Message + "');", true);

        }

        #region Andamento
        private void Andamento()
        {
            string retorno = Req.bAndamento.GravaAndamento(Req.ProtocoloRequerimento, Req.ProtocoloViabilidade, Req.UsuarioRegin, "AAA", "AN");
            if (retorno == "1")
            {
                string _usuario = Req.UsuarioRegin;
                Req = null;
                Response.Redirect("Exame1.aspx?usuarioid=" + _usuario, false);
            }
            else
            {
                ExibirMensagen(false, "Não foi possível dar o andamento do processo.");
            }
        }
        #endregion

        private void ExibirMensagenaaa(bool mostrar, string msg)
        {
            msg = msg.Trim();
            if (string.IsNullOrEmpty(msg))
                return;
            //Alert(msg);
            divAvisosMensagem.InnerText = msg;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MostrarMensagem_" + this.ClientID.ToString(), "MostraMensagem('" + msg + "');", true);
        }


        #region Mensagem Alerta
        void btnPerguntaSim_Click(object sender, EventArgs e)
        {
            string aa = "";
        }
        private bool DadosOk()
        {
            if (lblAutenticacao.Text == "")
            {
                ErrorSummary.AddErrorMessage("Data de Autenticação obrigatorio.");
                return false;
            }

            //if (txtNire.Text == "")
            //{
            //    ErrorSummary.AddErrorMessage("Nire Obrigatorio");
            //    return false;
            //}

            if (lblCpf.Text == "")
            {
                ErrorSummary.AddErrorMessage("CPF Responsavel obrigatorio");
                return false;
            }

            if (txtNire.Text.Trim().Length > 11)
            {
                ErrorSummary.AddErrorMessage("Número Órgao de Registro (NIRE/Matrícula), Inválido");
                return false;
            }

            if (OrgaoRegistro != "RCPJ")
            {
                if (txtNire.Text.Trim() != "")
                {
                    if (!psc.Framework.General.IsValidNIRE(txtNire.Text.Trim()))
                    {
                        ErrorSummary.AddErrorMessage("Número Órgao de Registro (NIRE/Matrícula), Inválido");
                        return false;
                    }
                }
            }

            if (divNIRE246.Visible)
            {
                if (txtNire246.Text.Trim() == "")
                {
                    ErrorSummary.AddErrorMessage("Número Órgao de Registro Evento 246 obrigatorio");
                    return false;
                }

                if (!psc.Framework.General.IsValidNIRE(txtNire246.Text.Trim()))
                {
                    ErrorSummary.AddErrorMessage("Número Órgao de Registro Evento 246, invalido");
                    return false;
                }
            }

            string Resp = validaTipoOrgaodeRegistroDeferimento(retorno);
            if (Resp != "")
            {
                ErrorSummary.AddErrorMessage(Resp);
                return false;
            }

            return true;
        }

        protected void btnGravarRequerimento_Click(object sender, EventArgs e)
        {
            try
            {

                if (!DadosOk()) return;

                //using (PSC_PROTOCOLO_DBE_RFB pp = new PSC_PROTOCOLO_DBE_RFB())
                //{
                //    pp.ppd_cnpj = lblCNPJ.Text;
                //    pp.ppd_nire = txtNire.Text;
                //    pp.ppd_numero_dbe = lblDBE.Text;
                //    pp.ppd_protocolo = lblNumProcesso.Text;
                //    pp.ppd_nro_viabilidade = lblNumViabilidade.Text;
                //    pp.ppd_usuario_ws05 = lblCpf.Text;
                //    pp.pXMLArquivo = pXmlRequerimento;
                //    pp.UpdateRequerimento();
                //    ErrorSummary.AddErrorMessage("SOLICITAÇÃO DE DBE, GRAVADA COM SUCESSO");
                //}
            }
            catch (Exception ex)
            {
                ErroresDeSistema(ex, ref ErrorSummary);
            }

        }

        protected void btnInDeferirDBEMatriz_Click(object sender, EventArgs e)
        {
            try
            {
                //ErrorSummary.AddErrorMessage("SOLICITAÇÃO DE DBE, INDEFERIDA NA RECEITA FEDERAL COM SUCESSO");
                //return;
                using (WsRFB.ServiceReginRFB c = new WsRFB.ServiceReginRFB())
                {
                    WsRFB.DadosWs06 dados = new WsRFB.DadosWs06();
                    WsRFB.Retorno pRetorno = new WsRFB.Retorno();

                    string nroRecibo = lblDBE.Text.Substring(0, 10);
                    string nroIdentificacao = lblDBE.Text.Substring(10, 14);

                    dados.cpfResponsavelDeferimento = pUsuarioSistema;

                    dados.resultadoRegistroIntegradorEstadual = "02";
                    dados.reciboSolicitacao = nroRecibo;
                    dados.identificacaoSolicitacao = nroIdentificacao;

                    if (dados.resultadoRegistroIntegradorEstadual == "02")
                    {
                        WsRFB.incompRegistroIntegradorEstadual[] inconpa = new WsRFB.incompRegistroIntegradorEstadual[20];
                        WsRFB.incompRegistroIntegradorEstadual inconpa2 = new WsRFB.incompRegistroIntegradorEstadual();
                        inconpa2.codigo = "Z01";
                        inconpa2.mensagem = "Solicitação indeferida pelo Órgão de Registro a pedido do contribuinte";
                        inconpa.SetValue(inconpa2, 0);
                        dados.incompRegistroIntegradorEstadual = inconpa;
                    }

                    c.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                    pRetorno = c.ServiceWs06(dados);

                    if (pRetorno.status == "OK")
                    {
                        ErrorSummary.AddErrorMessage("SOLICITAÇÃO DE DBE, INDEFERIDA NA RECEITA FEDERAL COM SUCESSO");
                        btnDeferirDBEMatriz.Visible = false;
                        btnInDeferirDBEMatriz.Visible = false;
                    }
                    else
                    {
                        ErrorSummary.AddErrorMessage(pRetorno.status + " " + pRetorno.descricao);
                    }
                }

            }
            catch (Exception ex)
            {
                ErroresDeSistema(ex, ref ErrorSummary);
            }

        }

        protected void btnDeferirDBEMatriz_Click(object sender, EventArgs e)
        {
            try
            {
                string grava = "";
                if (ConfigurationManager.AppSettings["GravaregistrosRucRegin"] != null && ConfigurationManager.AppSettings["GravaregistrosRucRegin"].ToString() != "")
                    grava = ConfigurationManager.AppSettings["GravaregistrosRucRegin"].ToString().ToLower();


                if (!DadosOk()) return;

                ServicosPortal.ServicosPortal ser = new ServicosPortal.ServicosPortal();
                ServicosPortal.DadosDBERFB dados = new ServicosPortal.DadosDBERFB();
                ServicosPortal.Retorno pRetornoMatriz = new ServicosPortal.Retorno();


                dados.cnpj = lblCNPJ.Text;
                dados.dataRegistro = DateTime.Parse(lblAutenticacao.Text).ToString("yyyyMMdd");
                dados.nire = txtNire.Text.Trim();
                if (txtNire246.Text.Trim() != "")
                {
                    dados.numeroNire246 = txtNire246.Text.Trim();
                }
                dados.nome = lblNomeEmpresa.Text;
                dados.nroDBE = lblDBE.Text;
                dados.nroViabilidade = lblNumViabilidade.Text;
                dados.pProtocolo = lblNumProcesso.Text;
                dados.UsuarioSistema = pUsuarioSistema;
                dados.numeroServentia = pNumeroServentia.Trim();
                dados.cnpjOrgaoRegistro = pCNPJIntituicaoUsuario;
                dados.TipoDeferidor = 2;

                if (ConfigurationManager.AppSettings["ServicosPortal"] == null || ConfigurationManager.AppSettings["ServicosPortal"].ToString() == "")
                {
                    throw new Exception("Falta Parametro ServicosPortal no web.config");
                }


                if (grava == "on" && pNumeroServentia.Trim() == "")
                {
                    //So faço quando for junta essa validação
                    int Resp = PSC_PROTOCOLO.IsExistDBE(dados.nroDBE);

                    if (Resp > 0)
                    {
                        ErrorSummary.AddErrorMessage("DBE já foi carregado no Regin");
                        return;
                    }

                    Resp = PSC_PROTOCOLO.IsExistProtocoloRegin(dados.pProtocolo, "");
                    if (Resp > 0)
                    {
                        ErrorSummary.AddErrorMessage("Protocolo já foi carregado no Regin");
                        return;
                    }
                }

                //Verifico se e evento 102, caso seja filal
                bool pEventoFilial = false;
                for (int i = 0; i < pEventos.Count; i++)
                {
                    if (pEventos[i].ToString() == "102")
                    {
                        pEventoFilial = true;
                    }

                }
                //Aqui nao valido se a filial e de fora ou dentro, so passo esse parametro para SIM, porque caso seja de fora, a RFB vai gerar o numero
                //Entao passando sim, nao e validado (Nire/Matricula inválido ou em branco) no ws que defere DefereProcessoRFB
                if (pEventoFilial)
                    dados.filialFora = "SIM";

                ser.Url = ConfigurationManager.AppSettings["ServicosPortal"].ToString();
                pRetornoMatriz = ser.DefereProcessoRFB(dados);
                if (pRetornoMatriz.status == "OK" || pRetornoMatriz.codretorno == "4")
                {
                    if (pRetornoMatriz.Cnpj == "")
                    {
                        pRetornoMatriz.Cnpj = lblCNPJ.Text;
                    }

                    if (lblCNPJ.Text == "" && pRetornoMatriz.Cnpj != "")
                    {
                        lblCNPJ.Text = pRetornoMatriz.Cnpj;
                    }

                    if (txtNire.Text == "" && pRetornoMatriz.Cnpj != "")
                    {
                        txtNire.Text = pRetornoMatriz.Nire;
                        if (txtNire.Text == "")
                        {
                            using (WsRFB.ServiceReginRFB c = new WsRFB.ServiceReginRFB())
                            {
                                c.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                                
                                retorno = c.ServiceWs11(pRetornoMatriz.Cnpj);

                                if (retorno.status == "OK")
                                {
                                    if (retorno.oCNPJResponse.dadosCNPJ[0].numeroOrgaoRegistro != "")
                                        txtNire.Text = retorno.oCNPJResponse.dadosCNPJ[0].numeroOrgaoRegistro.ToString();
                                }
                                //Vou pegar o Numero de Nire no DBE
                                // ser.se(dados);
                            }
                        }
                        
                    }

                    /*
                        So faço por enquando se for Cartorio
                     * Se for Junta, vou fazer a rotina para gravar somente nas pcs e nao no ruc
                     */


                    if (pNumeroServentia.Trim() != "" || grava == "on")
                    {
                        WsRFB.redesim dados35 = new WsRFB.redesim();

                        if (retorno.oWs35Response != null && retorno.oWs35Response.dadosRedesim != null)
                        {
                            dados35 = retorno.oWs35Response.dadosRedesim;
                        }

                        string Resp = GravaWsRFB11Ruc(pEventos, dados.pProtocolo, pRetornoMatriz.Cnpj, dados.nire, lblNumViabilidade.Text, "", pNumeroServentia, pCNPJIntituicaoUsuario, dados35, dados.nroDBE, pUsuarioSistema);

                        if (Resp != "")
                        {
                            throw new Exception("Erro ao tentar gravar nas tabelas Ruc. " + Resp);
                            //ErrorSummary.AddErrorMessage("Erro ao tentar gravar nas tabelas Ruc. " + Resp);
                        }
                    }


                    //using (MAC_CENTRAL_CARGA c = new MAC_CENTRAL_CARGA())
                    //{
                    //    c.mcc_nr_cnpj_matriz = txtCNPJMatriz.Value;
                    //    c.mcc_protocolo = NumeroProtocolo;
                    //    c.AtualizaCNPJ();
                    //}
                    //EncondeControlesDeferidoMatriz();
                    //btnConfirmaRegistro.Visible = true;
                    ErrorSummary.AddErrorMessage("SOLICITAÇÃO DE DBE, DEFERIDA NA RECEITA FEDERAL COM SUCESSO");
                    btnDeferirDBEMatriz.Visible = false;
                    btnInDeferirDBEMatriz.Visible = false;
                }
                else
                {
                    ErrorSummary.AddErrorMessage(pRetornoMatriz.descricao);
                }
            }
            catch (Exception ex)
            {
                ErroresDeSistema(ex, ref ErrorSummary);
            }

        }

        private void ExibirMensagen(bool mostrar, string msg)
        {
            //Mostrar = true mostra botao Sim  e Não
            //Mostrar = false mostra botao OK

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "" + this.ClientID.ToString(), "VerificaAlturaPagina();", true);
            telaCarregandoMensagens.Style.Add("height", hidTamPag.Text + "px");
            telaCarregandoMensagens.Style.Add("Display", "");
            divAvisos.Style.Add("Display", "");

            if (mostrar)
            {
                divBotoesPergunta.Visible = true;
                divBotaoOK.Visible = false;
            }
            if (!mostrar)
            {
                divBotoesPergunta.Visible = false;
                divBotaoOK.Visible = true;
            }
            if (msg.Substring(0, 1) == "<")
            {
                divAlertaPergunta.InnerHtml = msg;
                divAlertaPergunta.Style.Add("text-align", "justify");
                divCarregandoDentro.Style.Add("height", "400px");
            }
            else
            {
                divAlertaPergunta.InnerText = msg;
                divCarregandoDentro.Style.Add("height", "180px");
            }
        }

        void btnConfirmaSim_Click(object sender, EventArgs e)
        {
            if (hidOrigemMensagem.Text == "finalizaDBE")
            {
                hidOrigemMensagem.Text = "";
                //Faz o andamento para AN

                Andamento();

            }


        }

        #endregion
    }
}