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
/// <summary>
/// Summary description for PageBaseControl
/// </summary>
namespace RCPJ.Application
{
    public class PageBaseControl : System.Web.UI.UserControl
    {
        protected string[] tbEnquadramento = new string[12] { " - ME ", "- ME ", " -ME ", "-ME ", " ME ", " ME. ", " - EPP", "- EPP ", " -EPP ", "-EPP ", " EPP ", " EPP. " };


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

        [TransientPageState]
        public bool _dbeFilialOK ;

        public bool DbeFilialOK
        {
            get { return _dbeFilialOK; }
            set { _dbeFilialOK = value; }
        }

        public PageBaseControl()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string pCNPJInstituicaoDefault
        {
            get
            {
                return (ConfigurationManager.AppSettings["pCNPJInstituicaoDefault"] != null) ? ConfigurationManager.AppSettings["pCNPJInstituicaoDefault"].ToString() : "";
            }
        }

        public string RetiraTipoEnquadramento(string wTexto)
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

        }

        public bool VerificaEventoAltEndereco(List<bProtocoloEvento> ProtocoloEvento)
        {
            bool ret = false;
            foreach (bProtocoloEvento ev in ProtocoloEvento)
            {
                switch (Int32.Parse(ev.CodigoEvento.ToString()))
                {
                    case 209:
                        ret = true;
                        break;
                    case 210:
                        ret = true;
                        break;
                    case 211:
                        ret = true;
                        break;
                }

            }
            return ret;
        }

        #region Carrega Dados do WS RFB
        
        public WsRFB.Retorno getDbeRFB(string pDbe)
        {

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
        
        #endregion

        #region Carrega Dados Wb Viabilidade
        public static DataSet CarregaDadosWebServiceDbe(string pDbe)
        {

            ServicosRequerimento.Services wsControle = new ServicosRequerimento.Services();
            wsControle.Url = ConfigurationManager.AppSettings["wsServicoRequerimento"].ToString();
            wsControle.Timeout = 10000;
            return wsControle.GetDbeDS(pDbe);

        }
        #endregion

        #region Carrega Dados Wb Viabilidade
        public static DataSet CarregaDadosWebService(string protocolo, string pUF)
        {

            //bOrgaoRegistro cORFilial = new bOrgaoRegistro("", pUF);
            //string _url = cORFilial.WsViabilidade;
            string _url = ConfigurationManager.AppSettings["RJ_Viabilidade.WsControleViab"].ToString();
            if (_url == "")
            {
                throw new Exception("Erro no ws " + pUF);
            }

            DataSet result = new DataSet();
            WsControleViab wsControle = new WsControleViab();
            wsControle.Url = _url;
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
        public static DataSet CarregaDadosWebService(string protocolo)
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

        public static decimal getTipoProtocolo(List<bProtocoloEvento> ProtocoloEvento)
        {

            for (int i = 0; i < ProtocoloEvento.Count; i++)
            {
                bProtocoloEvento ev;
                ev = (bProtocoloEvento)ProtocoloEvento[i];
                if (ev.CodigoEvento == 101)
                {
                    return 1;
                }
            }
            return 2;
        }
        public static bool getEventoAvalia(List<bProtocoloEvento> ProtocoloEvento, string TipoDeEvento)
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
    }
}