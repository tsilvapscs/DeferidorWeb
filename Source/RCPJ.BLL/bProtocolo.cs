using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.Entities;
using RCPJ.DAL.ConnectionBase;
using System.Data;

namespace RCPJ.BLL
{
    [Serializable]
    public class bProtocolo: System.IDisposable
    {
        #region  Property Declarations
        private string _CNPJ_ORGAO_REGISTRO = "";
        private string _NroProtocolo = "";
        private string _requerimento = "";

       
        private int _SqPessoa = 0;
        private DateTime _DataEntrada = DateTime.MinValue;
        private DateTime _DataAverbacao = DateTime.MinValue;
        private string _NroProtocoloViabilidade = "";
        private string _NroDOCAD = "";
        private string _NroDBE = "";
        private decimal _t005_xl_docad;
        private decimal _t005_xl_dbe;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string CnpjOrgaoRegistro
        {
            get {return _CNPJ_ORGAO_REGISTRO;}
            set {_CNPJ_ORGAO_REGISTRO = value;}
        }
        public string Protocolo
        {
            get { return _NroProtocolo;}
            set { _NroProtocolo = value;}
        }
        public string Requerimento
        {
            get { return _requerimento; }
            set { _requerimento = value; }
        }
        public int SqPessoa
        {
            get {return _SqPessoa;}
            set {_SqPessoa = value;}
        }
        public DateTime DataEntrada
        {
            get { return _DataEntrada;}
            set { _DataEntrada = value;}
        }
        public DateTime DataAverbacao
        {
            get { return _DataAverbacao;}
            set {_DataAverbacao = value;}
        }
        public string NroProtocoloViabilidade
        {
            get {return _NroProtocoloViabilidade;}
            set {_NroProtocoloViabilidade = value;}
        }
        public string NroDOCAD
        {
            get { return _NroDOCAD;}
            set {_NroDOCAD = value;}
        }
        public string NroDBE
        {
            get { return _NroDBE;}
            set { _NroDBE = value;}
        }
        public decimal t005_xl_docad
        {
            get { return _t005_xl_docad;}
            set { _t005_xl_docad = value;}
        }
        public decimal t005_xl_dbe
        {
            get { return _t005_xl_dbe;}
            set { _t005_xl_dbe = value;}
        }
        #endregion 
        
        public void Update()
        { 
            //

            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    p.MainConnectionProvider = cp;
                    p.t004_nr_cnpj_org_reg = _CNPJ_ORGAO_REGISTRO;
                    p.t005_dt_averbacao = _DataAverbacao;
                    p.t005_dt_entrada = _DataEntrada;
                    p.t005_nr_docad = _NroDOCAD;
                    p.t005_nr_protocolo = _NroProtocolo;
                    p.t005_nr_protocolo_viabilidade = _NroProtocoloViabilidade;
                    p.t005_xl_dbe = _t005_xl_dbe;
                    p.t005_xl_docad = _t005_xl_docad;
                    p.Update();
                    cp.CommitTransaction();
                }
                //
            }
        }

        public void UpdateProtocoloOrgaoRegistro(string CnpjOrgaoRegistro, string pRequerimento, string pProtocoloOrgaoRegistro)
        {
            using (dT005_Protocolo c = new dT005_Protocolo())
            {
                c.UpdateProtocoloOrgaoRegistro(CnpjOrgaoRegistro, pRequerimento, pProtocoloOrgaoRegistro);
            }
        }

        #region Consultas

        public static DataTable ListaExaminador()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.ListaExaminador();
            }
        }
        public static DataTable ListaSituacao()
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.ListaSituacao();
            }
        }
        public static DataTable ListaProtocolo(params object[] param)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.ListaProtocolo(param);
            }
        }
        public static string FindRequerimentoByProtocolo(string _protcolo)
        {
            using (dT005_Protocolo p = new dT005_Protocolo())
            {
                return p.BuscaPorProcessoJunta(_protcolo);
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion
    }
}
