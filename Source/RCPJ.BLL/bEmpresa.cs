using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using RCPJ.DAL.ConnectionBase;
using RCPJ.DAL.Entities;
using RCPJ.DAL.Helper;
using System.Globalization;
namespace RCPJ.BLL
{
    [Serializable]
    public class bEmpresa : DBInteractionBase 
    {
        #region Dados da Empresa
        private int _sqEmpresa = 0;
        private String _nome = string.Empty;
        private string _nomeFantasia = string.Empty;
        private String _matricula = string.Empty;
        private String _CNPJ = string.Empty;
        private string _ddd = string.Empty;
        private string _telefone = string.Empty;
        private string _email = string.Empty;
        private int _Enquadramento;
        private string _porte = string.Empty;
        private String _objetoSocial = string.Empty;
        private String _cnpjOrgaoRegistro = string.Empty;
        private decimal _capitalSocial;
        private decimal _valorCota;
        private decimal _qtdCotas;
        private decimal _capitalIntegralizado;
        private decimal _capitalNaoItegralizado;
        private Nullable<DateTime> _dataLimiteIntegralizacao;
        private string _dsCapitalNaoIntegralizado = "";
        private string _descricaoReducaoCapital = "";
        private int _situacao;
        private string _situacaoDescricao;
        private int _naturezaJuridicaOR = 1;
        private Nullable<DateTime> _DataInicioSociedade;
        private Nullable<DateTime> _DuracaoSociedade;
        private Nullable<DateTime> _DataConstituicao;
        private Nullable<DateTime> _dataTerminoAtividade;
        private int _indicadorSPE = 2;
        private string _t005_protocolo_orgao_origem = string.Empty;
        private String _protocoloRCPJ = string.Empty;
        private int _T003_IND_CNAE_DESTACADA = 2;
        private string _t005UfOrigem = string.Empty;
        private int _empresaUnipessoal = 2;
        private int _integralizandoCapital = 2;
        private string _iptu = "";
        private decimal _areaUtilizada = 0;
        private int _prossuiEstabelecimento = 1;
        private int _indicadorMatriz = 1;
        private int _inReducaoCapital = 2;
        private int _filialComSedeFora = 2;
        private string _obrigacoesSociais = "";
        private string _codigoDBE = "";
        private string _protocoloViabilidade = "";
        private decimal _tipoPessoaJuridicaCodigo = 0;
        private decimal _naturezaJuridicaCodigo = 0;
        private string _naturezaJuridicaDescricao = string.Empty;
        private string _moedaCorrente = "";
        private Nullable<DateTime> _AssociacaoTempoDuracao;
        private string _tipoPessoaJuridicaDescricao = string.Empty;
        private int _eventoConsolidacao = 2;
        private int _eventoReativacao = 2;
        private int _eventoReRatificacao = 2;
        #endregion

        #region Class Property Declarations
        public int SqEmpresa
        {
            get { return _sqEmpresa; }
            set { _sqEmpresa = value; }
        }

        public String Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public String NomeFantasia
        {
            get { return _nomeFantasia; }
            set { _nomeFantasia = value; }
        }
        
        public String Matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
        }

        public String CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }

        public string Ddd
        {
            get { return _ddd; }
            set { _ddd = value; }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public int Enquadramento
        {
            get { return _Enquadramento; }
            set { _Enquadramento = value; }
        }

        public string Porte
        {
            get { return _porte; }
            set { _porte = value; }
        }

        public String ObjetoSocial
        {
            get { return _objetoSocial; }
            set { _objetoSocial = value; }
        }

        public String CnpjOrgaoRegistro
        {
            get { return _cnpjOrgaoRegistro; }
            set { _cnpjOrgaoRegistro = value; }
        }

        public decimal CapitalSocial
        {
            get { return _capitalSocial; }
            set { _capitalSocial = value; }
        }

        public decimal ValorCota
        {
            get { return _valorCota; }
            set { _valorCota = value; }
        }

        public decimal QtdCotas
        {
            get { return _qtdCotas; }
            set { _qtdCotas = value; }
        }

        public decimal CapitalIntegralizado
        {
            get { return _capitalIntegralizado; }
            set { _capitalIntegralizado = value; }
        }

        public decimal CapitalNaoItegralizado
        {
            get { return _capitalNaoItegralizado; }
            set { _capitalNaoItegralizado = value; }
        }

        public Nullable<DateTime> DataLimiteIntegralizacao
        {
            get { return _dataLimiteIntegralizacao; }
            set { _dataLimiteIntegralizacao = value; }
        }

        public string DsCapitalNaoIntegralizado
        {
            get { return _dsCapitalNaoIntegralizado; }
            set { _dsCapitalNaoIntegralizado = value; }
        }

        public string DescricaoReducaoCapital
        {
            get { return _descricaoReducaoCapital; }
            set { _descricaoReducaoCapital = value; }
        }

        public int Situacao
        {
            get { return _situacao; }
            set { _situacao = value; }
        }

        public string SituacaoDescricao
        {
            get { return _situacaoDescricao; }
            set { _situacaoDescricao = value; }
        }

        public int NaturezaJuridicaOR
        {
            get { return _naturezaJuridicaOR; }
            set { _naturezaJuridicaOR = value; }
        }

        public Nullable<DateTime> DataInicioSociedade
        {
            get { return _DataInicioSociedade; }
            set { _DataInicioSociedade = value; }
        }

        public Nullable<DateTime> DuracaoSociedade
        {
            get { return _DuracaoSociedade; }
            set { _DuracaoSociedade = value; }
        }

        public Nullable<DateTime> DataConstituicao
        {
            get { return _DataConstituicao; }
            set { _DataConstituicao = value; }
        }

        public Nullable<DateTime> DataTerminoAtividade
        {
            get { return _dataTerminoAtividade; }
            set { _dataTerminoAtividade = value; }
        }

        public int IndicadorSPE
        {
            get { return _indicadorSPE; }
            set { _indicadorSPE = value; }
        }
         public string T005_protocolo_orgao_origem
        {
            get { return _t005_protocolo_orgao_origem; }
            set { _t005_protocolo_orgao_origem = value; }
        }

        public String ProtocoloRCPJ
        {
            get { return _protocoloRCPJ; }
            set { _protocoloRCPJ = value; }
        }

        public int T003_IND_CNAE_DESTACADA
        {
            get { return _T003_IND_CNAE_DESTACADA; }
            set { _T003_IND_CNAE_DESTACADA = value; }
        }

        public string T005UfOrigem
        {
            get { return _t005UfOrigem; }
            set { _t005UfOrigem = value; }
        }

        public int EmpresaUnipessoal
        {
            get { return _empresaUnipessoal; }
            set { _empresaUnipessoal = value; }
        }

        public int IntegralizandoCapital
        {
            get { return _integralizandoCapital; }
            set { _integralizandoCapital = value; }
        }


        public string Iptu
        {
            get { return _iptu; }
            set { _iptu = value; }
        }

        public decimal AreaUtilizada
        {
            get { return _areaUtilizada; }
            set { _areaUtilizada = value; }
        }


        public int ProssuiEstabelecimento
        {
            get { return _prossuiEstabelecimento; }
            set { _prossuiEstabelecimento = value; }
        }

        public int IndicadorMatriz
        {
            get { return _indicadorMatriz; }
            set { _indicadorMatriz = value; }
        }

        public int InReducaoCapital
        {
            get { return _inReducaoCapital; }
            set { _inReducaoCapital = value; }
        }

        public int FilialComSedeFora
        {
            get { return _filialComSedeFora; }
            set { _filialComSedeFora = value; }
        }
        public string ObrigacoesSociais
        {
            get { return _obrigacoesSociais; }
            set { _obrigacoesSociais = value; }
        }

        public string DBE
        {
            get { return _codigoDBE; }
            set { _codigoDBE = value; }
        }
        public string ProtocoloViabilidade
        {
            get { return _protocoloViabilidade; }
            set { _protocoloViabilidade = value; }
        }
       
        public decimal TipoPessoaJuridicaCodigo
        {
            get { return _tipoPessoaJuridicaCodigo; }
            set { _tipoPessoaJuridicaCodigo = value; }
        }
        
        public decimal NaturezaJuridicaCodigo
        {
            get { return _naturezaJuridicaCodigo; }
            set { _naturezaJuridicaCodigo = value; }
        }
        
        public string MoedaCorrente
        {
            get { return _moedaCorrente; }
            set { _moedaCorrente = value; }
        }


        public Nullable<DateTime> AssociacaoTempoDuracao
        {
            get { return _AssociacaoTempoDuracao; }
            set { _AssociacaoTempoDuracao = value; }
        }

        public string NaturezaJuridicaDescricao
        {
            get { return _naturezaJuridicaDescricao; }
            set { _naturezaJuridicaDescricao = value; }
        }
        public String TipoPessoaJuridicaDescricao
        {
            get { return _tipoPessoaJuridicaDescricao; }
            set { _tipoPessoaJuridicaDescricao = value; }
        }
        public int EventoConsolidacao
        {
            get { return _eventoConsolidacao; }
            set { _eventoConsolidacao = value; }
        }
        public int EventoReativacao
        {
            get { return _eventoReativacao; }
            set { _eventoReativacao = value; }
        }
        public int EventoReRatificacao
        {
            get { return _eventoReRatificacao; }
            set { _eventoReRatificacao = value; }
        }
        #endregion

        public void Update()
        {
            int SqEmpresa = 0;


            using (ConnectionProvider cp = new ConnectionProvider())
            {
                cp.OpenConnection();
                cp.BeginTransaction();

                #region 1. GravandoPessoa Empresa
                using (dT001_Pessoa p = new dT001_Pessoa())
                {

                    p.MainConnectionProvider = cp;
                    p.t001_in_tipo_pessoa = "J";
                    p.t001_ds_pessoa = _nome;
                    p.t001_nome_fantasia = _nomeFantasia;
                    p.t001_in_dados_atualizados = "S";
                    p.t001_dt_ult_atualizacao = DateTime.Now;
                    p.t001_email = _email;
                    p.t001_ddd = _ddd;
                    p.t001_tel_1 = _telefone;
                    p.t001_tel_2 = "";
                    p.t001_sq_pessoa = _sqEmpresa;
                    _sqEmpresa = p.Update();

                }
                #endregion

                #region 2. Gravando Pessoa Jurídica (Requerimento)
                //(dados da pessoa juridica Empresa)

                using (dT003_Pessoa_Juridica pj = new dT003_Pessoa_Juridica())
                {
                    pj.MainConnectionProvider = cp;
                    pj.t001_sq_pessoa = _sqEmpresa;
                    pj.t004_nr_cnpj_org_reg = _cnpjOrgaoRegistro;


                    pj.t003_nr_matricula = _matricula;
                    pj.t003_nr_cnpj = _CNPJ;

                    if (_Enquadramento != 0)
                    {
                        pj.t003_tipo_enquadramento = _Enquadramento;
                    }

                    pj.t003_nr_inscricao_estadual = "";
                    pj.t003_nr_inscricao_municipal = "";
                    pj.t006_nr_cnpj_org_reg_ant = "";
                    pj.t003_nr_matricula_anterior = "";
                    pj.a006_co_natureza_juridica = _naturezaJuridicaCodigo;
                    pj.a011_co_porte = 0;
                    pj.a007_co_situacao = _situacao;
                    pj.T003_DS_SITUACAO = _situacaoDescricao;
                    pj.a008_co_status_atual = 0;
                    pj.t003_vl_capital_social = _capitalSocial;
                    pj.t003_vl_qtd_cotas = _qtdCotas;
                    pj.t003_vl_valor_cota = _valorCota;
                    pj.t003_vl_capital_integralizado = _capitalIntegralizado;
                    pj.t003_vl_capital_nao_integralizado = _capitalNaoItegralizado;
                    pj.t003_data_limite_integralizacao = _dataLimiteIntegralizacao;
                    pj.t003_ds_capital_nao_integralizado = _dsCapitalNaoIntegralizado;
                    pj.t003_moeda_corrente = _moedaCorrente;
                    pj.t003_co_tipo_pes_jur = _tipoPessoaJuridicaCodigo;
                    pj.t003_dt_inicio_atividade = _DataInicioSociedade;
                    pj.t003_dt_prazo_determinado = (_AssociacaoTempoDuracao);
                    pj.t003_dt_termino_ativ = _dataTerminoAtividade;
                    pj.t003_ds_objeto_social = _objetoSocial;
                    pj.t003_prot_viab = _protocoloViabilidade;
                    pj.t003_DBE = _codigoDBE;
                    pj.t003_socios_obrigacoes_sociais = _obrigacoesSociais;
                    pj.T003_IND_FILIAL_SEDE_FORA = _filialComSedeFora;
                    pj.T003_IND_UNIPESSOAL = _empresaUnipessoal;
                    pj.t003_dt_constituicao = _DataConstituicao;
                    pj.T003_in_reducao_capital = _inReducaoCapital;
                    pj.T003_ds_reducao_capital = _descricaoReducaoCapital;
                    pj.T003_ind_integralizando_cap = _integralizandoCapital;
                    pj.t003_ind_spe = _indicadorSPE;
                    pj.T003_ind_matriz = _indicadorMatriz;
                    pj.T003_in_end_estab = _prossuiEstabelecimento;
                    pj.Update();
                }
                #endregion

                //#region 9. Gravando Genprotocolo
                ////Informações complentares da alteração
                //_reqGenprotocolo.T005_NR_PROTOCOLO = _ProtocoloRequerimento;
                //_reqGenprotocolo.Update();

                //#endregion

                cp.CommitTransaction();


            }



        }

    }
}
