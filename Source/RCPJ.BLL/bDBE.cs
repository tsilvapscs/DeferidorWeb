using System;
using System.Collections.Generic;
using System.Text;
using RCPJ.DAL.ConnectionBase;
using System.Data;


namespace RCPJ.BLL
{
    [Serializable]
    public class bDBE : DBInteractionBase
    {
        

        #region Dados da Empresa
        private String _nrMatricula = string.Empty;

        public String NrMatricula
        {
            get { return _nrMatricula; }
            set { _nrMatricula = value; }
        }
        private String _nrEmpresaCNPJ = string.Empty;

        public String NrEmpresaCNPJ
        {
            get { return _nrEmpresaCNPJ; }
            set { _nrEmpresaCNPJ = value; }
        }
        private string _DDDEmpresa = string.Empty;

        public string DDDEmpresa
        {
            get { return _DDDEmpresa; }
            set { _DDDEmpresa = value; }
        }
        private string _TelefoneEmpresa = string.Empty;

        public string TelefoneEmpresa
        {
            get { return _TelefoneEmpresa; }
            set { _TelefoneEmpresa = value; }
        }
        private int _Enquadramento;

        public int Enquadramento
        {
            get { return _Enquadramento; }
            set { _Enquadramento = value; }
        }
        private String _SedeNome = string.Empty;

        public String SedeNome
        {
            get { return _SedeNome; }
            set { _SedeNome = value; }
        }
        private string _SedeEmail = string.Empty;

        public string SedeEmail
        {
            get { return _SedeEmail; }
            set { _SedeEmail = value; }
        }
        private string _Nome_Fantasia = string.Empty;

        public string Nome_Fantasia
        {
            get { return _Nome_Fantasia; }
            set { _Nome_Fantasia = value; }
        }
        private String _ObjetoSocial = string.Empty;

        public String ObjetoSocial
        {
            get { return _ObjetoSocial; }
            set { _ObjetoSocial = value; }
        }
        private String _ArtigoEstatuto = string.Empty;

        public String ArtigoEstatuto
        {
            get { return _ArtigoEstatuto; }
            set { _ArtigoEstatuto = value; }
        }
        private String _CNPJ_Orgao_Registro = string.Empty;

        public String CNPJ_Orgao_Registro
        {
            get { return _CNPJ_Orgao_Registro; }
            set { _CNPJ_Orgao_Registro = value; }
        }
        private decimal _CapitalSocial = 0;

        public decimal CapitalSocial
        {
            get { return _CapitalSocial; }
            set { _CapitalSocial = value; }
        }
        private decimal _ValorCota = 0;

        public decimal ValorCota
        {
            get { return _ValorCota; }
            set { _ValorCota = value; }
        }
        private decimal _QtdCotas = 0;

        public decimal QtdCotas
        {
            get { return _QtdCotas; }
            set { _QtdCotas = value; }
        }
        private decimal _capital_integralizado = 0;

        public decimal Capital_integralizado
        {
            get { return _capital_integralizado; }
            set { _capital_integralizado = value; }
        }
        private decimal _capital_nao_integralizado = 0;

        public decimal Capital_nao_integralizado
        {
            get { return _capital_nao_integralizado; }
            set { _capital_nao_integralizado = value; }
        }
        private Nullable<DateTime> _data_limite_integralizacao;

        public Nullable<DateTime> Data_limite_integralizacao
        {
            get { return _data_limite_integralizacao; }
            set { _data_limite_integralizacao = value; }
        }
        private string _ds_capital_nao_integralizado = "";

        public string Ds_capital_nao_integralizado
        {
            get { return _ds_capital_nao_integralizado; }
            set { _ds_capital_nao_integralizado = value; }
        }
        private int _SedeSituacao = 0;

        public int SedeSituacao
        {
            get { return _SedeSituacao; }
            set { _SedeSituacao = value; }
        }



        private Nullable<DateTime> _DataInicioSociedade;

        public Nullable<DateTime> DataInicioSociedade
        {
            get { return _DataInicioSociedade; }
            set { _DataInicioSociedade = value; }
        }
        private Nullable<DateTime> _DuracaoSociedade;

        public Nullable<DateTime> DuracaoSociedade
        {
            get { return _DuracaoSociedade; }
            set { _DuracaoSociedade = value; }
        }

        private string _t005_protocolo_orgao_origem = string.Empty;

        public string T005_protocolo_orgao_origem
        {
            get { return _t005_protocolo_orgao_origem; }
            set { _t005_protocolo_orgao_origem = value; }
        }
        private String _ProtocoloRCPJ = string.Empty;

        public String ProtocoloRCPJ
        {
            get { return _ProtocoloRCPJ; }
            set { _ProtocoloRCPJ = value; }
        }
        private int _T003_IND_CNAE_DESTACADA = 2;

        public int T003_IND_CNAE_DESTACADA
        {
            get { return _T003_IND_CNAE_DESTACADA; }
            set { _T003_IND_CNAE_DESTACADA = value; }
        }
        private string _t005_uf_origem = string.Empty;

        public string T005_uf_origem
        {
            get { return _t005_uf_origem; }
            set { _t005_uf_origem = value; }
        }
        private int _empresaUnipessoal = 2;

        public int EmpresaUnipessoal
        {
            get { return _empresaUnipessoal; }
            set { _empresaUnipessoal = value; }
        }


        #endregion

        private List<bSocios> _qsa = new List<bSocios>();

        public List<bSocios> QSA
        {
            get { return _qsa; }
            set { _qsa = value; }
        }

    }
}
