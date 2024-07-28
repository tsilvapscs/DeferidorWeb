using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using System.Text.RegularExpressions;

namespace RCPJ.BLL
{
    [Serializable]
    public class bReqGenProtocolo
    {
        #region  Property Declarations
        private string _T005_NR_PROTOCOLO;
        private int _T099_IN_TIPO_ENQUADRAMENTO = 0;
        private int _T009_CO_PORTE = 0;
        private string _T001_DS_PESSOA_ANT = "";

        private int _T009_TIP_ESTAB_RFB = 0;
        private string _T009_NR_ULT_REGISTRO = "";
        private Nullable<DateTime> _T009_DT_ULT_REGISTRO;
        private string _T009_NR_CNPJ_MATRIZ = "";
        private string _T009_UF_MATRIZ = "";
        private int _T009_TIPO_UNIDADE = 0;
        private string _T009_NR_CNPJ_ORG_REG = "";
        private string _T009_NR_NIRE_MATRIZ = "";

        private int _T009_IN_FORMA_ATUACAO = 0;
        private int _T009_IN_CENTRO_DISTRIBUICAO = 0;
        private int _T009_IN_FRANQUEADO = 0;
        private string _T009_NR_CNPJ_FRANQUEADOR = "";
        private string _T009_NR_ATO_LEGAL = "";

        private int _T009_IN_ATIV_AGROINDUSTRIA = 2;
        private int _T009_TIPO_PROPRIEDADE = 0;
        private int _T009_TIP_INSCRICAO_ST = 0;
        private string _T009_NR_CONVENIO = "";
        private int _T009_TP_ATO_LEGAL = 0;
        private string _T009_TIPO_FORMA_OUTROS = "";
        private string _T009_COORDENADAS = "";
        private decimal _T009_VL_PATRIMONIO = 0;
        private int _T009_IN_CANTEIRO_OBRAS = 2;
        private int _T009_IN_TRANSPORTE = 2;
        private int _T009_IN_COMERCIO_EXTERIOR = 0;
        private int _T009_IN_SUBSTITUTO_TRIB = 2;
       
        #endregion

        #region Class Member Declarations
        public string T005_NR_PROTOCOLO
        {
            get { return _T005_NR_PROTOCOLO; }
            set { _T005_NR_PROTOCOLO = value; }
        }
        public int T099_IN_TIPO_ENQUADRAMENTO
        {
            get { return _T099_IN_TIPO_ENQUADRAMENTO; }
            set { _T099_IN_TIPO_ENQUADRAMENTO = value; }
        }
        public int T009_CO_PORTE
        {
            get { return _T009_CO_PORTE; }
            set { _T009_CO_PORTE = value; }
        }
        public string T001_DS_PESSOA_ANT
        {
            get { return _T001_DS_PESSOA_ANT; }
            set { _T001_DS_PESSOA_ANT = value; }
        }
        public int T009_TIP_ESTAB_RFB
        {
            get { return _T009_TIP_ESTAB_RFB; }
            set { _T009_TIP_ESTAB_RFB = value; }
        }
        public string T009_NR_ULT_REGISTRO
        {
            get { return _T009_NR_ULT_REGISTRO; }
            set { _T009_NR_ULT_REGISTRO = value; }
        }
        public Nullable<DateTime> T009_DT_ULT_REGISTRO
        {
            get { return _T009_DT_ULT_REGISTRO; }
            set { _T009_DT_ULT_REGISTRO = value; }
        }
        public string T009_NR_CNPJ_MATRIZ
        {
            get { return _T009_NR_CNPJ_MATRIZ; }
            set { _T009_NR_CNPJ_MATRIZ = value; }
        }
        public string T009_UF_MATRIZ
        {
            get { return _T009_UF_MATRIZ; }
            set { _T009_UF_MATRIZ = value; }
        }
        public int T009_TIPO_UNIDADE
        {
            get { return _T009_TIPO_UNIDADE; }
            set { _T009_TIPO_UNIDADE = value; }
        }
        public string T009_NR_CNPJ_ORG_REG
        {
            get { return _T009_NR_CNPJ_ORG_REG; }
            set { _T009_NR_CNPJ_ORG_REG = value; }
        }
        public string T009_NR_NIRE_MATRIZ
        {
            get { return _T009_NR_NIRE_MATRIZ; }
            set { _T009_NR_NIRE_MATRIZ = value; }
        }
        public int T009_IN_FORMA_ATUACAO
        {
            get { return _T009_IN_FORMA_ATUACAO; }
            set { _T009_IN_FORMA_ATUACAO = value; }
        }

        public int T009_IN_CENTRO_DISTRIBUICAO
        {
            get { return _T009_IN_CENTRO_DISTRIBUICAO; }
            set { _T009_IN_CENTRO_DISTRIBUICAO = value; }
        }

        public int T009_IN_FRANQUEADO
        {
            get { return _T009_IN_FRANQUEADO; }
            set { _T009_IN_FRANQUEADO = value; }
        }

        public string T009_NR_CNPJ_FRANQUEADOR
        {
            get { return _T009_NR_CNPJ_FRANQUEADOR; }
            set { _T009_NR_CNPJ_FRANQUEADOR = value; }
        }
        public string T009_NR_ATO_LEGAL
        {
            get { return _T009_NR_ATO_LEGAL; }
            set { _T009_NR_ATO_LEGAL = value; }
        }
        public int T009_IN_ATIV_AGROINDUSTRIA
        {
            get { return _T009_IN_ATIV_AGROINDUSTRIA; }
            set { _T009_IN_ATIV_AGROINDUSTRIA = value; }
        }
        public int T009_TIPO_PROPRIEDADE
        {
            get { return _T009_TIPO_PROPRIEDADE; }
            set { _T009_TIPO_PROPRIEDADE = value; }
        }
        public int T009_TIP_INSCRICAO_ST
        {
            get { return _T009_TIP_INSCRICAO_ST; }
            set { _T009_TIP_INSCRICAO_ST = value; }
        }
        public string T009_NR_CONVENIO
        {
            get { return _T009_NR_CONVENIO; }
            set { _T009_NR_CONVENIO = value; }
        }
        public int T009_TP_ATO_LEGAL
        {
            get { return _T009_TP_ATO_LEGAL; }
            set { _T009_TP_ATO_LEGAL = value; }
        }
        public string T009_COORDENADAS
        {
            get { return _T009_COORDENADAS; }
            set { _T009_COORDENADAS = value; }
        }

        public string T009_TIPO_FORMA_OUTROS
        {
            get { return _T009_TIPO_FORMA_OUTROS; }
            set { _T009_TIPO_FORMA_OUTROS = value; }
        }
        public decimal T009_VL_PATRIMONIO
        {
            get { return _T009_VL_PATRIMONIO; }
            set { _T009_VL_PATRIMONIO = value; }
        }
        public int T009_IN_CANTEIRO_OBRAS
        {
            get { return _T009_IN_CANTEIRO_OBRAS; }
            set { _T009_IN_CANTEIRO_OBRAS = value; }
        }
        public int T009_IN_TRANSPORTE
        {
            get { return _T009_IN_TRANSPORTE; }
            set { _T009_IN_TRANSPORTE = value; }
        }
        public int T009_IN_COMERCIO_EXTERIOR
        {
            get { return _T009_IN_COMERCIO_EXTERIOR; }
            set { _T009_IN_COMERCIO_EXTERIOR = value; }
        }
        public int T009_IN_SUBSTITUTO_TRIB
        {
            get { return _T009_IN_SUBSTITUTO_TRIB; }
            set { _T009_IN_SUBSTITUTO_TRIB = value; }
        }

        #endregion

        
        #region Constructors
        public bReqGenProtocolo()
        {
            //InitClass();
        }
       
        public bReqGenProtocolo(string pProtocolo)
            : this()
        {
            _T005_NR_PROTOCOLO = pProtocolo;
            Populate();
        }
        #endregion

        private void Populate()
        {

            if (_T005_NR_PROTOCOLO == string.Empty)
                return;
            dT099_req_gen_protocolo reqGen = new dT099_req_gen_protocolo();
            reqGen.T005_NR_PROTOCOLO =_T005_NR_PROTOCOLO;

            DataTable dt = reqGen.Query();

            foreach (DataRow dr in dt.Rows)
            {
                _T099_IN_TIPO_ENQUADRAMENTO = Int32.Parse(dr["T099_IN_TIPO_ENQUADRAMENTO"].ToString());
                _T009_CO_PORTE = Int32.Parse(dr["T009_CO_PORTE"].ToString());
                _T001_DS_PESSOA_ANT = dr["T001_DS_PESSOA_ANT"].ToString();

                _T009_TIP_ESTAB_RFB = Int32.Parse(dr["T009_TIP_ESTAB_RFB"].ToString());
                _T009_NR_ULT_REGISTRO = dr["T009_NR_ULT_REGISTRO"].ToString();
                if (dr["T009_DT_ULT_REGISTRO"].ToString() == string.Empty || dr["T009_DT_ULT_REGISTRO"].ToString() == null)
                    _T009_DT_ULT_REGISTRO = null;
                else
                    _T009_DT_ULT_REGISTRO = Convert.ToDateTime(dr["T009_DT_ULT_REGISTRO"].ToString());

                _T009_NR_CNPJ_MATRIZ = dr["T009_NR_CNPJ_MATRIZ"].ToString();
                _T009_UF_MATRIZ = dr["T009_UF_MATRIZ"].ToString();
                _T009_TIPO_UNIDADE = Int32.Parse(dr["T009_TIPO_UNIDADE"].ToString());
                _T009_NR_CNPJ_ORG_REG = dr["T009_NR_CNPJ_ORG_REG"].ToString();
                _T009_NR_NIRE_MATRIZ = dr["T009_NR_NIRE_MATRIZ"].ToString();

                _T009_IN_FORMA_ATUACAO = Int32.Parse(dr["T009_IN_FORMA_ATUACAO"].ToString());
                _T009_IN_CENTRO_DISTRIBUICAO = Int32.Parse(dr["T009_IN_CENTRO_DISTRIBUICAO"].ToString());
                _T009_IN_FRANQUEADO = Int32.Parse(dr["T009_IN_FRANQUEADO"].ToString());
                _T009_NR_CNPJ_FRANQUEADOR = dr["T009_NR_CNPJ_FRANQUEADOR"].ToString();
                _T009_NR_ATO_LEGAL = dr["T009_NR_ATO_LEGAL"].ToString();
                _T009_IN_ATIV_AGROINDUSTRIA = Int32.Parse(dr["T009_IN_ATIV_AGROINDUSTRIA"].ToString());
                _T009_TIPO_PROPRIEDADE = Int32.Parse(dr["T009_TIP_PROPRIEDADE"].ToString());

                _T009_NR_CONVENIO = dr["T009_NR_CONVENIO"].ToString();
                _T009_TIP_INSCRICAO_ST = Int32.Parse(dr["T009_TIP_INSCRICAO_ST"].ToString());
                _T009_TP_ATO_LEGAL = Int32.Parse(dr["T009_TP_ATO_LEGAL"].ToString());
                _T009_TIPO_FORMA_OUTROS = dr["T009_TIPO_UNIDADE_OUTROS"].ToString();
                _T009_COORDENADAS = dr["T009_COORDENADAS"].ToString();
                _T009_VL_PATRIMONIO = Decimal.Parse(dr["T009_VL_PATRIMONIO"].ToString());
                _T009_IN_CANTEIRO_OBRAS = Int32.Parse(dr["T009_IN_CANTEIRO_OBRAS"].ToString());
                _T009_IN_TRANSPORTE = Int32.Parse(dr["T009_IN_TRANSPORTE"].ToString());
                _T009_IN_COMERCIO_EXTERIOR = Int32.Parse(dr["T009_IN_COMERCIO_EXTERIOR"].ToString());
                _T009_IN_SUBSTITUTO_TRIB = Int32.Parse(dr["T009_IN_SUBSTITUTO_TRIB"].ToString());
            }
           
        }
        private int GetTipoEnquadramento()
        {
            int ret = 0;
            //if (rdbEnquadraME.Checked || rdbEnquadraEPP.Checked)
            //    ret = 1;
            //if (rdbMEEPP.Checked)
            //    ret = 2;
            //if (rdbEPPME.Checked)
            //    ret = 3;
            //if (rdbDesenquadraME.Checked)
            //    ret = 4;
            //if (rdbDesenquadraEPP.Checked)
            //    ret = 5;

            return ret;

        }
        public void Update()
        {
            using (dT099_req_gen_protocolo dal = new dT099_req_gen_protocolo())
            {
                dal.T005_NR_PROTOCOLO = _T005_NR_PROTOCOLO;
                dal.T099_IN_TIPO_ENQUADRAMENTO = _T099_IN_TIPO_ENQUADRAMENTO;
                dal.T009_CO_PORTE = _T009_CO_PORTE;
                dal.T001_DS_PESSOA_ANT = _T001_DS_PESSOA_ANT;

                dal.T009_TIP_ESTAB_RFB = _T009_TIP_ESTAB_RFB;
                dal.T009_NR_ULT_REGISTRO = _T009_NR_ULT_REGISTRO;
                dal.T009_DT_ULT_REGISTRO = _T009_DT_ULT_REGISTRO;
                dal.T009_NR_CNPJ_MATRIZ = _T009_NR_CNPJ_MATRIZ;
                dal.T009_UF_MATRIZ = _T009_UF_MATRIZ;
                dal.T009_TIPO_UNIDADE = _T009_TIPO_UNIDADE;
                dal.T009_NR_CNPJ_ORG_REG = _T009_NR_CNPJ_ORG_REG;
                dal.T009_NR_NIRE_MATRIZ = _T009_NR_NIRE_MATRIZ;


                dal.T009_IN_FORMA_ATUACAO = _T009_IN_FORMA_ATUACAO;
                dal.T009_IN_CENTRO_DISTRIBUICAO = _T009_IN_CENTRO_DISTRIBUICAO;
                dal.T009_IN_FRANQUEADO = _T009_IN_FRANQUEADO;
                dal.T009_NR_CNPJ_FRANQUEADOR = _T009_NR_CNPJ_FRANQUEADOR;
                dal.T009_NR_ATO_LEGAL = _T009_NR_ATO_LEGAL;
                dal.T009_IN_ATIV_AGROINDUSTRIA = _T009_IN_ATIV_AGROINDUSTRIA;
                dal.T009_TIPO_PROPRIEDADE = _T009_TIPO_PROPRIEDADE;

                dal.T009_NR_CONVENIO = _T009_NR_CONVENIO;
                dal.T009_TIP_INSCRICAO_ST = _T009_TIP_INSCRICAO_ST;
                dal.T009_TP_ATO_LEGAL = _T009_TP_ATO_LEGAL;
                dal.T009_TIPO_FORMA_OUTROS = _T009_TIPO_FORMA_OUTROS;
                dal.T009_COORDENADAS = _T009_COORDENADAS;
                dal.T009_VL_PATRIMONIO = _T009_VL_PATRIMONIO;
                dal.T009_IN_CANTEIRO_OBRAS = _T009_IN_CANTEIRO_OBRAS;
                dal.T009_IN_TRANSPORTE = _T009_IN_TRANSPORTE;
                dal.T009_IN_COMERCIO_EXTERIOR = _T009_IN_COMERCIO_EXTERIOR;
                dal.T009_IN_SUBSTITUTO_TRIB = _T009_IN_SUBSTITUTO_TRIB;
                dal.Update();
            }
        }
       
    }
}
