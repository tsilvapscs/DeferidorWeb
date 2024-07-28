using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;

namespace RCPJ.BLL
{
    [Serializable]
    public class bContador : DBInteractionBase
    {
        #region Class Member Declarations
        private int _SqEmpresa;
        private int _SQPessoa;
        private string _requerimento;
        private string _cnpjOrgaoRegistro;


        private String _crc = string.Empty;
        private String _ufCrc = string.Empty;

        private String _Email;
        private String _Nome;
        private String _CPFCNPJ;

        private Nullable<DateTime> _DataEntrada;

        private String _EndCEP;
        private String _EndPais;
        private String _EndUF;
        private String _EndMunicipio;
        private String _EndCodMunicipio;


        private String _EndBairro;
        private String _EndTipoLogradouro;
        private string _EndDsTipoLogradouro;
        private String _EndLogradouro;
        private String _EndNumero;
        private String _EndComplemento;
        private String _DDD;
        private String _Telefone;


        #endregion

        #region Class Property Declarations

        public string CnpjOrgaoRegistro
        {
            get { return _cnpjOrgaoRegistro; }
            set { _cnpjOrgaoRegistro = value; }
        }
        public string Requerimento
        {
            get { return _requerimento; }
            set { _requerimento = value; }
        }


        public int SqEmpresa
        {
            get { return _SqEmpresa; }
            set { _SqEmpresa = value; }
        }
        public int SQPessoa
        {
            get { return _SQPessoa; }
            set { _SQPessoa = value; }
        }

        public String Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public String CRC
        {
            get { return _crc; }
            set { _crc = value; }
        }
        public String UfCrc
        {
            get { return _ufCrc; }
            set { _ufCrc = value; }
        }
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public Nullable<DateTime> DataEntrada
        {
            get { return _DataEntrada; }
            set { _DataEntrada = value; }
        }
        public string EndDsTipoLogradouro
        {
            get { return _EndDsTipoLogradouro; }
            set { _EndDsTipoLogradouro = value; }
        }
        public string EndPais
        {
            get { return _EndPais; }
            set { _EndPais = value; }
        }
        public String DDD
        {
            get { return _DDD; }
            set { _DDD = value; }
        }
        public String Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value; }
        }

        public String CPFCNPJ
        {
            get { return _CPFCNPJ.Trim(); }
            set { _CPFCNPJ = value; }
        }

        public String EndCEP
        {
            get { return _EndCEP; }
            set { _EndCEP = value; }
        }

        public String EndUF
        {
            get { return _EndUF; }
            set { _EndUF = value; }
        }

        public String EndMunicipio
        {
            get { return _EndMunicipio; }
            set { _EndMunicipio = value; }
        }

        public String EndCodMunicipio
        {
            get { return _EndCodMunicipio; }
            set { _EndCodMunicipio = value; }
        }

        public String EndBairro
        {
            get { return _EndBairro; }
            set { _EndBairro = value; }
        }

        public String EndTipoLogradouro
        {
            get { return _EndTipoLogradouro; }
            set { _EndTipoLogradouro = value; }
        }

        public String EndLogradouro
        {
            get { return _EndLogradouro; }
            set { _EndLogradouro = value; }
        }

        public String EndNumero
        {
            get { return _EndNumero; }
            set { _EndNumero = value; }
        }

        public String EndComplemento
        {
            get { return _EndComplemento; }
            set { _EndComplemento = value; }
        }

        #endregion

        #region Constructors
        public bContador()
        {
            InitClass();
        }

        public bContador(string pProtocolo)
            : this()
        {
            _requerimento = pProtocolo;
            Populate();
        }

        private void InitClass()
        {
            _SQPessoa = 0;
            _Nome = string.Empty;
            _CPFCNPJ = string.Empty;
            _crc = string.Empty;
            _ufCrc = string.Empty;
            _Email = string.Empty;
            _Telefone = string.Empty;
            //                 dr["a004_co_pais"].ToString();
            _EndCEP = string.Empty;
            _EndMunicipio = string.Empty;
            _EndCodMunicipio = string.Empty;
            _EndBairro = string.Empty;
            _EndTipoLogradouro = string.Empty;
            _EndLogradouro = string.Empty;
            _EndNumero = string.Empty;
            _EndComplemento = string.Empty;
        }
        private void Populate()
        {

            if (_requerimento == string.Empty)
                return;

            DataTable dt = Query();

            foreach (DataRow dr in dt.Rows)
            {
                _SQPessoa = Int32.Parse(dr["T001_SQ_PESSOA"].ToString());
                _Nome = dr["T001_DS_PESSOA"].ToString();
                _CPFCNPJ = dr["T006_NR_CPF_CONTADOR"].ToString();
                _crc = dr["T006_NR_CRC_CONTADOR"].ToString();
                _ufCrc = dr["A004_CO_UF"].ToString();
                _Email = dr["T001_EMAIL"].ToString();
                _Telefone = dr["T001_TEL_1"].ToString();
                _EndUF = dr["r002_uf"].ToString();
                _EndCEP = dr["r002_nr_cep"].ToString();
                _EndCodMunicipio = dr["a005_co_municipio"].ToString();
                _EndBairro = dr["r002_ds_bairro"].ToString();
                _EndDsTipoLogradouro = dr["a015_ds_tipo_logradouro"].ToString();
                _EndLogradouro = dr["r002_ds_logradouro"].ToString();
                _EndNumero = dr["r002_nr_logradouro"].ToString();
                _EndComplemento = dr["r002_ds_complemento"].ToString();
                _DataEntrada = DateTime.Parse(dr["R001_DT_ENTRADA_VINCULO"].ToString());
            }

        }

        #endregion

        #region Data Acces
        public DataTable Query()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"SELECT pe.T001_SQ_PESSOA
                                 , pe.T001_DS_PESSOA
                                 , pr.T006_NR_CPF_CONTADOR
                                 , pr.T006_NR_CRC_CONTADOR
                                 , pr.A004_CO_UF
                                 , pe.T001_EMAIL
                                 , pe.T001_TEL_1
                                 , v.a004_co_pais
                                 , v.r002_uf
                                 , v.r002_nr_cep
                                 , v.a005_co_municipio
                                 , v.r002_ds_bairro
                                 , v.a015_ds_tipo_logradouro
                                 , v.r002_ds_logradouro
                                 , v.r002_nr_logradouro
                                 , v.r002_ds_complemento
                                 , vp.R001_DT_ENTRADA_VINCULO
                            FROM t005_protocolo p 
                                 INNER JOIN r001_vinculo vp ON p.T001_SQ_PESSOA = vp.T001_SQ_PESSOA_PAI 
                                 INNER JOIN t001_pessoa pe ON vp.T001_SQ_PESSOA = pe.T001_SQ_PESSOA 
                                 LEFT JOIN t006_protocolo_requerimento pr ON p.T005_NR_PROTOCOLO = pr.T005_NR_PROTOCOLO 
                                 LEFT JOIN r002_vinculo_endereco v ON pe.T001_SQ_PESSOA = v.t001_sq_pessoa
                            WHERE p.T005_NR_PROTOCOLO = '" + _requerimento + "'");
            sql.AppendLine("        AND vp.A009_CO_CONDICAO = 99");

            return dHelperQuery.ExecuteQuery(sql.ToString());

        }

        public void Update()
        {
            try
            {
                using (ConnectionProvider cp = new ConnectionProvider())
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();
                    #region 1. GravandoPessoa
                    using (dT001_Pessoa p = new dT001_Pessoa())
                    {

                        p.MainConnectionProvider = cp;
                        p.t001_in_tipo_pessoa = "F";
                        p.t001_ds_pessoa = _Nome;
                        p.t001_nome_fantasia = "";
                        p.t001_in_dados_atualizados = "S";
                        p.t001_dt_ult_atualizacao = DateTime.Now;
                        p.t001_email = _Email;
                        p.t001_ddd = _DDD;
                        p.t001_tel_1 = _Telefone;
                        p.t001_tel_2 = "";
                        p.t001_sq_pessoa = _SQPessoa;
                        _SQPessoa = p.Update();

                    }
                    #endregion

                    #region Gravando Vinculo
                    using (dR001_Vinculo v = new dR001_Vinculo())
                    {
                        v.MainConnectionProvider = cp;
                        v.t001_sq_pessoa = _SQPessoa;
                        v.t001_sq_pessoa_pai = SqEmpresa;
                        v.a009_co_condicao = 99; // Contador
                        v.r001_dt_entrada_vinculo = _DataEntrada;
                        v.r001_ds_cargo_direcao = "CONTADOR";
                        v.r001_in_situacao = "A";
                        v.r001_vl_participacao = 0;
                        v.Update();
                    }
                    #endregion

                    #region 3.Gravando Vinculo Endereço
                    //(dados do endereço da Empresa)

                    using (dR002_Vinculo_Endereco ve = new dR002_Vinculo_Endereco())
                    {
                        int SqVinculoEndereco;
                        ve.MainConnectionProvider = cp;
                        ve.t001_sq_pessoa = _SQPessoa;
                        ve.t001_sq_pessoa_pai = SqEmpresa;
                        ve.a015_ds_tipo_logradouro = _EndDsTipoLogradouro;
                        ve.r002_ds_logradouro = _EndLogradouro;
                        ve.r002_nr_logradouro = _EndNumero;
                        ve.r002_ds_complemento = _EndComplemento;
                        ve.r002_ds_bairro = _EndBairro;
                        ve.a005_co_municipio = decimal.Parse(_EndCodMunicipio);
                        ve.a004_co_pais = 154;
                        ve.r002_nr_cep = _EndCEP;
                        ve.R002_uf = _EndUF;

                        SqVinculoEndereco = ve.Update();
                    }
                    #endregion

                    #region 5.Gravando Protocolo Requerimento
                    //(dados complementares Requerimento)

                    using (dT006_Protocolo_Requerimento pr = new dT006_Protocolo_Requerimento())
                    {
                        pr.MainConnectionProvider = cp;

                        pr.t004_nr_cnpj_org_reg = _cnpjOrgaoRegistro;
                        pr.t005_nr_protocolo = _requerimento;

                        pr.t006_ds_nome_contador = _Nome;
                        pr.t006_nr_cpf_contador = _CPFCNPJ;
                        pr.t006_nr_crc_contador = _crc;
                        pr.a004_co_uf = _ufCrc;

                        pr.Update();
                    }
                    #endregion

                    cp.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar gravar o Requerimento, por favor tentar de novo, se o erro persistir contatar suporte " + ex.Message);
            }
        }

        public void Delete()
        {

        }
        #endregion

        #region Validacao
        public bool IsValid()
        {

            if (_crc == "")
                return false;
            if (_ufCrc == "")
                return false;
            if (_Nome == "")
                return false;
            if (_CPFCNPJ == "")
                return false;
            if (_EndCEP == "")
                return false;
            if (_EndUF == "")
                return false;
            if (_EndCodMunicipio == "")
                return false;
            if (_EndBairro == "")
                return false;
            if (_EndDsTipoLogradouro == "")
                return false;
            if (_EndLogradouro == "")
                return false;
            if (_EndNumero == "")
                return false;
            return true;
        }
        #endregion
    }
}
