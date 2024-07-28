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

namespace RCPJ.Application
{
    [Serializable]
    public partial class ComparaDadosDBE : PageBase
    {
        [SessionPageState("psc.RCPJ.Requerimento")]
        protected bRequerimento req;
        [TransientPageState]
        protected string NroViabilidade = string.Empty;
        [TransientPageState]
        protected string NroDbe = string.Empty;

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
            btnCompViabDBE.Click += new EventHandler(btnCompViabDBE_Click);
            btnCompViaVoltar.Click += new EventHandler(btnCompViaVoltar_Click);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //req.ProtocoloViabilidade = txtviabilidade.Text;
                //req.ProtocoloRCPJ = txtProtocoloOR.Text;
                //req.CodigoDBE = retProt(txtDBE.Text);

                NroViabilidade = req.ProtocoloViabilidade;
                NroDbe = req.CodigoDBE;
                //divBotaoAdicionarViabilidade.Visible = false;
                divComparaDBE.Visible = true;
                pgnComparaDBE.CodNaturezaJuridica = req.NaturezaJuridicaCodigo;
                pgnComparaDBE.Viabilidade = NroViabilidade;
                pgnComparaDBE.Dbe = NroDbe;
                pgnComparaDBE.UF = "SC";
                pgnComparaDBE.Compara();
                pgnComparaDBE.Visible = true;
                if (pgnComparaDBE.ErroComplemento)
                {
                    //btnCompViabDBEConfirma.Visible = true;
                }
                btnCompViaVoltar.Visible = false;
                btnCompViabDBE.Visible = false;
                if (pgnComparaDBE.DbeOK)
                {
                    pgnComparaDBE.Messagem = "Clique no button avançar para proceguir";
                    div15.Visible = true;
                    btnCompViabDBE.Visible = true;
                }
                else
                {
                    pgnComparaDBE.Messagem = "Clique no button voltar para proceguir";
                    div15.Visible = true;
                    btnCompViaVoltar.Visible = true;

                    //pgnComparaDBE.Messagem = "O DBE e a Viabilidade estão com deferenças, por favor caso queira continuar mesmo assim, lembrando que o numero de viabilidade não sera associada ao processo clicle em avançar";
                    //NroViabilidade = "";
                    //req.ProtocoloViabilidade = "";
                    //pgnComparaDBE.Viabilidade = "";

                    //btnCompViabDBE.Visible = true;
                }
            }
        }
        void btnCompViaVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("entradadeferidor.aspx", true);
        }

        void btnCompViabDBE_Click(object sender, EventArgs e)
        {


            System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
            data.Add("pgnComparaDBE.Dbe", NroDbe);
            if (req.Data_Assinatura == DateTime.MinValue)
            {
                data.Add("pgnComparaDBE.Autenticacao", "");
            }
            else
            {
                data.Add("pgnComparaDBE.Autenticacao", ((DateTime)req.Data_Assinatura).ToString("yyyyMMdd"));
            }
            data.Add("pgnComparaDBE.Viabilidade", NroViabilidade);
            data.Add("pgnComparaDBE.ProtocoloRCPJ", req.ProtocoloRCPJ);
            data.Add("pgnComparaDBE.NroOrgaoRegistro", req.nrMatricula);
            data.Add("pgnComparaDBE.UsuarioSistema", req.UsuarioRegin);
            data.Add("pgnComparaDBE.DefereDBE", "on");


            HttpHelper.RedirectAndPOST(this.Page, "DeferidorGenerico.aspx", data);
            return;

            //divComparaDBE.Visible = false;

        }
        void btnCompViabDBEConfirma_Click(object sender, EventArgs e)
        {

            if (pgnComparaDBE.ErroComplemento)
            {
                //AdicionaFilial();
            }

            divComparaDBE.Visible = false;
            //divBotaoAdicionarViabilidade.Visible = true;

        }
    }
}