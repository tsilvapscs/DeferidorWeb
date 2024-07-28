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
using RCPJ.BLL;
using RCPJ.DAL;
using psc.ApplicationBlocks.SessionState;

namespace RCPJ.Application
{
    [Serializable]
    public partial class ComparacaoDBE : PageBaseControl
    {

        #region Variables Declarations
        [SessionPageState("psc.RCPJ.Requerimento")]
        protected bRequerimento Req;

        [TransientPageState]
        protected bRequerimento ReqFilial;


        [TransientPageState]
        protected bool _erroSocio = false;
        [TransientPageState]
        protected bool _erroCane = false;

        [TransientPageState]
        protected string _viabilidade = "";
        [TransientPageState]
        protected int _codNaturezaJuridica = 0;

        [TransientPageState]
        protected bool _dbOK = false;

        [TransientPageState]
        protected bool _erroComplemento = false;

        [TransientPageState]
        protected bool _origemComparacaoDBE = false;
        [TransientPageState]
        protected bool _erroEndereco = false;
       
        
        [TransientPageState]
        protected string _dbe = "";
        [TransientPageState]
        protected string _uf = "";

        public string Viabilidade
        {
            get { return _viabilidade; }
            set { _viabilidade = value; }
        }
        public string Dbe
        {
            get { return _dbe; }
            set { _dbe = value; }
        }
        public string UF
        {
            get { return _uf; }
            set { _uf = value; }
        }
        public int CodNaturezaJuridica
        {
            get { return _codNaturezaJuridica; }
            set { _codNaturezaJuridica = value; }
        }
        public bool DbeOK
        {
            get 
            {
                if (lblDbeOK.Text == "1")
                {
                    return true;
                }
                return false; 
            }

            set { _dbOK = value; }

        }
        public bool ErroComplemento
        {
            get
            {
                if (lblComparacaoComplmentoOK.Text == "1")
                {
                    return true;
                }
                return false;
            }

            set { _erroComplemento = value; }

        }
        public bool OrigemComparacaoDBE
        {
            get { return _origemComparacaoDBE; }
            set { _origemComparacaoDBE = value; }
        }

        public string Messagem
        {
            get { return lblPasso2Inferior.Text; }
            set { lblPasso2Inferior.Text = value; }
        }
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
            ListSociosViab.ItemDataBound += new DataListItemEventHandler(ListSociosViab_ItemDataBound);
            ListSociosDBE.ItemDataBound += new DataListItemEventHandler(ListSociosDBE_ItemDataBound);
            ListCNAEViab.ItemDataBound += new DataListItemEventHandler(ListCNAEViab_ItemDataBound);
            ListCNAEDBE.ItemDataBound += new DataListItemEventHandler(ListCNAEDBE_ItemDataBound);
        }
        #endregion

        // Delegate
        //public delegate void CancelarEventHandler(object sender, System.EventArgs e);
        // Event
        //public event CancelarEventHandler Autenticar;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Compara()
        {
            ReqFilial = new bRequerimento();
            ReqFilial.NaturezaJuridicaCodigo = _codNaturezaJuridica;
            ReqFilial.Parametros.cnpj = pCNPJInstituicaoDefault;
            ReqFilial.Parametros.Populate();

            DataSet dsViab = new DataSet();
            DataSet dsDBE = new DataSet();

            lblNumDBE.Text = _dbe;
            lblNumViabilidade.Text = _viabilidade;


            //Carrego o Dataset com os dados da viabilidade e o DBE
            dsViab = CarregaDadosWebService(_viabilidade,_uf);
            dsDBE = CarregaDadosWebServiceDbe(_dbe);

            
            //Carrego as classes do requerimento a partir do Dataset
            ReqFilial.CarregaDadosComparacaoViabilidade(dsViab, true);
            ReqFilial.CarregaDadosComparacaoDBE(dsDBE);

            //Faz a comparação
            bool Erro = !ReqFilial.ComparaDBExViabilidade();

            CarregaDadosTelaComparacao();
            if (ReqFilial.Parametros.getValor(bParametro.Valores.VALIDA_COMPLEMENTO_DBE) == "2")
            {
                //se só existir erro de complemento colocar como válido
                if (ReqFilial._DBEViab_Erro_Complemento && SomenteErrodeComplemento())
                {
                    Erro = false;
                }
            }
            if (ReqFilial.Parametros.getValor(bParametro.Valores.COMPARA_ENDERECO_DBE) == "1")
            {
                if (SomenteErrodeEndereco())
                    Erro = false;
            }
            //se o endereço estiver errado mas o municipio estiver certo deixa passar
            //se todo o endereço estiver errado não deixa passar

            if (ReqFilial.Parametros.getValor(bParametro.Valores.COMPARA_ENDERECO_DBE) == "3")
            {
                if (SomenteErrodeEnderecoMunicipio())
                    Erro = false;
            }

//            if (ReqFilial.Parametros.getValor(bParametro.Valores.VALIDA_COMPLEMENTO_DBE) == "2")
//            {
//                //se só existir erro de complemento colocar como válido
//                if (ReqFilial._DBEViab_Erro_Complemento && SomenteErrodeComplemento())
//                {
//                    Erro = false;
//                    _dbOK = false;
//                    lblDbeOK.Text = "0";
//                    lblComparacaoComplmentoOK.Text = "1";
//                    lblErroComplemento.Text = @"Existe divergência no complemento do endereço entre a Viabilidade e o DBE.
//                                              Para confirmnar o complemento da Viabilidade clique no Botão CONFIRMAR, 
//                                              caso contrário clique no Botão FECHAR e refaça a Viabilidade ou o DBE.";
//                    lblErroComplemento.Visible = true;
//                }
//            }
            if (Erro)
            {
                _dbOK = false;
                lblDbeOK.Text = "0";
                lblComparacaoComplmentoOK.Text = "0";

                lblPasso2Inferior.Text = @"As informações da Viabilidade e do DBE estão divergentes.
                                    Clique no botão FECHAR e faça uma Viabilidade e/ou DBE novo(s).";
                lblPasso2Inferior.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                if (lblComparacaoComplmentoOK.Text != "1")
                {
                    _dbOK = true;
                    lblDbeOK.Text = "1";
                    lblPasso2Inferior.Text = @"As informações da Viabilidade e do DBE estão compatíveis.
                                    Clique no botão FECHAR e complete os dados do requerimento.";
                    lblPasso2Inferior.ForeColor = System.Drawing.Color.Blue;
                }
            }

        }

  
       
        #region DataList
        void ListCNAEViab_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int i = e.Item.ItemIndex;
                if (ReqFilial._DBEViab_Erro_CNAE.Count > i && i > -1)
                {
                    if (ReqFilial._DBEViab_Erro_CNAE[i])
                    {

                        e.Item.CssClass = "tabelaConfirmacaoErro";
                        
                    }
                }
            }

        }

        void ListCNAEDBE_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int i = e.Item.ItemIndex;
                if (ReqFilial._DBEViab_Erro_CNAE.Count > i && i > -1)
                {
                    if (ReqFilial._DBEViab_Erro_CNAE[i])
                    {

                        e.Item.CssClass = "tabelaConfirmacaoErro";
                       
                    }
                }
            }

        }

        void ListSociosViab_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int i = e.Item.ItemIndex;
                if (ReqFilial._DBEViab_Erro_Socios.Count > i && i > -1)
                {
                    if (ReqFilial._DBEViab_Erro_Socios[i])
                    {

                        e.Item.CssClass = "tabelaConfirmacaoErro";
                        
                    }
                }
            }

        }

        void ListSociosDBE_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int i = e.Item.ItemIndex;
                if (ReqFilial._DBEViab_Erro_Socios.Count > i && i > -1)
                {
                    if (ReqFilial._DBEViab_Erro_Socios[i])
                    {

                        e.Item.CssClass = "tabelaConfirmacaoErro";
                        
                    }
                }
            }

        }


        #endregion


        void btnCancelar_Click(object sender, EventArgs e)
        {
            DbeFilialOK = _dbOK;
            
            _origemComparacaoDBE = true;
            hidOrigemComparacao.Text = "dbefilial";
            if (_dbOK)
                hidComparacaoOK.Text = "1";
            else
                hidComparacaoOK.Text = "2";

            this.Visible = false;
         

        }

        private bool SomenteErrodeComplemento()
        {
            if (!ReqFilial._DBEViab_Erro_NomeEmpresa && !ReqFilial._DBEViab_Erro_TipoLogradouro
            && !ReqFilial._DBEViab_Erro_Logradouro && !ReqFilial._DBEViab_Erro_Numero
            && !ReqFilial._DBEViab_Erro_Municipio && !ReqFilial._DBEViab_Erro_Bairro && !ReqFilial._DBEViab_Erro_CEP
            && !ReqFilial._DBEViab_Erro_NomeEmpresa && !_erroSocio && !_erroCane)
            {
                return true;
            }
            return false;
        }
        private bool SomenteErrodeEndereco()
        {
            //(Req._DBEViab_Erro_Evento.Count == 0 && !Req._DBEViab_Erro_Evento[0]) &&
            if ((!ReqFilial._DBEViab_Erro_NomeEmpresa && !_erroSocio && !_erroCane) &&
               (ReqFilial._DBEViab_Erro_TipoLogradouro || ReqFilial._DBEViab_Erro_Logradouro || ReqFilial._DBEViab_Erro_Numero
               || ReqFilial._DBEViab_Erro_Municipio || ReqFilial._DBEViab_Erro_Bairro || ReqFilial._DBEViab_Erro_CEP))
            {
                return true;
            }
            return false;
        }
        private bool SomenteErrodeEnderecoMunicipio()
        {
            
            if ((!ReqFilial._DBEViab_Erro_NomeEmpresa && !_erroSocio && !_erroCane) &&
                _erroEndereco && (ReqFilial._DBEViab_Erro_TipoLogradouro || ReqFilial._DBEViab_Erro_Logradouro || ReqFilial._DBEViab_Erro_Numero
                || ReqFilial._DBEViab_Erro_Bairro || ReqFilial._DBEViab_Erro_CEP) && !ReqFilial._DBEViab_Erro_Municipio)
            {
                return true;
            }
            return false;
        }
        public void GravaDadosDBENoRequerimento()
        {
            /*
             se no dbe tiver informação do socio atualiz o req com o socio
             se no dbe tiver evento de capital social atualizo o capital social no req
              
             */

            if (ReqFilial.DBE == null)
            {
                return;
            }
            try
            {

                //Se tem requerente no DBE atualizar no requerimento
                if (Req.DBE.RequerenteNome != "" || Req.DBE.RequerenteCPF != "")
                {

                    Req.RequerenteNome = Req.DBE.RequerenteNome;
                    Req.RequerenteCPF = Req.DBE.RequerenteCPF;
                    Req.RequerenteDDD = Req.DBE.RequerenteDDD;
                    Req.RequerenteTelefone = Req.DBE.RequerenteTelefone;
                    Req.RequerenteEmail = Req.DBE.RequerenteEmail;


                }

                Req.Contabilista.co_CRC_Empresa = Req.DBE.Contabilista.co_CRC_Empresa;
                Req.Contabilista.co_CRC_Resp = Req.DBE.Contabilista.co_CRC_Resp;
                Req.Contabilista.cpf_resp = Req.DBE.Contabilista.cpf_resp;
                Req.Contabilista.cpfCnpj = Req.DBE.Contabilista.cpfCnpj;
                Req.Contabilista.DataInscricao = Req.DBE.Contabilista.DataInscricao;
                Req.Contabilista.ds_Pessoa = Req.DBE.Contabilista.ds_Pessoa;
                Req.Contabilista.tip_Class_Empresa = Req.DBE.Contabilista.tip_Class_Empresa;
                Req.Contabilista.tip_Class_Resp = Req.DBE.Contabilista.tip_Class_Resp;
                Req.Contabilista.tip_CRC_Empresa = Req.DBE.Contabilista.tip_CRC_Empresa;
                Req.Contabilista.tip_CRC_Resp = Req.DBE.Contabilista.tip_CRC_Resp;
                Req.Contabilista.uf_CRC_Empresa = Req.DBE.Contabilista.uf_CRC_Empresa;
                Req.Contabilista.uf_CRC_Resp = Req.DBE.Contabilista.uf_CRC_Resp;

                //Atualizar os dados de acordo com o evento
                if (getTipoProtocolo(Req.ProtocoloEvento) == 2) //Alteração
                {

                    if (getEventoAvalia(Req.ProtocoloEvento, CODEVENTO_ALTERPORTE_EMPRESA))
                    {

                        //Tipo de Enquadramento 
                        //1-Enquadramento, 2 Reenq. ME-EPP,  3 Reenq. EPP-ME, 4-Desenquadramento ME, , 5-Desenquadramento EPP
                        //req.ReqGenprotocolo.T099_IN_TIPO_ENQUADRAMENTO = 1;
                        Req.Enquadramento = Req.DBE.Enquadramento;

                    }

                    //usar o Nome da viabilidade
                    if (getEventoAvalia(Req.ProtocoloEvento, CODEVENTO_ALTERACAO_NOME_EMPRESARIAL))
                    {
                        Req.Nome_Fantasia = Req.DBE.Nome_Fantasia;
                    }

                    //usar o endereço da Vaibilidade
                    if (VerificaEventoAltEndereco(Req.ProtocoloEvento))
                    {

                        //Verificar se o cep for generico
                        //Se for usar o CEP do DBE
                        if (Req.SedeCEP.Substring(4, 4) == "0000")
                        {
                            Req.SedeCEP = Req.DBE.SedeCEP;
                        }
                    }

                    if (getEventoAvalia(Req.ProtocoloEvento, CODEVENTO_CAPITAL_SOCIAL))
                    {
                        Req.CapitalSocial = Req.DBE.CapitalSocial;
                    }

                    if (getEventoAvalia(Req.ProtocoloEvento, CODEVENTO_ALTER_QSA))
                    {
                        Req.AtualizasocioComDBE();
                        //Req.Socios.Clear();
                        //Req.Socios = Req.DBE.Socios;
                    }

                }
                else
                {
                    //Constituição
                    //Verificar se o cep for generico
                    //Se for usar o CEP do DBE
                    if (Req.SedeCEP.Substring(4, 4) == "0000")
                    {
                        Req.SedeCEP = Req.DBE.SedeCEP;
                    }



                    Req.Nome_Fantasia = Req.DBE.Nome_Fantasia;
                    Req.SedeDDD = Req.DBE.SedeDDD;
                    Req.SedeTelefone = Req.DBE.SedeTelefone;
                    Req.SedeEmail = Req.DBE.SedeEmail;
                    Req.Enquadramento = Req.DBE.Enquadramento;
                    Req.CapitalSocial = Req.DBE.CapitalSocial;
                    Req.Capital_Integralizado = Req.DBE.CapitalSocial;
                    Req.CNPJ_Orgao_Registro = pCNPJInstituicaoDefault;

                    Req.Socios.Clear();
                    Req.Socios = Req.DBE.Socios;
                    // Req.DBE.Socios.Clear();
                }


                //Se tem dados do contabilista


                Req.TipoGravacao = "Tudo";
                Req.IndDbeCarregado = 1;
                Req.Update();
                Req.CarregaAba01Protocolo();

                if (Req._DBEViab_Erro_Complemento)
                {

                    Req.DivergenciaDBE = new bDivergenciaDBE();
                    Req.DivergenciaDBE.NumeroOrgaoRegistro = Req.orgaoRegistro.cnpj;
                    Req.DivergenciaDBE.NumeroProtocolo = Req.ProtocoloRequerimento;
                    Req.DivergenciaDBE.Item = 7;
                    Req.DivergenciaDBE.Texto = Req.DBE.SedeComplemento;
                    Req.DivergenciaDBE.Update();
                }

                Response.Redirect("Requerimento2a.aspx", false);
            }
            catch (Exception ex)
            {
                
               
            }

        }

        private void CarregaDadosTelaComparacao()
        {


            #region 1. Pessoa Jurídica - Empresa (Viabilidade)

            lblEventoViab.Text = "";
            foreach (bProtocoloEvento ev in ReqFilial.Viabilidade.ProtocoloEvento)
            {
                lblEventoViab.Text += ev.CodigoEvento + " - " + RCPJ.DAL.Helper.dHelperQuery.GetDescricaoEventoRFB(ev.CodigoEvento.ToString()) + "<BR>";

            }

            lblNomeEmpresaViab.Text = ReqFilial.Viabilidade.SedeNome;

            lblTipoEmpresaViab.Text = ReqFilial.NaturezaJuridicaCodigo.ToString() + " - " + ReqFilial.NaturezaJuridicaDescricao;
            #endregion

            #region Endereço
            lblStreetTypeViab.Text = ReqFilial.Viabilidade.SedeDsTipoLogradouro;

            lblLogradouroViab.Text = ReqFilial.Viabilidade.SedeLogradouro;

            lblNumeroViab.Text = ReqFilial.Viabilidade.SedeNumero;

            lblComplementoViab.Text = ReqFilial.Viabilidade.SedeComplemento;

            lblMunicipioViab.Text = ReqFilial.Viabilidade.SedeMunicipio + " - " + ReqFilial.Viabilidade.SedeNomeMunicipio;

            lblBairroViab.Text = ReqFilial.Viabilidade.SedeBairro;

            lblZipViab.Text = ReqFilial.Viabilidade.SedeCEP;

            lblUFViab.Text = ReqFilial.Viabilidade.SedeUF;

            lblPaisViab.Text = "Brasil";

            lblEmailViab.Text = ReqFilial.Viabilidade.SedeEmail;

            if (ReqFilial._DBEViab_Erro_TipoLogradouro || ReqFilial._DBEViab_Erro_Logradouro || ReqFilial._DBEViab_Erro_Numero
               || ReqFilial._DBEViab_Erro_Bairro || ReqFilial._DBEViab_Erro_CEP || ReqFilial._DBEViab_Erro_Municipio)
            {
                _erroEndereco = true;
            }

            #endregion



            #region 1. Pessoa Jurídica - Empresa (DBE)

            lblEventoDbe.Text = "";
            foreach (bProtocoloEvento ev in ReqFilial.DBE.ProtocoloEvento)
            {
                lblEventoDbe.Text += ev.CodigoEvento + " - " + RCPJ.DAL.Helper.dHelperQuery.GetDescricaoEventoRFB(ev.CodigoEvento.ToString()) + "<BR>";
            }

            lblNomeEmpresaDbe.Text = ReqFilial.DBE.SedeNome;

            lblTipoEmpresaDbe.Text = ReqFilial.DBE.NaturezaJuridicaCodigo.ToString();

            bTabelasAuxiliares nj = new bTabelasAuxiliares();
            string descnat = nj.GetNaturezaJuridica(ReqFilial.DBE.NaturezaJuridicaCodigo);
            if (descnat != "")
            {
                lblTipoEmpresaDbe.Text += " - " + descnat;
            }

            #region Endereço DBE
            lblStreetTypeDbe.Text = ReqFilial.DBE.SedeDsTipoLogradouro;

            lblLogradouroDbe.Text = ReqFilial.DBE.SedeLogradouro;

            lblNumeroDbe.Text = ReqFilial.DBE.SedeNumero;

            lblComplementoDbe.Text = ReqFilial.DBE.SedeComplemento;

            lblMunicipioDbe.Text = ReqFilial.DBE.SedeMunicipio + " - " + ReqFilial.DBE.SedeNomeMunicipio;

            lblBairroDbe.Text = ReqFilial.DBE.SedeBairro;

            lblZipDbe.Text = ReqFilial.DBE.SedeCEP;

            lblUFDbe.Text = ReqFilial.DBE.SedeUF;

            lblPaisDBE.Text = "Brasil";

            lblEmailDbe.Text = ReqFilial.DBE.SedeEmail;
            #endregion

            #endregion

            #region CNAE

            if (getEventoAvalia(ReqFilial.Viabilidade.ProtocoloEvento, CODEVENTO_CONSTITUICAO_EMPRESA)
                || getEventoAvalia(ReqFilial.Viabilidade.ProtocoloEvento, CODEVENTO_ALTERACAO_ATIVIDADES_ECONOMICAS)
                || getEventoAvalia(ReqFilial.Viabilidade.ProtocoloEvento, CODEVENTO_CONSTITUICAO_FILIAL))
            {
                ListCNAEViab.DataSource = ReqFilial.Viabilidade.CNAEs;
                ListCNAEViab.DataBind();
                ListCNAEDBE.DataSource = ReqFilial.DBE.CNAEs;
                ListCNAEDBE.DataBind();

                for (int i = 0; i < ReqFilial.Viabilidade.CNAEs.Count; i++)
                {
                    if (ReqFilial._DBEViab_Erro_CNAE.Count > i)
                    {
                        if (ReqFilial._DBEViab_Erro_CNAE[i])
                        {
                            ListCNAEViab.Items[i].CssClass = "tabelaConfirmacaoErro";
                            _erroCane = true;
                        }
                    }
                }

                for (int i = 0; i < ReqFilial.DBE.CNAEs.Count; i++)
                {
                    if (ReqFilial._DBEViab_Erro_CNAE.Count > i)
                    {
                        if (ReqFilial._DBEViab_Erro_CNAE[i])
                        {
                            ListCNAEDBE.Items[i].CssClass = "tabelaConfirmacaoErro";
                            _erroCane = true;
                        }
                    }
                }
            }

            #endregion

            #region Socios
            ListSociosViab.DataSource = ReqFilial.Viabilidade.Socios;
            ListSociosViab.DataBind();
            ListSociosDBE.DataSource = ReqFilial.DBE.Socios;
            ListSociosDBE.DataBind();



            for (int i = 0; i < ReqFilial.Viabilidade.Socios.Count; i++)
            {
                if (ReqFilial._DBEViab_Erro_Socios.Count > i)
                {
                    if (ReqFilial._DBEViab_Erro_Socios[i])
                    {

                        ListSociosViab.Items[i].CssClass = "tabelaConfirmacaoErro";
                        _erroSocio = true;

                    }
                }
            }

            //ListSociosViab.Items[0].BackColor = System.Drawing.Color.Red;

            for (int i = 0; i < ReqFilial.DBE.Socios.Count; i++)
            {
                if (ReqFilial._DBEViab_Erro_Socios.Count > i)
                {
                    if (ReqFilial._DBEViab_Erro_Socios[i])
                    {
                        ListSociosDBE.Items[i].CssClass = "tabelaConfirmacaoErro";
                        _erroSocio = true;
                    }
                }
            }


            #endregion


            #region Destaca Erro/Divergencias



            foreach (bool ev in ReqFilial._DBEViab_Erro_Evento)
            {
                if (ev)
                {
                    tdEventoDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                    tdEventoViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                    break;
                }
            }

            if (ReqFilial._DBEViab_Erro_Natureza)
            {
                tdNaturezaEmpresaViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdNaturezaEmpresaDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }

            if (ReqFilial._DBEViab_Erro_NomeEmpresa)
            {
                tdNomeEmpresaDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdNomeEmpresaViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }
            //else
            //{
            //    lblNomeEmpresaDbe.Attributes["class"]= "tabelaConfirmacao2";
            //    lblNomeEmpresaViab.Attributes["class"]= "tabelaConfirmacao2";

            //}

            if (ReqFilial._DBEViab_Erro_TipoLogradouro)
            {
                tdTipoLogradouroDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdTipoLogradouroViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }
            //else
            //{
            //    tdTipoLogradouroDBE.Attributes["class"]= "tabelaConfirmacao2";
            //    tdTipoLogradouroDBE.Attributes["class"]= "tabelaConfirmacao2";

            //}
            if (ReqFilial._DBEViab_Erro_Logradouro)
            {
                tdLogradouroDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdLogradouroViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }

            if (ReqFilial._DBEViab_Erro_Numero)
            {
                tdNumeroDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdNumeroViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }

            if (ReqFilial._DBEViab_Erro_Complemento)
            {
                tdComplementoDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdComplementoViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }

            if (ReqFilial._DBEViab_Erro_Municipio)
            {
                tdMunicipioDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdMunicipioViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }

            if (ReqFilial._DBEViab_Erro_Bairro)
            {
                tdBairroDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdBairroViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }
            //else
            //{
            //    tdBairroDBE.Attributes["class"]= "tabelaConfirmacao2";
            //    tdBairroViab.Attributes["class"]= "tabelaConfirmacao2";

            //}
            if (ReqFilial._DBEViab_Erro_CEP)
            {
                tdCEPDBE.Attributes["class"] = "tabelaConfirmacao2Divergencia";
                tdCEPViab.Attributes["class"] = "tabelaConfirmacao2Divergencia";
            }

            #endregion
        }


        private void CarregaDadosViabilidade(DataSet result)
        {
            bFilial filial = new bFilial();

            DataTable ViaProtocolo = result.Tables["VIA_PROTOCOLO_VIAB"];

            #region Requerente
            Req.RequerenteNome = ViaProtocolo.Rows[0]["VPV_NOM_SOLIC"].ToString();
            Req.RequerenteCPF = ViaProtocolo.Rows[0]["VPV_VSV_CPF_CNPJ_SOLIC"].ToString().Trim();
            Req.RequerenteEmail = ViaProtocolo.Rows[0]["VPV_EMAIL_SOLIC"].ToString();
            #endregion

            if (Req.CodigoEvento == 102)
            {
                filial.FilialCEP = ViaProtocolo.Rows[0]["VPV_CEP"].ToString();
                filial.FilialUF = ViaProtocolo.Rows[0]["VPV_TMU_TUF_UF"].ToString();
                filial.FilialCodMunicipio = Convert.ToInt32(ViaProtocolo.Rows[0]["VPV_TMU_COD_MUN"]);
                using (wsEndereco.WSEndereco wsendereco = new wsEndereco.WSEndereco())
                {
                    wsendereco.Url = ConfigurationManager.AppSettings["wsEndereco"].ToString();
                    filial.FilialMunicipio = wsendereco.BuscaDescricaoMunicipio(filial.FilialUF, filial.FilialCodMunicipio.ToString());
                }
                filial.FilialBairro = ViaProtocolo.Rows[0]["VPV_BAIRRO"].ToString();
                filial.FilialTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                filial.FilialLogradouro = ViaProtocolo.Rows[0]["VPV_LOGRADORO"].ToString();
                filial.FilialTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                filial.FilialNumero = ViaProtocolo.Rows[0]["VPV_NUM_LOGRADOURO"].ToString();
                filial.FilialComplemento = ViaProtocolo.Rows[0]["VPV_COMP_LOGRADOURO"].ToString();

                DataTable ViaCnae = result.Tables["VIA_PROT_CNAE"];
                filial.CNAEs.Clear();
                foreach (DataRow r in ViaCnae.Rows)
                {
                    bCNAE c = new bCNAE();
                    c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                    if (c.CodigoCNAE.Length == 6)
                        c.CodigoCNAE = "0" + c.CodigoCNAE;
                    c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                    filial.CNAEs.Add(c);
                }

                Req.Filiais.Add(filial);
            }
            else
            {
                Req.SedeCEP = ViaProtocolo.Rows[0]["VPV_CEP"].ToString();
                Req.SedeUF = ViaProtocolo.Rows[0]["VPV_TMU_TUF_UF"].ToString();
                Req.SedeMunicipio = ViaProtocolo.Rows[0]["VPV_TMU_COD_MUN"].ToString();
                Req.SedeBairro = ViaProtocolo.Rows[0]["VPV_BAIRRO"].ToString();
                Req.SedeTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                Req.SedeDsTipoLogradouro = ViaProtocolo.Rows[0]["VPV_TTL_TIP_LOGRADORO"].ToString();
                Req.SedeLogradouro = ViaProtocolo.Rows[0]["VPV_LOGRADORO"].ToString();
                Req.SedeNumero = ViaProtocolo.Rows[0]["VPV_NUM_LOGRADOURO"].ToString();
                Req.SedeComplemento = ViaProtocolo.Rows[0]["VPV_COMP_LOGRADOURO"].ToString();
                Req.SedeNomeMunicipio = RCPJ.DAL.Helper.dHelperQuery.BuscarDescricaoMunicipio(Req.SedeMunicipio);
            }

            #region Objeto Social
            Req.ObjetoSocial = ViaProtocolo.Rows[0]["VPV_OBJETIVO"].ToString();
            #endregion

            #region Razao Social
            DataTable ViaRazao = result.Tables["VIA_PROT_RAZAO_SOCIAL"];
            if (ViaRazao != null)
            {
                int tot = ViaRazao.Rows.Count;

                if (tot == 1)
                {
                    Req.SedeNome = ViaRazao.Rows[0]["VRS_RAZAO_SOCIAL"].ToString();
                }
                else if (tot == 2)
                {
                    Req.SedeNome = ViaRazao.Rows[0]["VRS_RAZAO_SOCIAL"].ToString();
                }
                else if (tot == 3)
                {
                    Req.SedeNome = ViaRazao.Rows[0]["VRS_RAZAO_SOCIAL"].ToString();

                }
                Req.SedeNome = RetiraTipoEnquadramento(Req.SedeNome).Trim();
            }
            #endregion

            #region CNAE
            DataTable ViaCnaeAux = result.Tables["VIA_PROT_CNAE"];
            Req.CNAEs.Clear();
            foreach (DataRow r in ViaCnaeAux.Rows)
            {
                bCNAE c = new bCNAE();
                c.CodigoCNAE = r["VPC_TAE_COD_ACTVD"].ToString();
                c.Descricao = r["VPC_TAE_DESC"].ToString();
                c.FormAtiv = c.CodigoCNAE.Substring(0, 5);
                c.Versao = "V02";
                c.Update111();
                if (c.CodigoCNAE.Length == 6)
                    c.CodigoCNAE = "0" + c.CodigoCNAE;
                c.TipoAtividade = decimal.Parse(r["VPV_TIP_CNAE"].ToString()) + 35;
                Req.CNAEs.Add(c);
            }
            #endregion

            #region socios
            DataTable socios = result.Tables["VIA_PROT_SOCIOS"];
            foreach (DataRow s in socios.Rows)
            {
                bSocios ns = new bSocios();
                ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                ns.Nome = s["VPS_NOME_SOCIO"].ToString();
                ns.Nome_Mae = s["VPS_NOME_MAE"].ToString();
                ns.Qualificacao = "22";
                ns.Qualificacao_Descricao = "Sócio";
                ns.CPFCNPJ = s["VPS_CPF_CNPJ_SOCIO"].ToString();
                if (ns.CPFCNPJ.Trim().Length == 11)
                    ns.TipoPessoa = "F";
                else
                    ns.TipoPessoa = "J";
                ns.ObrigacoesSociais = "S";
                ns.tipoacao = 1;
                Req.Socios.Add(ns);
            }
            #endregion

        }
       
        
    }
}